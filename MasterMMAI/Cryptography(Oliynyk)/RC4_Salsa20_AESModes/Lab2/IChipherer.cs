namespace Lab2
{
    interface IChipherer
    {
        byte[] Encrypt(byte[] plaintext);

        byte[] Decrypt(byte[] chiphertext);
    }
}
