using System;
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
            // MINH add code here for add new line
            lstWord.Sort();
            StreamWriter fDICT = new StreamWriter("DICT");
            for (int i = 0; i < lstWord.Count - 1; ++i)
            {
                fDICT.WriteLine(lstWord[i]);
            }
            fDICT.Write(lstWord[lstWord.Count-1]);
            fDICT.WriteLine();
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
        
        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            string TrainFilePath = tbTrainFilePath.Text;
            TrainFilePath = TrainFilePath.Substring(0, TrainFilePath.LastIndexOf('\\'));            
            
            Directory.CreateDirectory(TrainFilePath + "\\mfc");
            Directory.CreateDirectory(TrainFilePath + "\\MFCTest");
            
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
                Directory.Delete(TrainFilePath + "\\MFCTest", true);
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

        private void button1_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_MFC);
            if (string.IsNullOrEmpty(CommandHelper.GetOutput()))
            {
                MessageBox.Show("Create MFCC successfully!", "Info");
            }
        }

        private void btnInitModelHmm0_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP7_HMM0_CMD);
            if (string.IsNullOrEmpty(CommandHelper.GetOutput()))
            {
                MessageBox.Show("Run HMM0 successfully!", "Info");
            }
        }

        private void btnTrainHmm1_3_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM1_TRAIN);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM2_TRAIN);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM3_TRAIN);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE0);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE1);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //var hmm3Path = Application.StartupPath + "\\hmm3";
            //var hmm4Path = Application.StartupPath + "\\hmm4";

            //foreach (var file in Directory.GetFiles(hmm3Path))
            //    File.Copy(file, Path.Combine(hmm4Path, Path.GetFileName(file)));


            //StreamReader sReader = new StreamReader(hmm4Path + "\\hmmdefs");
            //var hmmDef = sReader.ReadToEnd();
            //sReader.Close();
            //var copyText = hmmDef.Substring(hmmDef.LastIndexOf("~h"));
            //copyText = copyText.Replace("sil", "sp");
            //StreamWriter fHmmdefs = new StreamWriter(hmm4Path + "\\hmmdefs");
            //hmmDef = hmmDef + copyText;
            //fHmmdefs.WriteLine(hmmDef);
            //fHmmdefs.Close();

            string str;
            int num3;
            File.Copy("HMM3/hmmdefs", "HMM4/hmmdefs", true);
            File.Copy("HMM3/macros", "HMM4/macros", true);
            using (StreamReader reader = new StreamReader("HMM4/hmmdefs"))
            {
                str = reader.ReadToEnd();
            }
            int index = str.IndexOf("sil");
            string str2 = str.Substring(index - 4);
            index = str2.IndexOf("<STATE> 3");
            int num2 = str2.IndexOf("<STATE>", (int)(index + 0x16));
            string str3 = str2.Substring(index + 0x15, num2 - (index + 0x15));
            StreamWriter writer = new StreamWriter("HMM4/hmmdefs");
            writer.Write(str);
            writer.WriteLine("~h \"sp\"");
            writer.WriteLine("<BEGINHMM>");
            writer.WriteLine("<NUMSTATES> 3");
            writer.WriteLine("<STATE> 2");
            writer.WriteLine("<MEAN> 39");
            writer.Write(str3);
            writer.WriteLine("<TRANSP> 3");
            index = str2.IndexOf("<TRANSP> 5");
            num2 = str2.IndexOf("<ENDHMM>");
            string[] strArray = str2.Substring(index + 11, num2 - (index + 11)).Split(new char[] { '\n' });
            string[] strArray2 = strArray[0].Split(new char[] { ' ' });
            string str4 = " ";
            for (num3 = 1; num3 <= 3; num3++)
            {
                str4 = str4 + strArray2[num3] + " ";
            }
            str4 = str4.Substring(0, str4.Length - 1);
            writer.WriteLine(str4);
            strArray2 = strArray[2].Split(new char[] { ' ' });
            str4 = " " + strArray2[1] + " " + strArray2[3] + " " + strArray2[4];
            writer.WriteLine(str4);
            strArray2 = strArray[4].Split(new char[] { ' ' });
            str4 = " ";
            for (num3 = 1; num3 <= 3; num3++)
            {
                str4 = str4 + strArray2[num3] + " ";
            }
            writer.WriteLine(str4);
            writer.WriteLine("<ENDHMM>");
            writer.Close();

            MessageBox.Show("Run successfully!", "Info");
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP11_CONNECTSILSP);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM6);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM7);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var src = Application.StartupPath + "\\phones1.mlf ";
            var dest = Application.StartupPath;
            File.Copy(src, Path.Combine(dest, "aligned.mlf"));
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP13_TRAINTOHMM8);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP13_TRAINTOHMM9);
            MessageBox.Show("Run successfully!", "Info");

        }

        private void button9_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP14_CREATEWINTRI);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP14_TRAINTOHMM10);
            UpdateFileWintri();
            MessageBox.Show("Run successfully!", "Info");
        }

        private void UpdateFileWintri()
        {
            string str;
            using (StreamReader reader = new StreamReader("wintri.mlf"))
            {
                str = reader.ReadToEnd();
            }

            string[] strArray = str.Split(new char[] { '\n' });
            StreamWriter writer = new StreamWriter("wintri.mlf");
            foreach (string strTmp in strArray)
            {
                string tmp = strTmp.Trim();
                if (strTmp.Contains(".lab\""))
                {
                     tmp = "\"*/" + tmp.Substring(1);
                }
                writer.WriteLine(tmp);
            }

            writer.Close();
        }

        private void btnTrainHMM11_12_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP15_TRAINTOHMM11);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP15_TRAINTOHMM12);
            MessageBox.Show("Run successfully!", "Info");
        }        

        private void createFullList()
        {
            string[] monophones = System.IO.File.ReadAllLines("monophones0");
            StreamWriter fFulllist = new StreamWriter("fulllist");
            int n = monophones.Length;

            // Monophone
            for (int i = 0; i < n; ++i)
                fFulllist.WriteLine(monophones[i]);
            fFulllist.WriteLine("sp");
            // Biphone
            for (int i = 0; i < n - 1; ++i)
                for (int j = 0; j < n; ++j)
                    fFulllist.WriteLine("{0}-{1}", monophones[i], monophones[j]);
            for (int j = 0; j < n - 1; ++j)
                fFulllist.WriteLine("{0}-{1}", monophones[n - 1], monophones[j]);
            for (int i = 0; i < n - 1; ++i)
                for (int j = 0; j < n; ++j)
                    fFulllist.WriteLine("{0}+{1}", monophones[i], monophones[j]);
            for (int j = 0; j < n - 1; ++j)
                fFulllist.WriteLine("{0}+{1}", monophones[n - 1], monophones[j]);
            // Triphone
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n - 1; ++j)
                    for (int k = 0; k < n; ++k)
                        fFulllist.WriteLine("{0}-{1}+{2}", monophones[i], monophones[j], monophones[k]);

            fFulllist.Close();
        }

        private void btnTrainHMM13_Click(object sender, EventArgs e)
        {
            createFullList();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP16_TRAINTOHMM13);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void btnTrainHMM14_15_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM14);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM15);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // create mfcc - test
            StreamWriter fMFC = new StreamWriter("mfcc-test.scp");
            string testFilePath = tbTestFilePath.Text;
            string lastFolder = testFilePath.Substring(testFilePath.LastIndexOf('\\') + 1);

            string[] files = System.IO.Directory.GetFiles(testFilePath, "*.wav");

            foreach (var file in files)
            {
                string filename = file.Replace(testFilePath, "");
                string filename2 = filename.Replace("wav", "mfc");
                fMFC.WriteLine(lastFolder + filename + "  MFCTest" + filename2);
            }
            fMFC.Close();

            // create gram txt
            //StreamReader reader = new StreamReader("gram-source.txt");
            //string txtGram = reader.ReadToEnd();
            //var words = txtGram.Split(' ').ToList();
            //string result = "$(tu) = {0}";
            //string txt = string.Empty;
            //int iCount = 0;
            //foreach (var word in words)
            //{
            //    txt += Word.ConvertUnicodeToTelex(word);
            //    if (iCount < words.Count)
            //    {
            //        txt += " | ";
            //    }
            //}
            //StreamWriter gramWriter = new StreamWriter("gram.txt");
            //result = string.Format(result, txt);
            //gramWriter.WriteLine(result);
            //gramWriter.WriteLine("$(tu)");
            //gramWriter.WriteLine("(SENT-START $(tu) SENT-END)");
            //gramWriter.Close();
            // change the dict
            //StreamReader dictReader = new StreamReader("DICT");
            //string dictText = dictReader.ReadToEnd();
            //dictReader.Close();
            //dictText = dictText.Remove(dictText.LastIndexOf("\r\n"));
            //using (StreamWriter dictWriter = new StreamWriter("DICT"))
            //{
            //    dictWriter.WriteLine(dictText);
            //    dictWriter.WriteLine("SENT-START  []	sil");
            //    dictWriter.WriteLine("SENT-END	[]	sil");
            //}

            // parse gram
            //var src = Application.StartupPath + "\\words.mlf";
            //var dest = Application.StartupPath;
            //File.Copy(src, Path.Combine(dest, "recout.mlf"));
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_CREATEMFCC);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_PARSEGRAM);
            MessageBox.Show("Create mfcc-test.scp and wdnet successfully", "Info");
        }        
        
        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter test = new StreamWriter("test.mlf");
            test.WriteLine("#!MLF!#");

            string[] chuso = { "KHOONG", "MOOJT", "HAI", "BA", "BOOSN", "NAWM", "SASU", "BARY", "TASM", "CHISN" };            
            string TestFilePath = tbTestFilePath.Text;
            string[] files = System.IO.Directory.GetFiles(TestFilePath, "*.wav");

            for (int i = 0; i < files.Length; ++i)
            {
                string filename = files[i].Replace(TestFilePath, "");
                filename = filename.Replace(".wav", ".lab");
                //test.WriteLine("\"MFCTest" + filename + "\"");
                test.WriteLine("\"*" + filename.Replace('\\','/') + "\"");                

                string so = files[i].Substring(files[i].IndexOf(".wav") - 1, 1);
                so = chuso[int.Parse(so)];
                test.WriteLine(so);
                test.WriteLine(".");
            }
            test.Close();
            MessageBox.Show("Create test.mlf successfully!", "Info");
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

        private void btnCreateRecout_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_RUN);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void btnGetResult_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP2_RESULT);
        }
    }
}