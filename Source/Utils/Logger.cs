using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace EbayMaster
{
   // There are two types of logs:
   // 1) System log: records synchronization/operation info.
   // 2) User log: records info valuable for user, available for user review.
   public class Logger
   {
      public static String SystemLogFileName = ConfigurationManager.AppSettings["SystemLogFileName"].ToString();
      public static String UserLogFileName = ConfigurationManager.AppSettings["UserLogFileName"].ToString();

      public static void WriteSystemUserLog(String logMsg)
      {
         WriteSystemLog(logMsg);
         WriteUserLog(logMsg);
      }

      public static void WriteSystemLog(String logMsg)
      {
         WriteLog(logMsg, true/*systemLog*/);
      }

      public static void WriteUserLog(String logMsg)
      {
         WriteLog(logMsg, false/*systemLog*/);
      }

      public static void WriteLog(String logMsg, bool systemLog)
      {
         String logFilePath = AppDomain.CurrentDomain.BaseDirectory +
            AppDomain.CurrentDomain.RelativeSearchPath;

         logFilePath += "//" + (systemLog ? SystemLogFileName: UserLogFileName);

         StreamWriter sw;
         if (File.Exists(logFilePath) == true)
         {
            sw = File.AppendText(logFilePath);
         }
         else
         {
            sw = new StreamWriter(logFilePath);
         }

         sw.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToString(), logMsg));
         sw.Flush();
         sw.Close();
      }  // WriteUserLog
   }
}
