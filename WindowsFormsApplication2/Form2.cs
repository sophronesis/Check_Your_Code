using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;


namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        private string openedFile = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"D:\KPI\4_term\OOP\ConsoleApplication1\ConsoleApplication1\bin\debug";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*) |*.*";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openedFile = openFileDialog1.FileName;
                    richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                    pictureBox1.Image = Properties.Resources.images;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : Could not read file from disk:	" + ex.Message);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openedFile != "")
            {
                try
                {
                    richTextBox1.LoadFile(openedFile, RichTextBoxStreamType.PlainText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : Could not read file from disk:	" + ex.Message);
                }

            }
        }
        public static string HttpPOST(string url, string querystring)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "text/x-c";
            request.Method = "POST";
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
            try
            {
                requestWriter.Write(querystring);
            }
            catch
            {
                throw;
            }
            finally
            {
                requestWriter.Close();
                requestWriter = null;
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string text = System.IO.File.ReadAllText(openedFile);
            richTextBox2.Text = HttpPOST("http://139.59.184.77/checkcpp/", text);
        }
    }
}
