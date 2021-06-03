/*
 * specter2HTML.hpp
 *
 *  Created on: Feb 7, 2020
 *      Author: vlad
 */
#ifndef SPECTER2HTML_HPP_
#define SPECTER2HTML_HPP_


class Specter2HTML
{
public:
	std::string specter2String (int* specter, int axes, int maxF);
	int specter2cout (int* specter, int axes, int maxF);
	int specter2File (int* specter, int axes, int maxF, std::string fileName);
private:
	std::string buildTable(int* array, int maxF, int recursionDepth, int index);
	int counter = 0;
};



#endif /* SPECTER2HTML_HPP_ */
