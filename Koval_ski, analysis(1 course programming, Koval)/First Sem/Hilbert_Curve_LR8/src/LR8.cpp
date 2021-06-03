//============================================================================
// Name        : LR8.cpp
// Author      : Vlad
// Version     :
// Copyright   : A project that is too secret to show to anybody
// Description : Hello World in C++, Ansi-style
//============================================================================

#include <iostream>
#include "CurveBuilder.hpp"
using namespace std;

#ifndef lll
#define lll 5
#endif

int main()
{
	int level = lll;
	CurveBuilder curve;
	curve.build(level);
	return 0;
}
