using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Timers;
using System.Net.Mail;
using System.Net;
namespace Pop3_Client
{
    public partial class Form1 : Form
    {
        Pop3 pop3c;
        System.Timers.Timer myTimer=new System.Timers.Timer();
        SmtpClient c = new SmtpClient();
        public Form1()
        {
            InitializeComponent();
            pop3c= new Pop3(995, "poczta.interia.pl");
            c.EnableSsl = true;
            c.Host = "smtp.gmail.com";
            c.Credentials = new NetworkCredential("jakis.mailzut@gmail.com","zutsieci");
            c.Port = 587;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            myTimer.Interval = 10000; // 1000 ms is one second
        }

       
        string[] lines;
        private void button2_Click(object sender, EventArgs e)
        {
            Regex r = new Regex("OK (?<mess>[0-9]+) (?<id>[0-9]+)");

            lines = Regex.Split(pop3c.login(textBox1.Text, textBox2.Text), "\r\n");
            foreach (string line in lines)
            {

                Match tcpline = r.Match(line);
                if (tcpline.Success)
                {
                    int messagesnumber = Convert.ToInt16(tcpline.Groups["mess"].Value);

                        oldmessagesno = messagesnumber;

                }
            }
            myTimer.Start();
            
        }

        int oldmessagesno = 0;
        public void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            Regex r = new Regex("OK (?<mess>[0-9]+) (?<id>[0-9]+)");

            lines = Regex.Split(pop3c.login(textBox1.Text, textBox2.Text), "\r\n");
            foreach (string line in lines)
            {

                Match tcpline = r.Match(line);
                if (tcpline.Success)
                {
                   int messagesnumber = Convert.ToInt16(tcpline.Groups["mess"].Value);
                    if (oldmessagesno < messagesnumber) {
                        int roznica = messagesnumber- oldmessagesno;
                        oldmessagesno = messagesnumber;
                       string[] newlines = Regex.Split(pop3c.checkHeaders(roznica, messagesnumber,textBox1.Text, textBox2.Text), "\r\n");
                        Regex r2 = new Regex("From: <(?<email>.*)>");
                        foreach (string newline in newlines)
                        {
                            Match mailline = r2.Match(newline);
                            if (mailline.Success)
                            { MessageBox.Show(mailline.Groups["email"].Value); }
                        }
                        }
                }
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] bytes = Encoding.Default.GetBytes("proba");
            string message = Encoding.UTF8.GetString(bytes);
            bytes = Encoding.Default.GetBytes("proba");
            string title = Encoding.UTF8.GetString(bytes);
            c.Send("jakis.mailzut@gmail.com", textBox1.Text, title, message);
        }
    }
}
