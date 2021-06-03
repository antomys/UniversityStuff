/*
 * GoldenBlock.hpp
 *
 *  Created on: Nov 6, 2019
 *      Author: vlad
 */

#ifndef GOLDENBLOCK_HPP_
#define GOLDENBLOCK_HPP_
#line 11 "rnd.hpp"
#include "rnd.hpp"

class GoldenBlock
{
private:
	double length_CM;
	double width_CM;
	double height_CM;
	double density_GperCM3;
	double volume_CM3;
	double mass_G;
	Rnd rndGenerator;
public:
	GoldenBlock ();
	GoldenBlock (GoldenBlock &block);
	void setParameters (double length, double width, double height, double density);
	void createBlock(int bound);
	void turn ();
	void rotate();
	double getLength_CM();
	double getWidth_CM();
	double getHeight_CM();
	double getDensity_GperCM3();
	double getMass_G();
	double getVolume_CM3();
};

#endif /* GOLDENBLOCK_HPP_ */
