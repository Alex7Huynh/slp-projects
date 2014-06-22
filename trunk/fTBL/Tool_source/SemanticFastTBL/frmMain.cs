using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SemanticFastTBL
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        FastTBL _fastTBL;

        private void frmMain_Load(object sender, EventArgs e)
        {
            _fastTBL = new FastTBL();
            btnTest.Enabled = false;
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            _fastTBL.Train();            
            tbTrainedRule.Text = _fastTBL.OutputTrainedRuleTemplate();
            btnTest.Enabled = true;
        }

        private void btnChooseFileTest_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();

            // Set filter options and filter index.
            ofDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            ofDialog.FilterIndex = 1;

            // Process input if the user clicked OK.
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                // Open the selected file to read.
                System.IO.Stream fileStream = ofDialog.OpenFile();

                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                {
                    tbTestData.Text = reader.ReadToEnd();                    
                }
                fileStream.Close();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbTestData.Text))
            {
                MessageBox.Show("Please choose file or input test data!", "Info");
                return;
            }
            _fastTBL.Run(tbTestData.Text);
            tbTestData.Text = _fastTBL.OutputTestData();
        }
    }
}
