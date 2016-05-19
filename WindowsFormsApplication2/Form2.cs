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
        private bool textEditorHidden = false;
        private int dx = 337;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"D:\KPI\4_term\OOP\WindowsFormsApplication2\WindowsFormsApplication2";
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
            richTextBox1.SaveFile("1.txt", RichTextBoxStreamType.PlainText);
            string text = System.IO.File.ReadAllText("1.txt");
            richTextBox2.Text = HttpPOST("http://139.59.184.77/checkcpp/", text);
            //File.Delete("1.cpp");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!textEditorHidden) 
            {
                richTextBox1.Hide();
                label2.Hide();
                richTextBox2.Location = new Point(richTextBox2.Location.X - dx, richTextBox2.Location.Y);
                button4.Location = new Point(button4.Location.X - dx, button4.Location.Y);
                label1.Location = new Point(label1.Location.X - dx, label1.Location.Y);
                comboBox1.Location = new Point(comboBox1.Location.X - dx, comboBox1.Location.Y);
                this.FindForm().Size = new Size(this.FindForm().Size.Width - dx,this.FindForm().Size.Height);
                textEditorHidden = true;
            }
            else
            {
                richTextBox1.Show();
                label2.Show();
                richTextBox2.Location = new Point(richTextBox2.Location.X + dx, richTextBox2.Location.Y);
                button4.Location = new Point(button4.Location.X + dx, button4.Location.Y);
                label1.Location = new Point(label1.Location.X + dx, label1.Location.Y);
                comboBox1.Location = new Point(comboBox1.Location.X + dx, comboBox1.Location.Y);
                this.FindForm().Size = new Size(this.FindForm().Size.Width + dx, this.FindForm().Size.Height);
                textEditorHidden = false;

            }
        }
    }
}
