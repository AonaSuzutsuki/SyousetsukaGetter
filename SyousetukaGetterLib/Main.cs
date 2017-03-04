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
            var tmp = new NovelDownloader("n6924dv", 1, false);
            tmp.DownloadNovel();
            foreach (string text in tmp.NovelText["n6924dv"])
            {
                Console.WriteLine(text);
                Console.WriteLine("********************************");
            }
            Console.Read();
        }
    }
}
