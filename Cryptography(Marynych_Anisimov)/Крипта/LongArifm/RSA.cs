using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace LongArifm
{
    public class RSA
    {
        public string name = "unnamed";
        public string n;
        public string e;

        private string d;
        static private int NUMBER_LENGTH = 20;
        private int MAX_LENGTH = 3 * ((NUMBER_LENGTH - 1) / 3);
        LongCalculator calc = new LongCalculator();
        //173 символа
        private string allSymbols = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNMйцукенгшщзхїфівапролджєяґчсмитьбюЙЦУКЕНГШЩЗХЇФІВАПРОЛДЖЄЯҐЧСМИТЬБЮёъыэЁЪЭЫ1234567890`:;~!@#$%^&*()_+-=[]{}\\.\',\"<>/?|₴'№ \n\r\t";

        public RSA(string name1)
        {
            name = name1;
            Console.WriteLine($"New user {name}");
            string p = calc.generatePrimeNumber(NUMBER_LENGTH);
            Console.Write($".");
            string q = calc.generatePrimeNumber(NUMBER_LENGTH);
            Console.Write($".");
            n = calc.Mult(p, q);
            Console.Write($".");
            string fn = calc.Mult(calc.Sub(p, "1"), calc.Sub(q, "1"));
            Console.Write($".");
            p = q = "";
            Console.Write($".");
            e = calc.genereteCoprimeNumberForX(fn);
            Console.Write($".");
            d = calc.ExtendedEuclideanAlgorithm(e, fn);
            Console.Write($".");
            Console.WriteLine($"\nis ready\n");
        }
        public RSA()
        {
            Console.WriteLine($"New user {name}");
            string p = calc.generatePrimeNumber(NUMBER_LENGTH);
            Console.Write($".");
            string q = calc.generatePrimeNumber(NUMBER_LENGTH);
            Console.Write($".");
            n = calc.Mult(p, q);
            Console.Write($".");
            string fn = calc.Mult(calc.Sub(p, "1"), calc.Sub(q, "1"));
            Console.Write($".");
            p = q = "";
            Console.Write($".");
            e = calc.genereteCoprimeNumberForX(fn);
            Console.Write($".");
            d = calc.ExtendedEuclideanAlgorithm(e, fn);
            Console.Write($".");
            Console.WriteLine($"\nis ready");
        }

        public string EncryptMessageToSend(string signature1, string m1, string e1, string n1)
        {
            string res = "Signature: ";
            signature1 = MessageToNumber(signature1);
            while(signature1.Length > MAX_LENGTH)
            {
                res += Signature(signature1.Substring(0, MAX_LENGTH)) +"&";
                signature1 = signature1.Remove(0, MAX_LENGTH);
            }
            if (signature1.Length > 0)
            {
                res += Signature(signature1);
            }

            res += " Message: ";
            m1 = MessageToNumber(m1);
            while (m1.Length > MAX_LENGTH)
            {
                res += Encrypt(m1.Substring(0, MAX_LENGTH), e1, n1) + "&";
                m1 = m1.Remove(0, MAX_LENGTH);
            }
            if (m1.Length > 0)
            {
                res += Encrypt(m1, e1, n1);
            }

            return res;
        }

        public string DecryptMessage(string message, string e1, string n1)
        {
            string[] spl = message.Split(' ');
            string res = spl[0] + " ";
            string[] buf = spl[1].Split('&');
            foreach(string s in buf)
            {
                res += NumberToMessage(CheckSignature(s, e1, n1));
            }
            res += "\n" + spl[2] + " ";
            buf = spl[3].Split('&');
            foreach (string s in buf)
            {
                res += NumberToMessage(Decrypt(s));
            }
            return res;
        }

        public string Encrypt(string m1, string e1, string n1)
        {
            return calc.Pow(m1, e1, n1);
        }

        public string Decrypt(string c1)
        {
            return calc.Pow(c1, d, n);
        }

        public string Signature(string m)
        {
            return calc.Pow(m, d, n);
        }

        public string CheckSignature(string s, string e1, string n1)
        {
            return calc.Pow(s, e1, n1);
        }

        public string MessageToNumber(string m)
        {
            var ch = m.ToList();
            var allsym = allSymbols.ToList();
            string res = "";
            foreach (var c in ch)
            {
                res += (100 + allsym.IndexOf(c)).ToString();
            }

            return res;
        }

        public string NumberToMessage(string n)
        {
            var allsym = allSymbols.ToList();
            string res = "";
            while (n.Length > 0)
            {
                res += allsym[int.Parse(n.Substring(0, 3)) - 100];
                n = n.Substring(3);
            }

            return res;
        }
    }
}
