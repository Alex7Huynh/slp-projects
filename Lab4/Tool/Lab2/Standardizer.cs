using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace MTT
{
    class Standardizer
    {
        private const string DictFolder = "StandardDict";
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
        public string Standardize()
        {
            // Standardize
            StandardizeNotation();
            string[] files = Directory.GetFiles(DictFolder, "*.txt");
            foreach (string s in files)
            {
                Standardize(s);
            }
            StandardizeNumber();
            StandardizeDate();

            sentennce = Regex.Replace(sentennce, @"\s+", " ");
            return sentennce;
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

        private void Standardize(string filename)
        {
            //sentennce = sentennce.Replace("°F", " ĐỘ ÉP");
            //sentennce = sentennce.Replace("°C", " ĐỘ XÊ");
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string origin = line.Split('\t')[0];
                string replaced = line.Split('\t')[1];
                sentennce = sentennce.Replace(origin, " " + replaced);
            }
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

            for (int i = 0; i < words.Length; ++i)
            {
                string s = words[i];
                string tmp = s;
                if (s.Count(f => f == '.') == 1) // maybe floating point
                {
                    string sReal = s.Split('.')[0];
                    string sDecimal = s.Split('.')[1];
                    int l, r;
                    if (int.TryParse(sReal, out l) && int.TryParse(sDecimal, out r)) // floating point
                    {
                        sReal = l.ToString();
                        sDecimal = r.ToString();
                        tmp = NumToString.convert(sReal).ToUpper() + " CHẤM " + NumToString.convert(sDecimal).ToUpper();                        
                    }
                }
                else if (s == "-")
                {
                    int l, r;
                    string LeftStr = (i - 1 >= 0) ? words[i - 1] : null;
                    string RightStr = (i + 1 < words.Length) ? words[i + 1] : null;
                    if (int.TryParse(LeftStr, out l) && int.TryParse(RightStr, out r))
                        tmp = " TRỪ ";
                }
                else if (s == "*")
                {
                    int l, r;
                    string LeftStr = (i - 1 >= 0) ? words[i - 1] : null;
                    string RightStr = (i + 1 < words.Length) ? words[i + 1] : null;
                    if (int.TryParse(LeftStr, out l) && int.TryParse(RightStr, out r))
                        tmp = " NHÂN ";
                }
                else if (s == "/")
                {
                    int l, r;
                    string LeftStr = (i - 1 >= 0) ? words[i - 1] : null;
                    string RightStr = (i + 1 < words.Length) ? words[i + 1] : null;
                    if (int.TryParse(LeftStr, out l) && int.TryParse(RightStr, out r))
                        tmp = " CHIA ";
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
