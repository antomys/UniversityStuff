#ifndef WINDOW_H
#define WINDOW_H

#include <QMainWindow>

#include <QLabel>
#include <QComboBox>
#include <QSpinBox>
#include <QCheckBox>

#include "renderarea.h"
#include "point.h"
#include "pointstate.h"


namespace Ui {
class Window;
}


class Window : public QMainWindow
{
    Q_OBJECT

    QList<QPointF> timePoints(qreal t);

public:


    Ui::Window *ui;
    Window(QWidget *parent = 0);

    ~Window();

    QList<float> changeTime;
    QList<QList<PointState> > region;

    RenderArea *renderArea;

    QList<Point> points;

    void InitState(QList<Point>);

    static QPointF inf;

    bool updateState();

private slots:
    void on_update_clicked();

    void on_update_2_clicked();

    //void on_pushButton_clicked();

    void on_show_clicked();

    void updater(qreal t,qreal maxt);

    void on_showtime_clicked();

    void on_stop_clicked();

    void on_info_clicked();

    void on_showDelaunay_stateChanged(int arg1);

    void on_showVoronoi_stateChanged(int arg1);

private:

    bool comp_by_angle(const int&,const int&);
    QList<QLineF> VoronoiEdgesList(QList<PointState> list, qreal t);
    QList<QLineF> DelaunayEdgesList(QList<PointState> list, qreal t);
    QList<QLineF> border;

    void checkState();

    void generate();

    void display(qreal t);

    void buildVoronoi(QList<Point> p,qreal t=0);

    void getInfo(qreal t);
};


#endif // WINDOW_H
