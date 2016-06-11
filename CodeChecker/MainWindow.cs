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
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;


namespace CodeChecker
{
    public partial class MainWindow : Form
    {
        private string openedFile = "";
        private string fileExtension = "";
        private bool textEditorHidden = false;
        private int dx = 337;
        public MainWindow()
        {
            InitializeComponent();
        }
        public bool fileIsChoosen = false;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"D:\KPI\4_term\OOP";
            openFileDialog1.Filter = "All files (*.*) |*.*";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.Split('.').Length == 2)
                {
                    fileExtension = openFileDialog1.FileName.Split('.')[1].ToLower();
                    loadedCodeTextBox.Language = Language.Custom;
                    switch (fileExtension)
                    {
                        case "py":
                            extensionPict.Image = Properties.Resources.py;
                            break;
                        case "cpp":
                        case "h":
                            extensionPict.Image = Properties.Resources.cpp;
                            loadedCodeTextBox.Language = Language.CSharp;
                            break;
                        case "cs":
                            extensionPict.Image = Properties.Resources.cs;
                            loadedCodeTextBox.Language = Language.CSharp;
                            break;
                        case "java":
                            extensionPict.Image = Properties.Resources.java;
                            break;
                        default:
                            extensionPict.Image = Properties.Resources.txt;
                            loadedCodeTextBox.Language = Language.CSharp;
                            break;

                    }
                    loadedCodeTextBox.Refresh();
                    loadedCodeTextBox.Update();
                    extensionPict.Refresh();
                    extensionPict.Update();

                }
                try
                {
                    openedFile = openFileDialog1.FileName;
                    var splitSlashes = openedFile.Split('\\');
                    filenameLabel.Text = splitSlashes[splitSlashes.Length-1];
                    loadedCodeTextBox.Text = File.ReadAllText(openFileDialog1.FileName);
                    fileIsChoosen = true;
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
                    loadedCodeTextBox.Text = File.ReadAllText(openedFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : Could not read file from disk:	" + ex.Message);
                }

            }
        }
        public static string HttpPOST(string url, string querystring)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "text/x-c";
                request.Method = "POST";
                StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                //bool isStarted = false;
                try
                {

                    //isStarted = true;
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
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
         }
        private void button4_Click(object sender, EventArgs e)
        {
            if (!fileIsChoosen)
            {
                MessageBox.Show("Оберіть файл для перевірки!");
            }

            else if (checkingTypeSelector.Text.ToString() == "")
            {
                MessageBox.Show("Оберіть тип перевірки!");
            }

            else if (checkingTypeSelector.SelectedIndex == 1)
            {
                /*
                richTextBox1.SaveFile("1.txt", RichTextBoxStreamType.PlainText);
                string text = System.IO.File.ReadAllText("1.txt");
                 * */
                File.WriteAllText("1.txt", loadedCodeTextBox.Text);
                string text = File.ReadAllText("1.txt");
                //System.Console.WriteLine(HttpPOST("http://139.59.184.77/checkcpp/?action=comments&task=5", text));
                resultBox.Text = HttpPOST("http://139.59.184.77/checkcpp/?action=check&task=1", text);
                var spl = resultBox.Text.Split('\n');
                //MessageBox.Show(richTextBox2.Text[richTextBox2.Text.Length - 1].ToString());
                if (resultBox.Text[resultBox.Text.Length - 1] == '\n')
                {
                    correctnessPict.Image = Properties.Resources.red_sign_black_green_flat_icon_right_blue_mark;
                }
                else
                {
                    correctnessPict.Image = Properties.Resources.cross_mark_304374_960_720;
                }
                correctnessPict.Refresh();
                correctnessPict.Update();
            }
            else if (checkingTypeSelector.SelectedIndex == 3)
            {
                File.WriteAllText("1.txt", loadedCodeTextBox.Text);
                string text = File.ReadAllText("1.txt");
                //System.Console.WriteLine(HttpPOST("http://139.59.184.77/checkcpp/?action=comments&task=5", text));
                resultBox.Text = HttpPOST("http://139.59.184.77/checkcpp/?action=comments", text);

            }
            /*
            File.WriteAllText("1.txt", fastColoredTextBox1.Text);
            string text = File.ReadAllText("1.txt");
            richTextBox2.Text = HttpPOST("http://139.59.184.77/checkcpp/?action=compile", text); 
             * */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!textEditorHidden) 
            { 
                dx = Size.Width-687;
                this.Size = new Size(Size.Width - dx, Size.Height);
                textEditorHidden = true;
                hideButton.Text = "Розгорнути";
            }
            else
            { 
                this.Size = new Size(Size.Width+dx,Size.Height);
                textEditorHidden = false;
                hideButton.Text = "Згорнути";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            switch (fileExtension)
            {
                case "cpp":
                case "h":
                case "cs":
                case "java":
                    loadedCodeTextBox.Language = Language.CSharp;
                    break;
                default:
                    loadedCodeTextBox.Language = Language.Custom;
                    break;

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            if (Size.Width < 660)
                Size = new Size(660, Size.Height);
            if (Size.Height < 320)
                Size = new Size(Size.Width, 320);
            if (Size.Width > 687)
            {
                hideButton.Text = "Згорнути";
                textEditorHidden = false;
            }
            loadButton.Location = new Point(35, Size.Height - 178);
            updateButton.Location = new Point(249, Size.Height - 178);
            hideButton.Location = new Point(249, Size.Height - 116);
            checkCodeButton.Location = new Point(35, Size.Height - 116); 
            checkTypeLabel.Location = new Point(Size.Width-336, 12);
            checkingTypeSelector.Location = new Point(Size.Width-336, 31);
            resultBox.Location = new Point(Size.Width-336, 57);
            correctnessPict.Location = new Point(Size.Width-227, Size.Height-130);
            loadedCodeTextBox.Size = new Size(((Size.Width - 690) > 0) ? (Size.Width - 690) : (0), Size.Height - 81);
            filenameLabel.Size = new Size(((Size.Width - 690) > 0) ? (Size.Width - 690) : (0), 17);
            resultBox.Size = new Size(307, Size.Height - 193);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openedFile != "")
            {
                Regex currRegex;
                //bubble sort
                if ((currRegex = new Regex(@"for.*?\(.*?\S+.*?([a-z_][a-z0-9_]*).*?;.+?\k<1>.*?<.*;.*?(\+\+)?\k<1>(\+\+)?\)", RegexOptions.IgnoreCase)).IsMatch(loadedCodeTextBox.Text))
                {
                    Match firstCheck = currRegex.Match(loadedCodeTextBox.Text);
                    string i_var = firstCheck.Groups[1].ToString();
                    if ((currRegex = new Regex(@"for.*?\(.*?\S+.*?([a-z_][a-z0-9_]*).+?;.*?\k<1>.*?<.*?" + i_var + @".*?;.*?(\+\+)?\k<1>(\+\+)?\)", RegexOptions.IgnoreCase)).IsMatch(loadedCodeTextBox.Text))
                    {
                        Match secondCheck = currRegex.Match(loadedCodeTextBox.Text);
                        string j_var = secondCheck.Groups[1].ToString();
                        if ((currRegex = new Regex(@"if \(.*?" + j_var + @".*?\)", RegexOptions.IgnoreCase)).IsMatch(loadedCodeTextBox.Text))
                        {
                            MessageBox.Show("Simple bubble sort.");
                        }
                    }
                    else if ((currRegex = new Regex(@"for.*?\(\S+.*?([a-z_][a-z_0-9]*).*?\+1;.*?\k<1><.*?;.*?\k<1>\)", RegexOptions.IgnoreCase)).IsMatch(loadedCodeTextBox.Text))
                    {
                        Match secondCheck = currRegex.Match(loadedCodeTextBox.Text);
                        string j_var = secondCheck.Groups[1].ToString();
                        if ((currRegex = new Regex(@"if\S*\(.*?"+j_var+@".*?\)", RegexOptions.IgnoreCase)).IsMatch(loadedCodeTextBox.Text))
                        {
                            MessageBox.Show("Simple bubble sort.");
                        }
                    }

                }
                //hello world
                else if (new Regex(@"cout\s*<<.*?Hello.*?World", RegexOptions.IgnoreCase).IsMatch(loadedCodeTextBox.Text) ||
                         new Regex(@"printf\(.*?Hello.*?world.*?\)",RegexOptions.IgnoreCase).IsMatch(loadedCodeTextBox.Text))
                        MessageBox.Show("Simple \"Hello world\"");
            }
            else
                MessageBox.Show("File not selected.");
        }
    
    }
}
