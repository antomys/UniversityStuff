/*
 * Boolba.hpp
 *
 *  Created on: Nov 6, 2019
 *      Author: vlad
 */

#ifndef BOOLBA_HPP_
#define BOOLBA_HPP_

#ifndef maximalMass_G
#define maximalMass_G 100000
#endif

#ifndef maximalVolume_CM3
#define maximalVolume_CM3 5000
#endif

#ifndef eps
#define eps 0.1
#endif

class Boolba
{
private:
	double currentVolume_CM3;
	double currentMass_G;
public:
	Boolba();
	bool isFull();
	bool checkMassAndVolume(double mass_G, double volume_CM3);
	bool checkMass(double mass_G);
	bool checkVolume(double volume_CM3);
	void increaseMass_G(double mass_G);
	void increaseVolume_CM3(double volume_CM3);
	double getCurrentMass_G();
	double getCurrentVolume_CM3();
	double getDiffInMass_G();
	double getDiffInVolume_CM3();
};
#endif /* BOOLBA_HPP_ */
