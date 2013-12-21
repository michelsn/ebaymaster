using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayMaster
{
    public class StringUtil
    {
        public static String GetSafeString(object obj)
        {
            if (null == obj)
                return "";

            return obj.ToString();  
        }

        public static int GetSafeInt(object obj)
        {
            if (null == obj)
                return 0;

            int retVal = 0;
            try
            {
                retVal = Convert.ToInt32(obj.ToString());
            }
            catch (System.Exception )
            {

            }

            return retVal;
        }

        public static double GetSafeDouble(object obj)
        {
            if (null == obj)
                return 0.0;

            double retVal = 0.0;
            try
            {
                retVal = Convert.ToDouble(obj.ToString());
            }
            catch (System.Exception)
            {
            }
            return retVal;
        }

        public static DateTime GetSafeDateTime(object obj)
        {
            if (obj == null)
                return DateTime.Now;

            string objStr = obj.ToString();
            if (objStr == null || objStr == "")
                return DateTime.Now;

            return DateTime.Parse(objStr);
        }

        public static bool GetSafeBool(object obj)
        {
            if (obj == null)
                return false;

            bool result = false;
            try
            {
                bool.TryParse(obj.ToString(), out result);
            }
            catch (System.Exception )
            {
           
            }
            return result;
        }   // GetSafeBool

        // Days_30
        public static int GetSafeListDurationDays(String listDurationStr)
        {
            if (listDurationStr == null || listDurationStr.Trim().Length == 0)
                return 0;

            int days = 0;
            try
            {
                string[] strs = listDurationStr.Split(new char[] { '_' });
                if (strs.Length == 2)
                {
                    Int32.TryParse(strs[1], out days);
                }
            }
            catch (System.Exception)
            {
                days = 0;
            }
            return days;
        } // GetSafeListDurationDays

        // P16DT1H15M2S
        // PT0S
        public static String GetSafeTimeLeftString(object obj)
        {
            if (null == obj)
                return "Unknown";

            String str = obj.ToString();
            String[] substrs = str.Split(new char[] { 'P', 'p', 'T', 't' });
            if (substrs.Length != 3)
                return "Unknown";

            if (substrs[1] == null || substrs[2] == null)
                return "Unknown";

            String daysStr = "0 days";
            if (substrs[1] != "")
            {
                daysStr = substrs[1].Replace("D", "days");
            }

            String hourStr = "00:00:00";
            if (substrs[2] != "")
            {
                hourStr = substrs[2].Replace("H", ":");
                hourStr = hourStr.Replace("M", ":");
                hourStr = hourStr.Replace("S", "");
            }

            return string.Format("{0} {1}", daysStr, hourStr);
        }
    } 
}
