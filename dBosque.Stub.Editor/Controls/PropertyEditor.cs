using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using dBosque.Stub.Editor.Controls.Interfaces;
using dBosque.Stub.Editor.Controls.Models;
using dBosque.Stub.Editor.Flow;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls
{
    public partial class PropertyEditor : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public PropertyEditor(IEventBroker broker)
        {
            broker.Register(this);
            InitializeComponent();       
        }

        private IPropertyProvider _controller;

        [EventSubscription("topic://PropertyFlowRequested", typeof(OnPublisher))]
        public void OnControllerFlow(PropertyFlowEventArgs args)
        {
            LoadController(args.Provider);            
        }

        private void LoadController(IPropertyProvider controller)
        {        
            _controller = controller;
            propertyGrid1.SelectedObject = _controller?.Load();
            ExpandGroup(propertyGrid1);

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {            
            if (propertyGrid1.SelectedObject != null && _controller != null)
                _controller.Save(propertyGrid1.SelectedObject as IPropertyBase);
        }


        private void ExpandGroup(GridItem g, int level)
        {
            if (level == 0)
                return;
            if (g.Expandable && !g.Expanded)
            {
                g.Expanded = true;
                level--;
            }
            foreach (GridItem i in g.GridItems)
                ExpandGroup(i, level);
        }
        private void ExpandGroup(PropertyGrid propertyGrid, int level = 1)
        {
            GridItem root = propertyGrid.SelectedGridItem;
            if (root == null)
                return;
            //Get the parent
            while (root.Parent != null)
                root = root.Parent;

            if (root != null)
                ExpandGroup(root, level);
        }
    }
}
