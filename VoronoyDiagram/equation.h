#ifndef EQUATION_H
#define EQUATION_H

#include <QList>

class Equation
{
public:
    Equation(int d = 0);

    QList<qreal> coef;
    int degree;


    Equation operator +(const Equation &b);
    Equation operator *(const Equation &b);
    Equation operator -(const Equation &b);
    Equation dif();

    Equation operator -();
    Equation operator /(const Equation &b);
    Equation operator %(const Equation &b);

     qreal coefAt (int pos)const;

    QList<qreal> solve();

    void normalize();

    qreal valAt(qreal t)const;
    void print();

    bool solved = true;
    QList<qreal> solution;

    QList<qreal> localize(QList<Equation>&,qreal,qreal);

    qreal bs(qreal l,qreal r);

    Equation(QList<qreal> l);
};

#endif // EQUATION_H
