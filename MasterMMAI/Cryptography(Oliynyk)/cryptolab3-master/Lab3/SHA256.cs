using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    public class SHA256 : IHashFunc
    {
        uint[] H;

        public SHA256() {
            H = new uint[8];
        }

        public byte[] CalcHash(byte[] message)
        {
            byte[] paddedmessage = MessagePreprocessHelper.GetPaddedMessage512(message);

            byte[][] blocks = MessagePreprocessHelper.SplitMessage512(paddedmessage);

            InitH();

            for (int i = 0; i < blocks.Length; i++)
            {
                ProcessBlock(blocks[i]);
            }

            byte[] res = new byte[32];

            for (int i = 0; i < 8; i++)
            {
                (BitConverter.GetBytes(H[i]).Reverse().ToArray()).CopyTo(res, i * 4);
            }

            return res;
        }

        private void ProcessBlock(byte[] block)
        {
            uint[] W = new uint[64];

            for (int i = 0, j = 0; i < 16; ++i, j += 4)
            {
                W[i] = (uint)((block[j] << 24) | (block[j + 1] << 16) | (block[j + 2] << 8) | (block[j + 3]));
            }

            for (uint i = 16; i < 64; i++)
            {
                W[i] = S1(W[i - 2]) + W[i - 7] + S0(W[i - 15]) + W[i - 16];
            }

            uint
                a = H[0],
                b = H[1],
                c = H[2],
                d = H[3],
                e = H[4],
                f = H[5],
                g = H[6],
                h = H[7],
                T1,
                T2;

            for (int i = 0; i < 64; i++)
            {
                T1 = h + Z1(e) + Ch(e, f, g) + K[i] + W[i];
                T2 = Z0(a) + Maj(a, b, c);
                h = g;
                g = f;
                f = e;
                e = d + T1;
                d = c;
                c = b;
                b = a;
                a = T1 + T2;
            }

            H[0] = H[0] + a;
            H[1] = H[1] + b;
            H[2] = H[2] + c;
            H[3] = H[3] + d;
            H[4] = H[4] + e;
            H[5] = H[5] + f;
            H[6] = H[6] + g;
            H[7] = H[7] + h;
        }

        private void InitH()
        {
            H[0] = 0x6a09e667; 
            H[1] = 0xbb67ae85; 
            H[2] = 0x3c6ef372; 
            H[3] = 0xa54ff53a;
            H[4] = 0x510e527f; 
            H[5] = 0x9b05688c; 
            H[6] = 0x1f83d9ab; 
            H[7] = 0x5be0cd19;
        }

        static uint RotR(uint a, byte n) 
        {
            return (((a) >> (n)) | ((a) << (32 - (n))));
        }
        static uint ShR(uint a, byte n) 
        { 
            return (a >> n); 
        }

        private static uint Ch(uint x, uint y, uint z)
        {
            return (((x) & (y)) ^ ((~x) & (z)));
        }

        private static uint Maj(uint x, uint y, uint z)
        {
            return (((x) & (y)) ^ ((x) & (z)) ^ ((y) & (z)));
        }

        private static uint Z0(uint x)
        {
            return (RotR(x, 2) ^ RotR(x, 13) ^ RotR(x, 22));
        }

        private static uint Z1(uint x)
        {
            return (RotR(x, 6) ^ RotR(x, 11) ^ RotR(x, 25));
        }

        private static uint S0(uint x)
        {
            return (RotR(x, 7) ^ RotR(x, 18) ^ ShR(x, 3));
        }

        private static uint S1(uint x)
        {
            return (RotR(x, 17) ^ RotR(x, 19) ^ ShR(x, 10));
        }

        private static uint[] K = { 0x428a2f98,0x71374491,0xb5c0fbcf,0xe9b5dba5,0x3956c25b,0x59f111f1,0x923f82a4,0xab1c5ed5,
                                    0xd807aa98,0x12835b01,0x243185be,0x550c7dc3,0x72be5d74,0x80deb1fe,0x9bdc06a7,0xc19bf174,
                                    0xe49b69c1,0xefbe4786,0x0fc19dc6,0x240ca1cc,0x2de92c6f,0x4a7484aa,0x5cb0a9dc,0x76f988da,
                                    0x983e5152,0xa831c66d,0xb00327c8,0xbf597fc7,0xc6e00bf3,0xd5a79147,0x06ca6351,0x14292967,
                                    0x27b70a85,0x2e1b2138,0x4d2c6dfc,0x53380d13,0x650a7354,0x766a0abb,0x81c2c92e,0x92722c85,
                                    0xa2bfe8a1,0xa81a664b,0xc24b8b70,0xc76c51a3,0xd192e819,0xd6990624,0xf40e3585,0x106aa070,
                                    0x19a4c116,0x1e376c08,0x2748774c,0x34b0bcb5,0x391c0cb3,0x4ed8aa4a,0x5b9cca4f,0x682e6ff3,
                                    0x748f82ee,0x78a5636f,0x84c87814,0x8cc70208,0x90befffa,0xa4506ceb,0xbef9a3f7,0xc67178f2};
    }
}
