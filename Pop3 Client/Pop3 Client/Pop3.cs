using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography;

using System.IO;
namespace Pop3_Client
{
    class Pop3
    {
        public TcpClient cli;
        private StreamWriter sw;
        private System.IO.StreamReader reader;
        private int port=110;
        private string host = "pop.gmail.com";
    
        public Pop3(int port, string host) {
            
            this.port = port;
            this.host = host;
            
        }
        public string clientRun() {
            this.cli = new TcpClient();
            this.cli.Connect(this.host, this.port);
            byte[] bytesToSend = System.Text.Encoding.ASCII.GetBytes("STAT");
            System.Net.Security.SslStream sslstream = new SslStream(this.cli.GetStream());
            sslstream.AuthenticateAsClient(this.host);
             sw = new StreamWriter(sslstream);
             reader = new StreamReader(sslstream);
            sw.WriteLine("STAT");
            sw.Flush();
            string str= reader.ReadLine();
            return str;
            this.cli.GetStream().Close();
            this.cli.Close();
        }
        public string stopClient() {
         
            this.cli = new TcpClient();
            this.cli.Connect(this.host, this.port);
            System.Net.Security.SslStream sslstream = new SslStream(this.cli.GetStream());
            sslstream.AuthenticateAsClient(this.host);
            sw = new StreamWriter(sslstream);
            reader = new StreamReader(sslstream);
            sw.WriteLine("QUIT");
            sw.Flush();
            string str = reader.ReadLine();
            this.cli.GetStream().Close();
            this.cli.Close();
            
            return str;
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public string login(string email, string password) {
            //password = GetMd5Hash(MD5.Create(), password);
            this.cli = new TcpClient();
            this.cli.Connect(this.host, this.port);
            System.Net.Security.SslStream sslstream = new SslStream(this.cli.GetStream());
            sslstream.AuthenticateAsClient(this.host);
            StreamWriter sw = new StreamWriter(sslstream);
            System.IO.StreamReader reader = new StreamReader(sslstream);
            sw.WriteLine("USER " + email); sw.Flush();
            sw.WriteLine("PASS "+password); sw.Flush();
            sw.WriteLine("STAT");
            sw.Flush();
            sw.WriteLine("Quit ");
            sw.Flush();
            string str = string.Empty;
            string strTemp = string.Empty;
            while ((strTemp = reader.ReadLine()) != null)
            {
                if (".".Equals(strTemp))
                {
                    break;
                }
                if (strTemp.IndexOf("-ERR") != -1)
                {
                    break;
                }
                str += strTemp+"\r\n";
            }
         
            string ret = str;
            return ret;
        }
        public string checkHeaders(int roznica, int messages,string email, string password)
        {
            //password = GetMd5Hash(MD5.Create(), password);
            this.cli = new TcpClient();
            this.cli.Connect(this.host, this.port);
            System.Net.Security.SslStream sslstream = new SslStream(this.cli.GetStream());
            sslstream.AuthenticateAsClient(this.host);
            StreamWriter sw = new StreamWriter(sslstream);
            System.IO.StreamReader reader = new StreamReader(sslstream);
            sw.WriteLine("USER " + email); sw.Flush();
            sw.WriteLine("PASS " + password); sw.Flush();
            for (int i = (messages-roznica)+1; i <= messages; i++) {
                sw.WriteLine("TOP "+i + " 0");
                sw.Flush();
            }
            sw.WriteLine("Quit ");
            sw.Flush();
            string str = string.Empty;
            string strTemp = string.Empty;
            while ((strTemp = reader.ReadLine()) != null)
            {
                if (".".Equals(strTemp))
                {
                    break;
                }
                if (strTemp.IndexOf("-ERR") != -1)
                {
                    break;
                }
                str += strTemp + "\r\n";
            }

            string ret = str;
            return ret;
        }
    }

}
