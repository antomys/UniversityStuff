//
// Created by Igor Volokhovych on 10/31/2019.
//

#ifndef NUMBTHEORCRYPTO_LONG_H
#define NUMBTHEORCRYPTO_LONG_H


#include <string>
#include <vector>
#include <utility>
#include <deque>
#include <iostream>
#include <stdexcept>

using namespace std;

typedef long long T;

class Long {
    friend class Random;
public:
    static const Long ZERO, ONE, N_ONE, TWO, THREE;
    static const bool PRINT_DELIMITER = false;

    Long();
    Long(T);
    Long(const string&);

    Long operator-() const;

    bool operator==(const Long&) const;
    bool operator!=(const Long&) const;
    bool operator<(const Long&) const;
    bool operator>(const Long&) const;
    bool operator<=(const Long&) const;
    bool operator>=(const Long&) const;
    int cmp_abs(const Long&) const;
    bool is_zero() const;
    bool is_negative() const;
    bool is_even() const;

    Long& operator++();
    Long& operator--();

    /*
    Long& operator+=(const Long&);
    Long& operator-=(const Long&);
    Long& operator*=(const Long&);
    Long& operator*=(T);
    Long& operator/(const Long&);
    Long& operator%(const Long&);
    Long& operator^(const Long&);
    */

    Long operator+(const Long&) const;
    Long operator-(const Long&) const;
    Long operator*(const Long&) const;
    Long operator*(T) const;
    Long operator/(const Long&) const;
    Long operator%(const Long&) const;
    Long operator^(const Long&) const;
    Long sqrt() const;
    static Long sqrt(const Long&);
    Long powmod(const Long&, const Long&) const;
    static Long gcd(const Long&, const Long&);
    static Long lcm(const Long&, const Long&);
    Long log2() const;
    Long mod2() const;
    Long div_2() const;
    Long abs() const;
    Long inverse(const Long&) const;
    bool is_composite() const;
    deque<Long> prime_factors() const;
    bool is_divisible_by(const Long&) const;

    static const Long& min(const Long&, const Long&);
    static Long& min(Long&, Long&);
    static Long primitive_root(const Long&);

    static T mobius(const Long&);
    static Long euler(Long);
    deque<Long> rho_pollard_factorization() const;
    Long rho_pollard_logarithm(const Long&, const Long&) const;
    deque<Long> fermat_factorization() const;
    deque<Long> quadratic_sieve_factorization() const;
    static pair<Long, Long> tonelli_shanks(const Long&, const Long&);

    static int legendre_sym(Long, Long);
    static int jacobi_sym(Long, Long);

    bool miller_rabin_test(Long) const;
    bool solovay_strassen_test(Long) const;
    bool lucas_test(Long) const;

    string to_string() const;
    friend ostream& operator<<(ostream&, const Long&);

private:

    class List {
    public:
        friend class Long;

        List();
        List(const List&);

        List& operator=(const List&);

        ~List();

        void addLast(T);
        void addFirst(T);

        T first() const;
        T last() const;

        bool removeFirst();
        bool removeLast();

        bool isEmpty() const;
        int size() const;

    private:

        class Node {
        public:

            T value;
            Node *next, *prev;

            Node(T v, Node *n = NULL, Node *p = NULL)
                    : value(v), next(n), prev(p) {}
        };
        Node *_start, *_end;
        int _size;

        void copy(const List&);
        void clear();

        Node* start() const;
        Node* end() const;
    };

    static const T BASE = 10000; // T * T <= max

    bool _sign;
    List _num;

    Long& normalize();

    void dec_abs();
    void inc_abs();

    Long add_abs(const Long&) const;
    Long sub_abs(const Long&) const;
    Long mul_abs(const Long&) const;
    Long div_abs(const Long&) const;
    Long mod_abs(const Long&) const;

    void self_div_2();
    Long mul_base(int) const;
    Long negate() const;
    int size() const;
};

class LongException : public invalid_argument {
public:
    LongException(const string msg) : invalid_argument(msg) {}
};

class ZeroDivisionException : public LongException {
public:
    ZeroDivisionException() : LongException("Division by zero!") {}
};

void elgamal(const Long&);

class Random {
public:

    static T seed();

    static Long next();
    static Long next(const Long&, const Long&);
    static Long next(Long);
    static Long next_prime();
    static Long next_prime(Long);
    static T next_scalar();

private:
    static T _seed;
};

class RSA {
public:
    RSA();

    Long encrypt(const Long&);
    Long decrypt(const Long&);

private:
    Long _e, _d, _n;

    Long get_e(const Long&);
};

class ElGamal {
public:
    ElGamal();

    pair<Long, Long> encrypt(const Long&);
    Long decrypt(const Long&, const Long&);
    Long decrypt(const pair<Long, Long>&);

private:
    Long _p, _g, _y; // public key
    Long _x; // private key
};

class EC {
    class ECCurve;

public:
    class ECPoint;

    EC();

    pair<ECPoint, ECPoint> encrypt(const Long&) const;
    Long decrypt(const ECPoint&, const ECPoint&) const;

    class ECPoint {
        friend class EC;
    public:
        bool inf() const;
        const Long& x() const;
        const Long& y() const;

        friend ostream& operator<<(ostream&, const ECPoint&);

    private:
        static const ECPoint INF;

        Long _x, _y;
        bool _inf;

        ECPoint(); // INF constructor
        ECPoint(const Long&, const Long&);

        /*
            P + O = O + P = P
            P — P = P + (-P) = O, где -P = (xP; -yP)
            R = P + Q = (xR; yR)
            xR = λ2 — xP — xQ
            yR = λ ( xP — xR) — yP
            λ = (3 xP2 + a) / (2 yP), если P = Q
            λ = (yP — yQ) / (xP — xQ), если P ≠ Q
        */
        bool operator==(const ECPoint&) const;
        ECPoint add(const ECPoint&, const ECCurve&) const;
        ECPoint& self_add(const ECPoint&, const ECCurve&);
        ECPoint mul(Long, const ECCurve&) const;

        ECPoint& negate();

        static Long lambda(const ECPoint&, const ECPoint&, const ECCurve&);
    };

private:

    // y^2 = (x^3 + a*x + b) mod m
    class ECCurve {
        friend class EC;
    public:
        ECCurve(const Long&, const Long&, const Long&, const ECPoint&);

        const Long& a() const;
        const Long& b() const;
        const Long& m() const;
        const ECPoint& G() const;

        Long y(const Long &) const;
        ECPoint point(const Long &) const;

    private:
        Long _a, _b, _m;
        ECPoint _G;
    };

    Long _n;
    ECCurve _c;
    Long _nA, _nB;
    ECPoint _PA, _PB;

};

#endif //NUMBTHEORCRYPTO_LONG_H
