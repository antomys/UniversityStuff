#include <iostream>
#include <map>
#include <set>
#include <sstream>
#include <string>
#include <vector>
#include <algorithm>
#include "test_runner.h"
using namespace std;


template <typename T>
vector<T> FindGreaterElements(const set<T>& elements, const T& border);

template <typename T>
vector<T> FindGreaterElements(const set<T>& elements, const T& border)
{
	auto it = find_if (begin(elements), end(elements), [border](const T element)
														{
															return element > border;
														});
	vector<T> result;
	for (auto tmp_it = it; tmp_it != end(elements); tmp_it++)
	{
		result.push_back((*tmp_it));
	}
	return result;
}


void TestInt()
{
	set<int> vi = {1, 4, 10, -9};
	vector<int> res = {};
	AssertEqual(FindGreaterElements(vi, 11), res, "test1");
}


void TestAll()
{
	TestRunner tr;
	tr.RunTest(TestInt, "test1xd");
}

int main() {
	TestAll();
	return 0;
}
