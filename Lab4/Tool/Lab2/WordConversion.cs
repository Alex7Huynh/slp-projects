using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MTT
{
    public class WordConversion
    {
		private static List<WordConversion> lstDictionary = new List<WordConversion>();
        private static Dictionary<string, string> lstUNI_TELEX = new Dictionary<string, string>();
        private static Dictionary<string, string> lstTELEX_UNI = new Dictionary<string, string>();
        private static List<string> lstConsonant = new List<string>();

        public static Dictionary<string, string> TELEX_UNI
        {
            get { return lstTELEX_UNI; }
            set { lstTELEX_UNI = value; }
        }

        public static List<string> Consonant
        {
            get { return lstConsonant; }
            set { lstConsonant = value; }
        }

        public static Dictionary<string, string> UNI_TELEX
        {
            get { return lstUNI_TELEX; }
            set { lstUNI_TELEX = value; }
        }

        public static List<WordConversion> Dictionary
        {
            get { return lstDictionary; }
            set { lstDictionary = value; }
        }
	
        string strUnicodeWord = string.Empty;
        public string UnicodeWord
        {
            get { return strUnicodeWord; }
            set { strUnicodeWord = value; }
        }

        string strTelexWord = string.Empty;
        public string TelexWord
        {
            get { return strTelexWord; }
            set { strTelexWord = value; }
        }

        string strPhoneWord = string.Empty;
        public string PhoneWord
        {
            get { return strPhoneWord; }
            set { strPhoneWord = value; }
        }

        public WordConversion()
        {
        }
        public WordConversion(string uni, string telex, string phone)
        {
            this.strUnicodeWord = uni;
            this.strTelexWord = telex;
            this.strPhoneWord = phone;
        }
		
		private static bool LoadUNI_TELEX()
        {
            if (!File.Exists(/*Global.DataPath + */"UNI_TELEX"))
            {
                //MessageBox.Show("UNI_TELEX not found!");
                return false;
            }
            using (StreamReader sr = File.OpenText(/*Global.DataPath + */"UNI_TELEX"))
            {
                string strLine = string.Empty;
                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    if (!string.IsNullOrEmpty(strLine))
                    {
                        string[] split = strLine.Split('\t');
                        lstUNI_TELEX.Add(split[0], split[1]);
                        lstTELEX_UNI.Add(split[1], split[0]);
                        //Global.UNI_TELEX.Add(split[0], split[1]);
                        //Global.TELEX_UNI.Add(split[1], split[0]);
                    }
                }
                sr.Close();
            }
            return true;
        }

        private static bool LoadConsonant()
        {
            if (!File.Exists(/*Global.DataPath + */"CONSONANT"))
            {
                //MessageBox.Show("CONSONANT not found!");
                return false;
            }
            using (StreamReader sr = File.OpenText(/*Global.DataPath + */"CONSONANT"))
            {
                string strLine = string.Empty;
                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    if (!string.IsNullOrEmpty(strLine))
                        lstConsonant.Add(strLine);
                        //Global.Consonant.Add(strLine);
                }
                sr.Close();
            }
            return true;
        }

        private static bool LoadDictionary()
        {
            if (!File.Exists(/*Global.DataPath + */"DICT_UNI"))
            {
                //MessageBox.Show("DICT not found!");
                return false;
            }
            using (StreamReader sr = File.OpenText(/*Global.DataPath + */"DICT_UNI"))
            {
                string strLine = string.Empty;
                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    if (!string.IsNullOrEmpty(strLine))
                    {
                        WordConversion w = new WordConversion();
                        w.UnicodeWord = strLine;
                        w.TelexWord = WordConversion.ConvertUnicodeToTelex(strLine);
                        w.PhoneWord = WordConversion.ConvertUnicodeToPhone(strLine);
                        lstDictionary.Add(w);
                        //Global.Dictionary.Add(w);
                    }
                }
                sr.Close();
            }
            return true;
        }
		static WordConversion()
		{
			LoadConsonant();
            LoadUNI_TELEX();
            LoadDictionary();
		}
        public static string ConvertUnicodeToPhone(string uni)
        {
            uni = uni.ToUpper();
            string strTelex = string.Empty;
            int intPos = 0;
            if (uni.Length >= 3 && Consonant.Contains(uni.Substring(0, 3)))
            {
                strTelex = uni.Substring(0, 3);
                intPos = 3;
            }
            else if (uni.Length >= 2 && Consonant.Contains(uni.Substring(0, 2)))
            {
                strTelex = uni.Substring(0, 2);
                intPos = 2;
            }
            else if (uni.Length >= 1 && Consonant.Contains(uni.Substring(0, 1)))
            {
                strTelex = uni.Substring(0, 1);
                intPos = 1;
            }
            strTelex += " ";
            for (; intPos < uni.Length; intPos++)
            {
                if (UNI_TELEX.ContainsKey(uni[intPos].ToString()))
                {
                    var item = UNI_TELEX.Single(x => x.Key.Equals(uni[intPos].ToString()));
                    strTelex += item.Value + " ";
                }
                else
                    break;
            }
            strTelex += uni.Substring(intPos);
            return strTelex.Trim();
        }
        public static string ConvertUnicodeToTelex(string uni)
        {
            uni = uni.ToUpper();
            string strTelex = string.Empty;
            int intPos = 0;
            if (uni.Length >= 3 && Consonant.Contains(uni.Substring(0, 3)))
            {
                strTelex = uni.Substring(0, 3);
                intPos = 3;
            }
            else if (uni.Length >= 2 && Consonant.Contains(uni.Substring(0, 2)))
            {
                strTelex = uni.Substring(0, 2);
                intPos = 2;
            }
            else if (uni.Length >= 1 && Consonant.Contains(uni.Substring(0, 1)))
            {
                strTelex = uni.Substring(0, 1);
                intPos = 1;
            }
            for (; intPos < uni.Length; intPos++)
            {
                if (UNI_TELEX.ContainsKey(uni[intPos].ToString()))
                {
                    var item = UNI_TELEX.Single(x => x.Key.Equals(uni[intPos].ToString()));
                    strTelex += item.Value;
                }
                else
                    break;
            }
            strTelex += uni.Substring(intPos);
            return strTelex;
        }
        public static string NormalizePhone(string phone) {
            string result = phone;
            string[] NguyenAm = {"A", "E", "I", "O", "U", "AW", "AA", "EE", "OO", "OW", "UW", "Y"};
            string[] BoDau = {"F", "S", "R", "X", "J"};
            for(int i = 0; i < NguyenAm.Length; ++i)
                for (int j = 0; j < BoDau.Length; ++j)
                {
                    string oldS = NguyenAm[i] + BoDau[j];
                    string newS = NguyenAm[i] + " <" + BoDau[j] + ">";
                    result = result.Replace(oldS, newS);
                }
            return result;
        }
    }
}
