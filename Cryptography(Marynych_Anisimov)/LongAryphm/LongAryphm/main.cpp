//
// Created by Igor Volokhovych on 10/22/2019.
//
#include "LongInts.cpp"
#include <iostream>
#include <string>
#include "chin_theor.h"

void allOper();

using namespace std;
int main()
{
    char choose; cout<<"Please choose: 1.Operations with Big numbers;\n 2.Chinese Remainder Theorem;\n";
    cout<<"Your desicion is: "; cin>>choose;
    switch(choose){
        case '1':
        {allOper();
            LongInts a,b,c,d;
            string operation;
            cout<<"First operator: ";cin>>a;cout<<'\n';
            cout<<"Operation: "; cin>>operation;cout<<'\n';
            cout<<"Second operator: ";
            if(operation=="+")
            {cin>>b;c=a+b;}
            if(operation=="-")
            {cin>>b;c=a-b;}
            if(operation=="*")
            {cin>>b;c=a*b;}
            if(operation=="/")
            {cin>>b;c=a/b;}
            if(operation=="^")
            {unsigned int b; cin>> b;c=LongInts::pow(a,b);}
            if(operation==">")
            {cin>>b ;c=a>b;}
            if(operation=="<")
            { cin>>b;c=a<b;}
            if(operation=="=")
            {cin>>b;c=(a==b);}
            if(operation=="%")
            { cin>>b;c=a%b;}
            if(operation=="sqrt")
            {cin>>b; c=LongInts::sqrt(a);}
            if(operation=="mod+")
            {LongInts w; cin>>b; cout<<"Mod by: "; cin>>w;  c=LongInts::add_mod(a,b,w);} //cout<<"Enter mod: ";cin>>w;
            if(operation=="mod-")
            {LongInts w; cin>>b; cout<<"Mod by: "; cin>>w; c=LongInts::sub_mod(a,b,w);}
            if(operation=="mod*")
            {LongInts w; cin>>b; cout<<"Mod by: "; cin>>w; c=LongInts::mul_mod(a,b,w);}
            if(operation=="mod/")
            {LongInts w; cin>>b; cout<<"Mod by: "; cin>>w; c=LongInts::div_mod(a,b,w);}
            if(operation=="mod^")
            {unsigned int b; LongInts w; cin>>b; cout<<"Mod by: "; cin>>w; c=LongInts::pow_mod(a,b,w);}
            if(operation=="mod%")
            {unsigned int b; LongInts w; cin>>b; cout<<"Mod by: "; cin>>w; c=LongInts::mod_mod(a,b,w);}
            cout<<"Result:" <<c;
            break;
        }
        case '2':
        {
            cout<<"NOTE: TO STOP COUT, PRESS CTRL+D\n Enter in format: (x [space] y)";
            solver::execute();
        }


    }


}

void allOper() {
    cout<<"\t\tAvailable operations:\n";
    cout<<"+ - adds two operators\n\t\t\tmod+ - adds two operators with mod\n";
    cout<<"- - subtracts two operators\n\t\t\tmod- - subtracts two operators with mod\n";
    cout<<"* - multiplies two operators\n\t\t\tmod* - multiplies two operators with mod\n";
    cout<<"/ - divides two operators\n\t\t\tmod/ - divides two operators with mod\n";
    cout<<"% - remainder of division\n\t\t\tmod% - remainder of division with mod\n";
    cout<<"^ - power\n\t\t\tmod^ - power with mod";
    cout<<"\t\tLogical:\n >- A is bigger than B (a>b=true, a<b=false)\n <- A is smaller than B(a>b=false, a<b=true)\n =-A is equal to B\n";
};

