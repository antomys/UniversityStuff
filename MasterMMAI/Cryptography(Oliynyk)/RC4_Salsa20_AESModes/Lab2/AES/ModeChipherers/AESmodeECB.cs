using System;

namespace Lab2.AES.ModeChipherers
{
    class AESmodeECB : IChipherer
    {
        AESAlgo aes;
        byte[] key;
        static int BLOCK_SIZE = 16;

        public AESmodeECB(byte[] key)
        {
            this.key = key;
            aes = new AESAlgo(key);
        }


        public byte[] Decrypt(byte[] chiphertext)
        {
            byte[] res = new byte[chiphertext.Length];
            int currpos = 0;
            byte[] inpBlock = new byte[BLOCK_SIZE];
            byte[] outpBlock = new byte[BLOCK_SIZE];

            while (currpos<chiphertext.Length)
            {
                Array.Copy(chiphertext, currpos, inpBlock, 0, BLOCK_SIZE);
                outpBlock = aes.Decrypt(inpBlock);
                Array.Copy(outpBlock, 0, res, currpos, BLOCK_SIZE);
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

            while (currpos < plaintext.Length)
            {
                Array.Copy(plaintext, currpos, inpBlock, 0, BLOCK_SIZE);
                outpBlock = aes.Encrypt(inpBlock);
                Array.Copy(outpBlock, 0, res, currpos, BLOCK_SIZE);
                currpos += BLOCK_SIZE;
            }

            return res;
        }
    }
}
