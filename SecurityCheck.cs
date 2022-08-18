using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace MBSD
{
    class SecurityCheck
    {

        public void attack_common(ConfigInfo insCI)
        {
            int attack_type = insCI.getAttackType();

            // SQLインジェクション
            if (attack_type == 1)
            {
                checkSQLInjection(insCI);
            }
            // クロスサイトスクリプティング
            else if(attack_type == 2)
            {
                checkXSS(insCI);
            }
            //csrf
            else if(attack_type == 3)
            {
                if (checkCSRF(insCI))
                {
                    insCI.setSecurityFlag(true);
                }
            }
            // OS
            else if (attack_type == 4)
            {
                if (checkOS(insCI))
                {
                    insCI.setSecurityFlag(true);
                }
            }
            // ディレクトリ・リスティング
            else if (attack_type == 5)
            {
                checkDirectoryListing(insCI);
            }
            // ディレクトリトラバーサル
            else if (attack_type == 6)
            {
                checkDirectoryTraversal(insCI);
            }

        }

        // SQLインジェクション攻撃
        public void checkSQLInjection(ConfigInfo insCI)
        {

            int attack_pattern = insCI.getAttackPattern();
            string method = insCI.getMethod();
            Dictionary<string, string> dictionary = insCI.getDictionary();
            int target_security_code = insCI.getTargetSecurityCode();
            MethodUtil insMU = new MethodUtil();
            string paramtuki_url = insCI.getParamtukiURL();
            // 標準出力の取得
            
            int after_status_code = insCI.getRequestURLAfterStatuCode();
            

            // チェックパターン1
            if (attack_pattern == 1)
            {
                string cookie = insMU.Cookie(paramtuki_url, method, dictionary);
                if (after_status_code == 302 || after_status_code == 200)
                {
                    if(cookie!= null)
                    {
                        insCI.setSecurityFlag(true);
                    }              
                }

            }
            if (attack_pattern == 2)
            {
                bool isFlag = checkError(insCI.getRequestURLAfterSroce());
                if (isFlag )
                {
                    insCI.setSecurityFlag(true);
                }
            }

            if (attack_pattern == 3)
            {
                if (checkSQLTime(insCI.getParamtukiURL(),method,dictionary))
                {
                    insCI.setSecurityFlag(true);
                }           
            }
        }


        public void checkXSS(ConfigInfo insCI)
        {
            MethodUtil insMU = new MethodUtil();

            //string before_url_sorce = insCI.getRequestURLSroce();
            string after_url_sorce = insCI.getRequestURLAfterSroce();


            string[] after_url = insMU.dataChange(after_url_sorce);
            string pattern = @"\s*<script>alert.*</script>Hack.*"; // \s :　空白 * : 0回以上の繰り返し $:末尾
            bool isFlag = false;
            string matchstr = "";
            for (var i = 0; i < after_url.Length; i++)
            {
                string strLine = after_url[i];
                (isFlag, matchstr) = insMU.checkRegix(strLine, pattern);
                if (isFlag)
                {
                    insCI.setSecurityFlag(true);
                    break;
                }
            }



        }

        public bool checkOS(ConfigInfo insCI)
        {
            try
            {
                string url = insCI.getParamtukiURL();
                string method = insCI.getMethod();
                Dictionary<string, string> dictionary = insCI.getDictionary();
                var timer = new System.Diagnostics.Stopwatch();
                if (method == "GET")
                {
                    timer.Start();
                    var client = new HttpClient();
                    var uri = new Uri(url);
                    var r = client.GetAsync(uri).Result;
                    timer.Stop();

                    if (timer.Elapsed.TotalSeconds > 5)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    timer.Start();
                    var client = new HttpClient();
                    var content = new FormUrlEncodedContent(dictionary);
                    var uri = new Uri(url);
                    var res = client.PostAsync(uri, content).Result;
                    timer.Stop();

                    if (timer.Elapsed.TotalSeconds > 2)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void checkDirectoryListing(ConfigInfo insCI)
        {

            MethodUtil insMU = new MethodUtil();

            string url = insCI.getParamtukiURL();
            int attack_pattern = insCI.getAttackPattern();
            bool is_flag = insMU.IndexSearch(url);

            if (attack_pattern == 1)
            {
                if (is_flag)
                {
                    insCI.setSecurityFlag(true);
                }
            }
        }

        public void checkDirectoryTraversal(ConfigInfo insCI)
        {
            int after_status_code = insCI.getRequestURLAfterStatuCode();
            int target_security_code = insCI.getTargetSecurityCode();

            if (after_status_code == target_security_code)
            {
                insCI.setSecurityFlag(true);
            }
        }



        public bool checkSQLTime(string url,string method,Dictionary<string,string> dictionary)
        {
            try
            {

                var timer = new System.Diagnostics.Stopwatch();
                if (method == "GET")
                {
                    timer.Start();
                    var client = new HttpClient();
                    var uri = new Uri(url);
                    var r = client.GetAsync(uri).Result;
                    timer.Stop();

                    if (timer.Elapsed.TotalSeconds > 5)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    timer.Start();
                    var client = new HttpClient();
                    var content = new FormUrlEncodedContent(dictionary);
                    var uri = new Uri(url);
                    var res = client.PostAsync(uri, content).Result;
                    timer.Stop();

                    if (timer.Elapsed.TotalSeconds > 2)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }


        }


        private bool checkCSRF(ConfigInfo insCI)
        {
            string file = insCI.getRequestURLSroce().ToLower();
            for (int i = 0; i < file.Length - 11; i++)
            {
                if ("<iframe " == file.Substring(i, 8))
                {
                    string src = "";
                    while ("</iframe>" != file.Substring(i, 9))
                    {
                        if ("src=" == file.Substring(i, 4) || "src =" == file.Substring(i, 5))
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
                            src = file.Substring(start, end - start);//取ってきたデータと終わりの位置を返す。
                            i = end - 1;
                            if (src == "/" || src == "#" || src == "")
                            {
                                return false;
                            }
                        }
                        i++;
                    }
                    if (src == "")
                    {
                        return false;
                    }
                    else
                    {
                        if (src == insCI.getURL())
                        {
                            return false;
                        }
                        else
                        {
                            file_search fs = new file_search(src);
                            List<string> action = fs.getAction();
                            if (action[0] != "")
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }
                    }
                }
            }
            return false;
        }


        public bool checkError(string soruse)
        {
            try
            {
                string[] list_src = soruse.Split(new string[] { "\n" }, StringSplitOptions.None);
                string[] pattern = { @"\s*syntax error.*", @"\s*You can only execute one.*", @"\s*You have an error in your SQL syntax.*" };

                foreach (var item in pattern)
                {
                    foreach (var item2 in list_src)
                    {
                        Match match = Regex.Match(item2, item);
                        if (match.Success)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {

                return false;
            }

        }
    }
}
