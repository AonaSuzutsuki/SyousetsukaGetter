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
            var jsonUrl = new JsonUrlManager();
            jsonUrl.SetOrder(JsonUrlManager.Order.favnovelcnt);
            var tmp = new PageDownloader(jsonUrl);
            tmp.StartDownload(1);
            tmp.GetText();
            Console.Read();
        }
    }
}
