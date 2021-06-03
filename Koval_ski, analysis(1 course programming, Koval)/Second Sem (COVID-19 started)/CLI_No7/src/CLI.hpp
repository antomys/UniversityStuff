/*
 * CLI.hpp
 *
 *  Created on: Mar 5, 2020
 *      Author: vlad
 */

#ifndef CLI_HPP_
#define CLI_HPP_

#ifndef string
#include <string>
#endif

#ifndef LOGENTRY_HPP_
#include "logEntry.hpp"
#endif

#ifndef deque
#include <deque>
#endif

class CLI
{
public:
void run();
private:


//typedef void (CLI::*CLIVoidFnPtr) (void);

void help(); // done
void quit(); // done
void exit(); // done
bool save(); // done
bool load(); // done
void logfile(); // done
void logfile (std::string path); // done
void list(); // done
void logON(); // done
void logOFF(); // done
void logAppend(); // done
void logNew(); // done
void logClearHistory(); // done

void callFunction(std::string command);
void wrongCommand(std::string command);
bool checkValidity(std::string path); // checks the validness of the path to logfile
std::string trimString (std::string& str);


std::deque<LogEntry*> history;
std::string pathToLogfile = "not set. Use logfile [fileName] to set a name for the log file.";
std::string previouslyCalledFn = "";
bool writeToHistoryFlag = true;
bool processingMode = false; // false for trunc, true for append
bool loopBreakerFlag = true;
};



#endif /* CLI_HPP_ */
