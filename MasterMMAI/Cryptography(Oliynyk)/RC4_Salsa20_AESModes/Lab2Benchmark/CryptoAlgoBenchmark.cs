using System.IO;
using BenchmarkDotNet.Attributes;
using Lab2;

namespace Lab2Benchmark
{
    [SimpleJob(launchCount: 1, warmupCount: 0, targetCount: 1)]
    [MemoryDiagnoser]
    public class CryptoAlgoBenchmark
    {
        string rootpath = Directory.GetCurrentDirectory();
        string inpFileName = "Lorem_ipsum.pdf";
        private FileChipherer chipherer;

        //RC4
        [Benchmark]
        public void Rc4Encrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.RC4);
            chipherer.Encrypt(Path.Combine(rootpath , inpFileName), Path.Combine(rootpath , "Encr_" + inpFileName));
        }

        [Benchmark]
        public void Rc4Decrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.RC4);
            chipherer.Decrypt(Path.Combine(rootpath , "Encr_" + inpFileName), Path.Combine(rootpath , "Decr_" + inpFileName));
        }


        //Salsa20
        [Benchmark]
        public void Salsa20Encrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.Salsa20);
            chipherer.Encrypt(Path.Combine(rootpath , inpFileName), Path.Combine(rootpath , "Encr_" + inpFileName));
        }

        [Benchmark]
        public void Salsa20Decrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.Salsa20);
            chipherer.Decrypt(Path.Combine(rootpath , "Encr_" + inpFileName), Path.Combine(rootpath , "Decr_" + inpFileName));
        }


        //AES_ECB
        [Benchmark]
        public void AES_ECBEncrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.AES_ECB);
            chipherer.Encrypt(Path.Combine(rootpath , inpFileName), Path.Combine(rootpath , "Encr_" + inpFileName));
        }

        [Benchmark]
        public void AES_ECBDecrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.AES_ECB);
            chipherer.Decrypt(Path.Combine(rootpath , "Encr_" + inpFileName), Path.Combine(rootpath , "Decr_" + inpFileName));
        }



        //AES_CBC
        [Benchmark]
        public void AES_CBCEncrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.AES_CBC);
            chipherer.Encrypt(Path.Combine(rootpath , inpFileName), Path.Combine(rootpath , "Encr_" + inpFileName));
        }

        [Benchmark]
        public void AES_CBCDecrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.AES_CBC);
            chipherer.Decrypt(Path.Combine(rootpath , "Encr_" + inpFileName), Path.Combine(rootpath , "Decr_" + inpFileName));
        }


        //AES_CFB
        [Benchmark]
        public void AES_CFBEncrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.AES_CFB);
            chipherer.Encrypt(Path.Combine(rootpath , inpFileName), Path.Combine(rootpath , "Encr_" + inpFileName));
        }

        [Benchmark]
        public void AES_CFBDecrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.AES_CFB);
            chipherer.Decrypt(Path.Combine(rootpath , "Encr_" + inpFileName), Path.Combine(rootpath , "Decr_" + inpFileName));
        }


        //AES_OFB
        [Benchmark]
        public void AES_OFBEncrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.AES_OFB);
            chipherer.Encrypt(Path.Combine(rootpath , inpFileName), Path.Combine(rootpath , "Encr_" + inpFileName));
        }

        [Benchmark]
        public void AES_OFBDecrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.AES_OFB);
            chipherer.Decrypt(Path.Combine(rootpath , "Encr_" + inpFileName), Path.Combine(rootpath , "Decr_" + inpFileName));
        }


        //AES_CTR
        [Benchmark]
        public void AES_CTREncrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.AES_CTR);
            chipherer.Encrypt(Path.Combine(rootpath , inpFileName), Path.Combine(rootpath , "Encr_" + inpFileName));
        }

        [Benchmark]
        public void AES_CTRDecrypt()
        {
            chipherer = new FileChipherer(ChiphererAlgo.AES_CTR);
            chipherer.Decrypt(Path.Combine(rootpath , "Encr_" + inpFileName), Path.Combine(rootpath , "Decr_" + inpFileName));
        }
    }
}
