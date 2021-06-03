/*
 * TableCreator.cpp
 *
 *  Created on: Nov 24, 2019
 *      Author: vlad
 */
#include "TableCreator.hpp"
#include "loopSearch.hpp"
#include "rnd.hpp"
#include <iostream>
using namespace std;

TableCreator::TableCreator()
{

}

void TableCreator::create()
{
	//cout << "Please, enter the the table size: \n";
	cin >> modulo;
	array2D.resize (modulo);
	for (int i = 0; i < modulo; i++)
	{
		array2D[i].resize(modulo);
	}

	for (int i = 0; i < modulo; i++)
	{
		for (int j = 0; j < modulo; j++)
		{
			array2D[i][j] = 0;
		}
	}

	Rnd rndGenerator;
	rndGenerator.setMRnd(modulo);
	loopSearch loopFinder(modulo);

	int lengthOfLoop = loopFinder.findLoop();
	if (lengthOfLoop % 2 == 0)
	{
		for (int i = 0; i < lengthOfLoop/2; i++)
		{
			int x = rndGenerator.rnd();
			int y = rndGenerator.rnd();
			array2D[x][y] = 1;
		//	cout << x << ' ' << y << ' ' << array2D[x][y] << endl;
		//	counter++;
		}
	}
	else
		{
		for (int i = 0; i < lengthOfLoop; i++)
				{
					int x = rndGenerator.rnd();
					int y = rndGenerator.rnd();
					array2D[x][y] = 1;
				//	cout << x << ' ' << y << ' ' << array2D[x][y] << endl;
				//	counter++;
				}
		}

	cout << "<table align = center> \n";
	cout << "<tr> \n";
	cout << "<td width = \"50\" height = \"50\"> </td>";
	for  (int i = 0; i < modulo; i++)
	{
		printf ("<td width = \"50\" height = \"50\"> %d </td>", i);
	}
	cout << "</tr> \n";
	for (int i = 0; i < modulo; i++)
	{
		cout << "<tr> \n";
		printf ("<td width = \"50\" height = \"50\"> %d </td>", i);
		for (int j = 0; j < modulo; j++)
		{
			if (array2D[i][j] == 1)
			{
				cout << "<td bgcolor = #000000 width = \"50\" height = \"50\">   </td> \n";
			}
			else cout << "<td bgcolor = #999999 width = \"50\" height = \"50\">  </td> \n";
		//	cout << array2D[i][j] << ' ';
		}
		cout << "</tr> \n";
	}
	cout << "</table>";
//	cout << counter;

}
