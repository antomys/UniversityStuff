#ifndef JACOBI_H
#define JACOBI_H
#include <vector>
#include <iostream>
#include <cmath>
using namespace std;
namespace Jacobi {


    bool isConvergence(const vector<vector<float>> a)
    {
        const unsigned n = a.size();

        int sumDiagonal = 0;
        int sumTriangle = 0;

        for (int i = 0; i < n; ++i){
            for(int j = 0; j < n; ++j){
                if(i == j){
                    sumDiagonal += a[i][j];
                }else{
                    sumTriangle += a[i][j];
                }
            }
        }
        if(sumDiagonal > sumTriangle){
            return true;
        }
        return false;
    }

    void solve(const vector<vector<float>> a, //квадратна матриця
               const vector<float> b, //вектор вільних елементів
               vector<float>& x, //сюди буде записано розв'язок
               const float allowed_error) //допустима похибка
    {
        if(!isConvergence(a)){
            return;
        }
        const unsigned n = x.size();
        vector<float> tmp_x(n);

        float error;
        int count = 0;
        do
        {
            count++;
            error = 0;

            tmp_x = b;
            for (unsigned i = 0; i < n; ++i)
            {
                for (unsigned j = 0; j < n; ++j)
                {
                    if (i != j)
                    {
                        tmp_x[i] -= a[i][j] * x[j];
                    }
                }

                //оновити x[i] та порахувати похибку
                const float x_updated = tmp_x[i] / a[i][i];
                const float e = fabs(x[i] - x_updated);
                tmp_x[i] = x_updated;
                if (e > error) { error = e; }
            }
            x = tmp_x;
        }
        while (error > allowed_error);

        cout << "Iterations count " << count;
    }
}
#endif // JACOBI_H
