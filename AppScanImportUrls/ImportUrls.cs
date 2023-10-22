using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AppScan;
using AppScan.Configuration;
using AppScan.Extensions;
using AppScan.Scan.Events;

namespace AppScanImportUrls
{
    /// <summary>
    /// Main extension logic class
    /// </summary>
    public class ImportUrls : IExtensionLogic
    {
        private IAppScan _appScan;
        private IAppScanGui _appScanGui;
        private MenuItem<EventArgs> _menuItem;


        public void Load(IAppScan appScan, IAppScanGui appScanGui, string extensionDir)
        {
            _appScan = appScan;
            _appScanGui = appScanGui;
            _menuItem = new MenuItem<EventArgs>("Import URLs from file", ImportUrlsDialog);
            _appScanGui.ExtensionsMenu.Add(_menuItem);

            _appScan.Scan.StateChanged += ScanOnStateChanged;

        }

        private void ScanOnStateChanged(object sender, StateChangedEventArgs e)
        {
            if (_menuItem != null)
            {
                _menuItem.Enabled = (e.CurrentState == ScanOperationState.Idle);
            }
        }

        /// <summary>
        /// Open the dialog for importing URLs
        /// </summary>
        /// <param name="args"></param>
        private void ImportUrlsDialog(EventArgs args)
        {
            string file = "";
            using (var form = new ImportUrlsForm())
            {
                form.chkUseCookies.Enabled = _appScan.Scan.ScanData.Config.SessionManagement.DetectedCookies.Any();

                var result = form.ShowDialog();
                file = form.txtFilename.Text;
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(file))
                {
                    Uri baseUri = String.IsNullOrWhiteSpace(form.txtBaseUrl.Text) ?
                        null : 
                        new Uri(form.txtBaseUrl.Text.Trim());
                    ImportUrlsFromFile(file, baseUri, form.chkUseCookies.Checked);
                }
            }

            using (var domainsForm=new DomainsViewForm(file))
            {
                var result = domainsForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    AddDomains(domainsForm.SelectedDomains);
                }
            }

        }

        /// <summary>
        /// Perform the import
        /// </summary>
        /// <param name="filePath">File to import from</param>
        /// <param name="baseUrl">Base URL to apply to any relative URLs in the file</param>
        /// <param name="useSessionCookies">Whether to apply session cookie from AppScan configuration to each request</param>
        private void ImportUrlsFromFile(string filePath, Uri baseUrl, bool useSessionCookies)
        {
            // Convert the list of URLS to an EXD file
            var urlsToExd = new UrlsToExd(_appScan)
            {
                Filename = filePath,
                BaseUrl = baseUrl,
                UseSessionCookies = useSessionCookies
            };
            var doc = urlsToExd.CreateExd();
            ImportExd(doc);
        }

        /// <summary>
        /// Import the given xml document as an EXD file
        /// </summary>
        /// <param name="doc"></param>
        private void ImportExd(XDocument doc)
        {
            using (var stream = new MemoryStream())
            {
                doc.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                // Load from memory to AppScan
                _appScan.Scan.RequestRecorder.ImportRecordedRequests(stream, false);
            }
        }
        /// <summary>
        /// adds the list of domains into AppScan additional servers
        /// </summary>
        /// <param name="domainsList">the list of domains to be added</param>
        public void AddDomains(List<string> domainsList)
        {
            foreach (var domain in domainsList)
            {
                _appScan.Scan.ScanData.Config.AdditionalServers.Add(domain);
            }
            _appScan.Scan.RequestRecorder.Analyse();
            _appScanGui.RefreshMainFormGui();
        }

        public ExtensionVersionInfo GetUpdateData(Edition edition, Version targetAppVersion)
        {
            return null;
        }
    }
}
