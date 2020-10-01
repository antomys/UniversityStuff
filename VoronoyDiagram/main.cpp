#include "window.h"
#include <QApplication>

QPointF Window::inf = QPointF(-1234,4321);

#include <equation.h>
#include <QDebug>

int main(int argc, char *argv[])
{

    QApplication a(argc, argv);
    Window w;

    int n = 32;
    int maxx = 900;
    int maxy = 600;

    QList<Point> l;


    //srand(0);

    srand(0);

    for(int i=0;i<n;++i){
        int x = rand()%(maxx-40)+20;
        int y = rand()%(maxy-40)+20;


        qreal an = rand()%360;
        l.append(Point(QPointF(x,y),QPointF(sin(an),cos(an))));

/*
        int u = rand()%4;
        l.append(Point(QPointF(x,y),2*QPointF((u&1)-1,2*((u&2)/2)-1)));
*/
        l[i].nom = i;

    }


    w.InitState(l);
    w.show();

    return a.exec();




}
