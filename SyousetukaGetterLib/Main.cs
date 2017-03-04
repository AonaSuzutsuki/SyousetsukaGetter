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
            var tmp = new NovelDownloader("n9669bk", 1, true);
            tmp.DownloadNovel();
            tmp.SetData("n6705dv", 1, false);
            tmp.DownloadNovel();
            foreach (string text in tmp.NovelText["n9669bk"])
            {
                foreach (string title in tmp.Title["n9669bk"])
                {
                    Console.WriteLine(title);
                    Console.WriteLine("+++++++++++++++++++++++++++++++");
                    Console.WriteLine(text);
                    Console.WriteLine("********************************");
                }
            }
            //foreach (string text in tmp.noveltext["n6705dv"])
            //{
            //    foreach (string title in tmp.title["n2627t"])
            //    {
            //        console.writeline(title);
            //        console.writeline("+++++++++++++++++++++++++++++++");
            //        console.writeline(text);
            //        console.writeline("********************************");
            //    }
            //}
            Console.Read();
        }
    }
}
