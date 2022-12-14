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
        private Stopwatch timer;
        public UserControl1(Form1 form, List<ConfigInfo> listCI,Stopwatch timer)
        {
            InitializeComponent();

            form.ctrl = this;
            this.timer = timer;

            for (var i = 0; i < listCI.Count; i++)
            {
                string line = listCI[i].makeFormat();
                // 読み込んだ一行をカンマ毎に分けて配列に格納する
                string[] values = line.Split(',');

                // 配列からリストに格納する                        
                lists.AddRange(values);
            }

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

            public Data(int no, string url, string method, int code, string name, int vulnerability, int weight)
            {
                this.no = no;
                this.url = url;
                this.method = method;
                this.code = code;
                this.name = name;
                this.vulnerability = vulnerability;
                this.weight = weight;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine(lists);

            List<Data> l = new List<Data>();

            for (int i = 0; i < lists.Count / 7; i++)
            {
                int no = int.Parse(lists[0 + i * 7]);
                string url = lists[1 + i * 7];
                string method = lists[2 + i * 7];
                int code = int.Parse(lists[3 + i * 7]);
                string name = lists[4 + i * 7];
                int vulnerability = int.Parse(lists[5 + i * 7]);
                int weight = int.Parse(lists[6 + i * 7]);

                l.Add(new Data(no, url, method, code, name, vulnerability, weight));
            }
            var count = l.Count();

            // カラム数を指定
            dataGridView1.ColumnCount = 8;

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
                            Console.WriteLine(dataGridView1[j, i].Value);
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
