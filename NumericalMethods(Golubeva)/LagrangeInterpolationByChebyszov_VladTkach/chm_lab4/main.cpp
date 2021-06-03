#include <cmath>
#include <ctgmath>
#include <iostream>
#include <map>

#define pi 3.14159265358979323846
#define n  10

double f(double x)
{
	return pow(x, 3) - 2 * pow(x, 2) + x + 1 + cos(x);
}


double xi(int k)
{
	return cos(pi * (2 * k - 1) / (2 * n));
}


double xiab(int k)
{
	double a = -1;
	double b = 0;
	return (a + b) / 2 + xi(k) * (b - a) / 2;
}


double L(double x)
{
	double res = 0;

	for (int i = 0; i < n; i++) {
		double addendum = f(xi(i + 1));

		for (int j = 0; j < n; j++) {
			if (i == j)
				continue;
			addendum *= (x - xi(j + 1)) / (xi(i + 1) - xi(j + 1));
		}

		res += addendum;
	}

	return res;
}

double yi(int k)
{
	return f(xiab(k));
}

double Ly(double y)
{
	double res = 0;

	for (int i = 0; i < n; i++) {
		double addendum = xiab(i + 1);

		for (int j = 0; j < n; j++) {
			if (i == j)
				continue;
			addendum *= (y - yi(j + 1)) / (yi(i + 1) - yi(j + 1));
		}

		res += addendum;
	}

	return res;
}


int main()
{
	double x0      = 0.5;
	double epsilon = 0.0001;
	double a       = -1;
	double b       = 0;
	double xn      = x0;
	int    i       = 0;

	if (L(a) * L(b) < 0) {
		do {
			++i;
			xn = (a + b) / 2;

			if (L(a) * L(xn) < 0) {
				b = xn;
			}
			else {
				a = xn;
			}

			std::cout << "x" << i << " = " << xn << "\n";
		} while (abs(xn - (a + b) / 2) > epsilon);
	}

	std::cout << "\nx = " << xn << "\n\n\n";

	std::cout << "\nx = " << Ly(0) << "\n\n\n";

	return 0;
}