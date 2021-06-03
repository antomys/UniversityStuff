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
#line 12 "loopSearch.hpp"
#include "loopSearch.hpp"
using namespace std;

loopSearch :: loopSearch (int M)
{
	arrayOfInts.resize (M);
	this -> M = M;
}

int loopSearch::findLoop ()
{
	// initialize random number generators
	Rnd r0;
	// set the modulo
	r0.setMRnd(M);

	// set the initial values
	int f0 = r0.rnd();
	int f1 = r0.rnd();
	int f2 = r0.rnd();
	int f3 = r0.rnd();
	int counter = 4;
	// find cycle
	do
	{
		f0 = f1;
		f1 = f2;
		f2 = f3;
		f3 = r0.rnd();
		counter++;
	} while (f0 != 0 || f1 != 1 || f2 != 1);
	return counter-4;
}

void loopSearch::assoc(int length)
{

	Rnd randomNumberGeneratorAssoc;
	randomNumberGeneratorAssoc.setMRnd (M);

	for (int i = 0; i < M; i++)
	{
		arrayOfInts[i] = 0;
	}

	for (int i = 0; i < length; i++)
	{
		arrayOfInts[randomNumberGeneratorAssoc.rnd()]++;
	}
}

int loopSearch::findMax ()
{
	int max = -1;
	for (int i = 0; i < M; i++)
	{
		if (max < arrayOfInts[i])
		{
			max = arrayOfInts[i];
		}
	}
	return max;
}
