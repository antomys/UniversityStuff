//============================================================================
// Name        : RandomFib.cpp
// Author      : Vlad
// Version     :
// Copyright   : Your copyright notice
// Description : Hello World in C++, Ansi-style
//============================================================================
#line 9 "iostream.hpp"
#include <iostream>
#line 11 "rnd.hpp"
#include "rnd.hpp"
#line 13 "fib.hpp"
#include  "fib.hpp"
#line 15 "loopSearch.hpp"
#include "loopSearch.hpp"
#line 17 "fstream.hpp"
#include <fstream>


using namespace std;

int a;

int main() {
	Rnd randomNumberGenerator;
	randomNumberGenerator.setMRnd (64);
	for (int i = 1; i < 100; i++)
	{
		a = randomNumberGenerator.rnd ();
		cout << "Random value from 0 to " << randomNumberGenerator.M << " : " << a << endl;
	}


	ofstream fout("/home/vlad/output.html");
	fout << "<table>";
	fout << "<tr>";
	fout << "<td>  M  </td>";
	fout << "<td>  C  </td>";
	fout << "</tr>";

	for (int i = 2; i <= 1024; i++)
	{
		fout << "<tr>";
		fout << "<td>" << i << "</td>";
		fout << "<td>" <<  findLoop(i) << "</td>";
		fout << "</tr>";

	}

	fout << "</table>";
	fout.close();
	return 0;
}
