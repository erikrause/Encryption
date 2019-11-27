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

        public byte[] Decrypt(byte[] data)
        {
            List<byte> decryptedData = new List<byte>();

            foreach (byte b in data)
            {
                byte newByte = BitShift(b, -Bias);
                decryptedData.Add(newByte);
            }

            return decryptedData.ToArray();
        }

        public byte[] Encrypt(byte[] data)
        {
            List<byte> encryptedData = new List<byte>();

            foreach (byte b in data)
            {
                byte newByte = BitShift(b, Bias);
                encryptedData.Add(newByte);
            }

            return encryptedData.ToArray();
        }

        protected byte BitShift(byte ch, int bias)
        {
            //int bitsInChar = sizeof(char) * 8;
            byte newByte;
            byte transfer = new byte();
            Func<byte> shift;

            if (bias >= 0)
            {
                shift = delegate()
                {
                    transfer = (byte)(ch << (8 - bias));
                    newByte = (byte)(ch >> bias);
                    newByte = (byte)(newByte | transfer);
                    return newByte;
                };

            }
            else
            {
                bias = -bias;
                shift = delegate()
                {
                    transfer = (byte)(ch >> (8 - bias));
                    newByte = (byte)(ch << bias);
                    newByte = (byte)(newByte | transfer);
                    return newByte;
                };
            }
            newByte = shift();

            return newByte;
        }
    }
}
