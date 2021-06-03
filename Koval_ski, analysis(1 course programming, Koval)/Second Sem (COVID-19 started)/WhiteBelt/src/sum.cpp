#include <iostream>
#include <vector>
#include <set>
#include <map>
#include <sstream>
#include <istream>
#include <iomanip>
#include <algorithm>
using namespace std;
// Реализуйте функции и методы классов и при необходимости добавьте свои

class Date {
public:
  int GetYear() const { return year;}
  int GetMonth() const { return month;}
  int GetDay() const {return day;}
  string toString() const // return string representation of Date. YYYY-MM-DD
  {
	  string result;
	  stringstream ss;
	  ss << setfill ('0') << setw(4) << to_string(year) << "-" << setfill ('0')<< setw(2) << to_string(month)
	     << "-" << setfill ('0')  << setw(2) << to_string(day);
	  result = ss.str();
	  return result;
  }
  Date (int newDay, int newMonth, int newYear)
  {
	  if (newMonth <= 0 || newMonth > 12) throw "Month value is invalid: " + to_string(newMonth);
	  if (newDay <= 0 || newDay > 31) throw "Day value is invalid: " + to_string(newDay);
	  day = newDay;
	  month = newMonth;
	  year = newYear;
  }
  Date ()
  {
	  day = 0;
	  month = 0;
	  year = 0;
  }
private:
  int year;
  int month;
  int day;
};

bool operator<(const Date& lhs, const Date& rhs)
{
	if (lhs.GetYear() < rhs.GetYear()) return true;
	if (lhs.GetYear() > rhs.GetYear()) return false;
	if (lhs.GetYear() == rhs.GetYear())
	{
		if (lhs.GetMonth() < rhs.GetMonth()) return true;
		if (lhs.GetMonth() > rhs.GetMonth()) return false;
		if (lhs.GetMonth() == rhs.GetMonth())
		{
			if (lhs.GetDay() < rhs.GetDay()) return true;
			if (lhs.GetDay() >= rhs.GetDay()) return false;
		}
	}
	return false;
}

// add exception handling! (after correct input is processed in a correct manner)
istream& operator>> (istream& input, Date& date)
{
	try
	{
	string tmp;
	int year, month, day;
	getline (input, tmp, ' ');
	bool checkForSpaces = false;
	while (tmp == "")
	{
		checkForSpaces = true;
		getline(input, tmp, ' ');
	}
	if (checkForSpaces)
	{
		throw ("Wrong date format: " + tmp);
	}
	stringstream ss;
	ss << tmp;
	if (!(ss >> year) || ss.peek() != '-') throw ("Wrong date format: " + tmp);
	ss.ignore(1);
	if (!(ss >> month) || ss.peek() != '-') throw ("Wrong date format: " + tmp);
	ss.ignore(1);
	if (!(ss >> day)) throw ("Wrong date format: " + tmp);
	string checkIfStreamIsEmpty;
	if (!(ss >> checkIfStreamIsEmpty))
	{
	date = Date (day, month, year);
	return input;
	}
	else throw ("Wrong date format: " + tmp);
	} catch (string& ex) {
		string tmp;
		getline (input, tmp);
		throw ex;
		return input;
	}
}

class Database {
public:
  void AddEvent(const Date& date, const string& event)
  {
	  dateEventsMap[date].insert(event);
	  return ;
  }
  bool DeleteEvent(const Date& date, const string& event)
  {
	  if (dateEventsMap.count(date) > 0 && dateEventsMap[date].count(event) > 0)
	  {
		  dateEventsMap[date].erase(event);
		  return true;
	  }
	  else
	  {
		  return false;
	  }

  }
  int  DeleteDate(const Date& date)
  {
	  if (dateEventsMap.count(date) > 0)
	  {
		  int amountOfEvents = dateEventsMap[date].size();
		  dateEventsMap.erase(date);
		  return amountOfEvents;
	  }
	  else
	  {
		  return 0;
	  }
  }
  set<string> Find(const Date& date) const
  {
	  if (dateEventsMap.count (date) > 0)
	  {
		  return dateEventsMap.at(date);
	  }
	  else {set<string> null; return null;}
  }

  void Print() const
  {
	  for (const auto& node : dateEventsMap)
	  {
		  for (const auto& entry : node.second)
		  {
			  cout << node.first.toString() << ' ' << entry << endl;
		  }
	  }
  }
private:
  map <Date, set<string>> dateEventsMap;
};





int main() {
	Database db;
	//	Date d (1, 1, 2012);
	//  Date wrong (1, 2, 2012);

	/* AddEvent & DeleteEvent test
	 *
	db.AddEvent(d, "test");
	cout << db.DeleteEvent(d, "nottest") << endl; // false
	cout << db.DeleteEvent(d, "test") << endl; // true
	cout << db.DeleteEvent (wrong, "test") << endl; // false
	 */

	/* AddEvent & DeleteDate test
	db.AddEvent (d, "test1");
	db.AddEvent (d, "test2");
	cout << db.DeleteDate(d) << endl; // 2
	cout << db.DeleteDate(wrong) << endl; // 0
	for (string event : db.Find(d)) // prints nothing
 		cout << event << endl;
	*/


	/* Find test
	db.AddEvent (d, "test1");
	db.AddEvent (d, "test2");
	for (string event : db.Find(d))  // test1 \n test2 \n
		cout << event << endl;

	for (string event : db.Find(wrong)) // nothing
		cout << event << endl;
	 */

	/* Print test // 2012-01-01 test1 \n  2012-01-01 test2 \n 2012-02-01 test1023012 \n
	db.AddEvent(d, "test2");
	db.AddEvent(d, "test1");
	db.AddEvent(wrong, "test1023012");
	db.Print();
	*/

	string command;
	// Считайте команды с потока ввода и обработайте каждую
	try
	{
		while (getline(cin, command))
		{
			stringstream ss (command);
			string token;
			while (getline(ss, token, ' '))
			{

				if (token == "Add")
				{
					Date date;
					string event;
					ss >> date >> event;
					db.AddEvent(date, event);
				}
				else if (token == "Del")
				{
					Date date;
					string event;
					ss >> date;

					if (ss >> event)
					{
						bool isDeleted = db.DeleteEvent(date, event);
						if (isDeleted) cout << "Deleted successfully" << endl;
						else cout << "Event not found" << endl;
					}
					else
					{
						int amountOfDeletedEvents = db.DeleteDate(date);
						cout << "Deleted " << amountOfDeletedEvents << " events" << endl;
					}
				}
				else if (token == "Find")
				{
					Date date;
					ss >> date;
					for (const string& event : db.Find(date))
						cout << event << endl;
				}
				else if (token == "Print")
				{
					db.Print();
				}
				else throw ("Unknown command: " + token);
				/*
				cout << "Printing all events:" << endl;
				db.Print();
				 */

			}
		}
	} catch (string& ex)
	{
		cout << ex << endl;
	}
	return 0;
}
