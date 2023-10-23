using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AppScanImportUrls
{
    public partial class DomainsViewForm : Form
    {
        private HashSet<string> _uniqueDomains = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public List<string> SelectedDomains = new List<string>();

        public DomainsViewForm(string filename)
        {
            InitializeComponent();
            // Read URLs from a file and extract unique domains
            if (!File.Exists(filename))
            {
                MessageBox.Show("File not found: " + filename);
            }
            foreach (var line in File.ReadLines(filename))
            {
                try
                {
                    Uri uri = new Uri(line);
                    string domain = uri.Host;
                    _uniqueDomains.Add(domain);
                }
                catch (UriFormatException)
                {
                    Console.WriteLine($"Invalid URL: {line}");
                }
            }

            // Populate the ListBox with unique domains
            foreach (var domain in _uniqueDomains)
            {
                DomainsListBox.Items.Add(domain);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Get selected domains from the ListBox
            foreach (var item in DomainsListBox.SelectedItems)
            {
                SelectedDomains.Add(item.ToString());
            }

            // For debug purpose uncomment the following:
            //string selectedDomainsText = string.Join(", ", SelectedDomains);
            //MessageBox.Show("Selected domains: " + selectedDomainsText);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void DomainsViewForm_Load(object sender, EventArgs e)
        {

        }

    }
}
