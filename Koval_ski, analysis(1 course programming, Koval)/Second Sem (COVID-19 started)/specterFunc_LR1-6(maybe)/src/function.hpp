/*
 * function.hpp
 *
 *  Created on: Feb 17, 2020
 *      Author: vlad
 */

#ifndef FUNCTION_HPP_
#define FUNCTION_HPP_

class Function
{
public:
	int next();
	void reset();
	Function (int (*fp)(void));
private:
	int (*fp) (void);
};



#endif /* FUNCTION_HPP_ */
