using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticFastTBL
{
    public class MyRuleTemplate
    {
        // WORD[-1] = ? ∧ WORD[0] = ? ∧ WORD[1] = ? ==> TAG[0] <-- ?
        int _index;        
        string _prevWord;
        string _currWord;
        string _nextWord;
        string _tag;
        int _score;
        List<int> _lstSenIndex;

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
        public string PrevWord
        {
            get { return _prevWord; }
            set { _prevWord = value; }
        }
        public string CurrWord
        {
            get { return _currWord; }
            set { _currWord = value; }
        }
        public string NextWord
        {
            get { return _nextWord; }
            set { _nextWord = value; }
        }
        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        public List<int> LstSenIndex
        {
            get { return _lstSenIndex; }
            set { _lstSenIndex = value; }
        }

        public MyRuleTemplate() { }
        public MyRuleTemplate(int pIndex, string pPrevWord, string pCurrWord, string pNextWord, string pTag)
        {
            _score = 0;
            _lstSenIndex = new List<int>();
            _index = pIndex;
            _prevWord = pPrevWord;
            _currWord = pCurrWord;
            _nextWord = pNextWord;
            _tag = pTag;
        }
        public override string ToString()
        {
            return _prevWord + " ∧ " + _currWord + " ∧ " + _nextWord + " ==> TAG[0] <-- " + _tag;
        }
    }
}