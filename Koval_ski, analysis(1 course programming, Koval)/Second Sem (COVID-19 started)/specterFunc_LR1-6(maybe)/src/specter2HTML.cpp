/*
 * specter2HTML.cpp
 *
 *  Created on: Feb 7, 2020
 *      Author: vlad
 */

#include <iostream>
#include <string>
#include <cmath>
#include <fstream>
#include <sstream>
#include "specter2HTML.hpp"


using namespace std;

// Converts specter to string.
string Specter2HTML::specter2String (int* specter // pointer to the first element in the array
									, int axes 	// amount of dimensions in the array
									, int maxF)	// amount of elements in one dimension
{
	counter = 0;
	string html = "";
	html += "<style>"
      "table{"
      "padding: 10px;"
      "border: 1px solid black;"
      "border-collapse: collapse;"
	  "background-color:#c75e5e;"
      "}"
	 "table td {border:solid 5px red;"
            "border-top-color:#FEB9B9;"
            "border-right-color:#B22222;"
            "border-bottom-color:#B22222;"
            "border-left-color:#FEB9B9;} </style>";

	html += "<html> \n";
	html += "<body> \n";

	html += buildTable(specter, maxF, axes, 0);

	html += "</body> \n";
	html += "</html> \n";
	return html;
}

// Writes out HTML to cout.
int Specter2HTML::specter2cout (int* specter // pointer to the first element in the array
								, int axes  // amount of dimensions in our array
								, int maxF) // maximal amount of elements in one dimension
{
	cout << specter2String (specter, axes, maxF);
	return counter;
}


// Writes out HTML to file (given the filename)
int Specter2HTML::specter2File (int* specter // pointer to the first element in the array
								, int axes // amount of dimensions in our array
								, int maxF // maximal amount of elements in one dimension
								,string fileName) // the name of the file where the output is written
{
	ofstream fout (fileName);
	fout << specter2String (specter, axes, maxF);
	fout.close();
	return counter;
}

/* Helper recursive function to build the structure of HTML file.
 * Depth of recursion = amount of axes.
 * (each new axis = new nested table)
 */

string Specter2HTML::buildTable(int* array // pointer to the first element in the array
								, int maxF // maximal amount of elements in one dimension
								, int depth // depth of recursion
								, int index) /* incomplete index from the previous recursion call
											  *	(see if-condition to understand how it is used)
											  */
{
	if (depth == 0)
	{

	}
	else
	{
		string tableHTML = "";
		tableHTML +="<table> \n";
		for (int i = 0; i < maxF; i++)
		{
		tableHTML +="<tr> \n";
		tableHTML +="<td> \n";

		if (depth > 1)
		{
			tableHTML += buildTable (array, maxF, depth-1, (index*maxF)+i);
		}
		else
		{

			int tmp = *(array+i);
			counter++;
			stringstream ss;
			ss << tmp;
			string str = ss.str();
			tableHTML += str;

		}

		tableHTML += "</td> \n";
		tableHTML +="</tr> \n";
		}
		tableHTML +="</table> \n";
		return tableHTML;
	}
}
