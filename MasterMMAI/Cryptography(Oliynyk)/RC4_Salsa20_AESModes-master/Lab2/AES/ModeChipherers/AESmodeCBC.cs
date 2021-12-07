using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.AES.ModeChipherers
{
    public class AESmodeCBC : IChipherer
    {

        AESAlgo aes;
        byte[] key;
        byte[] iv;
        static int BLOCK_SIZE = 16;

        public AESmodeCBC(byte[] key, byte[] iv)
        {
            this.key = key;
            this.iv = iv;
            aes = new AESAlgo(key);
        }


        public byte[] Decrypt(byte[] chiphertext)
        {
            byte[] res = new byte[chiphertext.Length];
            int currpos = 0;
            byte[] inpBlock = new byte[BLOCK_SIZE];
            byte[] outpBlock = new byte[BLOCK_SIZE];
            byte[] lastBlock = new byte[BLOCK_SIZE];

            Array.Copy(iv, 0, lastBlock, 0, BLOCK_SIZE);

            while (currpos < chiphertext.Length)
            {
                Array.Copy(chiphertext, currpos, inpBlock, 0, BLOCK_SIZE);

                outpBlock = aes.Decrypt(inpBlock);
                arrXOR(outpBlock, lastBlock);

                Array.Copy(outpBlock, 0, res, currpos, BLOCK_SIZE);
                Array.Copy(inpBlock, 0, lastBlock, 0, BLOCK_SIZE);
                currpos += BLOCK_SIZE;
            }

            return res;
        }

        public byte[] Encrypt(byte[] plaintext)
        {
            byte[] res = new byte[plaintext.Length];
            int currpos = 0;
            byte[] inpBlock = new byte[BLOCK_SIZE];
            byte[] outpBlock = new byte[BLOCK_SIZE];

            Array.Copy(iv, 0, outpBlock, 0, BLOCK_SIZE);

            while (currpos < plaintext.Length)
            {
                Array.Copy(plaintext, currpos, inpBlock, 0, BLOCK_SIZE);
                arrXOR(inpBlock, outpBlock);

                outpBlock = aes.Encrypt(inpBlock);

                Array.Copy(outpBlock, 0, res, currpos, BLOCK_SIZE);
                currpos += BLOCK_SIZE;
            }

            return res;
        }

        static void arrXOR(byte[] arr1, byte[] arr2)
        {
            for (int i = 0; i < BLOCK_SIZE; ++i)
                arr1[i] ^= arr2[i];
        }
    }
}
