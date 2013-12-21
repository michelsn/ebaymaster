using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Samples.Helper;
using eBay.Service.Core.Sdk;

namespace EbayMaster
{
    public class AccountType
    {
        public string ebayAccount = "";
        public string ebayToken = "";

        public string paypalAccount = "";
        public string paypalUsername = "";
        public string paypalPassword = "";
        public string paypalSignature = "";

        public ApiContext SellerApiContext = null;

        public AccountType()
        {

        }

        public override string ToString()
        {
            return ebayAccount;
        }

        public bool isEqual(AccountType other)
        {
            if (other == null)
                return false;

            if (other.ebayAccount != ebayAccount)
                return false;
            if (other.ebayToken != ebayToken)
                return false;
            if (other.paypalAccount != paypalAccount)
                return false;
            if (other.paypalUsername != paypalUsername)
                return false;
            if (other.paypalPassword != paypalPassword)
                return false;
            if (other.paypalSignature != paypalSignature)
                return false;

            return true;
        }
    }

    public class AccountUtil
    {
        private static XmlDocument xmlDoc = new XmlDocument();
        private static bool dirty = true;
        public static XmlDocument XmlDoc
        {
            get
            {
                if (dirty)
                {
                    xmlDoc.Load(EbayConstants.SystemSettingsXmlPath);
                    dirty = false;
                }
                return xmlDoc;
            }
        }

        // Store all the account info.
        private static List<AccountType> allAccounts = null;

        private static ExceptionFilter getExceptionFilter(LoggingProperties logProps)
        {
            if (logProps.LogPayloadErrorCodes == null && logProps.LogPayloadExceptions == null && logProps.LogPayloadHttpStatusCodes == null)
                return null;
            else
                return new ExceptionFilter(logProps.LogPayloadErrorCodes, logProps.LogPayloadExceptions, logProps.LogPayloadHttpStatusCodes);

        }

        public static void ReloadAllAccounts()
        {
            allAccounts = ReadAllAccounts();
            InitAllAccounts();
        }

        // Init all ebay accounts context.
        public static void InitAllAccounts()
        {
            foreach (AccountType account in allAccounts)
            {
                ApiContext context = AppSettingHelper.GetApiContext(account.ebayToken);
                if (context == null)
                {
                    //MessageBox.Show("初始化Ebay服务器连接失败，请查看网络或者配置情况！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                context.ApiLogManager = new ApiLogManager();
                LoggingProperties logProps = AppSettingHelper.GetLoggingProperties();
                context.ApiLogManager.ApiLoggerList.Add(new eBay.Service.Util.FileLogger(logProps.LogFileName, true, true, true));
                context.ApiLogManager.EnableLogging = true;
                context.ApiLogManager.MessageLoggingFilter = getExceptionFilter(logProps);
                context.Site = eBay.Service.Core.Soap.SiteCodeType.US;
                account.SellerApiContext = context;
            }
        }

        // Get a single ebay account info.
        public static AccountType GetAccount(String sellerName)
        {
            foreach (AccountType account in allAccounts)
            {
                if (account.ebayAccount == sellerName)
                    return account;
            }

            return null;
        }

        // get all ebay accounts.
        public static List<AccountType> GetAllAccounts()
        {
            return allAccounts;
        }

        // Private.
        private static List<AccountType> ReadAllAccounts()
        {
            List<AccountType> accounts = new List<AccountType>();

            XmlNodeList dbConnNodeList = XmlDoc.SelectNodes("/Settings/Accounts/Account");
            foreach (XmlNode node in dbConnNodeList)
            {
                AccountType account = new AccountType();
                account.ebayAccount = node.SelectSingleNode("ebayAccount").InnerText;
                account.ebayToken = node.SelectSingleNode("ebayToken").InnerText;
                account.paypalAccount = node.SelectSingleNode("paypalAccount").InnerText;
                account.paypalUsername = node.SelectSingleNode("paypalUsername").InnerText;
                account.paypalPassword = node.SelectSingleNode("paypalPassword").InnerText;
                account.paypalSignature = node.SelectSingleNode("paypalSignature").InnerText;
                accounts.Add(account);
            }

            return accounts;
        }

        public static bool AddOneAccount(AccountType account)
        {
            AccountType existedAccount = GetAccountByEbayUsername(account.ebayAccount);
            if (existedAccount != null)
                return false;

            XmlNode accountsNode = XmlDoc.SelectSingleNode("/Settings/Accounts");
            XmlElement xe = XmlDoc.CreateElement("Account");
            accountsNode.AppendChild(xe);

            // ebayAccount
            {
                XmlElement xeEbayAccount = XmlDoc.CreateElement("ebayAccount");
                xeEbayAccount.InnerText = account.ebayAccount;
                xe.AppendChild(xeEbayAccount);
            }
            // ebayToken
            {
                XmlElement xeEbayToken = XmlDoc.CreateElement("ebayToken");
                xeEbayToken.InnerText = account.ebayToken;
                xe.AppendChild(xeEbayToken);
            }
            // paypalAccount
            {
                XmlElement xePPAccount = XmlDoc.CreateElement("paypalAccount");
                xePPAccount.InnerText = account.paypalAccount;
                xe.AppendChild(xePPAccount);
            }
            // paypalUsername
            {
                XmlElement xePPUsername = XmlDoc.CreateElement("paypalUsername");
                xePPUsername.InnerText = account.paypalUsername;
                xe.AppendChild(xePPUsername);
            }
            // paypalPassword
            {
                XmlElement xePPPassword = XmlDoc.CreateElement("paypalPassword");
                xePPPassword.InnerText = account.paypalPassword;
                xe.AppendChild(xePPPassword);
            }
            // paypalSignature
            {
                XmlElement xePPSignature = XmlDoc.CreateElement("paypalSignature");
                xePPSignature.InnerText = account.paypalSignature;
                xe.AppendChild(xePPSignature);
            }

            XmlDoc.Save(EbayConstants.SystemSettingsXmlPath);
            ReloadAllAccounts();
            return true;
        }

        public static bool UpdateOneAccount(AccountType account)
        {
            bool found = false;
            
            string ebayAccount = account.ebayAccount;
            XmlNodeList accountNodeList = XmlDoc.SelectNodes("/Settings/Accounts/Account");
            foreach (XmlNode node in accountNodeList)
            {
                string nodeEbayAccount = node.SelectSingleNode("ebayAccount").InnerText;
                if (string.Compare(ebayAccount, nodeEbayAccount) == 0)
                {
                    found = true;

                    node.SelectSingleNode("ebayToken").InnerText = account.ebayToken;
                    node.SelectSingleNode("paypalAccount").InnerText = account.paypalAccount;
                    node.SelectSingleNode("paypalUsername").InnerText = account.paypalUsername;
                    node.SelectSingleNode("paypalPassword").InnerText = account.paypalPassword;
                    node.SelectSingleNode("paypalSignature").InnerText = account.paypalSignature;
                }
            }

            if (found)
            {
                XmlDoc.Save(EbayConstants.SystemSettingsXmlPath);
                ReloadAllAccounts();
            }

            return found;
        }

        public static bool DeleteOneAccount(string ebayAccount)
        {
            XmlNode accountsNode = XmlDoc.SelectSingleNode("/Settings/Accounts");

            bool removed = false;
            XmlNodeList accountNodeList = XmlDoc.SelectNodes("/Settings/Accounts/Account");
            foreach (XmlNode node in accountNodeList)
            {
                string nodeEbayAccount = node.SelectSingleNode("ebayAccount").InnerText;
                if (string.Compare(ebayAccount, nodeEbayAccount) == 0)
                {
                    accountsNode.RemoveChild(node);
                    removed = true;
                }
            }

            if (removed)
            {
                XmlDoc.Save(EbayConstants.SystemSettingsXmlPath);
                ReloadAllAccounts();
            }
            return removed;
        }


        public static AccountType GetAccountByEbayUsername(string ebayAccount)
        {
            AccountType account = null;

            XmlNodeList accountNodeList = XmlDoc.SelectNodes("/Settings/Accounts/Account");
            foreach (XmlNode node in accountNodeList)
            {
                string nodeEbayAccount = node.SelectSingleNode("ebayAccount").InnerText;
                if (string.Compare(ebayAccount, nodeEbayAccount) != 0)
                    continue;

                account = new AccountType();
                account.ebayAccount = nodeEbayAccount;
                account.ebayToken = node.SelectSingleNode("ebayToken").InnerText;
                account.paypalAccount = node.SelectSingleNode("paypalAccount").InnerText;
                account.paypalUsername = node.SelectSingleNode("paypalUsername").InnerText;
                account.paypalPassword = node.SelectSingleNode("paypalPassword").InnerText;
                account.paypalSignature = node.SelectSingleNode("paypalSignature").InnerText;
            }

            return account;
        }
    }
}
