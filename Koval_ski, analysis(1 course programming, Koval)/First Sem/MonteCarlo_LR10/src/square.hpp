/*
 * square.hpp
 *
 *  Created on: Dec 5, 2019
 *      Author: vlad
 */

#ifndef SQUARE_HPP_
#define SQUARE_HPP_

class Square
{
public:
	int x1, y1, x4, y4;
	Square(int x1, int y1, int x4, int y4);
	double calculateS();
	bool checkIfInside(int xCoord, int yCoord);
};

#endif /* SQUARE_HPP_ */
