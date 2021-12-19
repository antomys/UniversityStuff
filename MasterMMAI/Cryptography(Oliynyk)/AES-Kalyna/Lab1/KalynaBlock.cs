using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Lab1
{
    public class KalynaBlock
    {
        private readonly int BLOCK_SIZE = (128 / 8);

        public List<byte> Data { get; set; } = new List<byte>();

        public KalynaBlock() 
        {
            Data = new List<byte> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public KalynaBlock(byte[] data)
        {
            Data = new List<byte>();
            for (int i = 0; i < BLOCK_SIZE; ++i)
                Data.Add(data[i]);
        }

        public KalynaBlock(KalynaBlock block)
        {
            Data = new List<byte>(block.Data);
        }

        public void AddRoundKey(KalynaBlock key)
        {
            const int n = 8;
            for (var i = 0; i < 16; i += n)
            {
                var dataBi = new BigInteger(Data.Where((d, idx) => i <= idx && idx < i + n).ToArray());
                dataBi += new BigInteger(key.Data.Where((d, idx) => i <= idx && idx < i + n).ToArray());
                var newData = dataBi.ToByteArray();
                for (var j = 0; j < n; j++)
                    Data[i + j] = j < newData.Length ? newData[j] : (byte)0;
            }
        }

        public void SubRoundKey(KalynaBlock key)
        {
            const int n = 8;
            for (var i = 0; i < 16; i += n)
            {
                var dataBi = new BigInteger(Data.Where((d, idx) => i <= idx && idx < i + n).ToArray());
                dataBi -= new BigInteger(key.Data.Where((d, idx) => i <= idx && idx < i + n).ToArray());
                var newData = dataBi.ToByteArray();
                for (var j = 0; j < n; j++)
                    Data[i + j] = j < newData.Length ? newData[j] : (byte)0;
            }
        }

        public void RotateRight(int i)
        {
            var bi = new BigInteger(Data.ToArray());
            bi = (bi >> i % 128) + (bi << (128 - i % 128));
            Data = new List<byte>(bi.ToByteArray().Where((t, idx) => idx < 16));
        }

        public void RotateLeft(int i)
        {
            var bi = new BigInteger(Data.ToArray());
            bi = (bi << i % 128) + (bi >> (128 - i % 128));
            Data = new List<byte>(bi.ToByteArray().Where((t, idx) => idx < 16));
        }

        public void ShiftLeft(int i)
        {
            var bi = new BigInteger(Data.ToArray());
            bi <<= i;
            Data = new List<byte>(bi.ToByteArray());
        }

        public void SubBytes(byte[][][] table)
        {
            var trump = 0;
            for (var i = Data.Count - 1; 0 <= i; --i)
            {
                var d = Data[i];
                var upper = (d & 0xF0) >> 4;
                var lower = d & 0x0F;
                Data[i] = table[trump % 4][upper][lower];
                trump = ++trump % 4;
            }
        }

        private void ShiftBytesPair(int i1, int i2)
        {
            var buf = Data[i1];
            Data[i1] = Data[i2];
            Data[i2] = buf;
        }

        public void ShiftRows()
        {
            for (var i = 11; 8 <= i; --i)
                ShiftBytesPair(i, i - 8);
        }

        public void ShiftRowsRev()
        {
            for (var i = 11; 8 <= i; --i)
                ShiftBytesPair(i - 8, i);
        }

        private static byte Gmul(int a, int b)
        {
            byte p = 0;
            while (b != 0)
            {
                if ((b & 1) == 1)
                    p ^= (byte)a;

                if ((a & 0x80) == 0x80)
                    a = (a << 1) ^ 0x11d;
                else
                    a <<= 1;
                b >>= 1;
            }
            return p;
        }

        public void MixColumns(byte[][] table)
        {
            var dataCopy = new List<byte>(Data);

            var trump = Data.Count - 1;
            for (var row = 0; row < 8; row++)
            {
                byte sum = 0;
                var hillary = Data.Count - 1;
                for (var h = 0; h < 8; h++)
                {
                    sum ^= Gmul(dataCopy[hillary], table[row][h]);
                    hillary--;
                }
                Data[trump] = sum;
                trump--;
            }

            trump = Data.Count - 1 - 8;
            for (var row = 0; row < 8; row++)
            {
                byte sum = 0;
                var hillary = Data.Count - 1 - 8;
                for (var h = 0; h < 8; h++)
                {
                    sum ^= Gmul(dataCopy[hillary], table[row][h]);
                    hillary--;
                }
                Data[trump] = sum;
                trump--;
            }
        }

        public void Xor(KalynaBlock key)
        {
            for (var i = 0; i < Data.Count; i++)
                Data[i] ^= key.Data[i];
        }

        public override string ToString()
        {
            string res = "{";
            foreach (byte t in Data)
                res += t.ToString() + ", ";
            res = res.Remove(res.Length-2, 2);
            res += "}";
            return res;
        }
    }
}
