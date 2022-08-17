using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBSD
{

    public partial class Nowloding : UserControl
    {     
        Bitmap bitmap;
        
        const int num2 = 16;
        int[] screen = new int[num2] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] subscreen = new int[num2] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        bool isSecondPush = false;
        bool isInvaidEvent = false;
        int pushIndex;
        int prevPushIndex;

        bool MouthDown = true;
        bool MouthUp = true;

        //サイズ
        const int length = 90;
        //座標
        int x = 600;
        int y = 100;

        private void Nowloding_Paint(object sender, PaintEventArgs e)
        {
            var destinationRectangles = new Rectangle[num2];
            for (int i = 0; i < num2; ++i)
            {
                destinationRectangles[i] = new Rectangle(x + (i % 4) * length, y + (i / 4) * length, length, length);
            }
            var sourceRectangle = new Rectangle(0, 0, length, length);

            for (int i = 0; i < num2; ++i)
            {
                switch (screen[i])
                {
                    case 0:
                        bitmap = Properties.Resources.p0;
                        break;
                    case 1:
                        bitmap = Properties.Resources.p1;
                        break;
                    case 2:
                        bitmap = Properties.Resources.p2;
                        break;
                    case 3:
                        bitmap = Properties.Resources.p3;
                        break;
                    case 4:
                        bitmap = Properties.Resources.p4;
                        break;
                    case 5:
                        bitmap = Properties.Resources.p5;
                        break;
                    case 6:
                        bitmap = Properties.Resources.p6;
                        break;
                    case 7:
                        bitmap = Properties.Resources.p7;
                        break;
                    case 8:
                        bitmap = Properties.Resources.p8;
                        break;
                }
                e.Graphics.DrawImage(bitmap, destinationRectangles[i], sourceRectangle, GraphicsUnit.Point);
            }
        }

        private void Nowloding_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouthDown)
            {
                if (y < e.Location.Y && e.Location.Y < length * 4 + y && x < e.Location.X && e.Location.X < length * 4 + x)
                {
                    pushIndex = ((e.Location.Y - y) / length) * 4 + ((e.Location.X - x) / length);
                    if (screen[pushIndex] != 0)
                    {
                        isInvaidEvent = true;
                        return;
                    }
                    screen[pushIndex] = subscreen[pushIndex];
                    Invalidate();
                }
            }

        }


        private async void Nowloding_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouthUp)
            {
                if (pushIndex < 16)
                {
                    if (isInvaidEvent)
                    {
                        isInvaidEvent = false;
                        return;
                    }
                    var hit = (screen[pushIndex] == screen[prevPushIndex]);
                    MouthDown = false;
                    MouthUp = false;
                    if (isSecondPush && !hit)
                    {
                        await Task.Delay(1000);
                        screen[pushIndex] = 0;
                        screen[prevPushIndex] = 0;
                    }

                    Invalidate();
                    isSecondPush = !isSecondPush;
                    prevPushIndex = pushIndex;
                    MouthDown = true;
                    MouthUp = true;
                }
            }
        }
        private void RandomLayout()
        {
            var random = new Random();
            var lots = new List<int>() { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
            int i = 0;
            while (lots.Any())
            {
                var n = random.Next(lots.Count);
                subscreen[i] = lots[n];
                lots.RemoveAt(n);
                ++i;
            }
        }








        private List<file_search> file_output = new List<file_search> { };
        private List<string> url_list = new List<string> { };

        List<ConfigInfo> list_insCI;
        UserControl1 ctrl;
        private string URL_Path;
        private bool sql;
        private bool xss;
        private bool csrf;
        private bool os;
        private bool dt;
        private bool dr;
        private Form1 form;

        private Stopwatch timer;

        public Nowloding(Form1 form, UserControl1 ctrl,string URL_Path ,bool sql,bool xss,bool csrf, bool dt, bool dr,bool os)
        {
            this.URL_Path = URL_Path;
            this.sql = sql;
            this.xss = xss;
            this.csrf = csrf;
            this.dt = dt;       
            this.dr = dr;
            this.os = os;
            this.form = form;
            this.ctrl = ctrl;     

            InitializeComponent();

            RandomLayout();

            backgroundWorker1.RunWorkerAsync();
        }







        public void filecheak(string url)
        {
            if (!url_list.Contains(url) && url != "")
            {
                file_search fs = new file_search(url);//actionやnameやidなどの情報を持ったインスタンスを作る
                if (fs.getStatuscode() == 200)
                {
                    url_list.Add(url);
                    file_output.Add(fs);//インスタンスをリストに追加していく
                    href_check(fs);
                }
            }

        }
     
        public void href_check(file_search fs)
        {
            foreach (var item in fs.getHref())
            {
                string href = item;
                if (href != "" && href != "/" && href != "./")//中身が入っていたら
                {
                    string test = fs.migrate(href);
                    filecheak(test);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctrl = new UserControl1(form, list_insCI,timer);
            form.Controls.Add(ctrl);
            ctrl.Left = (form.ClientSize.Width - ctrl.Width) / 2;
            ctrl.BringToFront();
            ctrl.Visible = true;
            this.Hide();
        }



        //スレッドの処理
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Properties.Resources.namakemono2;

            timer = new Stopwatch();
            timer.Start();

            file_output.Clear();
            url_list.Clear();

            filecheak(URL_Path);
            backgroundWorker1.ReportProgress(10);

            file_attack_code fac = new file_attack_code(file_output);

            if (sql)//SQLのボタン
            {
                fac.SQL();
            }
            backgroundWorker1.ReportProgress(20);
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            if (xss)//XSSのボタン
            {
                fac.XSS();
            }
            backgroundWorker1.ReportProgress(30);
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            if (csrf)//CSRF
            {
                fac.CSRF();
            }
            backgroundWorker1.ReportProgress(40);
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            if (dt)//ディレクトリトラバーサルボタン
            {
                fac.DT();
            }
            backgroundWorker1.ReportProgress(50);
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            if (dr)//ディレクトリリスティング
            {
                fac.DR();
            }
            backgroundWorker1.ReportProgress(60);
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            if (os)//OSコマンドインジェクション
            {
                fac.OS();
            }
            backgroundWorker1.ReportProgress(70);
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            //if (OSCI_radioButton.Checked)
            //{

            //}

            //if (HTTPHI_radioButton.Checked)
            //{

            //}

            //if (other_radioButton.Checked)
            //{

            //}

            FileUtil insFU = new FileUtil(fac);

            list_insCI = insFU.setReadCsvFile();



            backgroundWorker1.ReportProgress(80);
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            // 脆弱性の検知
            SecurityCheck insSC = new SecurityCheck();
            for (var i = 0; i < list_insCI.Count; i++)
            {
                insSC.attack_common(list_insCI[i]);
            }      
            backgroundWorker1.ReportProgress(100);

            timer.Stop();
            pictureBox1.Image = Properties.Resources.namakemono3;
        }

        //進捗度
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        //完了イベント
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Text = "準備完了";
            button1.Enabled = true;
            if (e.Cancelled)
            {
                MessageBox.Show("キャンセルされました");
                this.Hide();
            }
            else
            {
                MessageBox.Show("処理が完了しました");
                cancel.Enabled = false;
            }
            
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
    }
}
