/*
 * GoldenBlock.cpp
 *
 *  Created on: Nov 6, 2019
 *      Author: vlad
 */

#include <iostream>
#line 10 "GoldenBlock.hpp"
#include "GoldenBlock.hpp"
#line 12 "fib.hpp"
#include "fib.hpp"
#line 14 "loopSearch.hpp"
#include "loopSearch.hpp"
#line 16 "GoldenBlock.cpp"
using namespace std;

GoldenBlock::GoldenBlock()
{
}

GoldenBlock::GoldenBlock (GoldenBlock &block)
{
	length_CM = block.length_CM;
	width_CM = block.width_CM;
	height_CM = block.height_CM;
	density_GperCM3 = block.density_GperCM3;
}



void GoldenBlock::createBlock(int bound)
{
	rndGenerator.setMRnd(bound);
	// go through the loop in order to actually randomize the sequence
	length_CM = rndGenerator.rnd();
	width_CM = rndGenerator.rnd();
	height_CM = rndGenerator.rnd();
	density_GperCM3 = rndGenerator.rnd();
	if (length_CM == 0) length_CM++;
	if (width_CM == 0) width_CM++;
	if (height_CM == 0) height_CM++;
	if (density_GperCM3 == 0) density_GperCM3++;
	volume_CM3 = length_CM*width_CM*height_CM;
	mass_G = volume_CM3*density_GperCM3;
}

void GoldenBlock::setParameters(double length, double width, double height, double density)
{
	length_CM = length;
	width_CM = width;
	height_CM = height;
	density_GperCM3 = density;
	volume_CM3 = length_CM*width_CM*height_CM;
	mass_G = volume_CM3*density_GperCM3;
}

double GoldenBlock::getLength_CM() {return length_CM;}
double GoldenBlock::getWidth_CM() {return width_CM;}
double GoldenBlock::getHeight_CM() {return height_CM;}
double GoldenBlock::getDensity_GperCM3() {return density_GperCM3;}
double GoldenBlock::getVolume_CM3() {return volume_CM3;}
double GoldenBlock::getMass_G() {return mass_G;}

void GoldenBlock::turn()
{
	double tmp = height_CM;
	height_CM = width_CM;
	width_CM = tmp;
}

void GoldenBlock::rotate()
{
	double tmp = height_CM;
	height_CM = length_CM;
	length_CM = tmp;
}
