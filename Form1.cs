using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Net.Http;

// プロジェクトの読込方法
// https://itsakura.com/visualstudio-projectref


namespace MBSD
{
    public partial class Form1 : Form
    {
        private bool ONOFF = false;
        Nowloding nl;
        internal UserControl1 ctrl = null;

        public Form1()
        {
            InitializeComponent();
        }
     
        private void ONOFFbutton_Click(object sender, EventArgs e)
        {
            SQL_radioButton.Checked = ONOFF;
            XSS_radioButton.Checked = ONOFF;
            DT_radioButton.Checked = ONOFF;
            DR_radioButton.Checked = ONOFF;
            CSRF_radioButton.Checked = ONOFF;
            OSCI_radioButton.Checked = ONOFF;
            HTTPHI_radioButton.Checked = ONOFF;
            other_radioButton.Checked = ONOFF;
            SQL_OFF_radioButton.Checked = !ONOFF;
            XSS_OFF_radioButton.Checked = !ONOFF;
            DT_OFF_radioButton.Checked = !ONOFF;
            DR_OFF_radioButton.Checked = !ONOFF;
            CSRF_OFF_radioButton.Checked = !ONOFF;
            OSCI_OFF_radioButton.Checked = !ONOFF;
            HTTPHI_OFF_radioButton.Checked = !ONOFF;
            other_OFF_radioButton.Checked = !ONOFF;
            if (ONOFF == false)
            {
                this.ONOFF = true;
                ONOFFbutton.Text = "一括 ON";
            }
            else
            {
                this.ONOFF = false;
                ONOFFbutton.Text = "一括 OFF";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //1つのURLからいろんな攻撃を行うためtextBoxからURLのコードを取得する
            string URL_Path = textBox1.Text;
            if (URL_Path.Length > 7 && URL_Path.Substring(0, 7) == "http://")
            {
                warn.Text = "";
                bool sql = SQL_radioButton.Checked;
                bool xss = XSS_radioButton.Checked;
                bool csrf = CSRF_radioButton.Checked;
                bool dt = DT_radioButton.Checked;
                bool dr = DR_radioButton.Checked;
                bool os = OSCI_radioButton.Checked;


                nl = new Nowloding(this,ctrl,URL_Path, sql, xss,csrf, dt, dr,os);

                this.Controls.Add(nl);
                nl.Left = (this.ClientSize.Width - nl.Width) / 2;
                nl.BringToFront();
                nl.Visible = true;
                button1.Enabled = true;

            }
            else
            {
                warn.ForeColor = Color.Red;
                warn.Text = "正しくありません。http://から始まるURLを入力してください。";
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            ctrl?.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
