#include "point.h"
#define P Point


P::Point(pos _stpos ,pos _speed){
    start_position = _stpos;
    speed = _speed;
}

pos P::toQPoint(ptype t){
    return QPoint(
                start_position.x()+ speed.x()*t,
                start_position.y() + speed.y()*t);
}

