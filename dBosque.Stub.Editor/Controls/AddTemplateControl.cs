using Appccelerate.EventBroker;
using dBosque.Stub.Editor.Controls.Behaviours;
using dBosque.Stub.Editor.Controls.Errorhandling;
using dBosque.Stub.Editor.Controls.Extensions;
using dBosque.Stub.Editor.Flow;
using dBosque.Stub.Editor.Models;
using dBosque.Stub.Editor.UserInteraction;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using DB = dBosque.Stub.Repository.StubDb.Entities;

namespace dBosque.Stub.Editor.Controls
{
    /// <summary>
    /// Form to create new Templates and Xpaths
    /// </summary>
    public partial class AddTemplateControl : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region Constructor
        public AddTemplateControl()
        {
            _dom = null;
            InitializeComponent();
            UpdateToolbarButtons();
            _dragDrop = new DragDropPasteBehaviour(documentTreeView, OnDragDrop);
        }

        private void UpdateToolbarButtons()
        {
            saveButton.Enabled = !string.IsNullOrEmpty(DescriptionTextBox.Text) && (XpathLstbx.SelectedItems.Count > 0 || !string.IsNullOrEmpty(regexTb.Text)) && !saved;
            clearButton.Enabled = _dom != null;
            deleteButton.Enabled = XpathLstbx.SelectedItems.Count > 0;
        }
        #endregion

        #region Private members

        private DragDropPasteBehaviour _dragDrop;
        private bool saved = false;
        /// <summary>
        /// The xml document loaded in the treeview
        /// </summary>
        XmlDocument _dom;

        #endregion

        #region Public properties

        public event FlowHandler<FlowEventArgs> OnSave;

        [EventPublication("topic://Error")]
        public event EventHandler<ErrorEventArgs> OnError;

        public bool StripSoapEnvelope { get; set; }
        public IStubDataRepository Repository { get; set; }
        /// <summary>
        /// The Xpath values selected in the listbox
        /// </summary>
        public List<DB.Xpath> SelectedXpaths { get; private set; }

        /// <summary>
        /// The messagetype for which a template is needed.
        /// </summary>
        public MessageTypeItem SelectedMessageType { set; private get; }

        /// <summary>
        /// The description of the Template
        /// </summary>
        public string Description { private set; get; }     

        public string Content { get; set; }

        #endregion

        #region EventHandlers

        #region OkButton
        /// <summary>
        /// User pressed ok, return selected xpaths
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkBtn_Click(object sender, EventArgs e)
        {
            SelectedXpaths = new List<DB.Xpath>(XpathLstbx.SelectedItems.Cast<DB.Xpath>().ToArray());        
            Description = DescriptionTextBox.Text;

            // Checking
            if (SelectedXpaths.Count <= 0 && string.IsNullOrEmpty(regexTb.Text))
                UserInteractor.NoXpathsRegExSelected.Warning();
            else if (string.IsNullOrEmpty(Description))
                UserInteractor.NoDescription.Warning();
            else
            {
                if (!string.IsNullOrEmpty(regexTb.Text))
                {
                    var regex = Repository.GetXpaths().Where(x => x.Type == 2).FirstOrDefault(a => a.Expression == regexTb.Text);
                    if (regex == null )
                        regex = Repository.UpdateXpath(null, a => { a.Expression = regexTb.Text; a.Type = 2; a.Description = string.Empty; });
                    SelectedXpaths = new List<DB.Xpath> { regex };
                }
                try
                {
                    var template = Repository.UpdateTemplate(null, t =>
                    {
                        t.MessageTypeId = SelectedMessageType.Id.Value;
                        t.Description = Description;
                        t.TemplateXpath.AddRange(SelectedXpaths.Select(x => new DB.TemplateXpath() { XpathId = x.XpathId }));

                    });
                    var combo = Repository.UpdateCombination(null, c =>
                    {
                        c.TemplateId = template.TemplateId;
                        c.MessageTypeId = SelectedMessageType.Id.Value;
                        c.Response = new DB.Response() { Description = Description, ResponseText = "<EMPTY_RESPONSE/>" };
                        c.CombinationXpath.AddRange(SelectedXpaths.Select(x => new DB.CombinationXpath() { XpathId = x.XpathId, XpathValue = "<EMPTY>" }));
                    });

                    Repository.UpdateTemplate(template.TemplateId, t => t.Combination.Add(combo));

                    Repository.Flush();
                    OnSave?.Invoke(this, new FlowEventArgs() { Id = combo.CombinationId });
                    saved = true;
                    this.Text = Description;
                    documentTreeView.Enabled = XpathLstbx.Enabled = DescriptionTextBox.Enabled = false;
                    this.Close();
                    
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(this, new ErrorEventArgs() { Message = ex.ToString(), Caption = "Warning" });
                }
                
            }
            UpdateToolbarButtons();
        }
        #endregion

        #region Cancel button

        /// <summary>
        /// User pressed cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region CreateXpath
        /// <summary>
        /// Add a new Xpath to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void documentTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                // Get the Xpath for the given Node
                var node = (XmlNode)e.Node.Tag;

                string path = node.FindXPath();
                if (GlobalSettings.Instance.StripSoapEnvelope)
                    path.StripSoapEnvelope();
                
             
                // Already exists?
                if (!Repository.GetXpaths().Any(x => string.Compare(x.Expression, path, StringComparison.Ordinal) == 0))
                    {
                        if (UserInteractor.AddXpathToList.Ask(path.RemoveLocalNamespace()))
                        {
                            // Create a new Xpath and refresh the listbox
                            Repository.UpdateXpath(null, a => { a.Expression = path; a.Description = string.Empty; a.Type = 0; });
                            RefreshListBox();
                        }
                    }
                    else
                    UserInteractor.XpathExists.Warning(path.RemoveLocalNamespace());
                
            }
            catch (Exception ex)
            {
                OnError(this, new ErrorEventArgs() { Caption = "Warning", Message = ex.ToString() });
            }
            UpdateToolbarButtons();
        }

        #endregion

        #region Clear Tree view
        /// <summary>
        /// Clear the treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            documentTreeView.Nodes.Clear();
            _dom = null;
            RefreshListBox();
            UpdateToolbarButtons();
        }
        #endregion

        #region DragDrop

        /// <summary>
        /// DragDrop event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragDrop(object sender, DragDropPasteContentEventArgs e)
        {
            LoadDataInTreeView(e.Data);
            toolTip1.SetToolTip(documentTreeView, "");
            RefreshListBox();
            UpdateToolbarButtons();
        }
        #endregion

        #region DeleteXpath
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (XpathLstbx.SelectedItems.Count > 1)
                    UserInteractor.XpathDeleteOneByOne.Warning();
                else if (XpathLstbx.SelectedItems.Count == 0)
                    UserInteractor.NoXpathsSelected.Warning();
                else
                {
                    var path = (DB.Xpath)XpathLstbx.SelectedItems[0];
                    if (path != null)
                    {
                        // Check to see if no one uses this xpath
                        if (Repository.IsInUse(path))
                            UserInteractor.XpathStillInUse.Warning(path.Expression);
                        else
                            if (UserInteractor.XpathDeleteConfirmation.Allowed(path.Expression))
                        {
                            Repository.DeleteXpath(path);
                            // Refresh the UI
                            RefreshListBox();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning");
            }
            UpdateToolbarButtons();
        }
        #endregion

        #endregion

        #region Fill the Xpath Listbox

        private IEnumerable<DB.Xpath> GetXPathsForRegEx(string pattern, int type)
        {
            var regex = new Regex(SelectedMessageType.NameSpace);
            var groups = regex.GetNamedGroups();
            if (groups.Any())
            {
                // Add all new regex groups
                var regexes = Repository.GetXpaths().Where(x => x.Type == type).Where(a => groups.Contains(a.Expression));
                if (regexes.Count() != groups.Count())
                {
                    foreach (var gr in groups)
                    {
                        if (!regexes.Any(a => a.Expression == gr))
                            Repository.UpdateXpath(null, a => { a.Expression = gr; a.Type = type; a.Description = string.Empty; });
                    }
                }
                // Reload
                return Repository.GetXpaths().Where(x => x.Type == type).Where(a => groups.Contains(a.Expression));
            }
            return Enumerable.Empty<DB.Xpath>();
        }
        /// <summary>
        /// Refresh the items in the xpathlistbox
        /// </summary>
        private void RefreshListBox()
        {
            var list = Repository.GetXpaths().Where(x => x.Type == 0).ToList();
            // All xpaths nicely sorted
            if (_dom != null)
                list = _dom.GetAllValidFor(list, (x) => x.Expression ,GlobalSettings.Instance.StripSoapEnvelope).ToList();

          
            list.AddRange(GetXPathsForRegEx(SelectedMessageType.NameSpace, 1));

            XpathLstbx.DataSource = list.OrderBy(a => a.Expression).ToList();    
        }
        #endregion
        /// <summary>
        /// Load a file in the treeView
        /// </summary>
        /// <param name="content"></param>
        private void LoadDataInTreeView(string content)
        {
            if (string.IsNullOrEmpty(content))
                return;

            try
            {
                var doc = content.GetDocumentInfo(GlobalSettings.Instance.StripSoapEnvelope);
                if (doc.Document?.DocumentElement != null)
                {
                    _dom = doc.Document;
                    // Load the XML into the TreeView. 
                    this.documentTreeView.Nodes.Clear();
                    this.documentTreeView.Nodes.Add(new TreeNode(_dom.DocumentElement.Name) { Tag = _dom.DocumentElement });
                    // Add the rest of the nodes to the treeview
                    _dom.DocumentElement.AddNodesToTree(this.documentTreeView.Nodes[0]);
                    // Show them all.
                    this.documentTreeView.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            UpdateToolbarButtons();
        }     

      
        private void XpathLstbx_DoubleClick(object sender, EventArgs e)
        {
            if (XpathLstbx.SelectedItems.Count == 0)
                UserInteractor.NoXpathsSelected.Warning();
            else
            {
                var path = (DB.Xpath)XpathLstbx.SelectedItems[0];
                if (path != null)
                {
                    // Check to see if no one uses this xpath
                    if (!Repository.IsInUse(path) || UserInteractor.XpathChange.Allowed(path.CleanExpression))
                    {
                        string input = string.Empty;
                        if (new ChangeXpathInputBox(path.Expression).AskInput(out input, path.Expression))
                            Repository.UpdateXpath(path.XpathId, p => p.Expression = input);                    
                        // Refresh the UI
                        RefreshListBox();
                    }
                }
            }
            UpdateToolbarButtons();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            var data = Clipboard.GetDataObject();
            string content = data.GetFileDropData();
            LoadDataInTreeView(content);
            this.toolTip1.SetToolTip(this.documentTreeView, "");
            RefreshListBox();
            UpdateToolbarButtons();
        }

        private void AddTemplateForm_Load(object sender, EventArgs e)
        {
            namespaceTb.Text = SelectedMessageType.NameSpace;
            if (!string.IsNullOrEmpty(Content))
            {
                LoadDataInTreeView(Content);
                this.toolTip1.SetToolTip(this.documentTreeView, "");
            }
            this.Text = "New template";
            RefreshListBox();
            UpdateToolbarButtons();
        }

        private void DescriptionTextBox_Leave(object sender, EventArgs e)
        {
            UpdateToolbarButtons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var regex = new Regex($"^{regexTb.Text}$");
                var isValid = regex.Match(regexTestTb.Text);
                if (regex.GetNamedGroups().Length == 0)
                {
                    UserInteractor.RegexNoNamedGroups.Error();
                }
                else if (isValid.Success && isValid.Groups.Count > 1)
                {
                    StringBuilder builder = new StringBuilder();
                    for (int i = 1; i < isValid.Groups.Count; i++)
                        builder.AppendFormat("{0}:{1}{2}", regex.GroupNameFromNumber(i), isValid.Groups[i].Value, Environment.NewLine);

                    UserInteractor.RegexMatch.Info(builder.ToString());

                }
                else
                {
                    UserInteractor.RegexNoMatch.Error();
                }
            }
            catch (Exception ex)
            {
                UserInteractor.RegexError.Error(ex.Message);
            }
        }
    }
}
