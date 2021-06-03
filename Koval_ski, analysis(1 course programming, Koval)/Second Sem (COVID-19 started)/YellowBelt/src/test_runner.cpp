/*
 * test_runner.cpp
 *
 *  Created on: Apr 26, 2020
 *      Author: vlad
 */

#include "test_runner.h"

void Assert(bool b, const string& hint) {
	AssertEqual(b, true, hint);
}


TestRunner::~TestRunner() {
		if (fail_count > 0) {
			cerr << fail_count << " unit tests failed. Terminate" << endl;
			exit(1);
		}
	}
