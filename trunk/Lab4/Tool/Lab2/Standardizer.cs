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
        string sentence;
        public string Sentennce
        {
            get { return sentence; }
            set { sentence = value; }
        }

        public Standardizer() { }
        public Standardizer(string pSentence)
        {
            sentence = pSentence.Trim().ToUpper();
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

            sentence = Regex.Replace(sentence, @"\s+", " ");
            return sentence;
        }

        private void StandardizeNotation()
        {
            // Remove new-line character
            sentence = sentence.Replace(".\r\n", ". ");
            sentence = sentence.Replace("\r\n", ". ");
            sentence = sentence + " ";

            sentence = sentence.Replace(", ", " !SP! ");
            sentence = sentence.Replace(". ", " !SIL! ");
            sentence = sentence.Replace(":", " ");
            sentence = sentence.Replace(";", " ");
            sentence = sentence.Replace("(", " ");
            sentence = sentence.Replace(")", " ");
            sentence = sentence.Replace("[", " ");
            sentence = sentence.Replace("]", " ");
            
            sentence = sentence.Trim();
        }
        private void StandardizeMathSymbol()
        {
            sentence = sentence.Replace("@", " A CÒNG");
            sentence = sentence.Replace("#", " THĂNG");
            sentence = sentence.Replace("%", " PHẦN TRĂM");
            sentence = sentence.Replace("+", " CỘNG");
            sentence = sentence.Replace("^", " LŨY THỪA");
            sentence = sentence.Replace("=", " BẰNG");
            sentence = sentence.Replace("->", " SUY RA");
        }
        private void StandardizePlaceNames()
        {
            sentence = sentence.Replace("BRVT", " BÀ RỊA VŨNG TÀU");
            sentence = sentence.Replace("TPHCM", " THÀNH PHỐ HỒ CHÍ MINH");
            sentence = sentence.Replace("TP.HCM", " THÀNH PHỐ HỒ CHÍ MINH");
            sentence = sentence.Replace("DNA", " ĐÔNG NAM Á");
            sentence = sentence.Replace("ĐNA", " ĐÔNG NAM Á");
        }

        private void StandardizeDistance()
        {
            sentence = sentence.Replace("KM", " KÍ LÔ MÉT");
            sentence = sentence.Replace("NM", " NA NÔ MÉT");

            sentence = sentence.Replace("FT", " PHÍCH");
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
                sentence = sentence.Replace(origin, " " + replaced);
            }
        }
        private void StandardizeCurrency()
        {
            sentence = sentence.Replace("$", " ĐÔ LA");
            sentence = sentence.Replace("USD", " ĐÔ LA MỸ");

            sentence = sentence.Replace("€", " Ơ RÔ");
            sentence = sentence.Replace("EURO", " Ơ RÔ");

            sentence = sentence.Replace("£", " BẢNG ANH");
            sentence = sentence.Replace("GBP", " BẢNG ANH");
            sentence = sentence.Replace("penny", " xu");
            sentence = sentence.Replace("pence", " xu");

            sentence = sentence.Replace("¥", " YÊN");
            sentence = sentence.Replace("JPY", " YÊN");

            sentence = sentence.Replace("VND", " ĐỒNG");
            sentence = sentence.Replace("VNĐ", " ĐỒNG");
        }
        private void StandardizeNumber()
        {
            string[] words = sentence.Split(' ');
            sentence = "";

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
                sentence += tmp + " ";
            }
            sentence = sentence.Trim();
        }
        private void StandardizeDate()
        {
            string[] words = sentence.Split(' ');
            sentence = "";
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
                sentence += tmp + " ";
            }
            sentence = sentence.Trim();
        }
    }
}
