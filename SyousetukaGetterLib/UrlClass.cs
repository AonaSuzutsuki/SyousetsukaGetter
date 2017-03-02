using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetukaGetterLib
{
    class UrlClass
    {
        public string Url
        {
            private set;
            get;
        }
        private const string defaultUrl = "http://api.syosetu.com/novelapi/api/?out=json";
        private string tmpUrl;

        public UrlClass()
        {
            tmpUrl = defaultUrl;
        }
        /*
         * コンストラクタ(url指定)
         * 
         * */
        public UrlClass(string url)
        {
            tmpUrl = defaultUrl + url;
        }
        /*
         * 出力数を変更する。
         * (最低1,最大500)
         * 
         * */
        public void LimSetter(int num, bool initialization = false)
        {
            LimRange(num);
            if (initialization)
            {
                tmpUrl = defaultUrl + "&lim=" + num;
            }
            else
            {
                tmpUrl = tmpUrl + "&lim" + num;
            }
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
        public void StSetter(int num, bool initialization = false)
        {
            StRange(num);
            if (initialization)
            {
                tmpUrl = defaultUrl + "&st=" + num;
            }
            else
            {
                tmpUrl = tmpUrl + "&st" + num;
            }
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
        public void Order(string order, bool initialization = false)
        {
            if (initialization)
            {
                tmpUrl = defaultUrl + "&order=" + order;
            }
            else
            {
               tmpUrl = tmpUrl + "&order=" + order;
            }
        }


    }
}
