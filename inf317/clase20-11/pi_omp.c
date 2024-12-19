#include <stdio.h>
#include <omp.h>

double f(double x) {
    return 4.0 / (1.0 + x * x);
}

int main() {
    int n = 1000000; 
    double a = 0.0, b = 1.0; 
    double h = (b - a) / n; 
    double total_sum = 0.0;

    int num_threads = 3;

    #pragma omp parallel num_threads(num_threads)
    {
        int id = omp_get_thread_num();
        int num_threads = omp_get_num_threads();
        int tamanio_del_bloque = n / num_threads;

        int start = id * tamanio_del_bloque;
        int end = (id == num_threads - 1) ? n : start + tamanio_del_bloque;

        double local_sum = 0.0;
        for (int i = start; i < end; i++) {
            double x = (i + 0.5) * h;
            local_sum += f(x) * h;
        }

        #pragma omp atomic
        total_sum += local_sum;
    }

    printf("Pi %f", total_sum);
    return 0;
}