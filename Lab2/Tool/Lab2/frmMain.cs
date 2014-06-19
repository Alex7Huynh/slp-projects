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
        private void btnCreateFolders_Click(object sender, EventArgs e)
        {
            fileManager.CreateFolders();
            MessageBox.Show("Create folders successfully!", "Info");            
        }

        private void btnCreateDICT_Click(object sender, EventArgs e)
        {
            fileManager.MakeDict();
            MessageBox.Show("Create " + fileManager.DictFilename + " successfully!", "Info");
        }

        private void btnCreateMonophones_Click(object sender, EventArgs e)
        {
            fileManager.MakeMonophones();
            MessageBox.Show("Create " + fileManager.MonophonesFilename + " successfully!", "Info");
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

        private void btnCreatePhones_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE0, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE1, false);
            MessageBox.Show("Create phones0 & phones1 successfully!", "Info");
            this.Focus();
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

        private void btnCreateMFCTrainFiles_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_MFC, false);
            if (string.IsNullOrEmpty(CommandHelper.GetOutput()))
            {
                MessageBox.Show("Create MFC train files successfully!", "Info");
            }
        }
        #endregion

        #region Build & train all HMMs
        private void btnInitModelHmm0_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP7_HMM0_CMD, false);
            if (string.IsNullOrEmpty(CommandHelper.GetOutput()))
            {
                MessageBox.Show("Run HMM0 successfully!", "Info");
            }
            this.Focus();
        }

        private void btnCreateMacros_Click(object sender, EventArgs e)
        {
            fileManager.MakeMacrosHMM0();
            fileManager.MakeHmmdefs();
            MessageBox.Show("Create macros & hmmdefs successfully!", "Info");
        }        

        private void btnCreateHMM3_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM1_TRAIN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM2_TRAIN, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP9_HMM3_TRAIN, false);
            MessageBox.Show("Create HMM1, HMM2, HMM3 successfully!", "Info");
            this.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fileManager.CreateHMM4();
            MessageBox.Show("Create HMM4 successfully!", "Info");
            this.Focus();
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
            fileManager.MakeFulllist();
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
        private void btnPrepareAllData_Click(object sender, EventArgs e)
        {
            fileManager.CreateFolders();
            fileManager.MakeDict();
            fileManager.MakeMonophones();
            fileManager.MakePrompts();
            fileManager.MakeWords();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE0, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_PHONE1, false);
            fileManager.MakeMfccTrainScp();
            fileManager.MakeTrainScp();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP0_CREATE_MFC, false);

            MessageBox.Show("Create all training data successfully!", "Info");
            this.Focus();
        }

        private void btnBuildAllHmm_Click(object sender, EventArgs e)
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
            fileManager.MakeFulllist();
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP16_TRAINTOHMM13, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM14, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_STEP17_TRAINTOHMM15, false);

            MessageBox.Show("Build & train all HMMs successfully!", "Info");
            this.Focus();
        }

        private void btnCreateMFCTestFiles_Click(object sender, EventArgs e)
        {
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

            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_CREATEMFCC, false);
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_PARSEGRAM, false);
            MessageBox.Show("Create mfcc-test.scp and wdnet successfully", "Info");
            this.Focus();
        }

        private void btnTestSCP_Click(object sender, EventArgs e)
        {
            fileManager.MakeTestScp();
            MessageBox.Show("Create test.scp successfully!", "Info");
        }

        private void btnCreateTestMlf_Click(object sender, EventArgs e)
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
                test.WriteLine("\"*" + filename.Replace('\\', '/') + "\"");

                string so = files[i].Substring(files[i].IndexOf(".wav") - 1, 1);
                so = chuso[int.Parse(so)];
                test.WriteLine(so);
                test.WriteLine(".");
            }
            test.Close();
            MessageBox.Show("Create test.mlf successfully!", "Info");
        }

        private void btnCreateRecout_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_STEP1_RUN, false);
            MessageBox.Show("Run successfully!", "Info");
            this.Focus();
        }

        private void btnGetResult_Click(object sender, EventArgs e)
        {
            CommandHelper.ExecuteCommand(ConstantValues.CMD_TEST_RESULT, true);
            MessageBox.Show(CommandHelper.GetOutput(), "Result");
            this.Focus();
        }
        #endregion
    }
}