/*
 * CLI.cpp
 *
 *  Created on: Mar 5, 2020
 *      Author: vlad
 */
#include <regex>
#include <iostream>
#include <string>
#include <sstream>
#include <fstream>
#include <deque>
#include <vector>
#include "CLI.hpp"
using namespace std;

void CLI::run()
{
	cout << "Path to logfile is " << pathToLogfile << endl;
	// print help to console
	help();
	previouslyCalledFn = "";
	string command;
	// infinite loop that breaks after quit() or exit()
	while (loopBreakerFlag)
	{
		cout << "$";
		// get command from user
		getline(cin, command);
		command = trimString (command);
		if (command == "") continue;
		// and call it
		callFunction (command);
		// push it to log
		if (writeToHistoryFlag || previouslyCalledFn == "logON" || previouslyCalledFn == "logOFF")
		{
			LogEntry* entryPtr = new LogEntry(previouslyCalledFn);
			history.push_back(entryPtr);
		}
	}
	logClearHistory();
}


void CLI::help()
{
	string result =
			"\n Commands that are recognized: \n"
			"- help \n"
			"- quit : save + exit \n"
			"- exit : exit from interface \n"
			"- save : write to logfile \n"
			"- load : load history from current logfile\n"
			"- logfile \n"
			"  (w/o param - return the name of the logfile, w/ param (name of logfile) - sets new logfile) \n"
			"- list \n"
			"date - command \n"
			"(2020.03.04 10:45:32) \n"
			"- log on - write to log \n"
			"- log off - stop writing to log \n"
			"- log append \n"
			"- log new \n"
			"- log clear history (what list displays) \n";
	previouslyCalledFn = __func__;
	 cout << result;
}

bool CLI::save()
{
	previouslyCalledFn = __func__;
	bool test = checkValidity(pathToLogfile);
	if (!test) return false;
	ofstream of (pathToLogfile, (processingMode ? ios::app : ios::trunc));
	for (int i = 0; i <(int)history.size(); i++)
	{
		LogEntry entry = *(history[i]);
		of << entry.getLogEntry() + "\n";
	}
	LogEntry saveEntry ("save");
	of << saveEntry.getLogEntry() + "\n";
	if (!of.fail()) cout << "Successful save!" << endl;
	else cout << "Something went wrong while saving..." << endl;
	of.close();
	return true;
}

bool CLI::load()
{
	bool test = checkValidity(pathToLogfile);
	if (!test) return false;

	string stringFromFile = "";
	ifstream ifs(pathToLogfile);
	logClearHistory();
	while (std::getline(ifs, stringFromFile))
	{
		if (stringFromFile == "") continue;
		LogEntry* ptr = new LogEntry();
		LogEntry& entry = *(ptr);
		entry.buildFromLog(stringFromFile);
		history.push_back(ptr);
	}
	previouslyCalledFn = __func__;
	if (ifs.eof()) cout << "Load successful!" << endl;
	else cout << "Something went wrong while loading..." << endl;
	return true;
}

void CLI::exit()
{
	cout << "Exiting..." << endl;
	loopBreakerFlag = false;
	previouslyCalledFn = __func__;
}

void CLI::list()
{

	cout << "Current processing mode:" << (processingMode ? "append" : "truncate") << endl;
	cout << "Displaying the history..." << endl;
	for (int i = 0; i < (int)history.size(); i++)
	{
		LogEntry tmp = *(history[i]);
		cout << tmp.getLogEntry() << endl;
	}
	previouslyCalledFn = __func__;
}

void CLI::logClearHistory()
{
	cout << "Cleaning the history..." << endl;
	for (int i = 0; i < (int)history.size(); i++)
	{
		delete history[i];
	}
	history.clear();
	cout << "History cleaned!" << endl;
	previouslyCalledFn = __func__;
}

void CLI::quit()
{
	exit();
	save();
	previouslyCalledFn = __func__;
}

void CLI::logON()
{
	writeToHistoryFlag = true;
	cout << "History of commands enabled. Use \"save\" in order to save the data to log." << endl;
	previouslyCalledFn = __func__;
}

void CLI::logOFF()
{
	writeToHistoryFlag = false;
	cout << "History of commands disabled." << endl;
	previouslyCalledFn = __func__;
}

void CLI::logAppend()
{
	processingMode = true;
	cout << "Processing mode of logfile : append new entries to log" << endl;
	previouslyCalledFn = __func__;
}

void CLI::logNew()
{
	processingMode = false;
	cout << "Processing mode of logfile : calling \"save\" destroys previous data in logfile." << endl;
	previouslyCalledFn = __func__;
}

void CLI::logfile()
{
	cout <<"Path to logfile is \"" << pathToLogfile  << "\""<< endl;
	previouslyCalledFn = __func__;
}

void CLI::logfile(string path)
{
	bool test = checkValidity(path);

	string functionName = __func__;
	previouslyCalledFn = functionName + "(" + path +")";

	if (!test) return;
	pathToLogfile = path;
	cout << "A new logfile has been set. \n";
}


// Helper methods.


void CLI::callFunction(string command)
{
	// vector of tokens
	vector<string> tokens;
	// tokenizing string
	stringstream ss (command);
	string token = "";
	while (getline(ss, token, ' '))
		tokens.push_back(token);

	// if-conditions depending on the amount of
	if (tokens.size() == 1)
	{
		if (tokens[0] == "help") help();
		else if (tokens[0] == "quit") quit();
		else if (tokens[0] == "save") save();
		else if (tokens[0] == "load") load();
		else if (tokens[0] == "exit") exit();
		else if (tokens[0] == "logfile") logfile();
		else if (tokens[0] == "list") list();
		else if (tokens[0] == "logNew") logNew();
		else if (tokens[0] == "logAppend") logAppend();
		else wrongCommand(command);
	}
	else if (tokens.size() == 2)
	{
		if (tokens[0] == "logfile") logfile(tokens[1]);
		else if (tokens[0] == "log" && tokens[1] == "on") logON();
		else if (tokens[0] == "log" && tokens[1] == "off") logOFF();
		else if (tokens[0] == "log" && tokens[1] == "append") logAppend();
		else if (tokens[0] == "log" && tokens[1] == "new") logNew();
		else wrongCommand(command);
	}
	else if (tokens.size() == 3)
	{
		if (tokens[0] == "log" && tokens[1] == "clear" && tokens[2] == "history") logClearHistory();
		else wrongCommand(command);
	}
	else wrongCommand(command);
}

void CLI::wrongCommand(string command)
{
	previouslyCalledFn = "failed: " + command;
	cout << "Wrong command! Type \"help\" for more info. \n";
}

bool CLI::checkValidity (string path)
{
	std::ifstream infile(path);
	if (!infile.good())
	{
		cout <<  "An error has occurred while trying to open a stream with such a filename. Try again. \n";
		return false;
	}

	infile.close();
	return true;
}

string CLI::trimString(string& str)
{
	string result;
	result = std::regex_replace(str, std::regex("^\\s+"), std::string(""));
	result = std::regex_replace(result, std::regex("\\s+$"), std::string(""));
	return result;
}
