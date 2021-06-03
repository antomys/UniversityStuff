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
}

int loopSearch::findLoop (int M)
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
		counter++;
	} while (f0 != 1 || f1 != 1);
	return counter-3;
}

void loopSearch::assoc(int length, int M)
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

int loopSearch::findMax (int M)
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
