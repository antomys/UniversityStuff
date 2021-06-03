/*
 * Zolotosyorb.hpp
 *
 *  Created on: Nov 6, 2019
 *      Author: vlad
 */

#ifndef ZOLOTOSYORB_HPP_
#define ZOLOTOSYORB_HPP_

#ifndef windowHeight
#define windowHeight 30
#endif

#ifndef windowWidth
#define windowWidth 30
#endif

#ifndef currentEnergy_J
#define currentEnergy_J 200
#endif

#line 24 "rnd.hpp"
#include "rnd.hpp"
#line 26 "Boolba.hpp"
#include "Boolba.hpp"
#line 28 "GoldenBlock.hpp"
#include "GoldenBlock.hpp"
#line 30 "Zolotosyorb.hpp"
#include <utility>
#include <iostream>

class Zolotosyorb
{
private:
	const double energyTurn_J = 1;
	const double energyPut_J = 1;
	const double energyRotate_J = 1;
	const double energyCut_J = 1;
	const double energyAccept_J = 1;
	const double energyDrop_J = 1;
	const double energyGoodDrop_J = 1;
	const double energyFly_J = 100;
	double energyRecuperator_J = 100;
	double energyCurrent_J = currentEnergy_J;
	const double delta_GperCM3 = 1.5;
	const double pGold_GperCM3 = 19.3;
	const double energyHeat_JperKelvinKG = 129;
	const double energyMelting_JperG = 64;
	const double initialTemperature_Celcium = 9;
	const double meltingTemperature_Celcium = 1064;
	const double boolbaTemperature_Celcium = -269.15;
	Boolba boolba;
	GoldenBlock currentBlock;

	void putBlock();
	void turnBlock();
	void rotateBlock();
	void cutBlock(double newLength_CM);
	void dropBadBlock();
	void dropGoodBlock();
	void acceptBlock(GoldenBlock &block);
	void flyAway();

	std::pair <bool, std::pair <std::string, double>> blockFits();
	bool isBoolbaFull();
	double calculateQForBlock (GoldenBlock &block);
	double howMuchCanRecuperatorMelt_G();
	double calculateVolumeOfBlockThatFits_CM3(double mass_G);
	double calculateMassOfBlockThatFits_G (double volume_CM3);
	double calculateLengthOfFirstBlock_CM (double volumeOfBlockOne_CM3);

public:
	Zolotosyorb();
	void startCollectingGold();
};
#endif /* ZOLOTOSYORB_HPP_ */
