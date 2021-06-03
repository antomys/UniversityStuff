/*
 * linearIndex.cpp
 *
 *  Created on: Feb 11, 2020
 *      Author: vlad
 */

#include <iostream>
#include "linearIndex.hpp"

// Linearizes the coordinates of an element.
long long Linearizer::lIndex(int Q // maximal amount of elements in one dimensions
							, int axes // amount of dimensions in the array
							, int *ijk) // pointer to the array of coordinates (from outer to inner dimensions respectively).
{
	long long resultIndex = 0;
	for (int i = 0; i < axes; i++)
	{
		resultIndex = resultIndex * Q + (*(ijk + i));
	}
	return resultIndex;
}
