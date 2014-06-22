using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticFastTBL
{
    public class MyRuleTemplate
    {
        // WORD[-1] = ? ∧ WORD[0] = ? ∧ WORD[1] = ? ==> TAG[0] <-- ?
        string _prevWord;
        string _currWord;
        string _nextWord;
        string _tag;
        int _score;        
        List<MySentence> _sentence;

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
        public List<MySentence> Sentence
        {
            get { return _sentence; }
            set { _sentence = value; }
        }

        public MyRuleTemplate() {
            _score = 0;
            _sentence = new List<MySentence>();
        }
        public MyRuleTemplate(string pPrevWord, string pCurrWord, string pNextWord, string pTag)
        {
            _score = 0;
            _sentence = new List<MySentence>();
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
