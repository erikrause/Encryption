using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Encryption_.Classes
{
    public class BitCryptographer : ICryptographer
    {
        // Смещение битов
        public int Bias;

        public BitCryptographer(int bias)
        {
            Bias = bias;
        }

        public string Decrypt(string text)
        {
            string decryptedText = "";

            foreach (char ch in text)
            {
                char newChar = BitShift(ch, -Bias);
                decryptedText += newChar;
            }

            return decryptedText;
        }

        public string Encrypt(string text)
        {
            string encryptedText = "";

            foreach (char ch in text)
            {
                char newChar = BitShift(ch, Bias);
                encryptedText += newChar;
            }

            return encryptedText;
        }

        private char BitShift(char ch, int bias)
        {
            //int bitsInChar = sizeof(char) * 8;
            char newCh;
            char transfer = new char();
            Func<char> shift;

            if (bias >= 0)
            {
                shift = delegate()
                {
                    transfer = (char)(ch << (16 - bias));
                    newCh = (char)(ch >> bias);
                    newCh = (char)(newCh | transfer);
                    return newCh;
                };

            }
            else
            {
                bias = -bias;
                shift = delegate()
                {
                    transfer = (char)(ch >> (16 - bias));
                    newCh = (char)(ch << bias);
                    newCh = (char)(newCh | transfer);
                    return newCh;
                };
            }
            newCh = shift();

            return newCh;
        }
    }
}
