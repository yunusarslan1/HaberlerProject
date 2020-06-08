using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HaberlerProject.Models.Tool
{
    public class Pages
    {
        public static string SetUrl(string Url)
        {
            if (Url == null) return "";
            string seourl = "";
            seourl = Url.Trim();
            seourl = seourl.ToLower();
            seourl = seourl.Replace("ğ", "g");
            seourl = seourl.Replace("Ğ", "G");
            seourl = seourl.Replace("ü", "u");
            seourl = seourl.Replace("Ü", "U");
            seourl = seourl.Replace("ş", "s");
            seourl = seourl.Replace("Ş", "S");
            seourl = seourl.Replace("ı", "i");
            seourl = seourl.Replace("İ", "I");
            seourl = seourl.Replace("ö", "o");
            seourl = seourl.Replace("Ö", "O");
            seourl = seourl.Replace("ç", "c");
            seourl = seourl.Replace("Ç", "C");
            seourl = seourl.Replace("-", "+");
            seourl = seourl.Replace(" ", "+");
            seourl = seourl.Trim();
            seourl = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9+]").Replace(seourl, "");
            seourl = seourl.Trim();
            seourl = seourl.Replace("+", "-");

            string seourlson = "";
            string[] a = seourl.Split('-');
            seourlson = string.Join("-", a.Where(x => !string.IsNullOrEmpty(x)));
            return seourlson;
        }
        public static string GetKeyForWebConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}