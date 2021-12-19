using System;

namespace Lab2.AES.ModeChipherers
{
    public class AESmodeCFB : IChipherer
    {
        AESAlgo aes;
        byte[] key;
        byte[] iv;
        static int BLOCK_SIZE = 16;

        public AESmodeCFB(byte[] key, byte[] iv)
        {
            this.key = key;
            this.iv = iv;
            aes = new AESAlgo(key);
        }


        public byte[] Decrypt(byte[] chiphertext)
        {
            byte[] res = new byte[chiphertext.Length];
            int currpos = 0;
            byte[] ci = new byte[BLOCK_SIZE];
            byte[] pi = new byte[BLOCK_SIZE];
            byte[] cim = new byte[BLOCK_SIZE];

            Array.Copy(iv, 0, cim, 0, BLOCK_SIZE);

            while (currpos < chiphertext.Length)
            {
                Array.Copy(chiphertext, currpos, ci, 0, BLOCK_SIZE);

                pi = aes.Encrypt(cim);
                arrXOR(pi, ci);

                Array.Copy(pi, 0, res, currpos, BLOCK_SIZE);
                Array.Copy(ci, 0, cim, 0, BLOCK_SIZE);
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
            byte[] cim = new byte[BLOCK_SIZE]; //C(i-1)

            Array.Copy(iv, 0, cim, 0, BLOCK_SIZE);

            while (currpos < plaintext.Length)
            {
                Array.Copy(plaintext, currpos, pi, 0, BLOCK_SIZE);

                ci = aes.Encrypt(cim); //C(i) = E(C(i-1))
                arrXOR(ci, pi); //C(i) = C(i) xor P(i)

                Array.Copy(ci, 0, res, currpos, BLOCK_SIZE);
                Array.Copy(ci, 0, cim, 0, BLOCK_SIZE);
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
