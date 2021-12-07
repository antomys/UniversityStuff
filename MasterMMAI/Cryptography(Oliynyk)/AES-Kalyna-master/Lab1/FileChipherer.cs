using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lab1.Interfaces;

namespace Lab1
{
    public enum ChiphererAlgo
    {
        AES,
        Kalyna
    }

    public class FileChipherer
    {
        private readonly IChipherer chipherer;
        
        public FileChipherer(ChiphererAlgo algo, byte[] key)
        {
            switch (algo)
            {
                case ChiphererAlgo.AES:
                    chipherer = new AES(key);
                    break;
                case ChiphererAlgo.Kalyna:
                    chipherer = new Kalyna(key);
                    break;
                default:
                    return;
            }
        }

        public void Encrypt(string inputPath, string outputPath)
        {
            using (FileStream inputFile = File.OpenRead(inputPath))
            using (FileStream outputFile = File.Create(outputPath))
            {
                byte[] buff = new byte[16];
                while (inputFile.Read(buff, 0, buff.Length)>0)
                {
                    outputFile.Write(chipherer.Encrypt(buff), 0, buff.Length);
                }
            }
        }

        public void Decrypt(string inputPath, string outputPath)
        {
            using (FileStream inputFile = File.OpenRead(inputPath))
            using (FileStream outputFile = File.Create(outputPath))
            {
                byte[] buff = new byte[16];
                while (inputFile.Read(buff, 0, buff.Length) > 0)
                {
                    outputFile.Write(chipherer.Decrypt(buff), 0, buff.Length);
                }
            }
        }
    }
}
