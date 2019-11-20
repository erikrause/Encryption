using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Encryption_.Classes
{
    public class KeyCryptographer : ICryptographer
    {
        private int _key;
        public string Key
        {
            set
            {
                _key = 0;

                foreach (char ch in value)
                {
                    _key += ch;
                }
            }
        }
        public KeyCryptographer(string key)
        {
            Key = key;
        }
        public string Decrypt(string text)
        {
            string decryptedText = "";
            int pos = 1;

            foreach (char ch in text)
            {
                char newChar = Calculate(ch, pos, 1/_key);
                decryptedText += newChar;
                pos++;
            }

            return decryptedText;
        }

        public string Encrypt(string text)
        {
            string encryptedText = "";
            int pos = 1;

            foreach (char ch in text)
            {
                char newChar = Calculate(ch, pos, _key);
                encryptedText += newChar;
                pos++;
            }

            return encryptedText;
        }

        private char Calculate(char ch, int pos, int Key)
        {
            char newCh;
            newCh = (char)(ch + (pos * Key));

            return newCh;
        }
    }
}
