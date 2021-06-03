// ChisMethods.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <algorithm>
#include <cmath>

using namespace std;

const int MAX_VECTOR = 4;

void printVector(double x[MAX_VECTOR])
{
	for (int i = 0; i < MAX_VECTOR; i++)
		cout << x[i] << " ";
	cout << "\n";
}

void perev(double m[MAX_VECTOR][MAX_VECTOR])
{
	for (int i = 0; i < MAX_VECTOR / 2; i++)
		for (int j = 0; j < MAX_VECTOR; j++)
			swap(m[i][j], m[MAX_VECTOR - 1 - i][MAX_VECTOR - 1 - j]);
	if (MAX_VECTOR % 2)
		reverse(&m[MAX_VECTOR / 2][0], &m[MAX_VECTOR / 2][MAX_VECTOR]);
}

void metodGauss(const double mp[MAX_VECTOR][MAX_VECTOR], const double vp[MAX_VECTOR], double x[MAX_VECTOR])
{
	double m[MAX_VECTOR][MAX_VECTOR], v[MAX_VECTOR];
	for (int i = 0; i < MAX_VECTOR; i++)
	{
		for (int j = 0; j < MAX_VECTOR; j++)
			m[i][j] = mp[i][j];
		v[i] = vp[i];
	}

	for (int pass = 0; pass < 2; pass++)
	{
		for (int k = 0; k < MAX_VECTOR - 1; k++)
			for (int j = k + 1; j < MAX_VECTOR; j++)
			{
				double c = -m[k][k] / m[j][k];
				for (int i = k; i < MAX_VECTOR; i++)
					m[j][i] = c * m[j][i] + m[k][i];
				v[j] = c * v[j] + v[k];
			}
		perev(m);
		reverse(&v[0], &v[MAX_VECTOR]);
	}

	for (int i = 0; i < MAX_VECTOR; i++)
		x[i] = v[i] / m[i][i];
}

bool iter_escape(double x1[MAX_VECTOR], double x2[MAX_VECTOR], double e)
{
	for (int i = 0; i < MAX_VECTOR; i++)
		if (fabs(x1[i] - x2[i]) > e)
			return false;
	return true;
}

int metodZeidel(const double m[MAX_VECTOR][MAX_VECTOR],
	const double v[MAX_VECTOR], double x[MAX_VECTOR], const double e)
{
	int iter = 0;
	double sum, xp[MAX_VECTOR];

	for (int i = 0; i < MAX_VECTOR; x[i++] = 0);
	do
		for (int i = 0; i < MAX_VECTOR; i++)
		{
			for (int j = sum = 0; j < MAX_VECTOR; j++)
				if (i != j)
					sum += m[i][j] * x[j];
			xp[i] = x[i];
			x[i] = (v[i] - sum) / m[i][i];
		}
	while (iter++, !iter_escape(x, xp, e));
	return iter;
}
double Matrix_Norm_First(double A[MAX_VECTOR][MAX_VECTOR], int n) {
	double sum = 0;
	double maxi;
	for (int j = 0; j < n; j++) {
		for (int i = 0; i < n; i++) {
			sum += abs(A[i][j]);
		}
		maxi = max(maxi, sum);
		sum = 0;
	}
	return maxi;

}
double Matrix_Norm_Infinite(double A[MAX_VECTOR][MAX_VECTOR], int n) {
	double sum = 0;
	double maxi;
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < n; j++) {
			sum += abs(A[i][j]);
		}
		maxi = max(maxi, sum);
		sum = 0;
	}
	return maxi;
}
double Matrix_Norm_Frobenius(double A[MAX_VECTOR][MAX_VECTOR], int n) {
	double sum = 0;
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < n; j++) {
			sum += A[i][j] * A[i][j];
		}
	}
	sum = sqrt(sum);
	return sum;
}
void Sort()
{
	for (int i = 0; i < 100; i++)
		cout << '=';
	cout << endl;
}



int main()
{
	int i, j;
	int det_res;
	int iter = 0;
	double det = 619.5321;
	double matrix[MAX_VECTOR][MAX_VECTOR] = 
	{
		4.1, 0.1, 0.2, 0.2,
		0.3, 5.3, 0.9, 0.1,
		0.2, 0.3, 3.2, 0.2,
		0.1, 0.1, 0.2, 9.1
	};
	double obernena_matrix[MAX_VECTOR][MAX_VECTOR] = {
		0.2449, -0.0037, -0.0139, -0.0050,
		-0.0114, 0.1919,-0.0053, -0.0006,
		-0.0140,-0.0176,0.3187,-0.0065,
		-0.0022,-0.0016,-0.0062, 0.1100
	};

	cout << "Matrix_Norm: " << Matrix_Norm_First(matrix, MAX_VECTOR) << endl; 
	cout << "Matrix_Infinite: " << Matrix_Norm_Infinite(matrix, MAX_VECTOR) << endl; 
	cout << "Matrix_Phlobiana: " << Matrix_Norm_Frobenius(matrix, MAX_VECTOR) << endl; 
	Sort();
	
	cout << "INVERTED:\n";
	cout << "Matrix_Norm: " << Matrix_Norm_First(obernena_matrix, MAX_VECTOR) << endl;
	cout << "Matrix_Infinite: " << Matrix_Norm_Infinite(obernena_matrix, MAX_VECTOR) << endl;
	cout << "Matrix_Phlobiana: " << Matrix_Norm_Frobenius(obernena_matrix, MAX_VECTOR) << endl;
	Sort();

	cout << "Number of conditionality:" << endl;
	cout << "Norm: " << 3.39157 << endl;
	cout << "Infinite: " << Matrix_Norm_Infinite(obernena_matrix, MAX_VECTOR)*Matrix_Norm_Infinite(matrix, MAX_VECTOR) << endl;
	cout << "Flobiana: " << Matrix_Norm_Frobenius(obernena_matrix, MAX_VECTOR)*Matrix_Norm_Frobenius(matrix, MAX_VECTOR) << endl;
	Sort();
	
		cout << "Determinant = " << det<<endl;
	Sort();

	double x[MAX_VECTOR], vect[MAX_VECTOR] = { 21.14, -17.82, 9.02, 17.08 };
	cout << "Vector =  "; printVector(x);
	Sort();
	
	metodGauss(matrix, vect, x);
	cout << "Gauss: ";
	printVector(x);
	cout << "\n";
	Sort();
	iter = metodZeidel(matrix, vect, x, 0.000000001);
	cout << "Zeidel : " << "counts: " << iter << endl;
	printVector(x);
	cout << "\n";
	Sort();
	return 0;
}