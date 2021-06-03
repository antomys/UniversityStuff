#include <iostream>
#define a func()
using namespace std;


int cnt = 0;

int func(){
    return cnt--;
}

int calc ()
{
	int b = a - a;
	return b;
}

int main()
{

    cout << calc() << '\n';
    return 0;
}
