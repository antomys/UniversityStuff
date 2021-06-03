/*
 * Zolotosyorb.cpp
 *
 *  Created on: Nov 6, 2019
 *      Author: vlad
 */


#line 10 "Zolotosyorb.hpp"
#include "Zolotosyorb.hpp"
#line 12 "loopSearch.hpp"
#include "loopSearch.hpp"
#line 14 "GoldenBlock.hpp"
#include "GoldenBlock.hpp"
#line 16 "Zolotosyorb.cpp"
#include <iostream>
#include <cstdio>
#include <utility>

#ifndef magicNumber
#define magicNumber 42
#endif

using namespace std;

Zolotosyorb::Zolotosyorb ()
{
}

void Zolotosyorb::startCollectingGold()
{
	/*
		 * while (block can be put && boolba isn't full)
		 * {
		 *  put block
		 *  check for drops
		 *  calculate whether it is needed to be rotated
		 *  cut (if impediment)
		 *
		 *  accept
		 *  change recuperator energy
		 *  change boolba's mass and volume
		 * }
		 * */
		while ((energyCurrent_J > energyPut_J) && (!boolba.isFull()) && (energyCurrent_J > energyFly_J))
		{
			// put block
			putBlock();
			// calculate the bound for density of the gold block for badDrop();
			double lowerBoundOfInterval = pGold_GperCM3 - delta_GperCM3;
			double upperBoundOfInterval = pGold_GperCM3 + delta_GperCM3;
			pair <bool, pair<string, double>> blockTestResult = blockFits();
			if (currentBlock.getDensity_GperCM3() < lowerBoundOfInterval || currentBlock.getDensity_GperCM3() > upperBoundOfInterval)
				dropBadBlock();
				else if (!blockTestResult.first)
						{
							dropGoodBlock();
						}
						else
							{
								string operations = blockTestResult.second.first; // get the string with operations performed on the block
								for (int i = 0; i < operations.length(); i++)
								{
									if (operations[i] == 't')
									{
										turnBlock();
									}
									else
										{
											rotateBlock();
										}
								}
								double blockMass_G = currentBlock.getMass_G();
								double maxMassForRecuperator_G = howMuchCanRecuperatorMelt_G();
								if (blockMass_G <= maxMassForRecuperator_G &&
									boolba.checkMassAndVolume(currentBlock.getMass_G(), currentBlock.getVolume_CM3()))
								{
									acceptBlock(currentBlock);
								}
								else
									{
										double prevEnergyValue_J = energyCurrent_J;
										do
										{
											prevEnergyValue_J = energyCurrent_J;
											double volumeOfBlockOne_CM3 = calculateVolumeOfBlockThatFits_CM3(maxMassForRecuperator_G);
											double newLength_CM = calculateLengthOfFirstBlock_CM (volumeOfBlockOne_CM3);
											if (newLength_CM < currentBlock.getLength_CM())
											{
												cutBlock (newLength_CM); // cut and accept (if possible) the half that can be melted
											}
											blockMass_G = currentBlock.getMass_G();
											maxMassForRecuperator_G = howMuchCanRecuperatorMelt_G();
										} while (blockMass_G > maxMassForRecuperator_G &&
												energyCurrent_J != prevEnergyValue_J);
										if (boolba.checkMassAndVolume(currentBlock.getMass_G(),currentBlock.getVolume_CM3()))
										{
											acceptBlock(currentBlock);
										}
										else
										{
											if (!boolba.checkMass(currentBlock.getMass_G()))
											{
												double diff_G = boolba.getDiffInMass_G();
												double volumeOfBlockOne_CM3 = calculateVolumeOfBlockThatFits_CM3(diff_G);
												double newLength_CM = calculateLengthOfFirstBlock_CM (volumeOfBlockOne_CM3);
												cutBlock(newLength_CM);
											}
											else if (!boolba.checkVolume(currentBlock.getVolume_CM3()))
													{
														double diff_CM3 = boolba.getDiffInVolume_CM3();
														double newLength_CM = calculateLengthOfFirstBlock_CM (diff_CM3);
														cutBlock(newLength_CM);
													}
										}
									}
							}
		}
		flyAway();
}

void Zolotosyorb::putBlock()
{
	// create a block which parameters are constrained by the argument
	currentBlock.createBlock(magicNumber);
	cout << "P" << endl;
	double width = currentBlock.getWidth_CM();
	double height = currentBlock.getHeight_CM();
	double length = currentBlock.getLength_CM();
	double mass = currentBlock.getMass_G();
	double density = currentBlock.getDensity_GperCM3();
	printf ("L %.1f (CM)\n"
			"W %.1f (CM)\n"
			"H %.1f (CM)\n"
			"M %.1f (G)\n"
			"P %.1f (G/CM3)\n",
			length, width, height, mass, density);
	printf ("E0 %.1f (J)\n"
			"D %.1f (J)\n"
			"E0` %.1f (J)\n",
			energyCurrent_J, energyPut_J, energyCurrent_J - energyPut_J);
	energyCurrent_J -= energyPut_J;
}

void Zolotosyorb::dropBadBlock ()
{
	if (energyCurrent_J - energyDrop_J >= energyFly_J)
	{
		cout << "DB" << endl;
		printf ("E0 %.1f (J)\n"
				"D %.1f (J)\n"
				"E0` %.1f (J)\n"
				"P %.1f (G/CM3)\n",
				energyCurrent_J, energyDrop_J, energyCurrent_J - energyDrop_J, currentBlock.getDensity_GperCM3());
		energyCurrent_J -= energyDrop_J;
	}
}

// test whether it is useful to rotate or turn the block
pair<bool, pair <string, double>> Zolotosyorb::blockFits()
{
	double energySpent_J = 0;
	GoldenBlock testBlock (currentBlock); // copy constructor
	string operations; // string with the sequence of turn / rotate operations encoded
	for (int i = 0; i < 6; i++)
	{
		if (i % 2 == 0)
		{
			energySpent_J += energyTurn_J;
			testBlock.turn();
			operations += 't';
		}
		else
			{
				energySpent_J += energyRotate_J;
				testBlock.rotate();
				operations += 'r';
			}
		if (windowWidth >=  testBlock.getWidth_CM() &&
			windowHeight >= testBlock.getHeight_CM() &&
			energyCurrent_J - energySpent_J >= energyFly_J)
		{
			return make_pair(true, make_pair (operations, energySpent_J));
		}
	}
	return make_pair(false, make_pair(operations, 0));
}

void Zolotosyorb::dropGoodBlock()
{
	// check whether we have the energy to drop the block
	if (energyCurrent_J - energyDrop_J >= energyFly_J)
	{
		cout << "DG" << endl;
			printf ("E0 %.1f (J)\n"
					"D %.1f (J)\n"
					"E0` %.1f (J)\n"
					"L %.1f (CM)\n"
					"W %.1f (CM)\n"
					"H %.1f (CM)\n",
						energyCurrent_J, energyDrop_J, energyCurrent_J - energyDrop_J,
						currentBlock.getLength_CM(), currentBlock.getWidth_CM(), currentBlock.getHeight_CM());
			energyCurrent_J -= energyDrop_J;
	}
}

void Zolotosyorb::turnBlock ()
{
	// assuming there is enough energy as blockFits() method returned true
	cout << 'T' << endl;
	currentBlock.turn();
	printf ("E0 %.1f (J)\n"
			"D %.1f (J)\n"
			"E0` %.1f (J)\n"
			"L %.1f (CM)\n"
			"W %.1f (CM)\n"
			"H %.1f (CM)\n",
					energyCurrent_J, energyTurn_J, energyCurrent_J - energyTurn_J,
					currentBlock.getLength_CM(), currentBlock.getWidth_CM(), currentBlock.getHeight_CM());
	energyCurrent_J -= energyTurn_J;
}

void Zolotosyorb::rotateBlock()
{
	// assuming there is enough energy as blockFits() method returned true
	cout << 'R' << endl;
	currentBlock.rotate();
	printf ("E0 %.1f (J)\n"
			"D %.1f (J)\n"
			"E0` %.1f (J)\n"
			"L %.1f (CM)\n"
			"W %.1f (CM)\n"
			"H %.1f (CM) \n",
				energyCurrent_J, energyRotate_J, energyCurrent_J - energyRotate_J,
				currentBlock.getLength_CM(), currentBlock.getWidth_CM(), currentBlock.getHeight_CM());
	energyCurrent_J -= energyRotate_J;
}



double Zolotosyorb::howMuchCanRecuperatorMelt_G()
{
	// m - in grams;
	// Q1 = dT * (m/1000) * energyHeat; - to heat (J = (K * KG * J) / (K*KG))
	// Q2 = m * energyMelting; - to melt (J = G * J / G)
	// Q1+Q2 = m (dT*energyHeat/1000 + energyMelting)
	// Q1+Q2 = m * ( dT * energyHeat+ 1000 * energyMelting)/1000; Q1+Q2 = recuperator energy
	// m = 1000*(Q1+Q2) / (dT * energyHeat+1000*energyMelting)
	double deltaT = meltingTemperature_Celcium - initialTemperature_Celcium;
	double maxMass_KG = (1000*(long long)energyRecuperator_J) / (double)(deltaT * energyHeat_JperKelvinKG + 1000*energyMelting_JperG);
	double maxMass_G = maxMass_KG*1000;
	return maxMass_G; // in G
}

double Zolotosyorb::calculateVolumeOfBlockThatFits_CM3(double mass_G)
{
	// m = V*p; V = m / p;
	return mass_G / currentBlock.getDensity_GperCM3();
}

double Zolotosyorb::calculateMassOfBlockThatFits_G (double volume_CM3)
{
	// m = V*p;
	return currentBlock.getVolume_CM3() * currentBlock.getDensity_GperCM3();
}

double Zolotosyorb::calculateLengthOfFirstBlock_CM (double volumeOfBlockOne_CM3)
{
	// V = a*b*c; a = V/b*c
	return volumeOfBlockOne_CM3 / (currentBlock.getWidth_CM() * currentBlock.getHeight_CM());
}


void Zolotosyorb::cutBlock (double newLength_CM)
{
	if (energyFly_J <= energyCurrent_J - energyCut_J)
	{
		double height_CM = currentBlock.getHeight_CM();
		double width_CM = currentBlock.getWidth_CM();
		double length_CM = currentBlock.getLength_CM();
		double density_GperCM3 = currentBlock.getDensity_GperCM3();
		// the cutting process itself
		cout << "C" << endl;
		printf ("C` %.1f (CM) \n", length_CM - newLength_CM);
		energyCurrent_J -= energyCut_J;
		// split one block in two; currentBlock = the one that goes afterwards, block1 = block that goes first
		GoldenBlock block1;
		block1.setParameters(newLength_CM, width_CM , height_CM, density_GperCM3);
		// set currentBlock's parameters as before but with reduced width
		currentBlock.setParameters(length_CM - newLength_CM, width_CM , height_CM, density_GperCM3);
		//TODO  block1 can be melted by the recuperator. the only thing left to check is whether we have enough
		// energy to accept it and check whether it fits in the boolba.
		double blockMass_G = block1.getMass_G();
		double blockVolume_CM3 = block1.getVolume_CM3();
		// if the boolba can take the block in, then acceptBlock() is performed:
		if (boolba.checkMassAndVolume(blockMass_G, blockVolume_CM3))
		{
			acceptBlock(block1);
		}
		else
		{
			if (!boolba.checkMass(block1.getMass_G()))
				{
					double diff_G = boolba.getDiffInMass_G();
					double volumeOfBlockOne_CM3 = calculateVolumeOfBlockThatFits_CM3(diff_G);
					double newLength_CM = calculateLengthOfFirstBlock_CM (volumeOfBlockOne_CM3);
					if (newLength_CM < block1.getLength_CM())
					{
						cutBlock (newLength_CM); // cut and accept (if possible) the half that can be melted
					}
				}
				else if (!boolba.checkVolume(block1.getVolume_CM3()))
						{
							double diff_CM3 = boolba.getDiffInVolume_CM3();
							double newLength_CM = calculateLengthOfFirstBlock_CM (diff_CM3);
							if (newLength_CM < block1.getLength_CM())
							{
								cutBlock (newLength_CM); // cut and accept (if possible) the half that can be melted
							}
						}
		}
	}
}

void Zolotosyorb::acceptBlock(GoldenBlock &block)
{
	double blockMass_G = block.getMass_G();
	double blockVolume_CM3 = block.getVolume_CM3();
	if (energyFly_J <= energyCurrent_J - energyAccept_J)
	{
			cout << "A" << endl;
			printf ("E0 %.1f (J) \n"
					"D %.1f (J) \n"
					"E0` %.1f (J) \n",
							energyCurrent_J, energyAccept_J, energyCurrent_J - energyAccept_J);
			energyCurrent_J -= energyAccept_J;
			boolba.increaseMass_G(blockMass_G);
			boolba.increaseVolume_CM3(blockVolume_CM3);
			energyRecuperator_J += calculateQForBlock(block);
	}
}

double Zolotosyorb::calculateQForBlock(GoldenBlock &block)
{
	// Q = dT * (m/1000) * energyHeat; - to heat (J = (Celcium * KG * J) / (Celcium*KG))
	double deltaT = meltingTemperature_Celcium - boolbaTemperature_Celcium;
	double mass_KG = block.getMass_G()/1000;
	double energyQ_J = deltaT * mass_KG * energyHeat_JperKelvinKG;
	return energyQ_J;
}

void Zolotosyorb::flyAway()
{
	cout << "F" << endl;
	printf ("E0 %.1f (J) \n"
			"D %.1f (J) \n"
			"E0` %.1f (J) \n",
			energyCurrent_J, energyFly_J, energyCurrent_J-energyFly_J);

	energyCurrent_J-=energyFly_J;
	printf ("E0 %.1f (J) \n"
			"V %.9f (CM3) \n"
			"M %.1f (G) \n",
			energyCurrent_J, boolba.getCurrentVolume_CM3(), boolba.getCurrentMass_G());
}
