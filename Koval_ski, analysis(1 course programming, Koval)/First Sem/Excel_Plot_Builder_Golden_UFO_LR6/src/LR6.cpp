//============================================================================
// Name        : LR6.cpp
// Author      : Vlad
// Version     :
// Copyright   : A project that is too secret to show to anybody
// Description : Hello World in C++, Ansi-style
//============================================================================

#include <iostream>
#include "PlotBuilder.hpp"
using namespace std;

int main()
{
	PlotBuilder plot;
	plot.parseFile ("/home/vlad/eclipse-workspace/LR6/src/log.txt");
}
