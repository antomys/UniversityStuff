using System;
using System.Security.Cryptography;
using System.Text;

namespace Lab4
{
    public static class OAEP
    {
        static readonly SHA256 sha256 = SHA256.Create();

        public static byte[] TransformOAEP(byte[] input, String parameters, int zeros)
        {
            Random random = new Random();
            String[] tokens = parameters.Split(' ');
            if (tokens.Length != 2 || tokens[0] != ("SHA-256") || tokens[1] != ("MGF1"))
            {
                return null;
            }
            int mLen = input.Length;
            int hLen = 32;
            int length = input.Length + (hLen << 1) + 1 + zeros;
            int zeroPad = length - mLen - (hLen << 1) - 1;
            byte[] dataBlock = new byte[length - hLen];
            Array.Copy(sha256.ComputeHash(Encoding.UTF8.GetBytes(parameters)), 0, dataBlock, 0, hLen);
            Array.Copy(input, 0, dataBlock, hLen + zeroPad + 1, mLen);
            dataBlock[hLen + zeroPad] = 1;
            byte[] seed = new byte[hLen];
            random.NextBytes(seed);
            byte[] dataBlockMask = MGF1(seed, 0, hLen, length - hLen);
            for (int i = 0; i < length - hLen; i++)
            {
                dataBlock[i] ^= dataBlockMask[i];
            }
            byte[] seedMask = MGF1(dataBlock, 0, length - hLen, hLen);
            for (int i = 0; i < hLen; i++)
            {
                seed[i] ^= seedMask[i];
            }
            byte[] padded = new byte[length];
            Array.Copy(seed, 0, padded, 0, hLen);
            Array.Copy(dataBlock, 0, padded, hLen, length - hLen);
            return padded;
        }

        public static byte[] RestoreOAEP(byte[] input, string parameters)
        {
            string[] tokens = parameters.Split(' ');

            if (tokens.Length != 2 || tokens[0] != ("SHA-256") || tokens[1] != ("MGF1"))
            {
                return null;
            }

            int mLen = input.Length;
            int hLen = 32;

            if (mLen < (hLen << 1) + 1)
            {
                return null;
            }

            byte[] copy = new byte[mLen];
            Array.Copy(input, 0, copy, 0, mLen);
            byte[] seedMask = MGF1(copy, hLen, mLen - hLen, hLen);

            for (int i = 0; i < hLen; i++)
            {
                copy[i] ^= seedMask[i];
            }

            byte[] paramsHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(parameters));
            byte[] dataBlockMask = MGF1(copy, 0, hLen, mLen - hLen);
            int index = -1;

            for (int i = hLen; i < mLen; i++)
            {
                copy[i] ^= dataBlockMask[i - hLen];

                if (i < (hLen << 1))
                {
                    if (copy[i] != paramsHash[i - hLen])
                    {
                        return null;
                    }
                }
                else if (index == -1)
                {
                    if (copy[i] == 1)
                    {
                        index = i + 1;
                    }
                }
            }
            
            if (index == -1 || index == mLen)
            {
                return null;
            }

            byte[] unpadded = new byte[mLen - index];
            Array.Copy(copy, index, unpadded, 0, mLen - index);

            return unpadded;
        }

        static byte[] MGF1(byte[] seed, int seedOffset, int seedLength, int desiredLength)
        {
            int hLen = 32;
            int offset = 0;
            int i = 0;
            byte[] mask = new byte[desiredLength];
            byte[] temp = new byte[seedLength + 4];
            Array.Copy(seed, seedOffset, temp, 4, seedLength);

            while (offset < desiredLength)
            {
                temp[0] = (byte)(i >> 24);
                temp[1] = (byte)(i >> 16);
                temp[2] = (byte)(i >> 8);
                temp[3] = (byte)i;
                int remaining = desiredLength - offset;
                Array.Copy(sha256.ComputeHash(temp), 0, mask, offset, remaining < hLen ? remaining : hLen);
                offset = offset + hLen;
                i = i + 1;
            }

            return mask;
        }
    }
}
