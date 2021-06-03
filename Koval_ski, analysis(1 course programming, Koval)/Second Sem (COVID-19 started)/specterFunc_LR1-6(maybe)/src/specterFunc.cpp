//============================================================================
// Name        : specterFunc.cpp
// Author      : Vlad
// Version     :
// Copyright   : A project that is too secret to show to anybody
// Description : Hello World in C++, Ansi-style
//============================================================================

#include <iostream>
#include <fstream>
#include <cstring>
#include "specter2HTML.hpp"
#include "linearIndex.hpp"
#include "specterBuilder.hpp"
#include "generatorFibonacci.hpp"

using namespace std;

/* f return numbers from 0 to 32767.
 * It should have crashed the program, but it doesn't. (maxF = 30).
 */
int f1()
{
	return rand();
}


// function that always returns 1
int f2()
{
	return 1;
}

int f3()
{
	return -1;
}

// SpecterBuilder test.
void test1 ()
{
	cout << "specterBuilder test\n";
	int array[30][30][30] = {};
	Specter specter (20, 30, 3, **array);
	// function that returns a value that is out of range of (0, maxF)
	int (*f1p) (void) = f1;
	Function function1 (f1);
	int result = specter.buildSpecter(function1);
	cout << result << endl;


	// function that returns a constant (1) -> 1 non-null element
	for (int i = 0; i < 30*30*30; i++)
	{
		*(**array+i) = 0;
	}
	int (*f2p) (void) = f2;
	Function function2 (f2);
	result = specter.buildSpecter(function2);
	cout << result << endl;

	// crashes the program as incorrect parameters were passed as arguments (exit code: -1)
	for (int i = 0; i < 30*30*30; i++)
	{
		*(**array+i) = i;
	}
	/*
	// can use f3p as first parameter and work with memory outside of an array. (bad, don't do that)
	int (*f3p) (void) = f3;
	result = specterBuilder.buildSpecter(f2p, 20, 30, 5, **array); // axes<=3 (!).
	cout << result << endl;
	*/
}


// Specter2HTML test
void test2 ()
{
	cout << "specter2HTML test\n";
	// create a test array
	int testArray[3][3] = {};
	// create a Specter object for this array
	Specter specter(39, 3, 2, *testArray);
	int (*f1p) (void) = f1;
	Function function1 (f1);
	// redirecting the stream to the HTML file
	ofstream fout ("table.html");
	// fill it with random numbers
	specter.buildSpecter(function1);
	// write the string (a really long one) to the HTML file
	fout << specter.specter2String();
	// write the HTML to the console
	// testObject.specter2cout();
	// write the HTML directly to the file. Print out the amount of elements that were processed.
	cout << specter.specter2File ("table.html");
	fout.close();
}


// Linearizer class test.
void test3 ()
{
	cout << "Linearizer test\n";
	// Create the object.
	Linearizer linearizer;
	// Given a 3-dimensional array, find the coordinates of the same element in flattened array.
	// For example, let's take (1,1,1).
	int coordinates[3] = {1,1,1};
	// As there are 27 elements in 3*3*3 array, (1, 1, 1) will have the coordinate 13 (given that we start counting from 0).
	cout << linearizer.lIndex(3, 3, coordinates);

}


// generatorFibonacci test.
void test4()
{
	cout << "generatorFibonacci test\n";
	/*
	try{
	generatorFibonacci gf (2, -1);
	} catch(const char *e)
	{
		cout << "Constructor exception: " << e << "\n";
	}
	*/
	generatorFibonacci gf (2, 4);
	for (int i = 0; i < 20; i++)
	{
		cout << gf.next() << endl;
	}
	cout << "----\n";
	cout << "Dividing by GCD (if called with proper arguments)... \n";
	cout << "----\n";
	gf.GCD();
	for (int i = 0; i < 20; i++)
	{
			cout << gf.next() << endl;
	}
}

void test5()
{
	cout << "Operator overloading test \n";
	int array1 [9] = {2, 4, 8};
	int array2 [3][3] = {{4, 8, 16},{},{}};
	Specter specter1 (9, 9, 1, array1);
	Specter specter2 (9, 3, 2, *array2);
	cout << string(specter1) << endl;
	cout << string(specter2) << endl;
	cout << (specter1 == specter2) << endl;
	cout << string(specter1) << endl;
	cout << string(specter2) << endl;
	// breaks when we are passing not a Specter&
	 // cout << (specter1 == 123) << endl;
}

void help()
{
	cout << "Pass an argument 'help', 'test1', 'test2', 'test3', 'test4', 'test5' or 'all' to the program to see how it is working." << endl;
}


int main(int argc, char **argv)
{

	if (argc == 1)
	{
		cout << "No arguments provided. Here is a tutorial to get things working:" << endl;
		help();
	}
	else if (!strcmp ("help", argv[1]))
	{
		cout << "Here is help:" << endl;
		help();
	}
	else if (!strcmp ("test1" , argv[1]))
	{
		cout << "Running test 1..." << endl;
		test1();
	}
	else if (!strcmp ("test2", argv[1]))
	{
		cout << "Running test 2..." << endl;
		test2();
	}
	else if (!strcmp ("test3", argv[1]))
	{
		cout << "Running test 3..." << endl;
		test3 ();
	}
	else if (!strcmp ("test4", argv[1]))
	{
		cout << "Running test 4..." << endl;
		test4();
	}
	else if (!strcmp ("test5", argv[1]))
	{
		cout << "Running test 5..." << endl;
		test5();
	}
	else if (!strcmp ("all", argv[1]))
	{
		cout << "Running all tests..." << endl;
		test1();
		test2();
		test3();
		test4();
		test5();
	}
	else
	{
		cout << "Wrong parameter! Here is help:"<< endl;
		help();
	}
	return 0;
}
