using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using dBosque.Stub.Editor.Controls.Extensions;
using dBosque.Stub.Editor.Controls.Interfaces;
using dBosque.Stub.Editor.Flow;
using dBosque.Stub.Repository.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace dBosque.Stub.Editor.Controls
{
    public class TemplateViewController
    {
        private IEventBroker _broker;
        private IControlFactory _factory;
        private DockPanel _panel;
        private ToolStripMenuItem _menu;
        private IStubDataRepository _repository;

        public TemplateViewController(IEventBroker broker)
        {
            _broker = broker;
            _broker.Register(this);
        }

        [EventPublication("topic://PropertyFlowRequested")]
        public event EventHandler<PropertyFlowEventArgs> OnPropertyFlowRequested;

        /// <summary>
        /// Register the control factory
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public TemplateViewController RegisterFactory(IControlFactory factory)
        {
            // Handle separatly, otherwise we get a circular reference
            _factory = factory;
            return this;
        }

        /// <summary>
        /// Register the dockpanel
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public TemplateViewController RegisterPanel(DockPanel panel)
        {
            _panel = panel;
            return this;
        }

        /// <summary>
        /// Register the menu shortcut
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public TemplateViewController RegisterMenu(ToolStripMenuItem menu)
        {
            _menu = menu;
            return this;
        }


        /// <summary>
        /// Catch event when the repository changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription("topic://RepositoryChanged", typeof(OnPublisher))]
        public void OnRepositoryChanged(RepositoryFlowEventArgs e)
        {
            _repository = e.Item;
        }

        /// <summary>
        /// Request to close a specific view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription("topic://TemplateTreeView.Closed", typeof(OnPublisher))]
        public void OnWindowClosed(object sender, EventArgs e)
        {
            if (sender is TemplateTreeviewControl tree)
            {
                _menu.DropDownItems.RemoveByKey(tree.Text);
                OnPropertyFlowRequested.Invoke(PropertyFlowEventArgs.Empty);
                _broker.Unregister(tree);
            }
        }

        /// <summary>
        /// Close all documents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription("topic://Documents.CloseAll", typeof(OnPublisher))]
        public void OnCloseAll(EventArgs e)
        {           
            _panel.Documents.ToList().ForEach(document =>
            {
                // IMPORANT: dispose all panes.
                document.DockHandler.DockPanel = null;
                document.DockHandler.Close();
            });
        }

        /// <summary>
        /// Create a specific template view from the given persistString
        /// </summary>
        /// <param name="persistString"></param>
        /// <returns></returns>
        public TemplateTreeviewControl CreateFrom(string persistString)
        {
            string[] parsedStrings = persistString.Split(new char[] { ',' });
            if (parsedStrings.Length != 3)
                return null;

            if (parsedStrings[0] != typeof(TemplateTreeviewControl).ToString())
                return null;

            int id = -1;
            if (parsedStrings[1] != string.Empty)
                id = int.Parse(parsedStrings[1]);

            OnCreate(null, new MessageTypeFlowEventArgs() { Id = id });
            return _panel.Documents.FirstOrDefault(d => d.GetHashCode() == id) as TemplateTreeviewControl;
        }

        /// <summary>
        /// Create a new template view, or activate an existing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription("topic://MessageTypeActivated", typeof(OnPublisher))]
        public void OnCreate(object sender, MessageTypeFlowEventArgs e)
        {
            bool show = sender != null;
            // Check if the document already esists.
            var doc = _panel.Documents.FirstOrDefault(d => d.GetHashCode() == e.Item.Id);

            if (doc == null)
            {
                var tree = _factory.Create<TemplateTreeviewControl>();
                tree.Repository = _repository;
                tree.Select(e);
                if (show)
                    tree.Show(_panel, DockState.Document);

                _menu.DropDownItems.Add(tree.Text, null, (a, b) => tree.Activate()).Name = tree.Text;
            }
            else
            {
                doc.DockHandler.Activate();
            }
        }
    }
}
