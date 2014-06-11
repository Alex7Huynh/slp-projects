using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MTT
{
    public class MyWord
    {
        int secondStart;
        int secondEnd;
        string str;
        string filename;
        string leftStr;
        string rightStr;

        public int SecondStart
        {
            get { return secondStart; }
            set { secondStart = value; }
        }
        public int SecondEnd
        {
            get { return secondEnd; }
            set { secondEnd = value; }
        }
        public string Str
        {
            get { return str; }
            set { str = value; }
        }
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        public string LeftStr
        {
            get { return leftStr; }
            set { leftStr = value; }
        }
        public string RightStr
        {
            get { return rightStr; }
            set { rightStr = value; }
        }

        public MyWord() { }
        public MyWord(int pSecondStart, int pSecondEnd, string pStr, string pFilename)
        {
            str = pStr;
            secondStart = pSecondStart;
            secondEnd = pSecondEnd;
            filename = pFilename;
        }

        // Read recout.mlf & get all words (MyWord)
        public static List<MyWord> ReadFile(string filename, string folderTrain)
        {
            List<MyWord> result = new List<MyWord>();

            if (!File.Exists(filename))
                return null;
            var lines = System.IO.File.ReadAllLines(filename);
            var sentences = new List<string>();
            string WavFilename = string.Empty;
            foreach (var line in lines)
            {
                //var l = line.Replace("\t", string.Empty).Replace("\n", string.Empty).ToLower();

                if (line.Contains("#!mlf!#"))
                    continue;

                if (line.Contains("*/"))
                {
                    WavFilename = line.Substring(line.IndexOf('/') + 1);
                    WavFilename = WavFilename.Remove(WavFilename.Length - 5);
                    WavFilename = folderTrain + "\\" + WavFilename + ".wav";
                    sentences = new List<string>();
                    continue;
                }
                if (line.Contains("."))
                {
                    for (int i = 0; i < sentences.Count; i++)
                    {
                        var parts = sentences[i].Split(' ');
                        MyWord w = new MyWord(int.Parse(parts[0]) / 10000, int.Parse(parts[1]) / 10000, parts[2], WavFilename);                        
                        w.LeftStr = (i - 1 >= 0) ? sentences[i - 1].Split(' ')[2] : null;
                        w.RightStr = (i + 1 < sentences.Count) ? sentences[i + 1].Split(' ')[2] : null;
                        result.Add(w);      
                    }
                    WavFilename = string.Empty;
                    continue;
                }
                sentences.Add(line);
            }
            return result;
        }

        // Read recout.mlf & get first word suitable (unigram, not compare left-right)
        public static MyWord ReadFile(string filename, string str, string folderTrain)
        {
            if (!File.Exists(filename))
                return null;
            //List<List<MyWord>> result = new List<List<MyWord>>();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            file.ReadLine();    // #!MLF!#            
            string WavFilename = "";
            while ((line = file.ReadLine()) != null)
            {
                //List<MyWord> lSentence = new List<MyWord>();
                if (line.Contains(".rec"))
                {
                    WavFilename = line.Substring(line.IndexOf('/')+1);
                    WavFilename = WavFilename.Remove(WavFilename.Length - 5);
                    WavFilename = "train\\" + WavFilename + ".wav";
                }
                while ((line = file.ReadLine()) != ".")
                {
                    string[] parts = line.Split(' ');
                    if (parts[2] == str)
                    {
                        MyWord w = new MyWord(int.Parse(parts[0]) / 10000, int.Parse(parts[1]) / 10000, parts[2], WavFilename);                        
                        return w;
                    }
                }
            }
            file.Close();
            return null;
        }
    }
}
