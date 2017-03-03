using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SyousetukaGetterLib
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            var url = new NovelUrlManager();
            string[] tmp = url.Novel(4);

            WebClient wc = new WebClient();

            Stream st = wc.OpenRead(tmp[0]);
            Encoding enc = Encoding.GetEncoding("UTF-8");
            StreamReader sr = new StreamReader(st, enc);
            string html = sr.ReadToEnd();
            sr.Close();
            st.Close();
            Console.WriteLine(html);
            Console.ReadLine();

        }
    }
}
