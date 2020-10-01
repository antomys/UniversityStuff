// IntegratingTrapedoizMethod.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <math.h>
#include <cmath>
#define PI 3.141596
using namespace std;

double fun(double x)
{
	//return (-3*x*x-2*x+9);
	return (sin((3 * x) / 2) + 1 / 2.0);
}
void TrapezeIntegral()//метод трапеций
{
	double a = 0, b = 1.0, h, x, S = 0, eps = 1e-4, fa, fb, f;
	int i, n = 1000;
	cout << "Enter [a;b]\na=";
	cin >> a;
	cout << "Entered: " << a << "\nEnter b= "; cin >> b;
	cout << "Entered: "<<b<<"\nAccuracy is (eps) " << eps;
	cout << "\nEnter steps of integrating(current is:  " << n << "):";
	cin >> n;
	x = a;
	h = (b - a) / n;
	fa = fun(a);
	fb = fun(b);
	f = h * (fa + fb) / 2.;
	for (i = 1; i < n; i++)
	{
		x = a + i * h;
		f += h * fun(x);
	}
	cout << "Trapezion Integral = " << f << endl;
}
void ZalyshIntegral()//метод оценки остаточных
{
	double a = 0, b = 1.0, h, x, S = 0, eps = 1e-4, fa, fb, f;
	int i, n = 1867; //3733;
	cout << "Enter [a;b]\na=";
	cin >> a;
	cout << "Entered: " << a << "\nEnter b= "; cin >> b;
	cout << "Entered: " << b << "\nAccuracy is (eps) " << eps;
	cout << "\nEnter steps of integrating = 2n =" << n << "):";
	x = a;
	h = (b - a) / n;
	fa = fun(a);
	fb = fun(b);
	f = h * (fa + fb) / 2.;
	for (i = 1; i < n; i++)
	{
		x = a + i * h;
		f += h * fun(x);
	}
	cout << "Zalyshkovyi Integral = " << f << endl;
}



void RectangleIntegral()// вычисление интеграла методом прямоугольников
{
	double a = 0, b = 1.0, h, x, S = 0, eps = 1e-4, f = 0;
	int i, n = 1000;
	cout << "Enter [a;b]\na=";
	cin >> a;
	cout << "Entered: " << a << "\nEnter b= "; cin >> b;
	cout << "Entered: " << b << "\nAccuracy is (eps) " << eps;
	cout << "\nEnter steps of integrating(current is:  " << n << "):";
	cin >> n;

	x = a;
	h = (b - a) / n;
	for (i = 0; i <= n; i++)
	{
		x = a + (i - 0.5)*h;
		f += h * fun(x);
	}
	cout << "Rect Integral = " << f << endl;
}
void RectZalyshIntegral()// вычисление интеграла методом прямоугольников
{
	double a = 0, b = 1.0, h, x, S = 0, eps = 1e-4, f = 0;
	int i, n = 170;
	cout << "Enter [a;b]\na=";
	cin >> a;
	cout << "Entered: " << a << "\nEnter b= "; cin >> b;
	cout << "Entered: " << b << "\nAccuracy is (eps) " << eps;
	cout << "\nEnter steps of integrating(current is:  " << n << "):";
	cin >> n;

	x = a;
	h = (b - a) / n;
	for (i = 0; i <= n; i++)
	{
		x = a + (i - 0.5)*h;
		f += h * fun(x);
	}
	cout << "Rect Zalysh Integral = " << f << endl;
}
int main()
{
	TrapezeIntegral();
	RectangleIntegral();
	ZalyshIntegral();
	RectZalyshIntegral();
	system("pause");
	return 0;
}





// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
