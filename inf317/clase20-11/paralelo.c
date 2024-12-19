#include <mpi.h>
#include <stdio.h>
#include <math.h>

double f(double x) {
    return 4.0 / (1.0 + x * x);
}

int main(int argc, char** argv) {
    int rank, size, n = 1000000;
    double a = 0.0, b = 1.0;
    double h = (b - a) / n;
    double local_sum = 0.0, total_sum = 0.0;
    
    MPI_Init(&argc, &argv);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    if (size != 3) {
        if (rank == 0) {
            printf("Se necesita 3 procesadores.\n");
        }
        MPI_Finalize();
        return 1;
    }

    int local_n = n / size;
    double local_a = a + rank * local_n * h;
    double local_b = local_a + local_n * h;

    for (int i = 0; i < local_n; i++) {
        double x = local_a + i * h + h / 2.0;
        local_sum += f(x) * h;
    }

    if (rank == 0) {
        total_sum = local_sum;

        // Recibir los resultados de otros procesos
        for (int i = 1; i < size; i++) {
            double recv_sum;
            MPI_Recv(&recv_sum, 1, MPI_DOUBLE, i, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
            total_sum += recv_sum;
        }

        printf("Pi %f\n", total_sum);

    } else {
        MPI_Send(&local_sum, 1, MPI_DOUBLE, 0, 0, MPI_COMM_WORLD);
    }

    MPI_Finalize();
    return 0;
}