//============================================================================
// Name        : FibonacciSequence.cpp
// Author      : Vlad
// Version     :
// Copyright   : Your copyright notice
// Description : Hello World in C++, Ansi-style
//============================================================================

#include <iostream>
#include <cmath>
using namespace std;

unsigned long long int a = 1;
unsigned long long int b = 2;
unsigned long long int c = 3;
unsigned long long moduleNumber = pow (2,10);

int fib ()
{
	if (a != 1 || b != 1 || c != 2)
	{
		a = b % moduleNumber;
		b = c % moduleNumber;
		c = (a + b) % moduleNumber;
	}
	return a;
}

int main() {
	for (int i = 0; i < 100; i++)
		cout << fib() << endl;
	return 0;
}
