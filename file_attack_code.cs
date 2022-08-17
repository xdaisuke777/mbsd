using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
namespace MBSD
{
    public class file_attack_code
    {
        private List<file_search> file_output;
        private List<file_attack_code_data> facd = new List<file_attack_code_data> { };

        private int No = 1;
        private List<string> DT_url_list = new List<string> { };
        private List<string> DR_url_list = new List<string> { };


        public file_attack_code(List<file_search> file_output)
        {
            facd.Clear();
            this.file_output = file_output;
        }

        public List<file_attack_code_data> getFacd()
        {
            return this.facd;
        }

        public void SQL()
        {
            foreach (var item in file_output)//インスタンスを一つ取り出す
            {
                if (item.getStatuscode() == 200)//取り出したインスタンスのHTTPステータスコードを調べ、ファイルが存在するか調べる
                {
                    //ex:  item.getAction()   [action1,action2,action3]
                    if (item.getAction().Count != 0 && item.getName().Count != 0)//item.getName()  ex:  [  [ name1,name2 ],[name3],[name4,name5,name6]  ]
                    {
                        for (int i = 0; i < Program.getAttack().Count / 6; i++)
                        {
                            if (Program.getAttack()[2 + i * 6] == "1")
                            {
                                for (int a = 0;a < item.getAction().Count;a++)
                                {
                                    List<string> key = new List<string> { };
                                    List<string> value = new List<string> { };
                                    string sqlurl = "";
                                    bool flg = true;
                                    bool flgSecond = true;
                                    for (int n = 0;n < item.getName()[a].Count;n++)
                                    {
                                        if (item.getName()[a][n] != "" && item.getName()[a][n] != null)
                                        {
                                            switch (item.getValue()[a][n].Length)// value1 → value2 ex:    "" → "hoge"
                                            {
                                                case int s when s == 0://valueがなかったら
                                                    if (flg)
                                                    {
                                                        sqlurl = "?" + sqlurl + item.getName()[a][n] + "=" + Program.getAttack()[0 + 6 * i];
                                                        key.Add(item.getName()[a][n]);
                                                        value.Add(Program.getAttack()[0 + 6 * i]);
                                                    }
                                                    else
                                                    {
                                                        if (flgSecond)
                                                        {
                                                            sqlurl = sqlurl + "&" + item.getName()[a][n] + "=" + Program.getAttack()[0 + 6 * i];
                                                            key.Add(item.getName()[a][n]);
                                                            value.Add(Program.getAttack()[1 + 6 * i]);
                                                        }
                                                        else
                                                        {
                                                            sqlurl = sqlurl + "&" + item.getName()[a][n] + "=" + Program.getAttack()[1 + 6 * i];
                                                            key.Add(item.getName()[a][n]);
                                                            value.Add(Program.getAttack()[1 + 6 * i]);
                                                        }

                                                    }
                                                    flg = false;
                                                    flgSecond = false;
                                                    break;

                                                case int s when s > 0://valueに文字が含まれていたら
                                                    if (flg)
                                                    {
                                                        sqlurl = "?" + sqlurl + item.getName()[a][n] + "=" + item.getValue()[a][n];
                                                        key.Add(item.getName()[a][n]);
                                                        value.Add(item.getValue()[a][n]);
                                                    }
                                                    else
                                                    {
                                                        sqlurl = sqlurl + "&" + item.getName()[a][n] + "=" + item.getValue()[a][n];
                                                        key.Add(item.getName()[a][n]);
                                                        value.Add(item.getValue()[a][n]);
                                                    }
                                                    flg = false;
                                                    break;
                                            }                                        
                                        }
                                    }
                                    if (sqlurl != "" && item.getAction()[a] != "")//sqlurlが空白でないなら
                                    {
                                        string action = item.getAction()[a];
                                        if (action != "/" && action != "./")
                                        {
                                            string url = item.migrate(action);
                                            if (url != "")
                                            {
                                                facd.Add(new file_attack_code_data(No,
                                                    url,
                                                    sqlurl, 
                                                    Program.getAttack()[2 + 6 * i],
                                                    Program.getAttack()[3 + 6 * i],
                                                    Program.getAttack()[4 + 6 * i],
                                                    Program.getAttack()[5 + 6 * i],
                                                    item.getMethod()[a],
                                                    key,
                                                    value,
                                                    item.getHtmlfile()));
                                                No++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void XSS()
        {
            foreach (var item in file_output)
            {
                if (item.getStatuscode() == 200)
                {
                    //ex:  item.getAction()   [action1,action2,action3]
                    if (item.getAction().Count != 0 && item.getName().Count != 0)//item.getName()  ex:  [  [ name1,name2 ],[name3],[name4,name5,name6]  ]
                    {
                        for (int i = 0; i < Program.getAttack().Count / 6; i++)
                        {
                            if (Program.getAttack()[2 + i * 6] == "2")
                            {
                                for (int a = 0; a < item.getAction().Count; a++)// a = 0～2
                                {
                                    List<string> key = new List<string> { };
                                    List<string> value = new List<string> { };
                                    string xssurl = "";
                                    bool flg = true;
                                    bool flgSecond = true;
                                    for (int n =0;n<item.getName()[a].Count;n++)//[name1,name2]　→　[name3] → [name4,name5,name6]
                                    {
                                        if (item.getName()[a][n] != "")//name1　→name2
                                        {
                                            switch (item.getValue()[a][n].Length)// value1 → value2 ex:    "" → "hoge"
                                            {
                                                case int s when s == 0://valueがなかったら
                                                    if (flg)
                                                    {
                                                        xssurl = xssurl + item.getName()[a][n] + "=" + Program.getAttack()[0 + i * 6];
                                                        key.Add(item.getName()[a][n]);
                                                        value.Add(Program.getAttack()[0 + 6 * i]);

                                                    }
                                                    else
                                                    {
                                                        if (flgSecond)
                                                        {
                                                            xssurl = xssurl + "&" + item.getName()[a][n] + "=" + Program.getAttack()[0 + i * 6];
                                                            key.Add(item.getName()[a][n]);
                                                            value.Add(Program.getAttack()[1 + 6 * i]);
                                                        }
                                                        else
                                                        {
                                                            xssurl = xssurl + "&" + item.getName()[a][n] + "=" + Program.getAttack()[1 + i * 6];
                                                            key.Add(item.getName()[a][n]);
                                                            value.Add(Program.getAttack()[1 + 6 * i]);
                                                        }
                                                    }
                                                    flg = false;
                                                    flgSecond = false;
                                                    break;

                                                case int s when s > 0://valueに文字が含まれていたらそのvalueを利用する。
                                                    if (flg)
                                                    {
                                                        xssurl = xssurl + item.getName()[a][n] + "=" + item.getValue()[a][n];
                                                        key.Add(item.getName()[a][n]);
                                                        value.Add(item.getValue()[a][n]);
                                                    }
                                                    else
                                                    {
                                                        xssurl = xssurl + "&" + item.getName()[a][n] + "=" + item.getValue()[a][n];
                                                        key.Add(item.getName()[a][n]);
                                                        value.Add(item.getValue()[a][n]);
                                                    }
                                                    flg = false;
                                                    break;
                                            }
                                        }
                                    }
                                    if (xssurl != "" && item.getAction()[a] != "")//組み合わせurlが空白でなく、actionがnullでないなら
                                    {
                                        string action = item.getAction()[a];
                                        if (action != "/" && action != "./")
                                        {
                                            string url = item.migrate(action);
                                            if (url != "")
                                            {
                                                facd.Add(new file_attack_code_data(No,
                                                    url,
                                                    "?" + xssurl,
                                                    Program.getAttack()[2 + 6 * i],
                                                    Program.getAttack()[3 + 6 * i],
                                                    Program.getAttack()[4 + 6 * i],
                                                    Program.getAttack()[5 + 6 * i],
                                                    item.getMethod()[a],
                                                    key,
                                                    value,
                                                    item.getHtmlfile()));
                                                No++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        public void CSRF()
        {
            List<string> test = new List<string> { "" };
            foreach (var item in file_output)
            {
                for (int i = 0;i < Program.getAttack().Count/6;i++)
                {
                    if (Program.getAttack()[2 + i * 6] == "3")
                    {
                        string url = item.getUrl();
                        try
                        {
                            facd.Add(new file_attack_code_data(No,
                                url,
                                Program.getAttack()[0 + 6 * i],
                                Program.getAttack()[2 + 6 * i],
                                Program.getAttack()[3 + 6 * i],
                                Program.getAttack()[4 + 6 * i],
                                Program.getAttack()[5 + 6 * i],
                                "GET",
                                test,
                                test,
                                item.getHtmlfile()));
                        }
                        catch
                        {
                            Console.WriteLine(item.getUrl());
                        }
                    }
                }
            }
        }


        public void DT()
        {
            List<string> query = new List<string> { };
            for (int i = 0; i < Program.getAttack().Count / 6; i++)
            {
                if (Program.getAttack()[2 + i * 6] == "6")
                {
                    query.Add(Program.getAttack()[0 + 6 * i]);
                    query.Add(Program.getAttack()[2 + 6 * i]);
                    query.Add(Program.getAttack()[3 + 6 * i]);
                    query.Add(Program.getAttack()[4 + 6 * i]);
                    query.Add(Program.getAttack()[5 + 6 * i]);
                }
            }
            foreach (var item in file_output)
            {
                if (item.getUrl().Substring(0, 7) == "http://" && item.url_slash_count(item.getUrl()) >= 3)//urlの始まりがhttpだったら
                {
                    string url = item.url_shave(item.getUrl());
                    try
                    {
                        dt_page_move(url, query);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message, "class file_attack_code DT", url);
                    }
                }
            }

        }
        public void DR()
        {
            List<string> query = new List<string> { };
            for (int i = 0; i < Program.getAttack().Count / 6; i++)
            {
                if (Program.getAttack()[2 + i * 6] == "5")
                {
                    query.Add(Program.getAttack()[0 + 6 * i]);
                    query.Add(Program.getAttack()[2 + 6 * i]);
                    query.Add(Program.getAttack()[3 + 6 * i]);
                    query.Add(Program.getAttack()[4 + 6 * i]);
                    query.Add(Program.getAttack()[5 + 6 * i]);
                }
            }
            foreach (var item in file_output)
            {
                if (item.getUrl().Substring(0, 7) == "http://" && item.url_slash_count(item.getUrl()) >= 3)
                {
                    string url = item.url_shave(item.getUrl());
                    try
                    {
                        dr_page_move(url, query);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message, "class file_attack_code DR", url);
                    }
                }
            }

        }
        public void OS()
        {
            foreach (var item in file_output)
            {
                if (item.getStatuscode() == 200)
                {
                    //ex:  item.getAction()   [action1,action2,action3]
                    if (item.getAction().Count != 0 && item.getName().Count != 0)//item.getName()  ex:  [  [ name1,name2 ],[name3],[name4,name5,name6]  ]
                    {
                        for (int i = 0; i < Program.getAttack().Count / 6; i++)
                        {
                            if (Program.getAttack()[2 + i * 6] == "4")
                            {
                                for (int a = 0; a < item.getAction().Count; a++)// a = 0～2
                                {
                                    List<string> key = new List<string> { };
                                    List<string> value = new List<string> { };
                                    string osurl = "";
                                    bool flg = true;
                                    bool flgSecond = true;
                                    for (int n = 0; n < item.getName()[a].Count; n++)//[name1,name2]　→　[name3] → [name4,name5,name6]
                                    {
                                        if (item.getName()[a][n] != "")//name1　→name2
                                        {
                                            switch (item.getValue()[a][n].Length)// value1 → value2 ex:    "" → "hoge"
                                            {
                                                case int s when s == 0://valueがなかったら
                                                    if (flg)
                                                    {
                                                        osurl = osurl + item.getName()[a][n] + "=" + Program.getAttack()[0 + i * 6];
                                                        key.Add(item.getName()[a][n]);
                                                        value.Add(Program.getAttack()[0 + 6 * i]);

                                                    }
                                                    else
                                                    {
                                                        if (flgSecond)
                                                        {
                                                            osurl = osurl + "&" + item.getName()[a][n] + "=" + Program.getAttack()[0 + i * 6];
                                                            key.Add(item.getName()[a][n]);
                                                            value.Add(Program.getAttack()[1 + 6 * i]);
                                                        }
                                                        else
                                                        {
                                                            osurl = osurl + "&" + item.getName()[a][n] + "=" + Program.getAttack()[1 + i * 6];
                                                            key.Add(item.getName()[a][n]);
                                                            value.Add(Program.getAttack()[1 + 6 * i]);
                                                        }
                                                    }
                                                    flg = false;
                                                    flgSecond = false;
                                                    break;

                                                case int s when s > 0://valueに文字が含まれていたらそのvalueを利用する。
                                                    if (flg)
                                                    {
                                                        osurl = osurl + item.getName()[a][n] + "=" + item.getValue()[a][n];
                                                        key.Add(item.getName()[a][n]);
                                                        value.Add(item.getValue()[a][n]);
                                                    }
                                                    else
                                                    {
                                                        osurl = osurl + "&" + item.getName()[a][n] + "=" + item.getValue()[a][n];
                                                        key.Add(item.getName()[a][n]);
                                                        value.Add(item.getValue()[a][n]);
                                                    }
                                                    flg = false;
                                                    break;
                                            }
                                        }
                                    }
                                    if (osurl != "" && item.getAction()[a] != "")//組み合わせurlが空白でなく、actionがnullでないなら
                                    {
                                        string action = item.getAction()[a];
                                        if (action != "/" && action != "./")
                                        {
                                            string url = item.migrate(action);
                                            if (url != "")
                                            {
                                                facd.Add(new file_attack_code_data(No,
                                                    url,
                                                    "?" + osurl,
                                                    Program.getAttack()[2 + 6 * i],
                                                    Program.getAttack()[3 + 6 * i],
                                                    Program.getAttack()[4 + 6 * i],
                                                    Program.getAttack()[5 + 6 * i],
                                                    item.getMethod()[a],
                                                    key,
                                                    value,
                                                    item.getHtmlfile()));

                                                No++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void HTTP()
        {
            ;
        }
        public void XSRF()
        {
            ;
        }
        public void dt_page_move(string url,List<string> query)
        { 
            file_search fs = new file_search(url);
            List<string> test = new List<string> {""};
            if (!DT_url_list.Contains(fs.getUrl()) && !DT_url_list.Contains(fs.getFile()))
            {
                for (int i = 0;i < query.Count/5;i++)
                {
                    facd.Add(new file_attack_code_data(No,
                        url,
                        query[0 + 5 * i],
                        query[1 + 5 * i],
                        query[2 + 5 * i],
                        query[3 + 5 * i],
                        query[4 + 5 * i],
                        "GET",
                        test,
                        test,
                        fs.getHtmlfile()));

                    No++;            
                }


                DT_url_list.Add(fs.getUrl());
                if (fs.getHtmlfile() != "" && fs.getHtmlfile() != null)
                {
                    DT_url_list.Add(fs.getHtmlfile());
                }

                string shave_url = fs.url_shave(fs.getUrl());//ex: http://192.168.33.10/MBSD/a/b/c/ →　http://192.168.33.10/MBSD/a/b/  

                if (fs.url_slash_count(shave_url) >= 3)
                {
                    dt_page_move(shave_url, query);
                }
                //下の階層があれば
                if (fs.getPage_down().Count != 0)//ページ表示ができれば
                {
                    foreach (var item in fs.getPage_down())
                    {
                        if (item != "" && item.Substring(0, 1) != "/")
                        {
                            string down_url = url + item;
                            dt_page_move(down_url, query);
                        }
                    }
                }
            }
        }


        public void dr_page_move(string url,List<string> query)
        {
            file_search fs = new file_search(url);
            List<string> test = new List<string> { "" };
            if (DR_url_list.Contains(fs.getUrl()) || DR_url_list.Contains(fs.getFile()))
            {
                return;
            }
            for (int i = 0; i < query.Count / 5; i++)
            {
                facd.Add(new file_attack_code_data(No,
                    url,
                    query[0 + 5 * i],
                    query[1 + 5 * i],
                    query[2 + 5 * i],
                    query[3 + 5 * i],
                    query[4 + 5 * i],
                    "GET",
                    test,
                    test,
                    fs.getHtmlfile()));

                No++;
            }

            DR_url_list.Add(fs.getUrl());
            if (fs.getHtmlfile() != null)
            {
                DR_url_list.Add(fs.getHtmlfile());
            }

            string shave_url = fs.url_shave(url);

            if (fs.url_slash_count(shave_url) >= 3)
            {
                dr_page_move(shave_url,query);
            }

            if (fs.getPage_down().Count != 0)//ページ表示ができれば
            {
                foreach (var item in fs.getPage_down())
                {
                    if (item != "" && item.Substring(0, 1) != "/")
                    {
                        string down_url = url + item;
                        dr_page_move(down_url,query);
                    }
                }
            }
        }
    }
    public class file_attack_code_data
    {
        private int No;
        private string before_url;
        private string query_parameter;
        private string vulnerability;
        private string weight;
        private string targetcode;
        private string kind;
        private string method;
        private List<string> name = new List<string> { };
        private List<string> value = new List<string> { };
        private string before_htmlfile;

        public file_attack_code_data(int No,string before_url, string query_parameter,string vulnerability,string weight,string targetcode,string kind, string method, List<string> name, List<string> value,string before_htmlfile)
        {
            this.No = No;
            this.before_url = before_url;
            this.query_parameter = query_parameter;
            this.vulnerability = vulnerability;
            this.weight = weight;
            this.targetcode = targetcode;
            this.kind = kind;
            this.method = method;
            this.name = name;
            this.value = value;
            this.before_htmlfile = before_htmlfile;
        }

        public int getNo()
        {
            return this.No;
        }
        public string getBefore_url()
        {
            return this.before_url;
        }

        public string getQuery_parameter()
        {
            return this.query_parameter;
        }

        public string getVulnerability()
        {
            return this.vulnerability;
        }
        public string getWeight()
        {
            return this.weight;
        }

        public string getTargetcode()
        {
            return this.targetcode;
        }

        public string getKind()
        {
            return this.kind;
        }

        public string getMethod()
        {
            return this.method;
        }
        public List<string> getName()
        {
            return this.name;
        }
        public List<string> getValue()
        {
            return this.value;
        }
        public string getBefore_htmlfile()
        {
            return this.before_htmlfile;
        }

    }
}
