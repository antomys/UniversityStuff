#include "line.h"

Line::Line(Point x,Point y):x(x),y(y){}

QLineF Line::toQLine(qreal t){
    return QLineF(x.toQPoint(t),y.toQPoint(t));
}
