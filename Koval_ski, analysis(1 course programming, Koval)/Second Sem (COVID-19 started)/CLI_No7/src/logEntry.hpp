/*
 * command.hpp
 *
 *  Created on: Mar 4, 2020
 *      Author: vlad
 */

#ifndef LOGENTRY_HPP_
#define LOGENTRY_HPP_

#ifndef string
#include <string>
#endif

class LogEntry
{
public:
LogEntry();
LogEntry(std::string command);
void buildFromLog(std::string logFileEntry);
std::string getLogEntry();
private:
std::string getCurrentDate();
std::string logEntry;
};

#endif /* LOGENTRY_HPP_ */
