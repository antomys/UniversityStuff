using System;
using System.Collections.Generic;

namespace Lab3.Kupyna
{
    class TPCircle
    {
        private static int ROUNDS_COUNT_256 = 10;

        private static int COLUMNS_256 = 8;

        private static Dictionary<String, Byte> PI_0 = PiTables.getPI0();

        private static Dictionary<String, Byte> PI_1 = PiTables.getPI1();

        private static Dictionary<String, Byte> PI_2 = PiTables.getPI2();

        private static Dictionary<String, Byte> PI_3 = PiTables.getPI3();

        private static uint[] P_512 = new uint[]{
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x50, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x60, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x70, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        private static byte[][] mds_matrix = new byte[][]{
            new byte[] {1, 1, 5, 1, 8, 6, 7, 4},
            new byte[] {4, 1, 1, 5, 1, 8, 6, 7},
            new byte[] {7, 4, 1, 1, 5, 1, 8, 6},
            new byte[] {6, 7, 4, 1, 1, 5, 1, 8},
            new byte[] {8, 6, 7, 4, 1, 1, 5, 1},
            new byte[] {1, 8, 6, 7, 4, 1, 1, 5},
            new byte[] {5, 1, 8, 6, 7, 4, 1, 1},
            new byte[] {1, 5, 1, 8, 6, 7, 4, 1}
    };

        public static byte[] generateMessageAfterTPlusCircle(byte[] messageForTPlusCircle)
        {
            byte[] iteration = messageForTPlusCircle;

            for (int round = 0; round < ROUNDS_COUNT_256; round++)
            {
                byte[] afterK = operationK(iteration);
                byte[] afterPi = operationPi(afterK);
                byte[] afterRo = operationRo(afterPi);
                byte[] afterW = operationW(afterRo);
                iteration = afterW;
            }
            resetP();

            return iteration;
        }

        private static byte[] operationK(byte[] firstStep)
        {
            byte[] afterK = XOR(firstStep, P_512);
            updateP();
            return afterK;
        }

        private static void updateP()
        {
            P_512[0] = (P_512[0] + 0x01);
            P_512[8] = (P_512[8] + 0x01);
            P_512[16] = (P_512[16] + 0x01);
            P_512[24] = (P_512[24] + 0x01);
            P_512[32] = (P_512[32] + 0x01);
            P_512[40] = (P_512[40] + 0x01);
            P_512[48] = (P_512[48] + 0x01);
            P_512[56] = (P_512[56] + 0x01);
        }

        private static byte[] operationPi(byte[] afterK)
        {
            String afterKHex = bytesToHexString(afterK);
            String[] splittedByOneSymbol = afterKHex.Split("");
            String[] splittedByTwoSymbol = new String[splittedByOneSymbol.Length / 2];
            int splittedByTwoSymbolCount = 0;
            for (int i = 0; i < splittedByOneSymbol.Length && splittedByOneSymbol.Length>1; i += 2)
            {
                splittedByTwoSymbol[splittedByTwoSymbolCount] = splittedByOneSymbol[i] + splittedByOneSymbol[i + 1];
                splittedByTwoSymbolCount++;
            }

            byte[] afterPi = new byte[64];

            if (splittedByTwoSymbol.Length > 0)
            {
                for (int j = 0; j < 64; j += 4)
                {
                    afterPi[j] = PI_0[(splittedByTwoSymbol[j])];
                    afterPi[j + 1] = PI_1[(splittedByTwoSymbol[j + 1])];
                    afterPi[j + 2] = PI_2[(splittedByTwoSymbol[j + 2])];
                    afterPi[j + 3] = PI_3[(splittedByTwoSymbol[j + 3])];
                }
            }

            return afterK;
        }

        private static byte[] operationRo(byte[] afterPi)
        {
            byte[] afterRo = new byte[afterPi.Length];

            afterRo[0] = afterPi[0];
            afterRo[9] = afterPi[1];
            afterRo[18] = afterPi[2];
            afterRo[27] = afterPi[3];
            afterRo[36] = afterPi[4];
            afterRo[45] = afterPi[5];
            afterRo[54] = afterPi[6];
            afterRo[63] = afterPi[7];

            afterRo[8] = afterPi[8];
            afterRo[17] = afterPi[9];
            afterRo[26] = afterPi[10];
            afterRo[35] = afterPi[11];
            afterRo[44] = afterPi[12];
            afterRo[53] = afterPi[13];
            afterRo[62] = afterPi[14];
            afterRo[7] = afterPi[15];

            afterRo[16] = afterPi[16];
            afterRo[25] = afterPi[17];
            afterRo[34] = afterPi[18];
            afterRo[43] = afterPi[19];
            afterRo[52] = afterPi[20];
            afterRo[61] = afterPi[21];
            afterRo[6] = afterPi[22];
            afterRo[15] = afterPi[23];

            afterRo[24] = afterPi[24];
            afterRo[33] = afterPi[25];
            afterRo[42] = afterPi[26];
            afterRo[51] = afterPi[27];
            afterRo[60] = afterPi[28];
            afterRo[5] = afterPi[29];
            afterRo[14] = afterPi[30];
            afterRo[23] = afterPi[31];

            afterRo[32] = afterPi[32];
            afterRo[41] = afterPi[33];
            afterRo[50] = afterPi[34];
            afterRo[59] = afterPi[35];
            afterRo[4] = afterPi[36];
            afterRo[13] = afterPi[37];
            afterRo[22] = afterPi[38];
            afterRo[31] = afterPi[39];

            afterRo[40] = afterPi[40];
            afterRo[49] = afterPi[41];
            afterRo[58] = afterPi[42];
            afterRo[3] = afterPi[43];
            afterRo[12] = afterPi[44];
            afterRo[21] = afterPi[45];
            afterRo[30] = afterPi[46];
            afterRo[39] = afterPi[47];

            afterRo[48] = afterPi[48];
            afterRo[57] = afterPi[49];
            afterRo[2] = afterPi[50];
            afterRo[11] = afterPi[51];
            afterRo[20] = afterPi[52];
            afterRo[29] = afterPi[53];
            afterRo[38] = afterPi[54];
            afterRo[47] = afterPi[55];

            afterRo[56] = afterPi[56];
            afterRo[1] = afterPi[57];
            afterRo[10] = afterPi[58];
            afterRo[19] = afterPi[59];
            afterRo[28] = afterPi[60];
            afterRo[37] = afterPi[61];
            afterRo[46] = afterPi[62];
            afterRo[55] = afterPi[63];

            return afterRo;
        }

        private static byte[] operationW(byte[] afterRo)
        {
            byte[][] tempState1 = new byte[8][];
            for (int i =0; i<8; ++i)
            {
                tempState1[i] = new byte[8];
            }

            int k = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tempState1[i][j] = afterRo[k];
                    k++;
                }
            }


            byte[] mixColumnsResult = new byte[8];
            for (int var4 = 0; var4 < COLUMNS_256; ++var4)
            {
                for(int i=0; i<mixColumnsResult.Length; ++i)
                {
                    mixColumnsResult[i] = 0;
                }

                int var5;
                for (var5 = 7; var5 >= 0; --var5)
                {
                    byte var7 = 0;

                    for (int var6 = 7; var6 >= 0; --var6)
                    {
                        var7 ^= multiplyGF(tempState1[var4][var6], mds_matrix[var5][var6]);
                    }

                    mixColumnsResult[var5] = var7;
                }

                for (var5 = 0; var5 < 8; ++var5)
                {
                    tempState1[var4][var5] = mixColumnsResult[var5];
                }
            }

            byte[] afterW = new byte[64];
            int z = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    afterW[z] = tempState1[i][j];
                    z++;
                }
            }

            return afterW;
        }

        private static byte multiplyGF(byte var1, byte var2)
        {
            byte var3 = 0;

            for (int var5 = 0; var5 < 8; ++var5)
            {
                if ((var2 & 1) == 1)
                {
                    var3 ^= var1;
                }

                byte var4 = (byte)(var1 & 128);
                var1 = (byte)(var1 << 1);
                if (var4 == 128)
                {
                    var1 = (byte)(var1 ^ 285);
                }

                var2 = (byte)(var2 >> 1);
            }

            return var3;
        }

        private static void resetP()
        {
            P_512 = new uint[]{
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x50, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x60, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x70, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
        }
        public static byte[] XOR(byte[] firstArray, uint[] secondArray)
        {
            byte[] thirdArray = new byte[firstArray.Length];
            for (int i = 0; i < firstArray.Length; i++)
            {
                thirdArray[i] = (byte)(firstArray[i] ^ secondArray[i]);
            }
            return thirdArray;
        }

        public static byte[] XOR(byte[] firstArray, byte[] secondArray)
        {
            byte[] thirdArray = new byte[firstArray.Length];
            for (int i = 0; i < firstArray.Length; i++)
            {
                thirdArray[i] = (byte)(firstArray[i] ^ secondArray[i]);
            }
            return thirdArray;
        }

        public static String bytesToHexString(byte[] bytes)
        {
            char[] hexChars = new char[bytes.Length * 2];

            for (int j = 0; j < bytes.Length; j++)
            {
                int v = bytes[j] & 0xFF;
                hexChars[j * 2] = HEX_ARRAY[v >> 4];
                hexChars[j * 2 + 1] = HEX_ARRAY[v & 0x0F];
            }

            return new string(hexChars);
        }

        private static char[] HEX_ARRAY = "0123456789ABCDEF".ToCharArray();
    }
}
