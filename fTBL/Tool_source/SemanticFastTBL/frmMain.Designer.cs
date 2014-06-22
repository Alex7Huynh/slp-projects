namespace SemanticFastTBL
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
            this.btnTrain = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTrainedRule = new System.Windows.Forms.TextBox();
            this.tbTestData = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnChooseFileTest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTrain
            // 
            this.btnTrain.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrain.Location = new System.Drawing.Point(12, 18);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(103, 29);
            this.btnTrain.TabIndex = 0;
            this.btnTrain.Text = "Train";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(122, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Trained rule:";
            // 
            // tbTrainedRule
            // 
            this.tbTrainedRule.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbTrainedRule.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTrainedRule.Location = new System.Drawing.Point(12, 51);
            this.tbTrainedRule.Multiline = true;
            this.tbTrainedRule.Name = "tbTrainedRule";
            this.tbTrainedRule.ReadOnly = true;
            this.tbTrainedRule.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTrainedRule.Size = new System.Drawing.Size(531, 159);
            this.tbTrainedRule.TabIndex = 2;
            // 
            // tbTestData
            // 
            this.tbTestData.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTestData.Location = new System.Drawing.Point(122, 245);
            this.tbTestData.Multiline = true;
            this.tbTestData.Name = "tbTestData";
            this.tbTestData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTestData.Size = new System.Drawing.Size(421, 233);
            this.tbTestData.TabIndex = 3;
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.Location = new System.Drawing.Point(12, 280);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(103, 29);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnChooseFileTest
            // 
            this.btnChooseFileTest.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseFileTest.Location = new System.Drawing.Point(13, 245);
            this.btnChooseFileTest.Name = "btnChooseFileTest";
            this.btnChooseFileTest.Size = new System.Drawing.Size(103, 29);
            this.btnChooseFileTest.TabIndex = 5;
            this.btnChooseFileTest.Text = "Choose file";
            this.btnChooseFileTest.UseVisualStyleBackColor = true;
            this.btnChooseFileTest.Click += new System.EventHandler(this.btnChooseFileTest_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(122, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Test data:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 490);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnChooseFileTest);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.tbTestData);
            this.Controls.Add(this.tbTrainedRule);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTrain);
            this.Name = "frmMain";
            this.Text = "Semantic Roles Labeling using Fast TBL by Alex Huynh";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTrainedRule;
        private System.Windows.Forms.TextBox tbTestData;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnChooseFileTest;
        private System.Windows.Forms.Label label2;
    }
}

