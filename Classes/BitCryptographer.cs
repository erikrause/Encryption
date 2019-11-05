using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Encryption_.Classes
{
    class BitCryptographer : ICryptographer
    {
        // Смещение битов
        public int Bias;

        public BitCryptographer(int bias)
        {
            Bias = bias;
        }

        public string Decrypt(string text)
        {
            int bitsInChar = sizeof(char) * 8;
            string decryptedText = "";

            foreach (char ch in text)//(int i; i < text.Length; i++)
            {
                char transfer = (char)(ch >> (bitsInChar - Bias));
                char newChar = (char)(ch << Bias);
                newChar = (char)(newChar | transfer);
                decryptedText += newChar;
            }

            return decryptedText;
        }

        public string Encrypt(string text)
        {
            int bitsInChar = sizeof(char) * 8;
            string encryptedText = "";

            foreach (char ch in text)//(int i; i < text.Length; i++)
            {
                char transfer = (char)(ch << (bitsInChar - Bias));
                char newChar = (char)(ch >> Bias);
                newChar = (char)(newChar | transfer);
                encryptedText += newChar;
            }

            return encryptedText;
        }
    }
}
