using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Interfaces
{
    interface IChipherer
    {
        byte[] Encrypt(byte[] plaintext);

        byte[] Decrypt(byte[] chiphertext);
    }
}
