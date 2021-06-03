/*
 * fib.cpp
 *
 *  Created on: Oct 11, 2019
 *      Author: vlad
 */

#line 9 "climits.hpp"
#include <climits>
#line 11 "fib.hpp"
#include "fib.hpp"

// next fibonacci number
int Fibonacci::fib ()
{
	f0 = f1  % M;
	f1 = f2 % M;
	f2 = (f0 + f1) % M;
	return f0;
}

// reset the sequence
void Fibonacci::resetFib (int f00, int f11, int f22)
{
	f0 = f00;
	f1 = f11;
	f2 = f22;
}

// reset to default values
bool Fibonacci::setFib ()
{
	bool OK = true;
	f0 = 0;
	f1 = 1;
	f2 = 1;
	return OK;
}

void Fibonacci::setM (int M)
{
	this -> M = M;
}
