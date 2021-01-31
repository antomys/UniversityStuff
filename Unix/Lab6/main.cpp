#include <iostream>
#include <thread>
#include <mutex>
#include <time.h>

const int NMAX = 1000;
const int N = 10000000;
std::mutex mut;
int A[NMAX][NMAX];
int B[NMAX][NMAX];
int C[NMAX][NMAX];

void Matrix_Multiply(int i, int j, int s){
    C[i][j] = 0;
    for (int k = 0; k < s; k++){
        C[i][j] += A[i][k]*B[k][j];
    }
        printf("C[%d][%d] = %d\n",i,j,C[i][j]);
}


void Generator(int r, int c, int M[][NMAX]){
    srand(time(NULL));
    for (int i = 0; i < r; i++){
        for (int j = 0; j < c; j++){
            M[i][j] = rand()%200;
            printf("\t%d",M[i][j]);
        }
        printf("\n");
    }
}
void withoutLock(int* x) {
    for (int i = 0; i < N; i++) {
        (*x)++;
    }

}
void withLock(int* y) {

    for (int i = 0; i < N; i++) {
        mut.lock();
        (*y)++;
        mut.unlock();
    }


}
void fun()
{
    int RowA = 0;
    int RowB = 0;
    std::cout << "A: " << std::endl;
    std::cin >> RowA >> RowB;
    Generator(RowA, RowB, A);
    int colsB = 0;
    std::cout << "B: " << std::endl;
    std::cin >>  colsB;
    Generator(RowB, colsB, B);

    std::thread * th[RowA][colsB];
    for (int i = 0; i < RowA; i++){
        for (int j = 0; j < colsB; j++){
            th[i][j] = new std::thread(Matrix_Multiply, i, j, RowB);
        }
    }

    for (int i = 0; i < RowA; i++){
        for (int j = 0; j < colsB; j++){
            (*th[i][j]).join();
        }
    }
};

void notfun()
{
  clock_t start1 = clock();
    int* x = new int;
    *x = 0;
    std::thread thread1(withoutLock, x);
    std::thread thread2(withoutLock, x);
    thread1.join();
    thread2.join();
    std::cout << "Without lock" << "\n";
    std::cout << *x << std::endl;
    start1=clock()-start1;
    printf("%f\n", (double)start1/CLOCKS_PER_SEC);

     clock_t start2 = clock();
    int* y = new int;
    *y = 0;
    std::thread thread3(withLock, y);
    std::thread thread4(withLock, y);
    thread3.join();
    thread4.join();
    std::cout << "With lock" << "\n";
    std::cout << *y << "\n";
    start2=clock()-start2;
    printf("%f\n", (double)start2/CLOCKS_PER_SEC);



}
int main()
{
    while(true)
    {
        int choose;
        printf("Choose Lab Work, where %d is Generating and multiplying matrixes\nAnd %d is Threads parallel work", 22,
               23);
        printf("\nTime to choose: ");
        std::cin >> choose;
        switch(choose){
            case 22:
                fun();
                break;
            case 23:
                notfun();
                break;
        }
        return 0;
    }
}
