// ConsoleApplication1.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include "pch.h"
#include <iostream>
#include <vector>



using namespace std;



int main()
{
	vector<double> x = { -1,0,1 };
	cout << "x:" << x[0]<<"," << x[1]<<"," << x[2]<<endl;
	vector<double> y = { 2, 1, 2 };
	cout << "f(x):" << y[0] << "," << y[1] << "," << y[2] << endl;
	vector<double> y1 = { -6,0,6 };
	cout << "f'(x):" << y1[0] << "," << y1[1] << "," << y1[2] << endl;
	vector<double> y2 = { 30,0,30 };
	cout << "f''(x):" << y2[0] << "," << y2[1] << "," << y2[2] << endl;
	vector<double> y3 = { -120, 0,120 };
	cout << "f'''(x):" << y3[0] << "," << y3[1] << "," << y3[2] << endl;
	vector<double> y4 = { 360,0,360 };
	cout << "f''''(x):" << y4[0] << "," << y4[1] << "," << y4[2] << endl;
	vector<double> z = { -1,-1,-1,-1,-1,0,0,0,0,0,1,1,1,1,1 };
	vector<double> yz = { 2,2,2,2,2,1,1,1,1,1,2,2,2,2,2 };
	vector<double> y1z = { -6,-6,-6,-6,-6,0,0,0,0,0,6,6,6,6,6 };
	vector<double> y2z = { 30,30, 30, 30, 30, 0,0,0,0,0,30, 30, 30, 30, 30, };
	vector<double> y3z = { -120, -120, -120, -120, -120, 0,0,0,0,0,-120, -120, -120, -120, -120, };
	vector<double> y4z = { 360,  360,  360,  360,  360, 0,0,0,0,0, 360,  360,  360,  360,  360, };
	vector<double> a;
	for (int j = 0; j < 15; j++)
	{
		a.push_back(0.);
	}
	vector<vector<double>> A;
	
	for (int i = 0; i < 15; i++)
	{
		A.push_back(a);
	}
		for (int j = 0; j < 15; j++)
		{
			A[j].push_back(0.);
	     }

	for (int i = 0; i < 15; i++) 
	{
		A[i][0]=(yz[i]);
		
	}
	for (int i = 1; i < 15; i++) 
	{
		for (int j = 0; j < 15; j++)
		{
			if (j <= (14 - i) )
			{
				if (i == 1 && A[j][i - 1] == A[j + 1][i - 1])
				{
					A[j][i]=(y1z[j]);
				}
				if (i == 2 && A[j][i - 1] == A[j + 1][i - 1])
				{
					A[j][i] = (y2z[j])/2;
				}
				if (i == 3 && A[j][i - 1] == A[j + 1][i - 1])
				{
					A[j][i] = (y3z[j])/6;
				}
				if (i == 4 && A[j][i - 1] == A[j + 1][i - 1])
				{
					A[j][i] = (y4z[j])/24;
				}
				if (A[j][i - 1] != A[j + 1][i - 1]) {
					A[j][i]=((A[j + 1][i - 1] - A[j][i - 1]) / (z[j + i] - z[j]));
				}
			}
			else if ((j > (14 - i)))
			{
				A[j][i]=0;
			}
		}

	}
	vector<double> koef;
	for(int i = 0; i < 15; i++ ) {
		koef.push_back(A[0][i]);
	}
	
	
cout <<"Fn(x)="<< koef[0] << koef[1] << "(x+1)" <<" +"<< koef[2] << "(x+1)^2" << koef[3] << "(x+1)^3" << "+"<< koef[4] << "(x+1)^4" << koef[5] << "(x+1)^5" << "+"<< koef[6] << "x(x+1)^5" << koef[13] << "x^5(x+1)^5(x-1)^3" << "+"<< koef[14] << "x^5(x+1)^5(x-1)^4"<<endl;

}

