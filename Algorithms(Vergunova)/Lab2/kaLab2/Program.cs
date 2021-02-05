using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;

namespace kaLab2
{
    class Program
    {
        private const int MAX_LENGTH = 15; //макс довжина слів
        private const int n = 1000000; //к-сть вхідних команд
        private const int p = 31; //константа поліноміальної хеш-функції для хеш-таблиці
        private const int k = 7; //к-сть хеш-функцій для фільтру Блума
        private static uint[] pmas = new uint[MAX_LENGTH]; //масив степенів р для ф-ції хешування
        private static uint[,] pb = new uint[k, MAX_LENGTH]; //масив степенів р для ф-цій хешування фільтру Блума
        private const int m = 100000321; //розмір хеш-таблиці
        private const int mb = 10098865; //розмір фільтру Блума
        private static string LETTERS = "abcdefghijklmnopqrstuvwxyz   "; //доступні символи
        private const int MAX_COLLISION = 4; //генерована максимальна к-сть колізій n % MAX_COLLISION == 0

        private static char[] GENERATOR = new char[] { '+', '?', '+', '?', '-' }; //множина генерувальних команд
        private const int GENERATOR_MAX_NUMBER = 5; //доступна к-сть елементів із множини генерувальних команд

        private static void Main(string[] args)
        {
            List<string>[] table = new List<string>[m];

            GenerateInputFile("input.txt");
            FillHashTable(ref table, "input.txt", "log.txt", "error.txt");
            MakeHistogram(ref table, "hist.txt");
            ResultsOutput(ref table, "output.txt");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static int h(string x)
        {
            ulong res = 0;
            List<char> c = x.ToList();
            for (int i = 0; i < c.Count; ++i)
            {
                res = (res + (pmas[i] * (ulong)LETTERS.IndexOf(c[i]) % m)) % m;
            }
            return (int)res;
        }

        private static int[] hb(string x)
        {
            int[] heshs = new int[k];
            ulong res = 0;
            List<char> c = x.ToList();
            int count = c.Count;
            for (int j = 0; j < k; ++j)
            {
                res = 0;
                for (int i = 0; i < count; ++i)
                {
                    res = (res + (pb[j, i] * (ulong)LETTERS.IndexOf(c[i]) % mb)) % mb;
                }
                heshs[j] = (int)res;
            }
            return heshs;
        }

        private static void GenerateInputFile(string fname)
        {
            Console.WriteLine("generation step 0");
            List<char> c = LETTERS.ToList();
            int l = n / MAX_COLLISION;
            string[] sm = new string[l];
            Random rand = new Random();

            Console.WriteLine("generation step 1");
            for (int j = 0; j < MAX_LENGTH; ++j)
            {
                for (int i = 0; i < l; ++i)
                {
                    sm[i] += c[rand.Next(0, 29)];
                    sm[i] = sm[i].TrimEnd(' ');
                }
            }

            Console.WriteLine("generation step 2");
            StreamWriter sw = new StreamWriter(fname);
            for (int j = 0; j < MAX_COLLISION; ++j)
            {
                for (int i = 0; i < l; ++i)
                {
                    sw.Write(GENERATOR[rand.Next(GENERATOR_MAX_NUMBER)]);
                    sw.Write(' ');
                    sw.WriteLine(sm[i]);
                }
            }
            sw.WriteLine("##");
            sw.Close();

            Console.WriteLine("generation complete");
        }

        private static void FillHashTable(ref List<string>[] table, string fname, string logname, string errname)
        {
            FillPowersP();
            FillPowersPB();

            BitArray b = new BitArray(mb);
            byte[] bc = new byte[mb];
            int[] heshs = new int[k];

            string line = "";
            StreamReader sr = new StreamReader(fname);
            StreamWriter sw = new StreamWriter(logname);
            StreamWriter swerr = new StreamWriter(errname);

            Console.WriteLine("Working with hashtable");
            int count = 0;//лічильник для візуалізації процесу виконання програми
            while (true)
            {
                if (++count % 10000 == 0) Console.WriteLine(count / 10000);

                line = sr.ReadLine();
                if (line.Equals("##")) break;

                string[] spl = line.Split(' ');
                string x = " ";
                if (spl.Count() == 2)
                {
                    x = spl[1];
                }

                if (spl[0].Equals("+"))
                {
                    bool atht = AddToHashTable(ref table, x);
                    bool atb = AddToBloom(ref b, x);
                    bool atbc = AddToBloomCount(ref bc, x);
                    if (atht != atb || atht != atbc || atb != atbc) swerr.WriteLine("{4} \t ADD {0} {1} {2} \t {3}", atht, atb, atbc, x, count);

                    if (atht)
                    {
                        sw.Write("+ " + x);
                    }
                    else
                    {
                        sw.Write("! + " + x);
                    }

                    if (atb)
                    {
                        sw.Write(" B+ " + x);
                    }
                    else
                    {
                        sw.Write(" B ! + " + x);
                    }

                    if (atbc)
                    {
                        sw.Write(" BC+ " + x);
                    }
                    else
                    {
                        sw.Write(" B ! + " + x);
                    }

                    sw.WriteLine();
                }
                else
                {
                    if (spl[0].Equals("-"))
                    {
                        bool siht = SearchInHashTable(ref table, x);
                        bool sibc = SearchInBloomCount(ref bc, x);
                        if (siht != sibc) swerr.WriteLine("{3} \t SEARCH BEFORE REMOVE {0} {1} \t {2}", siht, sibc, x, count);

                        bool rfht = RemoveFromHashTable(ref table, x);
                        bool rfbc = RemoveFromBloomCount(ref bc, x);
                        if (rfht != rfbc)
                            swerr.WriteLine("{3} \t REMOVE {0} {1} \t {2}", rfht, rfbc, x, count);

                        if (rfht)
                        {
                            sw.Write("- " + x);
                        }
                        else
                        {
                            sw.Write("- ?- " + x);
                        }

                        if (rfbc)
                        {
                            sw.Write(" BC- " + x);
                        }
                        else
                        {
                            sw.Write(" BC- ?- " + x);
                        }

                        sw.WriteLine();
                    }
                    else
                    {
                        if (spl[0].Equals("?"))
                        {
                            bool siht = SearchInHashTable(ref table, x);
                            bool sib = SearchInBloom(ref b, x);
                            bool sibc = SearchInBloomCount(ref bc, x);
                            if (siht != sib || siht != sibc || sib != sibc) swerr.WriteLine("{4} \t SEARCH {0} {1} {2} \t {3}", siht, sib, sibc, x, count);

                            if (siht)
                            {
                                sw.Write("?+ " + x);
                            }
                            else
                            {
                                sw.Write("?- " + x);
                            }

                            if (sib)
                            {
                                sw.Write(" B?+ " + x);
                            }
                            else
                            {
                                sw.Write(" B?- " + x);
                            }

                            if (sibc)
                            {
                                sw.Write(" BC?+ " + x);
                            }
                            else
                            {
                                sw.Write(" BC?- " + x);
                            }

                            sw.WriteLine();
                        }
                    }
                }
            }
            sw.Close();
            sr.Close();
            swerr.Close();

            ResultsOutBloom(ref b, "outBloom.txt");
            ResultsOutBloomCount(ref bc, "outBloomCount.txt");
        }

        private static void MakeHistogram(ref List<string>[] table, string fname)
        {
            Console.WriteLine("Making a histogram");
            StreamWriter sw = new StreamWriter(fname);
            int q = 0;
            for (int i = 0; i < m; ++i)
            {
                if (table[i] != null)
                {
                    sw.WriteLine(q);
                    q = 0;
                    foreach (string s in table[i])
                    {
                        sw.Write(s + ' ');
                    }
                    sw.WriteLine("____________________________________");
                }
                else
                {
                    ++q;
                }
            }
            sw.WriteLine(q);
            sw.Close();
        }

        private static void ResultsOutBloom(ref BitArray b, string fname)
        {
            Console.WriteLine("Writing Bloom filter");
            StreamWriter sw = new StreamWriter(fname);
            int q = 0;
            for (int i = 0; i < mb; ++i)
            {
                if (b[i])
                {
                    if (q > 0)  sw.WriteLine("0   x" + q);
                    q = 0;
                    sw.WriteLine("1");
                }
                else
                {
                    ++q;
                }
            }
            if (q > 0) sw.WriteLine("0   x" + q);
            sw.Close();
        }

        private static void ResultsOutBloomCount(ref byte[] bc, string fname)
        {
            Console.WriteLine("Writing counting Bloom filter");
            StreamWriter sw = new StreamWriter(fname);
            int q = 0;
            for (int i = 0; i < mb; ++i)
            {
                if (bc[i] > 0)
                {
                    if (q > 0) sw.WriteLine("0   x" + q);
                    q = 0;
                    sw.WriteLine(bc[i]);
                }
                else
                {
                    ++q;
                }
            }
            if (q > 0) sw.WriteLine("0   x" + q);
            sw.Close();
        }

        private static void ResultsOutput(ref List<string>[] table, string fname)
        {
            Console.WriteLine("Results");
            StreamWriter sw = new StreamWriter(fname);
            for (int i = 0; i < m; ++i)
            {
                if (i % 1000000 == 0) Console.WriteLine(i / 1000000);
                if (table[i] != null)
                {
                    while (table[i].Count > 0)
                    {
                        string word = table[i][0];
                        while (table[i].Remove(word))
                        {
                            sw.Write(word + ' ');
                        }
                        sw.WriteLine();
                    }
                }
            }
            sw.Close();
        }

        private static void FillPowersP()
        {
            pmas[0] = 1;
            for (int i = 1; i < MAX_LENGTH; ++i)
            {
                pmas[i] = (pmas[i - 1] * p) % m;
            }
        }

        private static void FillPowersPB()
        {
            uint[] pi = new uint[] { 31, 47, 97, 113, 211, 71, 179 };

            for (int i = 0; i < k; ++i)
            {
                pb[i, 0] = 1;
                for (int j = 1; j < MAX_LENGTH; ++j)
                {
                    pb[i, j] = (pb[i, j - 1] * pi[i]) % mb;
                }
            }
        }

        private static bool AddToHashTable(ref List<string>[] table, string x)
        {
            try
            {
                int t = h(x);
                if (table[t] == null) table[t] = new List<string>();
                table[t].Add(x);
                if (table[t].Count == 0)
                    table[t] = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool RemoveFromHashTable(ref List<string>[] table, string x)
        {
            try
            {
                bool res = false;
                int t = h(x);
                if (table[t] == null) table[t] = new List<string>();
                if (table[t].Remove(x)) res = true;
                if (table[t].Count == 0)
                    table[t] = null;
                return res;
            }
            catch
            {
                return false;
            }
        }

        private static bool SearchInHashTable(ref List<string>[] table, string x)
        {
            try
            {
                int t = h(x);
                if (table[t] == null) table[t] = new List<string>();
                if (table[t].Contains(x))
                {
                    if (table[t].Count == 0)
                        table[t] = null;
                    return true;
                }
                else
                {
                    if (table[t].Count == 0)
                        table[t] = null;
                    return false;
                }
            }
            catch
            {
                int t = h(x);
                if (table[t] == null) table[t] = new List<string>();
                if (table[t].Count == 0)
                    table[t] = null;
                return false;
            }

        }

        private static bool AddToBloom(ref BitArray b, string x)
        {
            try
            {
                int[] heshs = hb(x);
                for (int i = 0; i < k; ++i)
                {
                    b[heshs[i]] = true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool AddToBloomCount(ref byte[] bc, string x)
        {
            try
            {
                int[] heshs = hb(x);
                for (int i = 0; i < k; ++i)
                {
                    ++bc[heshs[i]];
                    if (bc[heshs[i]] > 200) Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! >200");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool RemoveFromBloomCount(ref byte[] bc, string x)
        {
            try
            {
                if(SearchInBloomCount(ref bc, x))
                {
                    bool res = true;
                    int[] heshs = hb(x);
                    for (int i = 0; i < k; ++i)
                    {
                        if (bc[heshs[i]] == 0) Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! == 0");
                        --bc[heshs[i]];
                    }
                    return res;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static bool SearchInBloom(ref BitArray b, string x)
        {
            try
            {
                int[] heshs = hb(x);
                for (int i = 0; i < k; ++i)
                {
                    if (b[heshs[i]] == false)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool SearchInBloomCount(ref byte[] bc, string x)
        {
            try
            {
                int[] heshs = hb(x);
                for (int i = 0; i < k; ++i)
                {
                    if (bc[heshs[i]] == 0)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
