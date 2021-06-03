/*
 * command.cpp
 *
 *  Created on: Mar 4, 2020
 *      Author: vlad
 */

#include <iostream>
#include <chrono>
#include <ctime>
#include <string>

#include "logEntry.hpp"
using namespace std;

LogEntry::LogEntry()
{
	this -> logEntry = "start";
}

LogEntry::LogEntry(string command)
{
	string tmp = getCurrentDate();
	for (int i = 0; tmp[i] != '\0'; i++)
	{
		logEntry += tmp[i];
	}
    logEntry += ' ' + command;
 //	this->logEntry = command;
}

void LogEntry::buildFromLog(string logFileEntry)
{
	this -> logEntry = logFileEntry;
	//cout << "new object logentry : " <<logEntry << endl;
}

string LogEntry::getCurrentDate()
{
	auto start = std::chrono::system_clock::now();
	tm m_time;
	std::time_t end_time = std::chrono::system_clock::to_time_t(start);
	localtime_r(&end_time, &m_time);
	std::string s (30, '\0');
	strftime (&s[0], s.length(), "%Y.%m.%d %H-%M-%S", &m_time);
	return s;
}

string LogEntry::getLogEntry() {return logEntry;}
