using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;

namespace EbayMaster
{
    public class EbayConstants
    {
        public static string SystemSettingsXmlPath = ConfigurationManager.AppSettings["SystemSettingsXmlPath"].ToString();

        public static string ExcelTemplateFor4pxFilePath = ConfigurationManager.AppSettings["ExcelTemplateFor4pxFilePath"].ToString();

        public Dictionary<string, string> ShippingServices = new Dictionary<string, string>();

        public static readonly int excelTemplateColumnHeadersCount = 29;
        public static string[] excelTemplateColumnHeadersFor4px = {"客户单号", "服务商单号", "运输方式", "目的国家", "寄件人公司名", 
                                                          "寄件人姓名", "寄件人地址", "寄件人电话", "寄件人邮编", "寄件人传真",
                                                          "收件人公司名","收件人姓名","州省","城市","联系地址", 
                                                          "收件人电话", "收件人邮箱","收件人邮编", "收件人传真","买家ID", 
                                                          "交易ID","保险类型", "保险价值", "订单备注", "海关报关品名1", 
                                                          "配货信息1", "申报价值1", "申报品数量1", "配货备注1"};

        public static string SenderCompanyName = "";
        public static string SenderName = "Zhi Wang";
        public static string SenderAddress = "Room 602, No 11,Yinghua fang, Mudan Rd 418, Pudong district, Shanghai";
        public static string SenderPhone = "8613816082110";
        public static string SenderPostalCode = "200122";
        public static string SenderFax = "";

        public static double DollarToRMB = 6.22;
        public static double GBPToRMB = 10.03;
        public static double EURToRMB = 8.23;
        public static double AUDToRMB = 6.57;

        private EbayConstants()
        {
            ShippingServices["B3"] = "香港小包挂号(B3)";
            ShippingServices["B4"] = "香港小包平邮(B4)";
            ShippingServices["B1"] = "新加坡小包挂号(B1)";
            ShippingServices["B2"] = "新加坡小包平邮(B2)";
            ShippingServices["A6"] = "4PX联邮通挂号(A6)";
            ShippingServices["A7"] = "4PX联邮通平邮(A7)";
            ShippingServices["EUB"] = "e邮宝";
            ShippingServices["B9"] = "中国邮政小包(上海)";
            ShippingServices["B6"] = "中国小包平邮(深圳)";
            ShippingServices["B5"] = "中国小包挂号(深圳)";
        }

        public static int sellerMessageTemplatesCount = 5;
        public static string[] sellerMessageTemplates = 
            { "Have you received the item? If yes, did you see any problem? \r\n\r\nThanks very much and have a nice day!\r\n\r\nRegards,",
               "I am composing this mail to ask if you have received the item?\r\n\r\nIf yes, did you see any problem?\r\n\r\nThanks and have a nice day!\r\n\r\nRegards,",
               "Have you received the item? From tracking I saw it is delivered, could you please tell me if you see any problem? \r\n\r\nThanks very much and have a nice day!\r\n\r\nRegards,",
                "Thanks for the confirmation and glad to know you like it :)\r\n\r\n Could you please leave us a feedback at your convenience which is important for us, thanks very much and have a nice day!\r\n\r\nRegards,",
                "Thanks for telling me about this and I am very sorry for the bad delivery.\r\n\r\nI shipped the necklaces on Jan 07, it is too long, it usually takes 3 weeks to your country, but sometimes the shipping maybe delayed due to weather etc reasons." 
                + "\r\n\r\nHere I want to help to solve the problem, either by resending or giving you a full refund, which one do you prefer? could you please tell me?\r\n\r\nThanks for your patience and understanding!\r\n\r\nRegards,"};

        public static EbayConstants Instance = new EbayConstants();

    }
}
