using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBosque.Stub.Editor
{
    using System;
    using System.Security.Permissions;
    using System.Windows.Forms;

    internal class CustomListView : ListView
    {
        public CustomListView() 
            : base()
        {
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        private const int WM_LBUTTONDBLCLK = 515;

        private bool doubleClickFired;

        ///<summary>Raises the <see cref="E:System.Windows.Forms.Control.DoubleClick" /> event.</summary>
        ///<param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            this.doubleClickFired = true;
        }

        ///<summary>Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
        ///<param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            this.doubleClickFired = false;

            base.WndProc(ref m);

            if (m.Msg == WM_LBUTTONDBLCLK && !this.doubleClickFired)
            {
                this.OnDoubleClick(EventArgs.Empty);
            }
        }
    }
}
