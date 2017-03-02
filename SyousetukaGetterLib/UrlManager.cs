using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetukaGetterLib
{
    class UrlManager
    {
        private const string defaultUrl = "http://api.syosetu.com/novelapi/api/?out=json";
        private int limNumber = 20;
        private string lim;
        private int stNumber = 1;
        private string st;
        private string order = "";

        public string Url
        {
            get
            {
                return defaultUrl + lim + st + order;
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
        public void StSetter(int num)
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
        public void OrderSetter(Order order)
        {
            switch (order)
            {
                case Order.basic:
                    this.order = "";
                    break;
                default:
                    this.order = "&order" + order;
                    break;
            }
            return;
        }


    }
}
