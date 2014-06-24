using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticFastTBL
{
    public class FastTBL
    {
        List<string> _nounSemanticRoles;    // list of semantic roles for noun
        List<string> _verbSemanticRoles;    // list of semantic roles for verb
        List<string> _adjSemanticRoles;     // list of semantic roles for adjective
        List<string> _lstPOS;               // list of Part-Of-Speech

        int nPOS;   // Number of Part-Of-Speech

        List<MyRuleTemplate> _lstRuleTemplate;          // list of Rule template
        List<MyRuleTemplate> _lstTrainedRuleTemplate;   // list of trained rule template
        List<MySentence> _trainDataLabeled;             // Original train data
        List<MySentence> _trainData;                    // Train data (will be unlabeled)
        List<MySentence> _testData;                     // Test data (will be unlabeled)

        // Constructor
        public FastTBL() { }
        // Init _lstPOS from file
        private void InitPOSList()
        {
            _lstPOS = new List<string>();
            string[] lines = System.IO.File.ReadAllLines("files\\pos_list.txt");
            nPOS = lines.Length;
            for (int i = 0; i < lines.Length; ++i)
            {
                _lstPOS.Add(lines[i].Split('\t')[1]);
            }
        }
        // Init semantic roles from file
        private void InitSemanticRoles()
        {
            _nounSemanticRoles = new List<string>();
            _verbSemanticRoles = new List<string>();
            _adjSemanticRoles = new List<string>();

            string[] lines = System.IO.File.ReadAllLines("files\\semantic_roles.txt");
            int i = 0;
            for (; i < lines.Length; ++i)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    break;
                _nounSemanticRoles.Add(lines[i]);
            }
            i++;
            for (; i < lines.Length; ++i)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    break;
                _verbSemanticRoles.Add(lines[i]);
            }
            i++;
            for (; i < lines.Length; ++i)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    break;
                _adjSemanticRoles.Add(lines[i]);
            }
        }
        // Generate rule templates
        private void GenerateRuleTemplates()
        {
            _lstRuleTemplate = new List<MyRuleTemplate>();
            // NN - 12, NNS - 15
            for (int i = 0; i < nPOS; i++)
            {
                for (int j = 0; j < nPOS; ++j)
                {
                    for (int k = 0; k < _nounSemanticRoles.Count; k++)
                    {
                        _lstRuleTemplate.Add(new MyRuleTemplate(_lstRuleTemplate.Count, _lstPOS[i], _lstPOS[11], _lstPOS[j], _nounSemanticRoles[k]));
                        _lstRuleTemplate.Add(new MyRuleTemplate(_lstRuleTemplate.Count, _lstPOS[i], _lstPOS[14], _lstPOS[j], _nounSemanticRoles[k]));
                    }
                }
            }
            // VB - 27, VBD, VBG, VBN, VBP, VBZ
            for (int i = 0; i < nPOS; i++)
            {
                for (int j = 0; j < nPOS; ++j)
                {
                    for (int k = 0; k < _verbSemanticRoles.Count; k++)
                    {
                        for (int m = 26; m <= 31; m++)
                            _lstRuleTemplate.Add(new MyRuleTemplate(_lstRuleTemplate.Count, _lstPOS[i], _lstPOS[m], _lstPOS[j], _verbSemanticRoles[k]));
                    }
                }
            }
            // JJ - 7, JJR, JJS
            for (int i = 0; i < nPOS; i++)
            {
                for (int j = 0; j < nPOS; ++j)
                {
                    for (int k = 0; k < _adjSemanticRoles.Count; k++)
                    {
                        for (int m = 6; m <= 8; m++)
                            _lstRuleTemplate.Add(new MyRuleTemplate(_lstRuleTemplate.Count, _lstPOS[i], _lstPOS[m], _lstPOS[j], _adjSemanticRoles[k]));
                    }
                }
            }
        }
        // Load train data into _trainData
        private List<MySentence> LoadTrainData()
        {
            List<MySentence> lst = new List<MySentence>();
            string[] lines = System.IO.File.ReadAllLines("files\\data_train.txt");
            MySentence sentence = new MySentence();
            for (int i = 0; i < lines.Length; ++i)
            {
                if (string.IsNullOrEmpty(lines[i]) || i == lines.Length - 1)
                {
                    if (sentence.Words.Count > 0)
                    {
                        lst.Add(sentence);
                        sentence = new MySentence();
                    }
                }
                else
                {
                    string[] parts = lines[i].Split(' ');
                    var part2 = (parts.Length == 3) ? parts[2] : null;
                    sentence.Words.Add(new MyWord(parts[0], parts[1], part2));
                }
            }
            return lst;
        }
        // Load test data into _testData
        private void LoadTestData(string str)
        {
            _testData = new List<MySentence>();
            string[] lines = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            MySentence sentence = new MySentence();
            for (int i = 0; i < lines.Length; ++i)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    if (sentence.Words.Count > 0)
                    {
                        _testData.Add(sentence);
                        sentence = new MySentence();
                    }
                }
                else
                {
                    string[] parts = lines[i].Split(' ');
                    var part2 = (parts.Length == 3) ? parts[2] : null;
                    sentence.Words.Add(new MyWord(parts[0], parts[1], part2));
                    if (i == lines.Length - 1 && sentence.Words.Count > 0)
                        _testData.Add(sentence);
                }
            }
        }
        // Set random semantic role for each word
        private void SetBaseLine(List<MySentence> lst)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                for (int j = 0; j < lst[i].Words.Count; j++)
                {
                    String pos = lst[i].Words[j].POS;
                    // NN, NNS
                    if (pos == _lstPOS[11] /*|| pos == _lstPOS[12] || pos == _lstPOS[13]*/ || pos == _lstPOS[14])
                    {
                        lst[i].Words[j].Tag = _nounSemanticRoles[0];
                    }
                    // VB, VBD, VBG, VBN, VBP, VBZ
                    else if (pos == _lstPOS[26] || pos == _lstPOS[27] || pos == _lstPOS[28]
                        || pos == _lstPOS[29] || pos == _lstPOS[30] || pos == _lstPOS[31] || pos == _lstPOS[32])
                    {
                        lst[i].Words[j].Tag = _verbSemanticRoles[0];
                    }
                    // JJ, JJR, JJS
                    else if (pos == _lstPOS[6] || pos == _lstPOS[7] || pos == _lstPOS[8])
                    {
                        lst[i].Words[j].Tag = _adjSemanticRoles[0];
                    }
                }
            }
        }
        // Apply a rule template on data
        public void ApplyRuleTemplate(MyRuleTemplate rt, List<MySentence> lst, bool bSave)
        {
            for (int j = 0; j < lst.Count; j++)
            {
                MySentence sentence = lst[j];
                for (int k = 1; k < sentence.Words.Count - 1; k++)
                {
                    MyWord prevWord = sentence.Words[k - 1];
                    MyWord currWord = sentence.Words[k];
                    MyWord nextWord = sentence.Words[k + 1];

                    if (string.Equals(prevWord.POS, rt.PrevWord)
                        && string.Equals(currWord.POS, rt.CurrWord)
                        && string.Equals(nextWord.POS, rt.NextWord))
                    {
                        lst[j].Words[k].Tag = rt.Tag;
                        if(bSave) rt.LstSenIndex.Add(j);
                    }
                }
            }
            rt.LstSenIndex = rt.LstSenIndex.Distinct().ToList();
        }
        // Evaluate a rule template
        private bool Evaluate(MyRuleTemplate rt)
        {            
            int good = 0;
            int bad = 0;
            bool bFlag = false; // good's not increase
            for (int i = 0; i < _trainData.Count; i++)
            {
                MySentence sentence = _trainData[i];
                for (int j = 1; j < sentence.Words.Count - 1; j++)
                {
                    MyWord prevWord = sentence.Words[j - 1];
                    MyWord currWord = sentence.Words[j];
                    MyWord nextWord = sentence.Words[j + 1];

                    if (string.Equals(prevWord.POS, rt.PrevWord)
                        && string.Equals(currWord.POS, rt.CurrWord)
                        && string.Equals(nextWord.POS, rt.NextWord))
                    {
                        if (!string.Equals(currWord.Tag, rt.Tag))    // this word has different tag (semantic role)
                        {
                            if (string.Equals(rt.Tag, _trainDataLabeled[i].Words[j].Tag)) // C[s] ≠ T[s] ∧ C[r(s)] = T[s]}|
                            {
                                good++;
                                bFlag = true;
                                sentence.LstRuleIndex.Add(rt.Index);
                            }
                            else // C[s]=T[s] ∧ C[r(s)] ≠ T[s]
                            {
                                bad++;
                            }
                        }
                    }
                }
            }
            rt.Score = good - bad;
            return bFlag;
        }
        // Train
        private void TrainRuleTemplates()
        {
            _lstTrainedRuleTemplate = new List<MyRuleTemplate>();
            int maxScore;
            bool bBestRuleFound;
            MyRuleTemplate bestRT = new MyRuleTemplate();
            List<int> affectRT = new List<int>();
            bool bFirst = true;
            do
            {
                maxScore = 0;
                bBestRuleFound = false;
                int bestRuleIndex = 0;
                List<int> lstIndexRemoved = new List<int>();

                for (int i = 0; i < _lstRuleTemplate.Count; ++i)
                {
                    MyRuleTemplate rt = _lstRuleTemplate[i];                    
                    bool scoreResult = true;

                    if (bFirst)
                    {
                        scoreResult = Evaluate(rt);
                    }
                    else
                    {
                        // Only re-evaluate rules in affectRT
                        if (affectRT.Any(item => item == rt.Index))
                            Evaluate(rt);
                    }

                    if (scoreResult)
                    {
                        if (rt.Score > maxScore)
                        {
                            maxScore = rt.Score;
                            bestRuleIndex = i;
                            bBestRuleFound = true;
                        }
                    }
                    else
                    {
                        lstIndexRemoved.Add(i);
                    }
                }

                bFirst = false;

                if (bBestRuleFound)
                {
                    // Add best rule template to _lstTrainedRuleTemplate
                    _lstTrainedRuleTemplate.Add(_lstRuleTemplate[bestRuleIndex]);
                    // Apply best rule template to _trainData
                    bestRT = _lstRuleTemplate[bestRuleIndex];
                    ApplyRuleTemplate(bestRT, _trainData, true);
                    // Find all rule templates affecting sentences which bestRT fixed
                    foreach(int senIndex in bestRT.LstSenIndex)
                    {
                        affectRT.AddRange(_trainData[senIndex].LstRuleIndex);
                    }
                    affectRT = affectRT.Distinct().ToList();
                }
                // Remove insignificant rules
                for (int i = lstIndexRemoved.Count - 1; i >= 0; i--)
                    _lstRuleTemplate.RemoveAt(lstIndexRemoved[i]);
                // Remove best rule from _lstRuleTemplate
                if(bestRT != null)                
                    _lstRuleTemplate.Remove(bestRT);
            } while (maxScore > 0 && bBestRuleFound);
        }
        // Output trained rule template to string (textbox)
        public string OutputTrainedRuleTemplate()
        {
            string result = "";
            int i = 1;
            foreach (MyRuleTemplate rt in _lstTrainedRuleTemplate)
            {
                result += string.Format("{4}) WORD[-1] = {0} ∧ WORD[0] = {1} ∧ WORD[1] = {2} ==> TAG[0] <-- {3}",
                    rt.PrevWord, rt.CurrWord, rt.NextWord, rt.Tag, i++);
                result += "\r\n";
            }
            result = result.Remove(result.Length - 2);
            return result;
        }
        // Output test data to string (textbox)
        public string OutputTestData()
        {
            string result = "";
            foreach (MySentence sen in _testData)
            {
                foreach (MyWord w in sen.Words)
                {
                    result += w.Word + " " + w.POS + " " + w.Tag;
                    result += "\r\n";
                }
                result += "\r\n";
            }
            result = result.Remove(result.Length - 4);
            return result;
        }
        // Main method - train
        public void Train()
        {
            InitPOSList();
            InitSemanticRoles();
            GenerateRuleTemplates();
            _trainDataLabeled = LoadTrainData();
            _trainData = LoadTrainData();            
            SetBaseLine(_trainData);
            TrainRuleTemplates();
        }
        // Main method - test  on string of data        
        public void Run(string str)
        {
            LoadTestData(str);
            SetBaseLine(_testData);
            // Find semantic roles
            for (int i = 0; i < _lstTrainedRuleTemplate.Count; i++)
            {
                MyRuleTemplate rt = _lstTrainedRuleTemplate[i];                
                ApplyRuleTemplate(rt, _testData, false);
            }
        }
    }
}