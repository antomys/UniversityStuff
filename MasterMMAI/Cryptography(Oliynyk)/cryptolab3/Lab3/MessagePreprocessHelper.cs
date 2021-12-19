using System;
using System.Linq;

namespace Lab3
{
    static class MessagePreprocessHelper
    {
        public static byte[] GetPaddedMessage512(byte[] input)
        {
            byte[] L = BitConverter.GetBytes(Convert.ToUInt64(input.Length * 8)).Reverse().ToArray();
            int k = 0;

            while ((input.Length * 8 + 8 + k + 64) % 512 != 0)
            {
                k += 8;
            }

            byte[] paddedmessage = new byte[(input.Length * 8 + 8 + k + 64) / 8];

            input.CopyTo(paddedmessage, 0);
            paddedmessage[input.Length] = 0x80;

            for (int i = 1; i < (1 + (k / 8)); i++)
                paddedmessage[input.Length + i] = 0x00;
            L.CopyTo(paddedmessage, input.Length + 1 + (k / 8));

            return paddedmessage;
        }

        public static byte[][] SplitMessage512(byte[] message)
        {
            int N = message.Length / 64;
            byte[][] res = new byte[N][];

            for (int i = 0; i < N; i++)
            {
                byte[] temp = new byte[64];
                for (int j = 0; j < 64; j++)
                    temp[j] = message[(i * 64) + j];
                res[i] = temp;
            }

            return res;
        }
    }
}
