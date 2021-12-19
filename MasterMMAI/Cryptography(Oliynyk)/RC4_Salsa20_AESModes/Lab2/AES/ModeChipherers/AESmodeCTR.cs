using System;

namespace Lab2.AES.ModeChipherers
{
    class AESmodeCTR : IChipherer
    {
        AESAlgo aes;
        byte[] key;
        byte[] iv;
        static int BLOCK_SIZE = 16;

        public AESmodeCTR(byte[] key, byte[] iv)
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
            byte[] ctr = new byte[BLOCK_SIZE];

            Array.Copy(iv, 0, ctr, 0, BLOCK_SIZE);

            while (currpos < chiphertext.Length)
            {
                Array.Copy(chiphertext, currpos, ci, 0, BLOCK_SIZE);

                pi = aes.Encrypt(ctr);

                arrXOR(pi, ci);

                Array.Copy(pi, 0, res, currpos, BLOCK_SIZE);
                currpos += BLOCK_SIZE;
                IncrCTR(ctr);
            }

            return res;
        }

        public byte[] Encrypt(byte[] plaintext)
        {
            byte[] res = new byte[plaintext.Length];
            int currpos = 0;
            byte[] pi = new byte[BLOCK_SIZE]; //P(i)
            byte[] ci = new byte[BLOCK_SIZE]; //C(i)
            byte[] ctr = new byte[BLOCK_SIZE];

            Array.Copy(iv, 0, ctr, 0, BLOCK_SIZE);

            while (currpos < plaintext.Length)
            {
                Array.Copy(plaintext, currpos, pi, 0, BLOCK_SIZE);

                ci = aes.Encrypt(ctr);

                arrXOR(ci, pi);

                Array.Copy(ci, 0, res, currpos, BLOCK_SIZE);
                currpos += BLOCK_SIZE;
                IncrCTR(ctr);
            }

            return res;
        }

        static void arrXOR(byte[] arr1, byte[] arr2)
        {
            for (int i = 0; i < BLOCK_SIZE; ++i)
                arr1[i] ^= arr2[i];
        }

        static void IncrCTR(byte[] ctr)
        {
            for (int i = 0; i < ctr.Length; ++i)
            {
                if (ctr[i] < 255)
                {
                    ctr[i]++;
                    return;
                }
            }
            
            for (int i = 0; i<ctr.Length; ++i)
            {
                ctr[i] = 0;
            }
        }
    }
}
