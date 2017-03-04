using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace SyousetukaGetterLib
{
    public class PageDownloader
    {
        private NovelUrlManager novelUrl;

        public Dictionary<string, List<string>> NovelText
        {
            get;
            private set;
        } = new Dictionary<string, List<string>>();

        /*
         * コンストラクタ
         * 
         * */
        public PageDownloader(JsonUrlManager jsonUrl)
        {
            novelUrl = new NovelUrlManager(jsonUrl);
        }

        /*
         * 小説のダウンロードを行う。
         * 
         * */
        public void StartDownload(int index)
        {
            string[] urls = novelUrl.Novel(index);
            var text = new List<string>();
            foreach (string url in urls)
            {
                string html = GetHtml(url);
                text.Add(AnalysisHtml(html));
            }
            NovelText.Add(novelUrl.NCode,text);
            return;
        }
        /*
         * 指定した小説のページから本文をHtmlで取得する。
         * 
         * */
        private string GetHtml(string url)
        {
            var wc = new WebClient();
            var st = wc.OpenRead(url);
            var enc = Encoding.GetEncoding("UTF-8");
            var sr = new StreamReader(st, enc);
            string html = sr.ReadToEnd();
            sr.Close();
            st.Close();
            return html;
        }
        /*
         * Htmlのデータを解析する。
         * 
         * */
        private string AnalysisHtml(string html)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.OptionAutoCloseOnEnd = false;  //最後に自動で閉じる（？）
            doc.OptionCheckSyntax = true;     //文法チェック。
            doc.OptionFixNestedTags = true;    //閉じタグが欠如している場合の処理
            doc.LoadHtml(html);
            HtmlAgilityPack.HtmlNode node = doc.GetElementbyId("novel_honbun");
            string text = node.InnerHtml.Replace("<br>", "");
            return text;
        }
        /*
         * 本文の表示を行う。
         * 
         * */
         public void GetText()
         {
            foreach(string text in NovelText[novelUrl.NCode])
            {
                Console.WriteLine(text); //本来は本文をダウンロードするためのメソッドを実装する。
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++");
            }
            return;
        }



    }
}
