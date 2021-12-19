using System;
using System.Diagnostics;
using System.Text;
using Lab4;

namespace Lab4ConsoleForTests
{
    class Program
    {
        static string TestWithKeyL(byte[] input, int keyLength)
        {
            Stopwatch st = new Stopwatch();

            RSA.keyLength = keyLength;

            (Key publicKey, Key privateKey) = RSA.GenerateRSAPair();

            st.Start();

            for (int i = 0; i < 5; ++i)
            {
                byte[] chipher = RSA.EncryptRSA(input, privateKey);

                byte[] plain = RSA.DecryptRSA(chipher, publicKey);
            }

            st.Stop();

            return $"Processed with key length={keyLength} for {st.ElapsedMilliseconds}ms";
        }


        static void Main(string[] args)
        {
            byte[] inp = Encoding.ASCII.GetBytes("Hello form Donbass children!!!");

            TestWithKeyL(inp, 1024);

            Console.WriteLine(TestWithKeyL(inp, 128));
            Console.WriteLine(TestWithKeyL(inp, 256));
            Console.WriteLine(TestWithKeyL(inp, 512));
            Console.WriteLine(TestWithKeyL(inp, 1024));

            //(Key publicKey, Key privateKey) = RSA.GenerateRSAPair();

            //byte[] chipher = RSA.EncryptRSA(inp, privateKey);

            //byte[] plain = RSA.DecryptRSA(chipher, publicKey);

            //Console.WriteLine("asdasd");

            Console.ReadKey();
        }
    }
}
