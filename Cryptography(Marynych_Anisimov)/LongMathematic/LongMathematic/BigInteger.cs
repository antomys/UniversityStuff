using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Numerics;

namespace LongMathematic
{
    internal class BigInteger
    {
        static BigInteger one = new BigInteger("1");
        static BigInteger zero = new BigInteger("0");
        private List<int> arr = new List<int>();
        private static int order = 6;
        private string f = "";

        public BigInteger(List<int> arr)
        {
            this.arr = arr;
            this.arr = this.Normalize(this.arr);
        }

        public BigInteger(List<int> arr, string flag)
        {
            this.arr = arr;
            f = flag;
            this.arr = this.Normalize(this.arr);
        }

        public BigInteger(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++) { this.arr.Add(arr[i]); }
            this.arr = this.Normalize(this.arr);
        }

        public BigInteger(string s)
        {
            if (s.Contains("-")) {
                f = "-";
                s = s.Substring(1, s.Length - 1);
            }
            int tempValue = 0;
            int tempOrder = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (tempOrder == order)
                {
                    arr.Add(tempValue);
                    tempValue = int.Parse(s[i].ToString());
                    tempOrder = 1;
                }
                else
                {
                    tempValue = tempValue + (int)Math.Pow(10, tempOrder) * int.Parse(s[i].ToString());
                    tempOrder++;
                }
            }
            if (tempOrder != 0)
            {
                arr.Add(tempValue);
            }
            this.arr = this.Normalize(this.arr);
        }

        private List<int> Normalize(List<int> arr)
        {
            while (arr.Count > 0 && arr[arr.Count - 1] == 0)
            {
                arr.RemoveAt(arr.Count - 1);
            }
            return arr;
        }

        public Boolean Equals(BigInteger value)
        {
            if (arr.Count != value.arr.Count) return false;
            for (int i = 0; i < arr.Count; i++)
            {
                if (!(arr[i] == value.arr[i])) return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            BigInteger target = (BigInteger)obj;
            if (target.arr.Count == arr.Count)
            {
                for (int i = 0; i < arr.Count; i++) { if (arr[i] != target.arr[i]) { return false; } }
            }
            else { return false; }
            return f == target.f;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            List<int> nullArray = new List<int>();
            nullArray.Add(0);
            BigInteger nullBI = new BigInteger(nullArray);
            if (this.Equals(nullBI)) return "0";
            else
            {
                StringBuilder ans = new StringBuilder();
                ans.Append(arr[arr.Count - 1].ToString());
                for (int i = arr.Count - 2; i >= 0; i--)
                {
                    string temp = arr[i].ToString();
                    int zeroCount = order - temp.Length;
                    for (int j = 0; j < zeroCount; j++) { ans.Append('0'); }
                    ans.Append(temp);
                }
                return f + ans.ToString();
            }
        }

        public BigInteger Add(BigInteger value)
        {
            int k = 0;
            string flag = "";
            int n = Math.Max(arr.Count, value.arr.Count);
            List<int> ans = new List<int>();
            BigInteger cthis = this;
            BigInteger cvalue = value;
            if (cthis.f.Contains("-"))
            {
                if (cvalue.f.Contains("-"))
                {
                    flag = "-";
                    cthis.f = "";
                    cvalue.f = "";
                }
                else
                {
                    cthis.f = "";
                    return cvalue.Subtract(cthis);
                }
            }
            else
            {
                if (cvalue.f.Contains("-")) {
                    cvalue.f = "";
                    return cthis.Subtract(cvalue);
                }
            }
            for (int i = 0; i < n; i++)
            {
                int tempA = (cthis.arr.Count > i) ? cthis.arr[i] : 0;
                int tempB = (cvalue.arr.Count > i) ? cvalue.arr[i] : 0;
                ans.Add(tempA + tempB + k);
                int size = Convert.ToString(ans[i]).Length;
                if (size > order)
                {
                    ans[i] -= (int)Math.Pow(10, order);
                    k = 1;
                }
                else
                {
                    k = 0;
                }
            }
            if (k == 1) ans.Add(k);
            BigInteger result = new BigInteger(ans, flag);
            return result;
        }
  
        public BigInteger Subtract(BigInteger value)
        {
            BigInteger cthis;
            BigInteger cvalue;
                cthis = this;
                cvalue = value;
            if (cthis.f.Contains("-"))
            {
                if (cvalue.f.Contains("-"))
                {
                    cvalue.f = "";
                    cthis.f = "";
                    return cvalue.Subtract(cthis);
                }
                else
                {
                    cthis.f = "";
                    BigInteger bigInteger = cthis.Add(cvalue);
                    bigInteger.f = "-";
                    return bigInteger;
                }
            }
            else
            {
                if (cvalue.f.Contains("-"))
                {
                    cvalue.f = "";
                    return cthis.Add(cvalue);
                }
            }
            bool sign = false;
            if (cthis.CompareTo(cvalue) == -1)
            {
                cthis = value;
                cvalue = this;
                sign = true;
            }

            int k = 0;
            int n = Math.Max(cthis.arr.Count, cvalue.arr.Count);
            //string flag = "";
            int tempA;
            int tempB;
            List<int> ans = new List<int>();

                for (int i = 0; i < n - 1; i++)
                {
                    tempA = (cthis.arr.Count > i) ? cthis.arr[i] : 0;
                    tempB = (cvalue.arr.Count > i) ? cvalue.arr[i] : 0;
                    if (tempA - tempB - k < 0)
                    {
                        ans.Add((int)Math.Pow(10, order) + tempA - tempB - k);
                        k = 1;
                    }
                    else
                    {
                        ans.Add(tempA - tempB - k);
                        k = 0;
                    }
                }

            while (ans.Count > 0 && ans[ans.Count - 1] == 0)
             {
                 ans.RemoveAt(ans.Count - 1);
             }
            if (n != 0)
            {
                tempA = (cthis.arr.Count == n) ? cthis.arr[n - 1] : 0;
                tempB = (cvalue.arr.Count == n) ? cvalue.arr[n - 1] : 0;
                ans.Add(tempA - tempB - k);
            }
            BigInteger answer = new BigInteger(ans);
            if (sign) answer.f = "-";
            return answer;
        }

        public BigInteger Multiply(int value)
        {
            int k = 0;
            List<int> ans = new List<int>();
            for (int i = 0; i < arr.Count; i++) {
                long temp = (long)arr[i] * (long)value + k;
                ans.Add((int)(temp % (int)Math.Pow(10, 6)));
                k = (int)(temp / (int)Math.Pow(10, 6));
            }
            ans.Add(k);
            return new BigInteger(Normalize(ans));
        }

        public BigInteger Multiply(BigInteger value)
        {
            BigInteger cvalue = value;
            BigInteger cthis = this;
            string flag = "";
            if (cthis.f.Contains("-"))
            {
                if (cvalue.f.Contains("-"))
                {
                    cvalue.f = "";
                    cthis.f = "";
                    flag = "";
                }
                else
                {
                    cthis.f = "";
                    flag = "-";
                }
            }
            else
            {
                if (cvalue.f.Contains("-"))
                {
                    flag = "-";
                }
            }
            BigInteger ans = new BigInteger("0");
            for (int i = 0; i < arr.Count; i++)
                { BigInteger temp = cvalue.Multiply(arr[i]);
                    for (int j = 0; j < i; j++)
                    { temp.arr.Insert(0,0); } ans = ans.Add(temp);
                }
            ans.f = flag;
            return ans; 
        }

        public int CompareTo(BigInteger other)
        {
            BigInteger cother = other;
            BigInteger cthis = this;
            if (cthis.f.Contains("-") && !(cother.f.Contains("-"))) return -1;
            if (!(cthis.f.Contains("-")) && cother.f.Contains("-")) return 1;
            int ans = 0;
            if (cthis.arr.Count == cother.arr.Count)
            {
                for (int i = 0; i < cthis.arr.Count; i++)
                {
                    if (cthis.arr[i] > cother.arr[i])
                    {
                        ans = 1;
                    }

                    if (cthis.arr[i] < cother.arr[i]) { ans = -1; }
                }
            }
            else
            {
                if (cthis.arr.Count > cother.arr.Count)
                {
                    ans = 1;
                }
                if (cthis.arr.Count < cother.arr.Count) { ans = -1; }
            }
            if (cthis.f.Contains("-") && cother.f.Contains("-")) return (-1) * ans;
            return ans;
        }

        public BigInteger Divide2(int v, out int r)
        {
            int[] ans = new int[arr.Count];
            r = 0;
            int j = arr.Count - 1;
            while (j >= 0)
            {
                long cur = (long)(r) * (long)(Math.Pow(10, 6)) + arr[j];
                ans[j] = (int)(cur / v);
                r = (int)(cur % v);
                j--;
            }
            if (this.f.Contains("-"))
            {
                if (v < 0)
                {
                    ans[0] = -ans[0] + 1;
                    r = -v - r;
                }
                else
                {
                    ans[ans.Length - 1]++;
                    BigInteger result = new BigInteger(ans);
                    result.f = "-";
                    r = v - r;
                    return result;
                }
            }
            BigInteger result1 = new BigInteger(ans);
            return result1;
        }

        public static int Divide(out BigInteger q, out BigInteger r, BigInteger u, BigInteger v)
        {
            BigInteger cu = u;
            BigInteger cv = v;
            BigInteger ccu = cu;
            BigInteger ccv = cv;

            string usign = u.f;
            string vsign = v.f;

            ccu.f = "";
            ccv.f = "";
            BigInteger result = new BigInteger("0");

            if (ccu.CompareTo(ccv) == -1)
            {
                if (!cu.f.Contains("-") && !cv.f.Contains("-"))
                {
                    List<int> nullArray = new List<int>();
                    nullArray.Add(0);
                    BigInteger nullBI = new BigInteger(nullArray);
                    q = nullBI;
                    r = ccu;
                }
                else
                {
                    List<int> nullArray = new List<int>();
                    nullArray.Add(0);
                    BigInteger nullBI = new BigInteger(nullArray);
                    q = nullBI;
                    r = ccu.Subtract(ccv);
                }
                return 0;
            }

            int n = cv.arr.Count;
            int m = cu.arr.Count - n;
            int[] tempArray = new int[m + 1];
            tempArray[m] = 1;
            result = new BigInteger(tempArray);

            int d = (int)(Math.Pow(10, 6) / (cv.arr[n - 1] + 1));
            cu = cu.Multiply(d);
            cv = cv.Multiply(d);
            if (cu.arr.Count == n + m)
            {
                cu.arr.Add(0);
            }
            int j = m;
            while (j >= 0)
            {
                long cur = (long)(cu.arr[j + n]) * (long)(Math.Pow(10, 6)) + cu.arr[j + n - 1];
                int tempq = (int)(cur / cv.arr[n - 1]);
                int tempr = (int)(cur % cv.arr[n - 1]);
                do
                {
                    if (tempq == Math.Pow(10, 6) || (long)tempq * (long)cv.arr[n - 2] > (long)Math.Pow(10, 6) * (long)tempr + cu.arr[j + n - 2])
                    {
                        tempq--;
                        tempr += cv.arr[n - 1];
                    }
                    else
                    {
                        break;
                    }
                }
                while (tempr < Math.Pow(10, 6));
                BigInteger u2 = new BigInteger(cu.arr.GetRange(j, n + 1));
                u2.f = "";
                u2 = u2.Subtract(cv.Multiply(tempq));
                bool flag = false;
                if (u2.f.Contains("-"))
                {
                    flag = true;
                    List<int> bn = new List<int>();
                    for (int i = 0; i <= n; i++)
                    {
                        bn.Add(0);
                    }
                    bn.Add(1);
                    u2.f = "";
                    u2 = new BigInteger(bn).Subtract(u2);
                }
                result.arr[j] = tempq;
                if (flag)
                {
                    result.arr[j]--;
                    u2 = u2.Add(cv);
                    if (u2.arr.Count > n + j) u2.arr.RemoveAt(n + j);

                }
                for (int h = j; h < j + n; h++)
                {
                    if (h - j >= u2.arr.Count)
                    {
                        cu.arr[h] = 0;
                    }
                    else
                    {
                        cu.arr[h] = u2.arr[h - j];
                    }
                }
                j--;
            }
            result.arr = result.Normalize(result.arr);
            int unusedR = 0;
            r = new BigInteger(cu.arr.GetRange(0, n));
            r.f = "";
            r = r.Divide2(d, out unusedR);
            if (usign.Contains("-"))
            {
                if (vsign.Contains("-"))
                {
                    result.arr[0]++;
                    r.f =  "-";
                }
                else
                {
                    result.arr[0]++;
                    r = cv.Subtract(r);
                }
            }
            else
            {
                if (vsign.Contains("-"))
                {
                    cv.f = "";
                    r = cv.Subtract(r);
                    r.f = "-";
                }
            }
            q = result;
            q.f = "-";
            if ((usign.Contains("-") && vsign.Contains("-")) || (!usign.Contains("-") && !vsign.Contains("-")))
           {
                q.f = "";
            }
            return 0;
        }

        public BigInteger Pow(int n, string mod)
        {
            BigInteger q = this;
            int r;
            string flag = "";
            if (q.f.Contains("-") && (n % 2 != 0)) flag = "-";
            while (n - 1 > 0)
            {
                q = q.Multiply(this);
                if (!mod.Equals(""))
                {
                    q.Divide2(Int32.Parse(mod), out r);
                    q = new BigInteger(r.ToString());
                }
                n--;
            }
           /* q.Divide2(Int32.Parse(mod), out r);
            q = new BigInteger(r.ToString());*/
            q.f = flag;
            return q;
        }
        public static BigInteger Sqrt(BigInteger a)
        {
            return GeronFunction(a, new BigInteger("1"), 1, 1000);
        }
        private static BigInteger GeronFunction(BigInteger a, BigInteger b, int shag, int n)
        {
            BigInteger ans = new BigInteger("1");
            BigInteger one = new BigInteger("1");
            int r = 0;
            ans = (b.Add(a.Div(b)).Divide2(2, out r));
            if (shag >= n || ans.Subtract(b).CompareTo(one) == 0)
            {
                return ans;
            }
            return GeronFunction(a, ans, shag + 1, n);
        }

        public static BigInteger SolveSystem(List<int> a, List<int> p)
        {
            BigInteger result = new BigInteger("0");
            int M = 1;
            for (int i = 0; i < a.Count(); i++)
            {
                M *= p[i];
            }
            List<int> ArrM = new List<int>();
            for (int i = 0; i < a.Count(); i++)
            {
                ArrM.Add(M/p[i]);
            }
            List<int> ArrM1 = new List<int>();
            for (int i = 0; i < a.Count(); i++)
            {
                int element = result.findR(ArrM[i], p[i]);
                ArrM1.Add(element);
            }
            int res = 0;
            for (int i = 0; i < a.Count(); i++)
            {            
                BigInteger z1 = new BigInteger(a[i].ToString());

                BigInteger z2 = new BigInteger(ArrM[i].ToString());
            
                BigInteger z3 = new BigInteger(ArrM1[i].ToString());

                result = result.Add(z1.Multiply(z2.Multiply(z3)));
            }
            result.Divide2(M, out res);
            List<int> finalArray = new List<int>();
            finalArray.Add(res);
            result = new BigInteger(finalArray);

            return result;
        }

        private int gcd(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            int x1, y1;
            int d = gcd(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }

        private int findR(int a, int m)
        {
            int x, y;
            int g = gcd(a, m, out x, out y);
            if (g != 1)
                return 0;
            else
            {
                x = (x % m + m) % m;
                return x;
            }
        }

        public BigInteger Pow2(BigInteger k, BigInteger n)
        {
            BigInteger a = new BigInteger(arr);
            BigInteger b = new BigInteger("1");
            while (k.CompareTo(new BigInteger("0")) > 0)
            {

                int r = 0;
                BigInteger q = k.Divide2(2, out r);
                if (r == 0)
                {
                    k = q;
                    a = a.Multiply(a).Mod(n);
                }
                else
                {
                    k = k.Subtract(new BigInteger("1"));
                    b = b.Multiply(a).Mod(n);
                }
            }
            return b;
        }

        public BigInteger Pow3(BigInteger k)
        {
            BigInteger a = new BigInteger(arr);
            BigInteger b = new BigInteger("1");
            while (k.CompareTo(new BigInteger("0")) > 0)
            {

                int r = 0;
                BigInteger q = k.Divide2(2, out r);
                if (r == 0)
                {
                    k = q;
                    a = a.Multiply(a);
                }
                else
                {
                    k = k.Subtract(new BigInteger("1"));
                    b = b.Multiply(a);
                }
            }
            return b;
        }

        public BigInteger Mod(BigInteger v)
        {
            BigInteger q;
            BigInteger r;
            BigInteger cthis = this;
            BigInteger cv = v;
            if (cthis.CompareTo(cv) > -1)
            {
                if (v.arr.Count == 1)
                {
                    int tempr = 0;
                    this.Divide2(v.arr[0], out tempr);
                    return new BigInteger(tempr.ToString());
                }
                Divide(out q, out r, this, v);
                return r;
            }
            else
            {
                return this;
            }
        }

        public BigInteger Div(BigInteger v)
        {
            BigInteger q;
            BigInteger r;
            if (this.CompareTo(v) > -1)
            {
                if (v.arr.Count == 1)
                {
                    int tempr = 0;
                    return this.Divide2(v.arr[0], out tempr);
                }
                Divide(out q, out r, this, v);
            }
            else
            {
                return new BigInteger("0");
            }
            return q;
        }

        public BigInteger Generate(BigInteger n)
        {
            BigInteger One = new BigInteger("1");
            BigInteger maxBigInteger = n.Subtract(One);
            BigInteger bigBase = new BigInteger(order.ToString());
            Random r = new Random();
            List<int> arr = new List<int>(maxBigInteger.arr.Count);
            bool flag = true;
            for (int i = maxBigInteger.arr.Count - 1; i >= 0; i--)
            {
                int temp;
                if (flag)
                {
                    temp = r.Next(maxBigInteger.arr[i] + 1);
                    flag = maxBigInteger.arr[i] == temp;
                }
                else
                {
                    temp = r.Next(order);
                }
                arr.Add(temp);
            }
            arr.Reverse();
            BigInteger result = new BigInteger(arr);
            return new BigInteger(arr);
        }

        private bool Witness(BigInteger a, BigInteger n)
        {
            BigInteger Zero = new BigInteger("0");
            BigInteger One = new BigInteger("1");
            BigInteger u = n.Subtract(One);
            int t = 0;
            while (u.Mod(new BigInteger("2")).Equals(Zero))
            {
                t++;
                u = u.Div(new BigInteger("2"));
            }
            BigInteger[] x = new BigInteger[t + 1];
            x[0] = a.Pow2(u, n);
            for (int i = 1; i <= t; i++) {
                x[i] = x[i - 1].Pow2(new BigInteger("2"), n);
                if (x[i].Equals((object)One) && !x[i - 1].Equals((object)One) && !x[i - 1].Equals((object)n.Subtract(One)))
                { return true; }
            }
            if (!x[t].Equals((object)One)) {
                return true;
            }
            return false;
        }

        public bool IsPrimeMillerRabin(BigInteger n, int s)
        {
            BigInteger pred = new BigInteger("0");
            for (int j = 0; j < s; j++)
            {
                BigInteger a = Generate(n);
                while (a.Equals((object)pred)) { a = Generate(n); }
                if (Witness(a, n)) { return false; }
            }
            return true;
        }


        public List<BigInteger> Factorization2(BigInteger a)
        {
            BigInteger Zero = new BigInteger("0");
            if (IsPrimeMillerRabin(a, 20))
            {
                return new List<BigInteger>() { a };
            }
            List<BigInteger> ans = new List<BigInteger>();
            BigInteger b = new BigInteger(a.ToString());
            for (int i = 2; i < 100; i++)
            {
                BigInteger temp = new BigInteger(i.ToString());
                if (b.Mod(temp).Equals((object)Zero))
                {
                    ans.AddRange(Factorization2(temp));
                    b = b.Div(temp);
                    ans.AddRange(Factorization2(b));
                    return ans;
                }
            }
            return Factorization(a);
        }

        public List<BigInteger> Factorization(BigInteger a)
        {
            List<BigInteger> ans = new List<BigInteger>();
            BigInteger temp = new BigInteger(a.ToString());
            while (!IsPrimeMillerRabin(temp, 20))
            {
                BigInteger temp2 = PollardRho(temp);
                if (temp.Equals((object)temp2))
                {
                    continue;
                }
                if (IsPrimeMillerRabin(temp2, 20))
                {
                    ans.Add(temp2);
                }
                else
                {
                    ans.AddRange(Factorization(temp2));
                }
                temp = temp.Div(temp2);
            }
            ans.Add(temp);
            return ans;
        }


        public BigInteger PollardRho(BigInteger n)
        {
            BigInteger One = new BigInteger("1");
            int i = 1;
            BigInteger x = Generate(n.Subtract(new BigInteger("2")));
            BigInteger y = new BigInteger(x.ToString());
            int k = 2;
            while (true)
            {
                i++;
                if (i > 10000)
                {
                    return n;
                }
                x = ((((x.Multiply(x)).Subtract(One))).Add(n)).Mod(n);
                BigInteger temp = y.Subtract(x);
                temp.f = "";
                BigInteger d = GCD(temp, n);
                if (!d.Equals((object)One) && !d.Equals((object)n))
                {
                    return d;
                }
                if (i == k)
                {
                    y = x;
                    k = 2 * k;
                }
            }
        }
         
        public BigInteger GCD(BigInteger a, BigInteger b)
        {
            BigInteger Zero = new BigInteger("0");
            if (b.Equals((object)Zero))
            {
                return a;
            }
            else
            {
                /* BigInteger r;
                 BigInteger q;
                 int result = Divide(out q, out r, a, b);*/
                if (b.CompareTo(new BigInteger("999999")) == -1)
                {
                    int r;
                    BigInteger q = a.Divide2(int.Parse(b.ToString()), out r);
                    BigInteger rb = new BigInteger(r.ToString());
                    return GCD(b, rb);
                }
                else
                {
                    BigInteger rc;
                    BigInteger q;
                    int result = Divide(out q, out rc, a, b);
                    return GCD(b, rc);
                }
            }
        }

        public BigInteger Solve(BigInteger a, BigInteger b, BigInteger m)
        {
            BigInteger n = BigInteger.Sqrt(m);
            n = n.Add(new BigInteger("1"));
            List<List<BigInteger>> vals = new List<List<BigInteger>>();
            //Dictionary<string, BigInteger> vals = new Dictionary<string, BigInteger>();
            BigInteger i = n;
           
            while (i.CompareTo(new BigInteger("1")) != -1)
            {
                i = i.Subtract(new BigInteger("1"));
                List<BigInteger> list = new List<BigInteger>();
                list.Add(a.Pow2(i.Multiply(n), m));
                list.Add(i);
                vals.Add(list);
            }
            i = new BigInteger("0");
               while (i.CompareTo(n) != 1)
               {
                   i = i.Add(new BigInteger("1"));
                   BigInteger cur = ((a.Pow2(i, m)).Multiply(b)).Mod(m);
                for (int l = 0; l < vals.Count; l++) {
                    if (vals[l].Contains(cur))
                    {
                        int k;
                        for (k = 0; k < vals.Count(); k++)
                        {
                            if (vals[k][0].Equals((object)cur)) break;
                        }
                        BigInteger ans = (vals[k][1].Multiply(n)).Subtract(i);
                        if (ans.CompareTo(m) == -1)
                        {
                            return ans;
                        }
                        break;
                    }
                }
               }
               BigInteger result = new BigInteger("1");
               result.f = "-";
               return result;         
        }

        public BigInteger phi(BigInteger n)
        {
            BigInteger result = n;
            BigInteger i = new BigInteger("2");
            while ((i.Multiply(i)).CompareTo(n) != 1)
            {
                if (n.Mod(i).CompareTo(new BigInteger("0")) == 0)
                {
                    while (n.Mod(i).CompareTo(new BigInteger("0")) == 0)
                    {
                        n = n.Div(i);
                    }
                    result = result.Subtract(result.Div(i));

                }
                i = i.Add(new BigInteger("1"));
            }
            if (n.CompareTo(new BigInteger("1")) == 1)
            {
                result = result.Subtract(result.Div(n));
            }
            return result;
        }

        public int Mobius(BigInteger n)
        {
            BigInteger c = new BigInteger("0");
            BigInteger s = BigInteger.Sqrt(n);
            BigInteger i = new BigInteger("2");
            while (i.CompareTo(s) != 1)
            {
                int zer = 0;
                while (n.Mod(i).CompareTo(new BigInteger("0")) == 0)
                {
                    n = n.Div(i);
                    c = c.Add(new BigInteger("1"));
                    zer++;
                    if (zer >= 2)
                    {
                        return 0;
                    }
                }
                i = i.Add(new BigInteger("1"));
            }
            if (n.CompareTo(new BigInteger("1"))==1)
            {
                c = c.Add(new BigInteger("1"));
            }
            if (c.Mod(new BigInteger("2")).CompareTo(new BigInteger("1")) == 0)
            {
                return -1;
            }
            return 1;
        }

        public int Legan (BigInteger n, BigInteger p)
        {
            int res = -1;
            if (n.Mod(p).CompareTo(new BigInteger("0")) == 0) return 0;
            BigInteger b = p.Subtract(new BigInteger("1")).Div(new BigInteger("2"));
            BigInteger resultPow;
            resultPow = n.Pow3(b);
            resultPow = resultPow.Mod(p);
            if (resultPow.CompareTo(new BigInteger("1")) == 0) res = 1;
            return res;
        }

        public int Jacobi(BigInteger a, BigInteger b)
        {
            BigInteger g = new BigInteger("0");
            //assert(odd(b));
            if (a.CompareTo(b) != -1) a = a.Mod(b); 
            if (a.CompareTo(new BigInteger("0")) == 0) return 0;
            if (a.CompareTo(new BigInteger("1")) == 0) return 1;
            if (a.CompareTo(new BigInteger("0")) == -1)
                if ((b.Subtract(new BigInteger("l"))).Div(new BigInteger("2")).Mod(new BigInteger("2")).CompareTo(new BigInteger("0")) == 0)
                {
                    BigInteger result = a;
                    result.f = "-";
                    return Jacobi(result, b);
                }
                else
                {
                    BigInteger result = a;
                    result.f = "-";
                    return -Jacobi(result, b);
                }
            if (a.Mod(new BigInteger("2")).CompareTo(new BigInteger("0")) == 0)
                if (((b.Multiply(b).Subtract(new BigInteger("1"))).Div(new BigInteger("8"))).Mod(new BigInteger("2")).CompareTo(new BigInteger("0")) == 0)
                    return Jacobi(a.Div(new BigInteger("2")), b);
                else
                {
                    return -Jacobi(a.Div(new BigInteger ("2")), b); 
                }
            g = GCD(a, b);
            if (g.CompareTo(a) == 0) 
                return 0; 
            else if (g.CompareTo(new BigInteger("1")) != 0)
                return Jacobi(g, b)*(Jacobi(a.Div(g), b)); 
            else if (((a.Subtract(new BigInteger("1"))).Multiply((b.Subtract(new BigInteger("1")))).Div(new BigInteger("4"))).Mod(new BigInteger("2")).CompareTo(new BigInteger("0")) == 0)
                return Jacobi(b, a); 
            else
                return -Jacobi(b, a);

        }

        public BigInteger Generate(BigInteger a, BigInteger b)
        {
            return Generate(b.Subtract(a)).Add(a);
        }

        public BigInteger GeneratePrime(BigInteger a, BigInteger b)
        {
            int i = 0;
            BigInteger last = new BigInteger("1");
            while (i < 10000)
            {
                BigInteger temp = Generate(a, b);
                if (IsPrimeMillerRabin(temp, 100))
                { return temp; }
                else { last = temp; i++; }
            }
            return null;//не нашли 
        }
        public List<BigInteger> Cipollo(BigInteger n, BigInteger p)
        {
            List<BigInteger> res = new List<BigInteger>();
            if (Legan(n, p) != 1)
            {
                res.Add(p);
                return res;
            }
            BigInteger one = new BigInteger("1");
            BigInteger two = new BigInteger("2");

            BigInteger sqrt = Sqrt(p).Add(one);
            BigInteger a = sqrt.Add(one);
            BigInteger y = a.Pow3(two).Subtract(n);
            //var y = new LongNumb("4");
            while (Legan(y, p) != -1)
            {
                a = a.Add(one);
                y = a.Pow3(two).Subtract(n);
            }
            BigInteger sum = new BigInteger("0");
            BigInteger pow = p.Add(one).Div(two);
            BigInteger k = pow;
            BigInteger sq = p.Pow3(two);

            while (k.f.Equals(""))
            {

                BigInteger n_k = pow.Subtract(k);
                BigInteger mult = a.Pow2(k, sq).Multiply(y.Pow2(n_k.Div(two), sq));
                BigInteger mult_ = mult.Mod(sq);
                BigInteger coef1 = Factorial(n_k, pow);
                BigInteger coef2 = Factorial(k);
                BigInteger coef = coef1.Div(coef2);
                BigInteger coeff = coef.Mod(sq);

                k = k.Subtract(two);
                sum = sum.Add(mult_.Multiply(coeff)).Mod(sq);

            }

            BigInteger residue = sum.Mod(sq).Mod(p);
            res.Add(residue);
            res.Add(p.Subtract(residue));
            return res;
        }
        private BigInteger GeneratorStringVer(BigInteger a, BigInteger b, int num)
        {
            
            string _numbers = "0123456789";
            Random random = new Random();
            StringBuilder builder = new StringBuilder(num);
            string numberAsString = "";
            //uint numberAsNumber = 0;
            for (var i = 0; i < num; i++)
            {
                builder.Append(_numbers[random.Next(0, _numbers.Length)]);
            }
            numberAsString = builder.ToString();
            BigInteger res = new BigInteger(numberAsString);

            //numberAsNumber = uint.Parse(numberAsString);
            return res;
        }
        

        public static BigInteger Factorial(BigInteger x1, BigInteger x2)
        {
            BigInteger one = new BigInteger("1");
            if (x1.CompareTo(x2) == 0)
            {
                return one;
            }

            BigInteger x = x1.Add(one);
            BigInteger plusOne = x.Add(one);
            BigInteger ans = x;
            while (plusOne.CompareTo(x2) != 1)
            {
                ans = ans.Multiply(plusOne);
                plusOne = plusOne.Add(one);
            }

            return ans;
        }

        public static BigInteger Factorial(BigInteger x1)
        {
            BigInteger one = new BigInteger("1");
            BigInteger x = new BigInteger("1");
            BigInteger ans = new BigInteger("1");
            BigInteger plusOne = x.Add(one);
            while (plusOne.CompareTo(x1) != 1)
            {
                ans = ans.Multiply(plusOne);
                plusOne = plusOne.Add(one);
            }

            return ans;
        }

        public List<BigInteger> ElGamal(BigInteger message)
        //public BigInteger ElGamal(BigInteger message)
        {
            List<BigInteger> res = new List<BigInteger>();
            BigInteger p = GeneratePrime(new BigInteger("7575"), new BigInteger("858657567667"));
            res.Add(p);
            BigInteger g = findPrimitiveRootModule(p);
            res.Add(g);
            BigInteger x = Generate(new BigInteger("1"), p.Subtract(new BigInteger("1")));
            res.Add(x);
            BigInteger y = g.Pow2(x, p);
            res.Add(y);
            bool t = true;
            while (t)
            {
                if (message.CompareTo(p) == -1)
                {
                    t = false;
                }
                else
                {
                    res.Add(new BigInteger("0"));
                    return res;
                }
            }
                BigInteger k = Generate(new BigInteger("1"), p.Subtract(new BigInteger("1")));
            res.Add(g.Pow2(k, p));
            res.Add(y.Pow2(k, p));
            return res;
        }

        public BigInteger findPrimitiveRootModule(BigInteger number)
        {
            List<BigInteger> factors = Factorization(number.Subtract(new BigInteger("1")));
            for (BigInteger i = new BigInteger("2"); i.CompareTo(number) == -1; i = i.Add(new BigInteger("1")))
            {
                bool p = false;
                foreach (BigInteger j in factors)
                {
                    if(i.Pow2(number.Subtract(new BigInteger("1")).Div(j), number).CompareTo(new BigInteger("1")) == 0)
                    {
                        p = true;
                    }
                }
                if (p)
                    continue;
                return i;
            }
            return new BigInteger("1");
        }
    }
}