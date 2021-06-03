/*
 * circle.cpp
 *
 *  Created on: Dec 5, 2019
 *      Author: vlad
 */

#include <iostream>
#include "circle.hpp"

Circle::Circle(int x1, int y1, int r)
{
	this -> r = r;
	this -> x1 = x1;
	this -> y1 = y1;
}

double Circle::calculateS()
{
	return 3.14 * (double)r * r;
}

bool Circle::checkIfInside(int xCoord, int yCoord)
{
	return (((xCoord-x1)*(xCoord-x1) + (yCoord-y1)*(yCoord-y1)) <= (r*r)) ;
}
