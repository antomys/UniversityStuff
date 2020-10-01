#ifndef NEIGHBOURSTORAGE_H
#define NEIGHBOURSTORAGE_H

#include <vector>
#include <map>

#include <QList>
#include <QLineF>

using std::map;
using std::vector;

class NeighbourStorage
{
    int count;

    vector<int> next;
    vector<int> prev;


    /*
     can be changed to array :
                       |   map    |  array  |
      time per event   | n log n  |    n    |
      memory per event |    n     |   n^2   |
     */
    map<int,int> valuePosition;

public:
    NeighbourStorage();

    vector<int> numb;

    int getNext(int n);
    int getPrev(int n);
    void addAfter(int v,int pr = -1);
    void addBefore(int v,int pr = -1);
    void remove(int v);

    void relaxate(int pos);

    int getFirst();
    NeighbourStorage(const NeighbourStorage& v);

    //test
    void printList();

    QList<QLineF> toLinesList(  );
};

#endif // NEIGHBOURSTORAGE_H
