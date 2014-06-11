using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MTT
{
    class Standardizer
    {
        string sentennce;
        public string Sentennce
        {
            get { return sentennce; }
            set { sentennce = value; }
        }

        public Standardizer() { }
        public Standardizer(string pSentence)
        {
            sentennce = pSentence.Trim().ToUpper();
        }
        public string Standardize(bool bTelex)
        {
            // Standardize
            StandardizeNotation();
            StandardizeMathSymbol();
            StandardizePlaceNames();
            StandardizeDistance();
            StandardizeTemperature();
            StandardizeCurrency();
            StandardizeNumber();
            StandardizeDate();

            if (!bTelex)
                return Regex.Replace(sentennce, @"\s+", " ");

            // Convert to Telex
            string result = "";
            string[] words = sentennce.Split(' ');
            for (int i = 0; i < words.Length; ++i)
            {
                result += WordConversion.ConvertUnicodeToTelex(words[i]) + " ";
            }
            result = result.Trim();
            result = Regex.Replace(result, @"\s+", " ");
            return result;
        }

        private void StandardizeNotation()
        {
            sentennce = sentennce.Replace(", ", " ");
            sentennce = sentennce.Replace(":", " ");
            sentennce = sentennce.Replace(";", " ");
            sentennce = sentennce.Replace("(", " ");
            sentennce = sentennce.Replace(")", " ");
            sentennce = sentennce.Replace("[", " ");
            sentennce = sentennce.Replace("]", " ");
            sentennce = sentennce.Replace(". ", " ");
            sentennce = sentennce.Trim();
        }
        private void StandardizeMathSymbol()
        {
            sentennce = sentennce.Replace("@", " A CÒNG");
            sentennce = sentennce.Replace("#", " THĂNG");
            sentennce = sentennce.Replace("%", " PHẦN TRĂM");
            sentennce = sentennce.Replace("+", " CỘNG");
            sentennce = sentennce.Replace("^", " LŨY THỪA");
            sentennce = sentennce.Replace("=", " BẰNG");
            sentennce = sentennce.Replace("->", " SUY RA");
        }
        private void StandardizePlaceNames()
        {
            sentennce = sentennce.Replace("BRVT", " BÀ RỊA VŨNG TÀU");
            sentennce = sentennce.Replace("TPHCM", " THÀNH PHỐ HỒ CHÍ MINH");
            sentennce = sentennce.Replace("TP.HCM", " THÀNH PHỐ HỒ CHÍ MINH");
            sentennce = sentennce.Replace("DNA", " ĐÔNG NAM Á");
            sentennce = sentennce.Replace("ĐNA", " ĐÔNG NAM Á");
        }

        private void StandardizeDistance()
        {
            sentennce = sentennce.Replace("KM", " KÍ LÔ MÉT");
            sentennce = sentennce.Replace("NM", " NA NÔ MÉT");

            sentennce = sentennce.Replace("FT", " PHÍCH");            
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
        private void StandardizeNumber()
        {
            string[] words = sentennce.Split(' ');
            sentennce = "";
            foreach (string s in words)
            {
                string tmp = s;
                if (s.Count(f => f == '.') == 1) // maybe floating point
                {
                    string sReal = s.Split('.')[0];
                    string sDecimal = s.Split('.')[1];
                    int r,d;
                    if (int.TryParse(sReal, out r) && int.TryParse(sDecimal, out d)) // floating point
                    {
                        sReal = r.ToString();
                        sDecimal = d.ToString();
                        tmp = NumToString.convert(sReal).ToUpper() + " CHẤM " + NumToString.convert(sDecimal).ToUpper();
                    }
                }
                else
                {
                    int n;
                    if (int.TryParse(s, out n))  // integer
                    {
                        tmp = NumToString.convert(s).ToUpper();
                    }

                }
                sentennce += tmp + " ";
            }
            sentennce = sentennce.Trim();
        }
        private void StandardizeDate()
        {
            string[] words = sentennce.Split(' ');
            sentennce = "";
            foreach (string s in words)
            {
                string tmp = s;
                int count = s.Count(f => f == '/');
                if (count == 2)
                {
                    string sDay = s.Split('/')[0];
                    sDay = int.Parse(sDay).ToString(); // 05 -> 5
                    string sMonth = s.Split('/')[1];
                    sMonth = int.Parse(sMonth).ToString();
                    string sYear = s.Split('/')[2];
                    sYear = int.Parse(sYear).ToString();

                    tmp = " " + NumToString.convert(sDay).ToUpper();
                    tmp += " THÁNG " + NumToString.convert(sMonth).ToUpper();
                    tmp += " NĂM " + NumToString.convert(sYear).ToUpper();
                }
                sentennce += tmp + " ";
            }
            sentennce = sentennce.Trim();
        }
    }
}
