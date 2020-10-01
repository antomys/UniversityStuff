#ifndef POINTSTATE_H
#define POINTSTATE_H

#include "point.h"
#include "neighbourstorage.h"

class PointState
{
public:
    PointState(Point p);

    Point point;
    NeighbourStorage neighbours;

protected:

};

#endif // POINTSTATE_H
