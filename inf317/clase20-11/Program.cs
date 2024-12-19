using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        static double[] results; 
        static int n = 1000000;  
        static double a = 0.0, b = 1.0; 
        static double h; 

        static void Main(string[] args)
        {

            int numThreads = 3;
            results = new double[numThreads];
            h = (b - a) / n; 

            Thread[] threads = new Thread[numThreads];

            for (int i = 0; i < numThreads; i++)
            {
                int threadId = i; 
                threads[i] = new Thread(() => calcular_suma_parcial(threadId, numThreads));
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            double totalSum = 0.0;
            foreach (var result in results)
            {
                totalSum += result;
            }

            Console.WriteLine("El valor aproximado de pi es "+ totalSum.ToString());
            Console.ReadKey();
        }

        static void calcular_suma_parcial(int threadId, int numThreads)
        {
            int tamanio_del_bloque = n / numThreads;
            int start = threadId * tamanio_del_bloque;
            int end = (threadId == numThreads - 1) ? n : start + tamanio_del_bloque;

            double parcialSuma = 0.0;
            for (int i = start; i < end; i++)
            {
                double x = (i + 0.5) * h;
                parcialSuma += 4.0 / (1.0 + x * x) * h;
            }

            results[threadId] = parcialSuma; 
        }
    }
}
