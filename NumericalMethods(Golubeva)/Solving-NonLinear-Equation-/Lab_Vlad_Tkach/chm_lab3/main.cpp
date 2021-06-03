#include <iostream>
#include <cmath>
#include <vector>

double f1(double x, double y, double z)
{
	return 3 * x * x + 1.5 * y * y + z * z - 5;
}

double f2(double x, double y, double z)
{
	return 6 * x * y * z - x + 5 * y + 3 * z;
}

double f3(double x, double y, double z)
{
	return 5 * x * z - y * z - 1;
}


double f1_x(double x, double y, double z)
{
	return 6 * x;
}

double f2_x(double x, double y, double z)
{
	return 6 * y * z - 1;
}

double f3_x(double x, double y, double z)
{
	return 5 * z;
}


double f1_y(double x, double y, double z)
{
	return 3 * y;
}

double f2_y(double x, double y, double z)
{
	return 6 * x * z + 5;
}

double f3_y(double x, double y, double z)
{
	return -z;
}


double f1_z(double x, double y, double z)
{
	return 2 * z;
}

double f2_z(double x, double y, double z)
{
	return 6 * y * x + 3;
}

double f3_z(double x, double y, double z)
{
	return 5 * x - y;
}


double det2(std::vector<std::vector<double>> mat)
{
	return mat[0][0] * mat[1][1] - mat[0][1] * mat[1][0];
}

double det3(std::vector<std::vector<double>> mat)
{
	double det_1 = mat[0][0] * det2({ { mat[1][1], mat[1][2]}, { mat[2][1], mat[2][2] } });
	double det_2 = mat[0][1] * det2({ { mat[1][0], mat[1][2]}, { mat[2][0], mat[2][2] } });
	double det_3 = mat[0][2] * det2({ { mat[1][0], mat[1][1]}, { mat[2][0], mat[2][1] } });

	return det_1 - det_2 + det_3;
}


int main()
{
	double epsilon = 0.001;

	double x0 = 1.2;
	double y0 = 1.3;
	double z0 = 1.6;

	double xn = x0;
	double yn = y0;
	double zn = z0;

	double dx = 0;
	double dy = 0;
	double dz = 0;

	int i = 0;

	do {
		double a00 = f1_x(xn, yn, zn);
		double a01 = f1_y(xn, yn, zn);
		double a02 = f1_z(xn, yn, zn);

		double a10 = f2_x(xn, yn, zn);
		double a11 = f2_y(xn, yn, zn);
		double a12 = f2_z(xn, yn, zn);

		double a20 = f3_x(xn, yn, zn);
		double a21 = f3_y(xn, yn, zn);
		double a22 = f3_z(xn, yn, zn);

		double b0 = -f1(xn, yn, zn);
		double b1 = -f2(xn, yn, zn);
		double b2 = -f3(xn, yn, zn);

		double det = det3({
			{ a00, a01, a02 },
			{ a10, a11, a12 },
			{ a20, a21, a22 }
			});

		dx = det3({
			{ b0, a01, a02 },
			{ b1, a11, a12 },
			{ b2, a21, a22 }
			}) / det;
		dy = det3({
			{ a00, b0, a02 },
			{ a10, b1, a12 },
			{ a20, b2, a22 }
			}) / det;
		dz = det3({
			{ a00, a01, b0 },
			{ a10, a11, b1 },
			{ a20, a21, b2 }
			}) / det;

		xn += dx;
		yn += dy;
		zn += dz;

		i += 1;

		std::cout << "x" << i << " " << xn << " y" << i << " " << yn << " z" << i << " " << zn << "\n";

	} while (abs(dx) > epsilon || abs(dy) > epsilon || abs(dz) > epsilon);

	return 0;
}