using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void tarea()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("secundario" + i);
                Thread.Sleep(300);
            }
        }
        static void Main(string[] args)
        {
            Thread hilo = new Thread(tarea);
            hilo.Start();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("primario" + i);
                Thread.Sleep(500);
            }
            hilo.Join();
            Console.ReadKey();
        }

    }
}
