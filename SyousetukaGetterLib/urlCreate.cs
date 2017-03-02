using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetukaGetterLib
{
    class urlCreate
    {
        private string url;
        private const string defaultUrl = "http://api.syosetu.com/novelapi/api/?out=json";

        public urlCreate()
        {
            url = defaultUrl;
        }
        /*
         * コンストラクタ(url指定)
         * 
         * */
        public urlCreate(string url)
        {
            this.url = defaultUrl + url;
        }
        /*
         * urlを取得。
         * 
         * */
         public string getterUrl()
         {
            return url;
         }
         /*
          * 出力数を変更する。
          * (最低1,最大500)
          * 
          * */
          public string limSetter(int num,bool initialization = false)
          {
            if (initialization)
            {
                return defaultUrl + "&lim=" + num;
            }
            else
            {
                return url + "&lim" + num;
            }
          }
    }
}
