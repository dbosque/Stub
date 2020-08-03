using Appccelerate.EventBroker;
using dBosque.Stub.Editor.Controls.Errorhandling;
using dBosque.Stub.Editor.Controls.Extensions;
using dBosque.Stub.Editor.Controls.Interfaces;
using dBosque.Stub.Editor.Controls.Models;
using dBosque.Stub.Editor.Controls.Models.Converters;
using dBosque.Stub.Editor.Controls.Models.Descriptors;
using dBosque.Stub.Editor.Flow;
using dBosque.Stub.Editor.Models;
using dBosque.Stub.Editor.UserInteraction;
using dBosque.Stub.Repository.Interfaces;
using dBosque.Stub.Repository.StubDb.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls
{

    /// <summary>
    /// Specific nodeTree
    /// </summary>
    public partial class TemplateTreeviewControl : WeifenLuo.WinFormsUI.Docking.DockContent, IPropertyProvider
    {
        [EventPublication("topic://Error")]
        public event EventHandler<ErrorEventArgs> OnError;
        [EventPublication("topic://PropertyFlowRequested")]
        public event EventHandler<PropertyFlowEventArgs> OnPropertyFlowRequested;
        [EventPublication("topic://TemplateTreeView.Closed")]
        public event EventHandler OnViewClosed;

        public IStubDataRepository Repository { get; set; }
        private MessageTypeItem _item;
        private TreeNode _selectedNode = null;


        protected override string GetPersistString()
        {
            // Add extra information into the persist string for this document
            // so that it is available when deserialized.
            return GetType() + "," + GetHashCode() + "," + Text;
        }

        ///<summary>Serves as the default hash function. </summary>
        ///<returns>A hash code for the current object.</returns>
        public override int GetHashCode() => (int)(_item?.Id.Value ?? -1);

        /// <summary>
        /// Default constructor
        /// </summary>
        [DesignOnly(true)]
        public TemplateTreeviewControl()
        {
            // Force close on close
            HideOnClose = false;
            InitializeComponent();
            Activated += TemplateTreeviewControl_Activated;
            FormClosed += (s, e) => OnViewClosed.Invoke(this);
        }
        /// <summary>
        /// DI constructor
        /// </summary>
        /// <param name="broker"></param>
        public TemplateTreeviewControl(IEventBroker broker)
             : this()
        {
            broker.Register(this);
        }

        private void TemplateTreeviewControl_Activated(object sender, EventArgs e)
        {
            refreshButton.PerformClick();
            OnPropertyFlowRequested.Invoke(new PropertyFlowEventArgs(this));
        }
  
        private void HandleError(string message, string caption)
        {
            OnError?.Invoke(this, new ErrorEventArgs() { Message = message, Caption = caption });
        }

        public void Select(MessageTypeFlowEventArgs args)
        {
            if (args.Item == null && args.Id.HasValue)
                args.Item = new MessageTypeItem(Repository.GetMessageType(args.Id.Value));

            if (args.Item == null)
                return;
            _item = args.Item;
            UpdateTreeView(args.Id);
            if (!string.IsNullOrEmpty(_item.RootNode))
                Text = $"{_item.NameSpace}/{_item.RootNode}";
            else
                Text = $"{_item.NameSpace}";
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                if (e.Label != null && e.Label != e.Node.Text)
                {
                    e.Node.Text = e.Label;
                    SaveItem(e.Node);
                }
            }
            catch (Exception)
            {
                OnError.Invoke(new ErrorEventArgs("Error updating the node. Please refresh first.", "Error"));
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Nodes without a Tag are not selectable
            if (e.Node.Tag == null)
            {
                if (e.Node.Nodes.Count > 0)
                    treeView1.SelectedNode = e.Node.Nodes[0];
                else
                    treeView1.SelectedNode = e.Node.PrevVisibleNode;
                return;
            }
           
            _selectedNode = null;

            var t = e.Node.GetSelectedNodeTag();
            if (t == null)
                return;

            // Een valide Tag?
            _selectedNode = e.Node;

            // Afhankelijk van het type node moeten we andere dingen doen     
            switch (t.nodeType)
            {
                case NodeType.Response:
                    {
                        OnPropertyFlowRequested.Invoke(new PropertyFlowEventArgs(this));                     
                        break;
                    }
                case NodeType.TemplateDescription:
                case NodeType.XpathValue:
                case NodeType.CombinationDescription:
                    {
                        // Update de textbox om de filter aan te passen
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void treeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = (e.Node.Tag == null);
        }

        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            // Default remove
            treeView1.ContextMenuStrip = null;
            deleteButton.Enabled = cloneButton.Enabled = false;
            // Nodes without a Tag are not selectable
            if (e.Node.Tag == null)
            {
                e.Node.ExpandAll();
                e.Node.BackColor = treeView1.BackColor;
                return;
            }

            var tag = e.Node.GetSelectedNodeTag(NodeType.Response, NodeType.TemplateDescription);
            if (tag == null)
                return;

            // Indien response item, zowel delete, als create 
            if (tag.nodeType == NodeType.Response)
            {
                treeView1.ContextMenuStrip = contextMenuTreeView;
                if (!treeView1.ContextMenuStrip.Items.Contains(createNewToolStripMenuItem))
                    treeView1.ContextMenuStrip.Items.Add(createNewToolStripMenuItem);
                cloneButton.Enabled = deleteButton.Enabled  = true;

            }
            // Indien template item, alleen remove (create via messagetype)
            if (tag.nodeType == NodeType.TemplateDescription)
            {
                treeView1.ContextMenuStrip = contextMenuTreeView;
                treeView1.ContextMenuStrip.Items.Remove(createNewToolStripMenuItem);
                cloneButton.Enabled = false;
                deleteButton.Enabled = true;

            }
        }

        private void treeView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                e.Handled = e.SuppressKeyPress = true;
                UpdateTreeView();
            }
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = e.SuppressKeyPress = true;
                deleteToolStripMenuItem_Click(this, EventArgs.Empty);
            }
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UpdateTreeView();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tag = treeView1.SelectedNode.GetSelectedNodeTag(NodeType.Response, NodeType.TemplateDescription);
            if (tag == null)
                return;

            // Alleen response type kunnen worden verwijderd
            if (tag.nodeType == NodeType.Response)
                DeleteCombinatie(tag.Key);
            // Templates worden anders verwijderd
            if (tag.nodeType == NodeType.TemplateDescription)
                DeleteTemplate(tag.Key);
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NodeTag tag = treeView1.SelectedNode.GetSelectedNodeTag(NodeType.Response);
            if (tag != null)
                CreateCombinatieCopy(tag.Key);
        }

        private void DeleteTemplate(long key)
        {
            try
            {
                if (UserInteractor.DeleteTemplate.Allowed())
                {
                    if (Repository.HasSharedCombinationsWithOther(key))
                    {
                        if (!UserInteractor.DeleteTemplateAll.Allowed())
                            return;
                    }
                    Repository.DeleteTemplateDown(key);
                }
                Repository.Flush();
            }
            catch (Exception ex)
            {
                HandleError(ex.ToString(), "Warning");
            }
            // Refresh
            UpdateTreeView();
        }

        private void CreateCombinatieCopy(long key)
        {
            long? id = null;
            try
            {

                id = Repository.CloneResponseTree(key);
                Repository.Flush();
            }
            catch (Exception ex)
            {
                HandleError(ex.ToString(), "Warning");
            }

            // Refresh
            UpdateTreeView(id);
        }

        private void DeleteCombinatie(long key)
        {
            try
            {
                if (UserInteractor.DeleteCombination.Allowed())
                {
                    Repository.DeleteCombinationDown(key);

                    Repository.Flush();
                }
            }
            catch (Exception ex)
            {
                HandleError(ex.ToString(), "Warning");
            }
            // Refresh
            UpdateTreeView();

        }

        private void SaveItem(TreeNode editNode = null)
        {
            var text = (editNode ?? _selectedNode).Text;
            NodeTag t = (editNode ?? _selectedNode).GetSelectedNodeTag();
            var selected = _selectedNode;
            if (t == null)
                return;

            try
            {
                switch (t.nodeType)
                {
                    case NodeType.TemplateDescription:
                        {
                            Repository.UpdateTemplate(t.Key, a => a.Description = text);                    
                            break;
                        }
                    case NodeType.XpathValue:
                        {
                            Repository.UpdateCombinationXpath(t.Key, a => a.XpathValue = text); 
                            break;
                        }
                    case NodeType.Response:
                        {
                            Repository.UpdateCombination(t.Key, a => a.Description = text);
                            break;
                        }
                    default:
                        {
                            break;
                        }

                }
                Repository.Flush();
                // Refresh
                UpdateTreeView();

                treeView1.Select();
                treeView1.SelectedNode = selected;
                _selectedNode = selected;
                treeView_AfterSelect(treeView1, new TreeViewEventArgs(selected));
                OnPropertyFlowRequested.Invoke(new PropertyFlowEventArgs(this));
            }
            catch (Exception ex)
            {
                HandleError(ex.ToString(), "Warning");
            }

        }


        /// <summary>
        /// Update de TreeView met de gegevens uit de database
        /// </summary>
        private void UpdateTreeView(long? expandId = null)
        {
            var previousNode = treeView1.SelectedNode;

            // Clear all old items
            treeView1.Nodes.Clear();

            var nodes = GetTemplates().ToTreeNode().ToArray();
            treeView1.Nodes.AddRange(nodes);
            treeView1.Nodes.Flattened().Where(a => a.Tag == null).ToList().ForEach(a => a.NodeFont = new System.Drawing.Font(treeView1.Font, System.Drawing.FontStyle.Bold));

            if (expandId != null)
            {
                var s = treeView1.Nodes.Flattened().FirstOrDefault(a => (a.Tag as NodeTag)?.nodeType == NodeType.Response && (a.Tag as NodeTag)?.Key == expandId);
                if (s != null)
                {
                    previousNode = s.Nodes.Flattened().FirstOrDefault(a => (a.Tag as NodeTag)?.nodeType == NodeType.XpathValue) ?? previousNode;
                }
            }

            if (previousNode == null && treeView1.Nodes.Count > 0)
            {
                previousNode = treeView1.Nodes[0];
                treeView1.FullRowSelect = true;
                // No currently selected node
                _selectedNode = null;
            }
            ExpandTreeViewNodeByTag(previousNode);

        }

        private List<Template> GetTemplates()
        {
            // If nothing selected, no templates
            if (_item == null)
                return new List<Template>();

            return Repository.GetTemplates(_item.Id.Value).ToList();

        }

        private bool DoesNodeStillExists(TreeNode node, IEnumerable<TreeNode> nodes)
        {
            return nodes.Where(n => n.Tag is NodeTag)
                .Any(n => (n.Tag).Equals(node.Tag));
        }

        /// <summary>
        /// Expand the treeview so the request tag will be visable.
        /// </summary>
        /// <param name="node"></param>
        private void ExpandTreeViewNodeByTag(TreeNode node)
        {
            if (node == null)
                return;

            var nodes = treeView1.Nodes.Flattened();
            if (!DoesNodeStillExists(node, nodes))
                node = nodes.FirstOrDefault();

            if (node == null)
                return;

            var tag = node.Tag as NodeTag;
            // If no tag specified, do nothing
            if (tag == null)
                return;

            foreach (TreeNode n in nodes)
            {
                // Does it have a tag?
                if (n.Tag is NodeTag curTag)
                {
                    // Does the tag match?
                    if (curTag.Key == tag.Key &&
                        curTag.nodeType == tag.nodeType)
                    {
                        // Select and expand
                        treeView1.SelectedNode = n;
                        TreeNode cur = n;
                        while (cur != null)
                        {
                            cur.Expand();
                            cur = cur.Parent;
                        }
                    }
                }
            }
        }

        ///<summary>
        ///Save only the message from the property
        ///</summary>
        ///<param name="property">The item to get the message from</param>
        void IPropertyProvider.SaveMessage(IPropertyBase property)
        {
            var prop = property as ResponseProperty;
            Repository.UpdateResponse(prop.Id, a => {
                a.ResponseText = property.Message;
            });
        }

        ///<summary>
        ///Save a property to the database
        ///</summary>
        ///<param name="property">The item to save</param>
        void IPropertyProvider.Save(IPropertyBase property)
        {
            var prop = property as ResponseProperty;
            Repository.UpdateResponse(prop.Id, a =>
            {
                a.Combination.FirstOrDefault().Description = prop.Name;
                a.ResponseText = property.Message;
                a.ContentType = prop.ContentType;
                a.Headers = a.ToHeaders(prop.Headers);
                a.StatusCode = StatusCodeConverter.Transpose(prop.StatusCode);
                a.Combination.FirstOrDefault().CombinationXpath.ToList().ForEach(x => x.XpathValue = prop.Matches[x.Xpath.CleanExpression]?.Value );
            });
            UpdateTreeView(expandId: prop.Id);
        }

        ///<summary>
        ///Load the property from the database.
        ///</summary>
        ///<returns></returns>
        IPropertyBase IPropertyProvider.Load()
        {
            if (_selectedNode == null)
                return null;
            var res = Repository.GetResponse(_selectedNode.GetSelectedNodeTag().Key);

            var matches = new MatchPropertyCollection(
                res.Combination.FirstOrDefault().CombinationXpath.Select(
                    m => new MatchProperty(m.Xpath.CleanExpression, m.XpathValue, m.Xpath.TypeToName())));

            return new ResponseProperty()
            {
                Id = res.ResponseId,
                Name = res.Combination.FirstOrDefault().Description,
                Headers = res.ToHeadersStringArray(),
                ContentType = res.ContentType ?? string.Empty,
                StatusCode = StatusCodeConverter.Transpose(res.StatusCode),
                Message = res.ResponseText,
                Matches = matches
            };           
        }
    }
}
