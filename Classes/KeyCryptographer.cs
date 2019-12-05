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
            List<int> encryptedData = new List<int>();
            // Byte[] to int[]:
            for(int i = 0; i < data.Length / 4; i++)
            {
                encryptedData.Add(BitConverter.ToInt32(data, i * 4));
            }

            List<byte> decryptedData = new List<byte>();
            int pos = 1;

            foreach (int b in encryptedData)
            {
                byte newByte = (byte)Calculate(b, pos, -_key);
                decryptedData.Add(newByte);
                pos++;
            }

            return decryptedData.ToArray();
        }

        public byte[] Encrypt(byte[] data)
        {
            List<int> encryptedData = new List<int>();
            int pos = 1;

            foreach (int value in data)
            {
                int newValue = Calculate(value, pos, _key);
                encryptedData.Add(newValue);
                pos++;
            }

            List<byte> encryptedBytes = new List<byte>();
            // Int[] to byte[]:
            foreach(int value in encryptedData)
            {
                encryptedBytes.AddRange(BitConverter.GetBytes(value));
            }

            return encryptedBytes.ToArray();
        }

        private int Calculate(int value, int pos, int Key)
        {
            int newValue;
            newValue = (value + (pos * Key));

            return newValue;
        }
    }
}
