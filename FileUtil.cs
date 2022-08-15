using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MBSD
{
	class FileUtil
	{
		private file_attack_code fac;

		public FileUtil(file_attack_code fac)
        {
			this.fac = fac;
        }

        public List<ConfigInfo>　setReadCsvFile()
		{

			List<ConfigInfo> cilist = new List<ConfigInfo>();

			try
			{
				int elements_length = new ConfigInfo().getLength();
				foreach(var item in fac.getFacd())
				{
					List<string> key = new List<string> { };
					List<string> value = new List<string> { };

					int no = item.getNo();
					string url = item.getBefore_url();
					string query_parameter = item.getQuery_parameter();
					int attack_type = Int32.Parse(item.getVulnerability());
					int omomi_type = Int32.Parse(item.getWeight());
					int target_status_code = Int32.Parse(item.getTargetcode());
					int attack_pattern = Int32.Parse(item.getKind());
					string method = item.getMethod();
					key = item.getName();
					value = item.getValue();
					string html = item.getBefore_htmlfile();



					ConfigInfo insCI = new ConfigInfo(no, url, query_parameter, attack_type, omomi_type, target_status_code, attack_pattern, method, key, value,html);


					cilist.Add(insCI);

				}
				return  cilist;
			}
			catch (Exception e)
			{

				return  cilist;
			}
		}
    }

	class MethodUtil
	{
		public (bool, string) checkRegix(string strLine, string pattern)
		{
			//string pattern = @"\s*<script>alert.*</script>Hack$"; //\s:空白 *:0回以上 $:行末 .:任意の一文字

			Match match = Regex.Match(strLine, pattern);

			return (match.Success, match.Value);
		}

		/// <summary>
		/// 与えられた文字列を改行コードで分割し、返す
		/// </summary>
		/// <param name="strInfo"></param>
		/// <returns></returns>
		public string[] dataChange(String strInfo)
		{
			string[] lines = strInfo.Split(new string[] { "\n" }, StringSplitOptions.None);
			return lines;
		}

		public string requestURLSroce(string url)
		{
			// ソースファイル
			string srocefile = "";
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
				Task<string> r2 = client.GetStringAsync(url);
				srocefile = r2.Result;
			}
			return srocefile;
		}

		public string requestURLStatusCode(string url)
		{
			// ステータスコード
			string status_code = "";


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
				var uri = new Uri(url);
				var r = client.GetAsync(uri).Result;
				status_code = string.Format("{0}", (int)r.StatusCode);
			}
			return status_code;
		}

		public string Cookie(string url,string method, Dictionary<string, string> dictionary)
		{
			try
			{

                CookieContainer cookies = new CookieContainer();
                HttpClientHandler handler = new HttpClientHandler();
                handler.CookieContainer = cookies;
                handler.UseCookies = true;

                if (method == "GET")
                {
					Uri uri = new Uri(url);
					HttpClient client = new HttpClient(handler);
					var response = client.GetAsync(uri).Result;
					Console.WriteLine(response);
                    string cookie = response.Headers.GetValues("Set-Cookie").First();

                    //return cookie;
                    //string text = "";
                    //var cookie = response.Headers.GetValues("Set-Cookie");
                    //foreach (var item in cookie)
                    //{
                    //	text += item + "\r\n";
                    //}
                    if (cookie.Substring(8,1) != ";")
                    {
						return cookie;
					}
					return null;
				}
                else
                {
                    Uri uri = new Uri(url);
                    HttpClient client = new HttpClient(handler);
                    var content = new FormUrlEncodedContent(dictionary);
                    var res = client.PostAsync(uri, content).Result;
                    string cookie = res.Headers.GetValues("Set-Cookie").First();

                    if (cookie.Substring(8,1) != ";")
                    {
						return cookie;
					}
					return null;
                }

            }
			catch (Exception e)
			{

				return null;
			}

		}

		public bool IndexSearch(string url)
		{
			bool is_flag = false;

			// ソースファイル
			string srocefile = "";
			try
			{
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
					Task<string> r2 = client.GetStringAsync(url);
					srocefile = r2.Result;
				}
			}
			// 404などでページのソースが取得できずに失敗したときの処理
			catch (Exception e)
			{
				return is_flag;
			}

			//string pattern = @"\s*<script>alert.*</script>Hack$";

			string[] list_src = dataChange(srocefile);
			string pattern = @"\s*<title>Index of.*</title>$";
			bool is_stat = false;
			string tmp = "";
			for (var i = 0; i < list_src.Length; i++)
			{
				string strLine = list_src[i];

				(is_stat, tmp) = checkRegix(strLine, pattern);
				if (is_stat)
				{
					is_flag = true;
					break;
				}

			}

			return is_flag;
		}

	}
}
