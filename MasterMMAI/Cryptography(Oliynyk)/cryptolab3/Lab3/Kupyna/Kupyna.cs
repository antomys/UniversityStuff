namespace Lab3.Kupyna
{
    public class Kupyna : IHashFunc
    {
        uint[] M256 = new uint[64]
        {
            0x40, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0
        };

        public byte[] CalcHash(byte[] message)
        {
            byte[] padded = MessagePreprocessHelper.GetPaddedMessage512(message);

            byte[] resultBlockOperation = processBlockOperation(padded);

            return resultBlockOperation;
        }


        private byte[] processBlockOperation(byte[] messageByBlocks)
        {

            byte[] messageForTPlusCircle = XOR(messageByBlocks, M256);

            byte[] messageForTPlus = messageByBlocks;

            byte[] messageAfterTPlusCircle = TPCircle.generateMessageAfterTPlusCircle(messageForTPlusCircle);

            byte[] messageAfterTPlus = TPlus.generateMessageAfterTPlus(messageForTPlus);

            byte[] messageAfterLastBlockOperation = lastBlockOperation(messageAfterTPlusCircle, messageAfterTPlus);

            byte[] messageAfterFinalOperation = finalOperation(messageAfterLastBlockOperation);

            byte[] cutMessage = cutTo256Bit(messageAfterFinalOperation);

            return cutMessage;
        }

        private byte[] lastBlockOperation(byte[] messageAfterTPlusCircle, byte[] messageAfterTPlus)
        {
            byte[] xorInputParameters = XOR(messageAfterTPlusCircle, messageAfterTPlus);

            return XOR(xorInputParameters, M256);
        }

        private byte[] finalOperation(byte[] messageAfterLastBlockOperation)
        {
            byte[] messageForXOR = messageAfterLastBlockOperation;

            byte[] messageAfterTPlusCircle = TPCircle.generateMessageAfterTPlusCircle(messageAfterLastBlockOperation);

            return XOR(messageForXOR, messageAfterTPlusCircle);
        }

        private byte[] cutTo256Bit(byte[] messageAfterFinalOperation)
        {
            byte[] message256Bit = new byte[32];

            for (int i = 0; i < 32; i++)
            {
                message256Bit[i] = messageAfterFinalOperation[32 + i];
            }

            return message256Bit;
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
    }
}
