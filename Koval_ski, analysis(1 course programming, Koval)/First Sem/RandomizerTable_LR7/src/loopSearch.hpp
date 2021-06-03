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
	int M;
	loopSearch (int M);
	int findLoop ();
	void assoc (int lengthOfLoop);
	int findMax ();
};

#endif /* LOOPSEARCH_HPP_ */
