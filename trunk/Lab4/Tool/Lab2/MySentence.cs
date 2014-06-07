using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MTT
{
    class MySentence
    {
        string sentennce;
        public string Sentennce
        {
            get { return sentennce; }
            set { sentennce = value; }
        }

        public MySentence() { }
        public MySentence(string pSentence)
        {
            sentennce = pSentence.Trim().ToUpper();
        }
        public string Standardize()
        {
            // Standardize
            StandardizePlaceNames();
            StandardizeTemperature();
            StandardizeCurrency();
            StandardizeFloatingPoint();
            StandardizeDate();

            // Convert to Telex
            string result = "";
            string[] words = sentennce.Split(' ');
            for (int i = 0; i < words.Length; ++i)
            {                
                result += Word.ConvertUnicodeToTelex(words[i]) + " ";
            }
            result = result.Trim();
            result = Regex.Replace(result, @"\s+", " ");
            return result;
        }

        private void StandardizePlaceNames()
        {
            sentennce = sentennce.Replace("TP.HCM", " THÀNH PHỐ HỒ CHÍ MINH");
        }
        private void StandardizeTemperature()        
        {
            sentennce = sentennce.Replace("°F", " ĐỘ ÉP");
            sentennce = sentennce.Replace("°C", " ĐỘ XÊ");

        }
        private void StandardizeCurrency()
        {
            sentennce = sentennce.Replace("$", " ĐÔ LA");
            sentennce = sentennce.Replace("USD", " ĐÔ LA MỸ");

            sentennce = sentennce.Replace("€", " Ơ RÔ");
            sentennce = sentennce.Replace("EURO", " Ơ RÔ");

            sentennce = sentennce.Replace("£", " BẢNG ANH");
            sentennce = sentennce.Replace("GBP", " BẢNG ANH");
            sentennce = sentennce.Replace("penny", " xu");
            sentennce = sentennce.Replace("pence", " xu");

            sentennce = sentennce.Replace("¥", " YÊN");
            sentennce = sentennce.Replace("JPY", " YÊN");
            
            sentennce = sentennce.Replace("VND", " ĐỒNG");
            sentennce = sentennce.Replace("VNĐ", " ĐỒNG");
        }
        private void StandardizeFloatingPoint()
        {

        }
        private void StandardizeDate()
        {

        }
    }
}
