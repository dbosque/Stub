namespace dBosque.Stub.Editor
{
    partial class DatabaseConnectionAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseConnectionAddForm));
            this.cancelBt = new System.Windows.Forms.Button();
            this.okBt = new System.Windows.Forms.Button();
            this.messageLabel = new System.Windows.Forms.Label();
            this.modelNameTb = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.customForm = new System.Windows.Forms.TabPage();
            this.browseButton = new System.Windows.Forms.Button();
            this.customProviderName = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.customTb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.modelnameLabel = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.modelNameTb2 = new System.Windows.Forms.TextBox();
            this.messageLabel2 = new System.Windows.Forms.Label();
            this.connectBt = new System.Windows.Forms.Button();
            this.passwordTb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.usernameTb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.databaseCombo = new System.Windows.Forms.ComboBox();
            this.connectionStringTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.updateBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.customForm.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelBt
            // 
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Location = new System.Drawing.Point(364, 14);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(4);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(93, 28);
            this.cancelBt.TabIndex = 13;
            this.cancelBt.Text = "Cancel";
            this.cancelBt.UseVisualStyleBackColor = true;
            // 
            // okBt
            // 
            this.okBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBt.Location = new System.Drawing.Point(465, 14);
            this.okBt.Margin = new System.Windows.Forms.Padding(4);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(93, 28);
            this.okBt.TabIndex = 0;
            this.okBt.Text = "Ok";
            this.okBt.UseVisualStyleBackColor = true;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // messageLabel
            // 
            this.messageLabel.ForeColor = System.Drawing.Color.Red;
            this.messageLabel.Location = new System.Drawing.Point(17, 122);
            this.messageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(524, 26);
            this.messageLabel.TabIndex = 15;
            // 
            // modelNameTb
            // 
            this.modelNameTb.Location = new System.Drawing.Point(21, 85);
            this.modelNameTb.Margin = new System.Windows.Forms.Padding(4);
            this.modelNameTb.Name = "modelNameTb";
            this.modelNameTb.Size = new System.Drawing.Size(520, 22);
            this.modelNameTb.TabIndex = 1;
            this.modelNameTb.TextChanged += new System.EventHandler(this.modelNameTb_TextChanged);
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
            this.splitContainer1.Panel2.Controls.Add(this.updateBtn);
            this.splitContainer1.Panel2.Controls.Add(this.okBt);
            this.splitContainer1.Panel2.Controls.Add(this.cancelBt);
            this.splitContainer1.Size = new System.Drawing.Size(593, 444);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 22;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.customForm);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(15, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(566, 363);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // customForm
            // 
            this.customForm.Controls.Add(this.browseButton);
            this.customForm.Controls.Add(this.customProviderName);
            this.customForm.Controls.Add(this.label6);
            this.customForm.Controls.Add(this.customTb);
            this.customForm.Controls.Add(this.label2);
            this.customForm.Controls.Add(this.modelnameLabel);
            this.customForm.Controls.Add(this.modelNameTb);
            this.customForm.Controls.Add(this.messageLabel);
            this.customForm.Location = new System.Drawing.Point(4, 25);
            this.customForm.Name = "customForm";
            this.customForm.Padding = new System.Windows.Forms.Padding(3);
            this.customForm.Size = new System.Drawing.Size(558, 334);
            this.customForm.TabIndex = 1;
            this.customForm.Text = "Fully qualified";
            // 
            // browseButton
            // 
            this.browseButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.browseButton.Location = new System.Drawing.Point(448, 35);
            this.browseButton.Margin = new System.Windows.Forms.Padding(4);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(93, 28);
            this.browseButton.TabIndex = 22;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // customProviderName
            // 
            this.customProviderName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customProviderName.FormattingEnabled = true;
            this.customProviderName.Location = new System.Drawing.Point(20, 135);
            this.customProviderName.Name = "customProviderName";
            this.customProviderName.Size = new System.Drawing.Size(519, 24);
            this.customProviderName.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 115);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "Provider name :";
            // 
            // customTb
            // 
            this.customTb.Location = new System.Drawing.Point(21, 38);
            this.customTb.Margin = new System.Windows.Forms.Padding(4);
            this.customTb.Name = "customTb";
            this.customTb.Size = new System.Drawing.Size(417, 22);
            this.customTb.TabIndex = 0;
            this.customTb.TextChanged += new System.EventHandler(this.customTb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Fully qualified connectionstring :";
            // 
            // modelnameLabel
            // 
            this.modelnameLabel.AutoSize = true;
            this.modelnameLabel.Location = new System.Drawing.Point(18, 64);
            this.modelnameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modelnameLabel.Name = "modelnameLabel";
            this.modelnameLabel.Size = new System.Drawing.Size(126, 17);
            this.modelnameLabel.TabIndex = 2;
            this.modelnameLabel.Text = "Connection name :";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.modelNameTb2);
            this.tabPage1.Controls.Add(this.messageLabel2);
            this.tabPage1.Controls.Add(this.connectBt);
            this.tabPage1.Controls.Add(this.passwordTb);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.usernameTb);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.databaseCombo);
            this.tabPage1.Controls.Add(this.connectionStringTb);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(558, 334);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SQL Connection";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 268);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "Connection name :";
            // 
            // modelNameTb2
            // 
            this.modelNameTb2.Location = new System.Drawing.Point(21, 289);
            this.modelNameTb2.Margin = new System.Windows.Forms.Padding(4);
            this.modelNameTb2.Name = "modelNameTb2";
            this.modelNameTb2.Size = new System.Drawing.Size(518, 22);
            this.modelNameTb2.TabIndex = 5;
            // 
            // messageLabel2
            // 
            this.messageLabel2.ForeColor = System.Drawing.Color.Red;
            this.messageLabel2.Location = new System.Drawing.Point(18, 328);
            this.messageLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.messageLabel2.Name = "messageLabel2";
            this.messageLabel2.Size = new System.Drawing.Size(524, 26);
            this.messageLabel2.TabIndex = 22;
            // 
            // connectBt
            // 
            this.connectBt.Location = new System.Drawing.Point(439, 181);
            this.connectBt.Margin = new System.Windows.Forms.Padding(4);
            this.connectBt.Name = "connectBt";
            this.connectBt.Size = new System.Drawing.Size(100, 28);
            this.connectBt.TabIndex = 3;
            this.connectBt.Text = "Connect";
            this.connectBt.UseVisualStyleBackColor = true;
            // 
            // passwordTb
            // 
            this.passwordTb.Location = new System.Drawing.Point(21, 138);
            this.passwordTb.Margin = new System.Windows.Forms.Padding(4);
            this.passwordTb.Name = "passwordTb";
            this.passwordTb.PasswordChar = '*';
            this.passwordTb.Size = new System.Drawing.Size(518, 22);
            this.passwordTb.TabIndex = 2;
            this.passwordTb.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 117);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Password :";
            // 
            // usernameTb
            // 
            this.usernameTb.Location = new System.Drawing.Point(21, 89);
            this.usernameTb.Margin = new System.Windows.Forms.Padding(4);
            this.usernameTb.Name = "usernameTb";
            this.usernameTb.Size = new System.Drawing.Size(518, 22);
            this.usernameTb.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 64);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "User Id :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 219);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Default database :";
            // 
            // databaseCombo
            // 
            this.databaseCombo.FormattingEnabled = true;
            this.databaseCombo.Location = new System.Drawing.Point(21, 240);
            this.databaseCombo.Margin = new System.Windows.Forms.Padding(4);
            this.databaseCombo.Name = "databaseCombo";
            this.databaseCombo.Size = new System.Drawing.Size(518, 24);
            this.databaseCombo.TabIndex = 4;
            // 
            // connectionStringTb
            // 
            this.connectionStringTb.Location = new System.Drawing.Point(21, 38);
            this.connectionStringTb.Margin = new System.Windows.Forms.Padding(4);
            this.connectionStringTb.Name = "connectionStringTb";
            this.connectionStringTb.Size = new System.Drawing.Size(518, 22);
            this.connectionStringTb.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Servername :";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // updateBtn
            // 
            this.updateBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.updateBtn.Location = new System.Drawing.Point(15, 14);
            this.updateBtn.Margin = new System.Windows.Forms.Padding(4);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(93, 28);
            this.updateBtn.TabIndex = 14;
            this.updateBtn.Text = "Update";
            this.updateBtn.UseVisualStyleBackColor = true;
            // 
            // DatabaseConnectionAddForm
            // 
            this.AcceptButton = this.okBt;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBt;
            this.ClientSize = new System.Drawing.Size(593, 444);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseConnectionAddForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection properties";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.customForm.ResumeLayout(false);
            this.customForm.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.TextBox modelNameTb;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox modelNameTb2;
        private System.Windows.Forms.Label messageLabel2;
        private System.Windows.Forms.Button connectBt;
        private System.Windows.Forms.TextBox passwordTb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox usernameTb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox databaseCombo;
        private System.Windows.Forms.TextBox connectionStringTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage customForm;
        private System.Windows.Forms.TextBox customTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label modelnameLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox customProviderName;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button updateBtn;
    }
}