/*
 * phone_number.cpp
 *
 *  Created on: Apr 28, 2020
 *      Author: vlad
 */


#include "phone_number.h"
#include <iostream>
#include <sstream>
using namespace std;

PhoneNumber::PhoneNumber(const string& international_number)
{

	stringstream ss (international_number);
	if (ss.peek() != '+') throw invalid_argument("loool 4head");
	ss.ignore(1);
	if (!(getline(ss, country_code_, '-'))) throw invalid_argument("loool 4head");
	if (!(getline(ss, city_code_, '-'))) throw invalid_argument("loool 4head");
	if (!(getline(ss, local_number_))) throw invalid_argument("loool 4head");
}

string PhoneNumber::GetCountryCode() const
{
	return country_code_;
}

string PhoneNumber::GetCityCode() const
{
	return city_code_;
}

string PhoneNumber::GetLocalNumber() const
{
	return local_number_;
}

string PhoneNumber::GetInternationalNumber() const
{
	return ("+" + country_code_+ "-" + city_code_ + "-" + local_number_);
}
