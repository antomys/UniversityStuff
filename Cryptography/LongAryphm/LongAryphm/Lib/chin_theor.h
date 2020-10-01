//
// Created by Igor Volokhovych on 10/22/2019.
//

#ifndef LONGARYPHM_CHINESE_THEOREM_H
#define LONGARYPHM_CHINESE_THEOREM_H
#include <LongInts.h>
#include <exception>
#include <vector>
#include <iostream>
using namespace std;

namespace solver {
    void gcdext (LongInts a, LongInts b, LongInts *d, LongInts *x, LongInts *y)
    {
        LongInts s;
        if (b == 0)
        {
            *d = a; *x = 1; *y = 0;
            return;
        }
        gcdext(b,a % b,d,x,y);
        s = *y;
        *y = *x - (a / b) * (*y);
        *x = s;
    }

    LongInts solve(LongInts a, LongInts c, LongInts b)
    {
        LongInts x,y,d;
        gcdext(a,b,&d,&y,&x);
        if(c%d ==0){
            c = c /d;
            return y*c;
        }else{
            //throw std::some_error;
        }
    }

    void execute(){
        vector<LongInts> c,m;
        LongInts w,t;
        LongInts M = 1;
        while (cin >> w >> t) {
            c.push_back(w);
            m.push_back(t);
            M*=t;
        }
        LongInts res = 0;
        for (size_t i = 0;i < m.size();++i) {
            LongInts Ms = M/m[i];
            res += solve(Ms,1,m[i])*c[i]*Ms;
        }
        LongInts x,y,d;
        gcdext(1,M,&d,&y,&x);
        LongInts rr = res%d;
        if(rr ==0){
            res = res /d;
            cout << "x = (" << y*res << "+k*" << M/d << "); k=0,1,2..";
        }
        else
        {
           throw system_error();
            //throw std::some_error;
        }
    }
}

#endif //LONGARYPHM_CHINESE_THEOREM_H
