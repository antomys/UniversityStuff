/*
 * loopSearch.hpp
 *
 *  Created on: Oct 17, 2019
 *      Author: vlad
 */

#ifndef LOOPSEARCH_HPP_
#define LOOPSEARCH_HPP_
#include <vector>

class loopSearch
{
public:
	std::vector<int> arrayOfInts;
	loopSearch (int M);
	int findLoop (int M);
	void assoc (int lengthOfLoop, int M);
	int findMax (int M);
};

#endif /* LOOPSEARCH_HPP_ */
