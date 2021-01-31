//
// Created by Igor Volokhovych on 10/22/2019.
//

#ifndef LONGARYPHM_LONGINTS_H
#define LONGARYPHM_LONGINTS_H

#include <cstdint>
#include <vector>
#include <iostream>
#include <stdexcept>
#include <algorithm>
#include <sstream>
#include <cctype>

using std::size_t;

class LongInts {
public:
    typedef std::uint32_t Digit;
    typedef std::uint64_t Wigit;
    static const unsigned BITS = 32;

    LongInts(std::int32_t i = 0);
    LongInts(const std::string& s);
    LongInts(const LongInts& copy);


    LongInts& operator= (const LongInts& rhs);
    LongInts operator++ (int);
    LongInts& operator++ ();
    LongInts  operator-- (int);
    LongInts& operator-- ();


    LongInts& operator%= (const LongInts& rhs);
    LongInts& operator+= (const LongInts& rhs);
    LongInts& operator-= (const LongInts& rhs);
    LongInts& operator*= (const LongInts& rhs);
    LongInts& operator/= (const LongInts& rhs);

    static LongInts sqrt(const LongInts& rhs);
    static LongInts pow(const LongInts& rhs, size_t v);
    static LongInts abs(const LongInts& rhs);
    static LongInts add_mod(const LongInts& u, const LongInts& v, const LongInts& m);
    static LongInts sub_mod(const LongInts& u, const LongInts& v,const LongInts& m);
    static LongInts mul_mod(const LongInts& u, const LongInts& v,const LongInts& m);
    static LongInts div_mod(const LongInts& u, const LongInts& v,const LongInts& m);
    static LongInts mod_mod(const LongInts& u, const LongInts& v,const LongInts& m);
    static LongInts pow_mod(const LongInts& rhs, size_t v,const LongInts& m);

    LongInts& operator<<= (size_t rhs);
    LongInts& operator>>= (size_t rhs);
    LongInts& operator&= (const LongInts& rhs);
    LongInts& operator^= (const LongInts& rhs);
    LongInts& operator|= (const LongInts& rhs);


    friend LongInts operator+ (const LongInts& u, const LongInts& v);
    friend LongInts operator- (const LongInts& u, const LongInts& v);
    friend LongInts operator* (const LongInts& u, const LongInts& v);
    friend LongInts operator/ (const LongInts& u, const LongInts& v);
    friend LongInts operator% (const LongInts& u, const LongInts& v);

    friend LongInts operator- (const LongInts& rhs);
    friend LongInts operator+ (const LongInts& rhs);


    friend LongInts operator<< (const LongInts& u, size_t v);
    friend LongInts operator>> (const LongInts& u, size_t v);
    friend LongInts operator& (const LongInts& u, const LongInts& v);
    friend LongInts operator^ (const LongInts& u, const LongInts& v);
    friend LongInts operator| (const LongInts& u, const LongInts& v);

    friend bool operator< (const LongInts& u, const LongInts& v);
    friend bool operator> (const LongInts& u, const LongInts& v);
    friend bool operator<= (const LongInts& u, const LongInts& v);
    friend bool operator>= (const LongInts& u, const LongInts& v);
    friend bool operator== (const LongInts& u, const LongInts& v);
    friend bool operator!= (const LongInts& u, const LongInts& v);

    friend std::ostream& operator<< (std::ostream& os, const LongInts& u);
    friend std::istream& operator>> (std::istream& is, LongInts& u);

    std::string to_string() const;
    void divide(LongInts v, LongInts& q, LongInts& r) const;
    void setNegative(bool v);
private:
    std::vector<Digit> digits;
    bool negative;
    void trim();
    bool checkZero();
};


#endif //LONGARYPHM_LONGINTS_H
