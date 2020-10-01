#include "neighbourstorage.h"

#include <QDebug>

#define NS NeighbourStorage

#include<iostream>
using std::cerr;

#define pb push_back

NS::NeighbourStorage()
{
    count = 0;
}

int NS::getNext(int n){
    return numb[ next [valuePosition[n] ]];
}

int NS::getPrev(int n){
    return numb[ prev [valuePosition[n] ]];
}
void NS::addAfter(int v,int pr){
    if(pr == -1){
        //incorrect insertion attempt
        if(count>0){
            cerr<< "Adding error,try to add with pr = -1 to non-emrty list\n";
            return ;
        }

        //new list first element
        next.pb(0);
        prev.pb(0);
        numb.pb(v);
        valuePosition[v] = 0;
        count++;
        return;
    }

    int cur_prev = valuePosition[pr];
    int cur_next =  next[cur_prev];

    numb.pb(v);
    next.pb(cur_next);
    prev.pb(cur_prev);
    next[cur_prev] = count;
    prev[cur_next] = count;
    valuePosition[v] = count++;


    //cerr << "Adding "<< numb[cur_prev]<<' '<<v<< ' '<<numb[cur_next]<<'\n';
}

void NS::addBefore(int v,int pr){
    if(pr == -1){
        //incorrect insertion attempt
        if(count>0){
            cerr<< "Adding error,try to add with pr = -1 to non-emrty list\n";
            return ;
        }

        //new list first element
        next.pb(0);
        prev.pb(0);
        numb.pb(v);
        valuePosition[v] = 0;
        count++;
        return;
    }

    int cur_next = valuePosition[pr];
    int cur_prev =  prev[cur_next];

    numb.pb(v);
    next.pb(cur_next);
    prev.pb(cur_prev);
    next[cur_prev] = count;
    prev[cur_next] = count;
    valuePosition[v] = count++;

}
void NS::remove(int v){
    int pos = valuePosition[v];
    int prev_pos = prev[pos];
    int next_pos = next[pos];

    if(count == 1){
        //??? bad case, is it possible? TODO

        prev.clear();
        next.clear();
        numb.clear();

        count = 0;
        return;
    }
    --count;
    prev[next_pos] = prev[pos];
    next[prev_pos] = next[pos];
    valuePosition.erase(v);


    relaxate(pos);

    // cant remove values from arrays,
    // cause VP map to be incorect
    // can probably cause ML? TODO
}

int NS::getFirst(){
    return numb[0];
}

void NS::printList(){
    if(count == 0){
        qDebug() << "Empty list\n";
        return;
    }
    int pos = 0;

    if(numb.size() == 0){
        qDebug() << "Empty list\n";
        return;
    }


   // qDebug() << "Next check ";

    QList<int> p1,p2;

    p1.append(numb[0]);
    do{
        pos = next[pos];
        p1.append(numb[pos]);

    }while(pos!=0);

    qDebug() << p1;

  //  qDebug() << "Prev check ";
    p2.append(numb[0]);
    pos = 0;
    do{
        pos = prev[pos];
        p2.append(numb[pos]);

    }while(pos!=0);
//    qDebug() << p2;
}


void NS::relaxate(int pos){
    if(pos == numb.size()-1){
        numb.pop_back();
        next.pop_back();
        prev.pop_back();
        return ;
    }

    int q = numb.size()-1;

    int pr = prev[q];
    int nx = next[q];

    numb[pos] = numb[q];
    valuePosition[numb[q]] =  pos;

    next[pr] = pos;
    prev[nx] = pos;

    next[pos] = nx;
    prev[pos] = pr;

    numb.pop_back();
    next.pop_back();
    prev.pop_back();
}

NS::NeighbourStorage(const NeighbourStorage &v)
{
    next = v.next;
    prev = v.prev;
    numb = v.numb;
    valuePosition = v.valuePosition;
    count = v.count;
}
