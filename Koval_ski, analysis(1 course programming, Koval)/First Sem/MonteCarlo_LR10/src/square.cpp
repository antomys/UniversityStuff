/*
 * square.cpp
 *
 *  Created on: Dec 5, 2019
 *      Author: vlad
 */

#include <iostream>
#include "square.hpp"

Square::Square(int x1, int y1, int x4,int y4)
{
	this -> x1 = x1;
	this -> y1 = y1;
	this -> x4 = x4;
	this -> y4 = y4;
}

double Square::calculateS ()
{
	return (x4-x1)*(y4-y1);
}

bool Square::checkIfInside (int xCoord, int yCoord)
{
	return (xCoord >= x1 && xCoord <= x4 && yCoord >= x1 && yCoord <= x4);
}

