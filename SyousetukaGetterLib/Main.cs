using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetukaGetterLib
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            var url = new NovelUrlManager();
            string[] tmp = url.Novel(8);
            foreach (string a in tmp)
            {
                Console.WriteLine(a);
            }
            Console.ReadLine();
        }
    }
}
