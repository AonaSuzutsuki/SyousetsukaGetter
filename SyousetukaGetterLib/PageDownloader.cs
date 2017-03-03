using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace SyousetukaGetterLib
{
    class PageDownloader
    {
        private NovelUrlManager novelUrl = new NovelUrlManager();

        /*
         * 小説のダウンロードを行う。
         * 
         * */
        public void StartDownload(int index)
        {
            string[] urls = novelUrl.Novel(index);
            foreach (string url in urls)
            {
                string html = GetHtml(url);
                AnalysisHtml(url);
            }
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
        private void AnalysisHtml(string html)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.OptionAutoCloseOnEnd = false;  //最後に自動で閉じる（？）
            doc.OptionCheckSyntax = false;     //文法チェック。
            doc.OptionFixNestedTags = true;    //閉じタグが欠如している場合の処理
            doc.LoadHtml(html);
            HtmlAgilityPack.HtmlNode node = doc.DocumentNode.SelectSingleNode("div[@id='novel_honbun']");
            System.Console.WriteLine(node.InnerHtml);
            System.Console.ReadLine();
            return;
        }


    }
}
