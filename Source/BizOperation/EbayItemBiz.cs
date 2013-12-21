using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using eBay.Service.Core.Sdk;
using Samples.Helper;

using eBay.Service.Core.Soap;
using eBay.Service.Call;
using eBay.Service.Util;

namespace EbayMaster
{
    public class EbayItemBiz
    {
        // Ebay API: GetItem
        //  http://developer.ebay.com/DevZone/XML/docs/Reference/eBay/GetItem.html
        //  Use this call to retrieve the data for a single item listed on an eBay site
        public static bool GetEbayItem(AccountType account)
        {
            GetItemCall getItemApiCall = new GetItemCall(account.SellerApiContext);
            getItemApiCall.ItemID = "130824387148";

            DetailLevelCodeType[] detailLevels = new DetailLevelCodeType[] { DetailLevelCodeType.ReturnSummary };
            getItemApiCall.DetailLevelList = new DetailLevelCodeTypeCollection(detailLevels);

            ItemType itemType = getItemApiCall.GetItem("130824387148");

            return true;
        }
    }
}
