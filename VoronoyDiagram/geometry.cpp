#include <iostream>
using std::cerr;
using std::cout;

#include "window.h"
#include "equation.h"



bool isInf(Point A){
    return A.toQPoint(0) == Window::inf;
}

QLineF getPerp(QPointF x,QPointF y){
    QPointF z = (x+y)/2.;
    QPointF v = x - y;
    v  = QPointF(v.y(),-v.x());
    v = z+v;

    return QLineF(z,v);
}


qreal dist(QPointF p1,QPointF p2){
#define sqr(a) ((a)*(a))


    auto gg = sqrt( sqr(p1.x()-p2.x()) + sqr(p1.y()- p2.y()));
    return gg;
}

qreal dist(QPointF point,QLineF line){
    QLineF nor = line.normalVector();
    QPointF normv = nor.p2() - nor.p1();

    QLineF l2 = QLineF(point,point + normv);

    QPointF*res= new QPointF();

    line.intersect(l2,res);

    return dist(point,*res);
}


bool isSameSide(QPointF x,QPointF m,QLineF z){
    qreal v = ((z.y1() - z.y2())*(x.x() - z.x1())  +
               (z.x2() - z.x1())*(x.y() - z.y1()))*
              ((z.y1() - z.y2())*(m.x() - z.x1())  +
               (z.x2() - z.x1())*(m.y() - z.y1()));

    return v>=-1e-4;
}

qreal getAngle(QPointF a,QPointF b,QPointF c){

    if (b == Window::inf)
  //      return 1;
        return 0.01;

    qreal v = QLineF(b,a).angleTo(QLineF(b,c));



    if( v > 180)
        v = 360 - v;

    return v;
}

template<typename T>
T det(T a1,T a2,T a3,
      T b1,T b2,T b3,
      T c1,T c2,T c3){
    return a1*b2*c3-a1*b3*c2+b1*c2*a3-b1*c3*a2+c1*a2*b3-c1*a3*b2;
}


qreal isInsideCircle(Point A,Point B,Point C,Point D,qreal t){
    qreal mul = 200;

    qreal x1(A.toQPoint(t).x()/mul);
    qreal y1(A.toQPoint(t).y()/mul);

    qreal x2(B.toQPoint(t).x()/mul);
    qreal y2(B.toQPoint(t).y()/mul);

    qreal x3(C.toQPoint(t).x()/mul);
    qreal y3(C.toQPoint(t).y()/mul);

    qreal x4(D.toQPoint(t).x()/mul);
    qreal y4(D.toQPoint(t).y()/mul);


    qreal a = det(x1,y1,1.,
                  x2,y2,1.,
                  x3,y3,1.
                  );

    qreal b = -det(x1*x1+y1*y1,y1,1.,
                      x2*x2+y2*y2,y2,1.,
                      x3*x3+y3*y3,y3,1.
                  );

    qreal c = det(x1*x1+y1*y1,x1,1.,
                      x2*x2+y2*y2,x2,1.,
                      x3*x3+y3*y3,x3,1.
                  );

    qreal d = -det(x1*x1+y1*y1,x1,y1,
                      x2*x2+y2*y2,x2,y2,
                      x3*x3+y3*y3,x3,y3
                  );




    qreal req = a*x4*x4+b*x4+a*y4*y4+c*y4+d;

    return req;
}


qreal updateCircleTime(Point A,Point B,Point C,Point D,qreal t){
    qreal delta = 1.;
    qreal l = t - delta,
          r = t + delta,
          m,q;

    while(r-l>1e-6){
        m = (l+r)/2;
        q =  isInsideCircle(A,B,C,D,m);

       // qDebug() << q << " " <<isInsideCircle(A,B,C,D,l)<<" " <<isInsideCircle(A,B,C,D,r)<<"\n";

        if(q*isInsideCircle(A,B,C,D,l)<=0)
            r = m;
        else
            l = m;
    }

    //qDebug()<<"Old - "<<t<< ",New -  "<<l<<"\n";

    return l;
}

qreal isCrossLine(Point A,Point B,Point C,qreal t){
    qreal mul = 1;

    qreal abx = ((B.toQPoint(t).x() - A.toQPoint(t).x())/mul);
    qreal aby = ((B.toQPoint(t).y() - A.toQPoint(t).y())/mul);


    qreal acx = ((C.toQPoint(t).x() - A.toQPoint(t).x())/mul);
    qreal acy = ((C.toQPoint(t).y() - A.toQPoint(t).y())/mul);

    qreal res = abx*acy -aby*acx;


    return res;
}

qreal updateLineTime(Point A,Point B,Point C,qreal t){

    if(t >=19 && t<=21){



        int x = 1;
        x = x +1;
        x = x  - x+ x;
        x = 2;

    }



    qreal delta = 1.;
    qreal l = t - delta,
          r = t + delta,
          m,q;





    while(r-l>1e-6){
        m = (l+r)/2;
        q =  isCrossLine(A,B,C,m);

        if(q*isCrossLine(A,B,C,l)<=0)
            r = m;
        else
            l = m;


        //qDebug() <<isCrossLine(A,B,C,l) <<isCrossLine(A,B,C,m) << isCrossLine(A,B,C,r);


    }
 //   if( fabs(t-l)>1e-3)
 //       qDebug()<<"Old - "<<t<< ",New -  "<<l<<"\n";

    return l;
}


QList<qreal> getCircleTime(Point A,Point B,Point C,Point D,qreal stTime){


    qreal mul = 200;

    Equation x1({A.toQPoint(0).x()/mul,A.speed.x()});
    Equation y1({A.toQPoint(0).y()/mul,A.speed.y()});

    Equation x2({B.toQPoint(0).x()/mul,B.speed.x()});
    Equation y2({B.toQPoint(0).y()/mul,B.speed.y()});

    Equation x3({C.toQPoint(0).x()/mul,C.speed.x()});
    Equation y3({C.toQPoint(0).y()/mul,C.speed.y()});

    Equation x4({D.toQPoint(0).x()/mul,D.speed.x()});
    Equation y4({D.toQPoint(0).y()/mul,D.speed.y()});

    Equation a = det(x1,y1,Equation({1,0}),
                     x2,y2,Equation({1,0}),
                     x3,y3,Equation({1,0})
                  );

    Equation b = -det(x1*x1+y1*y1,y1,Equation({1,0}),
                      x2*x2+y2*y2,y2,Equation({1,0}),
                      x3*x3+y3*y3,y3,Equation({1,0})
                  );

    Equation c = det(x1*x1+y1*y1,x1,Equation({1,0}),
                      x2*x2+y2*y2,x2,Equation({1,0}),
                      x3*x3+y3*y3,x3,Equation({1,0})
                  );

    Equation d = -det(x1*x1+y1*y1,x1,y1,
                      x2*x2+y2*y2,x2,y2,
                      x3*x3+y3*y3,x3,y3
                  );

    //a.print();b.print();c.print();d.print();
    //Equation req = a*a*x4*x4+a*b*x4+a*a*y4*y4+a*c*y4+a*d;
    Equation req = a*x4*x4+b*x4+a*y4*y4+c*y4+d;
    auto res = req.solve();

    for(int i=0;i<res.size();++i)
        res[i]*=mul;

 //   int pos = 0;
  //  while(pos<res.size() && res[pos]<stTime)++pos;


    //cerr << "A,B,C,D "<<A.nom<< " "<<B.nom<< " "<<C.nom<< " " <<D.nom<<" " <<((pos>=res.size())?(-1):res[pos])<<"\n"    ;
    //a.print();

  //  if(pos>=res.size())
  //          return -1;

    return res;

  //  return res[pos];
}



qreal vect(QPointF A,QPointF B){
    return A.x()*B.x() + A.y()*B.y();
}


QList<qreal> getCrossLineTime(Point A,Point B,Point C,qreal stMom = 0){


    //when C cross AB

    qreal mul = 200;

    Equation abx = Equation({(B.toQPoint(0).x() - A.toQPoint(0).x())/mul,(B.speed.x() - A.speed.x())});
    Equation aby = Equation({(B.toQPoint(0).y() - A.toQPoint(0).y())/mul,(B.speed.y() - A.speed.y())});


    Equation acx = Equation({(C.toQPoint(0).x() - A.toQPoint(0).x())/mul,(C.speed.x() - A.speed.x())});
    Equation acy = Equation({(C.toQPoint(0).y() - A.toQPoint(0).y())/mul,(C.speed.y() - A.speed.y())});

    Equation toSolve = abx*acy -aby*acx;
    //toSolve.print();
    auto solution  = toSolve.solve();
    for(int i =0;i<solution.size();++i)solution[i]*=mul;

    return solution;
}



qreal getTime(Point A,Point B,Point C,Point D,qreal ti){


    //ti = std::min(ti+ 0.05,ti*1.0001);
    qreal nonzero =  0.001;
    while( (ti+nonzero) - ti <=1e-7)
        nonzero*=2;

    ti+=nonzero*4;

    if (!isInf(A) && !isInf(B) && !isInf(C)&& !isInf(D)){
         QList<qreal> t = getCircleTime(A,B,C,D,ti);
         int pos = 0;
         while(pos<t.size()){
             //qDebug() << t[pos] <<" " << updateCircleTime(A,B,C,D,t[pos]) << "";
            // t[pos] = updateCircleTime(A,B,C,D,t[pos]);
             if(t[pos]>=ti)
                 return t[pos];


            /* if(ti - t[pos]<= 0.5){
                 qDebug() << "Decline "<<ti<< " "<<t[pos]<<" "<< A.nom<<B.nom<<C.nom<<D.nom<<"";
             }*/

             ++pos;
         }
         return -1;
    }



    if(isInf(B)){
        QList<qreal> t = getCrossLineTime(A,C,D,ti);
        int pos = 0;
        while(pos<t.size()){
           // t[pos] = updateLineTime(A,C,D,t[pos]);
            if(t[pos]>=ti)
                return t[pos];
            ++pos;
        }
        return -1;
    }


    if(isInf(C)){

        QList<qreal> t = getCrossLineTime(A,B,D,ti);
        int pos = 0;
        while(pos<t.size()){
          //  t[pos] = updateLineTime(A,B,D,t[pos]);
            if(t[pos]>=ti)
                return t[pos];
            ++pos;
        }
        return -1;

    }

    if(isInf(D)){
        QList<qreal> t = getCrossLineTime(A,B,C,ti);
        int pos = 0;
        while(pos<t.size()){
          //  t[pos] = updateLineTime(A,B,C,t[pos]);
            if(t[pos]>=ti)
                return t[pos];
            ++pos;
        }
        return -1;
    }
    //    return -1;
    //    return getCrossLineTime(A,B,C,ti);}


     cerr << ("Undefined:A is inf\n");


}

