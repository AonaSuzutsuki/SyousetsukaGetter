using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyousetukaGetterLib
{
    public class MainClass
    {
        static void Main(string[] args) {
            var url = new UrlManager();
            var jsonLoader = new JsonLoader(url.Url);
            string tmp = jsonLoader.GetValue(2, "title");
            Console.WriteLine(tmp);
            Console.ReadLine();
        }
    }
}
