namespace MTT
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnDi2Mn = new System.Windows.Forms.Button();
            this.tbTrainFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateDICT = new System.Windows.Forms.Button();
            this.btnCreatePROMPTS = new System.Windows.Forms.Button();
            this.btnCreateWORDS = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTestFilePath = new System.Windows.Forms.TextBox();
            this.btnCreateMfccTrain = new System.Windows.Forms.Button();
            this.btnCreateTrainingSCP = new System.Windows.Forms.Button();
            this.btnCreateFolder = new System.Windows.Forms.Button();
            this.btnCreateMacrosHmmdefs = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnInitModelHmm0 = new System.Windows.Forms.Button();
            this.btnTrainHmm1_3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.btnTrainHMM11_12 = new System.Windows.Forms.Button();
            this.btnTrainingHMM13 = new System.Windows.Forms.Button();
            this.btnPrepareDataTest = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCreateFulllist = new System.Windows.Forms.Button();
            this.btnCreateLmtrain = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTrainHMM14_15 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCreateNewLmtrain = new System.Windows.Forms.Button();
            this.btnTrainAll15Hmm = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.btnPrepareAll = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetResult = new System.Windows.Forms.Button();
            this.txtNGram = new System.Windows.Forms.TextBox();
            this.btnCreateRecout = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnBrowseTrainPath = new System.Windows.Forms.Button();
            this.btnBrowseTestPath = new System.Windows.Forms.Button();
            this.tbSentence = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTTS = new System.Windows.Forms.Button();
            this.btnTrainAll17Hmm = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckbIgnoreWords = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDi2Mn
            // 
            this.btnDi2Mn.Location = new System.Drawing.Point(6, 75);
            this.btnDi2Mn.Name = "btnDi2Mn";
            this.btnDi2Mn.Size = new System.Drawing.Size(126, 23);
            this.btnDi2Mn.TabIndex = 2;
            this.btnDi2Mn.Text = "DICT --> monophone";
            this.btnDi2Mn.UseVisualStyleBackColor = true;
            this.btnDi2Mn.Click += new System.EventHandler(this.btnDi2Mn_Click);
            // 
            // tbTrainFilePath
            // 
            this.tbTrainFilePath.Enabled = false;
            this.tbTrainFilePath.Location = new System.Drawing.Point(113, 12);
            this.tbTrainFilePath.Name = "tbTrainFilePath";
            this.tbTrainFilePath.Size = new System.Drawing.Size(423, 20);
            this.tbTrainFilePath.TabIndex = 33;
            this.tbTrainFilePath.Text = "E:\\sn0040\\train";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Train folder path:";
            // 
            // btnCreateDICT
            // 
            this.btnCreateDICT.Location = new System.Drawing.Point(6, 47);
            this.btnCreateDICT.Name = "btnCreateDICT";
            this.btnCreateDICT.Size = new System.Drawing.Size(126, 23);
            this.btnCreateDICT.TabIndex = 1;
            this.btnCreateDICT.Text = "Create DICT";
            this.btnCreateDICT.UseVisualStyleBackColor = true;
            this.btnCreateDICT.Click += new System.EventHandler(this.btnCreateDICT_Click);
            // 
            // btnCreatePROMPTS
            // 
            this.btnCreatePROMPTS.Location = new System.Drawing.Point(6, 133);
            this.btnCreatePROMPTS.Name = "btnCreatePROMPTS";
            this.btnCreatePROMPTS.Size = new System.Drawing.Size(126, 23);
            this.btnCreatePROMPTS.TabIndex = 4;
            this.btnCreatePROMPTS.Text = "Create PROMPTS";
            this.btnCreatePROMPTS.UseVisualStyleBackColor = true;
            this.btnCreatePROMPTS.Click += new System.EventHandler(this.btnCreatePROMPTS_Click);
            // 
            // btnCreateWORDS
            // 
            this.btnCreateWORDS.Location = new System.Drawing.Point(6, 161);
            this.btnCreateWORDS.Name = "btnCreateWORDS";
            this.btnCreateWORDS.Size = new System.Drawing.Size(126, 23);
            this.btnCreateWORDS.TabIndex = 5;
            this.btnCreateWORDS.Text = "Create WORDS.MLF";
            this.btnCreateWORDS.UseVisualStyleBackColor = true;
            this.btnCreateWORDS.Click += new System.EventHandler(this.btnCreateWORDS_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Test folder path:";
            // 
            // tbTestFilePath
            // 
            this.tbTestFilePath.Enabled = false;
            this.tbTestFilePath.Location = new System.Drawing.Point(113, 40);
            this.tbTestFilePath.Name = "tbTestFilePath";
            this.tbTestFilePath.Size = new System.Drawing.Size(423, 20);
            this.tbTestFilePath.TabIndex = 34;
            this.tbTestFilePath.Text = "E:\\sn0040\\test";
            // 
            // btnCreateMfccTrain
            // 
            this.btnCreateMfccTrain.Location = new System.Drawing.Point(6, 218);
            this.btnCreateMfccTrain.Name = "btnCreateMfccTrain";
            this.btnCreateMfccTrain.Size = new System.Drawing.Size(126, 23);
            this.btnCreateMfccTrain.TabIndex = 7;
            this.btnCreateMfccTrain.Text = "Create mfcc-train.scp";
            this.btnCreateMfccTrain.UseVisualStyleBackColor = true;
            this.btnCreateMfccTrain.Click += new System.EventHandler(this.btnCreateMfccTrain_Click);
            // 
            // btnCreateTrainingSCP
            // 
            this.btnCreateTrainingSCP.Location = new System.Drawing.Point(6, 246);
            this.btnCreateTrainingSCP.Name = "btnCreateTrainingSCP";
            this.btnCreateTrainingSCP.Size = new System.Drawing.Size(126, 23);
            this.btnCreateTrainingSCP.TabIndex = 8;
            this.btnCreateTrainingSCP.Text = "Create train.scp";
            this.btnCreateTrainingSCP.UseVisualStyleBackColor = true;
            this.btnCreateTrainingSCP.Click += new System.EventHandler(this.btnCreateTrainingSCP_Click);
            // 
            // btnCreateFolder
            // 
            this.btnCreateFolder.Location = new System.Drawing.Point(6, 19);
            this.btnCreateFolder.Name = "btnCreateFolder";
            this.btnCreateFolder.Size = new System.Drawing.Size(126, 23);
            this.btnCreateFolder.TabIndex = 0;
            this.btnCreateFolder.Text = "Create folders";
            this.btnCreateFolder.UseVisualStyleBackColor = true;
            this.btnCreateFolder.Click += new System.EventHandler(this.btnCreateFolder_Click);
            // 
            // btnCreateMacrosHmmdefs
            // 
            this.btnCreateMacrosHmmdefs.Location = new System.Drawing.Point(14, 46);
            this.btnCreateMacrosHmmdefs.Name = "btnCreateMacrosHmmdefs";
            this.btnCreateMacrosHmmdefs.Size = new System.Drawing.Size(261, 23);
            this.btnCreateMacrosHmmdefs.TabIndex = 11;
            this.btnCreateMacrosHmmdefs.Text = "Step 8: Create macros && hmmdefs";
            this.btnCreateMacrosHmmdefs.UseVisualStyleBackColor = true;
            this.btnCreateMacrosHmmdefs.Click += new System.EventHandler(this.btnCreateMacrosHmmdefs_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Create mfc train files";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnCreateMfccTrainFiles_Click);
            // 
            // btnInitModelHmm0
            // 
            this.btnInitModelHmm0.Location = new System.Drawing.Point(14, 19);
            this.btnInitModelHmm0.Name = "btnInitModelHmm0";
            this.btnInitModelHmm0.Size = new System.Drawing.Size(261, 23);
            this.btnInitModelHmm0.TabIndex = 10;
            this.btnInitModelHmm0.Text = "Step7: Init Model (hmm0)";
            this.btnInitModelHmm0.UseVisualStyleBackColor = true;
            this.btnInitModelHmm0.Click += new System.EventHandler(this.btnInitModelHmm0_Click);
            // 
            // btnTrainHmm1_3
            // 
            this.btnTrainHmm1_3.Location = new System.Drawing.Point(14, 75);
            this.btnTrainHmm1_3.Name = "btnTrainHmm1_3";
            this.btnTrainHmm1_3.Size = new System.Drawing.Size(261, 23);
            this.btnTrainHmm1_3.TabIndex = 12;
            this.btnTrainHmm1_3.Text = "Step 9: Train (3 times) to HMM3";
            this.btnTrainHmm1_3.UseVisualStyleBackColor = true;
            this.btnTrainHmm1_3.Click += new System.EventHandler(this.btnTrainHmm1_3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 189);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(126, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Create phones (mlf)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnMakePhones_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(14, 104);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(261, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "Step 10: Copy HMM3 -> HMM4, add sp to hmmdefs";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnCreateHMM4_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(14, 133);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(261, 23);
            this.button6.TabIndex = 14;
            this.button6.Text = "Step 11: Connect sil && sp (-->HMM5)";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btnCreateHMM5_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(14, 161);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(261, 23);
            this.button7.TabIndex = 15;
            this.button7.Text = "Step 12: Train twice to HMM7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.btnCreateHMM7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(14, 189);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(261, 23);
            this.button8.TabIndex = 16;
            this.button8.Text = "Step 13: Train twice to HMM9";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.btnCreateHMM9_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(14, 218);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(261, 23);
            this.button9.TabIndex = 17;
            this.button9.Text = "Step 14: Create triphone, wintri and train to HMM10";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.btnCreateHMM10_Click);
            // 
            // btnTrainHMM11_12
            // 
            this.btnTrainHMM11_12.Location = new System.Drawing.Point(14, 246);
            this.btnTrainHMM11_12.Name = "btnTrainHMM11_12";
            this.btnTrainHMM11_12.Size = new System.Drawing.Size(261, 23);
            this.btnTrainHMM11_12.TabIndex = 18;
            this.btnTrainHMM11_12.Text = "Step 15: Train twice to HMM12";
            this.btnTrainHMM11_12.UseVisualStyleBackColor = true;
            this.btnTrainHMM11_12.Click += new System.EventHandler(this.btnCreateHMM12_Click);
            // 
            // btnTrainingHMM13
            // 
            this.btnTrainingHMM13.Location = new System.Drawing.Point(14, 275);
            this.btnTrainingHMM13.Name = "btnTrainingHMM13";
            this.btnTrainingHMM13.Size = new System.Drawing.Size(261, 23);
            this.btnTrainingHMM13.TabIndex = 19;
            this.btnTrainingHMM13.Text = "Step 16: Train to HMM13";
            this.btnTrainingHMM13.Click += new System.EventHandler(this.btnCreateHMM13_Click);
            // 
            // btnPrepareDataTest
            // 
            this.btnPrepareDataTest.Location = new System.Drawing.Point(6, 75);
            this.btnPrepareDataTest.Name = "btnPrepareDataTest";
            this.btnPrepareDataTest.Size = new System.Drawing.Size(134, 23);
            this.btnPrepareDataTest.TabIndex = 21;
            this.btnPrepareDataTest.Text = "Create mfc test files";
            this.btnPrepareDataTest.UseVisualStyleBackColor = true;
            this.btnPrepareDataTest.Click += new System.EventHandler(this.btnCreateMfccTestFiles_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreateFulllist);
            this.groupBox1.Controls.Add(this.btnCreateDICT);
            this.groupBox1.Controls.Add(this.btnDi2Mn);
            this.groupBox1.Controls.Add(this.btnCreatePROMPTS);
            this.groupBox1.Controls.Add(this.btnCreateWORDS);
            this.groupBox1.Controls.Add(this.btnCreateMfccTrain);
            this.groupBox1.Controls.Add(this.btnCreateFolder);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.btnCreateTrainingSCP);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(23, 271);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 340);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prepare training data";
            // 
            // btnCreateFulllist
            // 
            this.btnCreateFulllist.Location = new System.Drawing.Point(6, 104);
            this.btnCreateFulllist.Name = "btnCreateFulllist";
            this.btnCreateFulllist.Size = new System.Drawing.Size(126, 23);
            this.btnCreateFulllist.TabIndex = 3;
            this.btnCreateFulllist.Text = "Create fulllist";
            this.btnCreateFulllist.UseVisualStyleBackColor = true;
            this.btnCreateFulllist.Click += new System.EventHandler(this.btnCreateFulllist_Click);
            // 
            // btnCreateLmtrain
            // 
            this.btnCreateLmtrain.Location = new System.Drawing.Point(6, 103);
            this.btnCreateLmtrain.Name = "btnCreateLmtrain";
            this.btnCreateLmtrain.Size = new System.Drawing.Size(133, 23);
            this.btnCreateLmtrain.TabIndex = 24;
            this.btnCreateLmtrain.Text = "Create lmtrain (270+30)";
            this.btnCreateLmtrain.UseVisualStyleBackColor = true;
            this.btnCreateLmtrain.Click += new System.EventHandler(this.btnCreateLmtrain_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTrainHMM14_15);
            this.groupBox2.Controls.Add(this.btnInitModelHmm0);
            this.groupBox2.Controls.Add(this.btnTrainingHMM13);
            this.groupBox2.Controls.Add(this.btnCreateMacrosHmmdefs);
            this.groupBox2.Controls.Add(this.btnTrainHmm1_3);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.btnTrainHMM11_12);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button9);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Location = new System.Drawing.Point(169, 271);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 340);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Build && train HMM";
            // 
            // btnTrainHMM14_15
            // 
            this.btnTrainHMM14_15.Location = new System.Drawing.Point(14, 304);
            this.btnTrainHMM14_15.Name = "btnTrainHMM14_15";
            this.btnTrainHMM14_15.Size = new System.Drawing.Size(261, 23);
            this.btnTrainHMM14_15.TabIndex = 20;
            this.btnTrainHMM14_15.Text = "Step 17: Train twice to HMM15";
            this.btnTrainHMM14_15.Click += new System.EventHandler(this.btnCreateHMM15_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCreateNewLmtrain);
            this.groupBox3.Controls.Add(this.btnTrainAll15Hmm);
            this.groupBox3.Controls.Add(this.button10);
            this.groupBox3.Controls.Add(this.btnPrepareAll);
            this.groupBox3.Controls.Add(this.btnCreateLmtrain);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnGetResult);
            this.groupBox3.Controls.Add(this.txtNGram);
            this.groupBox3.Controls.Add(this.btnCreateRecout);
            this.groupBox3.Controls.Add(this.btnPrepareDataTest);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new System.Drawing.Point(462, 271);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(150, 340);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lab3";
            // 
            // btnCreateNewLmtrain
            // 
            this.btnCreateNewLmtrain.Location = new System.Drawing.Point(6, 132);
            this.btnCreateNewLmtrain.Name = "btnCreateNewLmtrain";
            this.btnCreateNewLmtrain.Size = new System.Drawing.Size(133, 23);
            this.btnCreateNewLmtrain.TabIndex = 25;
            this.btnCreateNewLmtrain.Text = "Create lmtrain (270+n)";
            this.btnCreateNewLmtrain.UseVisualStyleBackColor = true;
            this.btnCreateNewLmtrain.Click += new System.EventHandler(this.btnCreateNewLmtrain_Click);
            // 
            // btnTrainAll15Hmm
            // 
            this.btnTrainAll15Hmm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnTrainAll15Hmm.Location = new System.Drawing.Point(6, 46);
            this.btnTrainAll15Hmm.Name = "btnTrainAll15Hmm";
            this.btnTrainAll15Hmm.Size = new System.Drawing.Size(133, 23);
            this.btnTrainAll15Hmm.TabIndex = 4;
            this.btnTrainAll15Hmm.Text = "Build && train all HMM0-15";
            this.btnTrainAll15Hmm.UseVisualStyleBackColor = false;
            this.btnTrainAll15Hmm.Click += new System.EventHandler(this.btnTrainAll15Hmm_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(6, 271);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(134, 23);
            this.button10.TabIndex = 30;
            this.button10.Text = "Show Perflexity";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.btnShowPerplexity_Click);
            // 
            // btnPrepareAll
            // 
            this.btnPrepareAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnPrepareAll.Location = new System.Drawing.Point(6, 19);
            this.btnPrepareAll.Name = "btnPrepareAll";
            this.btnPrepareAll.Size = new System.Drawing.Size(133, 23);
            this.btnPrepareAll.TabIndex = 3;
            this.btnPrepareAll.Text = "Prepare all data";
            this.btnPrepareAll.UseVisualStyleBackColor = false;
            this.btnPrepareAll.Click += new System.EventHandler(this.btnPrepareAll_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "n-gram number:";
            // 
            // btnGetResult
            // 
            this.btnGetResult.Location = new System.Drawing.Point(6, 244);
            this.btnGetResult.Name = "btnGetResult";
            this.btnGetResult.Size = new System.Drawing.Size(134, 23);
            this.btnGetResult.TabIndex = 29;
            this.btnGetResult.Text = "Show result";
            this.btnGetResult.UseVisualStyleBackColor = true;
            this.btnGetResult.Click += new System.EventHandler(this.btnGetResult_Click);
            // 
            // txtNGram
            // 
            this.txtNGram.Location = new System.Drawing.Point(98, 162);
            this.txtNGram.Name = "txtNGram";
            this.txtNGram.Size = new System.Drawing.Size(42, 20);
            this.txtNGram.TabIndex = 26;
            this.txtNGram.Text = "2";
            this.txtNGram.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNGram_KeyPress);
            // 
            // btnCreateRecout
            // 
            this.btnCreateRecout.Location = new System.Drawing.Point(6, 215);
            this.btnCreateRecout.Name = "btnCreateRecout";
            this.btnCreateRecout.Size = new System.Drawing.Size(134, 23);
            this.btnCreateRecout.TabIndex = 28;
            this.btnCreateRecout.Text = "Test model";
            this.btnCreateRecout.UseVisualStyleBackColor = true;
            this.btnCreateRecout.Click += new System.EventHandler(this.btnTestModel_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 188);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "Init && build n-gram model";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnInitBuildModel_Click);
            // 
            // btnBrowseTrainPath
            // 
            this.btnBrowseTrainPath.Location = new System.Drawing.Point(549, 12);
            this.btnBrowseTrainPath.Name = "btnBrowseTrainPath";
            this.btnBrowseTrainPath.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseTrainPath.TabIndex = 31;
            this.btnBrowseTrainPath.Text = "Browse";
            this.btnBrowseTrainPath.UseVisualStyleBackColor = true;
            this.btnBrowseTrainPath.Click += new System.EventHandler(this.btnBrowseTrainPath_Click);
            // 
            // btnBrowseTestPath
            // 
            this.btnBrowseTestPath.Location = new System.Drawing.Point(549, 40);
            this.btnBrowseTestPath.Name = "btnBrowseTestPath";
            this.btnBrowseTestPath.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseTestPath.TabIndex = 32;
            this.btnBrowseTestPath.Text = "Browse";
            this.btnBrowseTestPath.UseVisualStyleBackColor = true;
            this.btnBrowseTestPath.Click += new System.EventHandler(this.btnBrowseTestPath_Click);
            // 
            // tbSentence
            // 
            this.tbSentence.Location = new System.Drawing.Point(90, 16);
            this.tbSentence.Multiline = true;
            this.tbSentence.Name = "tbSentence";
            this.tbSentence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSentence.Size = new System.Drawing.Size(511, 141);
            this.tbSentence.TabIndex = 36;
            this.tbSentence.Text = "TP.HCM\r\nNgày 31/05/2014\r\nVĩ độ 21.02\r\nKinh độ: 105.85\r\nNhiệt độ hóa sương: 25°C\r\n" +
                "Tầm nhìn: 10.0 km\r\nĐộ ẩm: 100;\r\n13 + 52 + 20 = 85";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Sentence:";
            // 
            // btnTTS
            // 
            this.btnTTS.Location = new System.Drawing.Point(526, 164);
            this.btnTTS.Name = "btnTTS";
            this.btnTTS.Size = new System.Drawing.Size(75, 23);
            this.btnTTS.TabIndex = 2;
            this.btnTTS.Text = "Speak";
            this.btnTTS.UseVisualStyleBackColor = true;
            this.btnTTS.Click += new System.EventHandler(this.btnTTS_Click);
            // 
            // btnTrainAll17Hmm
            // 
            this.btnTrainAll17Hmm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnTrainAll17Hmm.Location = new System.Drawing.Point(90, 164);
            this.btnTrainAll17Hmm.Name = "btnTrainAll17Hmm";
            this.btnTrainAll17Hmm.Size = new System.Drawing.Size(218, 22);
            this.btnTrainAll17Hmm.TabIndex = 0;
            this.btnTrainAll17Hmm.Text = "Prepare data, build && train all HMM0-17";
            this.btnTrainAll17Hmm.UseVisualStyleBackColor = false;
            this.btnTrainAll17Hmm.Click += new System.EventHandler(this.btnTrainAll17Hmm_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ckbIgnoreWords);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btnTrainAll17Hmm);
            this.groupBox4.Controls.Add(this.tbSentence);
            this.groupBox4.Controls.Add(this.btnTTS);
            this.groupBox4.Location = new System.Drawing.Point(23, 66);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(607, 199);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Lab4";
            // 
            // ckbIgnoreWords
            // 
            this.ckbIgnoreWords.AutoSize = true;
            this.ckbIgnoreWords.Location = new System.Drawing.Point(346, 167);
            this.ckbIgnoreWords.Name = "ckbIgnoreWords";
            this.ckbIgnoreWords.Size = new System.Drawing.Size(134, 17);
            this.ckbIgnoreWords.TabIndex = 1;
            this.ckbIgnoreWords.Text = "Ignore unknown words";
            this.ckbIgnoreWords.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 616);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnBrowseTestPath);
            this.Controls.Add(this.btnBrowseTrainPath);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbTestFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTrainFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "MTT Tool by Alex Huynh, Master Minh & HVTan";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDi2Mn;
        private System.Windows.Forms.TextBox tbTrainFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateDICT;
        private System.Windows.Forms.Button btnCreatePROMPTS;
        private System.Windows.Forms.Button btnCreateWORDS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTestFilePath;
        private System.Windows.Forms.Button btnCreateMfccTrain;
        private System.Windows.Forms.Button btnCreateTrainingSCP;
        private System.Windows.Forms.Button btnCreateFolder;
        private System.Windows.Forms.Button btnCreateMacrosHmmdefs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnInitModelHmm0;
        private System.Windows.Forms.Button btnTrainHmm1_3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button btnTrainHMM11_12;
        private System.Windows.Forms.Button btnTrainingHMM13;
        private System.Windows.Forms.Button btnPrepareDataTest;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnTrainHMM14_15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGetResult;
        private System.Windows.Forms.Button btnCreateRecout;
        private System.Windows.Forms.Button btnCreateLmtrain;
        private System.Windows.Forms.Button btnCreateFulllist;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNGram;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button btnBrowseTrainPath;
        private System.Windows.Forms.Button btnBrowseTestPath;
        private System.Windows.Forms.Button btnCreateNewLmtrain;
        private System.Windows.Forms.TextBox tbSentence;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTTS;
        private System.Windows.Forms.Button btnPrepareAll;
        private System.Windows.Forms.Button btnTrainAll15Hmm;
        private System.Windows.Forms.Button btnTrainAll17Hmm;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox ckbIgnoreWords;
    }
}

