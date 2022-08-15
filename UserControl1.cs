using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBSD
{
    public partial class UserControl1 : UserControl
    {
        List<string> lists = new List<string> { };
        public int pageSize = 1000;
        public int dataCount = 0;

        private List<ConfigInfo> listCI;
        private Stopwatch timer;
        public UserControl1(Form1 form, List<ConfigInfo> listCI,Stopwatch timer)
        {
            InitializeComponent();
            this.listCI = listCI;
            form.ctrl = this;
            this.timer = timer;
        }


        public class Data
        {
            internal int no;
            internal string url;
            internal string method;
            internal int code;
            internal string name;
            internal int vulnerability;
            internal int weight;
            internal string res;

            public Data(int no, string url, string method, int code, string name, int vulnerability, int weight,string res)
            {
                this.no = no;
                this.url = url;
                this.method = method;
                this.code = code;
                this.name = name;
                this.vulnerability = vulnerability;
                this.weight = weight;
                this.res = res;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Data> l = new List<Data>();
            int No = 1;
            foreach(var item in listCI)
            {

                int no = No;
                string url;
                if (item.getMethod() == "GET")
                {
                    url = item.getParamtukiURL();
                }
                else
                {
                    url = item.getUrl_Key_Value();
                }
                string method = item.getMethod();
                int code = item.getRequestURLAfterStatuCode();
                string name = item.afterStatusname();
                int vulnerability = item.getAttackType();
                int weight;
                if (item.getSecurityFlag())
                {
                    weight = item.getOmomi();
                }
                else
                {
                    weight = 0;
                }
                
                string res = item.getRequestURLAfterSroce();

                l.Add(new Data(no, url, method, code, name, vulnerability, weight,res));
                No++;
            }
            var count = l.Count();

            // カラム数を指定
            dataGridView1.ColumnCount = 9;

            // カラム名を指定
            dataGridView1.Columns[0].HeaderText = "No";
            dataGridView1.Columns[1].HeaderText = "URL";
            dataGridView1.Columns[2].HeaderText = "METHOD";
            dataGridView1.Columns[3].HeaderText = "Status code";
            dataGridView1.Columns[4].HeaderText = "Status name";
            dataGridView1.Columns[5].HeaderText = "検知した脆弱性";
            dataGridView1.Columns[6].HeaderText = "Weight";

            {
                DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                iconColumn.Name = "result";
                dataGridView1.Columns.Insert(7, iconColumn);
            }
            int safety = 0;
            int little_danger = 0;
            int danger = 0;
            int very_danger = 0;

            int a = 0;
            foreach (Data item in l)
            {
                var i = dataGridView1.Rows.Add(item.no, item.url, item.method, item.code, item.name, item.vulnerability, item.weight);
                //,
                //item.weight);

                switch (item.weight)
                {
                    case 0:
                        dataGridView1["result", i].Value = global::MBSD.Properties.Resources.safety;
                        safety++;
                        break;
                    case 1:
                        dataGridView1["result", i].Value = global::MBSD.Properties.Resources.little_danger;
                        little_danger++;
                        break;
                    case 10:
                        dataGridView1["result", i].Value = global::MBSD.Properties.Resources.danger;
                        danger++;
                        break;
                    default:
                        dataGridView1["result", i].Value = global::MBSD.Properties.Resources.very_danger;
                        very_danger++;
                        break;

                }
                switch (item.vulnerability)
                {
                    case 1:
                        dataGridView1[5, i].Value = "SQLインジェクション";
                        break;
                    case 2:
                        dataGridView1[5, i].Value = "クロスサイトスクリプティング";
                        break;
                    case 3:
                        dataGridView1[5, i].Value = "クロスサイト・リクエスト・フォージェリ";
                        break;
                    case 4:
                        dataGridView1[5, i].Value = "OSコマンドインジェクション";
                        break;
                    case 5:
                        dataGridView1[5, i].Value = "ディレクトリ・リスティング";
                        break;
                    case 6:
                        dataGridView1[5, i].Value = "ディレクトリトラバーサル";
                        break;
                    case 7:
                        dataGridView1[5, i].Value = "意図しないリダイレクト";
                        break;
                    case 8:
                        dataGridView1[5, i].Value = "HTTPヘッダインジェクション";
                        break;

                }
                a++;

                if (a >= pageSize)
                {
                    break;
                }
            }
            timerLabel.Text = string.Format("計測時間：{0:0.000}秒", timer.Elapsed.TotalSeconds);
            AllNumber.Text = string.Format("全 {0} 件", count);
            safetylabel.Text = string.Format("{0} 件", safety);
            little_dangerlabel.Text = string.Format("{0} 件", little_danger);
            dangerlabel.Text = string.Format("{0} 件", danger);
            very_dangerlabel.Text = string.Format("{0} 件", very_danger);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //@"C:\Users\admin7.DESKTOP-2GRCJ92\Desktop\test.csv
                // 保存用のファイルを開く
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName, false, Encoding.GetEncoding("shift_jis")))
                {

                    int rowCount = dataGridView1.Rows.Count;

                    // ユーザによる行追加が許可されている場合は、最後の新規入力用の
                    // 1行分を差し引く
                    if (dataGridView1.AllowUserToAddRows == true)
                    {
                        rowCount = rowCount - 1;
                    }

                    // 行
                    for (int i = 0; i < rowCount; i++)
                    {
                        // リストの初期化
                        List<String> strList = new List<String>();

                        // 列
                        for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            strList.Add(dataGridView1[j, i].Value.ToString());
                        }
                        String[] strArray = strList.ToArray(); // 配列へ変換

                        // CSV 形式に変換
                        String strCsvData = String.Join(",", strArray);

                        writer.WriteLine(strCsvData);
                    }

                }

            }
        }



        private void button2_Click(object sender, EventArgs e)
        {

            this.Visible = false;

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void little_dangerlabel_Click(object sender, EventArgs e)
        {

        }
    }
}
