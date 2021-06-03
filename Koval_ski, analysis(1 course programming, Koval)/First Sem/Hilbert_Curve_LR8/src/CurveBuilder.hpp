/*
 * CurveBuilder.hpp
 *
 *  Created on: Nov 28, 2019
 *      Author: vlad
 */

#ifndef CURVEBUILDER_HPP_
#define CURVEBUILDER_HPP_
#ifndef vector
#include <vector>
#endif
class CurveBuilder
{
private:
	std::vector <std::vector <std::pair <int, int>>> array2d;
	std::vector <std::vector <std::pair  <bool, int>>> canvas;
	int currentPositionX;
	int currentPositionY;
	int counter = 0;
public:
	void build(int n);
	int mod4(int number, int add);
	int hilbertMinus(int level, int direction);
	int hilbertPlus(int level, int direction);
	int dot();
	int connector(int direction);
	bool inRange(int x, int y);
};
#endif /* CURVEBUILDER_HPP_ */
