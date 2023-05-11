using AppScan;
using AppScan.Scan.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppScanImportUrls
{
 
    /// <summary>
    /// Converts a list of URLs to an EXD (Explore Data) file
    /// </summary>
    internal class UrlsToExd
    {
        private readonly IAppScan _appScan;

        /// <summary>
        /// If true, session cookies from AppScan configuration will be added to each request
        /// </summary>
        public bool UseSessionCookies { get; set; }


        /// <summary>
        /// Base URL to apply to any relatives URLs
        /// </summary>
        public Uri BaseUrl { get; set; }

        /// <summary>
        /// File to import from
        /// </summary>
        public String Filename { get; set; }

        public UrlsToExd(IAppScan appScan)
        {
            _appScan = appScan;
        }

        /// <summary>
        /// Generate an EXD file (as XDocument) from the file containing the list of URLs
        /// </summary>
        /// <returns></returns>
        public XDocument CreateExd()
        {
            var lines = File.ReadLines(Filename);

            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-16", "true"),
                new XElement("requests",
                    from line in lines
                    select CreateRequestNode(line))
            );
            return doc;
        }

        /// <summary>
        /// Generate an xml request node for the exd
        /// </summary>
        /// <param name="strUrl">The URL to generate a request for</param>
        /// <returns>XML Element for the request</returns>
        private XElement CreateRequestNode(string strUrl)
        {
            try
            {
                var url = BaseUrl == null ?
                    new Uri(strUrl.Trim()) :
                    new Uri(BaseUrl, strUrl.Trim());

                return new XElement("request",
                    new XAttribute("scheme", url.Scheme),
                    new XAttribute("host", url.Host),
                    new XAttribute("port", url.Port),
                    new XAttribute("path", url.PathAndQuery),
                    new XAttribute("method", "GET"),
                    new XAttribute("SessionRequestType", "Regular"),
                    new XElement("raw",
                        new XAttribute("encoding", "none"),
                        FormatRequest(url)
                        )
                    );
            }
            catch
            {
                // invalid url (probably)
                return null;
            }
        }

        /// <summary>
        /// Create raw request from given url
        /// </summary>
        /// <param name="url">URL to generate the request from</param>
        /// <returns></returns>
        private string FormatRequest(Uri url)
        {
            string cookies = null;
            if (UseSessionCookies)
            {
                // Generate cookies header
                cookies = string.Join(";",
                    from cookie in _appScan.Scan.ScanData.Config.SessionManagement.DetectedCookies
                    where MatchCookie(url, cookie)
                    select $"{cookie.Name}={cookie.Value}");
            }

            string cookieStr = string.IsNullOrWhiteSpace(cookies) ? "" : $"Cookie: {cookies}\r\n";
            return $"GET {url.PathAndQuery} HTTP/1.1\r\nHost: {url.Host}:{url.Port}\r\n{cookieStr}\r\n";
        }

        /// <summary>
        /// Check if the given cookie should be applied to the given URL
        /// </summary>
        /// <param name="url">URL to check</param>
        /// <param name="cookie">Cookie to check</param>
        /// <returns></returns>
        private bool MatchCookie(Uri url, ICookie cookie)
        {
            return url.Host.Equals(cookie.Domain, StringComparison.OrdinalIgnoreCase) &&
                url.LocalPath.StartsWith(cookie.Path, StringComparison.OrdinalIgnoreCase);
        }
    }
}
