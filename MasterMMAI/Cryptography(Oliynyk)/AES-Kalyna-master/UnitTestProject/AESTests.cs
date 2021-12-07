using NUnit.Framework;
using Lab1;

namespace UnitTestProject
{
    public class AESTests
    {
        [TestCase(0)]
        public void ForwardAESTest(int index)
        {
            AESBlock input = new AESBlock(plain[index]);
            AES aes = new AES(keys[index]);
            AESBlock expected = new AESBlock(chipher[index]);

            AESBlock actual = aes.Encrypt(input);

            CollectionAssert.AreEqual(expected.data, actual.data);
        }

        [TestCase(0)]
        public void ReverseAESTest(int index)
        {
            AESBlock input = new AESBlock(chipher[index]);
            AES aes = new AES(keys[index]);
            AESBlock expected = new AESBlock(plain[index]);

            AESBlock actual = aes.Decrypt(input);

            CollectionAssert.AreEqual(expected.data, actual.data);
        }

        public byte[][] keys =
        {
            new byte[]
            {
                0x2b, 0x7e, 0x15, 0x16, 0x28, 0xae, 0xd2, 0xa6, 0xab, 0xf7, 0x15, 0x88, 0x09, 0xcf, 0x4f, 0x3c
            }
        };

        public byte[][] plain =
        {
            new byte []
            {
                0x32, 0x43, 0xf6, 0xa8, 0x88, 0x5a, 0x30, 0x8d, 0x31, 0x31, 0x98, 0xa2, 0xe0, 0x37, 0x07, 0x34
            }
        };

        public byte[][] chipher =
        {
            new byte[]
            {
                0x39, 0x25, 0x84, 0x1d, 0x02, 0xdc, 0x09, 0xfb, 0xdc, 0x11, 0x85, 0x97, 0x19, 0x6a, 0x0b, 0x32
            }
        };
    }
}