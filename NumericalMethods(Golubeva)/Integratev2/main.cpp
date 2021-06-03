#define __STDCPP_WANT_MATH_SPEC_FUNCS__ 1
#include <iostream>
#include <conio.h>
#include <math.h>
#include <cmath>
#include <vector>
#include "poly34.cpp"
#include "poly34.h"
const int n = 4;
const int N = 100;
double c1,c2,c3,c4;

using namespace std;

double f(double x)
{
    return exp(-x)*sin(x);
}

double dis(double a, double b, double c)// функця по нахождению дискриминанта
{
    double xld;
    xld = ((b*b) - (4 * a*c));
    cout << "D= " << xld << endl;
    return xld;
}


long double fact(int N)
{

    if(N < 0) // если пользователь ввел отрицательное число
        return 0; // возвращаем ноль
    if (N == 0) // если пользователь ввел ноль,
        return 1; // возвращаем факториал от нуля - не удивляетесь, но это 1 =)
    else // Во всех остальных случаях
        return N * fact(N - 1); // делаем рекурсию.
}

double Calculate (double a, double b, double c, double d, double &xa, double &xb)
{
    double disb;
    d=dis(a, b, c);
    if (d < 0)//ветвление для защиты от отрицательного дискриминаната
    {
        cout << "No roots." << endl;
    }
    else
    {
        disb = sqrt(d); //вычисляем квадратный корень
        xa = ((-b) + disb) / (2 * a);// находим корни
        xb = ((-b) - disb) / (2 * a);
        cout << "First root: " << xa << endl;//выводим корни
        cout << "Second root: " << xb << endl;
    }
    return xa,xb;
}
double Coeff (double x, double &c)
{
    c= fact(2)*fact(3-1)/(x*pow((2*x-4),2));
    return c;
}

void integralCalc(double c1,double c2,double xa, double xb)
{
    double res = (c2*sin(xb))+(c1*sin(xa));
    cout<< res;
}
double Zalysh(int n)
{
    double ksi=0.00001;
    long double res;
    res=(fact(n)*fact(n)/(fact(2*n))*pow(f(ksi),2*n));
    return res;
}



void StepinTwo(){
    double xa,xb;
    cout<<"1. Умови:\ts=0; n=2\n";
    cout<<"2. n=2 => Lagger's polynom power 2 is: x^2-4x+2\n";
    cout<<"Roots of Lagger's polynom are: (x^2-4x+2=0)\n";
    double a=1,b=-4,c=2;
    double d;
    d=dis(a, b, c);
    Calculate(a,b,c,d,xa,xb);
    cout<<"3. Finding coeffs (ci) Formula: Ci= n!Г(s+n+1)/(xi[(Ln^s)'(xi)]^2\n";



    c1=Coeff(xa,c1);
    c2=Coeff(xb,c2);
    cout<<"Coefficients: "<<c1<<' '<<c2<<'\n';
    cout<<"4. Calculating Integral: ";
    integralCalc(c1,c2,xa,xb);
    long double zal=Zalysh(2);
    cout<<"\nЗалишковий: "<<zal;

}
void Stepin4()
{
    double xa,xb;
    cout<<"1. Умови:\ts=0; n=4\n";
    cout<<"2. n=4 => Lagger's polynom power 4 is: x^4-16x^3+72x^2-96x+24)\n";
    cout<<"Roots of Lagger's polynom are: (x^4-16x^3+72x^2-96x+24=0)\n";
    double a=-16,b=72,c=-96,d=24;
    double x[4];
    SolveP4(x,a,b,c,d);
    //16 x^6 - 384 x^5 + 3456 x^4 - 14592 x^3 + 29952 x^2 - 27648 x + 9216
    for(int i=0;i<4;i++)
        cout<<i+1<<" Root is:"<<x[i]<<'\n';
    cout<<"3. Finding coeffs (ci) Formula: Ci= n!Г(s+n+1)/(xi[(Ln^s)'(xi)]^2\n";
    double coeff[4];
    for(int i=0;i< 4;i++) {
        coeff[i] = 576 / (x[i] * (16 * pow(x[i], 6) - 384 * pow(x[i], 5) + 3456 * pow(x[i], 4) - 14592 * pow(x[i], 3) +
                                  29952 * pow(x[i], 2) - 27648 * x[i] + 9216));
        cout << "COEFFS: " << i+1 <<": "<<coeff[i]<<'\n';
    }
    double res;
    for(int i=0;i<4;i++)
        res+=coeff[i]*sin(x[i]);
    cout<<"ІНТЕГРУВАННЯ РЕЗУЛЬТАТ: "<<res<<'\n';
    long double zal=Zalysh(4);
    cout<<"Залишковий: "<<zal<<'\n';
}

int main()
{

Stepin4();
StepinTwo();
return 0;

/*double x1=4.53662;
double res = 576/(x1* (16*pow(x1,6) - 384* pow(x1,5) + 3456* pow(x1,4) - 14592* pow(x1,3) + 29952*pow( x1,2) - 27648* x1 + 9216));
cout<<res;*/
}