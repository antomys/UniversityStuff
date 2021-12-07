using Lab3;
using Lab3.Kupyna;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleForTests
{
    class Program
    {
        static Random random = new Random();

        //static public void FindCollision(IHashFunc hashFunc, byte[] ex)
        //{
        //    byte[] inp = new byte[ex.Length];
        //    string hash = "";
        //    string exHash = Encoding.ASCII.GetString(hashFunc.CalcHash(ex));
        //    ulong iterationsCounter = 0;

        //    Stopwatch stopwatch = new Stopwatch();

        //    stopwatch.Start();

        //    while (!hash.Equals(exHash))
        //    {
        //        iterationsCounter++;
        //        RandomizeByteArr(inp);

        //        hash = Encoding.ASCII.GetString(hashFunc.CalcHash(inp));
        //    }

        //    stopwatch.Stop();

        //    Console.WriteLine($"Found in {iterationsCounter} iterations \n Time spent {stopwatch.ElapsedMilliseconds} ms");
        //}

        //static public void RandomizeByteArr(byte[] inp)
        //{
        //    for (int i = 0; i < inp.Length; ++i)
        //        inp[i] = (byte)(random.Next(254)+1);
        //}

        static void ProofOfWork(IHashFunc hashFunc ,string initStr, string matched)
        {
            string hash = "";
            ulong iterationsCounter = 0;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while (!hash.StartsWith(matched))
            {
                iterationsCounter++;

                hash = Encoding.ASCII.GetString(hashFunc.CalcHash(Encoding.ASCII.GetBytes(initStr + iterationsCounter.ToString())));
            }

            stopwatch.Stop();

            Console.WriteLine($"Found in {iterationsCounter} iterations \n Time spent {stopwatch.ElapsedMilliseconds} ms");
        }

        static void Main(string[] args)
        {

            IHashFunc kupyna = new Kupyna();
            IHashFunc sha256Func = new SHA256();

            Console.WriteLine("SHA256");
            ProofOfWork(sha256Func, "asdasd", "ab");
            Console.WriteLine();

            Console.WriteLine("Kupyna");
            ProofOfWork(kupyna, "asdasd", "ab");
            Console.WriteLine();
        }
    }
}
