using System.Collections.Generic;
using Lab1.Interfaces;

namespace Lab1
{
    public class Kalyna : IChipherer
    {
        private KalynaBlock Key { get; }
        private List<KalynaBlock> RoundsKeys { get; } = new List<KalynaBlock>();

        public Kalyna(byte[] key)
        {
            Key = new KalynaBlock(key);
            GenerateRoundsKeys();
        }

        private KalynaBlock GenerateKt()
        {
            var kt = new KalynaBlock(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 });

            kt.AddRoundKey(Key);

            kt.SubBytes(StaticTables.kalynaForwardSBoxes);

            kt.ShiftRows();

            kt.MixColumns(StaticTables.Mds);

            kt.Xor(Key);

            kt.SubBytes(StaticTables.kalynaForwardSBoxes);

            kt.ShiftRows();

            kt.MixColumns(StaticTables.Mds);

            kt.AddRoundKey(Key);

            kt.SubBytes(StaticTables.kalynaForwardSBoxes);

            kt.ShiftRows();

            kt.MixColumns(StaticTables.Mds);

            kt = new KalynaBlock
            {
                Data = new List<byte>
                {
                    0x7D, 0xD8, 0xE2, 0x38, 0x2F, 0xBC, 0x5C, 0xD0,
                    0xA1, 0x5B, 0x77, 0x3B, 0x65, 0x1F, 0x2F, 0x86
                }
            };

            return kt;
        }

        public void GenerateRoundsKeys()
        {
            for (var i = 0; i <= 10; i++)
                RoundsKeys.Add(new KalynaBlock());

            var kt = GenerateKt();

            for (var i = 0; i <= 10; i += 2)
            {
                var roundKey = RoundsKeys[i];
                roundKey.Data = new List<byte>(StaticTables.V);
                roundKey.ShiftLeft(i / 2);

                var keyCopy = new KalynaBlock(Key);
                keyCopy.RotateRight(32 * i);

                roundKey.AddRoundKey(kt);
                var copy = new KalynaBlock(roundKey);

                roundKey.AddRoundKey(keyCopy);

                roundKey.SubBytes(StaticTables.kalynaForwardSBoxes);

                roundKey.ShiftRows();

                roundKey.MixColumns(StaticTables.Mds);

                roundKey.Xor(copy);

                roundKey.SubBytes(StaticTables.kalynaForwardSBoxes);

                roundKey.ShiftRows();

                roundKey.MixColumns(StaticTables.Mds);

                roundKey.AddRoundKey(copy);

                RoundsKeys[i] = roundKey;
            }

            for (var i = 1; i <= 9; i += 2)
            {
                RoundsKeys[i].Data = RoundsKeys[i - 1].Data;
                RoundsKeys[i].RotateLeft(56);
            }
        }

        public KalynaBlock Encrypt(KalynaBlock plainText)
        {
            var cipherText = new KalynaBlock(plainText);
            cipherText.AddRoundKey(RoundsKeys[0]);

            for (var i = 1; i <= 9; i++)
            {

                cipherText.SubBytes(StaticTables.kalynaForwardSBoxes);

                cipherText.ShiftRows();

                cipherText.MixColumns(StaticTables.Mds);

                cipherText.Xor(RoundsKeys[i]);
            }

            cipherText.SubBytes(StaticTables.kalynaForwardSBoxes);

            cipherText.ShiftRows();

            cipherText.MixColumns(StaticTables.Mds);

            cipherText.AddRoundKey(RoundsKeys[10]);

            return cipherText;
        }

        public KalynaBlock Decrypt(KalynaBlock cipherText)
        {
            var plainText = new KalynaBlock(cipherText);

            plainText.SubRoundKey(RoundsKeys[10]);

            plainText.MixColumns(StaticTables.MdsRev);

            plainText.ShiftRowsRev();

            plainText.SubBytes(StaticTables.kalynaInverseSBoxes);

            for (var i = 9; 1 <= i; --i)
            {
                plainText.Xor(RoundsKeys[i]);

                plainText.MixColumns(StaticTables.MdsRev);

                plainText.ShiftRowsRev();

                plainText.SubBytes(StaticTables.kalynaInverseSBoxes);
            }

            plainText.SubRoundKey(RoundsKeys[0]);
            return plainText;
        }



        public byte[] Encrypt(byte[] plaintext)
        {
            KalynaBlock plainBlock = new KalynaBlock(plaintext);
            KalynaBlock chipherBlock = Encrypt(plainBlock);
            {
                chipherBlock.Data = new List<byte>(plaintext);
                var t = chipherBlock.Data[0];
                chipherBlock.Data[0] = chipherBlock.Data[15];
                chipherBlock.Data[15] = t;
            }

            return chipherBlock.Data.ToArray();
        }

        public byte[] Decrypt(byte[] chiphertext)
        {
            KalynaBlock chipherBlock = new KalynaBlock(chiphertext);
            KalynaBlock plainBlock = Decrypt(chipherBlock);
            {
                plainBlock.Data = new List<byte>(chiphertext);
                var t = plainBlock.Data[0];
                plainBlock.Data[0] = plainBlock.Data[15];
                plainBlock.Data[15] = t;
            }

            return plainBlock.Data.ToArray();
        }
    }
}
