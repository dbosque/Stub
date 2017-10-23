namespace dBosque.Stub.Editor.Forms
{
    partial class MainFormDockPanel
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormDockPanel));
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.visualStudioToolStripExtender1 = new WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender(this.components);
            this.vS2015DarkTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.pluginButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.dummyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.stripSoapEnvelopeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.connectionManager = new System.Windows.Forms.ToolStripButton();
            this.tenantsButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dummyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.autoStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetWindowLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeLlDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenantsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPanel1 = new System.Windows.Forms.ToolStripPanel();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStripPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockPanel1
            // 
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.Location = new System.Drawing.Point(0, 55);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(822, 266);
            this.dockPanel1.TabIndex = 0;
            // 
            // visualStudioToolStripExtender1
            // 
            this.visualStudioToolStripExtender1.DefaultRenderer = null;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginButton,
            this.toolStripSeparator3,
            this.stripSoapEnvelopeButton,
            this.toolStripSeparator2,
            this.connectionManager,
            this.tenantsButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(130, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // pluginButton
            // 
            this.pluginButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pluginButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dummyToolStripMenuItem});
            this.pluginButton.Image = ((System.Drawing.Image)(resources.GetObject("pluginButton.Image")));
            this.pluginButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pluginButton.Name = "pluginButton";
            this.pluginButton.Size = new System.Drawing.Size(34, 24);
            this.pluginButton.Text = "Plugins";
            // 
            // dummyToolStripMenuItem
            // 
            this.dummyToolStripMenuItem.Name = "dummyToolStripMenuItem";
            this.dummyToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.dummyToolStripMenuItem.Text = "Dummy";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // stripSoapEnvelopeButton
            // 
            this.stripSoapEnvelopeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stripSoapEnvelopeButton.Image = ((System.Drawing.Image)(resources.GetObject("stripSoapEnvelopeButton.Image")));
            this.stripSoapEnvelopeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stripSoapEnvelopeButton.Name = "stripSoapEnvelopeButton";
            this.stripSoapEnvelopeButton.Size = new System.Drawing.Size(24, 24);
            this.stripSoapEnvelopeButton.Text = "Strip soapenvelope";
            this.stripSoapEnvelopeButton.Click += new System.EventHandler(this.stripSoapEnvelopeButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // connectionManager
            // 
            this.connectionManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.connectionManager.Image = ((System.Drawing.Image)(resources.GetObject("connectionManager.Image")));
            this.connectionManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectionManager.Name = "connectionManager";
            this.connectionManager.Size = new System.Drawing.Size(24, 24);
            this.connectionManager.Text = "Connection Manager";
            this.connectionManager.Click += new System.EventHandler(this.connectionsToolStripMenuItem_Click);
            // 
            // tenantsButton
            // 
            this.tenantsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tenantsButton.Image = ((System.Drawing.Image)(resources.GetObject("tenantsButton.Image")));
            this.tenantsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tenantsButton.Name = "tenantsButton";
            this.tenantsButton.Size = new System.Drawing.Size(24, 24);
            this.tenantsButton.Text = "Tenants";
            this.tenantsButton.Click += new System.EventHandler(this.tenantsToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.pluginsToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.fileToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(822, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dummyToolStripMenuItem1});
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            // 
            // dummyToolStripMenuItem1
            // 
            this.dummyToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoStartToolStripMenuItem});
            this.dummyToolStripMenuItem1.Name = "dummyToolStripMenuItem1";
            this.dummyToolStripMenuItem1.Size = new System.Drawing.Size(136, 26);
            this.dummyToolStripMenuItem1.Text = "Dummy";
            // 
            // autoStartToolStripMenuItem
            // 
            this.autoStartToolStripMenuItem.CheckOnClick = true;
            this.autoStartToolStripMenuItem.Name = "autoStartToolStripMenuItem";
            this.autoStartToolStripMenuItem.Size = new System.Drawing.Size(149, 26);
            this.autoStartToolStripMenuItem.Text = "Auto start";
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetWindowLayoutToolStripMenuItem,
            this.closeLlDocumentsToolStripMenuItem,
            this.toolStripSeparator1});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.windowsToolStripMenuItem.Text = "&Window";
            // 
            // resetWindowLayoutToolStripMenuItem
            // 
            this.resetWindowLayoutToolStripMenuItem.Name = "resetWindowLayoutToolStripMenuItem";
            this.resetWindowLayoutToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.resetWindowLayoutToolStripMenuItem.Text = "&Reset Window Layout";
            this.resetWindowLayoutToolStripMenuItem.Click += new System.EventHandler(this.resetWindowLayoutToolStripMenuItem_Click);
            // 
            // closeLlDocumentsToolStripMenuItem
            // 
            this.closeLlDocumentsToolStripMenuItem.Name = "closeLlDocumentsToolStripMenuItem";
            this.closeLlDocumentsToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.closeLlDocumentsToolStripMenuItem.Text = "C&lose All Documents";
            this.closeLlDocumentsToolStripMenuItem.Click += new System.EventHandler(this.closeLlDocumentsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(224, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionManagerToolStripMenuItem,
            this.tenantsToolStripMenuItem1});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // connectionManagerToolStripMenuItem
            // 
            this.connectionManagerToolStripMenuItem.Name = "connectionManagerToolStripMenuItem";
            this.connectionManagerToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.connectionManagerToolStripMenuItem.Text = "&Connection manager";
            this.connectionManagerToolStripMenuItem.Click += new System.EventHandler(this.connectionsToolStripMenuItem_Click);
            // 
            // tenantsToolStripMenuItem1
            // 
            this.tenantsToolStripMenuItem1.Name = "tenantsToolStripMenuItem1";
            this.tenantsToolStripMenuItem1.Size = new System.Drawing.Size(222, 26);
            this.tenantsToolStripMenuItem1.Text = "&Tenants";
            this.tenantsToolStripMenuItem1.Click += new System.EventHandler(this.tenantsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripPanel1
            // 
            this.toolStripPanel1.Controls.Add(this.menuStrip1);
            this.toolStripPanel1.Controls.Add(this.toolStrip1);
            this.toolStripPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripPanel1.Location = new System.Drawing.Point(0, 0);
            this.toolStripPanel1.Name = "toolStripPanel1";
            this.toolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripPanel1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripPanel1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripPanel1.Size = new System.Drawing.Size(822, 55);
            // 
            // MainFormDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(822, 321);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.toolStripPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFormDockPanel";
            this.Text = "Stub Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripPanel1.ResumeLayout(false);
            this.toolStripPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender visualStudioToolStripExtender1;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme vS2015DarkTheme1;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton pluginButton;
        private System.Windows.Forms.ToolStripMenuItem dummyToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton stripSoapEnvelopeButton;
        private System.Windows.Forms.ToolStripPanel toolStripPanel1;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetWindowLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeLlDocumentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dummyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoStartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenantsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton connectionManager;
        private System.Windows.Forms.ToolStripButton tenantsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}