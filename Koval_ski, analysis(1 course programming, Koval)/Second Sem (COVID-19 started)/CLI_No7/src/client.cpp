//============================================================================
// Name        : client.cpp
// Author      : Vlad
// Version     :
// Copyright   : A project that is too secret to show to anybody
// Description : Hello World in C++, Ansi-style
//============================================================================

#include <iostream>
#include "CLI.hpp"
#include "logEntry.hpp"
using namespace std;

int main(int argc, char** argv) {
	CLI cli;
	cli.run();
	return 0;
}
