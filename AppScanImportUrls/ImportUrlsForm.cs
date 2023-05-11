using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppScanImportUrls
{
    public partial class ImportUrlsForm : Form
    {
        public ImportUrlsForm()
        {
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilename.Text = openFileDialog.FileName;
                }
            }
        }

        private void ImportUrlsForm_Load(object sender, EventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtFilename.Text))
            {
                MessageBox.Show("Invalid file name", "Error");
            } 
            else if (!CheckBaseUrl(txtBaseUrl.Text))
            {
                MessageBox.Show("Invalid Base URL", "Error");
            } 
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool CheckBaseUrl(string baseUri)
        {
            if (String.IsNullOrWhiteSpace(baseUri))
            {
                return true;
            }

            return Uri.TryCreate(baseUri, UriKind.Absolute, out _);
        }
    }
}
