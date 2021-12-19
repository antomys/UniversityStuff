namespace Lab2
{
    public class RC4 : IChipherer
    {
        private const int N = 256;
        private int[] sbox;
        private byte[] key;

        public RC4(byte[] key)
        {
            this.key = key;
        }

        public byte[] Decrypt(byte[] chiphertext)
        {
            return Transform(chiphertext);
        }

        public byte[] Encrypt(byte[] plaintext)
        {
            return Transform(plaintext);
        }

        public byte[] Transform(byte[] input)
        {
            RC4Initialize();

            int i = 0, j = 0, k = 0;
            byte[] cipher = new byte[input.Length];
            for (int a = 0; a < input.Length; a++)
            {
                i = (i + 1) % N;
                j = (j + sbox[i]) % N;
                int tempSwap = sbox[i];
                sbox[i] = sbox[j];
                sbox[j] = tempSwap;

                k = sbox[(sbox[i] + sbox[j]) % N];
                int cipherBy = ((int)input[a]) ^ k;
                cipher[a] = (byte)cipherBy;
            }
            return cipher;
        }

        private void RC4Initialize()
        {
            sbox = new int[N];
            int[] keyBytes = new int[N];
            int n = key.Length;

            for (int a = 0; a < N; a++)
            {
                keyBytes[a] = (int)key[a % n];
                sbox[a] = a;
            }

            int b = 0;

            for (int a = 0; a < N; a++)
            {
                b = (b + sbox[a] + keyBytes[a]) % N;
                int tempSwap = sbox[a];
                sbox[a] = sbox[b];
                sbox[b] = tempSwap;
            }
        }
    }
}
