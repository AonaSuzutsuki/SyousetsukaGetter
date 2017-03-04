using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SyousetukaGetterLib
{
    public class NovelDownloader
    {
        private const string defaultNovelUrl = "http://ncode.syosetu.com"; //小説の本文のurl
        /*
         * 小説のID
         * 
         * */
        public string Ncode
        {
            private set;
            get;
        }
        /*
         * ページ数
         * 
         * */
        public int Page
        {
            private set;
            get;
        }
        /*
         * 連載か短編か
         * true = 連載
         * false = 短編
         * 
         * */
        public bool Juge
        {
            private set;
            get;
        }
        /*
         * 小説の本文
         * key = Ncode
         * values = 本文
         * 
         * */
        public Dictionary<string, List<string>> NovelText
        {
            get;
            private set;
        } = new Dictionary<string, List<string>>();
        /*
         * 小説のタイトル
         * key = Ncode
         * values = タイトル
         * 
         * */
        public Dictionary<string, List<string>> Title
        {
            get;
            private set;
        } = new Dictionary<string, List<string>>();
        /*
         * 小説本文のurl
         * 
         * */
        private string NovelUrl
        {
            get
            {
                return defaultNovelUrl + "/" + Ncode + "/";
            }
        }



        /*
         * コンストラクタ
         * 
         * */
        public NovelDownloader(string nCode, int page, bool juge)
        {
            Ncode = nCode;
            Page = page;
            Juge = juge;
        }
        /*
         * 設定を再度行う
         * 
         * */
        public void SetData(string nCode, int page, bool juge)
        {
            Ncode = nCode;
            Page = page;
            Juge = juge;
            return;
        }
        /*
         * 小説のダウンロードを行う。
         * 
         * 
         * */
        public void DownloadNovel()
        {
            var urls = GetNovelUrl();
            var text = new List<string>();
            var title = new List<string>();
            foreach (string url in urls)
            {
                string html = GetHtml(url);
                text.Add(AnalysisHtml(html));
                if (Juge)
                {
                    title.Add(AnalysisHtmTitlel(html));
                }
            }
            NovelText.Add(Ncode, text);
            if (Juge)
            {
                Title.Add(Ncode, title);
            }

            return;
        }
        /*
         * 小説本文のUrlを手に入れる。
         * 
         * */
        private List<string> GetNovelUrl()
        {
            var urls = new List<string>();
            if (Juge)
            {
                urls.Add(NovelUrl + Page);
            }
            else
            {
                urls.Add(NovelUrl);
            }
            return urls;
        }
        /*
         * Htmlを手に入れる
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
         * Htmlのデータを解析し本文を取得する。
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
         * Htmlのデータを解析しタイトルを取得する。
         * 
         * */
        private string AnalysisHtmTitlel(string html)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.OptionAutoCloseOnEnd = false;  //最後に自動で閉じる（？）
            doc.OptionCheckSyntax = true;     //文法チェック。
            doc.OptionFixNestedTags = true;    //閉じタグが欠如している場合の処理
            doc.LoadHtml(html);
            HtmlAgilityPack.HtmlNode node = doc.DocumentNode.SelectSingleNode("//p[@class='novel_subtitle']");
            return node.InnerHtml;
        }
    }
}
