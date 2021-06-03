/*
 * CurveBuilder.cpp
 *
 *  Created on: Nov 28, 2019
 *      Author: vlad
 */
#include <iostream>
#include <cmath>
#include <sstream>
#include "CurveBuilder.hpp"
using namespace std;

int CurveBuilder::mod4(int number, int add)
{
	if (number + add < 0)
	{
		add = abs(add) - number;
		return 4-abs(add);
	}
	else return abs((number+add) % 4);
}
void CurveBuilder::build(int n)
{
	array2d.resize (40);
	for (int i = 0; i < 40; i++)
	{
		array2d[i].resize(40, make_pair(9, 0));
	}

	canvas.resize (400);
	for (int i = 0; i < 400; i++)
	{
		canvas[i].resize(400, make_pair(false, 0));
	}
	currentPositionX = 30;
	currentPositionY = 20;

	if (n != 0)
	{
		cout << hilbertPlus(n, 0) << endl;
			for (int i = 0; i < 40; i++)
			{
				for (int j = 0; j < 40; j++)
				{
					if (array2d[i][j].first == 0)
					{
						for (int k = 0; k < 10; k++)
						{
							if (inRange((i*10)+5-k,(j*10)+5))
							{
								canvas[(i*10)+5-k][(j*10)+5].first = true;
								canvas[(i*10)+5-k][(j*10)+5].second = array2d[i][j].second;
							}
						}
					}
					if (array2d[i][j].first == 1)
								{
									for (int k = 0; k < 10; k++)
									{
										if (inRange((i*10)+5,(j*10)+5+k))
										{
											canvas[(i*10)+5][(j*10)+5+k].first = true;
											canvas[(i*10)+5][(j*10)+5+k].second = array2d[i][j].second;
										}
									}
								}
					if (array2d[i][j].first == 2)
								{
									for (int k = 0; k < 10; k++)
									{
										if (inRange((i*10)+5+k,(j*10)+5))
										{
											canvas[(i*10)+5+k][(j*10)+5].first = true;
											canvas[(i*10)+5+k][(j*10)+5].second = array2d[i][j].second;
										}
									}
								}
					if (array2d[i][j].first == 3)
								{
									for (int k = 0; k < 10; k++)
									{
										if (inRange((i*10)+5,(j*10)+5-k))
										{
											canvas[(i*10)+5][(j*10)+5-k].first = true;
											canvas[(i*10)+5][(j*10)+5-k].second = array2d[i][j].second;
										}
									}
								}
				}
			}
			double delta = 255/(double)(n*n*n*n);
			double color = 255;
			double delta1 = 0/(double)(n*n*n*n);
			double color1 = 255;
			double delta2 = 255/(double)(n*n*n*n);
			double color2 = 0;

			cout << "<table> \n";
			for (int i = 0; i < 400; i++)
			{

				cout << "<tr> \n";
				for (int j = 0; j < 400; j++)
				{
					if (canvas[i][j].first == true)
						{
							double tmp_color = color - delta*(double)canvas[i][j].second;
							//cout << color << endl;
							string q;
							stringstream ss;
							ss << std::hex << (int)tmp_color;
							q = ss.str();

							double tmp_color1 = color1 -  delta1*(double)canvas[i][j].second;
							//cout << color << endl;
							string q1;
							stringstream ss1;
							ss1 << std::hex << (int)tmp_color1;
							q1 = ss1.str();

							double tmp_color2 = color2 + delta2*(double)canvas[i][j].second;
							//cout << color << endl;
							string q2;
							stringstream ss2;
							ss2 << std::hex << (int)tmp_color2;
							q2 = ss2.str();

							cout << "<td bgcolor = #" << q << q1 << q2 <<">  </td> \n";

						}
					else cout << "<td bgcolor = #FFFFFF>  </td> \n";
				}
				cout << "</tr> \n";
			}
			cout << "</table> \n";



		/*
			for (int i = 0; i < 40; i++)
			{
				for (int  j = 0; j < 40; j++)
				{
					if (i == 10 && j == 10) cout <<"!" << array2d[i][j] << ' ';
					else if (array2d [i][j] != 9)	cout <<array2d [i][j]<<"  ";
					else cout <<"   ";
				}
				cout << endl << endl;
			}
			*/
	}
	else
	{
		cout << 0 << endl;
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
				canvas[i][j].first=true;
		}


		cout << "<table> \n";
		for (int i = 0; i < 400; i++)
		{

			cout << "<tr> \n";
			for (int j = 0; j < 400; j++)
			{
				if (canvas[i][j].first == true) cout << "<td bgcolor = #000000>  </td> \n";
				else cout << "<td bgcolor = #999999>  </td> \n";
			}
			cout << "</tr> \n";
		}
		cout << "</table> \n";

	}
}

int CurveBuilder::dot()
{
	return 0;
}

int CurveBuilder::hilbertPlus(int level, int direction)
{
/*	if (level == 0)
	{
		if (currentPositionY >= 0 && currentPositionY <=39 && currentPositionX >= 0 && currentPositionX <=39)
			array2d[currentPositionY][currentPositionX] = direction;
	}*/
	//cout << "Plus Level" << level << "Direction" << direction << endl;
	return (level == 0)? dot():
					hilbertMinus (level-1, mod4(direction, 1))
					+
					connector(mod4(direction, -2))
					+
					hilbertPlus (level-1, mod4(direction, 0))
					+
					connector(mod4(direction, -1))
					+
					hilbertPlus (level-1, mod4(direction, 0))
					+
					connector(mod4(direction, 0))
					+
					hilbertMinus (level-1, mod4(direction, -1));
}


int CurveBuilder::hilbertMinus(int level, int direction)
{
/*	if (level == 0)
		{
			if (currentPositionY >= 0 && currentPositionY <=39 && currentPositionX >= 0 && currentPositionX <=39)
				array2d[currentPositionY][currentPositionX] = direction;
		}*/
//	cout << "Minus Level" << level << "Direction" << direction << endl;
	return (level == 0)? dot():
					hilbertPlus (level-1, mod4(direction, -1))
					+
					connector(mod4(direction, -2))
					+
					hilbertMinus (level-1, mod4(direction, 0))
					+
					connector(mod4(direction, 1))
					+
					hilbertMinus (level-1, mod4(direction, 0))
					+
					connector(mod4(direction, 0))
					+
					hilbertPlus (level-1, mod4(direction, 1));
}

int CurveBuilder::connector (int direction)
{
	//cout << "Connector Dir" << direction << endl;
	int tmpX = currentPositionX;
	int tmpY = currentPositionY;
	if (direction == 0) currentPositionY--;
	if (direction == 1) currentPositionX++;
	if (direction == 2) currentPositionY++;
	if (direction == 3) currentPositionX--;
	if (tmpY >= 0 && tmpY <=39 && tmpX >= 0 && tmpX <=39)
	{
		array2d[tmpY][tmpX].first = direction;
		array2d[tmpY][tmpX].second = counter++;
	}
	return 1;
}

bool CurveBuilder::inRange(int x, int y)
{
	if (x >= 0 && x < 400 && y >= 0 && y < 400)
		return true;
	else return false;
}
