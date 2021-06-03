/*
 * rnd.hpp
 *
 *  Created on: Oct 11, 2019
 *      Author: vlad
 */

#ifndef RND_HPP_
#define RND_HPP_
#include "fib.hpp"
class Rnd
{
public:
	int M;
	Fibonacci fibo;
	int rnd ();
	void setMRnd (int m);
	Rnd ();
};
#endif /* RND_HPP_ */
