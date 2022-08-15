using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace MBSD
{
    public class ConfigInfo
    {
        private int index;
        private string url;
        private string query_parameter;
        private int attack_type;
        private int omomi_type;
        private int target_security_code;
        private int attack_pattern;
        private bool security_flag;
        private int before_status_code;
        private string before_url_sorce;
        private int after_status_code;
        private string after_url_sorce;
        private string paramtuki_url; // url と query_parameterを足したもの
        public bool flg;//ifrem


        public string after_status_name;


        private Dictionary<string, string> dictionary = new Dictionary<string, string>{ };
        private string url_key_value = "";
        private string method;

        // 引数なしコンストラクタ
        public ConfigInfo()
        {

        }

        // コンストラクタ
        public ConfigInfo(int no, string url, string query_parameter, int attack_type, int omomi_type, int target_status_code, int attack_pattern, string method,List<string> key,List<string> value,string html)
        {
            this.index = no;
            this.url = url;
            this.query_parameter = query_parameter;
            this.attack_type = attack_type;
            this.omomi_type = omomi_type;
            this.target_security_code = target_status_code;
            this.attack_pattern = attack_pattern;
            // 初期化の値を代入
            this.security_flag = false;
            this.method = method;
            this.before_url_sorce = html;



            




            // urlから攻撃前のステータスコードをセットする
            //setRequestURLStatuCode();
            //this.before_status_code = ;
            // urlから攻撃前のソースファイルをセットする
            setRequestURLSroce();
            if (this.method == "GET")
            {
                
                this.paramtuki_url = url + query_parameter;
                // 攻撃後のソースとステータスコードをセットする
                this.after_status_code = requestURLStatusCode(this.paramtuki_url);
                this.after_url_sorce = requestURLSroce(this.paramtuki_url);
            }
            else
            {
                string url_key_value = "";

                for(int i = 0;i < key.Count;i++)
                {
                    this.dictionary.Add(key[i],value[i]);
                    url_key_value = url_key_value + string.Format("key:{0} value:{1}", key[i], value[i]) + " ";
                }
                this.url_key_value = string.Format("{0}  ",this.url) + url_key_value.Substring(0,url_key_value.Length-1);
                this.paramtuki_url = url;
                post_after_set_htmlsorce(dictionary);
                post_after_set_statuscode(dictionary);
            }

        }
        public int getAttackType()
        {
            return this.attack_type;
        }
        //脆弱性とみなすステータスコードの取得
        public int getTargetSecurityCode()
        {
            return this.target_security_code;
        }
        public string getURL()
        {
            return this.url;
        }
        public string getParamtukiURL()
        {
            return this.paramtuki_url;
        }
        public int getAttackPattern()
        {
            return this.attack_pattern;
        }
        public string getAfter_url_sorce()
        {
            return this.after_url_sorce;
        }

        public int getOmomi()
        {
            return this.omomi_type;
        }

        public string afterStatusname()
        {
            return this.after_status_name;
        }


        public bool getSecurityFlag()
        {
            return this.security_flag;
        }





        public void setSecurityFlag(bool security_flag)
        {
            this.security_flag = security_flag;
        }
        public void setRequestURLSroce()
        {
            try
            {
                string url = this.url;
                var handler = new HttpClientHandler
                {
                    // 自動でリダイレクトする機能をOFFにする
                    AllowAutoRedirect = false
                };
                // ハンドラーを指定してHttpClientを作る
                using (var client = new HttpClient(handler))
                {
                    var uri = new Uri(url);
                    var r = client.GetAsync(uri).Result;
                    var tmpStatusCode = (int)r.StatusCode;
                    // 404または403以外のときにソースを取得する
                    Task<string> r2 = client.GetStringAsync(url);
                    this.before_url_sorce = r2.Result;
                }
            }
            catch (Exception e)
            {
                this.before_url_sorce = "";

            }
        }
        public string getRequestURLSroce()
        {
            return this.before_url_sorce;
        }
        public string getRequestURLAfterSroce()
        {
            return this.after_url_sorce;
        }
        public void setRequestURLStatuCode()
        {
            string url = this.url;
            // urlに対してのステータスコードセットする処理を下記に記載する
            var handler = new HttpClientHandler
            {
                // 自動でリダイレクトする機能をOFFにする
                AllowAutoRedirect = false
            };
            // ハンドラーを指定してHttpClientを作る
            using (var client = new HttpClient(handler))
            {
                this.url = url;
                var uri = new Uri(url);
                var r = client.GetAsync(uri).Result;
                this.before_status_code = (int)r.StatusCode;
            }
        }
        public int getRequestURLStatuCode()
        {
            return this.before_status_code;
        }
        public int getRequestURLAfterStatuCode()
        {
            return this.after_status_code;
        }


        public string getMethod()
        {
            return this.method;
        }

        public string getUrl_Key_Value()
        {
            return this.url_key_value;
        }

        public Dictionary<string, string> getDictionary()
        {
            return this.dictionary;
        }

        public string requestURLSroce(string url)
        {
            // ソースファイル
            string srocefile = "";
            try
            {
                bool isFlag = false;
                if (this.target_security_code == 302)
                {
                    isFlag = true;
                }
                var handler = new HttpClientHandler
                {
                    // 自動でリダイレクトする機能をOFF(falseの場合OFF)にする
                    AllowAutoRedirect = isFlag

                };
                // ハンドラーを指定してHttpClientを作る
                using (var client = new HttpClient(handler))
                {
                    
                    //var uri = new Uri(url);
                    //var r = client.GetAsync(url).Result;
                    Task<string> r2 = client.GetStringAsync(url);
                    srocefile = r2.Result;
                }
                return srocefile;
            }
            catch (Exception e)
            {
                return srocefile;
            }
        }
        public int requestURLStatusCode(string url)
        {
            // ステータスコード
            int status_code;
            try
            {

                // urlに対してのステータスを取得する処理
                // urlに対してのステータスコードセットする処理を下記に記載する
                var handler = new HttpClientHandler
                {
                    // 自動でリダイレクトする機能をOFFにする
                    AllowAutoRedirect = false
                };
                // ハンドラーを指定してHttpClientを作る
                using (var client = new HttpClient(handler))
                {
                    var r = client.GetAsync(url).Result;
                    status_code = (int)r.StatusCode;
                }
                Hashtable ht = new Hashtable
                {
                    [200] = "OK",
                    [302] = "Found",
                    [403] = "Forbidden",
                    [404] = "Not found",
                    [500] = "Server Error"
                };
                this.after_status_name = (string)ht[status_code];

                return status_code;
            }
            catch (Exception e)
            {
                this.after_status_name = "";
                return 9999;
            }
        }

        public int getLength()
        {
            string[] elements_list = { "no", "url", "query_parameter", "attack_type", "omomi_type", "target_status_code", "attack_pattern", "method","key","value" };

            return elements_list.Length;

        }


        public void post_after_set_htmlsorce(Dictionary<string, string> dictionary)
        {
            try
            {               
                bool isFlag = false;
                if (this.target_security_code == 302)
                {
                    isFlag = true;
                }
                var handler = new HttpClientHandler
                {
                    // 自動でリダイレクトする機能をOFFにする
                    AllowAutoRedirect = isFlag
                };
                using (var client = new HttpClient(handler))
                {
                    var content = new FormUrlEncodedContent(dictionary);
                    var uri = new Uri(this.url);
                    var res = client.PostAsync(uri, content).Result;
                    string html = res.Content.ReadAsStringAsync().Result;
                    this.after_url_sorce = html;
                }
            }
            catch (Exception e)
            {

                this.after_url_sorce = "";
            }
        }


        public void post_after_set_statuscode(Dictionary<string, string> dictionary)
        {
            try
            {             
                var handler = new HttpClientHandler
                {
                    // 自動でリダイレクトする機能をOFFにする
                    AllowAutoRedirect = false
                };
                using (var client = new HttpClient(handler))
                {
                    var content = new FormUrlEncodedContent(dictionary);
                    var uri = new Uri(this.url);
                    var res = client.PostAsync(uri, content).Result;
                    this.after_status_code = (int)res.StatusCode;
                    Hashtable ht = new Hashtable
                    {
                        [200] = "OK",
                        [302] = "Found",
                        [403] = "Forbidden",
                        [404] = "Not found",
                        [500] = "Server Error"
                    };
                    this.after_status_name = (string)ht[(int)res.StatusCode];
                }

            }
            catch (Exception e)
            {
                this.after_status_name = "";
               this.after_status_code = 9999;
            }
        }


        public Dictionary<string, string> key_value(string key,string value)
        {
            var dictionary = new Dictionary<string, string> { };
            string[] keys = key.Split(',');
            string[] values = value.Split(',');
            //Console.WriteLine(keys.Count());
            for (int i = 0;i< keys.Count();i++)
            {
                try
                {
                    dictionary.Add(keys[i],values[i]);
                }
                catch
                {
                    Console.WriteLine(i);
                }
                
            }
            return dictionary;
        }
    }
}