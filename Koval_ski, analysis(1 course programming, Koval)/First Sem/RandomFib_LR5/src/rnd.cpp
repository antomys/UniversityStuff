/*
 * rnd.cpp
 *
 *  Created on: Oct 11, 2019
 *      Author: vlad
 */

//R() = Fib () % M;
#line 10 "rnd.hpp"
#include  "rnd.hpp"
#line 12 "fib.hpp"
#include  "fib.hpp"
#line 14 "iostream.hpp"
#include <iostream>
using namespace std;


// initialize the modulo field and set the default values for Fibonacci sequence
Rnd::Rnd()
{
	this -> M = 20;
	fibo.setFib();
	fibo.setM (M);
}

// set the modulo
void Rnd::setMRnd (int m)
{
	M = m;
	fibo.setM (m);
}

// generate new "random" number
int Rnd::rnd ()
{
	int randomNum = fibo.fib ();
	return randomNum ;
}



