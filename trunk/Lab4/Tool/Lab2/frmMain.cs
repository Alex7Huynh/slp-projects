using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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

        #region Prepare data
        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            fileManager.CreateFolders();
            MessageBox.Show("Create folders successfully!", "Info");
        }

        private void btnCreateDICT_Click(object sender, EventArgs e)
        {
            fileManager.MakeDict();
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
        }

        private void btnCreateMFC_Click(object sender, EventArgs e)
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
        private void btnInitModelHmm0_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP7_HMM0_CMD, false);
            if (string.IsNullOrEmpty(CommandHelper.GetOutput()))
            {
                MessageBox.Show("Create HMM0 successfully!", "Info");
            }
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
        }

        private void btnCreateHMM7_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM6, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP12_TRAINTOHMM7, false);
            MessageBox.Show("Create HMM6, HMM7 successfully!", "Info");
        }

        private void btnCreateHMM9_Click(object sender, EventArgs e)
        {
            string src = Application.StartupPath + "\\phones1.mlf ";
            string dest = Application.StartupPath;
            File.Copy(src, Path.Combine(dest, "aligned.mlf"));
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP13_TRAINTOHMM8, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP13_TRAINTOHMM9, false);
            MessageBox.Show("Create HMM8, HMM9 successfully!", "Info");
        }

        private void btnCreateHMM10_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP14_CREATEWINTRI, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP14_TRAINTOHMM10, false);
            fileManager.ModifyWintriMlf();
            MessageBox.Show("Create HMM10 successfully!", "Info");
        }

        private void btnCreateHMM12_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP15_TRAINTOHMM11, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP15_TRAINTOHMM12, false);
            MessageBox.Show("Create HMM11, HMM12 successfully!", "Info");
        }

        private void btnCreateHMM13_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP16_TRAINTOHMM13, false);
            MessageBox.Show("Create HMM13 successfully!", "Info");
        }

        private void btnCreateHMM15_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM14, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM15, false);
            MessageBox.Show("Create HMM14, HMM15 successfully!", "Info");
        }
        #endregion

        #region Test
        private void btnCreateMfccTestFiles_Click(object sender, EventArgs e)
        {
            fileManager.MakeMfccTestScp();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_CREATEMFCC, false);
            //CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_PARSEGRAM, false); // gram --> wdnet
            MessageBox.Show("Create " + fileManager.MfccTestScpFilename + " successfully", "Info");
        }

        private void btnCreateTestScp_Click(object sender, EventArgs e)
        {
            fileManager.MakeTestScp();
            MessageBox.Show("Create " + fileManager.TestScpFilename + " successfully!", "Info");
        }

        private void btnCreateTestMlf_Click(object sender, EventArgs e)
        {
            fileManager.MakeTestMlf();
            MessageBox.Show("Create " + fileManager.TestMlfFilename + " successfully!", "Info");
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
        }

        private void btnCreateRecoutMlf_Click(object sender, EventArgs e)
        {
            //CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_RUN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_LAB3_STEP3_RECOGNITION_HDECODE, false);
            MessageBox.Show("Create recout.mlf successfully!", "Info");
        }

        private void btnGetResult_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP2_RESULT, true);
            MessageBox.Show(CommandHelper.GetOutput(), "Result");
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
        }
        #endregion

        
    }
}