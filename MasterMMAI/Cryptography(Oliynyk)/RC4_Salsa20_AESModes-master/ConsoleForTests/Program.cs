using Lab2;
using System;
using System.Diagnostics;
using System.Text;

namespace ConsoleForTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //string key = "aaaaaaaabbbbbbbbccccccccdddddddd";
            //string input = "hello form Donbass children";

            //byte[] plain = Encoding.ASCII.GetBytes(input);
            ////RC4 encr = new RC4(Encoding.ASCII.GetBytes(key));
            ////byte[] chiphered = encr.Transform(plain);
            ////byte[] decoded = encr.Transform(chiphered);


            //Salsa20 salsa20 = new Salsa20(Encoding.ASCII.GetBytes(key), new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            //byte[] chiphered = salsa20.Transform(plain);
            //byte[] decoded = salsa20.Transform(chiphered);

            //Console.WriteLine(Encoding.ASCII.GetString(decoded));



            string rootpath = "C:\\Users\\Skaner\\Downloads\\";
            string inpFileName = "lect10-earley (2).ppt";

            FileChipherer chipherer = new FileChipherer(ChiphererAlgo.AES_CTR);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            chipherer.Encrypt(rootpath + inpFileName, rootpath + "Encr_" + inpFileName);
            stopwatch.Stop();

            Console.WriteLine($"\tSpent {stopwatch.ElapsedMilliseconds} ms to encrypt");
            stopwatch.Reset();

            stopwatch.Start();
            chipherer.Decrypt(rootpath + "Encr_" + inpFileName, rootpath + "Decr_" + inpFileName);
            stopwatch.Stop();

            Console.WriteLine($"\tSpent {stopwatch.ElapsedMilliseconds} ms to decrypt");

            Console.ReadLine();
        }
    }
}
