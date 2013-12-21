#region Copyright
//	Copyright (c) 2007 eBay, Inc.
//
//	This program is licensed under the terms of the eBay Common Development and 
//	Distribution License (CDDL) Version 1.0 (the "License") and any subsequent 
//	version thereof released by eBay.  The then-current version of the License 
//	can be found at https://www.codebase.ebay.com/Licenses.html and in the 
//	eBaySDKLicense file that is under the eBay SDK install directory.
#endregion

using System;
using System.Xml;
using System.IO;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;


namespace Samples.Helper
{
	/// <summary>
	/// Summary description for ConfigurationSettingHelper.
	/// </summary>
	public class AppSettingHelper
	{
		static XmlDocument _config = null;
		static string _fileName = null;

		private AppSettingHelper()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns> the API context for the default user </returns>
		public static ApiContext GetApiContext(string apiToken)
		{
			ApiContext apiContext = GetGenericApiContext();

			ApiCredential apiCredential = apiContext.ApiCredential;
            apiCredential.eBayToken = apiToken;

			return apiContext;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns> the API context for the second user </returns>
		public static ApiContext GetUser2ApiContext()
		{
			ApiContext apiContext = GetGenericApiContext();

			ApiCredential apiCredential = apiContext.ApiCredential;
			apiCredential.eBayToken = System.Configuration.ConfigurationManager.AppSettings.Get(API_TOKEN2);

			return apiContext;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns> API Context without user crendials
		/// </returns>
		private static ApiContext GetGenericApiContext()
		{
			ApiContext apiContext = new ApiContext();

            apiContext.Version = System.Configuration.ConfigurationManager.AppSettings.Get(VERSION);
            apiContext.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get(TIME_OUT));
            apiContext.SoapApiServerUrl = System.Configuration.ConfigurationManager.AppSettings.Get(API_SERVER_URL);
            apiContext.EPSServerUrl = System.Configuration.ConfigurationManager.AppSettings.Get(EPS_SERVER_URL);
            apiContext.SignInUrl = System.Configuration.ConfigurationManager.AppSettings.Get(SIGNIN_URL);	

			ApiAccount apiAccount = new ApiAccount();
            apiAccount.Developer = System.Configuration.ConfigurationManager.AppSettings.Get(DEV_ID);
            apiAccount.Application = System.Configuration.ConfigurationManager.AppSettings.Get(APP_ID);
            apiAccount.Certificate = System.Configuration.ConfigurationManager.AppSettings.Get(CERT_ID);

			ApiCredential apiCredential = new ApiCredential();
			apiCredential.ApiAccount = apiAccount;

			apiContext.ApiCredential = apiCredential;

            apiContext.EnableMetrics = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings.Get(ENABLE_METRICS));

            string site = System.Configuration.ConfigurationManager.AppSettings.Get(EBAY_USER_SITE_ID);
			if (site != null) 
			{
				apiContext.Site = (SiteCodeType)Enum.Parse(typeof(SiteCodeType), site, false);
			}

            apiContext.RuleName = System.Configuration.ConfigurationManager.AppSettings.Get(RULE_NAME);

			return apiContext;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="apiContext"></param>
		public static void SetApiContext(ApiContext apiContext)
		{
			SetAppSetting(VERSION, apiContext.Version);
			SetAppSetting(TIME_OUT, apiContext.Timeout.ToString());
			SetAppSetting(API_SERVER_URL, apiContext.SoapApiServerUrl);	
			SetAppSetting(EPS_SERVER_URL, apiContext.EPSServerUrl);		
			SetAppSetting(SIGNIN_URL, apiContext.SignInUrl);	
			SetAppSetting(DEV_ID, apiContext.ApiCredential.ApiAccount.Developer);
			SetAppSetting(APP_ID, apiContext.ApiCredential.ApiAccount.Application);
			SetAppSetting(CERT_ID, apiContext.ApiCredential.ApiAccount.Certificate);
			SetAppSetting(API_TOKEN, apiContext.ApiCredential.eBayToken);
			SetAppSetting(RULE_NAME, apiContext.RuleName);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static XmlDocument GetConfiguration()
		{
			if (_config == null) 
			{
				string fileName = System.AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
				if (!File.Exists(fileName)) 
				{
					return null;
				}
				_config = new XmlDocument();
				_config.Load(fileName); 
				_fileName = fileName;
			}
			return _config;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static XmlDocument GetConfiguration(string fileName)
		{
			if (_config == null) 
			{
				if (!File.Exists(fileName)) 
				{
					return null;
				}
				_config = new XmlDocument();
				_config.Load(fileName); 
				_fileName = fileName;
			}
			return _config;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="val"></param>
		public static void SaveAppSetting(string key, string val)
		{
			SetAppSetting(key, val);
			SaveAppSettings();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="val"></param>
		public static void SetAppSetting(string key, string val)
		{
			XmlDocument doc= GetConfiguration();
			if (doc != null) 
			{
				string xpath = "//configuration/appSettings/add[@key='" + key + "']";
				XmlNode node = doc.SelectSingleNode(xpath);
				if (node != null) 
				{
					node.Attributes.GetNamedItem("value").Value = val;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public static void SaveAppSettings()
		{
			if (_config != null) 
			{
				SaveAppSettings(_config);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		public static void SaveAppSettings(string fileName)
		{
			if (_config != null) 
			{
				SaveAppSettings(fileName, _config);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="doc"></param>
		public static void SaveAppSettings(XmlDocument doc) 
		{
			string fileName = null;

			if (_fileName != null) 
			{
				fileName = _fileName;
			}
			else 
			{
				fileName = System.AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
			if(fileName.StartsWith("file:///")) 
			{        
				fileName = fileName.Remove(0,8);
			}
			SaveAppSettings(fileName, doc);
		}           

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="doc"></param>
		public static void SaveAppSettings(string fileName, XmlDocument doc) 
		{ 
			doc.Save(fileName); 
			//##			doc = null; 
		}
        
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static LoggingProperties GetLoggingProperties()
		{
			LoggingProperties outProps = new LoggingProperties();
            outProps.LogFileName = System.Configuration.ConfigurationManager.AppSettings.Get(LOG_FILE_NAME);
            outProps.LogPayloadErrorCodes = System.Configuration.ConfigurationManager.AppSettings.Get(LOG_PAYLOAD_ERROR_CODES);
            outProps.LogPayloadExceptions = System.Configuration.ConfigurationManager.AppSettings.Get(LOG_PAYLOAD_EXCEPTIONS);
            outProps.LogPayloadHttpStatusCodes = System.Configuration.ConfigurationManager.AppSettings.Get(LOG_PAYLOAD_HTTP_STATUS_CODES);
			return outProps;
		}

		/// <summary>
		/// 
		/// </summary>
		public const string DB_CONNECTION = "DbConnection";

		/// <summary>
		/// 
		/// </summary>
		public const string VERSION = "Version";

		/// <summary>
		/// 
		/// </summary>
		public const string TIME_OUT = "TimeOut";

		/// <summary>
		/// 
		/// </summary>
		public const string DEMO_ENV = "DemoEnvironment";

		/// <summary>
		/// 
		/// </summary>
		public const string DEMO_USER = "DemoUser";

		/// <summary>
		/// 
		/// </summary>
		public const string LOG_FILE_NAME = "LogFileName";

		/// <summary>
		/// 
		/// </summary>
		public const string LOG_PAYLOAD_ERROR_CODES = "LogPayloadErrorCodes";

		/// <summary>
		/// 
		/// </summary>
		public const string LOG_PAYLOAD_HTTP_STATUS_CODES = "LogPayloadHttpStatusCodes";
		
		/// <summary>
		/// 
		/// </summary>
		public const string LOG_PAYLOAD_EXCEPTIONS = "LogPayloadExceptions";
		
		/// <summary>
		/// 
		/// </summary>
		public const string ENABLE_METRICS = "EnableMetrics";
		
		/// <summary>
		/// 
		/// </summary>
		public const string ENV_NAME = "Environment.Name";

		/// <summary>
		/// 
		/// </summary>
		public const string ENV_DESC = "Environment.Description";

		/// <summary>
		/// 
		/// </summary>
		public const string USER_DESC = "UserAccount.Description";

		/// <summary>
		/// 
		/// </summary>
		public const string USER_AUTH_TYPE = "UserAccount.AuthenticationType";

		/// <summary>
		/// 
		/// </summary>
		public const string API_SERVER_URL = "Environment.ApiServerUrl";	

		/// <summary>
		/// 
		/// </summary>
		public const string EPS_SERVER_URL = "Environment.EpsServerUrl";		

		/// <summary>
		/// 
		/// </summary>
		public const string SIGNIN_URL = "Environment.SignInUrl";	
		
		/// <summary>
		/// 
		/// </summary>
		public const string DEV_ID = "Environment.DevId";

		/// <summary>
		/// 
		/// </summary>
		public const string APP_ID = "Environment.AppId";

		/// <summary>
		/// 
		/// </summary>
		public const string CERT_ID = "Environment.CertId";

		/// <summary>
		/// 
		/// </summary>
		public const string EBAY_USER = "UserAccount.eBayUser";

		/// <summary>
		/// 
		/// </summary>
		public const string EBAY_PASSWORD = "UserAccount.eBayPassword";

		/// <summary>
		/// 
		/// </summary>
		public const string API_TOKEN = "UserAccount.ApiToken";
		/// <summary>
		/// 
		/// </summary>
		public const string EBAY_USER2 = "UserAccount.eBayUser2";

		/// <summary>
		/// 
		/// </summary>
		public const string EBAY_PASSWORD2 = "UserAccount.eBayPassword2";


		/// <summary>
		/// 
		/// </summary>
		public const string API_TOKEN2 = "UserAccount.ApiToken2";

		/// <summary>
		/// 
		/// </summary>
		public const string API_TOKEN_EXP_DATE = "UserAccount.TokenExpirationDate";

		/// <summary>
		/// 
		/// </summary>
		public const string API_TOKEN_CREATION_DATE = "UserAccount.TokenCreationDate";

		/// <summary>
		/// 
		/// </summary>
		public const string API_TOKEN_SECRET = "UserAccount.TokenSecret";

		/// <summary>
		/// 
		/// </summary>
		public const string API_TOKEN_SECRET_CREATION_DATE = "UserAccount.TokenSecretCreationDate";

		/// <summary>
		/// 
		/// </summary>
		public const string EBAY_USER_SITE_ID = "UserAccount.eBayUserSiteId";

		/// <summary>
		/// 
		/// </summary>
		public const string RULE_NAME = "RuName";
	}

	/// <summary>
	/// 
	/// </summary>
	public class LoggingProperties 
	{
		/// <summary>
		/// 
		/// </summary>
		public string LogFileName 
		{
			get	{ return mLogFileName; }
			set { mLogFileName = value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string LogPayloadErrorCodes 
		{
			get	{ return mLogPayloadErrorCodes; }
			set { mLogPayloadErrorCodes = value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string LogPayloadHttpStatusCodes 
		{
			get	{ return mLogPayloadHttpStatusCodes; }
			set { mLogPayloadHttpStatusCodes = value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string LogPayloadExceptions 
		{
			get	{ return mLogPayloadExceptions; }
			set { mLogPayloadExceptions = value; }
		}
		private string mLogFileName;
		private string mLogPayloadErrorCodes;
		private string mLogPayloadHttpStatusCodes;
		private string mLogPayloadExceptions;
	}
}
