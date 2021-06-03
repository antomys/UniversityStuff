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

int main() {
	unsigned long long int a = 0;
	unsigned long long int b = 1;
	unsigned long long int c = 1;
	unsigned long long moduleNumber = pow (2,10);
	unsigned long long int counter = 1;

	bool f = false;
	while (counter < 100)
	{
		a = b;
		b = c;
		c = (a + b);
		counter++;
		cout << c << ' ';
	}
	return 0;
}
