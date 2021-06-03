/*
 * PlotBuilder.cpp
 *
 *  Created on: Nov 28, 2019
 *      Author: vlad
 */

#include <iostream>
#include <fstream>
#include <sstream>
#include "PlotBuilder.hpp"
using namespace std;

void PlotBuilder::parseFile(string path)
{
	ifstream fin (path);
	string s;
	if (!fin) {
	    cerr << "Unable to open file log.txt";
	    exit(1);   // call system to stop
	}
	while (std::getline (fin, s))
	{
		stringstream ss(s);
		string item;
		string e0 = "E0`";
		char delimiter = ' ';
		bool f = false;
		while (std::getline (ss, item, delimiter))
		{
			if (f)
			{
				cout << item << endl;
				f = false;
			}
			if (item.compare(e0) == 0)  f = true;
		}
	}
}
