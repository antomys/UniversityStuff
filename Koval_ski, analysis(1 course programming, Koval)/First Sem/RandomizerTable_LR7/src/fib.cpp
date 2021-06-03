/*
 * fib.cpp
 *
 *  Created on: Oct 11, 2019
 *      Author: vlad
 */

#include "fib.hpp"
#include <iostream>

// set modulo
void Fibonacci::setM (int M)
{
	this -> M = M;
}

// next fibonacci number
int Fibonacci::fib2 ()
{
	f0_2 = f1_2  % M;
	f1_2 = f2_2 % M;
	f2_2 = (f0_2 + f1_2) % M;
	if (f0_2 >= 0) return f0_2;
	else return -1;
}

// reset the sequence
void Fibonacci::resetFib2 (int f00, int f11, int f22)
{
	f0_2 = f00;
	f1_2 = f11;
	f2_2 = f22;
}

// reset to default values
void Fibonacci::setFib2 ()
{
	f0_2 = 0;
	f1_2 = 1;
	f2_2 = 1;
}


long long Fibonacci::fib3()
{
	f0_3 = f1_3;
	f1_3 = f2_3;;
	f2_3 = f3_3;
	f3_3 = (f0_3+f1_3+f2_3);
//	printf ("f1 %lld f2 %lld f3 %lld  f4 %lld \n", f0_3, f1_3, f2_3, f3_3);
	f0_3_mod = f1_3_mod % M;
	f1_3_mod = f2_3_mod % M;
	f2_3_mod = f3_3_mod % M;
	f3_3_mod = (f0_3_mod + f1_3_mod + f2_3_mod) % M;
//	printf ("mod: f1 %lld f2 %lld f3 %lld  f4 %lld \n", f0_3_mod, f1_3_mod, f2_3_mod, f3_3_mod);
	return f0_3_mod;
}

void Fibonacci::moveBackFib3()
{
	f3_3 = f2_3;
	f2_3 = f1_3;
	f1_3 = f0_3;
	f0_3 = f3_3-(f2_3+f1_3);
	// if statements to prevent undefined behaviour
	if (f3_3 > 0) f3_3_mod = f3_3 % M;
	if (f2_3 > 0) f2_3_mod = f2_3 % M;
	if (f1_3 > 0) f1_3_mod = f1_3 % M;
	if (f0_3 > 0) f0_3_mod = f0_3 % M;
}

void Fibonacci::setFib3()
{
	f0_3 = 0;
	f1_3 = 1;
	f2_3 = 1;
	f3_3 = 2;
	f0_3_mod = 0;
	f1_3_mod = 1;
	f2_3_mod = 1;
	f3_3_mod = 2;
}
