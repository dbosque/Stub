using dBosque.Stub.Editor.Models;
using System.Windows.Forms;

namespace dBosque.Stub.Editor.Controls.Models
{
    public static class ModelFormExtensions
    {
        /// <summary>
        /// Return the currently selected messagetype
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static MessageTypeItem Selected(this ListView view)
        {
            if (view.SelectedItems != null && view.SelectedItems.Count > 0)
                return view.SelectedItems[0].Tag as MessageTypeItem;
            return null;
        }

        /// <summary>
        /// Select a specific listviewitem
        /// </summary>
        /// <param name="view"></param>
        /// <param name="rootnode"></param>
        /// <param name="name"></param>
        public static void SelectWhere(this ListView view, string rootnode, string name)
        {
            view.SelectedItems.Clear();
            foreach (ListViewItem i in view.Items)
            {
                var t = i.Tag as MessageTypeItem;
                if (t?.NameSpace == name && t?.RootNode == rootnode)
                {
                    i.Selected = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Convert a messagetypeitem to a listview element
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ListViewItem AsListViewItem(this MessageTypeItem item)
        {
            ListViewItem it = new ListViewItem(item.NameSpace) { Tag = item };
            // The order of subitems, is the order on screen
            it.SubItems.AddRange(new string[] {  item.RootNode, item.PassThroughLayout, item.Description });
            return it;
        }
    }
}
