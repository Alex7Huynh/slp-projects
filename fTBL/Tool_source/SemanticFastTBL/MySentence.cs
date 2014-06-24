using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticFastTBL
{
    public class MySentence
    {        
        List<MyWord> _words;
        List<int> _lstRuleIndex;

        public List<MyWord> Words
        {
            get { return _words; }
            set { _words = value; }
        }
        public List<int> LstRuleIndex
        {
            get { return _lstRuleIndex; }
            set { _lstRuleIndex = value; }
        }

        public MySentence()
        {
            _words = new List<MyWord>();
            _lstRuleIndex = new List<int>();
        }
    }
}
