//============================================================================
// Name        : Ambiguous.cpp
// Author      : Vlad
// Version     :
// Copyright   : A project that is too secret to show to anybody
// Description : Hello World in C++, Ansi-style
//============================================================================
#define a (c--)
#include <iostream>
using namespace std;

int main() {
	int c = 0;
	int  b = a-a;
	cout << b;
	cout << ' ' << c;
	return 0;
}
