/*
 * MonteCarlo.cpp
 *
 *  Created on: Dec 5, 2019
 *      Author: vlad
 */

#ifndef l
#define l 400;
#endif

#include <iostream>
#include <iomanip>
#include "MonteCarlo.hpp"
#include "square.hpp"
#include "circle.hpp"

using namespace std;

// circle inside of square

void MonteCarlo::start()
{
	int length = l;
	Square square (0, 0, length, length);
	Circle circle (length / 2 , length / 2 , 100);
	double squareArea = square.calculateS();
	double circleArea = circle.calculateS();
	//cout << squareArea << ' ' << circleArea << endl;
	srand (time(0));
	double l2 = length*2;
	double l5 = length*5;
	double delta = (l5-l2) / 100;
	while (l2 < l5)
	{
		int amountOfGeneratedDots = 0;
		int dotsThatAreInsideCircle = 0;
		int dotsThatAreInsideSquare = 0;
		for (int i = 0; i < l2; i++)
		{
			int dotX = rand () % (length);
			int dotY = rand () % (length);
			if (circle.checkIfInside(dotX, dotY)) dotsThatAreInsideCircle++;
			if (square.checkIfInside(dotX, dotY)) dotsThatAreInsideSquare++;
			amountOfGeneratedDots++;
		}
		double circleMCArea = (double)(dotsThatAreInsideCircle*square.calculateS())/(double)dotsThatAreInsideSquare;
		cout << setprecision(5);
		cout << fixed;
		cout << amountOfGeneratedDots << ' ' << ((double)abs(circle.calculateS()-circleMCArea)/(double)circle.calculateS())*100 << endl;
		l2+=delta;
		//cout <<"Amount of dots:" << l2 << endl;
	}
}
