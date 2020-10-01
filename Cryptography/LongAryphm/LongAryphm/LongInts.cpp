//
// Created by Igor Volokhovych on 10/22/2019.
//

#include "Lib/LongInts.h"

LongInts::LongInts(std::int32_t u): digits(1, 0), negative(false)
{
    if(u < 0){
        digits[0] = -u;
        setNegative(true);
    }else{
        digits[0] = u;
    }
}
LongInts::LongInts(const std::string& s):digits(1, 0)
{
    std::istringstream iss(s);
    iss >> *this;
    if(iss.fail() || !iss.eof()){
        throw std::runtime_error("Error::LongInts::string");
    }
}
LongInts::LongInts(const LongInts& copy):digits(copy.digits), negative(copy.negative){}

LongInts &LongInts::operator=(const LongInts &rhs)
{
    digits = rhs.digits;
    negative = rhs.negative;
    return *this;
}

LongInts LongInts::operator++(int)
{
    LongInts t(*this);
    ++(*this);
    return t;
}

LongInts &LongInts::operator++()
{
    if(negative){
        setNegative(false);
        --(*this);
        setNegative(true);
    }else{
        for(size_t i = 0; i < digits.size() && ++digits[i] == 0; ++i);
        if(digits.back() == 0){
            digits.push_back(1);
        }
    }
    return *this;
}

LongInts LongInts::operator--(int)
{
    LongInts t(*this);
    --(*this);
    return t;
}

LongInts &LongInts::operator--()
{
    if(digits.back() == 0){
        ++(*this);
        setNegative(true);
    }else{
        if(negative){
            setNegative(false);
            ++(*this);
            setNegative(true);
        }else{
            for(size_t i = 0; i < digits.size() && digits[i]-- == 0; ++i);
            trim();
        }
    }
    return *this;
}

LongInts LongInts::sqrt(const LongInts& rhs)
{
    if (rhs.negative)
    {
        throw std::invalid_argument("Error: Unsigned::Negative_number");
    }
    LongInts t = rhs / 2, x;
    do{
        x = t;
        t = (x + rhs / x) / 2;
    }while (x - t > 1 || x - t < -1);
    return x;
}

LongInts LongInts::pow(const LongInts &rhs, size_t v)
{
    LongInts w=1,t=rhs;
    while(v)
    {
        if(v&1)
        {
            w*=t;
            --v;
        }
        else{
            t*=t;
            v>>1;
        }
    }

    /*LongInts w = 1;
    for(size_t i = 0; i < v; ++i){
        w *= rhs;
    }
    return w;*/
}

LongInts LongInts::abs(const LongInts &rhs)
{
    LongInts w(rhs);
    if(w.negative){
        w *= -1;
    }
    return w;
}

LongInts LongInts::add_mod(const LongInts &u, const LongInts &v, const LongInts &m)
{
    LongInts w(u), t(v);
    w += t;
    return w % m;
}

LongInts LongInts::sub_mod(const LongInts &u, const LongInts &v, const LongInts &m)
{
    LongInts w(u), t(v);
    w -= t;
    return w % m;
}

LongInts LongInts::mul_mod(const LongInts &u, const LongInts &v, const LongInts &m)
{
    LongInts w(u), t(v);
    w *= t;
    return w % m;
}

LongInts LongInts::div_mod(const LongInts &u, const LongInts &v, const LongInts &m)
{
    LongInts w(u), t(v);
    w /= t;
    return w % m;
}

LongInts LongInts::mod_mod(const LongInts &u, const LongInts &v, const LongInts &m)
{
    LongInts w(u), t(v);
    w %= t;
    return w % m;
}

LongInts LongInts::pow_mod(const LongInts &rhs, size_t v, const LongInts &m)
{
   LongInts w=1;
   for(size_t i=1; i<v; i++) {w*=rhs; w%=m;}
   return w;

    /* LongInts w(rhs);
    w = pow(w, v);
    return w % m;*/
}

LongInts &LongInts::operator%=(const LongInts &rhs)
{
    LongInts q;
    divide(rhs, q, *this);

    return *this;
}

LongInts &LongInts::operator+=(const LongInts &rhs)
{
    if(negative == rhs.negative){
        const size_t n = rhs.digits.size();
        if(digits.size() < n){
            digits.resize(n, 0);
        }
        size_t i = 0;
        Wigit k = 0;
        for(; i < n; ++i){
            k = k + digits[i] + rhs.digits[i];
            digits[i] = static_cast<Digit>(k);
            k >>= BITS;
        }
        for(; k != 0 && i < digits.size(); ++i){
            k += digits[i];
            digits[i] = static_cast<Digit>(k);
            k >>= BITS;
        }
        if(k != 0){
            digits.push_back(1);
        }
        return *this;
    }else{
        if(negative){
            LongInts w(rhs), t(*this);
            t.setNegative(false);
            w -= t;
            *this = w;
            return *this;
        }else{
            LongInts w(*this), t(rhs);
            t.setNegative(false);
            w -= t;
            *this = w;
            return *this;
        }
    }
}

LongInts &LongInts::operator-=(const LongInts &rhs)
{
    if(negative == rhs.negative){
        if(negative){
            LongInts w(*this), t(rhs);
            t.setNegative(true);
            w += t;
            *this = w;
            return *this;
        }else{
            if ((*this) < rhs)
            {
                LongInts w(rhs), t(*this);
                w -= t;
                w.setNegative(true);
                *this = w;
                return *this;
            }
            size_t i = 0;
            Wigit k = 0;
            for (; i < rhs.digits.size(); ++i)
            {
                k = k + digits[i] - rhs.digits[i];
                digits[i] = static_cast<Digit>(k);
                k = ((k >> BITS) ? -1 : 0);
            }
            for (; k != 0 && i < digits.size(); ++i)
            {
                k += digits[i];
                digits[i] = static_cast<Digit>(k);
                k = ((k >> BITS) ? -1 : 0);
            }
            trim();
            return *this;
        }
    }else if(negative){
        LongInts w(*this), t(rhs);
        w.setNegative(false);
        w += t;
        w.setNegative(true);
        *this = w;
        return *this;
    }else{
        LongInts w(*this), t(rhs);
        t.setNegative(false);
        w += t;
        *this = w;
        return *this;
    }
}

LongInts &LongInts::operator*=(const LongInts &rhs)
{
//    *this = (*this) * rhs;
//    return *this;


    const size_t m = digits.size();
    const size_t n = rhs.digits.size();
    LongInts w;
    w.digits.resize(m + n, 0);
    for(size_t j = 0; j < n; ++j){
        std::uint64_t k = 0;
        for(size_t i = 0; i < m; ++i){
            k += static_cast<std::uint64_t>(digits[i]) * rhs.digits[j] + w.digits[i + j];
            w.digits[i + j] = static_cast<std::uint32_t>(k);
            k >>= 32;
        }
        w.digits[j + m] = static_cast<std::uint32_t>(k);
    }
    w.trim();
    if(negative){
        if(rhs.negative){
            w.setNegative(false);
        }else{
            w.setNegative(true);
        }
    }else if(rhs.negative){
        w.setNegative(true);
    }

    *this = w;
    return *this;
}

LongInts &LongInts::operator/=(const LongInts &rhs)
{
    LongInts r;
    divide(rhs, *this, r);
    return *this;
}

LongInts &LongInts::operator<<=(size_t rhs)
{
    if(digits.back() != 0 && rhs != 0){
        const size_t n = rhs / BITS;
        digits.insert(digits.begin(), n, 0);
        rhs -= n * BITS;
        Wigit k = 0;
        for(size_t i = n; i < digits.size(); ++i){
            k |= static_cast<Wigit>(digits[i]) << rhs;
            digits[i] = static_cast<Digit>(k);
            k >>= BITS;
        }
        if(k != 0){
            digits.push_back(static_cast<Digit>(k));
        }
    }
    return *this;
}

LongInts &LongInts::operator>>=(size_t rhs)
{
    const size_t n = rhs / BITS;
    if( n >= digits.size()){
        digits.assign(1, 0);
        setNegative(false);
    }else{
        digits.erase(digits.begin(), digits.begin() + n);
        rhs -= n * BITS;
        Wigit k = 0;
        for(size_t i = digits.size(); i-- != 0;){
            k = k << BITS | digits[i];
            digits[i] = static_cast<Digit>(k >> rhs);
            k = static_cast<Digit>(k);
        }
        trim();
    }
    return *this;
}

LongInts &LongInts::operator&=(const LongInts &rhs)
{
    const size_t n = rhs.digits.size();
    if (digits.size() > n)
    {
        digits.resize(n);
    }
    for (size_t j = 0; j < digits.size(); ++j)
    {
        digits[j] &= rhs.digits[j];
    }
    trim();
    setNegative(negative & rhs.negative);
    return *this;
}

LongInts &LongInts::operator^=(const LongInts &rhs)
{
    const size_t n = rhs.digits.size();
    if (digits.size() < n)
    {
        digits.resize(n, 0);
    }
    for (size_t j = 0; j < n; ++j)
    {
        digits[j] ^= rhs.digits[j];
    }
    trim();
    setNegative(negative & rhs.negative);
    return *this;
}

LongInts &LongInts::operator|=(const LongInts &rhs)
{
    const size_t n = rhs.digits.size();
    if (digits.size() < n)
    {
        digits.resize(n, 0);
    }
    for (size_t j = 0; j < n; ++j)
    {
        digits[j] |= rhs.digits[j];
    }
    setNegative(negative & rhs.negative);
    return *this;
}


std::string LongInts::to_string() const
{
    std::ostringstream oss;
    LongInts q(*this), r;
    char sign = '\0';
    if(q.negative){
        sign = '-';
    }
    do
    {
        q.divide(10, q, r);
        oss << r.digits[0];
    } while (q.digits.back() != 0);
    if(sign == '-'){
        oss << sign;
    }
    std::string s(oss.str());
    std::reverse(s.begin(), s.end());
    return s;
}

void LongInts::divide(LongInts v, LongInts &q, LongInts &r) const
{
    // Handle special cases (m < n).
    if (v.digits.back() == 0)
    {
        throw std::overflow_error("Error: Unsigned::overflow");
    }
    r.digits = digits;
    const size_t n = v.digits.size();
    if (digits.size() < n) { q.digits.assign(1, 0); return; } // Normalize divisor (v[n-1] >= BASE/2).
    unsigned d = BITS;
    for (Digit vn = v.digits.back(); vn != 0; vn >>= 1, --d);
    v <<= d;
    r <<= d;
    const Digit vn = v.digits.back();

    // Ensure first single-digit quotient (u[m-1] < v[n-1]).
    r.digits.push_back(0);
    const size_t m = r.digits.size();
    q.digits.resize(m - n);
    LongInts w;
    w.digits.resize(n + 1);
    const Wigit MAX_DIGIT = (static_cast<Wigit>(1) << BITS) - 1;
    for (size_t j = m - n; j-- != 0;)
    {
        // Estimate quotient digit.
        Wigit qhat = std::min(MAX_DIGIT,
                              (static_cast<Wigit>(r.digits[j + n]) << BITS |
                               r.digits[j + n - 1]) / vn);

        // Compute partial product (w = qhat * v).
        Wigit k = 0;
        for (size_t i = 0; i < n; ++i)
        {
            k += qhat * v.digits[i];
            w.digits[i] = static_cast<Digit>(k);
            k >>= BITS;
        }
        w.digits[n] = static_cast<Digit>(k);

        // Check if qhat is too large (u - w < 0).
        bool is_trial = true;
        while (is_trial)
        {
            size_t i = n;
            for (; i != 0 && r.digits[j + i] == w.digits[i]; --i);
            if ((is_trial = (r.digits[j + i] < w.digits[i])))
            {
                // Adjust partial product (w -= v).
                --qhat;
                k = 0;
                for (size_t i = 0; i < n; ++i)
                {
                    k = k + w.digits[i] - v.digits[i];
                    w.digits[i] = static_cast<Digit>(k);
                    k = ((k >> BITS) ? -1 : 0);
                }
                w.digits[n] = static_cast<Digit>(k + w.digits[n]);
            }
        }
        q.digits[j] = static_cast<Digit>(qhat);

        // Compute partial remainder (u -= w).
        k = 0;
        for (size_t i = 0; i < n; ++i)
        {
            k = k + r.digits[j + i] - w.digits[i];
            r.digits[j + i] = static_cast<Digit>(k);
            k = ((k >> BITS) ? -1 : 0);
        }
    }

    // Denormalize remainder.
    q.trim();
    r.digits.resize(n);
    r >>= d;

    if(v.negative){
        if(q.negative){
            q.setNegative(false);
        }else{
            q.setNegative(true);
        }
    }

    if(r.checkZero()){
        r.setNegative(false);
    }
    if(q.checkZero()){
        q.setNegative(false);
    }
}

void LongInts::setNegative(bool v)
{
    if(checkZero()){
        negative = false;
    }else{
        negative = v;
    }
}

LongInts operator+(const LongInts &u, const LongInts &v)
{
    LongInts w(u);
    w += v;
    return w;
}

LongInts operator-(const LongInts &u, const LongInts &v)
{
    LongInts w(u);
    w -= v;
    return w;
}

LongInts operator*(const LongInts &u, const LongInts &v)
{
//    const size_t m = u.digits.size();
//    const size_t n = v.digits.size();
//    LongInts w;
//    w.digits.resize(m + n, 0);
//    for (size_t j = 0; j < n; ++j)
//    {
//        std::uint64_t k = 0;
//        for (size_t i = 0; i < m; ++i)
//        {
//            k += static_cast<std::uint64_t>(u.digits[i]) * v.digits[j] +
//                w.digits[i + j];
//            w.digits[i + j] = static_cast<std::uint32_t>(k);
//            k >>= 32;
//        }
//        w.digits[j + m] = static_cast<std::uint32_t>(k);
//    }
//    w.trim();
//    if(u.negative){
//        if(v.negative){
//            w.setNegative(false);
//        }else{
//            w.setNegative(true);
//        }
//    }else if(v.negative){
//        w.setNegative(true);
//    }
//    return w;


    LongInts w(u);
    w *= v;
    return w;
}

LongInts operator/(const LongInts &u, const LongInts &v)
{
    LongInts w(u);
    w /= v;
    return w;
}

LongInts operator%(const LongInts &u, const LongInts &v)
{
    LongInts w(u);
    w %= v;
    return w;
}


LongInts operator+(const LongInts &rhs)
{
    LongInts w(rhs);
    if(w.negative){
        w *= -1;
    }
    return w;
}

LongInts operator-(const LongInts &rhs)
{
    LongInts w(rhs);
    return w * (-1);
}

LongInts operator<<(const LongInts &u, size_t v)
{
    LongInts w(u);
    w <<= v;
    return w;
}

LongInts operator>>(const LongInts &u, size_t v)
{
    LongInts w(u);
    w >>= v;
    return w;
}

LongInts operator&(const LongInts &u, const LongInts &v)
{
    LongInts w(u);
    w &= v;
    return w;
}

LongInts operator^(const LongInts &u, const LongInts &v)
{
    LongInts w(u);
    w ^= v;
    return w;
}

LongInts operator|(const LongInts &u, const LongInts &v)
{
    LongInts w(u);
    w |= v;
    return w;
}

bool operator<(const LongInts &u, const LongInts &v)
{
    const size_t m = u.digits.size();
    size_t n = v.digits.size();
    if(u.negative){
        if(v.negative){
            if (m != n)
            {
                return !(m < n);
            }
            for (--n; n != 0 && u.digits[n] == v.digits[n]; --n);
            return !(u.digits[n] < v.digits[n]);
        }else{
            return true;
        }
    }else if(v.negative){
        return false;
    }
    if (m != n)
    {
        return (m < n);
    }
    for (--n; n != 0 && u.digits[n] == v.digits[n]; --n);
    return (u.digits[n] < v.digits[n]);

}

bool operator>(const LongInts &u, const LongInts &v)
{
    return (v < u);
}

bool operator<=(const LongInts &u, const LongInts &v)
{
    return !(v < u);
}

bool operator>=(const LongInts &u, const LongInts &v)
{
    return !(u < v);
}

bool operator==(const LongInts &u, const LongInts &v)
{
    return (u.negative == v.negative && u.digits == v.digits);
}

bool operator!=(const LongInts &u, const LongInts &v)
{
    return !(u == v);
}

std::ostream &operator<<(std::ostream &os, const LongInts &u)
{
    os << u.to_string();
    return os;
}

std::istream &operator>>(std::istream &is, LongInts &u)
{
    char digit = '\0';
    is >> digit;
    if(digit == '-'){
        u.negative = true;
        is >> digit;
    }
    if (is.good() && std::isdigit(digit))
    {
        u = digit - '0';
        while (std::isdigit(is.peek()))
        {
            is >> digit;
            u = 10 * u + (digit - '0');
        }
    }
    else
    {
        is.setstate(std::ios_base::failbit);
    }
    return is;
}


void LongInts::trim()
{
    while(digits.back() == 0 && digits.size() > 1){
        digits.pop_back();
    }
}

bool LongInts::checkZero()
{
    if(digits.size() == 1 && digits[0] == 0){
        return true;
    }
    return false;
}

