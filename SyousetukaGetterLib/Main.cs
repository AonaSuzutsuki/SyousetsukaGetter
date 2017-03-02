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
            var jsonLoader = new JsonLoader("test.txt", true);
            Console.WriteLine(jsonLoader.GetValue(1, "ncode"));
            Console.ReadLine();
        }
    }
}
