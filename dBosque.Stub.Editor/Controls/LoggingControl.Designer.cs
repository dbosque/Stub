namespace dBosque.Stub.Editor.Controls
{
    partial class LoggingControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggingControl));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.responseDatumTijdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uriDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requestDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.responseTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.templateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.combinationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namespaceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rootnodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenantDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.matchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noMatchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stpselectLogResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.severityButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.allToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.matchesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.noMatchesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.playstopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stpselectLogResultBindingSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.responseDatumTijdDataGridViewTextBoxColumn,
            this.uriDataGridViewTextBoxColumn,
            this.requestDataGridViewTextBoxColumn,
            this.responseTextDataGridViewTextBoxColumn,
            this.templateDataGridViewTextBoxColumn,
            this.combinationDataGridViewTextBoxColumn,
            this.namespaceDataGridViewTextBoxColumn,
            this.rootnodeDataGridViewTextBoxColumn,
            this.tenantDataGridViewTextBoxColumn});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DataSource = this.stpselectLogResultBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(803, 149);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            // 
            // responseDatumTijdDataGridViewTextBoxColumn
            // 
            this.responseDatumTijdDataGridViewTextBoxColumn.DataPropertyName = "ResponseDatumTijd";
            this.responseDatumTijdDataGridViewTextBoxColumn.HeaderText = "Time";
            this.responseDatumTijdDataGridViewTextBoxColumn.Name = "responseDatumTijdDataGridViewTextBoxColumn";
            this.responseDatumTijdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // uriDataGridViewTextBoxColumn
            // 
            this.uriDataGridViewTextBoxColumn.DataPropertyName = "Uri";
            this.uriDataGridViewTextBoxColumn.HeaderText = "Uri";
            this.uriDataGridViewTextBoxColumn.Name = "uriDataGridViewTextBoxColumn";
            this.uriDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // requestDataGridViewTextBoxColumn
            // 
            this.requestDataGridViewTextBoxColumn.DataPropertyName = "Request";
            this.requestDataGridViewTextBoxColumn.HeaderText = "Request";
            this.requestDataGridViewTextBoxColumn.Name = "requestDataGridViewTextBoxColumn";
            this.requestDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // responseTextDataGridViewTextBoxColumn
            // 
            this.responseTextDataGridViewTextBoxColumn.DataPropertyName = "ResponseText";
            this.responseTextDataGridViewTextBoxColumn.HeaderText = "Response";
            this.responseTextDataGridViewTextBoxColumn.Name = "responseTextDataGridViewTextBoxColumn";
            this.responseTextDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // templateDataGridViewTextBoxColumn
            // 
            this.templateDataGridViewTextBoxColumn.DataPropertyName = "Template";
            this.templateDataGridViewTextBoxColumn.HeaderText = "Template";
            this.templateDataGridViewTextBoxColumn.Name = "templateDataGridViewTextBoxColumn";
            this.templateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // combinationDataGridViewTextBoxColumn
            // 
            this.combinationDataGridViewTextBoxColumn.DataPropertyName = "Combination";
            this.combinationDataGridViewTextBoxColumn.HeaderText = "Combination";
            this.combinationDataGridViewTextBoxColumn.Name = "combinationDataGridViewTextBoxColumn";
            this.combinationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // namespaceDataGridViewTextBoxColumn
            // 
            this.namespaceDataGridViewTextBoxColumn.DataPropertyName = "Namespace";
            this.namespaceDataGridViewTextBoxColumn.HeaderText = "Namespace";
            this.namespaceDataGridViewTextBoxColumn.Name = "namespaceDataGridViewTextBoxColumn";
            this.namespaceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rootnodeDataGridViewTextBoxColumn
            // 
            this.rootnodeDataGridViewTextBoxColumn.DataPropertyName = "Rootnode";
            this.rootnodeDataGridViewTextBoxColumn.HeaderText = "Rootnode";
            this.rootnodeDataGridViewTextBoxColumn.Name = "rootnodeDataGridViewTextBoxColumn";
            this.rootnodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tenantDataGridViewTextBoxColumn
            // 
            this.tenantDataGridViewTextBoxColumn.DataPropertyName = "Tenant";
            this.tenantDataGridViewTextBoxColumn.HeaderText = "Tenant";
            this.tenantDataGridViewTextBoxColumn.Name = "tenantDataGridViewTextBoxColumn";
            this.tenantDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(131, 28);
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem1,
            this.matchesToolStripMenuItem,
            this.noMatchesToolStripMenuItem});
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.allToolStripMenuItem.Text = "Severity";
            // 
            // allToolStripMenuItem1
            // 
            this.allToolStripMenuItem1.Name = "allToolStripMenuItem1";
            this.allToolStripMenuItem1.Size = new System.Drawing.Size(163, 26);
            this.allToolStripMenuItem1.Text = "&All";
            this.allToolStripMenuItem1.Click += new System.EventHandler(this.allToolStripMenuItem1_Click);
            // 
            // matchesToolStripMenuItem
            // 
            this.matchesToolStripMenuItem.Name = "matchesToolStripMenuItem";
            this.matchesToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.matchesToolStripMenuItem.Text = "&Matches";
            this.matchesToolStripMenuItem.Click += new System.EventHandler(this.matchesToolStripMenuItem_Click);
            // 
            // noMatchesToolStripMenuItem
            // 
            this.noMatchesToolStripMenuItem.Name = "noMatchesToolStripMenuItem";
            this.noMatchesToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.noMatchesToolStripMenuItem.Text = "&No matches";
            this.noMatchesToolStripMenuItem.Click += new System.EventHandler(this.noMatchesToolStripMenuItem_Click);
            // 
            // stpselectLogResultBindingSource
            // 
            this.stpselectLogResultBindingSource.DataSource = typeof(Stub.Repository.StubDb.Entities.stp_selectLog_Result);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.severityButton,
            this.playstopButton});
            this.toolStrip1.Location = new System.Drawing.Point(8, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(70, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // severityButton
            // 
            this.severityButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.severityButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem2,
            this.matchesToolStripMenuItem1,
            this.noMatchesToolStripMenuItem1});
            this.severityButton.Image = ((System.Drawing.Image)(resources.GetObject("severityButton.Image")));
            this.severityButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.severityButton.Name = "severityButton";
            this.severityButton.Size = new System.Drawing.Size(34, 24);
            this.severityButton.Text = "Severity";
            // 
            // allToolStripMenuItem2
            // 
            this.allToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("allToolStripMenuItem2.Image")));
            this.allToolStripMenuItem2.Name = "allToolStripMenuItem2";
            this.allToolStripMenuItem2.Size = new System.Drawing.Size(163, 26);
            this.allToolStripMenuItem2.Text = "All";
            this.allToolStripMenuItem2.Click += new System.EventHandler(this.allToolStripMenuItem1_Click);
            // 
            // matchesToolStripMenuItem1
            // 
            this.matchesToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("matchesToolStripMenuItem1.Image")));
            this.matchesToolStripMenuItem1.Name = "matchesToolStripMenuItem1";
            this.matchesToolStripMenuItem1.Size = new System.Drawing.Size(163, 26);
            this.matchesToolStripMenuItem1.Text = "Matches";
            this.matchesToolStripMenuItem1.Click += new System.EventHandler(this.matchesToolStripMenuItem_Click);
            // 
            // noMatchesToolStripMenuItem1
            // 
            this.noMatchesToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("noMatchesToolStripMenuItem1.Image")));
            this.noMatchesToolStripMenuItem1.Name = "noMatchesToolStripMenuItem1";
            this.noMatchesToolStripMenuItem1.Size = new System.Drawing.Size(163, 26);
            this.noMatchesToolStripMenuItem1.Text = "No Matches";
            this.noMatchesToolStripMenuItem1.Click += new System.EventHandler(this.noMatchesToolStripMenuItem_Click);
            // 
            // playstopButton
            // 
            this.playstopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.playstopButton.Image = ((System.Drawing.Image)(resources.GetObject("playstopButton.Image")));
            this.playstopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playstopButton.Name = "playstopButton";
            this.playstopButton.Size = new System.Drawing.Size(24, 24);
            this.playstopButton.Text = "Play";
            this.playstopButton.Click += new System.EventHandler(this.playstopButton_Click);
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dataGridView1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(803, 149);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(803, 176);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // LoggingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 176);
            this.Controls.Add(this.toolStripContainer1);
            this.HideOnClose = true;
            this.Name = "LoggingControl";
            this.ShowIcon = false;
            this.Text = "Logging";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stpselectLogResultBindingSource)).EndInit();
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

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource stpselectLogResultBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn responseDatumTijdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uriDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn requestDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn responseTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn templateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn combinationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn namespaceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rootnodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenantDataGridViewTextBoxColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem matchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noMatchesToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripDropDownButton severityButton;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem matchesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem noMatchesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton playstopButton;
    }
}
