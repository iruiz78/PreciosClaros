using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class Setting
    {
        public static string LOG_FILE
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["LOG_FILE"];
                }
                catch (Exception ex)
                {
                    Logs.LogFile("LOG_FILE:" + ex.ToString());
                    return "";
                }
            }
        }
        public static string Get_ConnectionString
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["sql"].ToString();
                }
                catch (Exception ex)
                {
                    Logs.LogFile("Get_ConnectionString:" + ex.ToString());
                    return "";
                }
            }
        }
        public static string GetUrlBranchOffice
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["UrlBranchOffice"];
                }
                catch (Exception ex)
                {
                    Logs.LogFile("GetUrlBranchOffice:" + ex.ToString());
                    return "";
                }
            }
        }
        public static string GetUrlBranchOfficeProducts
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["UrlBranchOfficeProducts"];
                }
                catch (Exception ex)
                {
                    Logs.LogFile("GetUrlBranchOfficeProducts:" + ex.ToString());
                    return "";
                }
            }
        }
        public static string GetUrlPriceProduct
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["UrlPriceProduct"];
                }
                catch (Exception ex)
                {
                    Logs.LogFile("GetUrlPriceProduct:" + ex.ToString());
                    return "";
                }
            }
        }
    }
}
