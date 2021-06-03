/*
 * fib.hpp
 *
 *  Created on: Oct 11, 2019
 *      Author: vlad
 */

#ifndef _FIB_HPP_
#define _FIB_HPP_

class Fibonacci {
public:
	int M;
	int f0, f1, f2;
	int fib ();
	void resetFib (int f00, int f11, int f22);
	bool setFib ();
	void setM (int M);
};
#endif /* _FIB_HPP_ */
