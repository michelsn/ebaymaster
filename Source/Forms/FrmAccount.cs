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
    public partial class FrmAccount : Form
    {
        public FrmAccount()
        {
            InitializeComponent();
        }

        private void LoadAllAccounts()
        {
            this.listBoxAllAccounts.Items.Clear();

            List<AccountType> accounts = AccountUtil.GetAllAccounts();
            foreach (AccountType account in accounts)
            {
                this.listBoxAllAccounts.Items.Add(account);
            }
        }

        private void FrmAccount_Load(object sender, EventArgs e)
        {
            LoadAllAccounts();
        }

        private void listBoxAllAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountType selectedAccount = (AccountType)this.listBoxAllAccounts.SelectedItem;
            if (selectedAccount == null)
                return;

            this.textBoxEbayAccount.Text = selectedAccount.ebayAccount;
            this.textBoxEbayToken.Text = selectedAccount.ebayToken;
            this.textBoxPayPalAccount.Text = selectedAccount.paypalAccount;
            this.textBoxPayPalUsername.Text = selectedAccount.paypalUsername;
            this.textBoxPayPalPassword.Text = selectedAccount.paypalPassword;
            this.textBoxPayPalSignature.Text = selectedAccount.paypalSignature;
        }

        private void btnAddOrUpdateAccount_Click(object sender, EventArgs e)
        {
            string ebayAccount = this.textBoxEbayAccount.Text;
            if (ebayAccount.Trim() == "")
            {
                MessageBox.Show("请输入Ebay账号", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ebayToken = this.textBoxEbayToken.Text;
            if (ebayToken.Trim() == "")
            {
                MessageBox.Show("请输入Ebay Token", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AccountType account = new AccountType();
            account.ebayAccount = ebayAccount;
            account.ebayToken = ebayToken;
            account.paypalAccount = this.textBoxPayPalAccount.Text.Trim();
            account.paypalUsername = this.textBoxPayPalUsername.Text.Trim();
            account.paypalPassword = this.textBoxPayPalPassword.Text.Trim();
            account.paypalSignature = this.textBoxPayPalSignature.Text.Trim();

            bool update = false;

            AccountType existedAccount = AccountUtil.GetAccountByEbayUsername(ebayAccount);
            if (existedAccount != null)
            {
                if (existedAccount.isEqual(account))
                {
                    MessageBox.Show("无任何信息需要更新！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                update = true;
            }

            if (update)
            {
                AccountUtil.UpdateOneAccount(account);
                MessageBox.Show("更新账号信息成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AccountUtil.AddOneAccount(account);
                this.listBoxAllAccounts.Items.Add(account);

                MessageBox.Show("添加账号信息成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void buttonDelAccount_Click(object sender, EventArgs e)
        {
            AccountType account = (AccountType)this.listBoxAllAccounts.SelectedItem;
            if (account == null)
            {
                MessageBox.Show("没有选中任何账号", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool result = AccountUtil.DeleteOneAccount(account.ebayAccount);
            if (result)
            {
                this.listBoxAllAccounts.Items.Remove(account);
                this.textBoxEbayAccount.Text = "";
                this.textBoxEbayToken.Text = "";
                MessageBox.Show("删除账号信息成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("删除账号信息失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGetToken_Click(object sender, EventArgs e)
        {
            FrmAccountGetToken frm = new FrmAccountGetToken();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadAllAccounts();
            }
        }
    }
}
