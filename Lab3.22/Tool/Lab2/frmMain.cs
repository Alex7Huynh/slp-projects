using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab2
{
    public partial class frmMain : Form
    {
        public frmMain()
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
            string[] lines = File.ReadAllLines(fileName);
            var lstWord = new List<string>();
            foreach (string line in lines)
            {
                string[] words = Regex.Split(line, " ");

                for (int i = 1; i < words.Length - 1; ++i)
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
            var fMonophone0 = new StreamWriter("monophones0");
            var fMonophone1 = new StreamWriter("monophones1");

            foreach (string word in lstWord)
            {
                fMonophone0.WriteLine(word);
                fMonophone1.WriteLine(word);
            }
            fMonophone0.WriteLine("sil");
            fMonophone1.WriteLine("sil");
            fMonophone1.WriteLine("sp");

            fMonophone0.Close();
            fMonophone1.Close();
        }

        private void btnCreateDICT_Click(object sender, EventArgs e)
        {
            string TrainFilePath = tbTrainFilePath.Text;
            //int TrainFileCount = int.Parse(tbTrainFileCount.Text);
            //string path = "E:\\sn0040\\train";
            string[] files = Directory.GetFiles(TrainFilePath, "*.txt");
            int TrainFileCount = files.Length;

            var lstWord = new List<string>();

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string[] lines = File.ReadAllLines(files[i]);
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
            var fDICT = new StreamWriter("DICT");
            for (int i = 0; i < lstWord.Count; ++i)
            {
                fDICT.WriteLine(lstWord[i]);
            }
            fDICT.WriteLine("SENT-START	[]	sil");
            fDICT.WriteLine("SENT-END	[]	sil");

            fDICT.Close();

            MessageBox.Show("Create DICT for " + TrainFileCount + " files successfully!", "Info");
        }

        private void btnCreatePROMPTS_Click(object sender, EventArgs e)
        {
            string TrainFilePath = tbTrainFilePath.Text;
            string lastFolder = TrainFilePath.Substring(TrainFilePath.LastIndexOf('\\') + 1);
            string[] files = Directory.GetFiles(TrainFilePath, "*.txt");
            int TrainFileCount = files.Length;
            var fPROMPTS = new StreamWriter("PROMPTS");
            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TrainFilePath, "");
                filename = filename.Remove(0, 1);

                string[] lines = File.ReadAllLines(files[i]);
                string newSentence = "";
                string sentence = lines[0];
                string[] words = sentence.Split(' ');
                for (int j = 0; j < words.Length - 1; ++j)
                {
                    string word = Word.ConvertUnicodeToTelex(words[j]);
                    newSentence += word + " ";
                }
                newSentence += Word.ConvertUnicodeToTelex(words[words.Length - 1]);
                fPROMPTS.WriteLine(lastFolder + "\\" + filename + "  " + newSentence);
            }
            fPROMPTS.Close();
            MessageBox.Show("Create PROMPTS successfully!", "Info");
        }

        private void btnCreateWORDS_Click(object sender, EventArgs e)
        {
            var fWORDS = new StreamWriter("WORDS.MLF");
            fWORDS.WriteLine("#!MLF!#");

            string TrainFilePath = tbTrainFilePath.Text;
            string[] files = Directory.GetFiles(TrainFilePath, "*.txt");
            int TrainFileCount = files.Length;

            for (int i = 0; i < TrainFileCount; ++i)
            {
                string filename = files[i].Replace(TrainFilePath, "");
                filename = filename.Remove(0, 1);
                filename = filename.Replace("txt", "lab");

                fWORDS.WriteLine("\"*/" + filename + "\"");

                string[] lines = File.ReadAllLines(files[i]);
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
            var fMFC = new StreamWriter("mfcc.scp");
            string TrainFilePath = tbTrainFilePath.Text;
            string lastFolder = TrainFilePath.Substring(TrainFilePath.LastIndexOf('\\') + 1);

            string[] files = Directory.GetFiles(TrainFilePath, "*.wav");
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
            var fTraining = new StreamWriter("train.scp");
            string TrainFilePath = tbTrainFilePath.Text;
            string lastFolder = TrainFilePath.Substring(TrainFilePath.LastIndexOf('\\') + 1);

            string[] files = Directory.GetFiles(TrainFilePath, "*.wav");
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
            Directory.CreateDirectory(TrainFilePath + "\\LM");

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
                Directory.Delete(TrainFilePath + "\\LM", true);
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
            string[] proto = File.ReadAllLines("hmm0\\proto");
            string[] vFloors = File.ReadAllLines("hmm0\\vFloors");
            var fMacros = new StreamWriter("hmm0\\macros");
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
            var fHmmdefs = new StreamWriter("hmm0\\hmmdefs");
            string[] proto = File.ReadAllLines("hmm0\\proto");
            string[] phones = File.ReadAllLines("monophones0");
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
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_MFC, false);
            if (string.IsNullOrEmpty(CommandHelper.GetOutput()))
            {
                MessageBox.Show("Create MFCC successfully!", "Info");
            }
        }

        private void btnInitModelHmm0_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP7_HMM0_CMD, false);
            if (string.IsNullOrEmpty(CommandHelper.GetOutput()))
            {
                MessageBox.Show("Run HMM0 successfully!", "Info");
            }
        }

        private void btnTrainHmm1_3_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM1_TRAIN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM2_TRAIN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM3_TRAIN, false);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE0, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE1, false);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str;
            int num3;
            File.Copy("HMM3/hmmdefs", "HMM4/hmmdefs", true);
            File.Copy("HMM3/macros", "HMM4/macros", true);
            using (var reader = new StreamReader("HMM4/hmmdefs"))
            {
                str = reader.ReadToEnd();
            }
            int index = str.IndexOf("sil");
            string str2 = str.Substring(index - 4);
            index = str2.IndexOf("<STATE> 3");
            int num2 = str2.IndexOf("<STATE>", index + 0x16);
            string str3 = str2.Substring(index + 0x15, num2 - (index + 0x15));
            var writer = new StreamWriter("HMM4/hmmdefs");
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
            string[] strArray = str2.Substring(index + 11, num2 - (index + 11)).Split(new[] { '\n' });
            string[] strArray2 = strArray[0].Split(new[] { ' ' });
            string str4 = " ";
            for (num3 = 1; num3 <= 3; num3++)
            {
                str4 = str4 + strArray2[num3] + " ";
            }
            str4 = str4.Substring(0, str4.Length - 1);
            writer.WriteLine(str4);
            strArray2 = strArray[2].Split(new[] { ' ' });
            str4 = " " + strArray2[1] + " " + strArray2[3] + " " + strArray2[4];
            writer.WriteLine(str4);
            strArray2 = strArray[4].Split(new[] { ' ' });
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
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP11_CONNECTSILSP, false);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM6, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM7, false);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string src = Application.StartupPath + "\\phones1.mlf ";
            string dest = Application.StartupPath;
            File.Copy(src, Path.Combine(dest, "aligned.mlf"));
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP13_TRAINTOHMM8, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP13_TRAINTOHMM9, false);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP14_CREATEWINTRI, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP14_TRAINTOHMM10, false);
            UpdateFileWintri();
            MessageBox.Show("Run successfully!", "Info");
        }

        private void UpdateFileWintri()
        {
            string str;
            using (var reader = new StreamReader("wintri.mlf"))
            {
                str = reader.ReadToEnd();
            }

            string[] strArray = str.Split(new[] { '\n' });
            var writer = new StreamWriter("wintri.mlf");
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
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP15_TRAINTOHMM11, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP15_TRAINTOHMM12, false);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void btnCreateFulllist_Click(object sender, EventArgs e)
        {
            string[] monophones = File.ReadAllLines("monophones1");

            var fFulllist = new StreamWriter("fulllist");
            int n = monophones.Length;

            string tmp = monophones[n - 2];
            monophones[n - 2] = monophones[n - 1];
            monophones[n - 1] = tmp;

            // Monophone
            fFulllist.WriteLine("sil");
            fFulllist.WriteLine("sp");

            // Triphone
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n - 1; ++j)
                    for (int k = 0; k < n; ++k)
                        fFulllist.WriteLine("{0}-{1}+{2}", monophones[i], monophones[j], monophones[k]);

            fFulllist.Close();
            MessageBox.Show("Create hmmdefs successfully!", "Info");
        }

        private void btnTrainHMM13_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP16_TRAINTOHMM13, false);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void btnTrainHMM14_15_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM14, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM15, false);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // create mfcc - test
            var fMFC = new StreamWriter("mfcc-test.scp");
            string testFilePath = tbTestFilePath.Text;
            string lastFolder = testFilePath.Substring(testFilePath.LastIndexOf('\\') + 1);

            string[] files = Directory.GetFiles(testFilePath, "*.wav");

            foreach (string file in files)
            {
                string filename = file.Replace(testFilePath, "");
                string filename2 = filename.Replace("wav", "mfc");
                fMFC.WriteLine(lastFolder + filename + "  MFCTest" + filename2);
            }
            fMFC.Close();

            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_CREATEMFCC, false);
            //CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_PARSEGRAM, false);
            MessageBox.Show("Create mfcc-test.scp successfully", "Info");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var test = new StreamWriter("test.mlf");
            test.WriteLine("#!MLF!#");


            string TestFilePath = tbTestFilePath.Text;
            string[] filesTest = Directory.GetFiles(TestFilePath, "*.txt");

            for (int i = 0; i < filesTest.Length; ++i)
            {
                string filename = filesTest[i].Replace(TestFilePath, "");
                filename = filename.Replace(".txt", ".lab");
                test.WriteLine("\"*" + filename.Replace('\\', '/') + "\"");

                string[] lines = File.ReadAllLines(filesTest[i]);
                string[] words = lines[0].Split(' ');
                for (int j = 0; j < words.Length; ++j)
                    test.WriteLine(Word.ConvertUnicodeToTelex(words[j]));


                test.WriteLine(".");
            }
            test.Close();
            MessageBox.Show("Create test.mlf successfully!", "Info");
        }

        private void btnTestSCP_Click(object sender, EventArgs e)
        {
            var fTest = new StreamWriter("test.scp");
            string TestFilePath = tbTestFilePath.Text;
            string lastFolder = TestFilePath.Substring(TestFilePath.LastIndexOf('\\') + 1);

            string[] files = Directory.GetFiles(TestFilePath, "*.wav");
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

        private void btnCreateLmtrain_Click(object sender, EventArgs e)
        {
            string TrainFilePath = tbTrainFilePath.Text;
            string TestFilePath = tbTestFilePath.Text;

            string[] filesTrain = Directory.GetFiles(TrainFilePath, "*.txt");
            string[] filesTest = Directory.GetFiles(TestFilePath, "*.txt");

            var fPROMPTS = new StreamWriter("lmtrain.txt");
            // 270 lines
            for (int i = 0; i < filesTrain.Length; ++i)
            {
                string[] lines = File.ReadAllLines(filesTrain[i]);
                string newSentence = "";
                string[] words = lines[0].Split(' ');
                for (int j = 0; j < words.Length - 1; ++j)
                    newSentence += Word.ConvertUnicodeToTelex(words[j]) + " ";
                newSentence += Word.ConvertUnicodeToTelex(words[words.Length - 1]);
                fPROMPTS.WriteLine("<s> " + newSentence + " </s>");
            }
            // 30 lines
            for (int i = 0; i < filesTest.Length; ++i)
            {
                string[] lines = File.ReadAllLines(filesTest[i]);
                string newSentence = "";
                string[] words = lines[0].Split(' ');
                for (int j = 0; j < words.Length - 1; ++j)
                    newSentence += Word.ConvertUnicodeToTelex(words[j]) + " ";
                newSentence += Word.ConvertUnicodeToTelex(words[words.Length - 1]);
                fPROMPTS.WriteLine("<s> " + newSentence + " </s>");
            }

            fPROMPTS.Close();
            MessageBox.Show("Create lmtrain.txt successfully!", "Info");
        }

        private void btnCreateNewLmtrain_Click(object sender, EventArgs e)
        {
            string[] filesTrain = Directory.GetFiles(tbTrainFilePath.Text, "*.txt");

            var fLmtrain = new StreamWriter("lmtrain.txt");
            // 270 lines
            for (int i = 0; i < filesTrain.Length; ++i)
            {
                string[] lines = File.ReadAllLines(filesTrain[i]);
                string newSentence = "";
                string[] words = lines[0].Split(' ');
                for (int j = 0; j < words.Length - 1; ++j)
                    newSentence += Word.ConvertUnicodeToTelex(words[j]) + " ";
                newSentence += Word.ConvertUnicodeToTelex(words[words.Length - 1]);
                fLmtrain.WriteLine("<s> " + newSentence + " </s>");
            }
            // n lines
            string NewSentencesFile = "config/NewSentences.txt";
            if (File.Exists(NewSentencesFile))
            {
                string[] newlines = File.ReadAllLines(NewSentencesFile);
                for (int i = 0; i < newlines.Length; ++i)
                {
                    newlines[i] = newlines[i].Trim();
                    if (newlines.Length != 0)
                    {
                        string newSentence = "";
                        string[] words = newlines[i].Split(' ');
                        for (int j = 0; j < words.Length - 1; ++j)
                            newSentence += Word.ConvertUnicodeToTelex(words[j]) + " ";
                        newSentence += Word.ConvertUnicodeToTelex(words[words.Length - 1]);
                        fLmtrain.WriteLine("<s> " + newSentence + " </s>");
                    }
                }
            }

            fLmtrain.Close();
            MessageBox.Show("Create lmtrain.txt successfully!", "Info");
        }

        private void btnCreateRecout_Click(object sender, EventArgs e)
        {
            //CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_RUN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_LAB3_STEP3_RECOGNITION_HDECODE, false);
            MessageBox.Show("Run successfully!", "Info");
        }

        private void btnGetResult_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP2_RESULT, true);
            MessageBox.Show(CommandHelper.GetOutput(), "Result");
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            string currentpath = Directory.GetCurrentDirectory();
            tbTestFilePath.Text = currentpath + "\\Test";
            tbTrainFilePath.Text = currentpath + "\\Train";
        }

        private void txtNGram_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                if (Regex.IsMatch(txtNGram.Text, "\\D+"))
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = e.KeyChar != (char)Keys.Back;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string numberOfGram = txtNGram.Text;
            if (string.IsNullOrEmpty(numberOfGram))
            {
                MessageBox.Show("Please input value");
                return;
            }
            CommandHelper.ExecuteCommand(string.Format(ConstantValues.CMD_LAB3_STEP2_BUILD_NGRAM_NEW, numberOfGram),
                false);
            CommandHelper.ExecuteCommand(string.Format(ConstantValues.CMD_LAB3_STEP2_BUILD_NGRAM_INIT, numberOfGram),
                false);
            CommandHelper.ExecuteCommand(string.Format(ConstantValues.CMD_LAB3_STEP2_BUILD_NGRAM_BUILD, numberOfGram),
                false);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var fLMTestFile = new StreamWriter("lmtest.txt");
            string[] filesTest = Directory.GetFiles(tbTestFilePath.Text, "*.txt");
            foreach (var fileTest in filesTest)
            {
                string[] lines = File.ReadAllLines(fileTest);
                string newSentence = "";
                string[] words = lines[0].Split(' ');
                for (int j = 0; j < words.Length - 1; ++j)
                    newSentence += Word.ConvertUnicodeToTelex(words[j]) + " ";
                newSentence += Word.ConvertUnicodeToTelex(words[words.Length - 1]);
                fLMTestFile.WriteLine("<s> " + newSentence + " </s>");
            }
            fLMTestFile.Close();
            string numberOfGram = txtNGram.Text;
            if (string.IsNullOrEmpty(numberOfGram))
            {
                MessageBox.Show("Please input value");
                return;
            }
            CommandHelper.ExecuteCommand(string.Format(ConstantValues.CMD_LAB3_PERFEXCITY, numberOfGram), true);
            MessageBox.Show(CommandHelper.GetOutput(), "Result");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (string.IsNullOrEmpty(fbd.SelectedPath))
                return;
            tbTrainFilePath.Text = fbd.SelectedPath;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (string.IsNullOrEmpty(fbd.SelectedPath))
                return;
            tbTestFilePath.Text = fbd.SelectedPath;
        }


    }
}