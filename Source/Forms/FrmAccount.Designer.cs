namespace EbayMaster
{
    partial class FrmAccount
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
            this.listBoxAllAccounts = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxPayPalPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPayPalUsername = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPayPalSignature = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPayPalAccount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxEbayToken = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEbayAccount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddOrUpdateAccount = new System.Windows.Forms.Button();
            this.buttonDelAccount = new System.Windows.Forms.Button();
            this.buttonGetToken = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxAllAccounts
            // 
            this.listBoxAllAccounts.FormattingEnabled = true;
            this.listBoxAllAccounts.ItemHeight = 12;
            this.listBoxAllAccounts.Location = new System.Drawing.Point(27, 22);
            this.listBoxAllAccounts.Name = "listBoxAllAccounts";
            this.listBoxAllAccounts.Size = new System.Drawing.Size(205, 376);
            this.listBoxAllAccounts.TabIndex = 0;
            this.listBoxAllAccounts.SelectedIndexChanged += new System.EventHandler(this.listBoxAllAccounts_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxPayPalPassword);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxPayPalUsername);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxPayPalSignature);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxPayPalAccount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxEbayToken);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxEbayAccount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(265, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 383);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // textBoxPayPalPassword
            // 
            this.textBoxPayPalPassword.Location = new System.Drawing.Point(118, 267);
            this.textBoxPayPalPassword.Name = "textBoxPayPalPassword";
            this.textBoxPayPalPassword.Size = new System.Drawing.Size(153, 21);
            this.textBoxPayPalPassword.TabIndex = 24;
            this.textBoxPayPalPassword.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "paypal password";
            this.label6.Visible = false;
            // 
            // textBoxPayPalUsername
            // 
            this.textBoxPayPalUsername.Location = new System.Drawing.Point(118, 231);
            this.textBoxPayPalUsername.Name = "textBoxPayPalUsername";
            this.textBoxPayPalUsername.Size = new System.Drawing.Size(153, 21);
            this.textBoxPayPalUsername.TabIndex = 22;
            this.textBoxPayPalUsername.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "paypal username";
            this.label5.Visible = false;
            // 
            // textBoxPayPalSignature
            // 
            this.textBoxPayPalSignature.Location = new System.Drawing.Point(118, 316);
            this.textBoxPayPalSignature.Multiline = true;
            this.textBoxPayPalSignature.Name = "textBoxPayPalSignature";
            this.textBoxPayPalSignature.Size = new System.Drawing.Size(368, 48);
            this.textBoxPayPalSignature.TabIndex = 20;
            this.textBoxPayPalSignature.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 334);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "paypal signature";
            this.label4.Visible = false;
            // 
            // textBoxPayPalAccount
            // 
            this.textBoxPayPalAccount.Location = new System.Drawing.Point(118, 193);
            this.textBoxPayPalAccount.Name = "textBoxPayPalAccount";
            this.textBoxPayPalAccount.Size = new System.Drawing.Size(153, 21);
            this.textBoxPayPalAccount.TabIndex = 18;
            this.textBoxPayPalAccount.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "paypal账号";
            this.label3.Visible = false;
            // 
            // textBoxEbayToken
            // 
            this.textBoxEbayToken.Location = new System.Drawing.Point(118, 63);
            this.textBoxEbayToken.Multiline = true;
            this.textBoxEbayToken.Name = "textBoxEbayToken";
            this.textBoxEbayToken.ReadOnly = true;
            this.textBoxEbayToken.Size = new System.Drawing.Size(368, 104);
            this.textBoxEbayToken.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "ebay token";
            // 
            // textBoxEbayAccount
            // 
            this.textBoxEbayAccount.Location = new System.Drawing.Point(118, 20);
            this.textBoxEbayAccount.Name = "textBoxEbayAccount";
            this.textBoxEbayAccount.ReadOnly = true;
            this.textBoxEbayAccount.Size = new System.Drawing.Size(153, 21);
            this.textBoxEbayAccount.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "ebay账号";
            // 
            // btnAddOrUpdateAccount
            // 
            this.btnAddOrUpdateAccount.Location = new System.Drawing.Point(588, 408);
            this.btnAddOrUpdateAccount.Name = "btnAddOrUpdateAccount";
            this.btnAddOrUpdateAccount.Size = new System.Drawing.Size(106, 23);
            this.btnAddOrUpdateAccount.TabIndex = 14;
            this.btnAddOrUpdateAccount.Text = "添加/更新账号";
            this.btnAddOrUpdateAccount.UseVisualStyleBackColor = true;
            this.btnAddOrUpdateAccount.Visible = false;
            this.btnAddOrUpdateAccount.Click += new System.EventHandler(this.btnAddOrUpdateAccount_Click);
            // 
            // buttonDelAccount
            // 
            this.buttonDelAccount.Location = new System.Drawing.Point(92, 408);
            this.buttonDelAccount.Name = "buttonDelAccount";
            this.buttonDelAccount.Size = new System.Drawing.Size(106, 23);
            this.buttonDelAccount.TabIndex = 15;
            this.buttonDelAccount.Text = "删除账号";
            this.buttonDelAccount.UseVisualStyleBackColor = true;
            this.buttonDelAccount.Click += new System.EventHandler(this.buttonDelAccount_Click);
            // 
            // buttonGetToken
            // 
            this.buttonGetToken.Location = new System.Drawing.Point(454, 408);
            this.buttonGetToken.Name = "buttonGetToken";
            this.buttonGetToken.Size = new System.Drawing.Size(106, 23);
            this.buttonGetToken.TabIndex = 16;
            this.buttonGetToken.Text = "获取token";
            this.buttonGetToken.UseVisualStyleBackColor = true;
            this.buttonGetToken.Click += new System.EventHandler(this.buttonGetToken_Click);
            // 
            // FrmAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 443);
            this.Controls.Add(this.buttonGetToken);
            this.Controls.Add(this.buttonDelAccount);
            this.Controls.Add(this.btnAddOrUpdateAccount);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBoxAllAccounts);
            this.Name = "FrmAccount";
            this.Text = "账号设置";
            this.Load += new System.EventHandler(this.FrmAccount_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAllAccounts;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxPayPalPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPayPalUsername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPayPalSignature;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPayPalAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxEbayToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxEbayAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddOrUpdateAccount;
        private System.Windows.Forms.Button buttonDelAccount;
        private System.Windows.Forms.Button buttonGetToken;
    }
}