using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Mail;

namespace SMTP_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            button2.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        SmtpClient c = new SmtpClient();

        bool setted = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (setted == true) {
                button2.Enabled = true;
            }
            else button2.Enabled = false; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox3.Text != string.Empty && textBox2.Text != string.Empty  && textBox4.Text != string.Empty)
            {
                c.EnableSsl = true;
                c.Host = textBox4.Text;
                c.Credentials = new NetworkCredential(textBox1.Text, textBox3.Text);
                c.Port = Convert.ToInt32(textBox2.Text);
                setted = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox5.Text != string.Empty && textBox6.Text != string.Empty && richTextBox1.Text != string.Empty)
            {
                byte[] bytes = Encoding.Default.GetBytes(richTextBox1.Text);
                string message = Encoding.UTF8.GetString(bytes);
                bytes = Encoding.Default.GetBytes(textBox6.Text);
                string title = Encoding.UTF8.GetString(bytes);
                c.Send(textBox1.Text, textBox5.Text, title, message);
                setted = false;
            }
            }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
