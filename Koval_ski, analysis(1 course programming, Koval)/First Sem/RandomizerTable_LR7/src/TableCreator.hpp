/*
 * TableCreator.hpp
 *
 *  Created on: Nov 24, 2019
 *      Author: vlad
 */

#ifndef TABLECREATOR_HPP_
#define TABLECREATOR_HPP_
#include <vector>
class TableCreator
{
public:
	int modulo;
	std::vector<std::vector<int>> array2D;
	TableCreator();
	void create();
};
#endif /* TABLECREATOR_HPP_ */
