using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EbayMaster
{
    public partial class FrmDBConnection : Form
    {
        public FrmDBConnection()
        {
            InitializeComponent();
        }

        private void rbAccess_CheckedChanged(object sender, EventArgs e)
        {
            this.panelAccess.Visible = this.rbAccess.Checked;
        }

        private void rbSqlServer_CheckedChanged(object sender, EventArgs e)
        {
            bool show = this.rbSqlServer.Checked;
            this.panelSQLServer.Visible = show;
            if (show)
            {
                this.panelSQLServer.Top = this.panelAccess.Top;
            }
        }

        private void FrmDBConnection_Load(object sender, EventArgs e)
        {
            DBConnectionType dbConnType = DBConnectionUtil.GetDBConnectionType(true);
            DBConnectionType unusedDbConnType = DBConnectionUtil.GetDBConnectionType(false);
            if (dbConnType.dbType == DatabaseType.Access)
            {
                this.panelAccess.Visible = true;
                this.panelSQLServer.Visible = false;

                this.rbAccess.Checked = true;
                this.rbSqlServer.Checked = false;

                this.textBoxAccessFilePath.Text = dbConnType.AccessFilePath;

                this.textBoxSQLServerDBAddr.Text = unusedDbConnType.SQLServerAddress;
                this.textBoxSQLServerDBName.Text = unusedDbConnType.SQLServerDBName;
                this.textBoxSQLServerUserId.Text = unusedDbConnType.SQLServerUsername;
                this.textBoxSQLServerPassword.Text = unusedDbConnType.SQLServerPassword;
            }
            else
            {
                this.panelAccess.Visible = false;
                this.panelSQLServer.Visible = true;
                this.panelSQLServer.Top = this.panelAccess.Top;

                this.rbAccess.Checked = false;
                this.rbSqlServer.Checked = true;

                this.textBoxSQLServerDBAddr.Text = dbConnType.SQLServerAddress;
                this.textBoxSQLServerDBName.Text = dbConnType.SQLServerDBName;
                this.textBoxSQLServerUserId.Text = dbConnType.SQLServerUsername;
                this.textBoxSQLServerPassword.Text = dbConnType.SQLServerPassword;

                this.textBoxAccessFilePath.Text = unusedDbConnType.AccessFilePath;
            }
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            DBConnectionType dbConnType = new DBConnectionType();
            if (rbAccess.Checked)
            {
                dbConnType.dbType = DatabaseType.Access;

                string accessFilePath = this.textBoxAccessFilePath.Text.Trim();
                if (false == File.Exists(accessFilePath))
                {
                    MessageBox.Show("Access文件不存在，请重新指定文件路径。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dbConnType.AccessFilePath = accessFilePath;
            }
            else
            {
                dbConnType.dbType = DatabaseType.SQLServer;

                string address = this.textBoxSQLServerDBAddr.Text.Trim();
                string dbName = this.textBoxSQLServerDBName.Text.Trim();
                string userId = this.textBoxSQLServerUserId.Text.Trim();
                string password = textBoxSQLServerPassword.Text.Trim();

                dbConnType.SQLServerAddress = address;
                dbConnType.SQLServerDBName = dbName;
                dbConnType.SQLServerUsername = userId;
                dbConnType.SQLServerPassword = password;
            }

            dbConnType.isUsed = true;

            bool result = DBConnectionUtil.SaveDBConnectionType(dbConnType);
            if (result)
                MessageBox.Show("更新数据库配置成功！\r\n你需要重新启动系统以使改动生效！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("更新数据库配置失败，请检查输入！", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonNavigateItemImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Access文件(*.accdb)|*.accdb";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = dlg.FileName;
                this.textBoxAccessFilePath.Text = filePath;
            }
        }
    }
}
