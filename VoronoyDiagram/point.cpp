#include "point.h"
#define P Point


P::Point(QPointF _stpos ,QPointF _speed){
    start_position = _stpos;
    speed = _speed;
}

QPointF P::toQPoint(qreal t){
    return QPoint(
                start_position.x()+ speed.x()*t,
                start_position.y() + speed.y()*t);
}

#define sqr(a) ((a)*(a))
qreal P::norm(qreal t){
    return sqr(start_position.x()+ speed.x()*t)+
            sqr(start_position.y() + speed.y()*t);
}
