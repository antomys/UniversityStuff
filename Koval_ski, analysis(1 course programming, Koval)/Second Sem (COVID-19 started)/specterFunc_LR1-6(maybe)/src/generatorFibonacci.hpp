/*
 * fib.hpp
 *
 *  Created on: Oct 11, 2019
 *      Author: vlad
 */

#ifndef _FIB_HPP_
#define _FIB_HPP_

class generatorFibonacci
{
private:
	int fibLoopLimit;
	int fib[3];
public:
	generatorFibonacci();
	generatorFibonacci(int f0, int f1);
	generatorFibonacci (int f0, int f1, int f2);
	void GCD();
	long long next();
	void reset();
};
#endif /* _FIB_HPP_ */
