#include<iostream>  //Header file for cin & cout
#include<cmath>  //Header file for mathematical operartions
#include<iomanip> //Header file for precession
using namespace std;  //calling the standard directory

//Given dy/dx
float f(float(x),float(y), float(z))
{
    return y/x-z*pow(y,2);
}
//Given dz/dx
float g(float(x),float(y), float(z)){
    return pow(z,2)*y;
}
float Analytf(float(x),float(y), float(z))
{
    return exp(pow(x,2));
}
float Analytg(float(x),float(y), float(z)){
    return 2*x*exp(pow(-x,2));
}

int main()
{
    int n,i,j;
    float h;
    cout<<"Aim : To solve a differential equation by Second Order Runge-Kutta's Method"<<endl;
    cout<<"Given Equtaions z' = z^2*y, y'= y/x-z*y^2 "<<endl;

//Entering the Number of Iterations
    cout<<"Enter the number of solutions "<<endl;
    cin>>n;

//Entering the width of step size
    cout<<"Enter The Value of Step Size "<<endl;
    cin>>h;

    long double y[n],e[n],x[n],Y[n],L[n],k[n][n],z[n],Analyt_y[n],Analyt_z[n];

//Entering the initial values of x & y
    cout<<"Enter the value of x0 "<<endl;
    cin>>x[0];
    cout<<"Enter The Value of y0 "<<endl;
    cin>>y[0];
    cout<<"Enter The Value of z0 "<<endl;
    cin>>z[0];
    Analyt_y[0]=y[0];
    Analyt_z[0]=z[0];

//Calculating the value of x,y,z
    cout<<"Solution of the given differential equation by Second Order Runga Kutta Method is "<<endl;
    for(i=1;i<=n;i++)
    {
        x[i]=x[i-1]+h;
        y[i]=y[i-1]+h*f(x[i-1],y[i-1],z[i-1]);
        z[i]=z[i-1]+h*g(x[i-1],y[i-1],z[i-1]);
        Analyt_y[i]=Analyt_y[i-1]+h*Analytf(x[i-1],Analyt_y[i-1],Analyt_z[i-1]);
        Analyt_z[i]=Analyt_z[i-1]+h*Analytg(x[i-1],Analyt_y[i-1],Analyt_z[i-1]);
        cout<<'['<<i<<']';
        cout<<"\tx["<<h*i<<"] = "<<setprecision(5)<<x[i];
        cout<<"\ty["<<h*i<<"] = "<<setprecision(6)<<y[i];
        cout<<"\tz["<<h*i<<"] = "<<setprecision(5)<<z[i];
        cout<<"\tALPHA_y["<<h*i<<"] = "<<setprecision(5)<<Analyt_y[i];
        cout<<"\tALPHA_z["<<h*i<<"] = "<<setprecision(5)<<Analyt_z[i]<<endl;

    }
//Calculating the value of y



/*//Calculating & Printing the values of k1, k2 & y
    for(j=1;j<=n;j++)
    {
        k[1][j]=h*f(x[j-1],y[j-1]);
        cout<<"K[1] = "<<k[1][j]<<"\t";
        k[2][j]=h*f(x[j-1]+h,y[j-1]+k[1][j]);
        cout<<"K[2] = "<<k[2][j]<<"\n";
        y[j]=y[j-1]+((k[1][j]+k[2][j])/2);
        cout<<"y["<<h*j<<"] = "<<setprecision(5)<<y[j]<<endl;
    }*/

    return 0;
}