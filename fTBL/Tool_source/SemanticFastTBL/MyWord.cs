using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticFastTBL
{
    public class MyWord
    {
        string _word;
        string _POS;
        string _tag;

        public string Word
        {
            get { return _word; }
            set { _word = value; }
        }        
        public string POS
        {
            get { return _POS; }
            set { _POS = value; }
        }
        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public MyWord() { }
        public MyWord(string pWord, string pPOS, string pTag)
        {
            _word = pWord;
            _POS = pPOS;
            _tag = pTag;
        }
        public override string ToString()
        {
            string str = _word + " " + _POS + " " + _tag;            
            return str;
        }
    }
}