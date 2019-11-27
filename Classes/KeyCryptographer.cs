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
        public byte[] Decrypt(byte[] data)
        {
            List<byte> decryptedData = new List<byte>();
            int pos = 1;

            foreach (byte b in data)
            {
                byte newByte = Calculate(b, pos, 1/_key);
                decryptedData.Add(newByte);
                pos++;
            }

            return decryptedData.ToArray();
        }

        public byte[] Encrypt(byte[] data)
        {
            List<byte> encryptedText = new List<byte>();
            int pos = 1;

            foreach (byte b in data)
            {
                byte newByte = Calculate(b, pos, _key);
                encryptedText.Add(newByte);
                pos++;
            }

            return encryptedText.ToArray();
        }

        private byte Calculate(byte b, int pos, int Key)    // need to debug decrypt!!!
        {
            byte newByte;
            newByte = (byte)(b + (pos * Key));

            return newByte;
        }
    }
}
