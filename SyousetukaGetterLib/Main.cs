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
            var test = new Linechang();
           
            Console.WriteLine(test.setterJson());
            Console.ReadLine();
        }
    }
}
