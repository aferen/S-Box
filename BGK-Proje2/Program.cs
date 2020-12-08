using BGK_Proje2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGK_Proje2
{
    class Program
    {
        static void Main(string[] args)
        {
            Polynomial polynomial = new Polynomial();
            polynomial.solve();
            Map map = new Map();
            map.solve();
            Console.ReadKey();
        }
    }
}

