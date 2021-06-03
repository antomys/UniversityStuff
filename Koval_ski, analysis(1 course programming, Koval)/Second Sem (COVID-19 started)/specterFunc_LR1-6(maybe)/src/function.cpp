/*
 * function.cpp
 *
 *  Created on: Feb 17, 2020
 *      Author: vlad
 */

#include "function.hpp"

Function::Function(int (*fp)(void))
{
	(*this).fp = fp;
}

int Function::next()
{
	return fp();
}

void Function::reset()
{
}
