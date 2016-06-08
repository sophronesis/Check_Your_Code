using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeChecker
{
    public partial class Title : Form
    {
        private Label startLabel;
        private PictureBox smallFlyPict;
    
        public Title()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.CCode_7;
            //System.Threading.Thread.Sleep(5500);
            this.BackgroundImage = Properties.Resources.CCode_6;
            //System.Threading.Thread.Sleep(1500);
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ////System.Threading.Thread.Sleep(11500);
            //Form2 f2 = new Form2();
            //f2.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainWindow f2 = new MainWindow();
            f2.Show();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Title));
            this.startLabel = new System.Windows.Forms.Label();
            this.smallFlyPict = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.smallFlyPict)).BeginInit();
            this.SuspendLayout();
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(679, 414);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(36, 13);
            this.startLabel.TabIndex = 3;
            this.startLabel.Text = "Старт";
            // 
            // smallFlyPict
            // 
            this.smallFlyPict.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.smallFlyPict.Image = ((System.Drawing.Image)(resources.GetObject("smallFlyPict.Image")));
            this.smallFlyPict.Location = new System.Drawing.Point(667, 345);
            this.smallFlyPict.Name = "smallFlyPict";
            this.smallFlyPict.Size = new System.Drawing.Size(59, 61);
            this.smallFlyPict.TabIndex = 2;
            this.smallFlyPict.TabStop = false;
            this.smallFlyPict.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // Title
            // 
            this.BackgroundImage = global::CodeChecker.Properties.Resources.CCode_6;
            this.ClientSize = new System.Drawing.Size(757, 444);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.smallFlyPict);
            this.Name = "Title";
            this.Text = "Check your Code";
            this.Load += new System.EventHandler(this.Title_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smallFlyPict)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            MainWindow f2 = new MainWindow();
            f2.Show();
        }

        private void Title_Load(object sender, EventArgs e)
        {
            //new System.Media.SoundPlayer(@"D:\Downloads\Fly-Sound.wav").Play();
        }
    }
}
