//
// Created by Igor Volokhovych on 10/31/2019.
//

#include <iomanip>
#include <sstream>
#include <cmath>
#include <algorithm>
#include <deque>
#include <utility>
#include <functional>
#include <set>
#include <stack>
#include <map>
#include "List.cpp"

#include "Long.h"

struct refs_less {
    bool operator()(const std::reference_wrapper<Long> &r1, const std::reference_wrapper<Long> &r2) {
        return r1.get() < r2.get();
    }
};


const Long Long::ZERO, Long::ONE(1), Long::N_ONE(-1), Long::TWO(2), Long::THREE(3);

Long::Long() : Long(0) {}

Long::Long(T n) : _sign(n < 0) {
    if (!n) {
        _num.addLast(0);
    }
    else {
        n = n < 0 ? -n : n;
        while (n) {
            _num.addLast(n % BASE);
            n /= BASE;
        }
    }

    normalize();
}

Long::Long(const string &_s) : _sign(false) {
    if (_s.empty()) {
        _num.addLast(0);
        _sign = false;
    }
    else {
        string s = _s;
        if (!isdigit(s[0]))
            if (s[0] == '-')
                _sign = true;

        auto i = s.begin();
        while (i != s.end()) {
            if (isdigit(*i))
                ++i;
            else
                i = s.erase(i);
        }

        i = s.begin();
        while (i != s.end() && *i == '0')
            i = s.erase(i);

        if (s.empty()) {
            _num.addLast(0);
            _sign = false;
        }
        else {
            auto ri = s.rbegin(), rend = s.rend();
            while (ri != rend) {
                int n = 0;
                T max_num = BASE;
                while (ri != rend && (max_num /= 10)) {
                    n += (*ri - '0') * (BASE / max_num / 10);
                    ++ri;
                }

                _num.addLast(n);
            }
        }
    }

    normalize();
}

Long Long::operator-() const {
    return this->negate().normalize();
}

Long& Long::operator++() {
    if (this->is_zero())
        _num.start()->value = 1;
    else {
        if (is_negative())
            dec_abs();
        else inc_abs();
    }

    return this->normalize();
}

void Long::inc_abs() {
    Long::List::Node *node = _num.start();
    T carry = 0;

    node->value += 1;
    carry = node->value / BASE;
    node->value %= BASE;

    node = node->next;

    while (node && carry) {
        T val = node->value + carry;

        carry = val / BASE;
        node->value = val % BASE;

        node = node->next;
    }

    while (carry) {
        _num.addLast(carry % BASE);
        carry /= BASE;
    }
}

Long& Long::operator--() {
    if (this->is_zero()) {
        _num.start()->value = 1;
        _sign = true;
    }
    else {
        if (is_negative())
            inc_abs();
        else dec_abs();
    }

    return this->normalize();
}

void Long::dec_abs() {
    Long::List::Node *node = _num.start();
    T carry = 0;

    node->value -= 1;
    if (node->value < 0) {
        carry = 1;
        node->value += BASE;
    }
    else {
        carry = 0;
    }

    node = node->next;

    while (carry) {
        T val = node->value;

        val -= carry;
        if (val < 0) {
            carry = 1;
            val += BASE;
        }
        else {
            carry = 0;
        }

        node->value = val;
        node = node->next;
    }
}

bool Long::operator==(const Long &l) const {
    if (_sign != l._sign || _num.size() != l._num.size())
        return false;

    Long::List::Node *node = _num.start(), *lnode = l._num.start();
    while (node) {
        if (node->value != lnode->value)
            return false;

        node = node->next;
        lnode = lnode->next;
    }

    return true;
}

bool Long::operator!=(const Long &l) const {
    return !(*this == l);
}

bool Long::operator<(const Long &l) const {
    if (_sign != l._sign)
        return _sign;

    int cmp = cmp_abs(l);
    return _sign ? cmp > 0 : cmp < 0;
}

bool Long::operator>(const Long &n) const {
    return !(*this <= n);
}
bool Long::operator<=(const Long &n) const {
    return *this < n || *this == n;
}

bool Long::operator>=(const Long &n) const {
    return !(*this < n);
}

int Long::cmp_abs(const Long &l) const {
    int size = _num.size(), lsize = l._num.size();
    if (size < lsize)
        return -1;
    else if (size > lsize)
        return 1;

    Long::List::Node *node = _num.end(), *lnode = l._num.end();
    while (node && node->value == lnode->value) {
        lnode = lnode->prev;
        node = node->prev;
    }

    if (node) {
        if (node->value < lnode->value)
            return -1;

        return 1;
    }

    return 0;
}

bool Long::is_zero() const {
    return _num.size() == 1 && _num.first() == 0;
}

bool Long::is_negative() const {
    return _sign;
}

bool Long::is_even() const {
    return !(_num.first() % 2);
}

Long Long::operator+(const Long &l) const {
    Long res;

    int cmp = cmp_abs(l);
    if (_sign != l._sign) {
        if (cmp == 1) {
            res = sub_abs(l);
            res._sign = _sign;
        }
        else {
            res = l.sub_abs(*this);
            res._sign = l._sign;
        }
    }
    else {
        if (cmp == 1)
            res = add_abs(l);
        else
            res = l.add_abs(*this);

        res._sign = _sign;
    }

    return res.normalize();
}

Long Long::operator-(const Long &l) const {
    Long res;

    int cmp = cmp_abs(l);
    if (_sign != l._sign) {
        if (cmp == 1)
            res = add_abs(l);
        else
            res = l.add_abs(*this);

        res._sign = _sign;
    }
    else {
        if (cmp == 1) {
            res = sub_abs(l);
            res._sign = _sign;
        }
        else {
            res = l.sub_abs(*this);
            res._sign = !_sign;
        }
    }

    return res.normalize();
}

Long Long::operator*(const Long &l) const {
    Long res;

    if (*this < l)
        res = l.mul_abs(*this);
    else
        res = mul_abs(l);

    res._sign = _sign != l._sign;

    return res.normalize();
}

Long Long::operator*(T multiplier) const {
    if (!multiplier)
        return ZERO;
    else if (multiplier == 1)
        return *this;

    Long res = *this;

    if (multiplier < 0) {
        res._sign = !res._sign;
        multiplier = -multiplier;
    }

    if (multiplier > BASE) {
        while (multiplier) {
            res = res * (multiplier % BASE);
            multiplier /= BASE;
        }
    }
    else {
        T carry = 0;
        Long::List::Node *node = res._num.start();
        while (node) {
            T val = node->value;

            val = val * multiplier + carry;
            node->value = val % BASE;
            carry = val / BASE;

            node = node->next;
        }

        while (carry) {
            res._num.addLast(carry % BASE);
            carry /= BASE;
        }
    }

    return res.normalize();
}

Long Long::operator/(const Long &l) const {
    if (l.is_zero())
        throw ZeroDivisionException();

    Long res;
    int cmp = cmp_abs(l);
    if (!cmp)
        res = ONE;
    else if (cmp > 0)
        res = div_abs(l);

    if (cmp >= 0)
        res._sign = l._sign != _sign;

    /*
    if (cmp < 0 && !is_zero() && (_sign ^ l._sign))
        res = Long(-1);
    else if (res._sign) {
        if (res * l != *this)
            res = res - Long(1);
    }
    */

    return res.normalize();
}


Long Long::operator%(const Long &l) const {
    if (l.is_zero())
        throw ZeroDivisionException();
    if (is_zero())
        return ZERO;

    Long res;
    if (cmp_abs(l) < 0) {
        res = this->abs();
    }
    else res = mod_abs(l);

    if (_sign && l._sign) {
        res = -res;
    }
    else if (_sign) {
        res = l - res;
    }
    else if (l._sign) {
        res = l + res;
    }

    return res.normalize();
}

Long Long::operator^(const Long &l) const {
    if (l.is_negative())
        return ZERO;
    if (l.is_zero())
        return ONE;
    if (l == ONE)
        return *this;

    Long n = l, y = ONE, x = *this;
    while (ONE < n) {
        if (n.is_even()) {
            x = x * x;
            n = n.div_2();
        }
        else {
            y = x * y;
            x = x * x;
            n = (n - 1).div_2();
        }
    }

    return x * y;
}


Long Long::sqrt() const {
    const Long &n = *this;
    if (n.is_negative())
        throw LongException("Sqrt negative!");
    if (n < TWO)
        return n;

    Long x = n.div_2(), _x, d = ZERO;
    do {
        _x = x;
        x = (x + n / x).div_2();
        d = x - _x;
        d._sign = false;
    } while (d > ONE);

    Long &minx = min(x, _x);
    Long xx = minx * minx, xm1 = minx - ONE, _xx = xm1 * xm1;

    if (xx <= n)
        return minx;

    return xm1;
}

Long Long::powmod(const Long &_a, const Long &n) const {
    if (_a.is_negative() || _a.is_zero())
        return ONE % n;
    else if (_a == ONE)
        return *this % n;

    deque<Long> pows;
    Long max_power = ZERO, power = 1, a = _a;
    while (power <= a) {
        ++max_power;
        power = power * 2;
    }

    power.self_div_2();
    --max_power;
    pows.push_front(max_power);
    a = a - power;

    while (!a.is_zero()) {
        while (power > a) {
            power.self_div_2();
            --max_power;
            if (max_power < ZERO)
                return Long();
        }

        pows.push_front(max_power);
        a = a - power;
    }

    Long res = ONE;
    max_power = ZERO, power = *this;
    while (!pows.empty()) {
        if (max_power == pows[0]) {
            res = res * power;
            pows.pop_front();
        }
        power = (power * power) % n;
        ++max_power;
    }

    return res % n;
}

Long Long::gcd(const Long &l1, const Long &l2) {
    if (l2.is_zero())
        return l1.abs();//throw ZeroDivisionException();
    else if (l1.is_zero())
        return l2.abs();
    /*
    if (l1.is_zero())
        return l2.abs();
    else if (l2.is_zero())
        return l1.abs();
    */

    Long res1, res2;
    if (l1 < l2) {
        res1 = l2.abs();
        res2 = l1.abs();
    }
    else {
        res1 = l1.abs();
        res2 = l2.abs();
    }

    bool odd = false; // true - res1 > res2, false - res1 < res2
    while (!res2.is_zero() && !res1.is_zero()) {
        if (odd)
            res1 = res1 % res2;
        else res2 = res2 % res1;

        odd = !odd;
    }

    return odd ? res1.normalize() : res2.normalize();
}

Long Long::lcm(const Long &l1, const Long &l2) {
    return (l1 / gcd(l1, l2) * l2).abs();
}

Long Long::abs() const {
    Long res = *this;
    res._sign = false;
    return res;
}


const Long& Long::min(const Long &n1, const Long &n2) {
    if (n1 < n2)
        return n1;

    return n2;
}

Long& Long::min(Long &n1, Long &n2) {
    if (n1 < n2)
        return n1;

    return n2;
}

Long Long::primitive_root(const Long &p) {
    Long phi = euler(p);
    auto factors = phi.rho_pollard_factorization();
    for (Long &f : factors) {
        f = phi / f;
    }

    for (Long r = TWO; r <= phi; ++r) {
        bool ok = true;
        for (Long &f : factors) {
            if (r.powmod(f, p) == ONE) {
                ok = false;
                break;
            }
        }

        if (ok)
            return r;
    }

    return N_ONE;
}

Long Long::log2() const {
    if (this->is_zero())
        throw LongException("Log2 of 0");

    Long n = this->abs(),
            res = 0;

    while (n > ONE) {
        ++res;
        n.self_div_2();
    }

    return res;
}

Long Long::mod2() const {
    return is_even() ? ZERO : ONE;
}

Long Long::negate() const {
    if (is_zero())
        return ZERO;

    Long res = *this;
    res._sign = !_sign;

    return res;
}

Long& Long::normalize() {
    while (_num.size() > 1 && _num.last() == 0)
        _num.removeLast();

    if (is_zero())
        _sign = false;

    return *this;
}

Long Long::add_abs(const Long &l) const {
    Long res = this->abs();

    Long::List::Node *node = res._num.start(), *lnode = l._num.start();
    T carry = 0;

    while (lnode) {
        T val = node->value;

        val += carry + lnode->value;
        carry = val / BASE;
        node->value = val % BASE;

        node = node->next;
        lnode = lnode->next;
    }

    while (carry && node) {
        T val = node->value + carry;

        carry = val / BASE;
        node->value = val % BASE;

        node = node->next;
    }

    while (carry) {
        res._num.addLast(carry % BASE);
        carry /= BASE;
    }

    return res.normalize();
}

Long Long::sub_abs(const Long &l) const {
    Long res = this->abs();

    Long::List::Node *node = res._num.start(), *lnode = l._num.start();
    T carry = 0;

    while (lnode) {
        T val = node->value;

        val -= carry + lnode->value;
        if (val < 0) {
            carry = 1;
            val += BASE;
        }
        else {
            carry = 0;
        }

        node->value = val;
        node = node->next;
        lnode = lnode->next;
    }

    while (carry && node) {
        T val = node->value;

        val -= carry;
        if (val < 0) {
            carry = 1;
            val += BASE;
        }
        else {
            carry = 0;
        }

        node->value = val;
        node = node->next;
    }

    return res.normalize();
}

Long Long::mul_abs(const Long &l) const {
    Long res;
    Long::List::Node *node = l._num.start();
    int digit = 0;
    while (node) {
        Long mul = *this * node->value;
        mul._sign = false;
        for (int i = 0; i < digit; ++i)
            mul._num.addFirst(0);

        res = res + mul;

        ++digit;
        node = node->next;
    }

    return res.normalize();

}

Long Long::div_abs(const Long &_l) const {
    Long res;
    Long tabs = this->abs(),
            min,
            max = tabs,
            l = _l.abs();
    while (min * l < tabs && tabs < max * l && ONE < max - min) {
        Long mid = (min + max).div_2();

        if (mid * l < tabs) {
            min = mid;
        } else {
            max = mid;
        }
    }

    if (max * l == tabs)
        res = max;
    else res = min;

    return res.normalize();
}

Long Long::div_2() const {
    Long res;
    res._num.removeFirst();

    Long::List::Node *node = _num.end();
    long carry = 0;

    while (node) {
        T val = node->value;

        long r = (carry + val) / 2;
        carry = (carry + val) - r * 2;
        carry *= BASE;

        res._num.addFirst(r);

        node = node->prev;
    }

    return res.normalize();
}

void Long::self_div_2() {
    Long::List::Node *node = _num.end();
    long carry = 0;

    while (node) {
        T val = node->value;

        long r = (carry + val) / 2;
        carry = (carry + val) - r * 2;
        carry *= BASE;

        node->value = r;

        node = node->prev;
    }

    normalize();
}

Long Long::mod_abs(const Long &l) const {
    Long res = this->abs(),
            labs = l.abs();
    res = res - labs * (res / labs);

    return res.normalize();
}

int Long::size() const {
    return _num.size();
}

int log10(int n) {
    int res = 0;
    while ((n /= 10)) {
        res++;
    }

    return res;
}

string Long::to_string() const {
    stringstream ss;

    int len = log10(BASE);

    if (is_negative()) ss << "-";

    Long::List::Node *node = _num.end();

    ss << node->value;
    node = node->prev;

    while (node) {
        if (PRINT_DELIMITER)
            ss << ",";
        ss << setw(len) << setfill('0') << node->value;

        node = node->prev;
    }

    return ss.str();
}

ostream& operator<<(ostream& out, const Long &l) {
    out << l.to_string();

    return out;
}

T Long::mobius(const Long &n) {
    if (n.is_zero() || n.is_negative())
        throw LongException("Argument must be natural");

    if (n == ONE)
        return 1;

    auto factors = n.rho_pollard_factorization();
    Long res = 1;
    for (const Long& f : factors) {
        res = res * f;
    }

    return res == n
           ? (factors.size() % 2 ? -1 : 1)
           : 0;
}

Long Long::euler(Long n) {
    if (n.is_zero())
        return ZERO;

    n._sign = false;
    if (n == ONE)
        return ONE;

    if (n.is_even()) {
        n.self_div_2();
        if (n.is_even())
            return euler(n) * 2;
        else return euler(n);
    }

    Long res = n;
    //Long nsqrt = n.sqrt();
    auto factors = n.rho_pollard_factorization();
    /*
    for (Long p = THREE; p <= nsqrt; ++p) {
        bool ok = false;
        while (n % p == ZERO) {
            n = n / p;
            ok = true;
        }

        if (ok)
            res = res - res/p;
    }
    */
    for (auto &f : factors) {
        bool ok = false;
        while (n.is_divisible_by(f)) {
            n = n / f;
            ok = true;
        }

        if (ok)
            res = res - res / f;
    }

    if (n > ONE)
        res = res - res / n;

    return res;
}

// DONE
deque<Long> Long::rho_pollard_factorization() const {
    deque<Long> factors;
    set<reference_wrapper<Long>, refs_less> unique_factors;
    Long n = this->abs();

    if (n <= ONE)
        return factors;

    Long sr = n.sqrt();
    while (sr * sr == n) {
        n = sr;
        sr = n.sqrt();
    }

    if (!n.is_composite()) {
        factors.push_back(n);
        return factors;
    }

    Long x0 = TWO, c = ONE;
    auto g = [&c, &n](Long &x) -> Long { return (x*x + c) % n; };

    while (ONE < n/* && x0 < n*/) {
        Long x = x0, y = g(x), d = ONE;
        while (d == ONE) {
            x = g(x);
            y = g(y);
            y = g(y);
            d = gcd((x-y).abs(), n);
        }

        if (d == n) {
            //cout << n << " || " << d << endl;
            // failure
            //++x0;
            x = Random::next(n._num.size() + 1) % (n - THREE) + TWO;
        }
        else {
            //cout << n << " | " << d << endl;
            auto d_factors = d.rho_pollard_factorization();

            for (auto &l : d_factors) {
                if (unique_factors.find(l) == unique_factors.end()) {
                    factors.push_back(l);
                    unique_factors.insert(ref(factors.back()));
                }
            }
            while (!n.is_zero() && n.is_divisible_by(d))
                n = n / d;

            if (n < TWO)
                break;

            auto n_factors = n.rho_pollard_factorization();
            for (auto &l : n_factors) {
                if (unique_factors.find(l) == unique_factors.end()) {
                    factors.push_back(l);
                    unique_factors.insert(ref(factors.back()));
                }
            }

            break;
        }
    }

    return factors;
}


deque<Long> Long::fermat_factorization() const {
    deque<Long> factors;

    if (cmp_abs(ONE) <= 0)
        return factors;

    stack<Long> st;
    st.push(abs());

    while (!st.empty()) {
        Long n = st.top(),
                a = n.sqrt();

        if (a * a == n) {
            st.push(a);
            continue;
        }
        else ++a;

        Long b2 = a*a - n,
                sr = b2.sqrt();

        st.pop();

        while (sr * sr != b2) {
            ++a;
            b2 = a*a - n;
            sr = b2.sqrt();
            cout << b2 << endl;
        }

        Long x1 = a - sr,
                x2 = a + sr;

        //cout << n << " " << x1 << " " << x2 << endl;

        if (x1 == ONE || x2 == ONE) {
            factors.push_back(n);
        }
        else {
            while (x1.is_divisible_by(x2))
                x1 = x1 / x2;
            while (x2.is_divisible_by(x1))
                x2 = x2 / x1;

            st.push(x1);
            st.push(x2);
        }
    }

    return factors;
}

pair<Long, Long> Long::tonelli_shanks(const Long &n, const Long &p) {
    if (p == TWO)
        return make_pair(n, n);

    Long q = p - 1,
            s = ZERO;
    while (q.is_even()) {
        q.self_div_2();
        ++s;
    }

    Long z = TWO;
    while (legendre_sym(z, p) != -1)
        ++z;

    Long c = z.powmod(q, p),
            r = n.powmod((q+ONE).div_2(), p),
            t = n.powmod(q, p),
            m = s;

    while (!(t - 1).is_divisible_by(p)) { // t % p != 1
        // min i : t^2^i = 1 (mod p), 0 < i < m
        Long i = 1;
        while (t.powmod(TWO ^ i, p) != ONE)
            ++i;

        // b = c^2^(m - i - 1)
        Long b = c.powmod(TWO ^ (m - i - ONE), p);

        r = (r * b) % p;
        t = (t * b * b) % p;
        c = (b * b) % p;
        m = i;
    }

    return make_pair(r, p-r);
}

deque<Long> Long::quadratic_sieve_factorization() const {
    //check 1, 0
    deque<Long> factors;
    Long n = abs(),
            log2 = n.log2(),
            B = Long(250) + THREE ^ (log2 * log2.log2()).sqrt(),
            sr = n.sqrt();

    // BEGIN factor base
    deque<Long> FB;
    //primes.push_back(N_ONE);
    FB.push_back(TWO);

    for (Long p = THREE; p <= B; ++p) {
        if (!p.is_composite() && legendre_sym(n, p) == 1) {
            FB.push_back(p);
        }
        ++p;
    }
    // END factor base

    map<reference_wrapper<Long>, pair<Long, Long>, refs_less> roots;
    for (auto &p : FB) {
        auto r = tonelli_shanks(n % p, p);
        Long r1 = (r.first - sr) % p,
                r2 = (r.second - sr) % p;
        if (r1.is_negative())
            r1 = (r1 + p) % p;
        if (r2.is_negative())
            r2 = (r2 + p) % p;

        roots[p] = make_pair(r1, r2);
    }



    return factors;
}

/*
deque<Long> Long::quadratic_sieve_factorization() const {
    //check 1, 0
    deque<Long> factors;
    Long n = abs(),
         nlog2 = n.log2(),
         B = TWO ^ (nlog2 * nlog2.log2()).sqrt(),
         m = n.sqrt(),
         a = n - m*m;

    auto q = [&n,&m](const Long &x) -> Long {
        Long xm = x + m;
        return xm * xm - n;
    };

    deque<Long> FB;
    map<reference_wrapper<Long>, pair<Long, Long>, refs_less> roots;
    do {
        //primes.push_back(N_ONE);
        FB.push_back(TWO);

        /
        Long p = 3,
             pc = primes.size();
        while (pc < B) {
            if (!p.is_composite() && legendre_sym(n, p) == 1) {
                primes.push_back(p);
                ++pc;
            }
        }
        /
        for (Long p = THREE; p <= B; ++p) {
            if (!p.is_composite() && legendre_sym(n, p) == 1) {
                FB.push_back(p);
            }
            ++p;
        }

        Long m2 = m * 2;
        for (auto &p : FB) {
            Long pm12 = (p - 1).div_2();
            for (Long i = 0; i <= pm12; ++i) {
                if ((q(i) % p).is_zero()) {
                    Long r1 = i,
                         r2 = -r1  % p;
                    roots[p] = pair<Long, Long>(r1, r2);
                    break;
                }
            }
        }
    } while (false);



    return factors;
}
*/

Long Long::inverse(const Long &m) const {
    Long x = *this, a = ZERO, b = m, u = 1;
    while (!x.is_negative() && !x.is_zero()) {
        Long _x = x, _a = a;
        x = b % x;
        a = u;
        u = _a - b / _x * u;
        b = _x;
    }

    if (b == ONE) {
        return a % m;
    }

    return ZERO;
}

bool Long::is_composite() const {
    if (cmp_abs(THREE) <= 0)
        return false;

    vector<T> primes = {2,3,5,7,11,13,17,19,23,29,31,37};
    for (T &p : primes) {
        if (!cmp_abs(p))
            return false;
        if (is_divisible_by(p))
            return true;
    }

    return miller_rabin_test(log2());// || solovay_strassen_test(log2()); //|| lucas_test(log2())*/;
}

deque<Long> Long::prime_factors() const {
    return rho_pollard_factorization();
}

bool Long::is_divisible_by(const Long &d) const {
    return d * (*this / d) == *this;
}

// TODO factors contain powers
Long Long::rho_pollard_logarithm(const Long &b, const Long &p) const {
    if (this->is_negative() || b < TWO || p.is_negative())
        return ZERO;
    Long u1 = ZERO, u2 = ZERO, v1 = ZERO, v2 = ZERO, z1 = ONE, z2 = ONE;
    Long pm1 = p - 1, pd3 = p / 3, pd32 = pd3 * 2;
    auto f = [&p, &pm1, &pd3, &pd32, &b, this](Long &u, Long &v, Long &z) {
        if (z < pd3) {
            ++u;
            u = u % pm1;
            //v = v % pm1;
            z = (b * z) % p;
        }
        else if (pd32 < z) {
            ++v;
            // u = u % pm1;
            v  = v % pm1;
            z = (*this * z) % p;
        }
        else {
            u = (u * 2) % pm1;
            v  = (v * 2) % pm1;
            z = (z * z) % p;
        }
    };

    f(u2, v2, z2);
    do {
        f(u1, v1, z1);

        f(u2, v2, z2);
        f(u2, v2, z2);
    } while (z1 != z2);

    Long du = u1 - u2, dv = v2 - v1, d = gcd(du, pm1);
    if (d == ONE) {
        return ((dv % pm1) * (du % pm1).inverse(pm1)) % pm1;
    }

    Long pm1dd = pm1 / d, bmodp = b % p;
    Long l = ((dv % pm1dd) * (du % pm1dd).inverse(pm1dd)) % (pm1dd);
    for (Long m = 0; m <= d; ++m) {
        if (this->powmod(l, p) == bmodp)
            return l;
        l = (l + pm1dd) % pm1;
    }

    return N_ONE;
}

int Long::legendre_sym(Long a, Long p) {
    if (p.is_composite() || p.is_zero())
        throw LongException("Second argument must be prime!");
    if (p.is_negative())
        throw LongException("Second argument must be positive");

    Long pm1d2 = p - 1;
    pm1d2.self_div_2();

    if (a.is_negative())
        a.negate();

    Long r = a.powmod(pm1d2, p);
    int res = 1;
    if (r > ONE)
        res = -1;
    else if (r.is_zero())
        res = 0;
    else if (r == ONE)
        res = 1;
    else if (r == N_ONE)
        res = -1;

    return res;
    //return jacobi_sym(a, p);
}

int Long::jacobi_sym(Long a, Long b) {
    if (b.is_negative())
        throw LongException("Second argument must be positive!");
    if (b.is_even())
        throw LongException("Second argument must be odd!");

    int res = 1;
    if (a < 0) {
        a._sign = !a._sign;
        if (!(b / 4).is_even())
            res = -res;
    }

    while (!a.is_zero()) {
        while (a.is_even()) {
            a.self_div_2();
            Long bmod8 = b % 8;
            if (bmod8 == 3 || bmod8 == 5)
                res = -res;
        }

        swap(a, b);

        if (a % 4 == 3 && b % 4 == 3)
            res = -res;
        a = a % b;
    }


    if (b == ONE)
        return res;

    return 0;
}

// true - composite
// false - prime
bool Long::lucas_test(Long k) const {
    const bool COMPOSITE = true,
            PRIME = false;

    Long n = this->abs();
    if (n <= THREE)
        return PRIME;
    else if (n.is_even())
        return COMPOSITE;

    Long a = this->abs(),
            nm1 = n - ONE;
    auto factors = nm1.prime_factors();
    for (auto &f : factors) {
        f = nm1 / f;
    }

    k._sign = false;
    k = k < n - 2 ? k : n - 3;
    //for (Long i = ZERO, a = TWO; i < k; ++i, ++a) {
    for (Long i = ZERO; i < k; ++i) {\
        Long a = Random::next(2, nm1);
        if (a.powmod(nm1, n) != ONE)
            return COMPOSITE;

        bool ok = false;
        for (auto &f : factors) {
            ok = a.powmod(f, n) != ONE;
            if (!ok)
                break;
        }

        if (ok)
            return PRIME;
    }

    return COMPOSITE;
}

// true - composite
// false - prime
bool Long::miller_rabin_test(Long k) const {
    const bool COMPOSITE = true,
            PRIME = false;

    Long n = this->abs();
    if (n <= THREE)
        return PRIME;
    else if (n.is_even())
        return COMPOSITE;

    Long nm3 = n - THREE;
    /*
    Long nm2 = n - TWO;
    if (k > nm2)
        k = nm2;
    */

    Long nm1 = n - ONE;
    Long t = nm1, s = ZERO;
    while (t.is_even()) {
        ++s;
        t.self_div_2();
    }

    k._sign = false;
    for (Long i = ZERO, a = TWO; i < k; ++i, ++a) {
        //for (Long i = ZERO; i < k; ++i) {
        //Long a = Random::next(2*_num.size()) % nm3 + 2;
        Long x = a.powmod(t, n);
        if (x == ONE || x == nm1)
            continue;

        bool ok = true;
        for (Long j = ONE; j < s; ++j) {
            x = x.powmod(2, n);//(x * x) % n;
            if (x == ONE)
                return COMPOSITE;
            else if (x == nm1) {
                ok = false;
                break;
            }
        }

        if (ok)
            return COMPOSITE;
    }

    return PRIME;
}

// true - composite
// false - prime
bool Long::solovay_strassen_test(Long k) const {
    Long n = this->abs();
    if (n < THREE)
        return false;
    else if (n.is_even())
        return true;

    Long nm1 = n - ONE, nm12 = nm1.div_2();
    k._sign = false;
    if (k > nm1)
        k = nm1;

    for (Long a = TWO; a <= k; ++a) {
        int j = jacobi_sym(a, n);
        if (!j || Long(j) % n != a.powmod(nm12, n)) {
            return true;
        }
    }

    return false;
}

void elgamal(const Long& M) {
    // key generation
    Long p = 89, // random prime number > M
            g = Long::ZERO, // g^Ф(p) = 1 (mod p)
            x = 50; // 1 < x < p

    Long y = g.powmod(x, p);

    // open - p, g, y
    // closed - x

    Long k = 42, // 1 < k < p - 1
            a = g.powmod(k, p),
            b = (y.powmod(k, p) * (M % p)) % p;

    // cipher text - a, b

    Long _M = ((b % p) * (a.powmod(x, p).inverse(p))) % p;

    cout << M << " -> " << a << " " << b << " -> " << _M << endl;
}


// Random

T Random::_seed = 100;

T Random::seed() {
    return 0;
}

Long Random::next() {
    return next(next_scalar() % 30 + 1);
}

// [a,b]
Long Random::next(const Long &_a, const Long &_b) {
    const Long &a = Long::min(_a, _b), &b = _a == a ? _b : _a;
    return Random::next(b._num.size() * 2) % (b-a+1) + a;
}

Long Random::next(Long k) {
    if (k.is_negative() || k.is_zero())
        return Long::ZERO;

    Long res;
    res._num.removeLast();
    while (!k.is_zero()) {
        res._num.addLast(next_scalar() % Long::BASE);
        --k;
    }

    return res.normalize();
}

Long Random::next_prime() {
    return next_prime(next_scalar() % 3 + 1);
}

Long Random::next_prime(Long k) {
    Long res = next(k);
    if (res.is_zero())
        return res;

    if (res.is_even())
        ++res;

    while (res.is_composite()) {
        ++res;
        ++res;
        //res = res + TWO;
    }

    return res;
}

T Random::next_scalar() {
    _seed = 214013 * _seed + 2531011;
    return (_seed >> 16) & 0x7FFF;
}


// RSA

RSA::RSA() {
    Long p = Random::next_prime(2),//3557
            q = Random::next_prime(2);//2579

    while (p == q)
        q = Random::next_prime(1);

    _n = p * q;
    Long phi_n = (p - 1) * (q - 1);

    _e = get_e(phi_n);// Long::THREE % (phi_n - 1) + 1; // 1 < e < phi_n
    _d = (_e % phi_n).inverse(phi_n);
    cout << "p=" << p << " q=" << q << " n=" << _n << " Ф(n)=" << phi_n << " e=" << _e << " d=" << _d << endl;
}

Long RSA::encrypt(const Long &m) {
    return m.powmod(_e, _n);
}

Long RSA::decrypt(const Long &c) {
    return c.powmod(_d, _n);
}

Long RSA::get_e(const Long &phi_n) {
    T r = Random::next_scalar() % 3, _r = r;
    Long fermat[] = { 17, 257, 65537 };

    do {
        Long &f = fermat[r];
        if (f < phi_n && f * (phi_n / f) != phi_n)
            return f;

        ++r;
        r %= 3;
    } while (r != _r);

    Long res = phi_n.sqrt();
    if (res.is_even() && phi_n.is_even())
        ++res;

    while (Long::gcd(res, phi_n) > 1) {
        ++res;
        ++res;
    }

    return res;
}

// ELGAMAL

ElGamal::ElGamal() {
    _p = Random::next_prime(2); // random prime
    _g = Long::primitive_root(_p);//7; // primitive root of Zp
    _x = Random::next(2, _p-2); // random in range [2,p-2]
    _y = _g.powmod(_x, _p);
}

pair<Long, Long> ElGamal::encrypt(const Long &m) {
    Long k = Random::next(2, _p-2), // random in range [2,p-2]
            c1 = _g.powmod(k, _p),
            c2 = (_y.powmod(k, _p) * (m % _p)) % _p;

    return pair<Long, Long>(c1, c2);
}

Long ElGamal::decrypt(const Long &c1, const Long &c2) {
    return (c1.powmod(_p - _x - 1, _p) * (c2 % _p)) % _p;
}

Long ElGamal::decrypt(const pair<Long, Long> &p) {
    return decrypt(p.first, p.second);
}


// EC

const EC::ECPoint EC::ECPoint::INF(0, 0);

//EC::EC() : _n(386), _c(-1, 188, 751, ECPoint(0, 376)), _PB(201, 5) {}
//EC::EC() : _n(211), _c(0, -4, 241, ECPoint(2, 2)), _nA(121), _nB(203) {
EC::EC() : _n("4451685225093714776491891542548933"), _c( Long("4451685225093714772084598273548424"), Long("2061118396808653202902996166388514"),Long("4451685225093714772084598273548427"), ECPoint(Long("188281465057972534892223778713752"), Long("3419875491033170827167861896082688"))), _nA(121), _nB(203) {
    _PA = _c.G().mul(_nA, _c);
    _PB = _c.G().mul(_nB, _c);
    //cout << "PA=" << _PA << " PB=" << _PB << endl;
}

pair<EC::ECPoint, EC::ECPoint> EC::encrypt(const Long &m) const {
    ECPoint P = _c.point(m);

    return pair<EC::ECPoint, EC::ECPoint>(_c.G().mul(_n, _c), P.add(_PB.mul(_n, _c), _c));
}

Long EC::decrypt(const EC::ECPoint &p1, const EC::ECPoint &p2) const {
    return p2.add(p1.mul(_nB, _c).negate(), _c).x();
}

EC::ECPoint::ECPoint(const Long &_x, const Long &_y) : _x(_x), _y(_y), _inf(false) {}

EC::ECPoint::ECPoint() : _x(0), _y(0), _inf(true) {}

bool EC::ECPoint::inf() const {
    return _inf;
}


const Long& EC::ECPoint::x() const {
    return _x;
}
const Long& EC::ECPoint::y() const {
    return _y;
}

ostream& operator<<(ostream &out, const EC::ECPoint &p) {
    if (p._inf)
        out << "(INF)";
    else out << "(" << p._x << ", " << p._y << ")";
    return out;
}

bool EC::ECPoint::operator==(const ECPoint &q) const {
    return (_inf == q._inf == false) || (_inf == q._inf && _x == q._x && _y == q._y);
}

EC::ECPoint EC::ECPoint::add(const EC::ECPoint &q, const EC::ECCurve &c) const {
    const ECPoint &p = *this;
    if (p.inf())
        return q;
    if (q.inf())
        return p;
    if (p._x == q._x && p._y == -q._y)
        return INF;

    Long l = c.a();//lambda(p, q, c);

    Long xr = l*l - p._x - q._x;
    return ECPoint(xr % c.m(), (l * (p._x - xr) - p._y) % c.m());
}

EC::ECPoint& EC::ECPoint::self_add(const EC::ECPoint &q, const EC::ECCurve &c) {
    if (inf())
        *this = q;
    else if (_x == q._x && _y == -q._y)
        *this = INF;
    else if (*this == q) {

    }
    else if (!q.inf()) {
        /*
        Long dx = q._x - _x,
             dy = q._y - _y;
        if (dx.is_negative())
            dx = dx + c.m();
        if (dy.is_negative())
            dy = dy + c.m();
        */
        Long l = c.a();//lambda(*this, q, c);

        Long xr = l*l - _x - q._x;
        *this = ECPoint(xr % c.m(), (l * (_x - xr) - _y) % c.m());
    }

    return *this;
}

EC::ECPoint EC::ECPoint::mul(Long K, const EC::ECCurve &c) const {
    auto const &p = *this;
    if (p.inf() || K.is_negative() || K.is_zero())
        return INF;

    /*
    ECPoint res = p;
    Long k = Long::TWO;
    while (k <= K) {
        res.self_add(res, c);
        k = k * 2;
    }

    k = k.div_2();

    cout << k << "/" << K << endl;
    while (k < K) {
        res.self_add(p, c);
        ++k;
    }
    */
    if (K == 1)
        return p;

    ECPoint res = p;

    while (K.is_even()) {
        res.self_add(res, c);
        K = K.div_2();
    }

    return p.add(res.mul(K-Long::ONE, c), c);
}

EC::ECPoint& EC::ECPoint::negate() {
    _y = -_y;
    return *this;
}

Long EC::ECPoint::lambda(const EC::ECPoint &p, const EC::ECPoint &q, const EC::ECCurve &c) {
    if (p == q)
        return (p._x*p._x*3 + c.a()) / (p._y * 2);

    return (p._y - q._y) / (p._x - q._x);
}

// EC::ECCurve

EC::ECCurve::ECCurve(const Long &_a_, const Long &_b_, const Long &_m_, const ECPoint &_G_) : _a(_a_), _b(_b_), _m(_m_), _G(_G_) {}

const Long& EC::ECCurve::a() const {
    return _a;
}

const Long& EC::ECCurve::b() const {
    return _b;
}

const Long& EC::ECCurve::m() const {
    return _m;
}

const EC::ECPoint& EC::ECCurve::G() const {
    return _G;
}

Long EC::ECCurve::y(const Long &x) const {
    return (x*x*x + _a*x + _b).powmod((_m-1).div_2(), _m);
}

EC::ECPoint EC::ECCurve::point(const Long &x) const {
    return EC::ECPoint(x, y(x));
}
