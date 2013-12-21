using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using eBay.Service.Core.Sdk;
using Samples.Helper;

using eBay.Service.Core.Soap;
using eBay.Service.Call;
using eBay.Service.Util;

namespace EbayMaster
{
    public class EbayAccountBiz
    {
        public static String EbayAppId = ConfigurationManager.AppSettings["Environment.AppId"].ToString();
        public static String EbayDevId = ConfigurationManager.AppSettings["Environment.DevId"].ToString();
        public static String EbayCertId = ConfigurationManager.AppSettings["Environment.CertId"].ToString();
        public static String EbayRuName = ConfigurationManager.AppSettings["RuName"].ToString();

        public static String getAuthenticateUrl(out String sessionId)
        {
            sessionId = "";

            ApiAccount apiAccount = new ApiAccount();
            apiAccount.Application = EbayAppId;
            apiAccount.Certificate = EbayCertId;
            apiAccount.Developer = EbayDevId;

            ApiContext localContext = new ApiContext();
            localContext.ApiCredential = new eBay.Service.Core.Sdk.ApiCredential();
            localContext.ApiCredential.ApiAccount = apiAccount;
            localContext.RuName = EbayRuName;
            localContext.SoapApiServerUrl = System.Configuration.ConfigurationManager.AppSettings.Get(AppSettingHelper.API_SERVER_URL);
            localContext.SignInUrl = System.Configuration.ConfigurationManager.AppSettings.Get(AppSettingHelper.SIGNIN_URL);

            GetSessionIDCall apiCall = new GetSessionIDCall(localContext);
            apiCall.RuName = EbayRuName;
            apiCall.Execute();

            sessionId = apiCall.SessionID;
            String authUrl = String.Format("{0}&RuName={1}&SessID={2}", localContext.SignInUrl, EbayRuName, sessionId);
            return authUrl;
        }

        public static String FetchUserToken(String sessionId, out String userId)
        {
            userId = "";

            if (sessionId == "")
                return "";

            String token = "";

            ApiAccount apiAccount = new ApiAccount();
            apiAccount.Application = EbayAppId;
            apiAccount.Certificate = EbayCertId;
            apiAccount.Developer = EbayDevId;

            ApiContext localContext = new ApiContext();
            localContext.ApiCredential = new eBay.Service.Core.Sdk.ApiCredential();
            localContext.ApiCredential.ApiAccount = apiAccount;
            localContext.RuName = EbayRuName;
            localContext.SoapApiServerUrl = System.Configuration.ConfigurationManager.AppSettings.Get(AppSettingHelper.API_SERVER_URL);
            localContext.SignInUrl = System.Configuration.ConfigurationManager.AppSettings.Get(AppSettingHelper.SIGNIN_URL);

            ConfirmIdentityCall apiCall = new ConfirmIdentityCall(localContext);
            apiCall.SessionID = sessionId;
            try
            {
                apiCall.ConfirmIdentity(sessionId);
                userId = apiCall.UserID;
            }
            catch (System.Exception)
            {
            }

            FetchTokenCall fetchTokenApiCall = new FetchTokenCall(localContext);
            apiCall.SessionID = sessionId;
            try
            {
                fetchTokenApiCall.FetchToken(sessionId);
                token = fetchTokenApiCall.eBayToken;
            }
            catch (System.Exception)
            {
            }

            return token;
        }
    }
}
