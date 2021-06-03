/*
 * loopSearch.cpp
 *
 *  Created on: Oct 11, 2019
 *      Author: vlad
 */
#line 8 "rnd.hpp"
#include "rnd.hpp"
#line 10 "iostream.hpp"
#include <iostream>
using namespace std;

int findLoop (int M)
{
	// initialize random number generators
	Rnd r0;

	// set the modulo
	r0.setMRnd(M);

	// set the initial values
	int f0 = r0.rnd();
	int f1 = r0.rnd();
	int f2 = r0.rnd();
	int counter = 3;
	// find cycle
	do
	{
		f0 = f1;
		f1 = f2;
		f2 = r0.rnd();
		//cout << f0 << ' ' << f1 << ' ' << f2 << ' ' << endl;
		counter++;
	} while (f0 != 1 || f1 != 1);
	return counter-3;
}
//r1.rnd ();
