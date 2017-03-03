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
            bool novelType = NovelType(loader, index);
            GetNcode(loader, index);

            GetPage(loader, index, novelType);
            return GetNovel(novelType);
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
        private void GetPage(JsonLoader loader, int index, bool novelType)
        {
            string generalAllNo = "1";
            if (novelType)
            {
                generalAllNo = loader.GetValue(index, "general_all_no");
                page = int.Parse(generalAllNo);
                return;
            }
            else
            {
                page = 1;
                return;
            }

        }
        /*
         * 小説をダウンロードするためのurlを取得する。
         * 
         * */
        private string[] GetNovel(bool novelType)
        {
            string[] novelUrl = new string[page];
            if (novelType)
            {
                for (int i = 0; i < page; i++)
                {
                    novelUrl[i] = NovelUrl + Convert.ToString(i + 1);
                }
                return novelUrl;
            }
            else
            {
                novelUrl[0] = NovelUrl;
                return novelUrl;
            }

        }
        /*
         * 連載か短編かを判定する。
         * (連載 juge = 1, 短編 juge = 2)
         * 
         * */
        private bool NovelType(JsonLoader loader, int index)
        {
            int juge = int.Parse(loader.GetValue(index, "novel_type"));
            if (juge == 1)
            {
                return true;
            }
            else if (juge == 2)
            {
                return false;
            }
            return true;
        }
    }
}
