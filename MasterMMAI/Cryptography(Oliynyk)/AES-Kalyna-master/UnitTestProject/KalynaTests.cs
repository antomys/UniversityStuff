using Lab1;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject
{
    class KalynaTests
    {
        [TestCase(0)]
        public void ForwardAESTest(int index)
        {
            KalynaBlock input = new KalynaBlock(plain[index]);
            Kalyna kalyna = new Kalyna(keys[index]);
            KalynaBlock expected = new KalynaBlock(chipher[index]);

            KalynaBlock actual = kalyna.Encrypt(input);

            CollectionAssert.AreEqual(expected.Data, actual.Data);
        }

        [TestCase(0)]
        public void ReverseAESTest(int index)
        {
            KalynaBlock input = new KalynaBlock(chipher[index]);
            Kalyna kalyna = new Kalyna(keys[index]);
            KalynaBlock expected = new KalynaBlock(plain[index]);

            KalynaBlock actual = kalyna.Decrypt(input);

            CollectionAssert.AreEqual(expected.Data, actual.Data);
        }

        public byte[][] keys =
        {
            new byte[]
            {
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
            }
        };

        public byte[][] plain =
        {
            new byte []
            {
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F
            }
        };

        public byte[][] chipher =
        {
            new byte[]
            {
               0x64, 0xAD, 0x44, 0xC8, 0x9A, 0x73, 0x0A, 0xE4, 0x46, 0x10, 0xAE, 0xDC, 0xC3, 0x69, 0xD4, 0xFB
            }
        };
    }
}
