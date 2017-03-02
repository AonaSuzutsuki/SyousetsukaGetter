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
            string url = "http://api.syosetu.com/novelapi/api/?out=json";
            var jsonLoader = new JsonLoader(url);
            string tmp = jsonLoader.GetValue(2, "title");
            Console.WriteLine(tmp);
            Console.ReadLine();
        }
    }
}
