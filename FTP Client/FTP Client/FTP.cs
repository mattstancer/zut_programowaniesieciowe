using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;
namespace FTP_Client
{
    class FTP
    {
        List<string> strs = new List<string>();
        private FtpWebRequest ftpWebRequest;
        private int port = 21;
        private string host = "ftp://mkwk018.cba.pl/zutps.cba.pl/";
        private string user = "zutps";
        private string password = "Zutsieci1";
        private TcpClient client;
        public FTP()
        {
            this.getDirs("");
        }
        public void FtpConnect(string root)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            string newhost = host + root;
            ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(newhost);
            ftpWebRequest.Credentials = new NetworkCredential(user, password);
            
            //string line = reader.ReadLine();

            //{
            //    if (line.Contains("<DIR>"))
            //    {
            //        string msg = line.Substring(line.LastIndexOf("<DIR>") + 5).Trim();
            //        strs.Add(msg);
            //    }
            //    line = reader.ReadLine();
            //}

            //response.Close();

        }
        public void getDirs( string root) {
            this.FtpConnect(root);
            ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string readed = reader.ReadToEnd();
            int i = 0;
            string[] lines = Regex.Split(readed, "\r\n");
            foreach (string line in lines)
            {
               
                if (line != "." && line != ".." && line != "")
                {
                    
                    string filepath = root+ line+"/";
                    Console.WriteLine(filepath);
                    getDirs(filepath);
                }
            }
        }
    }
}
