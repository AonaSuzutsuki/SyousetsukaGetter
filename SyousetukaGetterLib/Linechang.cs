using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web; //参照設定の追加
using System.Net; //HTTPアクセス

namespace SyousetukaGetterLib
{
    class Linechang
    {
        private string url;
        private WebClient webClient = new WebClient();
        private string json;

        public string getterUrl()
        {
            return url;
        }
        /*
         * jsonのデータを設定する。(デフォルト)
         * 
         * */
        public string setterJson() {
            byte[] data = webClient.DownloadData(url);
            json = byteChangeString(data);
            json = json.TrimEnd(']').TrimStart('[');
            var source = DynamicJson.Parse(json);
            return (string)source.allcount;
        }
        /*
         * jsonのデータを設定する。(並び替え)
         * 
         * */
        public string setterJson(string url) {
            byte[] data = webClient.DownloadData(url);
            json = byteChangeString(data);
            json = json.TrimEnd(']').TrimStart('[');
            var source = DynamicJson.Parse(json);
            return (string)source;
        }

        public string getterJson() {
            return json;
        }
        /*
         * コンストラクタ
         * 
         * */
        public Linechang(){
            url = "http://api.syosetu.com/novelapi/api/?out=json";
        }
        /*
         * byte[]型をstring型に変換する。
         * 
         * */
        private string byteChangeString(byte[] data) {
            string changeData = System.Text.Encoding.UTF8.GetString(data);
            return changeData;
        }
        /*
         * 出力順序をブックマーク数の多い順に行う。
         * 
         * */
        public string orderFavnovelcnt() {
            string favnovelcntUrl = (url + "&order=favnovelcnt");
            return setterJson(favnovelcntUrl);
        }
        /*
         * 出力順序をレビュー数の多い順に行う。
         * 
         * */
        public string orderReviewcnt() {
            string reviewcntUrl = (url + "&order=reviewcnt");
            return setterJson(reviewcntUrl);
        }

        /*
         * 出力順序を古い順に行う。
         * 
         */
        public string orderOld()
        {
            string oldUrl = (url + "&order=old");
            return setterJson(oldUrl);
        }

    }
}
