/*
 * Boolba.cpp
 *
 *  Created on: Nov 6, 2019
 *      Author: vlad
 */
#include <iostream>
#line 9 "Boolba.hpp"
#include "Boolba.hpp"
#line 11 "Boolba.cpp"

using namespace std;

bool Boolba::isFull() {return   ((currentMass_G == maximalMass_G || currentMass_G >= maximalMass_G-eps)
								||
								(currentVolume_CM3 == maximalVolume_CM3) || (currentVolume_CM3 >= maximalVolume_CM3-eps));}

Boolba::Boolba ()
{
	currentMass_G = 0.0;
	currentVolume_CM3 = 0.0;
}

bool Boolba::checkMassAndVolume(double blockMass_G, double blockVolume_CM3)
{
	if ((currentMass_G + blockMass_G <= maximalMass_G) && (currentVolume_CM3 + blockVolume_CM3 <= maximalVolume_CM3))
	{
		return true;
	}
	else return false;
}

bool Boolba::checkMass(double blockMass_G)
{
	if (currentMass_G + blockMass_G <= maximalMass_G)
	{
		return true;
	}
	else return false;
}

bool Boolba::checkVolume(double blockVolume_CM3)
{
	if (currentVolume_CM3 + blockVolume_CM3 <= maximalVolume_CM3)
	{
		return true;
	}
	else return false;
}

void Boolba::increaseMass_G(double mass_G)
{
	currentMass_G += mass_G;
}

void Boolba::increaseVolume_CM3(double volume_CM3)
{
	currentVolume_CM3 += volume_CM3;
}

double Boolba::getDiffInMass_G() { return maximalMass_G - currentMass_G;}

double Boolba::getDiffInVolume_CM3() {return maximalVolume_CM3 - currentVolume_CM3;}

double Boolba::getCurrentVolume_CM3() { return currentVolume_CM3; }

double Boolba::getCurrentMass_G() { return currentMass_G; }
