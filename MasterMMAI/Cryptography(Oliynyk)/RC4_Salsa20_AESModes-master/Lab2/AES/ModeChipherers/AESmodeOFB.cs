using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.AES.ModeChipherers
{
    class AESmodeOFB : IChipherer
    {
        AESAlgo aes;
        byte[] key;
        byte[] iv;
        static int BLOCK_SIZE = 16;

        public AESmodeOFB(byte[] key, byte[] iv)
        {
            this.key = key;
            this.iv = iv;
            aes = new AESAlgo(key);
        }


        public byte[] Decrypt(byte[] chiphertext)
        {
            byte[] res = new byte[chiphertext.Length];
            int currpos = 0;
            byte[] pi = new byte[BLOCK_SIZE]; //P(i)
            byte[] ci = new byte[BLOCK_SIZE]; //C(i)
            byte[] oi = new byte[BLOCK_SIZE]; //O(i)
            byte[] oim = new byte[BLOCK_SIZE]; //O(i-1)

            Array.Copy(iv, 0, oim, 0, BLOCK_SIZE);

            while (currpos < chiphertext.Length)
            {
                Array.Copy(chiphertext, currpos, ci, 0, BLOCK_SIZE);

                oi = aes.Encrypt(oim);
                Array.Copy(oi, 0, oim, 0, BLOCK_SIZE);

                arrXOR(ci, oi);

                Array.Copy(ci, 0, res, currpos, BLOCK_SIZE);
                currpos += BLOCK_SIZE;
            }

            return res;
        }

        public byte[] Encrypt(byte[] plaintext)
        {
            byte[] res = new byte[plaintext.Length];
            int currpos = 0;
            byte[] pi = new byte[BLOCK_SIZE]; //P(i)
            byte[] ci = new byte[BLOCK_SIZE]; //C(i)
            byte[] oi = new byte[BLOCK_SIZE]; //O(i)
            byte[] oim = new byte[BLOCK_SIZE]; //O(i-1)

            Array.Copy(iv, 0, oim, 0, BLOCK_SIZE);

            while (currpos < plaintext.Length)
            {
                Array.Copy(plaintext, currpos, pi, 0, BLOCK_SIZE);

                oi = aes.Encrypt(oim);
                Array.Copy(oi, 0, oim, 0, BLOCK_SIZE);

                arrXOR(pi, oi);

                Array.Copy(pi, 0, res, currpos, BLOCK_SIZE);
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
