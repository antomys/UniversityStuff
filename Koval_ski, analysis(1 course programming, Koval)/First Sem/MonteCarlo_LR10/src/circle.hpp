/*
 * circle.hpp
 *
 *  Created on: Dec 5, 2019
 *      Author: vlad
 */

#ifndef CIRCLE_HPP_
#define CIRCLE_HPP_

class Circle
{
public:
	int x1, y1, r;
	Circle(int x1, int y1, int r);
	double calculateS ();
	bool checkIfInside (int xCoord, int yCoord);
};


#endif /* CIRCLE_HPP_ */
