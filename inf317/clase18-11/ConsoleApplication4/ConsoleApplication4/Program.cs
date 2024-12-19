using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 8;
            Thread hilo = new Thread(() => fibo(n));
            hilo.Start();
            Console.ReadKey();
        }

        static void fibo(int n)
        {
            double termino1, termino2;
            termino1 = (1 / Math.Sqrt(5)) * Math.Pow((1 + Math.Sqrt(5)) / 2, n);
            termino2 = (1 / Math.Sqrt(5)) * Math.Pow((1 - Math.Sqrt(5)) / 2, n);
            Console.WriteLine((termino1 - termino2).ToString());
        }

    }
}
