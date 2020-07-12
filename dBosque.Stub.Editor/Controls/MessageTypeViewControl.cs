using Appccelerate.EventBroker;
using dBosque.Stub.Editor.Controls.Behaviours;
using dBosque.Stub.Editor.Controls.Errorhandling;
using dBosque.Stub.Editor.Controls.Extensions;
using dBosque.Stub.Editor.Controls.Interfaces;
using dBosque.Stub.Editor.Controls.Models;
using dBosque.Stub.Editor.Flow;
using dBosque.Stub.Editor.Models;
using dBosque.Stub.Editor.UserInteraction;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Services.Extensions;
using dBosque.Stub.Services.ExternalReferenceResolvers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls
{
    public partial class MessageTypeViewControl :WeifenLuo.WinFormsUI.Docking.DockContent, IPropertyProvider, IMessageTypeViewEvents
    {
        public event EventHandler<ErrorEventArgs> OnError;
        public event EventHandler<PropertyFlowEventArgs> OnPropertyFlowRequested;     
        public event EventHandler<MessageTypeFlowEventArgs> OnMessageTypeActivated;

        /// <summary>
        /// De items in de listview, wordt gebruikt ter controle
        /// </summary>
        private List<MessageTypeItem> _typeList = null;

        /// <summary>
        /// Original color for the background, used to signal newly available data
        /// </summary>
        private Color messageTypeViewOrginalColor;
        
        /// <summary>
        /// Check each x ms for new data.
        /// </summary>
        private PeriodicEventBehaviour _tick;

        /// <summary>
        /// Drag drop behaviour in a generic way
        /// </summary>
        private DragDropPasteBehaviour _dragdrop;

        /// <summary>
        /// The data repository
        /// </summary>
        private IStubDataRepository _repository;

        /// <summary>
        /// The currently selected item
        /// </summary>
        private MessageTypeItem _selectedItem = null;
        private MessageTypeItem Selected => messageTypeView.Selected() ?? _selectedItem;

        /// <summary>
        /// A factory to resolve external messagetype resolvers (WSDL/Swagger)
        /// </summary>
        private ExternalResolverFactory _resolverFactory = new ExternalResolverFactory();
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="broker"></param>
        public MessageTypeViewControl(IEventBroker broker)
        {
            broker.Register(this);

            InitializeComponent();
            deleteButton.Enabled = addTemplateButton.Enabled = false;
            Activated += MessageTypes_SelectedValueChanged;
            messageTypeViewOrginalColor = messageTypeView.BackColor;
            _tick = new PeriodicEventBehaviour(this, CheckNewData);
            ApplyTheme();
        }

        /// <summary>
        /// Timer to check for new data
        /// </summary>
        private void CheckNewData()
        {
            if (_repository == null)
                return;

            var newData = _repository.GetMessageTypes().ToList();
            bool same = (newData.Count == _typeList.Count);
            newData.RemoveAll(a => _typeList.Select(t => t.Id).Contains(a.MessageTypeId));
            same &= newData.Count == 0;
            _dragdrop = new DragDropPasteBehaviour(messageTypeView, MessageTypes_DragDrop);
            messageTypeView.BackColor = same ? messageTypeViewOrginalColor : Color.AliceBlue;

        }


    
        public void OnRepositoryChanged(RepositoryFlowEventArgs e)
        {
            _repository = e.Item;
            LoadMessageTypes();
        }


        private void UpdateTreeView(long? expandId = null)
        {
            OnMessageTypeActivated.SafeInvoke(this, new MessageTypeFlowEventArgs() { Item = Selected, Id = expandId });           
        }

        public void CreateMessageTypeFor( CreateMessageTypeFlowEventArgs args)
        {
            if (string.IsNullOrEmpty(args.Rootnode) && string.IsNullOrEmpty(args.Namespace))
                CreateMessageTypeOfFileContent(args.Content);
            else
            {
                messageTypeView.SelectWhere(args.Namespace, args.Rootnode);               
                CreateTemplate(args.Content);
            }
        }

        private void LoadMessageTypes()
        {
            try
            {
                // Zeker zijn dat de View leeg is.
                messageTypeView.Items.Clear();

                // Get the unique messagetypes from the database
                _typeList = new List<MessageTypeItem>();

                _typeList.AddRange(
                    _repository.GetMessageTypes()
                    .OrderBy(m => m.Namespace)
                    .Select(t => new MessageTypeItem(t)));


                // Fill the ListView
                _typeList.ForEach(i => messageTypeView.Items.Add(i.AsListViewItem()));

                messageTypeView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                messageTypeView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message);
                // Show messagebox and exit
                //OnError?.Invoke(this, new ErrorEventArgs(ex));
            }

        }

        /// <summary>
        /// Create a messagetype based on an xml file
        /// </summary>
        /// <param name="content"></param>
        private void CreateMessageTypeOfFileContent(string content)
        {
            if (string.IsNullOrEmpty(content))
                return;

            try
            {
                if (content.EndsWith("wsdl", StringComparison.OrdinalIgnoreCase))
                {
                    var v = new UriBuilder(content);
                    var form = new AddMessageTypesControl(content, _repository);
                    form.OnSave += (a, b) => LoadMessageTypes();
                    form.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                    throw new UriFormatException();
            }
            catch (UriFormatException)
            {
                try
                {
                    var doc = content.GetDocumentInfo(GlobalSettings.Instance.StripSoapEnvelope);

                    // Zorg dat ze niet bestaan.
                    if (_typeList.Exists(t => t.NameSpace == doc.Namespace && t.RootNode == doc.RootNode))
                        UserInteractor.NamespaceExists.Warning(doc.Namespace, doc.RootNode);
                    else
                    {
                        string input = string.Empty;
                        // Indien ok, nog eens vragen of het goed is.
                        if (new NewMessageTypeDescriptionInputBox(doc.Namespace, doc.RootNode).AskInput(out input, doc.RootNode))
                        {
                            if (UserInteractor.NamesspaceCreated.Allowed(doc.Namespace, doc.RootNode, input))
                            {
                                // Voeg maar toe
                                _repository.UpdateMessageType(null, a =>
                                {
                                    a.Description = input;
                                    a.Rootnode = doc.RootNode;
                                    a.Namespace = doc.Namespace;
                                    a.Sample = content;
                                    a.PassthroughEnabled = false;
                                });
                                _repository.Flush();
                            }
                        }
                        // Reload messageType view
                        LoadMessageTypes();
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(this, new ErrorEventArgs(ex));                   
                }
            }
        }


        private void CreateTemplate(string content = null)
        {
            // Is er iets geselecteerd
            if (messageTypeView.SelectedItems.Count == 0)
                return;

            // Which message are we dealing with
            // Which type is seleced
            MessageTypeItem selectedMessageType = Selected;

            var form = new AddTemplateControl() {
                SelectedMessageType = selectedMessageType,
                Repository = _repository,
                Content = content.IfEmpty(selectedMessageType.Sample)
            };            
            form.OnSave += (a, b) => UpdateTreeView(b.Id);
            form.OnError += (s, e) => OnError?.Invoke(s, e);
            form.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);

        }

        private void messageTypeView_DoubleClick(object sender, EventArgs e)
        {
            // Haal de data op uit de database.
            UpdateTreeView();
        }

        private void messageTypeView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected && messageTypeView.Selected() == null)
                OnPropertyFlowRequested(this, PropertyFlowEventArgs.Empty);

            deleteButton.Enabled = addTemplateButton.Enabled = e.IsSelected || (messageTypeView.Selected() != null);
        }

        private void MessageTypes_SelectedValueChanged(object sender, EventArgs e)
        {            
            _selectedItem = messageTypeView.Selected();
            if (_selectedItem != null)
                OnPropertyFlowRequested.Invoke(new PropertyFlowEventArgs(this));
    
        }


        private void messageTypeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                (contextMenuTemplate.Items["addToolStripMenuItem"] as ToolStripDropDownItem).DropDownItems["stubToolStripMenuItem"].Visible =
                contextMenuTemplate.Items["deleteToolStripMenuItem"].Visible =
                contextMenuTemplate.Items["toolStripSeparator1"].Visible = messageTypeView.GetItemAt(e.X, e.Y) != null;

                (contextMenuTemplate.Items["addToolStripMenuItem"] as ToolStripDropDownItem).DropDownItems["messageTypeToolStripMenuItem"].Visible = messageTypeView.GetItemAt(e.X, e.Y) == null;

                Point pos = this.PointToClient(Cursor.Position);
                contextMenuTemplate.Show(this, pos);
            }
           
        }

        private void messageTypeView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                e.Handled = e.SuppressKeyPress = true;
                LoadMessageTypes();
            }
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = e.SuppressKeyPress = true;
                deleteToolStripMenuItem_Click(this, EventArgs.Empty);
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                MessageTypes_SelectedValueChanged(this, EventArgs.Empty);
            }
        }

        private void MessageTypes_DragDrop(object sender, DragDropPasteContentEventArgs a)
        {
            CreateMessageTypeOfFileContent(a.Data);
        }

        private void messageTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new AddEditMessageTypeForm()
            {
                Message = messageTypeView.Selected()??new MessageTypeItem()
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var m = dialog.Message;
                if (_typeList.Exists(t => t.NameSpace == m.NameSpace && t.RootNode == m.RootNode && t.Id != m.Id))
                    UserInteractor.NamespaceExists.Warning(m.NameSpace, m.RootNode);
                else
                {
                    if (UserInteractor.NamesspaceCreated.Allowed(m.NameSpace, m.RootNode, m.Description))
                    {
                        _repository.UpdateMessageType(m.Id, a => {
                            a.Description = m.Description;
                            a.Rootnode = m.RootNode;
                            a.Namespace = m.NameSpace;
                            a.PassthroughEnabled = m.AllowPassthrough;
                            a.PassthroughUrl = m.PassthroughUri;
                            a.Sample = m.Sample;

                        });

                        // Reload messageType view
                        LoadMessageTypes();
                    }
                }

            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageTypeItem selectedMessageType = Selected;
            if (selectedMessageType != null)
            {
                if (_repository.GetTemplates(selectedMessageType.Id.Value).Any())
                {
                    UserInteractor.UnableToDeleteMessageType.Warning(selectedMessageType.Description);
                }
                else
                {
                    if (UserInteractor.DeleteMessageType.Allowed(selectedMessageType.Description))
                    {
                        _repository.DeleteMessageType(selectedMessageType.Id.Value);
                        _repository.Flush();
                        LoadMessageTypes();
                    }
                }

            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateMessageTypeOfFileContent(Clipboard.GetDataObject().GetFileDropData());
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMessageTypes();
        }

        private void templateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTemplate();
        }

        private void MessageTypeViewControl_Load(object sender, EventArgs e)
        {
          //  LoadMessageTypes();
        }

        ///<summary>
        ///Save only the message from the property
        ///</summary>
        ///<param name="property">The item to get the message from</param>
        void IPropertyProvider.SaveMessage(IPropertyBase property)
        {
            var prop = property as MessageTypeProperty;
            _repository.UpdateMessageType(prop.Id, a => {             
                a.Sample = property.Message;
            });
        }

        ///<summary>
        ///Save a property to the database
        ///</summary>
        ///<param name="property">The item to save</param>
        void IPropertyProvider.Save(IPropertyBase property)
        {
            var prop = property as MessageTypeProperty; 
            _repository.UpdateMessageType(prop.Id, a => {
                a.Description = prop.Description;
                a.Rootnode = prop.RootNode;
                a.Namespace = prop.NameSpace;
                a.PassthroughEnabled = prop.AllowPassthrough;
                a.PassthroughUrl = prop.PassthroughUri;
                

            });

            LoadMessageTypes();
        }

        ///<summary>
        ///Load the property from the database.
        ///</summary>
        ///<returns></returns>
        IPropertyBase IPropertyProvider.Load()
        {
            return new MessageTypeProperty(Selected.Org);
        }

        private void wsdlimportButton_Click(object sender, EventArgs e)
        {
            string input = string.Empty;
            if (new WsdlInputBox().AskInput(out input, "https://artikel.borger.dk/ArticleExport.svc?wsdl"))
            {
                var form = new AddMessageTypesControl(input, _repository, _resolverFactory.Create("wsdl"));
                form.OnSave += (a, b) => LoadMessageTypes();
                form.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
        }

        private void swaggerButton_Click(object sender, EventArgs e)
        {
            string input = string.Empty;
            if (new SwaggerInputBox().AskInput(out input, "http://petstore.swagger.io/v2/swagger.json"))
            {
                var form = new AddMessageTypesControl(input, _repository, _resolverFactory.Create("swagger"));
                form.OnSave += (a, b) => LoadMessageTypes();
                form.Show(DockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
        }
    }
}
