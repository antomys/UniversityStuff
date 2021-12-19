using System;
using System.Diagnostics;
using System.IO;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            //AES
            AESBlock input1 = new AESBlock(new byte[] { 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 });
            Console.WriteLine("AES:");
            Console.WriteLine("\tInput block: " + input1.ToString());

            AES aes = new AES(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
            AESBlock chipheredBlock1 = aes.Encrypt(input1);
            Console.WriteLine("\tEncripted block: " + chipheredBlock1.ToString());

            AESBlock output1 = aes.Decrypt(chipheredBlock1);
            Console.WriteLine("\tDecripted vlock: " + output1.ToString());


            //Kalyna
            KalynaBlock input2 = new KalynaBlock(new byte[] { 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 });
            Console.WriteLine("Kalyna:");
            Console.WriteLine("\tInput block: " + input2.ToString());

            Kalyna kalyna = new Kalyna(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
            KalynaBlock chipheredBlock2 = kalyna.Encrypt(input2);
            Console.WriteLine("\tEncripted block: " + chipheredBlock2.ToString());

            KalynaBlock output2 = kalyna.Decrypt(chipheredBlock2);
            Console.WriteLine("\tDecripted vlock: " + output2.ToString());


            var rootpath = Directory.GetCurrentDirectory();
            var inpFileName = "Lorem_ipsum.pdf";

            //AES
            {
                Console.WriteLine("AES:");
                FileChipherer chipherer = new FileChipherer(ChiphererAlgo.AES, new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                chipherer.Encrypt(Path.Combine(rootpath , inpFileName), Path.Combine(rootpath , "Encr_" + inpFileName));
                stopwatch.Stop();

                Console.WriteLine($"\tSpent {stopwatch.ElapsedMilliseconds} ms to encrypt");
                stopwatch.Reset();

                stopwatch.Start();
                chipherer.Decrypt(Path.Combine(rootpath , "Encr_" + inpFileName),Path.Combine( rootpath , "Decr_" + inpFileName));
                stopwatch.Stop();

                Console.WriteLine($"\tSpent {stopwatch.ElapsedMilliseconds} ms to decrypt");
            }

            //Kalyna
            {
                Console.WriteLine("Kalyna:");
                FileChipherer chipherer = new FileChipherer(ChiphererAlgo.Kalyna, new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                chipherer.Encrypt(Path.Combine(rootpath , inpFileName), Path.Combine(rootpath , "Encr_" + inpFileName));
                stopwatch.Stop();

                Console.WriteLine($"\tSpent {stopwatch.ElapsedMilliseconds} ms to encrypt");
                stopwatch.Reset();

                stopwatch.Start();
                chipherer.Decrypt(Path.Combine(rootpath , "Encr_" + inpFileName), Path.Combine(rootpath , "Decr_" + inpFileName));
                stopwatch.Stop();

                Console.WriteLine($"\tSpent {stopwatch.ElapsedMilliseconds} ms to decrypt");
            }

            Console.ReadKey();
        }
    }
}
