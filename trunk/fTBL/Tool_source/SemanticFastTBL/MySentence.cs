using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticFastTBL
{
    public class MySentence
    {
        List<MyWord> _words;
        List<MyRuleTemplate> _rules;

        public List<MyWord> Words
        {
            get { return _words; }
            set { _words = value; }
        }
        public List<MyRuleTemplate> Rules
        {
            get { return _rules; }
            set { _rules = value; }
        }

        public MySentence() 
        {
            _words = new List<MyWord>();
            _rules = new List<MyRuleTemplate>();
        }
    }
}
