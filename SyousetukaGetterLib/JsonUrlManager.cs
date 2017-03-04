using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetukaGetterLib
{
    public class JsonUrlManager
    {
        private const string defaultJsonUrl = "http://api.syosetu.com/novelapi/api/?out=json";
        private int limNumber = 20;
        private string lim;
        private int stNumber = 1;
        private string st;
        private string order = "";
        private string genreItems = "";
        private string secondGenreItems = "";
        private string searchWordText = "";
        private string titleIsChecked = "";
        private string storyIsChecked = "";
        private string keywordIsChecked = "";
        private string writerIsChecked = "";
        private string userIDText = "";
        private string nCodeText = "";

        public string JsonUrl
        {
            get
            {
                return defaultJsonUrl + lim + st + order + genreItems + secondGenreItems + searchWordText + titleIsChecked + storyIsChecked + keywordIsChecked + writerIsChecked + userIDText + nCodeText;
            }
        }

        public enum Order
        {
            basic, //新着順
            favnovelcnt, //ブックマーク数の多い順
            reviewcnt, //レビュー数の多い順
            hyoka, //総合評価の高い順
            hyokaasc, //総合評価の低い順
            impressioncnt, //感想の多い順
            hyokacnt, //評価者数の多い順
            hyokacntasc, //評価者数の少ない順
            weekly, //週間ユニークユーザの多い順
            lengthdesc, //小説本文の文字数が多い順
            lengthasc, //小説本文の文字数が少ない順
            ncodedesc, //Nコードが新しい順
            old, //古い順
        }
        /*
         * 出力数を変更する。
         * (最低1,最大500)
         * 
         * */
        public void SetLim(int num)
        {
            num = LimRange(num);
            limNumber = num;
            lim = "&lim=" + limNumber;
        }
        /*
         * limの範囲が正しいかどうかの判定
         * 
         * */
        private int LimRange(int num)
        {
            if (num < 1)
            {
                return 1;
            }
            else if (num > 500)
            {
                return 500;
            }
            else
            {
                return num;
            }
        }
        /*
         * 表示開始位置の指定。
         * (1から2000)
         * 
         * */
        public void SetSt(int num)
        {
            num = StRange(num);
            stNumber = num;
            st = "&st=" + stNumber;
        }
        /*
         * stの範囲判定
         * 
         * */
        private int StRange(int num)
        {
            if (num < 0)
            {
                return 1;
            }
            else if (num > 2000)
            {
                return 2000;
            }
            else
            {
                return num;
            }
        }
        /*
         * 出力順序指定
         * 
         * */
        public void SetOrder(Order order)
        {
            switch (order)
            {
                case Order.basic:
                    this.order = "";
                    return;
                default:
                    this.order = "&order=" + order;
                    return;
            }
        }
        /*
         * 大ジャンル指定
         * 1: 恋愛
         * 2: ファンタジー
         * 3: 文芸
         * 4: SF
         * 98: ノンジャンル
         * 99: その他
         * 
         * */
        public void SetGenreItems(string id)
        {
            int select = int.Parse(id);
            switch (select)
            {
                case 0:
                    genreItems = "";
                    return;
                default:
                    SetBiggenre(id);
                    return;
            }
        }
        /*
         * 大ジャンル指定したurlを設定
         * 
         * */
        private void SetBiggenre(string id)
        {
            genreItems = "&biggenre=" + id;
            return;
        }
        /*
         * ジャンル指定
         * 101: 異世界[恋愛]
         * 102: 現実世界[恋愛]
         * 201: ハイファンタジー[ファンタジー]
         * 202: ローファンタジー〔ファンタジー〕
         * 301: 純文学〔文芸〕
         * 302: ヒューマンドラマ〔文芸〕
         * 303: 歴史〔文芸〕
         * 304: 推理〔文芸〕
         * 305: ホラー〔文芸〕
         * 306: アクション〔文芸〕
         * 307: コメディー〔文芸〕
         * 401: VRゲーム〔SF〕
         * 402: 宇宙〔SF〕
         * 403: 空想科学〔SF〕
         * 404: パニック〔SF〕
         * 9901: 童話〔その他〕
         * 9902: 童話〔その他〕
         * 9903: エッセイ〔その他〕
         * 9904: リプレイ〔その他〕
         * 9801: ノンジャンル〔ノンジャンル〕
         * 9999: その他〔その他〕
         * 
         * */
        public void SetSecondGenreItems(string id)
        {
            int select = int.Parse(id);
            switch (select)
            {
                case 0:
                    secondGenreItems = "";
                    return;
                default:
                    SetGenre(id);
                    return;
            }
        }
        /*
         * ジャンル指定したurlを設定
         * 
         * */
        private void SetGenre(string id)
        {
            secondGenreItems = "&genre=" + id;
            return;
        }
        /*
         * 検索単語を設定する。
         * 
         * */
        public void SetSearchWordText(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                searchWordText = "";
                return;
            }
            else
            {
                searchWordText = "&word=" + word;
                return;
            }
        }
        /*
         * タイトルを検索するかどうかを決定する
         * 
         * 
         * */
        public void SetTitleIsChecked(bool check)
        {
            if (check)
            {
                titleIsChecked = "&title=1";
                return;
            }
            else
            {
                titleIsChecked = "";
                return;
            }
        }
        /*
         * あらすじを検索するかどうかを決定する
         * 
         * */
        public void SetStoryIsChecked(bool check)
        {
            if (check)
            {
                storyIsChecked = "&ex=1";
                return;
            }
            else
            {
                storyIsChecked = "";
                return;
            }
        }
        /*
         * キーワードを検索するかどうかを決定する
         * 
         * */
        public void SetKeywordIsChecked(bool check)
        {
            if (check)
            {
                keywordIsChecked = "&keyword=1";
                return;
            }
            else
            {
                keywordIsChecked = "";
                return;
            }
        }
        /*
         * 作者名を検索するかどうかを決定する
         * 
         * */
        public void SetWriterIsChecked(bool check)
        {
            if (check)
            {
                writerIsChecked = "&wname=1";
                return;
            }
            else
            {
                writerIsChecked = "";
                return;
            }
        }
        /*
         * ユーザIDを検索する
         * 
         * */
        public void SetUserIDText(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                userIDText = "";
                return;
            }
            else
            {
                userIDText = "&userid=" + userID;
                return;
            }
        }
        /*
         * nCodeで検索する
         * 
         * */
        public void SetNCodeText(string nCode)
        {
            if (string.IsNullOrEmpty(nCode))
            {
                nCodeText = "";
                return;
            }
            else
            {
                nCodeText = "&ncode=" + nCode;
                return;
            }
        }


    }
}
