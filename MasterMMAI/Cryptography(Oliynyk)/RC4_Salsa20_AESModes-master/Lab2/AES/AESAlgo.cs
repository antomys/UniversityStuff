using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.AES
{
    public class AESAlgo : IChipherer
    {
        private const int ROUND_NO = 10;
        private readonly byte[] roundCoefficient = new byte[ROUND_NO] {
            0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36 };

        public AESBlock key;
        private AESBlock[] roundKeys = new AESBlock[ROUND_NO + 1];

        public void GenerateSubKeys()
        {
            roundKeys[0] = new AESBlock(key);
            for (int i = 0; i < ROUND_NO; ++i)
            {
                roundKeys[i + 1] = new AESBlock(roundKeys[i]);
                roundKeys[i + 1].KeyTransform(roundCoefficient[i]);
            }
        }

        public AESAlgo(byte[] key)
        {
            this.key = new AESBlock(key);

            for (int i = 0; i < ROUND_NO + 1; ++i)
                roundKeys[i] = new AESBlock();

            GenerateSubKeys();
        }

        public AESBlock Encrypt(AESBlock inputBlock)
        {
            AESBlock chipherBlock = new AESBlock(inputBlock);

            chipherBlock.AddRoundKey(roundKeys[0]);

            for (int r = 0; r < ROUND_NO; ++r)
            {
                chipherBlock.SubBytesForward();

                chipherBlock.ShiftRows();

                if (r < ROUND_NO - 1)
                    chipherBlock.MixColumns();

                chipherBlock.AddRoundKey(roundKeys[r + 1]);
            }

            return chipherBlock;
        }

        public AESBlock Decrypt(AESBlock chipherBlock)
        {
            AESBlock outputBlock = new AESBlock(chipherBlock);

            for (int r = 0; r < ROUND_NO; ++r)
            {
                outputBlock.AddRoundKey(roundKeys[ROUND_NO - r]);

                if (r > 0)
                    outputBlock.inverseMixColumns();

                outputBlock.InverseShiftRows();

                outputBlock.SubBytesInverse();
            }

            outputBlock.AddRoundKey(roundKeys[0]);

            return outputBlock;
        }

        public byte[] Encrypt(byte[] plaintext)
        {
            AESBlock plainBlock = new AESBlock(plaintext);
            AESBlock chipherBlock = Encrypt(plainBlock);

            return chipherBlock.data;
        }

        public byte[] Decrypt(byte[] chiphertext)
        {
            AESBlock chipherBlock = new AESBlock(chiphertext);
            AESBlock plainBlock = Decrypt(chipherBlock);

            return plainBlock.data;
        }
    }
}
