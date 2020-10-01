#ifndef POINT_H
#define POINT_H

#include<iostream>

#include<QPoint>



class Point
{


public:

    int nom;
    QPointF speed;
    QPointF start_position;
    Point(QPointF _stpos = QPointF(0,0),QPointF speed_ = QPointF(0,0));

    QPointF toQPoint(qreal t);

    void display();
    qreal norm(qreal t);

};



#endif // POINT_H
