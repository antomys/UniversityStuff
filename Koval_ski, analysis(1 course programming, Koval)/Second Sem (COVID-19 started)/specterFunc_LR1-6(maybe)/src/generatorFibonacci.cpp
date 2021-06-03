/*
 * fib.cpp
 *
 *  Created on: Oct 11, 2019
 *      Author: vlad
 */

#include "generatorFibonacci.hpp"
#include <iostream>

using namespace std;

// reset to default values
generatorFibonacci::generatorFibonacci(): fibLoopLimit(3)
{
	(*this).fib[0] = 0;
	(*this).fib[1] = 1;
	(*this).fib[2] = 1;
}

generatorFibonacci::generatorFibonacci(int f0, int f1): fibLoopLimit(2)
{
	if (fib[0] >= 0)	(*this).fib[0] = f0;
	else throw "Illegal argument!";
	if (fib[1] >= 0) 	(*this).fib[1] = f1;
	else throw "Illegal argument!";
}

generatorFibonacci::generatorFibonacci(int f0, int f1, int f2):  fibLoopLimit(3)
{
	if (fib[0] >= 0)	(*this).fib[0] = f0;
	else throw "Illegal argument!";
	if (fib[1] >= 0) 	(*this).fib[1] = f1;
	else throw "Illegal argument!";
	if (fib[2] >= 0)	(*this).fib[2] = f2;
	else throw "Illegal argument!";
}

long long generatorFibonacci::next()
{
	int tmp = 0;
	for (int i = 0; i < fibLoopLimit; i++)
	{
		tmp += fib[i];
		if (i < fibLoopLimit-1)	fib[i] = fib[i+1];
	}
	fib[fibLoopLimit-1] = tmp;
	return fib[fibLoopLimit-1];
}



void generatorFibonacci::reset()
{
	(*this).fib[0] = 0;
	(*this).fib[1] = 1;
	(*this).fib[2] = 1;
}

void generatorFibonacci::GCD()
{
	if (fibLoopLimit == 2)
	{
			int max = (fib[0] >= fib[1]) ? fib[0] : fib[1];
			int min = (fib[0] >= fib[1]) ? fib[1] : fib[0];
			while (min > 0)
			{
				max = max % min;
				int tmp = max;
				max = min;
				min = tmp;
			}
			fib[0] /= max;
			fib[1] /= max;
			fib[2] /= max;
	}
	else
	{
		cout << "Your object should be created with 2 variables in constructor in order to run this function!" << endl;
	}
}
