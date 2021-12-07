using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    public class Salsa20 : IChipherer
    {
        private uint[] state;

        private static int blockSize = 64;
        private readonly int roundsCount = 20;
        byte[] constants = Encoding.ASCII.GetBytes("expand 32-byte k");
        private readonly byte[] key;
        private readonly byte[] iv;

        public Salsa20(byte[] key, byte[] iv) // key - 32; IV - 8
        {
            this.key = key;
            this.iv = iv;
        }

        private void InitState()
        {
            state = new uint[16];

            state[1] = ToUInt32(key, 0);
            state[2] = ToUInt32(key, 4);
            state[3] = ToUInt32(key, 8);
            state[4] = ToUInt32(key, 12);

            state[11] = ToUInt32(key, 16);
            state[12] = ToUInt32(key, 20);
            state[13] = ToUInt32(key, 24);
            state[14] = ToUInt32(key, 28);

            state[0] = ToUInt32(constants, 0);
            state[5] = ToUInt32(constants, 4);
            state[10] = ToUInt32(constants, 8);
            state[15] = ToUInt32(constants, 12);

            state[6] = ToUInt32(iv, 0);
            state[7] = ToUInt32(iv, 4);
            state[8] = 0;
            state[9] = 0;
        }

        public byte[] Transform(byte[] input)
        {
            InitState();

            byte[] res = new byte[input.Length]; 
            byte[] outputBlock = new byte[blockSize];
            int currPos = 0;

            while (currPos<input.Length)
            {
                BlockTransform(outputBlock, state);

                state[8] = AddOne(state[8]);
                if (state[8] == 0)
                {
                    state[9] = AddOne(state[9]);
                }

                int currBlockSize = blockSize > input.Length - currPos ? input.Length - currPos : blockSize;

                for (int i = 0; i < currBlockSize; i++)
                {
                    res[currPos + i] = (byte)(input[currPos + i] ^ outputBlock[i]);
                }

                currPos += blockSize;
            }

            return res;
        }

        private void BlockTransform(byte[] output, uint[] input)
        {
            uint[] tmp = (uint[])input.Clone();

            for (int i = roundsCount; i > 0; i -= 2)
            {
                tmp[4] ^= Rotate(Add(tmp[0], tmp[12]), 7);
                tmp[8] ^= Rotate(Add(tmp[4], tmp[0]), 9);
                tmp[12] ^= Rotate(Add(tmp[8], tmp[4]), 13);
                tmp[0] ^= Rotate(Add(tmp[12], tmp[8]), 18);
                tmp[9] ^= Rotate(Add(tmp[5], tmp[1]), 7);
                tmp[13] ^= Rotate(Add(tmp[9], tmp[5]), 9);
                tmp[1] ^= Rotate(Add(tmp[13], tmp[9]), 13);
                tmp[5] ^= Rotate(Add(tmp[1], tmp[13]), 18);
                tmp[14] ^= Rotate(Add(tmp[10], tmp[6]), 7);
                tmp[2] ^= Rotate(Add(tmp[14], tmp[10]), 9);
                tmp[6] ^= Rotate(Add(tmp[2], tmp[14]), 13);
                tmp[10] ^= Rotate(Add(tmp[6], tmp[2]), 18);
                tmp[3] ^= Rotate(Add(tmp[15], tmp[11]), 7);
                tmp[7] ^= Rotate(Add(tmp[3], tmp[15]), 9);
                tmp[11] ^= Rotate(Add(tmp[7], tmp[3]), 13);
                tmp[15] ^= Rotate(Add(tmp[11], tmp[7]), 18);

                tmp[1] ^= Rotate(Add(tmp[0], tmp[3]), 7);
                tmp[2] ^= Rotate(Add(tmp[1], tmp[0]), 9);
                tmp[3] ^= Rotate(Add(tmp[2], tmp[1]), 13);
                tmp[0] ^= Rotate(Add(tmp[3], tmp[2]), 18);
                tmp[6] ^= Rotate(Add(tmp[5], tmp[4]), 7);
                tmp[7] ^= Rotate(Add(tmp[6], tmp[5]), 9);
                tmp[4] ^= Rotate(Add(tmp[7], tmp[6]), 13);
                tmp[5] ^= Rotate(Add(tmp[4], tmp[7]), 18);
                tmp[11] ^= Rotate(Add(tmp[10], tmp[9]), 7);
                tmp[8] ^= Rotate(Add(tmp[11], tmp[10]), 9);
                tmp[9] ^= Rotate(Add(tmp[8], tmp[11]), 13);
                tmp[10] ^= Rotate(Add(tmp[9], tmp[8]), 18);
                tmp[12] ^= Rotate(Add(tmp[15], tmp[14]), 7);
                tmp[13] ^= Rotate(Add(tmp[12], tmp[15]), 9);
                tmp[14] ^= Rotate(Add(tmp[13], tmp[12]), 13);
                tmp[15] ^= Rotate(Add(tmp[14], tmp[13]), 18);
            }

            for (int i = 0; i < 16; i++)
            {
                ToBytes(Add(tmp[i], input[i]), output, 4 * i);
            }
        }

        private static uint Rotate(uint v, int c)
        {
            return (v << c) | (v >> (blockSize/2 - c));
        }

        private static uint Add(uint v, uint w)
        {
            return unchecked(v + w);
        }

        private static uint AddOne(uint v)
        {
            return unchecked(v + 1);
        }

        private static uint ToUInt32(byte[] input, int inputOffset)
        {
            unchecked
            {
                return (uint)(((input[inputOffset] |
                                (input[inputOffset + 1] << 8)) |
                                (input[inputOffset + 2] << 16)) |
                                (input[inputOffset + 3] << 24));
            }
        }

        private static void ToBytes(uint input, byte[] output, int outputOffset)
        {
            unchecked
            {
                output[outputOffset] = (byte)input;
                output[outputOffset + 1] = (byte)(input >> 8);
                output[outputOffset + 2] = (byte)(input >> 16);
                output[outputOffset + 3] = (byte)(input >> 24);
            }
        }

        public byte[] Encrypt(byte[] plaintext)
        {
            return Transform(plaintext);
        }

        public byte[] Decrypt(byte[] chiphertext)
        {
            return Transform(chiphertext);
        }
    }
}
