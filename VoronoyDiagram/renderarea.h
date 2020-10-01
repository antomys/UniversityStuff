#ifndef RENDERAREA_H
#define RENDERAREA_H

#include<QSize>
#include<QPen>
#include<QBrush>
#include<QWidget>
#include<QPainter>
#include<QPaintDevice>
#include<QLine>

#include "point.h"
#include "line.h"


class RenderArea : public QWidget
{
    Q_OBJECT

public:

    RenderArea(QWidget *parent = 0);

    QList<QPointF> points;
    QList<QLineF> VoronoiEdges,DelaunayEdges;

    void setPoints(QList<QPointF>);
    void setVoronoiEdges(QList<QLineF>);
    void setDelaunayEdges(QList<QLineF>);

    QColor pointColor,VoronoiColor,DelaunayColor;

protected:
    void paintEvent(QPaintEvent *event) override;

private:


};


#endif // RENDERAREA_H
