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
	int f0_2, f1_2, f2_2;
	long long f0_3, f1_3, f2_3, f3_3;
	int f0_3_mod, f1_3_mod, f2_3_mod, f3_3_mod;
	int fib2 ();
	void resetFib2 (int f00, int f11, int f22);
	void setFib2 ();
	long long fib3();
	void moveBackFib3();
	void setFib3();
	void setM (int M);
};
#endif /* _FIB_HPP_ */
