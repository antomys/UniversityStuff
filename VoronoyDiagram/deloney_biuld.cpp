#include "window.h"

#include "window.cpp"



void Window::buildVoronoi(QList<Point> p, qreal t){

    points = p;

    QList<QPointF> po = QList<QPointF>();
    QList<PointState> ne;

    for(auto x:p)
        po.append(x.toQPoint(t));

    for(auto x:p)
        ne.append(PointState(x));

   // ne.append(Point);

}
