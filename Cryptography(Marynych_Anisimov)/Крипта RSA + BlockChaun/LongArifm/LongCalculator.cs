using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongArifm
{
    class LongCalculator
    {
        int LENGTH = 9;
        int MAX = 1000000000;

        Random rand = new Random();

        private List<int> getList(string s)
        {
            List<int> res = new List<int>();
            bool zn = false;
            if (s[0] == '-')
            {
                zn = true;
                s = s.Remove(0, 1);
            }
            while (s.Length >= LENGTH)
            {
                res.Add(Convert.ToInt32(s.Substring(s.Length - LENGTH, LENGTH)));
                s = s.Remove(s.Length - LENGTH, LENGTH);
            }
            if (s.Length > 0)
                res.Add(Convert.ToInt32(s));
            res.Reverse();
            if (zn) res[0] *= (-1);
            return res;
        }

        private string outString(List<int> r)
        {
            int l = r.Count();
            string res = r[0].ToString();
            string buf = "";
            for (int i = 1; i < l; ++i)
            {
                buf = r[i].ToString();
                if (buf.Length < LENGTH)
                {
                    for (int j = LENGTH - buf.Length; j > 0; --j)
                        res += "0";
                }
                res += buf;
            }
            return res;
        }

        public string Abs(string a)
        {
            if (a.Contains('-')) return a.Substring(1);
            return a;
        }

        public bool More(string a, string b)
        {
            if (a.Length == 0 || b.Length == 0) return false;
            if (Equal(a, b)) return false;

            bool za = true;
            bool zb = true;

            if (a[0] == '-') { za = false; a = a.Remove(0, 1); }
            if (b[0] == '-') { zb = false; b = b.Remove(0, 1); }

            if (za && !zb) return true;
            if (!za && zb) return false;

            bool answer = true;

            if (!za && !zb) answer = false;

            if (a.Length > b.Length) return answer;
            if (a.Length < b.Length) return !answer;

            int la = a.Length;
            for (int i = 0; i < la; ++i)
            {
                if (Convert.ToByte(a[i]) > Convert.ToByte(b[i])) return answer;
                if (Convert.ToByte(a[i]) < Convert.ToByte(b[i])) return !answer;
            }

            return false;
        }

        public bool Less(string a, string b)
        {
            if (a.Length == 0 || b.Length == 0) return false;
            if (Equal(a, b)) return false;

            bool za = true;
            bool zb = true;

            if (a[0] == '-') { za = false; a = a.Remove(0, 1); }
            if (b[0] == '-') { zb = false; b = b.Remove(0, 1); }

            if (za && !zb) return false;
            if (!za && zb) return true;

            bool answer = true;

            if (!za && !zb) answer = false;

            if (a.Length < b.Length) return answer;
            if (a.Length > b.Length) return !answer;

            int la = a.Length;
            for (int i = 0; i < la; ++i)
            {
                if (Convert.ToByte(a[i]) < Convert.ToByte(b[i])) return answer;
                if (Convert.ToByte(a[i]) > Convert.ToByte(b[i])) return !answer;
            }

            return false;
        }

        public bool Equal(string a, string b)
        {
            if (a.Equals(b)) return true;

            return false;
        }

        public string Add(string a, string b)
        {
            bool za = true;
            bool zb = true;

            if (a[0] == '-') { za = false; a = a.Remove(0, 1); }
            if (b[0] == '-') { zb = false; b = b.Remove(0, 1); }

            if ((za && zb) || (!za && !zb))
            {
                int la = a.Length;
                int lb = b.Length;

                if (la > lb)
                {
                    int dif = la - lb;
                    string buf = "";
                    for (int i = 0; i < dif; ++i)
                        buf += "0";
                    b = b.Insert(0, buf);
                }
                if (la < lb)
                {
                    int dif = lb - la;
                    string buf = "";
                    for (int i = 0; i < dif; ++i)
                        buf += "0";
                    a = a.Insert(0, buf);
                    la = lb;
                }

                int sum = 0;
                List<int> asb = new List<int>(getList(a));
                List<int> bsb = new List<int>(getList(b));
                List<int> r = new List<int>();
                for (int i = asb.Count - 1; i >= 0; --i)
                {
                    sum += asb[i] + bsb[i];
                    r.Insert(0, (sum % MAX));
                    sum /= MAX;
                }

                if (sum > 0) r.Insert(0, sum);

                if (!za) return "-" + outString(r);
                return outString(r);
            }

            if (za && !zb) return Sub(a, b);
            return Sub(b, a);
        }

        public string Sub(string a, string b)
        {
            if (Equal(a, b)) return "0";

            bool za = true;
            bool zb = true;

            if (a[0] == '-') { za = false; a = a.Remove(0, 1); }
            if (b[0] == '-') { zb = false; b = b.Remove(0, 1); }

            bool firstMore = true;
            if (Less(a, b)) firstMore = false;

            if (za && zb && firstMore)
            {
                List<int> asb = new List<int>(getList(a));
                List<int> bsb = new List<int>(getList(b));
                int la = asb.Count;
                int lb = bsb.Count;

                for (int i = 1; i <= lb; ++i)
                {
                    asb[la - i] -= bsb[lb - i];
                }
                for (int i = la - 1; i > 0; --i)
                {
                    if (asb[i] < 0)
                    {
                        asb[i] += MAX;
                        --asb[i - 1];
                    }
                }
                while (asb[0] == 0)
                    asb.RemoveAt(0);

                return outString(asb);
            }
            if (za && !zb) return Add(a, b);
            if (!za && zb) return "-" + Add(a, b);
            if (za && zb && !firstMore) return "-" + Sub(b, a);
            if (!firstMore) return Sub(b, a);
            return "-" + Sub(a, b);
        }

        public string Mult(string a, string b)
        {
            if (a == "0" || b == "0") return "0";
            if (More(b, a)) return Mult(b, a);

            bool za = true;
            bool zb = true;

            if (a[0] == '-') { za = false; a = a.Remove(0, 1); }
            if (b[0] == '-') { zb = false; b = b.Remove(0, 1); }

            List<int> asb = new List<int>(getList(a));
            List<int> bsb = new List<int>(getList(b));
            List<int> r = new List<int>();
            string bufForNull = "";
            string res = "0";
            int la = asb.Count;
            int lb = bsb.Count;
            long mul = 0;
            int buf = 0;
            for (int i = lb - 1; i >= 0; --i)
            {
                mul = 0;
                r.Clear();
                buf = bsb[i];
                for (int j = la - 1; j >= 0; --j)
                {
                    mul += Convert.ToInt64(asb[j]) * Convert.ToInt64(buf);
                    r.Insert(0, Convert.ToInt32(mul % MAX));
                    mul /= MAX;
                }
                if (mul > 0) r.Insert(0, Convert.ToInt32(mul));
                res = Add(res, outString(r) + bufForNull);
                bufForNull += "000000000";
            }

            if ((za && zb) || (!za && !zb)) return res;
            return "-" + res;
        }

        public string Div(string a, string b)
        {
            if (b == "0") return "0";
            if (Equal(a, b)) return "1";

            bool za = true;
            bool zb = true;

            if (a[0] == '-') { za = false; a = a.Remove(0, 1); }
            if (b[0] == '-') { zb = false; b = b.Remove(0, 1); }
            if (Equal(a, b)) return "-1";

            if (Less(a, b)) return "0";

            List<int> r = new List<int>();
            int la = a.Length;
            int lb = b.Length;
            int buf = 0;

            string sub = a.Substring(0, lb - 1);
            for (int i = lb-1; i < la; ++i)
            {
                buf = 0;
                sub += a[i];
                while (!Less(sub, b))
                {
                    ++buf;
                    sub = Sub(sub, b);
                }
                r.Add(buf);
                while (sub.Length > 0 && sub[0] == '0') sub = sub.Remove(0, 1);
            }

            while (r[0] == 0)
                r.RemoveAt(0);

            int k = 1;
            int m = 0;

            List<int> res = new List<int>();
            int count = r.Count - LENGTH * (r.Count / LENGTH);
            if (count != 0)
            {
                for (int i = count - 1; i >= 0; --i)
                {
                    m += r[i] * k;
                    k *= 10;
                }
                res.Add(m);
                r.RemoveRange(0, count);
            }
            k = MAX;
            m = 0;
            for (int i = 0; i < r.Count; ++i)
            {
                k /= 10;
                m += r[i] * k;
                if (k == 1)
                {
                    res.Add(m);
                    k = MAX;
                    m = 0;
                }
            }
            if ((za && zb) || (!za && !zb)) return outString(res);
            return "-" + outString(res);
        }

        public string Mod(string a, string b)
        {
            if (b == "0") return "0";

            return Sub(a, Mult(b, Div(a,b)));
        }

        public string Pow(string x, string n)
        {
            if (n == "0")
            {
                return "1";
            }

            if (Convert.ToInt32(n[n.Length - 1]) % 2 == 0)
            {
                string p = Pow(x, Div(n, "2"));
                return Mult(p, p);
            }
            else
            {
                return Mult(x, Pow(x, Sub(n, "1")));
            }
        }

        public string Add(string a, string b, string mod)
        {
            return Mod(Add(a, b), mod);
        }

        public string Sub(string a, string b, string mod)
        {
            return Mod(Sub(a, b), mod);
        }

        public string Mult(string a, string b, string mod)
        {
            return Mod(Mult(a, b), mod);
        }

        public string Div(string a, string b, string mod)
        {
            return Mod(Div(a, b), mod);
        }  

        public string Pow(string x, string n, string mod)
        {
            if (n == "0")
            {
                return "1";
            }

            if (Convert.ToInt32(n[n.Length - 1]) % 2 == 0)
            {
                string p = Pow(x, Div(n, "2"), mod);
                return Mult(p, p, mod);
            }
            else
            {
                return Mult(x, Pow(x, Sub(n, "1"), mod), mod);
            }
        }

        public string Mod(string a, string b, string mod)
        {
            return Mod(Mod(a, b), mod);
        }

        public string Sqrt(string n)
        {
            string r = "1";
            string q = "";
            for (; ; )
            {
                q = Div(Add(r, Div(n, r)), "2");
                if (!Less(Pow(Add(q, "1"), "2"), n) && !More(Pow(q, "2"), n)) break;
                r = q;
            }
            return q;
        }

        public string China(List<string> b, List<string> a) // return X ::=  (X mod a[i] == b[i])
        {
            string M = "1";
            int l = b.Count;
            foreach (string ai in a) M = Mult(M, ai);
            List<string> m = new List<string>();
            List<string> m1 = new List<string>();
            for (int i = 0; i < l; ++i)
            {
                m.Add(Div(M, a[i]));
                m1.Add(Mod(m[i], a[i]));
            }
            List<string> y = new List<string>();
            for (int i = 0; i < l; ++i)
            {
                y.Add("1");
                while (Mod(Sub(Mult(y[i], m1[i]), b[i]), a[i]) != "0") y[i] = Add(y[i], "1");
            }
            string res = Mult(m[0], y[0], M);
            for (int i = 1; i < l; ++i) res = Add(res, Mult(m[i], y[i], M));
            return Mod(res, M);
        }

        public string China2(List<string> b, List<string> a) // return X ::=  (X mod a[i] == b[i])
        {
            List<List<string>> all = new List<List<string>>();
            foreach (string s in b)
            {
                List<string> buf = new List<string>();
                buf.Add(s);
                all.Add(buf);
            }

            int l = all.Count;
            List<string> res = new List<string>();
            while (true)
            {
                res.Clear();
                res.AddRange(all[0]);
                for (int i = 1; i < l; ++i)
                {
                    List<string> buf = new List<string>();
                    buf.AddRange(res.Intersect(all[i]));
                    res.Clear();
                    if (buf.Count == 0) break;
                    res.AddRange(buf);
                }
                if (res.Count != 0) return res[0];
                for (int i = 0; i < l; ++i)
                {
                    all[i].Add(Add(all[i].Last(), a[i]));
                }
            }
        }

        private string fact(string l, string r, string n)
        {
            if (More(l, r)) return "1";
            if (Equal(l, r)) return l;
            if (Equal("1", Sub(r, l))) return Mult(l, r, n);
            string m = Div(Add(l, r), "2");
            return Mult(fact(l, m, n), fact(Add(m, "1"), r, n), n);
        }

        public string factorial(string n)
        {
            if (Less(n, "0")) return "0";
            if (Equal(n, "0")) return "1";
            if (Equal(n, "1") || Equal(n, "2")) return n;
            string res = fact("2", n, Add(n, "1"));
            return res;
        }
        public string NSD(string a, string b)
        {
            while(More(a, "1") && More(b, "1"))
            {
                if(More(a, b))
                {
                    a = Mod(a, b);
                    if (Equal(a, "0")) return b;
                }
                else
                {
                    b = Mod(b, a);
                    if (Equal(b, "0")) return a;
                }
                if (Equal(a, b)) return a;
            }
            return "1";
        }

        public string generateRandomNumber(string a, string b) //return random n Є [a, b]
        {
            string res = "1";
            string buf = Add("1", Sub(b, a));
            while(Less(res, buf))
            {
                res = Mult(Add(res, rand.Next().ToString()), rand.Next().ToString());
            }
            res = Add(a, Mod(res, buf));
            return res;
        }

        private string F(string x, string N, string a)
        {
            return Mod(Add(Mult(x, x), a), N);
        }

        public string factorPollard(string N) //find a1, ....., an : a1*...*an = N
        {
            if (primeFerma(N)) return N;
            if (Equal(N, "4")) return "2 2";
            string x = "2";
            string y = "2";
            string nsd = "1";
            string buf = "0";
            string a = "1";
            string count = "-" + N;
            while (Less(nsd, "2") && Less(count, N))
            {
                x = F(x, N, a);
                y = F(F(y, N, a), N, a);
                buf = Sub(y, x);
                if (Less(buf, "0")) buf = Mult(buf, "-1");
                nsd = NSD(buf, N);
                count = Add(count, "1");
            }
            if (More(nsd, "1")) return factorPollard(nsd) + " " + factorPollard(Div(N, nsd));
            count = "-" + N;
            a = "-1";
            nsd = "1";
            x = "2";
            y = "2";
            while (Less(nsd, "2") && Less(count, N))
            {
                x = F(x, N, a);
                y = F(F(y, N, a), N, a);
                buf = Sub(y, x);
                if (Less(buf, "0")) buf = Mult(buf, "-1");
                nsd = NSD(buf, N);
                count = Add(count, "1");
            }
            if (More(nsd, "1")) return factorPollard(nsd) + " " + factorPollard(Div(N, nsd));
            a = "-2";
            while (Less(a, N))
            {
                if (More(a, "0")) a = "-" + a;
                else
                    a = Add("1", Mult(a, "-1"));
                count = "-" + N;
                nsd = "1";
                x = "2";
                y = "2";
                while (Less(nsd, "2") && Less(count, N))
                {
                    x = F(x, N, a);
                    y = F(F(y, N, a), N, a);
                    buf = Sub(y, x);
                    if (Less(buf, "0")) buf = Mult(buf, "-1");
                    nsd = NSD(buf, N);
                    count = Add(count, "1");
                }
                if (More(nsd, "1")) return factorPollard(nsd) + " " + factorPollard(Div(N, nsd));
            }
            return N;
        }

        public string logBigSmal(string a, string b, string n) //find x : a^x = b (mod n)
        {
            string m = Add("0", Sqrt(n));
            string y = Pow(a, m, n);
            string buf = y;
            List<string> il = new List<string>();
            List<string> yl = new List<string>();
            for(string i = "1"; !More(i, m); i = Add("1", i))
            {
                il.Add(i);
                yl.Add(y);
                y = Mult(y, buf, n);
            }
            for (string j = "0"; !More(j, m); j = Add("1", j)) 
            {
                string bufer = Mult(b, Pow(a, j), n);
                if (yl.Contains(bufer))
                {
                    int numb = yl.IndexOf(bufer);
                    return Sub(Mult(il[numb], m), j);
                }
            }
            return "\\(-_-)/";
        }

        public string Mobius(string n) //M(1)=1; M(N)=(-1)^k, if N=a1*...ak && ai!=aj, else M(x)=0
        {
            if (Equal(n, "1")) return "1"; 
            List<string> buf = factorPollard(n).Split().ToList();
            int s = buf.Count;
            buf = buf.Distinct().ToList();
            if (s != buf.Count) return "0";
            if (s % 2 == 0) return "1";
            return "-1";
        }

        public string Euler(string n) // find count(x) : x<n && NSD(x, n)=1
        {
            if (Equal(n, "1")) return "1";
            List<string> buf = factorPollard(n).Split().ToList();
            List<string> num = new List<string>();
            List<int> count = new List<int>();
            string res = "1";
            int k = 0;
            foreach(string s in buf)
            {
                k = num.IndexOf(s);
                if(k == -1)
                {
                    num.Add(s);
                    count.Add(1);
                }
                else
                {
                    count[k]++;
                }
            }
            for(int i = 0; i < num.Count; ++i)
            {
                res = Mult(res, Mult(Sub(num[i], "1"), Pow(num[i], Sub(count[i].ToString(), "1"))));
            }
            return res;
        }

        public string Legenre(string a, string p) //L(a|p)=1, if Ex:x^2=a(mod p)
        {
            if (Less(p, "3")) return "0";
            //if (!prost(p)) return "0";
            if (More(NSD(Abs(a), p), "1")) return "0";
            if (Equal(a, "0") || Equal(a, "1")) return "1";

            string buf = Pow(a, Div(Sub(p, "1"), "2"), p);
            if (Equal("1", buf)) return "1";
            //if (Equal("-1", buf)) return "-1";
            return "-1";
        }

        public string Jacobi(string a, string p)//J(a|p)=L(a|p1)*...*L(a|pn), p=p1*...*pn
        {
            List<string> buf = factorPollard(p).Split().ToList();
            string res = "1";
            foreach (string s in buf)
            {
                res = Mult(res, Legenre(a, s));
            }
            return res;
        }

        public string Chipolla(string a, string p) // find x : x^2 = a (mod p)
        {
            string w = "0";
            string b = "1";
            while(true)
            {
                //b = Add(b, "1");
                b = generateRandomNumber("0", Sub(p, "1"));
                w = Add(p, Sub(Mult(b, b), a), p);
                if (Equal(w, "0")) return b + " " + Sub(p, b);
                if (Equal("-1", Legenre(w, p))) break;
            }
            string ac = b;
            string bc = "1";
            string ai = ac;
            string bi = bc;
            string bufa = "";
            string bufb = "";
            string step = Div(Add(p, "1"), "2");
            for(string i = "1"; Less(i, step); i = Add(i, "1"))
            {
                bufa = Add(Mult(ai, ac), Mult(w, bi), p);
                bufb = Add(ai, Mult(bi, ac), p);
                ai = bufa;
                bi = bufb;
            }
            return ai + " " + Sub(p, ai);
        }

        public bool prime100withFactorial(string n)
        {
            if (Equal(Add("0", factorial(Sub(n, "1")), n), Sub(n, "1"))) return true;
            return false;
        }

        public bool primeFerma(string n) //return prost(a)
        {
            if (n.Equals("1")) return false;
            for (int i = 0; i < 20; ++i)
            {
                string a = rand.Next().ToString();
                while (Equal("0", Mod(a, n))) a = rand.Next().ToString();
                if (!Equal(Pow(a, Sub(n, "1"), n), "1")) return false;
            }
            return true;
        }

        public bool primeFerma(string n, int count) //return prost(a)
        {
            if (n.Equals("1")) return false;
            for (int i = 0; i < count; ++i)
            {
                string a = rand.Next().ToString();
                while (Equal("0", Mod(a, n))) a = rand.Next().ToString();
                if (!Equal(Pow(a, Sub(n, "1"), n), "1")) return false;
            }
            return true;
        }

        public bool primeMillerRabin(string n) //return prost(a)
        {
            string nm1 = Sub(n, "1");
            string buf = nm1;
            uint s = 0;
            while (int.Parse(buf[buf.Length - 1].ToString()) % 2 == 0)
            {
                buf = Div(buf, "2");
                ++s;
            }
            string t = Div(nm1, Pow("2", s.ToString()));
            string nm2 = Sub(n, "2");
            for (int k = n.Length * 4; k > 0; --k)
            {
                string a = generateRandomNumber("2", nm2);
                string x = Pow(a, t, n);
                if (Equal(x, "1") || Equal(x, nm1)) continue;
                bool cont = false;
                for (uint i = s; i > 1; --i)
                {
                    x = Mult(x, x, n);
                    if (Equal(x, "1")) return false;
                    if (Equal(x, nm1))
                    {
                        cont = true;
                        break;
                    }
                }
                if (cont) continue;
                return false;
            }
            return true;
        }

        public bool primeMillerRabin(string n, int count) //return prost(a)
        {
            string nm1 = Sub(n, "1");
            string buf = nm1;
            uint s = 0;
            while (int.Parse(buf[buf.Length - 1].ToString()) % 2 == 0)
            {
                buf = Div(buf, "2");
                ++s;
            }
            string t = Div(nm1, Pow("2", s.ToString()));
            string nm2 = Sub(n, "2");
            for (int k = count; k > 0; --k)
            {
                string a = generateRandomNumber("2", nm2);
                string x = Pow(a, t, n);
                if (Equal(x, "1") || Equal(x, nm1)) continue;
                bool cont = false;
                for (uint i = s; i > 1; --i)
                {
                    x = Mult(x, x, n);
                    if (Equal(x, "1")) return false;
                    if (Equal(x, nm1))
                    {
                        cont = true;
                        break;
                    }
                }
                if (cont) continue;
                return false;
            }
            return true;
        }

        public bool primeSoloveyShtrassen(string a) //return prost(a)
        {
            if (Equal("0", Mod(a, "2"))) return false;
            for (int i = 0; i < 20; ++i)
            {

                string k = generateRandomNumber("2", Sub(a, "1"));
                if (More(NSD(a, k), "1")) return false;
                string l = Pow(k, Div(Sub(a, "1"), "2"), a);
                string r = Legenre(k, a);
                if (Less(r, "0")) r = Add(r, a);
                if (!Equal(l, r)) return false;
            }
            return true;
        }

        public bool primeSoloveyShtrassen(string a, int count) //return prost(a)
        {
            if (Equal("0", Mod(a, "2"))) return false;
            for (int i = 0; i < count; ++i)
            {

                string k = generateRandomNumber("2", Sub(a, "1"));
                if (More(NSD(a, k), "1")) return false;
                string l = Pow(k, Div(Sub(a, "1"), "2"), a);
                string r = Legenre(k, a);
                if (Less(r, "0")) r = Add(r, a);
                if (!Equal(l, r)) return false;
            }
            return true;
        }

        public string generateRandomNumber(int length)
        {
            string res = "";
            res += rand.Next(1, 10);
            for (int i = 1; i < length; ++i)
                res += rand.Next(0, 10);

            return res;
        }

        public string generatePrimeNumber(int length)
        {
            while (true)
            {
                Console.Write('.');
                string res = generateRandomNumber(length);
                if (!simplePrimeTest(res)) continue;
                if (primeMillerRabin(res, 10)) return res;
            }
        }
        
        public string generatePrimeNumber(string a, string b)
        {
            int max = 1000;
            for (int i = 0; i < max; ++i)
            {
                string res = generateRandomNumber(a, b);
                if (!simplePrimeTest(res)) continue;
                if (primeMillerRabin(res, 10)) return res;
            }
            Console.WriteLine($"В iнтервалi [{a}, {b}] за {max} спроб просте число не знайдено");
            return generatePrimeNumber(b.Length);
        }

        public bool simplePrimeTest(string s)
        {
            int length = s.Length;
            if (int.Parse(s[length - 1].ToString()) % 2 == 0) return false;//2
            if (int.Parse(s[length - 1].ToString()) % 5 == 0) return false;//5

            //3
            int sum = 0;
            for (int i = 0; i < length; ++i)
                sum += int.Parse(s[i].ToString());
            if (sum % 3 == 0) return false;

            //11
            int sum1 = 0;
            for (int i = 0; i < length; i += 2)
                sum1 += int.Parse(s[i].ToString());
            int sum2 = 0;
            for (int i = 1; i < length; i += 2)
                sum2 += int.Parse(s[i].ToString());
            if ((sum1 - sum2) % 11 == 0) return false;

            return true;
        }

        public string ExtendedEuclideanAlgorithm(string e, string f)//e*x = 1 (mod f(n))
        {
            List<string> a = new List<string> { e };
            List<string> b = new List<string> { f };
            List<string> c = new List<string> { Div(e, f) };
            List<string> d = new List<string> { Mod(e, f) };
            int k = 0;
            while (!d[k].Equals("0"))
            {
                a.Add(b[k]);
                b.Add(d[k]);
                ++k;
                c.Add(Div(a[k], b[k]));
                d.Add(Mod(a[k], b[k]));
            }

            --k;
            string x = a[k];
            string x1 = "1";
            string y1 = c[k];
            while (k > 0)
            {
                --k;
                if (x.Equals(d[k]))
                {
                    x = a[k];
                    y1 = Add(y1, Mult(c[k], x1));
                }
                else
                {
                    x1 = Add(x1, Mult(c[k], y1));
                }
            }
            string resx = x1;
            if (!e.Equals(x))
            {
                resx = Mult("-1", y1);
            }
            if (Less(resx, "0"))
            {
                resx = Add(resx, f);
            }
            return resx;
        }
    
        public string genereteCoprimeNumberForX(string x)
        {
            string xSUB1 = Sub(x, "1");
            int max = 1000;
            for (int i = 0; i < max; ++i)
            {
                string res = generateRandomNumber("2", xSUB1);
                if (NSD(res, x).Equals("1")) return res;
            }
            Console.WriteLine($"В iнтервалi [2, {xSUB1}] за {max} спроб взаємно просте число не знайдено");
            return generatePrimeNumber(x.Length);
        }
    }
}
