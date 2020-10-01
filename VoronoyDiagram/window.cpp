#include "window.h"
#include "ui_window.h"

#include <algorithm>
#include<set>
#include <math.h>
#include <QTimer>

using std::set;
using std::sort;

#include <QDebug>

#include "equation.h"

#include "geometry.cpp"

using namespace std;


Window::Window(QWidget *parent):
        QMainWindow(parent),
        ui(new Ui::Window)
{

     ui->setupUi(this);
    ui->timeIn->setMaximum(10000);
    ui->speed->setMinimum(0);
    ui->speed->setValue(0.05);
    ui->speed->setMaximum(100);
    //ui->showDelaunay->setCheckState(Qt::Checked);
    //ui->showVoronoi->setCheckState(Qt::Checked);

    int w = 900;
    int h = 600;
    int dh = 300;

    int move = 100000;
    resize(w,h+dh);
    renderArea = new RenderArea(this);

    border.append(QLine(0 - move,0 -  move,0 -move,h+ move));
    border.append(QLine(0 - move,h+move,w+move,h+move));
    border.append(QLine(w+move,h+move,w+move,0-move));
    border.append(QLine(w+move,0 -move,0-move,0-move));

    renderArea->show();
    renderArea->update();

}


QLineF getNormal(QLineF l){
    QPointF v = l.p2() - l.p1();
    v = QPointF(v.y(),-v.x());
    return QLineF(l.p1(),l.p1()+v);
}


QPointF basePos = QPointF(0,0);

QList<QLineF> Window::DelaunayEdgesList(QList<PointState> list, qreal t){
     QList<QLineF> ans;
     for(auto point:list){

         if(point.neighbours.numb.size() == 0)
             continue;

         int p1n  = point.neighbours.getFirst();
         int p2n;
         int f = p1n;

         int infpos = -1;

         do
         {
             Point p1 = list[p1n].point;

             if(point.point.toQPoint(0) == inf){
                 break;
             }
             p2n = point.neighbours.getNext(p1n);

             if( isInf(p1)){
                 p1n = p2n;
                 continue;
             }
             ans.append(QLineF(point.point.toQPoint(t),p1.toQPoint(t)));
             p1n = p2n;
         }while(p1n !=f);


     }
     return ans;
}



QList<QLineF> Window::VoronoiEdgesList(QList<PointState> list, qreal t){

    QList<QLineF> ans;


    //qDebug() << "To line list\n";

    QList<QPointF> inters;
    QList<int> mark;
    for(auto point:list){

        if(point.neighbours.numb.size() == 0)
            continue;

        //if(point.point.nom != 5)continue;

        int p1n  = point.neighbours.getFirst();
        int f = p1n;


        int infpos = -1;

        do
        {
            int p2n = point.neighbours.getNext(p1n);
            Point p1 = list[p1n].point;
            Point p2 = list[p2n].point;
            mark.append(0);
            if(point.point.toQPoint(0) == inf){
                break;
            }


            if( isInf(p1)){
                QPointF m = (p2.toQPoint(t)+ point.point.toQPoint(t))/2.;
                //QLineF perp = QLineF(m,p2.toQPoint(t)).normalVector();

                QLineF perp = getNormal(QLineF(m,p2.toQPoint(t)));

                inters.append(m + 1000*( m - perp.p2()));

                infpos = inters.size();

                p1n = p2n;
                continue;
            }

            if(isInf(p2)){
                QPointF m = (p1.toQPoint(t)+ point.point.toQPoint(t))/2.;
               // QLineF perp = QLineF(m,p1.toQPoint(t)).normalVector();
                QLineF perp = getNormal(QLineF(m,p1.toQPoint(t)));
                inters.append(m - 1000*( m - perp.p2()));
                p1n = p2n;
                continue;
            }

            QLineF l1 = getPerp(point.point.toQPoint(t),p1.toQPoint(t));
            QLineF l2 = getPerp(point.point.toQPoint(t),p2.toQPoint(t));

            QPointF* in = new QPointF();
            if( l1.intersect(l2,in) != QLineF::NoIntersection){
#define sqr(a) ((a)*(a))
                qreal q = sqr(in->x()) + sqr(in->y());
                //qDebug() << q;
                /*
                if( q>= 1e10){
                    QPointF m = (p1.toQPoint(t)+ point.point.toQPoint(t))/2.;
                    QLineF perp = getNormal(QLineF(m,p1.toQPoint(t)));
                    inters.append(m + 1000*( m - perp.p2()));
                }
                else*/
                    inters.append(*in);
            }
            delete in;
            p1n = p2n;
        }while(p1n !=f);

        if(inters.size() <= 0)
        {
            qDebug() << "fail" << point.point.toQPoint(0)<<'\n';
            continue;
        }

        inters.append(inters[0]);
        for(int i=0;i<inters.size()-1;++i){
            if(i != infpos)
                ans.append(QLineF(inters[i],inters[i+1]));
        }
        inters.clear();
    }
    return ans;
}

void Window::generate(){


    qreal time = clock();
    int eventcount = 0;
    while(updateState())
    {

        if(changeTime.back()>= 231.8){

            auto x= 10;
            auto y =2*x;
        }

        ++eventcount;
        qDebug() << eventcount;
    }

    qDebug() << "Events - "<<eventcount;
    qreal q = (clock() - time);
    qDebug() << "Time - "<<q/CLOCKS_PER_SEC;
    qDebug() << "Clocks - "<< q;
    qDebug() << "Clocks/event="<<q/eventcount
             << "\nClocks/(event*n)"
             <<q/(eventcount*(points.size()-1))<<"";
}

void Window::display(qreal t){

    int pos = 0;
    while(pos<changeTime.size() && changeTime[pos]<t)++pos;
    if(pos>0)
        --pos;

    ui->timeIn->setValue(t);
    renderArea->setDelaunayEdges(DelaunayEdgesList(region[pos],t));
    renderArea->setVoronoiEdges(VoronoiEdgesList(region[pos],t));
    renderArea->setPoints(timePoints(t));
    renderArea->update();

    update();
}

QList<QPointF> Window::timePoints(qreal t){
    QList<QPointF> p;
    for(auto x:points)
        p.append(x.toQPoint(t));

    return p;
}

bool Window::updateState(){

    if(region.size() == 0)
        return 0;

    //checkState();

    QList<PointState> state = region[region.size()-1];

   // qDebug() << "Updating state";

    qreal time = changeTime[changeTime.size()-1];
    int na,nb,nc,nd;
    qreal minTime = 10000000;
    int an = -1;

    for(PointState a:state){
        an++;
        if(a.neighbours.numb.size() <= 1)
            continue;

        for(int bn:a.neighbours.numb){

            Point b = points[bn];
            Point c = points[state[an].neighbours.getPrev(bn)];
            Point d = points[state[an].neighbours.getNext(bn)];

            qreal newTime = getTime(a.point,b,c,d,time);
            if(newTime == -1)
                continue;

            if(newTime< minTime){
                minTime = newTime;
                na = an;nb = bn;

                //qDebug() << an <<  " "<<bn << " "<<state[an].neighbours.getNext(bn) << " "<<state[an].neighbours.getPrev(bn)<<"\n";

                nd = state[an].neighbours.getNext(bn);
                nc = state[an].neighbours.getPrev(bn);
            }
        }
    }

    if(minTime  == 10000000)
        return 0;

   // qDebug() << minTime ;
   // qDebug() <<"switching "<< na <<  " "<<nb << " "<<nc << " "<<nd<<"\n";

    if(!isInf(state[nc].point))
        state[nc].neighbours.addBefore(nd,na);
    if(!isInf(state[nd].point))
        state[nd].neighbours.addAfter(nc,na);


    state[na].neighbours.remove(nb);
    if(!isInf(state[nb].point))
        state[nb].neighbours.remove(na);

    region.append(state);
    changeTime.append(minTime);

    return 1;
}

void Window::InitState(QList<Point> p){

    //build static voronoi diagram here


    int n = p.size();

    points = p;
    QList<QPointF> intersections;
    QList<QLineF> lines;
    QList<int> np1,np2;

    int xp = -1;
    for(auto x:points){
        xp++;
        if(x.toQPoint(0) == inf)
            continue;

        int yp = -1;
        for(auto y:points){
            yp++;
            if(y.toQPoint(0) == inf)
                continue;

            if(x.toQPoint(0) !=y.toQPoint(0)){

                lines.append(getPerp(x.toQPoint(0),y.toQPoint(0)));
                np1.append(xp);
                np2.append(yp);
            }
        }
    }

    QList<QList<int> > ne;
    for(int i=points.size()-1;i>=0;--i)
        ne.append(QList<int>());

    for(int i=0;i<points.size();++i){
        QList<int> lp,p1,p2;
        QList<QLineF> pointLines;
        QList<QPointF> npoints;

        QPointF cur = points[i].toQPoint(0);
        for(int j=0;j<points.size();++j){
            if(i==j)
                continue;
            QPointF nex = points[j].toQPoint(0);
            lp.append(j);
            pointLines.append(getPerp(cur,nex));
        }

        for(int g=0;g<pointLines.size();++g){
            auto x = pointLines[g];
            for(int j=0;j<pointLines.size();++j){
                auto y = pointLines[j];
                QPointF *in = new QPointF();
                if( x.intersect(y,in) != QLineF::NoIntersection){
                    p1.append(lp[g]);
                    p2.append(lp[j]);
                    npoints.append(*in);
                }
            }
        }

        set<int> nei;
        for(int g=0;g<npoints.size();++g){
            auto point = npoints[g];
            bool ok = true;


            int qq = 0;
            for(auto line:pointLines){
                  if( !isSameSide(points[i].toQPoint(0),point,line)){
                    ok = false;
                    break;
                }
                ++qq;
            }
            if(ok){
//                qDebug() << p1[g] <<","<<p2[g]<<'\n';
                nei.insert(p1[g]);
                nei.insert(p2[g]);
            }
        }
        for(auto x:nei)
            ne[i].append(x);
    }

    p.append(inf);
    points.append(inf);
    QList<QPointF> np;
    ne.append(QList<int>());
    for(int i=0;i<points.size();++i){
        np.append(points[i].toQPoint(0));
    }

    QList<PointState> st;


    for(int i=0;i<points.size();++i){
        basePos = points[i].toQPoint(0);
        st.append(PointState(points[i]));

        if(ne[i].size() == 0)
            continue;

        sort(ne[i].begin(),ne[i].end(),
             [np](const int & a, const int & b) -> bool
             {
            qreal xa = np[a].x() - basePos.x();
            qreal ya = np[a].y() - basePos.y();
            qreal xb = np[b].x() - basePos.x();
            qreal yb = np[b].y() - basePos.y();

            return atan2(xa,ya)<atan2(xb,yb);
             });

        st[i].neighbours.addAfter(ne[i][0]);
        for(int j=1;j<ne[i].size();++j)
            st[i].neighbours.addAfter(ne[i][j],ne[i][j-1]);


        int cur = ne[i][0];
        int nex;
        do{
            nex = st[i].neighbours.getNext(cur);
           QPointF ip = points[i].toQPoint(0);
           QPointF cp = points[cur].toQPoint(0);
           QPointF np = points[nex].toQPoint(0);


           QLineF p1 = getPerp(cp,ip);
           QLineF p2 = getPerp(np,ip);
           QPointF *in = new QPointF();

           if (p1.intersect(p2,in) == QLineF::NoIntersection){
               cur = nex;
               continue;}

           QPointF v1 = cp - ip,v2 = ip - np;
           qreal dot = v1.x()*v2.x() + v1.y()*v2.y();
           qreal det = v1.x()*v2.y() - v1.y()*v2.x();
           qreal angle = atan2(det, dot);

           if(angle < 0)
           {
               st[i].neighbours.addAfter(n,cur);
           }
            cur = nex;
        }while(cur != ne[i][0]);


    }

    changeTime.append(0);
    region.append(st);


    renderArea->setPoints(np);
   // renderArea->setEdges(lines);
    renderArea->setVoronoiEdges(VoronoiEdgesList(st,0));
    renderArea->setDelaunayEdges(DelaunayEdgesList(st,0));
   // renderArea->setEdges();



    int aa = 16,bb = 18, cc = 19;

    auto r = getCrossLineTime(points[aa],points[bb],points[cc],0);
    /*
    qDebug() << "\n";
    for(auto x:r)
        qDebug() << x<<""<< updateLineTime(points[aa],points[bb],points[cc],x);
    */
    //qDebug() << "zzz"<<""<<r.size();
    //checkState();
}

Window::~Window(){
    delete ui;
}

int stage = 0;
void Window::on_update_clicked()
{

    generate();

}

void Window::checkState(){
    if(region.size() == 0)
        return;
    auto x = region[region.size()-1];


    qDebug() << "Time = "<<changeTime[changeTime.size()-1];
    for(auto y:x){
        qDebug() << y.point.nom;
        y.neighbours.printList();
        cout<<'\n';
    }
}

void Window::on_update_2_clicked()
{
    if(region.size()<=1)
        return;
    region.pop_back();
    changeTime.pop_back();
    QList<QPointF> p;
    qreal time = changeTime.back();
    for(auto x:region.back())
        p.append(x.point.toQPoint(time));

    renderArea->setPoints(p);
    renderArea->setVoronoiEdges(VoronoiEdgesList(region.back(),time));
    renderArea->setDelaunayEdges(DelaunayEdgesList(region.back(),time));

    ui->timeIn->setValue(time);
    update();
    renderArea->update();
}


void Window::updater(qreal t,qreal maxt){
    if(t>maxt)
        return;
    qreal step  = ui->speed->value();

    if(step == 0)
        return;

    QTimer::singleShot(10, this, [this,t,maxt,step](){updater(t+step/10,maxt);});

    display(t);
}

void Window::on_show_clicked()
{
    qreal maxtime = 100000;
    qreal stTime = ui->timeIn->value();
    qreal speed = ui->speed->value();

    if(speed == 0)
        return;

    QTimer::singleShot(200, this, [this,stTime,maxtime](){updater(stTime,maxtime);});
}

void Window::on_showtime_clicked()
{
    qreal time = ui->timeIn->value();
    display(time);
}

void Window::on_stop_clicked(){
    ui->speed->setValue(0);
}

void Window::getInfo(qreal t){
    int pos = 0;
    while(pos<changeTime.size() && changeTime[pos]<t)++pos;

    if(pos >0)--pos;


    auto x = region[pos];

    qDebug() << "Time = "<<changeTime[pos];
    for(auto y:x){
        qDebug() << y.point.nom;
        y.neighbours.printList();
        cout<<'\n';
    }
    int i = 0;
    for(auto x:points)
        qDebug() <<"Point "<< i++ << x.toQPoint(t) << "";

}

void Window::on_info_clicked()
{
    getInfo(ui->timeIn->value());
}

void Window::on_showDelaunay_stateChanged(int state)
{
    if(state == Qt::Checked)
        renderArea->DelaunayColor = Qt::blue;
    else
        renderArea->DelaunayColor = Qt::white;

}

void Window::on_showVoronoi_stateChanged(int state)
{
    if(state == Qt::Checked)
        renderArea->VoronoiColor = Qt::red;
    else
        renderArea->VoronoiColor = Qt::white;
}
