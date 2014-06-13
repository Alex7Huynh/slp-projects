using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NAudio.Wave;

namespace MTT
{
    public partial class frmMain : Form
    {
        FileManager fileManager;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string currentpath = Directory.GetCurrentDirectory();
            tbTrainFilePath.Text = currentpath + "\\Train";
            tbTestFilePath.Text = currentpath + "\\Test";

            fileManager = new FileManager(tbTrainFilePath.Text, tbTestFilePath.Text);
        }

        private void btnBrowseTrainPath_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (string.IsNullOrEmpty(fbd.SelectedPath))
                return;
            tbTrainFilePath.Text = fbd.SelectedPath;
            fileManager = new FileManager(tbTrainFilePath.Text, tbTestFilePath.Text);
        }

        private void btnBrowseTestPath_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (string.IsNullOrEmpty(fbd.SelectedPath))
                return;
            tbTestFilePath.Text = fbd.SelectedPath;
            fileManager = new FileManager(tbTrainFilePath.Text, tbTestFilePath.Text);
        }

        private void btnTTS_Click(object sender, EventArgs e)
        {
            // Remove new-line character
            string sentence = tbSentence.Text.Replace("\r\n", " ");
            // Normalize every words
            Standardizer standardizer = new Standardizer(sentence);            
            sentence = standardizer.Standardize();
            // Convert every words to telex
            string result = "";
            string[] OriginWords = sentence.Split(' ');
            for (int i = 0; i < OriginWords.Length; ++i)
            {
                result += WordConversion.ConvertUnicodeToTelex(OriginWords[i]) + " ";
            }
            result = result.Trim();
            // Compare to lstWord from recout.mlf & get byte array
            string[] words = result.Split(' ');
            byte[] buffer = new byte[0];
            string sample = ""; // sample filename to set format for out.wav

            string folderTrain = tbTrainFilePath.Text;
            folderTrain = folderTrain.Substring(folderTrain.LastIndexOf('\\')+1);
            List<MyWord> lstWord = MyWord.ReadFile("recout.mlf", folderTrain);

            for (int i = 0; i < words.Length; i++)
            {                
                var leftStr = (i - 1 >= 0 ? words[i - 1] : null);
                var rightStr = (i + 1 < words.Count() ? words[i + 1] : null);

                var word = lstWord.FirstOrDefault(p => p.Str == words[i] && p.LeftStr == leftStr && p.RightStr == rightStr);
                if (word == null)
                    word = lstWord.FirstOrDefault(p => p.Str == words[i] && p.LeftStr == leftStr);
                if (word == null)
                    word = lstWord.FirstOrDefault(p => p.Str == words[i] && p.RightStr == rightStr);
                if (word == null)
                    word = lstWord.FirstOrDefault(p => p.Str == words[i]);
                
                if (word != null)
                {
                    sample = word.Filename;

                    var ByteArray = SoundManager.TrimWavByteArray(
                        word.Filename,
                        TimeSpan.FromMilliseconds(word.SecondStart),
                        TimeSpan.FromMilliseconds(word.SecondEnd));

                    var length = buffer.Length + ByteArray.Length;
                    var merge = new byte[length];
                    System.Buffer.BlockCopy(buffer, 0, merge, 0, buffer.Length);
                    System.Buffer.BlockCopy(ByteArray, 0, merge, buffer.Length, ByteArray.Length);
                    buffer = merge;
                }
                else
                {
                    if (!ckbIgnoreWords.Checked)
                    {
                        MessageBox.Show("Some words are not recorded yet!", "Info");
                        return;
                    }
                }
            }
            // If sample file to set format for output not found
            if (String.IsNullOrEmpty(sample))
            {
                MessageBox.Show("Some words cannot be parsed yet!", "Info");
                return;
            }
            // Write output wav file
            using (WaveFileReader reader = new WaveFileReader(sample))
            {
                using (WaveFileWriter writer = new WaveFileWriter("out.wav", reader.WaveFormat))
                {
                    writer.Write(buffer, 0, buffer.Length);
                }
            }
            //MessageBox.Show(sentence, "Sentence -> stardandized");
            SoundManager.PlayWavFile("out.wav");
            
        }
        #region Prepare data
        private void btnPrepareAll_Click(object sender, EventArgs e)
        {
            fileManager.CreateFolders();
            fileManager.MakeDict(false);
            fileManager.MakeMonophones();
            fileManager.MakeFulllist();
            fileManager.MakePrompts();
            fileManager.MakeWords();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE0, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE1, false);
            fileManager.MakeMfccTrainScp();
            fileManager.MakeTrainScp();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_MFC, false);
            MessageBox.Show("Create all training files successfully!", "Info");
            this.Focus();
        }

        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            fileManager.CreateFolders();
            MessageBox.Show("Create folders successfully!", "Info");
        }

        private void btnCreateDICT_Click(object sender, EventArgs e)
        {
            fileManager.MakeDict(false);
            MessageBox.Show("Create " + fileManager.DictFilename + " successfully!", "Info");
        }

        private void btnDi2Mn_Click(object sender, EventArgs e)
        {
            fileManager.MakeMonophones();
            MessageBox.Show("Create " + fileManager.MonophonesFilename + " successfully!", "Info");
        }

        private void btnCreateFulllist_Click(object sender, EventArgs e)
        {
            fileManager.MakeFulllist();
            MessageBox.Show("Create " + fileManager.FulllistFilename + " successfully!", "Info");
        }

        private void btnCreatePROMPTS_Click(object sender, EventArgs e)
        {
            fileManager.MakePrompts();
            MessageBox.Show("Create " + fileManager.PromptsFilename + " successfully!", "Info");
        }

        private void btnCreateWORDS_Click(object sender, EventArgs e)
        {
            fileManager.MakeWords();
            MessageBox.Show("Create " + fileManager.WordsFilename + " successfully!", "Info");
        }

        private void btnMakePhones_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE0, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE1, false);
            MessageBox.Show("Create phones0 & phones1 successfully!", "Info");
            this.Focus();
        }

        private void btnCreateMfccTrain_Click(object sender, EventArgs e)
        {
            fileManager.MakeMfccTrainScp();
            MessageBox.Show("Create " + fileManager.MfccTrainScpFilename + " successfully!", "Info");
        }

        private void btnCreateTrainingSCP_Click(object sender, EventArgs e)
        {
            fileManager.MakeTrainScp();
            MessageBox.Show("Create " + fileManager.TrainScpFilename + " successfully!", "Info");
        }

        private void btnCreateMfccTrainFiles_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_MFC, false);
            if (string.IsNullOrEmpty(CommandHelper.GetOutput()))
            {
                MessageBox.Show("Create MFCC train files successfully!", "Info");
            }
        }


        #endregion

        #region Build & train HMM models
        private void btnTrainAll15Hmm_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP7_HMM0_CMD, false);
            fileManager.MakeMacrosHMM0();
            fileManager.MakeHmmdefs();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM1_TRAIN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM2_TRAIN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM3_TRAIN, false);
            fileManager.CreateHMM4();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP11_CONNECTSILSP, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM6, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM7, false);
            File.Copy(Application.StartupPath + "\\phones1.mlf ", Path.Combine(Application.StartupPath, "aligned.mlf"));
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP13_TRAINTOHMM8, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP13_TRAINTOHMM9, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP14_CREATEWINTRI, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP14_TRAINTOHMM10, false);
            fileManager.ModifyWintriMlf();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP15_TRAINTOHMM11, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP15_TRAINTOHMM12, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP16_TRAINTOHMM13, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM14, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM15, false);
            this.Focus();
        }
        private void btnTrainAll17Hmm_Click(object sender, EventArgs e)
        {
            // Prepare training data
            fileManager.CreateFolders();
            fileManager.MakeDict(true);
            fileManager.MakeMonophones();
            fileManager.MakeFulllist();
            fileManager.MakePrompts();
            fileManager.MakeWords();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE0, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE1, false);
            fileManager.MakeMfccTrainScp();
            fileManager.MakeTrainScp();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_MFC, false);

            // Build and train all HMM0-17
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP7_HMM0_CMD, false);
            fileManager.MakeMacrosHMM0();
            fileManager.MakeHmmdefs();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM1_TRAIN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM2_TRAIN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM3_TRAIN, false);
            fileManager.CreateHMM4();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP11_CONNECTSILSP, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM6, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM7, false);
            for (int i = 7; i < 17; ++i)
            {
                CommandHelper.ExecuteCommand(string.Format(ConstantValues.CMD_LAB4_TRAIN_TO_HMM17, i, i + 1), true);
            }
            CommandHelper.ExecuteCommand(ConstantValues.CMD_LAB4_HVITE, false);
        }

        private void btnInitModelHmm0_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP7_HMM0_CMD, false);
            if (string.IsNullOrEmpty(CommandHelper.GetOutput()))
            {
                MessageBox.Show("Create HMM0 successfully!", "Info");
            }
            this.Focus();
        }

        private void btnCreateMacrosHmmdefs_Click(object sender, EventArgs e)
        {
            fileManager.MakeMacrosHMM0();
            fileManager.MakeHmmdefs();
            MessageBox.Show("Create macros & hmmdefs successfully!", "Info");
        }

        private void btnTrainHmm1_3_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM1_TRAIN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM2_TRAIN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM3_TRAIN, false);
            MessageBox.Show("Create HMM1, HMM2, HMM3 successfully!", "Info");
            this.Focus();
        }

        private void btnCreateHMM4_Click(object sender, EventArgs e)
        {
            fileManager.CreateHMM4();
            MessageBox.Show("Create HMM4 successfully!", "Info");
        }

        private void btnCreateHMM5_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP11_CONNECTSILSP, false);
            MessageBox.Show("Create HMM5 successfully!", "Info");
            this.Focus();
        }

        private void btnCreateHMM7_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM6, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM7, false);
            MessageBox.Show("Create HMM6, HMM7 successfully!", "Info");
            this.Focus();
        }

        private void btnCreateHMM9_Click(object sender, EventArgs e)
        {
            File.Copy(Application.StartupPath + "\\phones1.mlf ", Path.Combine(Application.StartupPath, "aligned.mlf"));
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP13_TRAINTOHMM8, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP13_TRAINTOHMM9, false);
            MessageBox.Show("Create HMM8, HMM9 successfully!", "Info");
            this.Focus();
        }

        private void btnCreateHMM10_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP14_CREATEWINTRI, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP14_TRAINTOHMM10, false);
            fileManager.ModifyWintriMlf();
            MessageBox.Show("Create HMM10 successfully!", "Info");
            this.Focus();
        }

        private void btnCreateHMM12_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP15_TRAINTOHMM11, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP15_TRAINTOHMM12, false);
            MessageBox.Show("Create HMM11, HMM12 successfully!", "Info");
            this.Focus();
        }

        private void btnCreateHMM13_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP16_TRAINTOHMM13, false);
            MessageBox.Show("Create HMM13 successfully!", "Info");
            this.Focus();
        }

        private void btnCreateHMM15_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM14, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM15, false);
            MessageBox.Show("Create HMM14, HMM15 successfully!", "Info");
            this.Focus();
        }
        #endregion

        #region Test
        private void btnCreateMfccTestFiles_Click(object sender, EventArgs e)
        {
            fileManager.MakeTestScp();
            fileManager.MakeTestMlf();
            fileManager.MakeMfccTestScp();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_CREATEMFCC, false);
            //CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_PARSEGRAM, false); // gram --> wdnet
            MessageBox.Show("Create " + fileManager.MfccTestScpFilename + " successfully", "Info");
            this.Focus();
        }

        private void btnCreateLmtrain_Click(object sender, EventArgs e)
        {
            fileManager.MakeLmTrain();
            MessageBox.Show("Create " + fileManager.LmTrainFilename + " successfully!", "Info");
        }

        private void btnCreateNewLmtrain_Click(object sender, EventArgs e)
        {
            fileManager.MakeLmTrain("config/NewSentences.txt");
            MessageBox.Show("Create " + fileManager.LmTrainFilename + " successfully!", "Info");
        }

        // Check numeral input
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

        private void btnInitBuildModel_Click(object sender, EventArgs e)
        {
            string numberOfGram = txtNGram.Text;
            if (string.IsNullOrEmpty(numberOfGram))
            {
                MessageBox.Show("Please input value!");
                return;
            }
            CommandHelper.ExecuteCommand(string.Format(ConstantValues.CMD_LAB3_STEP2_BUILD_NGRAM_NEW, numberOfGram), false);
            CommandHelper.ExecuteCommand(string.Format(ConstantValues.CMD_LAB3_STEP2_BUILD_NGRAM_INIT, numberOfGram), false);
            CommandHelper.ExecuteCommand(string.Format(ConstantValues.CMD_LAB3_STEP2_BUILD_NGRAM_BUILD, numberOfGram), false);
            this.Focus();
        }

        private void btnTestModel_Click(object sender, EventArgs e)
        {
            //CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_RUN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_LAB3_STEP3_RECOGNITION_HDECODE, false);
            MessageBox.Show("Create recout.mlf successfully!", "Info");
            this.Focus();
        }

        private void btnGetResult_Click(object sender, EventArgs e)
        {
            if (!File.Exists("recout.mlf"))
            {
                MessageBox.Show("recout.mlf not found!", "Warning");
                return;
            }
            if (!File.Exists("test.mlf"))
            {
                MessageBox.Show("test.mlf not found!", "Warning");
                return;
            }
            if (!File.Exists("tiedlist"))
            {
                MessageBox.Show("tiedlist not found!", "Warning");
                return;
            }
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_HRESULTS, true);
            MessageBox.Show(CommandHelper.GetOutput(), "Result");
            this.Focus();
        }

        private void btnShowPerplexity_Click(object sender, EventArgs e)
        {
            fileManager.MakeLmTest();
            string numberOfGram = txtNGram.Text;
            if (string.IsNullOrEmpty(numberOfGram))
            {
                MessageBox.Show("Please input value");
                return;
            }
            CommandHelper.ExecuteCommand(string.Format(ConstantValues.CMD_LAB3_PERFEXCITY, numberOfGram), true);
            MessageBox.Show(CommandHelper.GetOutput(), "Result");
            this.Focus();
        }
        #endregion




    }
}