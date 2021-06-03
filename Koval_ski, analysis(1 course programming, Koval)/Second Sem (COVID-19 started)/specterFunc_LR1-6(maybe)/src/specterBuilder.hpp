/*
 * functionItself.hpp
 *
 *  Created on: Jan 29, 2020
 *      Author: vlad
 */

#ifndef SPECTERBUILDER_HPP_
#define SPECTERBUILDER_HPP_

#ifndef FUNCTION_HPP_
#include "function.hpp"
#endif

class Specter
{
public:
	operator std::string() const;
	bool operator == (Specter& that);

	Specter (int length, int maxF, int axes, int* specterArray);
	Specter (const Specter& oldSpecter);
	int buildSpecter(Function& function);
	std::string specter2String ();
	int specter2cout ();
	int specter2File (std::string fileName);
private:
	int counter = 0;
	int length;
	int maxF;
	int axes;
	int* specter;
	long long totalElements;
	std::string buildTable(int recursionDepth, int index);
	int GCD(int a, int b);
	int LCM (int a, int b);
	bool compareSpecters (Specter& sp1, Specter& sp2);
};



#endif /* SPECTERBUILDER_HPP_ */
