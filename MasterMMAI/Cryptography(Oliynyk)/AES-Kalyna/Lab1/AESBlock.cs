using System;

namespace Lab1
{
    public class AESBlock
    {
        public static int BLOCK_SIZE = 128/8;
        public const byte BYTE_POLY_REDUCTION = 0x1b;
        public const ushort POLY_REDUCTION = 0x11b;

        public byte[] data;

        public AESBlock()
        {
            data = new byte[BLOCK_SIZE];
            for (int i = 0; i < BLOCK_SIZE; ++i)
                data[i] = 0;
        }

        public AESBlock(byte[] d)
        {
            data = new byte[BLOCK_SIZE];
            for (int i = 0; i < BLOCK_SIZE; ++i)
                data[i] = d[i];
        }

        public AESBlock(AESBlock block)
        {
            data = new byte[BLOCK_SIZE];
            for (int i = 0; i < BLOCK_SIZE; ++i)
                data[i] = block.data[i];
        }

        public void KeyTransform(byte roundCoef)
        {
            byte[] keyword = new byte[4];
            Array.Copy(data, 12, keyword, 0, 4);

            byte buf = keyword[0];
            keyword[0] = (byte)(StaticTables.forwardSBox[keyword[1]] ^ roundCoef);
            keyword[1] = StaticTables.forwardSBox[keyword[2]];
            keyword[2] = StaticTables.forwardSBox[keyword[3]];
            keyword[3] = StaticTables.forwardSBox[buf];

            for (int k = 0; k < 4; ++k)
                data[k] ^= keyword[k];
            for (int i = 0; i < BLOCK_SIZE - 4; ++i)
                data[i + 4] ^= data[i];
        }

        public void SubBytesForward()
        {
            for (int i = 0; i < BLOCK_SIZE; ++i)
                data[i] = StaticTables.forwardSBox[data[i]];
        }

        public void SubBytesInverse()
        {
            for (int i = 0; i < BLOCK_SIZE; ++i)
                data[i] = StaticTables.inverseSBox[data[i]];
        }

        public void AddRoundKey(AESBlock rkey)
        {
            for (int i = 0; i < BLOCK_SIZE; ++i)
                data[i] ^= rkey.data[i];
        }

        public void ShiftRows()
        {
            byte buf;
            //shift row 2 (1 -> 13, 5 -> 1, 9 -> 5, 13 -> 9)
            buf = data[1];
            data[1] = data[5];
            data[5] = data[9];
            data[9] = data[13];
            data[13] = buf;

            //shift row 3 (2 -> 10, 6 -> 14, 10 -> 2, 14 -> 6)
            buf = data[2];
            data[2] = data[10];
            data[10] = buf;
            buf = data[6];
            data[6] = data[14];
            data[14] = buf;

            //shift row 4 (3 -> 7, 7 -> 11, 11 -> 15, 15 -> 3)
            buf = data[15];
            data[15] = data[11];
            data[11] = data[7];
            data[7] = data[3];
            data[3] = buf;
        }

        public void InverseShiftRows()
        {
            byte buf;
            //inverse shift row 2 (1 -> 5, 5 -> 9, 9 -> 13, 13 -> 1)
            buf = data[1];
            data[1] = data[13];
            data[13] = data[9];
            data[9] = data[5];
            data[5] = buf;

            //inverse shift row 3 (2 -> 10, 6 -> 14, 10 -> 2, 14 -> 6)
            buf = data[2];
            data[2] = data[10];
            data[10] = buf;
            buf = data[6];
            data[6] = data[14];
            data[14] = buf;

            //inverse shift row 4 (3 -> 15, 7 -> 3, 11 -> 7, 15 -> 11)
            buf = data[15];
            data[15] = data[3];
            data[3] = data[7];
            data[7] = data[11];
            data[11] = buf;
        }

        public void MixColumns()
        {
            byte[] temp = new byte[4];
            int p;
            for (int i = 0; i < 4; ++i)
            {
                p = i * 4;
                temp[0] = (byte)(galoisMult2(data[p]) ^ (galoisMult2(data[p + 1]) ^ data[p + 1]) ^ data[p + 2] ^ data[p + 3]);
                temp[1] = (byte)(galoisMult2(data[p + 1]) ^ (galoisMult2(data[p + 2]) ^ data[p + 2]) ^ data[p + 3] ^ data[p]);
                temp[2] = (byte)(galoisMult2(data[p + 2]) ^ (galoisMult2(data[p + 3]) ^ data[p + 3]) ^ data[p] ^ data[p + 1]);
                temp[3] = (byte)(galoisMult2(data[p + 3]) ^ (galoisMult2(data[p]) ^ data[p]) ^ data[p + 1] ^ data[p + 2]);
                Array.Copy(temp, 0, data, p, 4);
            }
        }

        private byte[] inverseMixColumnMatrixElementTable = new byte[] { 0x0B, 0x0D, 0x09, 0x0E, 0x0B, 0x0D, 0x09 };
        public void inverseMixColumns()
        {
            byte[] temp = new byte[4];
            int p, p2;
            for (int i = 0; i < 4; ++i)
            {
                p = i * 4;
                for (int j = 0; j < 4; ++j)
                {
                    p2 = 3 - j;
                    temp[j] = (byte)(galoisDefaultMult(data[p], inverseMixColumnMatrixElementTable[p2]) ^
                        galoisDefaultMult(data[p + 1], inverseMixColumnMatrixElementTable[p2 + 1]) ^
                        galoisDefaultMult(data[p + 2], inverseMixColumnMatrixElementTable[p2 + 2]) ^ 
                        galoisDefaultMult(data[p + 3], inverseMixColumnMatrixElementTable[p2 + 3]));
                }
                Array.Copy(temp, 0, data, p, 4);
            }
        }

        private byte galoisMult2(byte val, byte polyRed = BYTE_POLY_REDUCTION)
        {
            return val >= 128 ? (byte)((val << 1) ^ polyRed) : (byte)(val << 1);
        }

        private byte[] quickXORTable = new byte[8] { 0x00, 0x1b, 0x36, 0x2d, 0x6c, 0x77, 0x5a, 0x41 };
        private byte galoisDefaultMult(byte val, byte mult)
        {
            int buf = val << 3;
            if (mult != 0x0E)
                buf ^= val;
            if (mult > 0x0C)
                buf ^= val << 2;
            if ((mult & 0x02) > 0)
                buf ^= val << 1;
            byte xorval = quickXORTable[buf >> 8];
            return xorval == 0 ? (byte)buf : (byte)(buf ^ xorval);
        }

        public override string ToString()
        {
            string res = "{";
            for (int i = 0; i < BLOCK_SIZE; ++i)
                res += data[i].ToString() + (i!=BLOCK_SIZE-1 ? ", " : "");
            res += "}";
            return res;
        }
    }
}
