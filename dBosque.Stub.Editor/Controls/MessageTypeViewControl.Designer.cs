namespace dBosque.Stub.Editor.Controls
{
    partial class MessageTypeViewControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageTypeViewControl));
            this.contextMenuTemplate = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addStubButton = new System.Windows.Forms.ToolStripButton();
            this.addTemplateButton = new System.Windows.Forms.ToolStripButton();
            this.wsdlimportButton = new System.Windows.Forms.ToolStripButton();
            this.swaggerButton = new System.Windows.Forms.ToolStripButton();
            this.pasteButton = new System.Windows.Forms.ToolStripButton();
            this.deleteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.messageTypeView = new dBosque.Stub.Editor.CustomListView();
            this.columnnamespace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRootnode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPassThrough = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuTemplate.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuTemplate
            // 
            this.contextMenuTemplate.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuTemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.pasteToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuTemplate.Name = "contextMenuTemplate";
            this.contextMenuTemplate.Size = new System.Drawing.Size(128, 106);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stubToolStripMenuItem,
            this.messageTypeToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // stubToolStripMenuItem
            // 
            this.stubToolStripMenuItem.Name = "stubToolStripMenuItem";
            this.stubToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.stubToolStripMenuItem.Text = "Template";
            this.stubToolStripMenuItem.Click += new System.EventHandler(this.templateToolStripMenuItem_Click);
            // 
            // messageTypeToolStripMenuItem
            // 
            this.messageTypeToolStripMenuItem.Name = "messageTypeToolStripMenuItem";
            this.messageTypeToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.messageTypeToolStripMenuItem.Text = "MessageType";
            this.messageTypeToolStripMenuItem.Click += new System.EventHandler(this.messageTypeToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStubButton,
            this.addTemplateButton,
            this.wsdlimportButton,
            this.swaggerButton,
            this.pasteButton,
            this.deleteButton,
            this.toolStripSeparator2,
            this.refreshButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(186, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addStubButton
            // 
            this.addStubButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addStubButton.Image = ((System.Drawing.Image)(resources.GetObject("addStubButton.Image")));
            this.addStubButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addStubButton.Name = "addStubButton";
            this.addStubButton.Size = new System.Drawing.Size(24, 24);
            this.addStubButton.Text = "Add new stub";
            this.addStubButton.Click += new System.EventHandler(this.messageTypeToolStripMenuItem_Click);
            // 
            // addTemplateButton
            // 
            this.addTemplateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTemplateButton.Image = ((System.Drawing.Image)(resources.GetObject("addTemplateButton.Image")));
            this.addTemplateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTemplateButton.Name = "addTemplateButton";
            this.addTemplateButton.Size = new System.Drawing.Size(24, 24);
            this.addTemplateButton.Text = "Add new Template";
            this.addTemplateButton.Click += new System.EventHandler(this.templateToolStripMenuItem_Click);
            // 
            // wsdlimportButton
            // 
            this.wsdlimportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.wsdlimportButton.Image = ((System.Drawing.Image)(resources.GetObject("wsdlimportButton.Image")));
            this.wsdlimportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.wsdlimportButton.Name = "wsdlimportButton";
            this.wsdlimportButton.Size = new System.Drawing.Size(24, 24);
            this.wsdlimportButton.Text = "Import from Wsdl";
            this.wsdlimportButton.Click += new System.EventHandler(this.wsdlimportButton_Click);
            // 
            // swaggerButton
            // 
            this.swaggerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.swaggerButton.Image = ((System.Drawing.Image)(resources.GetObject("swaggerButton.Image")));
            this.swaggerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.swaggerButton.Name = "swaggerButton";
            this.swaggerButton.Size = new System.Drawing.Size(24, 24);
            this.swaggerButton.Text = "Import from Swagger";
            this.swaggerButton.Click += new System.EventHandler(this.swaggerButton_Click);
            // 
            // pasteButton
            // 
            this.pasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteButton.Image")));
            this.pasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(24, 24);
            this.pasteButton.Text = "Paste";
            this.pasteButton.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(24, 24);
            this.deleteButton.Text = "Delete";
            this.deleteButton.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(24, 24);
            this.refreshButton.Text = "Refresh";
            this.refreshButton.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.messageTypeView);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(605, 81);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(605, 108);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // messageTypeView
            // 
            this.messageTypeView.AllowDrop = true;
            this.messageTypeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnnamespace,
            this.columnRootnode,
            this.columnPassThrough,
            this.columnDescription});
            this.messageTypeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageTypeView.FullRowSelect = true;
            this.messageTypeView.HideSelection = false;
            this.messageTypeView.Location = new System.Drawing.Point(0, 0);
            this.messageTypeView.MultiSelect = false;
            this.messageTypeView.Name = "messageTypeView";
            this.messageTypeView.Size = new System.Drawing.Size(605, 81);
            this.messageTypeView.TabIndex = 0;
            this.messageTypeView.UseCompatibleStateImageBehavior = false;
            this.messageTypeView.View = System.Windows.Forms.View.Details;
            this.messageTypeView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.messageTypeView_ItemSelectionChanged);
            this.messageTypeView.Click += new System.EventHandler(this.MessageTypes_SelectedValueChanged);
            this.messageTypeView.DoubleClick += new System.EventHandler(this.messageTypeView_DoubleClick);
            this.messageTypeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.messageTypeView_KeyUp);
            this.messageTypeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.messageTypeView_MouseUp);
            // 
            // columnnamespace
            // 
            this.columnnamespace.Text = "Namespace / Regex";
            this.columnnamespace.Width = 169;
            // 
            // columnRootnode
            // 
            this.columnRootnode.Text = "Rootnode";
            this.columnRootnode.Width = 171;
            // 
            // columnPassThrough
            // 
            this.columnPassThrough.Text = "Passthrough";
            this.columnPassThrough.Width = 133;
            // 
            // columnDescription
            // 
            this.columnDescription.Text = "Description";
            this.columnDescription.Width = 151;
            // 
            // MessageTypeViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(605, 108);
            this.Controls.Add(this.toolStripContainer1);
            this.HideOnClose = true;
            this.Name = "MessageTypeViewControl";
            this.Load += new System.EventHandler(this.MessageTypeViewControl_Load);
            this.contextMenuTemplate.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomListView messageTypeView;
        private System.Windows.Forms.ColumnHeader columnnamespace;
        private System.Windows.Forms.ColumnHeader columnDescription;
        private System.Windows.Forms.ColumnHeader columnRootnode;
        private System.Windows.Forms.ColumnHeader columnPassThrough;
        private MetroFramework.Controls.MetroContextMenu contextMenuTemplate;
        //private System.Windows.Forms.ContextMenuStrip contextMenuTemplate;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem messageTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private System.Windows.Forms.ToolStripButton pasteButton;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripButton wsdlimportButton;
        private System.Windows.Forms.ToolStripButton addStubButton;
        private System.Windows.Forms.ToolStripButton addTemplateButton;
        private System.Windows.Forms.ToolStripButton swaggerButton;
    }
}
