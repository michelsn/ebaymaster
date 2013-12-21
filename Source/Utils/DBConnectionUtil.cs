using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Configuration;
using System.IO;

namespace EbayMaster
{
    // Wrap the info of a specific database connection type.
    public class DBConnectionType
    {
        public DatabaseType dbType = DatabaseType.NotSpecified;
        public bool isUsed = false;
        public string DBConnectionString = "";

        public string AccessFilePath = "";
        public string SQLServerAddress = "";
        public string SQLServerDBName = "";
        public string SQLServerUsername = "";
        public string SQLServerPassword = "";

        public DBConnectionType()
        {

        }

        public bool isEqual(DBConnectionType other)
        {
            if (dbType != other.dbType)
                return false;

            if (AccessFilePath != other.AccessFilePath)
                return false;

            if (SQLServerAddress != other.SQLServerAddress)
                return false;
            if (SQLServerDBName != other.SQLServerDBName)
                return false;
            if (SQLServerUsername != other.SQLServerUsername)
                return false;
            if (SQLServerPassword != other.SQLServerPassword)
                return false;

            return true;
        }
    }

    // This class is used to read/write system settings info from/to SystemSettings.xml file.
    // Please ref to SystemSettings for the format of how to store the system settings info.
    // Two kinds of info are stored there:
    //  1) Database connection info, which type of database is used: Access or SQL Server, and 
    //  the connection info.
    //  2) Ebay accounts and paypal accounts plus the tokens/credentials.
    public class DBConnectionUtil
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

        private static DBConnectionType _dbConn = null;
        public static DBConnectionType DBConn
        {
            get 
            {
                if (_dbConn == null)
                    _dbConn = GetDBConnectionType(true);
                return _dbConn;
            }
        }

        public static bool CheckAccessFileExistence()
        {
            if (DBConn == null)
                return false;

            if (File.Exists(DBConn.AccessFilePath) == false)
                return false;

            return true;
        }

        // Access connection string is like:
        //  "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\\Ebay\\svn\\trunk\\Database\\ebaydata.accdb"
        // we want to extract string "F:\Ebay\svn\trunk\Database\ebaydata.accdb"
        private static string GetAccessFilePathFromConnectionString(string connStr)
        {
            string accessFilePath = "";

            int indexOfDataSource = connStr.IndexOf("Data Source=");
            if (indexOfDataSource < 0 || indexOfDataSource > connStr.Length - 1)
                return accessFilePath;

            accessFilePath = connStr.Substring(indexOfDataSource+12);
            accessFilePath = accessFilePath.Replace("\\\\", "\\");

            if (File.Exists(accessFilePath) == false)
            {
                String dir = Directory.GetCurrentDirectory();
                accessFilePath = String.Format("{0}\\data\\ebaydata.accdb", dir);
                if (File.Exists(accessFilePath) == false)
                {
                    accessFilePath = "";
                }
            }
            return accessFilePath;
        }

        private static string GetAccessConnectionStringFromFilePath(string filePath)
        {
            filePath = filePath.Replace("\\", "\\\\");
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath;
        }

        // Get SQL Server info from connection string which is like:
        //  "Data Source=zhiwang,49172;Network Library=DBMSSOCN;Initial Catalog=ebaybachelor;User ID=sa;Password=567835;"
        private static DBConnectionType GetSQLServerInfoFromConnectionString(string connStr)
        {
            DBConnectionType connType = new DBConnectionType();
            connType.dbType = DatabaseType.SQLServer;

            int indexOfDataSource = connStr.IndexOf("Data Source=");
            int indexOfNetworkLibrary = connStr.IndexOf("Network Library=");
            int indexOfInitialCatelog = connStr.IndexOf("Initial Catalog=");
            int indexOfUserId = connStr.IndexOf("User ID=");
            int indexOfPassword = connStr.IndexOf("Password=");

            string dataSourceStr = connStr.Substring(indexOfDataSource + 12, indexOfNetworkLibrary - indexOfDataSource - 13);
            string dbNameStr = connStr.Substring(indexOfInitialCatelog + 16, indexOfUserId - indexOfInitialCatelog - 17);
            string userIdStr = connStr.Substring(indexOfUserId + 8, indexOfPassword - indexOfUserId - 9);
            string passwordStr = connStr.Substring(indexOfPassword + 9).TrimEnd(new char[] { ';' });

            connType.SQLServerAddress = dataSourceStr;
            connType.SQLServerDBName = dbNameStr;
            connType.SQLServerUsername = userIdStr;
            connType.SQLServerPassword = passwordStr;
            connType.DBConnectionString = connStr;
            return connType;
        }

        private static string GetSQLServerConnectionString(DBConnectionType dbConn)
        {
            string connStr = string.Format("Data Source={0};Network Library=DBMSSOCN;Initial Catalog={1};User ID={2};Password={3};",
                dbConn.SQLServerAddress, dbConn.SQLServerDBName, dbConn.SQLServerUsername, dbConn.SQLServerPassword);
            return connStr;
        }

        // Database connection utilities.
        public static DBConnectionType GetDBConnectionType(bool used)
        {
            DBConnectionType dbConnectionType = new DBConnectionType();

            XmlNodeList dbConnNodeList = XmlDoc.SelectNodes("/Settings/DBConnection/option");
            foreach (XmlNode node in dbConnNodeList)
            {
                string dbTypeStr = node.Attributes["DBType"].Value;
                string selectedStr = node.Attributes["selected"].Value;
                bool isUsed = 0 == string.Compare(selectedStr, "1");
                string dbConnStr = node.Attributes["value"].Value;
                dbConnectionType.DBConnectionString = dbConnStr;

                if (isUsed == used)
                {
                    if (0 == string.Compare(dbTypeStr, "SQLSERVER"))
                        dbConnectionType.dbType = DatabaseType.SQLServer;
                    else if (0 == string.Compare(dbTypeStr, "ACCESS"))
                        dbConnectionType.dbType = DatabaseType.Access;

                    if (DatabaseType.Access == dbConnectionType.dbType)
                    {
                        dbConnectionType.AccessFilePath = GetAccessFilePathFromConnectionString(dbConnStr);
                    }
                    else if (DatabaseType.SQLServer == dbConnectionType.dbType)
                    {
                        dbConnectionType = GetSQLServerInfoFromConnectionString(dbConnStr);
                    }

                    break;
                }
            }

            dbConnectionType.isUsed = true;
            return dbConnectionType;
        }

        public static bool SaveDBConnectionType(DBConnectionType dbConnType)
        {
            //DBConnectionType usedDBConnType = GetDBConnectionType(true);
            //if (usedDBConnType.isEqual(dbConnType))
            //    return false;

            XmlNodeList dbConnNodeList = XmlDoc.SelectNodes("/Settings/DBConnection/option");
            foreach (XmlNode node in dbConnNodeList)
            {
                DatabaseType dbType = DatabaseType.NotSpecified;

                string dbTypeStr = node.Attributes["DBType"].Value;
                if (string.Compare(dbTypeStr, "ACCESS") == 0)
                    dbType = DatabaseType.Access;
                else
                    dbType = DatabaseType.SQLServer;

                if (dbType == dbConnType.dbType)
                {
                    node.Attributes["selected"].Value = "1";

                    if (dbType == DatabaseType.Access)
                        node.Attributes["value"].Value = GetAccessConnectionStringFromFilePath(dbConnType.AccessFilePath);
                    else
                        node.Attributes["value"].Value = GetSQLServerConnectionString(dbConnType);
                }
                else
                {
                    node.Attributes["selected"].Value = "0";
                }

            }

            XmlDoc.Save(EbayConstants.SystemSettingsXmlPath);
            return true;
        }
    }
}
