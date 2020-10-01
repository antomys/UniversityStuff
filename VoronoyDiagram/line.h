#ifndef LINE_H
#define LINE_H


#include "point.h"

#include <QLine>

class Line
{
public:


    Point x;
    Point y;

    Line(Point x,Point y);
    QLineF toQLine(qreal t);
};

#endif // LINE_H
