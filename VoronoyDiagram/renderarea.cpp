#include "renderarea.h"

#include<QPalette>

#include <QDebug>

RenderArea::RenderArea(QWidget *parent)
    : QWidget(parent)
{
    QPalette pal = palette();
    pal.setColor(QPalette::Background, Qt::white);
    setAutoFillBackground(true);
    setPalette(pal);

    resize(QSize(900,600));

    pointColor = Qt::black;
    VoronoiColor = Qt::red;
    DelaunayColor = Qt::blue;

}

qreal xmin= 10 ,xmax = 10,ymin = 10, ymax= 10;

qreal xmaxr = 600,
      ymaxr = 600;

qreal scalex ;
qreal scaley ;
qreal xshift ;
qreal yshift ;

QPointF transorm(QPointF p){
   // return p;
    /*
    xshift = 400;
    scalex = 2;
    yshift = 400;
    scaley = 2;*/

    return QPointF( (p.x() + xshift)/scalex , (p.y() + yshift)/scaley);
}

#include <math.h>
using std::min;
using std::max;

void RenderArea::paintEvent(QPaintEvent * /* event */)
{

    xmin= 10 ,xmax = 10,ymin = 10, ymax= 10;
    xmaxr = 900,
    ymaxr = 600;

    for(int i=0;i<points.size()-1;++i){
        auto x = points[i];
        xmin = min(xmin,x.x());
        xmax = max(xmax,x.x());
        ymin = min(ymin,x.y());
        ymax = max(ymax,x.y());
    }

    xmin-=50;ymin-=50;
    xmax+=50;ymax+=50;

    scalex = (xmax - xmin)/xmaxr;
    scaley = (ymax - ymin)/ymaxr;

    scalex = max(scalex,scaley);
    scaley = max(scalex,scaley);

    xshift = -xmin;
    yshift = -ymin;

    QPainter painter(this);
    QPen pen = QPen();
    pen.setColor(VoronoiColor);
    QBrush br = QBrush();
    br.setColor(pointColor);

    QPainterPath path;

    for(auto x:VoronoiEdges){
        path.moveTo(transorm(x.p1()));
        path.lineTo(transorm(x.p2()));
    }
    painter.setPen(pen);
    painter.setBrush(br);
    painter.drawPath(path);



    QPen pen1 = QPen();
    pen1.setColor(DelaunayColor);
    QBrush br1 = QBrush();
    br1.setColor(pointColor);

    QPainterPath path1;

    for(auto x:DelaunayEdges){
        path1.moveTo(transorm(x.p1()));
        path1.lineTo(transorm(x.p2()));
    }
    painter.setPen(pen1);
    painter.setBrush(br1);
    painter.drawPath(path1);


    QPen linepen(Qt::black);
    linepen.setCapStyle(Qt::RoundCap);
    linepen.setWidth(4);
    painter.setRenderHint(QPainter::Antialiasing,true);
    painter.setPen(linepen);


    int c = 0;
    for(auto x:points){
        painter.drawPoint(transorm(x));
        painter.drawText(transorm(x) + QPointF(5,5),QString::number(c++));
    }


    linepen = QPen(Qt::blue);
    linepen.setCapStyle(Qt::RoundCap);
    linepen.setWidth(4);
    painter.setRenderHint(QPainter::Antialiasing,true);
    painter.setPen(linepen);

    painter.drawPoint(transorm(QPointF(0,0)));
    painter.drawText(transorm(QPointF(0,0)) + QPointF(5,5),"(0,0)");

    painter.drawPoint(transorm(QPointF(50,0)));
    painter.drawText(transorm(QPointF(50,0)) + QPointF(5,5),"(50,0)");

    painter.drawPoint(transorm(QPointF(0,50)));
    painter.drawText(transorm(QPointF(0,50)) + QPointF(5,5),"(0,50)");
}

void RenderArea::setPoints(QList<QPointF> new_list){
    points = new_list;
}

void RenderArea::setVoronoiEdges(QList<QLineF> new_list){
    VoronoiEdges = new_list;
}

void RenderArea::setDelaunayEdges(QList<QLineF> new_list){
    DelaunayEdges = new_list;
}

