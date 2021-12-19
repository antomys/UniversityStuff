namespace Lab1.Interfaces
{
    interface IChipherer
    {
        byte[] Encrypt(byte[] plaintext);

        byte[] Decrypt(byte[] chiphertext);
    }
}
