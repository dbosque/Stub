namespace dBosque.Stub.Editor
{
    partial class AddEditMessageTypeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditMessageTypeForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.passthroughEnabled = new System.Windows.Forms.CheckBox();
            this.tbPassthrougUri = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.regexTb = new System.Windows.Forms.TextBox();
            this.namespaceTb = new System.Windows.Forms.TextBox();
            this.descriptionTb = new System.Windows.Forms.TextBox();
            this.rootnodeTb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.sampleTb = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(589, 442);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(15, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(566, 380);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.passthroughEnabled);
            this.tabPage1.Controls.Add(this.tbPassthrougUri);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.regexTb);
            this.tabPage1.Controls.Add(this.namespaceTb);
            this.tabPage1.Controls.Add(this.descriptionTb);
            this.tabPage1.Controls.Add(this.rootnodeTb);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(558, 351);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add or Edit MessageType";
            // 
            // passthroughEnabled
            // 
            this.passthroughEnabled.AutoSize = true;
            this.passthroughEnabled.Location = new System.Drawing.Point(21, 269);
            this.passthroughEnabled.Name = "passthroughEnabled";
            this.passthroughEnabled.Size = new System.Drawing.Size(237, 21);
            this.passthroughEnabled.TabIndex = 11;
            this.passthroughEnabled.Text = "Relay when no match was found.";
            this.passthroughEnabled.UseVisualStyleBackColor = true;
            // 
            // tbPassthrougUri
            // 
            this.tbPassthrougUri.Location = new System.Drawing.Point(21, 313);
            this.tbPassthrougUri.Name = "tbPassthrougUri";
            this.tbPassthrougUri.Size = new System.Drawing.Size(523, 22);
            this.tbPassthrougUri.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "PassthroughUri :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Test the regular expression :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Namespace or URI regular expression : ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(451, 136);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 28);
            this.button3.TabIndex = 2;
            this.button3.Text = "Validate";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rootnode : ";
            // 
            // regexTb
            // 
            this.regexTb.Location = new System.Drawing.Point(21, 99);
            this.regexTb.Name = "regexTb";
            this.regexTb.Size = new System.Drawing.Size(523, 22);
            this.regexTb.TabIndex = 1;
            // 
            // namespaceTb
            // 
            this.namespaceTb.Location = new System.Drawing.Point(21, 46);
            this.namespaceTb.Name = "namespaceTb";
            this.namespaceTb.Size = new System.Drawing.Size(523, 22);
            this.namespaceTb.TabIndex = 0;
            // 
            // descriptionTb
            // 
            this.descriptionTb.Location = new System.Drawing.Point(21, 227);
            this.descriptionTb.Name = "descriptionTb";
            this.descriptionTb.Size = new System.Drawing.Size(523, 22);
            this.descriptionTb.TabIndex = 3;
            // 
            // rootnodeTb
            // 
            this.rootnodeTb.Location = new System.Drawing.Point(21, 179);
            this.rootnodeTb.Name = "rootnodeTb";
            this.rootnodeTb.Size = new System.Drawing.Size(523, 22);
            this.rootnodeTb.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description :";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.sampleTb);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(558, 351);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sample";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // sampleTb
            // 
            this.sampleTb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sampleTb.Location = new System.Drawing.Point(3, 3);
            this.sampleTb.Multiline = true;
            this.sampleTb.Name = "sampleTb";
            this.sampleTb.Size = new System.Drawing.Size(552, 345);
            this.sampleTb.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(470, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 28);
            this.button2.TabIndex = 0;
            this.button2.Text = "Apply";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(371, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddEditMessageTypeForm
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(589, 442);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditMessageTypeForm";
            this.ShowInTaskbar = false;
            this.Text = "Properties";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rootnodeTb;
        private System.Windows.Forms.TextBox namespaceTb;
        private System.Windows.Forms.TextBox descriptionTb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox regexTb;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox passthroughEnabled;
        private System.Windows.Forms.TextBox tbPassthrougUri;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox sampleTb;
    }
}