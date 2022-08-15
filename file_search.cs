using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MBSD
{
    public class file_search
    {
        private string url;//URL  ex:http://000.00.00.000/aaabbbccc.php
        private string domein_url;
        private string directory_url;


        private int statuscode;//HTTPステータスコード ex: 404
        private string statusDescription;//HTTPステータスコードの内容 ex:Not Found
        private string htmlfile;//URLで表示させたfileの内容
        private string file;//URLで表示させたfileの内容(Lower)       
        private int l;//表示したファイルの文字数を取得
        private string urlfile;  //ex: aaabbbccc.php

        //fileが存在すればより精密に内容を取得
        private List<string> action = new List<string> { };
        private List<string> method = new List<string> { };
        private List<List<string>> name = new List<List<string>> { };
        private List<List<string>> value = new List<List<string>> { };
        private List<string> href = new List<string> { };

        //ディレトラ、ﾃﾞｨレリス用
        private List<string> page_down_href = new List<string> { };
        private string page_up_href;

        private List<string> kick = new List<string> { ".jpg", ".gif", ".png", ".tif", ".bmp", ".pdf",".mp4",".mp3" };

        /// <summary>
        /// https://blog.okazuki.jp/entry/20121225/HttpClientTips
        /// </summary>
        /// <param name="url"></param>
        public file_search(string url)
        {
            try
            {
                var client = new HttpClient();
                this.url = url;
                (this.domein_url,this.directory_url) = setUrl(url);

                var uri = new Uri(url);
                var r = client.GetAsync(uri).Result;
                statuscode = (int)r.StatusCode;
                statusDescription = r.ReasonPhrase;
                urlfile = r.RequestMessage.RequestUri.Segments.Last();

                Task<string> r2 = client.GetStringAsync(url);
                htmlfile = comment_out_delete(r2.Result);
                file = htmlfile.ToLower();

                l = file.Length;

                action = Hit_action();
                method = Hit_method();
                (name,value) = Hit_name_and_value();
                href = Hit_href();
                page_down_href = Hit_page_down();
                page_up_href = Hit_page_up();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message,"file_search");
                Console.WriteLine(this.url);
            }
        }

        public string getUrl()
        {
            return this.url;
        }

        public string getDomein()
        {
            return this.domein_url;
        }

        public string getDirectory_url()
        {
            return this.directory_url;
        }

        public int getStatuscode()
        {
            return this.statuscode;
        }

        public string getStatusDescription()
        {
            return this.statusDescription;
        }

        public List<string> getAction()
        {
            return this.action;
        }

        public List<string> getMethod()
        {
            return this.method;
        }

        public List<List<string>> getName()
        {
            return this.name;
        }

        public List<List<string>> getValue()
        {
            return this.value;
        }

        public List<string> getHref()
        {
            return this.href;
        }

        public string getUrlfile()
        {
            return this.urlfile;
        }

        public string getHtmlfile()
        {
            return this.htmlfile;
        }

        public string getFile()
        {
            return this.file;
        }

        public List<string> getPage_down()
        {
            return this.page_down_href;
        }

        public string getPage_up()
        {
            return this.page_up_href;
        }

        public (string domein,string directory) setUrl(string url)
        {
            int count = 0;
            for (int i = 0; i < url.Length; i++)
            {
                if (url.Substring(i, 1) == "/")
                {
                    count++;
                    if (count == 3)
                    {
                        return (url.Substring(0, i + 1), url.Substring(i + 1, url.Length - i - 1));
                    }
                }
            }
            return ("", "");
        }



        public List<string> Hit_action()
        {
            //空のリストを作成
            List<string> action_list = new List<string> { };
            for (int i = 0; i < l - 11; i++)
            {
                if ("<form " == file.Substring(i, 6))
                {
                    string action = "";
                    while ("</form>" != file.Substring(i, 7))
                    {
                        if (" action=" == file.Substring(i, 8) || " action =" == file.Substring(i, 9) || " onsubmit=" == file.Substring(i, 10) || " onsubmit =" == file.Substring(i,11))
                        {
                            (action,i) = sourceCatch(i);
                            if (action == "/" || action == "#" || action == "")
                            {
                                action = urlfile;
                            }
                        }
                        i++;
                    }
                    if (action == "")
                    {
                        action_list.Add(urlfile);
                    }
                    else
                    {
                        if (!kick.Contains(action.Substring(action.Length - 4,4)) && !action.Contains("@"))
                        {
                            action_list.Add(action);
                        }             
                    }
                }
            }
            return action_list;
        }


        public List<string> Hit_method()
        {
            //空のリストを作成
            List<string> method_list = new List<string> { };
            for (int i = 0; i < l - 9; i++)
            {
                if ("<form " == file.Substring(i, 6))
                {
                    string method = "";
                    while ("</form>" != file.Substring(i, 7))
                    {
                        if (" method=" == file.Substring(i, 8) || " method =" == file.Substring(i,9))
                        {                  
                            (method,i) = sourceCatch(i);
                            if (method == "/" || method == "#" || method == "")
                            {
                                method_list.Add("GET");
                            }
                            else
                            {
                                method_list.Add(method.ToUpper());
                            }
                        }
                        i++;
                    }
                    if (method == "")
                    {
                        method_list.Add("GET");
                    }
                }
            }
            return method_list;
        }




        public (List<List<string>> name , List<List<string>>value) Hit_name_and_value()
        {
            List<string> name_list1 = new List<string> { };
            List<List<string>> name_list2 = new List<List<string>> { };
            List<string> value_list1 = new List<string> { };
            List<List<string>> value_list2 = new List<List<string>> { };
            for (int i = 0; i < l - 9; i++)
            {
                if ("<form " == file.Substring(i, 6))
                {
                    int start = i;
                    while ("</form>" != file.Substring(i, 7))
                    {
                        if ("<input " == file.Substring(i, 7) || "<button " == file.Substring(i, 8))
                        {
                            string type = "";
                            string name = "";
                            string value = "";
                            while (">" != file.Substring(i, 1))
                            {
                                if ("type=" == file.Substring(i, 5) || "type =" == file.Substring(i, 6))//typeの属性を取得する
                                {
                                    (type, i) = sourceCatch(i);
                                    type = type.ToLower();//小文字に統一させる
                                }
                                if (" name=" == file.Substring(i, 6) || " name =" == file.Substring(i, 7))//nameの値を取得する
                                {
                                    (name,i) = sourceCatch(i);                                  
                                }
                                if ("value=" == file.Substring(i, 6) || "value =" == file.Substring(i, 7))//valueの値を取得する
                                {
                                    (value, i) = sourceCatch(i);
                                }
                                i++;
                            }
                            if (!name_list1.Contains(name) && name != "")//被っているnameは取得しない 
                            {
                                name_list1.Add(name);
                                if ("hidden" == type || "submit" == type)
                                {
                                    value_list1.Add(value);
                                }
                                else
                                {
                                    value_list1.Add("");
                                }
                            }
                        }
                        if ("<select " == file.Substring(i, 8))
                        {
                            string name = "";
                            string value = "";
                            while ("</select>" != file.Substring(i, 9))
                            {
                                if (" name=" == file.Substring(i, 6) || " name =" == file.Substring(i, 7))//nameの値を取得する
                                {
                                    (name, i) = sourceCatch(i);
                                }
                                if ("<option" == file.Substring(i,7))
                                {
                                    while (">" != file.Substring(i,1))
                                    {
                                        if ("value=" == file.Substring(i, 6) || "value =" == file.Substring(i, 7))//valueの値を取得する
                                        {
                                            (value, i) = sourceCatch(i);
                                        }
                                        i++;
                                    }
                                    break;
                                }
                                i++;
                            }
                            if (!name_list1.Contains(name) && name != "")
                            {
                                name_list1.Add(name);
                                value_list1.Add(value);
                            }
                        }
                        if (i == l - 9)
                        {
                            i = start;
                            break;
                        }
                        i++;
                    }
                    name_list2.Add(name_list1);
                    name_list1 = new List<string> { };
                    value_list2.Add(value_list1);
                    value_list1 = new List<string> { };
                }
            }
            return (name_list2,value_list2);
        }
        public List<string> Hit_href()
        {         
            //空のリストを作成
            List<string> href_list = new List<string> { };
            for (int i = 0; i < l - 9; i++)
            {
                if ("<script " == file.Substring(i, 8))
                {
                    int start = i;
                    while ("</script>" != file.Substring(i, 9))
                    {
                        i++;
                        if (i == l - 9)
                        {
                            i = start;
                            break;
                        }
                    }
                }
                if ("<a " == file.Substring(i, 3))
                {
                    string href = "";
                    while ("</a>" != file.Substring(i, 4) && "<a>" != file.Substring(i, 3))
                    {
                        if ("href=" == file.Substring(i, 5) || "href =" == file.Substring(i, 6))
                        {
                            (href,i) = sourceCatch(i);                      
                        }
                        i++;
                    }
                    if (href != "" && !kick.Contains(href.Substring(href.Length - 4,4)) && !href.Contains("@"))
                    {
                        href_list.Add(href);
                    }
                }
            }     
            return href_list;
        }

        public List<string> Hit_page_down()
        {
            List<string> down_list = new List<string> { };
            for (int i = 0; i < l - 8; i++)
            {
                if ("<tr><td " == file.Substring(i, 8))
                {
                    while ("</td></tr>" != file.Substring(i, 10))
                    {          
                        if ("alt=" == file.Substring(i, 4) || "alt =" == file.Substring(i, 5))
                        {
                            string alt = "";
                            (alt,i) = sourceCatch(i);

                            if ("[DIR]" == alt)
                            {
                                while (true)
                                {
                                    if ("<a " == file.Substring(i, 3))
                                    {
                                        string href = "";
                                        while ("</a>" != file.Substring(i, 4))
                                        {

                                            if ("href=" == file.Substring(i, 5) || "href =" == file.Substring(i, 6))
                                            {
                                                (href,i) = sourceCatch(i);
                                            }
                                            i++;
                                        }
                                        if (href != "" && href.Substring(0,1) != "/")
                                        {
                                            down_list.Add(href);
                                        }
                                        break;
                                    }
                                    i++;
                                }
                                break;
                            }
                        }
                        i++;
                    }
                }
            }
            return down_list;
        }

        public string Hit_page_up()
        {
            string page_up = "";
            for (int i = 0; i < l - 8; i++)
            {
                if ("<tr><td " == file.Substring(i, 8))
                {
                    while ("</td></tr>" != file.Substring(i, 10))
                    {
                        if ("alt=" == file.Substring(i, 4) || "alt =" == file.Substring(i, 5))
                        {
                            string alt = "";
                            (alt, i) = sourceCatch(i);
                            if ("[DIR]" == alt)
                            {
                                while (true)
                                {
                                    if ("<a " == file.Substring(i, 3))
                                    {
                                        while ("</a>" != file.Substring(i, 4))
                                        {
                                            if ("href=" == file.Substring(i, 5) || "href =" == file.Substring(i, 6))
                                            {
                                                (page_up, i) = sourceCatch(i);
                                            }
                                            i++;
                                        }
                                        if (page_up != "")
                                        {
                                            return page_up;
                                        }
                                        break;
                                    }
                                    i++;
                                }
                                break;
                            }
                        }
                        i++;
                    }
                }
            }
            return page_up;
        }

        public string comment_out_delete(string html)
        {
            string before = html;
            string after = html;
            
            for (int i = 0;i < before.Length - 4;i++)
            {         
                if ("<!--" == before.Substring(i, 4))
                {
                    string coment = "";
                    int start = i;
                    while ("-->" != before.Substring(i, 3))
                    {
                        i++;
                        if (i == before.Length - 4)
                        {
                            break;
                        }
                    }
                    int end = i;
                    coment = before.Substring(start ,end - start + 3);
                    if (coment != "")
                    {
                        after = after.Replace(coment, "");
                    }
                }

            }
            return after;

        }
        public string url_shave(string url)
        {
            string url_shave = "";
            for (int j = url.Length; j > 1; j--)
            {
                if (url.Substring(j - 2, 1) == "/")
                {
                    url_shave = url.Substring(0, j - 1);
                    break;
                }
            }
            if ("/" == url_shave)
            {
                return "";
            }
            return url_shave;
        }

        public int url_slash_count(string url)
        {
            int count1 = 0;// URLのスラッシュの数
            for (int i = 0; i < url.Length; i++)// URLに含まれているスラッシュの数を調べる
            {
                if (url.Substring(i, 1) == "/")
                {
                    count1++;
                }
            }
            return count1;
        }

        private (string source,int end) sourceCatch(int i)
        {
            int start = i;
            while ("=" != file.Substring(start, 1))
            {
                start++;
            }
            start++;
            int end = start + 1;
            while (true)
            {
                if ("\"" == file.Substring(start, 1))//ダブルクォーテーションだった場合
                {
                    while ("\"" != file.Substring(end, 1))
                    {
                        end++;
                    }
                    start++;
                    break;
                }
                else if ("\'" == file.Substring(start, 1))//シングルクォーテーションだった場合
                {
                    while ("\'" != file.Substring(end, 1))
                    {
                        end++;
                    }
                    start++;
                    break;
                }
                else if (" " == file.Substring(start, 1))//空白だった場合 
                {
                    start++;//一文字ずらす
                    end++;
                }
                else//クォーテーションがついていなかった場合
                {
                    while (" " != file.Substring(end, 1) && ">" != file.Substring(end, 1))
                    {
                        end++;
                    }
                    break;
                }
            }
            return  (htmlfile.Substring(start, end - start),end - 1);//取ってきたデータと終わりの位置を返す。
        }


        public string migrate(string migrate)
        {
            string url = this.url;
            if (migrate.Length > 4)
            {
                if (migrate.Substring(0, 4) == "http")
                {
                    string test = "";
                    int count = 0;
                    for (int s = 0; s < migrate.Length; s++)
                    {
                        if (url.Substring(s, 1) == "/")
                        {
                            count++;
                            if (count == 3)
                            {
                                test = migrate.Substring(0, s + 1);
                                break;
                            }
                        }
                    }
                    if (test == this.domein_url)
                    {
                        url = migrate;
                        return url;
                    }
                }
                else
                {
                    int count1 = url_slash_count(url) - 2;// ex:  http://000.00.00.000/aaa/bbb/ccc/testphp  →　4 (http://のスラッシュ分は省く)
                    if (migrate.Substring(0, 2) == "./")
                    {
                        int count2 = 0;
                        bool flg1 = true;
                        while (migrate.Length > 2)
                        {
                            if (flg1)
                            {
                                migrate = migrate.Substring(2, migrate.Length - 2); // ex:   ./../../../../../test.php →  ../../../../../test.php 
                                count2++;
                                flg1 = false;
                            }
                            else
                            {
                                if (migrate.Substring(0, 3) != "../")//  始まり３文字が../でなければ
                                {
                                    break;
                                }
                                migrate = migrate.Substring(3, migrate.Length - 3); //ex: action が　../../test.php →　../test.php
                                count2++;
                            }
                        }
                        if (count1 < count2)  // action の../の回数がhttp://000.00.00.000/aaa/bbb/ccc/testphpのスラッシュの数より多い場合
                        {
                            count2 = count1; //小さいほうに合わせる。
                        }
                        while (count2 > 0)
                        {
                            url = url_shave(url);
                            count2--;
                        }
                        return url + migrate;
                    }
                    else if (migrate.Substring(0, 3) == "../")
                    {
                        int count2 = 1;
                        while (migrate.Length > 3 && migrate.Substring(0, 3) == "../")
                        {
                            migrate = migrate.Substring(3, migrate.Length - 3);
                            count2++;
                        }
                        if (count1 < count2)  // href の../の回数がhttp://000.00.00.000/aaa/bbb/ccc/testphpのスラッシュの数より多い場合
                        {
                            count2 = count1; //小さいほうに合わせる。
                        }
                        while (count2 > 0)
                        {
                            url = url_shave(url);
                            count2--;
                        }
                        return url + migrate;
                    }
                    else if (migrate.Substring(0, 1) == "/" && migrate.Substring(0, 2) != "//")//ドメインを省略している、かつ、プロトコル名を省略ではない。
                    {
                        return this.domein_url + migrate.Substring(1, migrate.Length - 1);
                    }
                    else if (migrate.Substring(0, 2) == "//")//プロトコルを省略している。
                    {
                        string http_url = "http:" + migrate;
                        (string domein, string directory) = setUrl(http_url);
                        if (this.domein_url == domein)
                        {
                            return http_url;
                        }
                    }
                    else
                    {
                        if (migrate == this.urlfile)
                        {
                            return this.url;
                        }
                        else
                        {
                            return this.domein_url + url_shave(this.directory_url) + migrate;
                        }
                    }
                }
            }
            return "";
        }
    }
}
