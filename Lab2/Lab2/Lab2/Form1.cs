﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDi2Mn_Click(object sender, EventArgs e)
        {            
            List<String> lstWord = makeDict2Monophone("DICT");           
            output2File(lstWord);
            MessageBox.Show("Create 2 files MONOPHONES successfully!", "Info");
        }
        public List<String> makeDict2Monophone(string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);
            List<String> lstWord = new List<string>();
            foreach (string line in lines)
            {                
                string[] words = Regex.Split(line, " ");
                
                for(int i = 1; i < words.Length-1; ++i)
                {
                    if (words[i].Contains(" ") || words[i] == "")
                        continue;
                    lstWord.Add(words[i]);
                }
            }
            
            lstWord = lstWord.Distinct().ToList();
            lstWord.Sort();
            return lstWord;
        }
        public void output2File(List<String> lstWord)
        {
            StreamWriter fMonophone0 = new StreamWriter("monophones0");
            StreamWriter fMonophone1 = new StreamWriter("monophones1");

            foreach (string word in lstWord)
            {
                fMonophone0.WriteLine(word);
                fMonophone1.WriteLine(word);
            }
            fMonophone0.WriteLine("sil");
            //fMonophone0.WriteLine("");
            fMonophone1.WriteLine("sil");
            fMonophone1.WriteLine("sp");
            //fMonophone1.WriteLine("");

            fMonophone0.Close();
            fMonophone1.Close();
        }

        private void btnCreateDICT_Click(object sender, EventArgs e)
        {
            string TrainFilePath = tbTrainFilePath.Text;
            //int TrainFileCount = int.Parse(tbTrainFileCount.Text);
            //string path = "E:\\sn0040\\train";
            string[] files = System.IO.Directory.GetFiles(TrainFilePath, "*.txt");
            int TrainFileCount = files.Length;

            List<String> lstWord = new List<string>();

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string[] lines = System.IO.File.ReadAllLines(files[i]);
                string sentence = lines[0];
                string[] words = sentence.Split(' ');
                for (int j = 0; j < words.Length; ++j)
                {
                    string word1 = Word.ConvertUnicodeToTelex(words[j]);
                    string word2 = Word.ConvertUnicodeToPhone(words[j]);
                    word2 = Word.NormalizePhone(word2);
                    string word = word1 + "    " + word2 + " sp";
                    
                    lstWord.Add(word);
                }
            }
            lstWord = lstWord.Distinct().ToList();
            lstWord.Sort();
            StreamWriter fDICT = new StreamWriter("DICT");
            for (int i = 0; i < lstWord.Count - 1; ++i)
            {
                fDICT.WriteLine(lstWord[i]);
            }
            fDICT.Write(lstWord[lstWord.Count-1]);
            fDICT.Close();


            MessageBox.Show("Create DICT for " + TrainFileCount + " files successfully!", "Info");
        }

        private void btnCreatePROMPTS_Click(object sender, EventArgs e)
        {
            string TrainFilePath = tbTrainFilePath.Text;
            string lastFolder = TrainFilePath.Substring(TrainFilePath.LastIndexOf('\\')+1);
            string[] files = System.IO.Directory.GetFiles(TrainFilePath, "*.txt");
            int TrainFileCount = files.Length;
            StreamWriter fPROMPTS = new StreamWriter("PROMPTS");
            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TrainFilePath, "");
                filename = filename.Remove(0, 1);
                
                string[] lines = System.IO.File.ReadAllLines(files[i]);
                string newSentence = "";
                string sentence = lines[0];
                string[] words = sentence.Split(' ');
                for (int j = 0; j < words.Length-1; ++j)
                {
                    string word = Word.ConvertUnicodeToTelex(words[j]);
                    newSentence += word + " ";
                }
                newSentence += Word.ConvertUnicodeToTelex(words[words.Length-1]);
                fPROMPTS.WriteLine(lastFolder + "\\" + filename + "  " + newSentence);
            }
            fPROMPTS.Close();
            MessageBox.Show("Create PROMPTS successfully!", "Info");
        }

        private void btnCreateWORDS_Click(object sender, EventArgs e)
        {
            StreamWriter fWORDS = new StreamWriter("WORDS.MLF");
            fWORDS.WriteLine("#!MLF!#");

            string TrainFilePath = tbTrainFilePath.Text;
            string[] files = System.IO.Directory.GetFiles(TrainFilePath, "*.txt");
            int TrainFileCount = files.Length;
            
            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TrainFilePath, "");
                filename = filename.Remove(0, 1);
                filename = filename.Replace("txt", "lab");

                fWORDS.WriteLine("\"*/" + filename + "\"");                

                string[] lines = System.IO.File.ReadAllLines(files[i]);                
                string sentence = lines[0];
                string[] words = sentence.Split(' ');
                for (int j = 0; j < words.Length; ++j)
                {
                    string word = Word.ConvertUnicodeToTelex(words[j]);
                    fWORDS.WriteLine(word);
                }
                fWORDS.WriteLine(".");                
            }
            fWORDS.Close();
            MessageBox.Show("Create WORDS.MLF successfully!", "Info");
        }

        private void btnCreateMFC_Click(object sender, EventArgs e)
        {
            StreamWriter fMFC = new StreamWriter("mfcc.scp");
            string TrainFilePath = tbTrainFilePath.Text;
            string lastFolder = TrainFilePath.Substring(TrainFilePath.LastIndexOf('\\') + 1);
            
            string[] files = System.IO.Directory.GetFiles(TrainFilePath, "*.wav");
            int TrainFileCount = files.Length;

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TrainFilePath, "");                
                string filename2 = filename.Replace("wav", "mfc");
                fMFC.WriteLine(lastFolder + filename + "  MFC" + filename2);
            }
            fMFC.Close();
            MessageBox.Show("Create mfcc.scp successfully!", "Info");
        }
        private void btnCreateTrainingSCP_Click(object sender, EventArgs e)
        {
            StreamWriter fTraining = new StreamWriter("train.scp");
            string TrainFilePath = tbTrainFilePath.Text;
            string lastFolder = TrainFilePath.Substring(TrainFilePath.LastIndexOf('\\') + 1);

            string[] files = System.IO.Directory.GetFiles(TrainFilePath, "*.wav");
            int TrainFileCount = files.Length;

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TrainFilePath, "");
                filename = filename.Replace("wav", "mfc");
                fTraining.WriteLine("MFC" + filename);
            }
            fTraining.Close();
            MessageBox.Show("Create train.scp successfully!", "Info");
        }
        private void btnTestSCP_Click(object sender, EventArgs e)
        {
            StreamWriter fTest = new StreamWriter("test.scp");
            string TestFilePath = tbTestFilePath.Text;
            string lastFolder = TestFilePath.Substring(TestFilePath.LastIndexOf('\\') + 1);

            string[] files = System.IO.Directory.GetFiles(TestFilePath, "*.wav");
            int TrainFileCount = files.Length;

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TestFilePath, "");
                filename = filename.Replace("wav", "mfc");
                fTest.WriteLine("MFCTest" + filename);
            }
            fTest.Close();
            MessageBox.Show("Create test.scp successfully!", "Info");
        }

        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            string TrainFilePath = tbTrainFilePath.Text;
            TrainFilePath = TrainFilePath.Substring(0, TrainFilePath.LastIndexOf('\\'));            
            
            Directory.CreateDirectory(TrainFilePath + "\\mfc");
            for (int i = 0; i <= 15; ++i)
            {
                Directory.CreateDirectory(TrainFilePath + "\\hmm" + i);
            }
            MessageBox.Show("Create folders successfully!", "Info");
        }

        private void btnDeleteFolder_Click(object sender, EventArgs e)
        {
            string TrainFilePath = tbTrainFilePath.Text;
            TrainFilePath = TrainFilePath.Substring(0, TrainFilePath.LastIndexOf('\\'));
            try
            {
                Directory.Delete(TrainFilePath + "\\mfc", true);
                for (int i = 0; i <= 15; ++i)
                {
                    Directory.Delete(TrainFilePath + "\\hmm" + i);
                }
                MessageBox.Show("Delete folders successfully!", "Info");
            }
            catch (Exception ex)
            {
            }
        }
        private void btnCreateMacros_Click(object sender, EventArgs e)
        {
            string[] proto = System.IO.File.ReadAllLines("hmm0\\proto");
            string[] vFloors = System.IO.File.ReadAllLines("hmm0\\vFloors");
            StreamWriter fMacros = new StreamWriter("hmm0\\macros");
            for (int i = 0; i < 3; ++i)
            {
                fMacros.WriteLine(proto[i]);
            }
            for (int i = 0; i < vFloors.Length; ++i)
            {
                fMacros.WriteLine(vFloors[i]);
            }
            fMacros.Close();
            MessageBox.Show("Create macros successfully!", "Info");
        }
        private void btnCreateHmmdefs_Click(object sender, EventArgs e)
        {
            StreamWriter fHmmdefs = new StreamWriter("hmm0\\hmmdefs");
            string[] proto = System.IO.File.ReadAllLines("hmm0\\proto");
            string[] phones = System.IO.File.ReadAllLines("monophones0");
            for (int i = 0; i < phones.Length; ++i)
            {
                fHmmdefs.WriteLine("~h \"" + phones[i] + "\"");
                for (int j = 4; j < proto.Length; ++j)
                {
                    fHmmdefs.WriteLine(proto[j]);
                }
            }
            fHmmdefs.Close();
            MessageBox.Show("Create hmmdefs successfully!", "Info");
        }
    }
}