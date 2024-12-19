using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string cadena;
            double fn;
            Console.WriteLine("Escribe un n");
            cadena = System.Console.ReadLine();
            fn = fibo(Convert.ToInt32(cadena));
            Console.WriteLine(Convert.ToInt32(fn).ToString());
            Console.ReadKey();
        }

        static double fibo(int n)
        {
            double termino1, termino2;
            termino1 = (1 / Math.Sqrt(5)) * Math.Pow((1 + Math.Sqrt(5)) / 2, n);
            termino2 = (1 / Math.Sqrt(5)) * Math.Pow((1 - Math.Sqrt(5)) / 2, n);
            return termino1 - termino2;
        }
    }
}
