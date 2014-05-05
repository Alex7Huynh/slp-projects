namespace Lab2
{
    partial class Form1
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
            this.btnDi2Mn = new System.Windows.Forms.Button();
            this.tbTrainFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateDICT = new System.Windows.Forms.Button();
            this.btnCreatePROMPTS = new System.Windows.Forms.Button();
            this.btnCreateWORDS = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTestFilePath = new System.Windows.Forms.TextBox();
            this.btnCreateMFC = new System.Windows.Forms.Button();
            this.btnTestSCP = new System.Windows.Forms.Button();
            this.btnCreateTrainingSCP = new System.Windows.Forms.Button();
            this.btnCreateFolder = new System.Windows.Forms.Button();
            this.btnDeleteFolder = new System.Windows.Forms.Button();
            this.btnCreateHmmdefs = new System.Windows.Forms.Button();
            this.btnCreateMacros = new System.Windows.Forms.Button();
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
            this.SuspendLayout();
            // 
            // btnDi2Mn
            // 
            this.btnDi2Mn.Location = new System.Drawing.Point(109, 95);
            this.btnDi2Mn.Name = "btnDi2Mn";
            this.btnDi2Mn.Size = new System.Drawing.Size(128, 23);
            this.btnDi2Mn.TabIndex = 3;
            this.btnDi2Mn.Text = "DICT --> monophone";
            this.btnDi2Mn.UseVisualStyleBackColor = true;
            this.btnDi2Mn.Click += new System.EventHandler(this.btnDi2Mn_Click);
            // 
            // tbTrainFilePath
            // 
            this.tbTrainFilePath.Location = new System.Drawing.Point(97, 12);
            this.tbTrainFilePath.Name = "tbTrainFilePath";
            this.tbTrainFilePath.Size = new System.Drawing.Size(223, 20);
            this.tbTrainFilePath.TabIndex = 0;
            this.tbTrainFilePath.Text = "E:\\sn0040\\train";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Train folder path:";
            // 
            // btnCreateDICT
            // 
            this.btnCreateDICT.Location = new System.Drawing.Point(109, 66);
            this.btnCreateDICT.Name = "btnCreateDICT";
            this.btnCreateDICT.Size = new System.Drawing.Size(128, 23);
            this.btnCreateDICT.TabIndex = 2;
            this.btnCreateDICT.Text = "Create DICT";
            this.btnCreateDICT.UseVisualStyleBackColor = true;
            this.btnCreateDICT.Click += new System.EventHandler(this.btnCreateDICT_Click);
            // 
            // btnCreatePROMPTS
            // 
            this.btnCreatePROMPTS.Location = new System.Drawing.Point(109, 124);
            this.btnCreatePROMPTS.Name = "btnCreatePROMPTS";
            this.btnCreatePROMPTS.Size = new System.Drawing.Size(128, 23);
            this.btnCreatePROMPTS.TabIndex = 4;
            this.btnCreatePROMPTS.Text = "Create PROMPTS";
            this.btnCreatePROMPTS.UseVisualStyleBackColor = true;
            this.btnCreatePROMPTS.Click += new System.EventHandler(this.btnCreatePROMPTS_Click);
            // 
            // btnCreateWORDS
            // 
            this.btnCreateWORDS.Location = new System.Drawing.Point(109, 153);
            this.btnCreateWORDS.Name = "btnCreateWORDS";
            this.btnCreateWORDS.Size = new System.Drawing.Size(128, 23);
            this.btnCreateWORDS.TabIndex = 5;
            this.btnCreateWORDS.Text = "Create WORDS.MLF";
            this.btnCreateWORDS.UseVisualStyleBackColor = true;
            this.btnCreateWORDS.Click += new System.EventHandler(this.btnCreateWORDS_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Test folder path:";
            // 
            // tbTestFilePath
            // 
            this.tbTestFilePath.Location = new System.Drawing.Point(97, 40);
            this.tbTestFilePath.Name = "tbTestFilePath";
            this.tbTestFilePath.Size = new System.Drawing.Size(221, 20);
            this.tbTestFilePath.TabIndex = 1;
            this.tbTestFilePath.Text = "E:\\sn0040\\test";
            // 
            // btnCreateMFC
            // 
            this.btnCreateMFC.Location = new System.Drawing.Point(109, 240);
            this.btnCreateMFC.Name = "btnCreateMFC";
            this.btnCreateMFC.Size = new System.Drawing.Size(128, 23);
            this.btnCreateMFC.TabIndex = 7;
            this.btnCreateMFC.Text = "Create mfcc.scp";
            this.btnCreateMFC.UseVisualStyleBackColor = true;
            this.btnCreateMFC.Click += new System.EventHandler(this.btnCreateMFC_Click);
            // 
            // btnTestSCP
            // 
            this.btnTestSCP.Location = new System.Drawing.Point(192, 269);
            this.btnTestSCP.Name = "btnTestSCP";
            this.btnTestSCP.Size = new System.Drawing.Size(148, 23);
            this.btnTestSCP.TabIndex = 9;
            this.btnTestSCP.Text = "Create test.scp";
            this.btnTestSCP.UseVisualStyleBackColor = true;
            this.btnTestSCP.Click += new System.EventHandler(this.btnTestSCP_Click);
            // 
            // btnCreateTrainingSCP
            // 
            this.btnCreateTrainingSCP.Location = new System.Drawing.Point(28, 269);
            this.btnCreateTrainingSCP.Name = "btnCreateTrainingSCP";
            this.btnCreateTrainingSCP.Size = new System.Drawing.Size(128, 23);
            this.btnCreateTrainingSCP.TabIndex = 8;
            this.btnCreateTrainingSCP.Text = "Create train.scp";
            this.btnCreateTrainingSCP.UseVisualStyleBackColor = true;
            this.btnCreateTrainingSCP.Click += new System.EventHandler(this.btnCreateTrainingSCP_Click);
            // 
            // btnCreateFolder
            // 
            this.btnCreateFolder.Location = new System.Drawing.Point(14, 211);
            this.btnCreateFolder.Name = "btnCreateFolder";
            this.btnCreateFolder.Size = new System.Drawing.Size(158, 23);
            this.btnCreateFolder.TabIndex = 6;
            this.btnCreateFolder.Text = "Create folders (mfc, hmm0..15)";
            this.btnCreateFolder.UseVisualStyleBackColor = true;
            this.btnCreateFolder.Click += new System.EventHandler(this.btnCreateFolder_Click);
            // 
            // btnDeleteFolder
            // 
            this.btnDeleteFolder.Location = new System.Drawing.Point(178, 211);
            this.btnDeleteFolder.Name = "btnDeleteFolder";
            this.btnDeleteFolder.Size = new System.Drawing.Size(163, 23);
            this.btnDeleteFolder.TabIndex = 10;
            this.btnDeleteFolder.Text = "Delete folders (mfc, hmm0..15)";
            this.btnDeleteFolder.UseVisualStyleBackColor = true;
            this.btnDeleteFolder.Click += new System.EventHandler(this.btnDeleteFolder_Click);
            // 
            // btnCreateHmmdefs
            // 
            this.btnCreateHmmdefs.Location = new System.Drawing.Point(29, 385);
            this.btnCreateHmmdefs.Name = "btnCreateHmmdefs";
            this.btnCreateHmmdefs.Size = new System.Drawing.Size(312, 23);
            this.btnCreateHmmdefs.TabIndex = 11;
            this.btnCreateHmmdefs.Text = "Step 8: monophones0, proto (hmm0) --> hmmdefs";
            this.btnCreateHmmdefs.UseVisualStyleBackColor = true;
            this.btnCreateHmmdefs.Click += new System.EventHandler(this.btnCreateHmmdefs_Click);
            // 
            // btnCreateMacros
            // 
            this.btnCreateMacros.Location = new System.Drawing.Point(29, 356);
            this.btnCreateMacros.Name = "btnCreateMacros";
            this.btnCreateMacros.Size = new System.Drawing.Size(312, 23);
            this.btnCreateMacros.TabIndex = 10;
            this.btnCreateMacros.Text = "Step 8: proto, vFloors (hmm0) --> macros";
            this.btnCreateMacros.UseVisualStyleBackColor = true;
            this.btnCreateMacros.Click += new System.EventHandler(this.btnCreateMacros_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 298);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(312, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Create MFC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInitModelHmm0
            // 
            this.btnInitModelHmm0.Location = new System.Drawing.Point(29, 327);
            this.btnInitModelHmm0.Name = "btnInitModelHmm0";
            this.btnInitModelHmm0.Size = new System.Drawing.Size(312, 23);
            this.btnInitModelHmm0.TabIndex = 13;
            this.btnInitModelHmm0.Text = "Step7: Init Model (hmm0)";
            this.btnInitModelHmm0.UseVisualStyleBackColor = true;
            this.btnInitModelHmm0.Click += new System.EventHandler(this.btnInitModelHmm0_Click);
            // 
            // btnTrainHmm1_3
            // 
            this.btnTrainHmm1_3.Location = new System.Drawing.Point(29, 428);
            this.btnTrainHmm1_3.Name = "btnTrainHmm1_3";
            this.btnTrainHmm1_3.Size = new System.Drawing.Size(312, 23);
            this.btnTrainHmm1_3.TabIndex = 14;
            this.btnTrainHmm1_3.Text = "Step 9: Run Train (3 times) to HMM3";
            this.btnTrainHmm1_3.UseVisualStyleBackColor = true;
            this.btnTrainHmm1_3.Click += new System.EventHandler(this.btnTrainHmm1_3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(109, 182);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "Create phones.mlf (2 file)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(29, 470);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(311, 23);
            this.button5.TabIndex = 16;
            this.button5.Text = "Step 10: Copy HMM3 -> HMM4, add sp to hmmdefs";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(28, 522);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(312, 23);
            this.button6.TabIndex = 17;
            this.button6.Text = "Step 11: Connect sil & sp";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(28, 551);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(312, 23);
            this.button7.TabIndex = 18;
            this.button7.Text = "Step 12: Training twices to hmm7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(28, 580);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(312, 23);
            this.button8.TabIndex = 19;
            this.button8.Text = "Step 13: Training  twices to hmm9";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(28, 609);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(312, 23);
            this.button9.TabIndex = 20;
            this.button9.Text = "Step 14: Create triphone, wintri and train to hmm10";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // btnTrainHMM11_12
            // 
            this.btnTrainHMM11_12.Location = new System.Drawing.Point(28, 638);
            this.btnTrainHMM11_12.Name = "btnTrainHMM11_12";
            this.btnTrainHMM11_12.Size = new System.Drawing.Size(312, 23);
            this.btnTrainHMM11_12.TabIndex = 21;
            this.btnTrainHMM11_12.Text = "Step 15: Training twices to hmm12";
            this.btnTrainHMM11_12.UseVisualStyleBackColor = true;
            this.btnTrainHMM11_12.Click += new System.EventHandler(this.btnTrainHMM11_12_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 682);
            this.Controls.Add(this.btnTrainHMM11_12);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnTrainHmm1_3);
            this.Controls.Add(this.btnInitModelHmm0);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCreateMacros);
            this.Controls.Add(this.btnCreateHmmdefs);
            this.Controls.Add(this.btnDeleteFolder);
            this.Controls.Add(this.btnCreateFolder);
            this.Controls.Add(this.btnCreateTrainingSCP);
            this.Controls.Add(this.btnTestSCP);
            this.Controls.Add(this.btnCreateMFC);
            this.Controls.Add(this.tbTestFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCreateWORDS);
            this.Controls.Add(this.btnCreatePROMPTS);
            this.Controls.Add(this.btnCreateDICT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTrainFilePath);
            this.Controls.Add(this.btnDi2Mn);
            this.Name = "Form1";
            this.Text = "Lab2 by Alex Huynh";
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
        private System.Windows.Forms.Button btnCreateMFC;
        private System.Windows.Forms.Button btnTestSCP;
        private System.Windows.Forms.Button btnCreateTrainingSCP;
        private System.Windows.Forms.Button btnCreateFolder;
        private System.Windows.Forms.Button btnDeleteFolder;
        private System.Windows.Forms.Button btnCreateHmmdefs;
        private System.Windows.Forms.Button btnCreateMacros;
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
    }
}

