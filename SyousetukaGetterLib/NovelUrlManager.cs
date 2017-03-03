using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetukaGetterLib
{
    class NovelUrlManager
    {
        private const string defaultNovelUrl = "http://ncode.syosetu.com";
        private string nCode;
        private int page = 1;
        private JsonUrlManager json = new JsonUrlManager();

        private string NovelUrl
        {
            get
            {
                return defaultNovelUrl + "/" + nCode + "/";
            }
        }

        /*
        * ダウンロードする小説のNcodeとpage数を設定しUrlを決定する。
        * 
        * 
        * */
        public string[] Novel(int index)
        {
            var loader = new JsonLoader(json.JsonUrl);
            GetNcode(loader, index);
            GetPage(loader, index);
            return GetNovel();
        }
        /*
         * Ncodeを取得する。
         * 
         * */
        private void GetNcode(JsonLoader loader, int index)
        {
            nCode = loader.GetValue(index, "ncode");
        }
        /*
         * page数を取得する。
         * 
         * */
        private void GetPage(JsonLoader loader, int index)
        {
            string generalAllNo = "1";
            generalAllNo = loader.GetValue(index, "general_all_no");
            page = int.Parse(generalAllNo);
            return;
        }
        /*
         * 小説をダウンロードするためのurlを取得する。
         * 
         * */
        private string[] GetNovel()
        {
            string[] novelUrl = new string[page];
            for (int i = 0; i < page; i++)
            {
                novelUrl[i] = NovelUrl + Convert.ToString(i + 1);
            }
            return novelUrl;
        }
    }
}
