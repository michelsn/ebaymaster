using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EbayMaster
{
    public partial class FrmAccountGetToken : Form
    {
        public String SessionId = "";
        public String UserName = "";
        public String Token = "";

        public FrmAccountGetToken()
        {
            InitializeComponent();
        }

        private void buttonGetTokenStep1_Click(object sender, EventArgs e)
        {
            this.buttonGetTokenStep1.Text += "\r\n请等待浏览器打开";

            String url = EbayAccountBiz.getAuthenticateUrl(out SessionId);
            System.Diagnostics.Process.Start(url);

            if (SessionId != "")
            {
                this.buttonGetTokenStep1.Enabled = false;
                this.buttonGetTokenStep2.Enabled = true;
            }
        }

        private void buttonGetTokenStep2_Click(object sender, EventArgs e)
        {
            if (SessionId == "")
            {
                MessageBox.Show("无SessionId，请重新执行第一步", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.buttonGetTokenStep2.Enabled = false;
                this.buttonGetTokenStep1.Enabled = true;
                return;
            }

            this.buttonGetTokenStep2.Enabled = false;

            Token = EbayAccountBiz.FetchUserToken(SessionId, out UserName);

            this.textBoxToken.Text = Token;
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            AccountType account = new AccountType();
            account.ebayAccount = UserName;
            account.ebayToken = Token;

            AccountType existedAccount = AccountUtil.GetAccountByEbayUsername(UserName);
            if (existedAccount != null)
            {
                MessageBox.Show("已存在此账号token，需先删除已有账号！", "抱歉", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AccountUtil.AddOneAccount(account); 
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
