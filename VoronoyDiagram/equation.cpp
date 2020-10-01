#include "equation.h"
#include <iomanip>
#include<math.h>
#include<iostream>
using std::cerr;
using std::max;

#include <QDebug>

void Equation::normalize(){
    while(!coef.empty() && fabs(coef.back()) <=1e-5){
        coef.pop_back();--degree;
    }
/*
    for(int i=0;i<=degree;++i)
        coef[i]/=coef.back();
*/
    if(degree < 0){
        degree = 0;
        coef.append(0);
    }

}

qreal Equation::valAt(qreal t)const{
    qreal r = 0;
    qreal p = 1;
    for(int i=0;i<=degree;++i){
        r = r  + coef[i]*p;
        p = p*t;
    }
    return r;
}

Equation::Equation(int d)
{
    solved = false;
    degree = d;
    for(int i=0;i<=d;++i)
        coef.append(0);
}

Equation Equation::operator +(const Equation &b){
    Equation res(max(degree ,b.degree));
    for(int i=0;i<=res.degree;++i)
        res.coef[i] = coefAt(i)+b.coefAt(i);
    res.normalize();
    return res;
}
Equation Equation::operator -(const Equation &b){
    Equation res(max(degree ,b.degree));
    for(int i=0;i<=res.degree;++i)
        res.coef[i] = coefAt(i)-b.coefAt(i);
    //res.normalize();
    return res;
}

Equation Equation::operator *(const Equation &b){
    Equation res(degree + b.degree);
    for(int i=0;i<=degree;++i)
        for(int j=0;j<=b.degree;++j)
            res.coef[i+j] += coefAt(i)*b.coefAt(j);
    //res.normalize();
    return res;
}

Equation Equation::dif(){
    Equation res(degree-1);
    for(int i=1;i<=degree;++i)
        res.coef[i-1] = coef[i]*i;
    //res.normalize();
    return res;
}

qreal Equation::coefAt(int pos)const{
    if(pos>=0 && pos<=degree)
        return coef[pos];
    return 0;
}

Equation Equation::operator -(){
    Equation res = *this;
    for(int i=0;i<=degree;++i)
        res.coef[i] *=-1;
    //res.normalize();
    return res;
}
Equation Equation::operator /(const Equation &b){
    Equation res(degree-b.degree);
    int p1 = degree;
    int p2 = degree - b.degree;
    Equation t = *this;
    while(p2>=0){
        qreal co = t.coef[p1]/b.coef[b.degree];
        res.coef[p2] = co;
        for(int i=p1;i>=p1-b.degree;--i){
            t.coef[i]-=co*b.coef[i-p1+b.degree];
        }
        --p1;--p2;
    }
    //res.normalize();
    return res;
}

Equation::Equation(QList<qreal> l){
    degree = l.size()-1;
    coef = l;
}

Equation Equation::operator %(const Equation &b){
    Equation res =(*this - (*this/b)*b);
    res.normalize();
    return res;
}
void Equation::print(){
    cerr << coefAt(0)<<"";
    for(int i=1;i<=degree;++i)
        cerr << "+x^"<<i<<"*"<<coefAt(i)<<"";

    cerr<<"\n";


    //cerr <<valAt(19.0/200) <<  " "<<valAt(19.2/200)<< " " << valAt(19.4/200) << " "<<valAt(18.8898/200) << endl;


}

QList<qreal> Equation::localize(QList<Equation>& st,qreal l,qreal r){

    qreal m = (l+r)/2;


    if(r-l<1e-5){
        if(fabs(st[0].valAt(r))<=1e-3)
            return QList<qreal>() << r;
        else
            return QList<qreal>();
    }

    int a = 0,b=0,c=0;

    for(int i=0;i<st.size()-1;++i){
        a+= (st[i].valAt(l)*st[i+1].valAt(l)<0);
        //qDebug() << st[i].valAt(l) << " " << st[i+1].valAt(l) << "";
        b+= (st[i].valAt(m)*st[i+1].valAt(m)<0);
        c+= (st[i].valAt(r)*st[i+1].valAt(r)<0);
    }
    QList<qreal> res;
    if(a!=b){
        if(a-b == 1)
            res.append(bs(l,m));
        else
            res.append(localize(st,l,m));

    }

    if(b!=c){
        if(b-c == 1)
            res.append(bs(m,r));
        else
            res.append(localize(st,m,r));

    }

    return res;
}

qreal Equation::bs(qreal l,qreal r){
    qreal m;
    while(r-l>1e-7){
        m =(l+r)/2;
        if(valAt(l)*valAt(m)<=0)
            r = m;
        else
            l = m;
    }
    return l;
}

QList<qreal> Equation::solve(){

    if(solved)
        return solution;

    solved = true;

    QList<Equation> sturm;
    sturm.append(*this);
    sturm.append(sturm.back().dif());
    Equation pr = sturm[0];
    Equation cu = sturm[1];
    while(sturm.back().degree>0){
        sturm.append(-(pr%cu));
        //pr.print();cu.print();sturm.back().print();cerr << "\n\n";
        pr = cu;cu  = sturm.back();
    }

    qreal l = 0;
    qreal r = 10000;

    solution = localize(sturm,l,r);

    return solution;
}
