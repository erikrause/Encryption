using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace lab1_Encryption_.Classes
{
    public class GOSTCryptographer : ICryptographer
    {
        public static readonly int[] IP ={ 57, 49, 41, 33, 25, 17, 9, 1, 58, 50,
                                             42, 34, 26, 18, 10, 2, 59, 51, 43, 35,
                                             27, 19, 11, 3, 60, 52, 44, 36, 63, 55,
                                             47, 39, 31, 23, 15, 7, 62, 54, 46, 38,
                                             30, 22, 14, 6, 61, 53, 45, 37, 29, 21,
                                             13, 5, 28, 20, 12, 4 };

        public static readonly int[,] S1 ={ { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9,
                                               0, 7 },
                                             { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9,
                                               5, 3, 8 },
                                             { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3,
                                               10, 5, 0 },
                                             { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10,
                                               0, 6, 13 } };
        public static readonly int[] E =
        {
            32,1,2,3,4,5,
            4,5,6,7,8,9,
            8,9,10,11,12,13,
            12,13,14,15,16,17,
            16,17,18,19,20,21,
            24,25,26,27,28,29,
            28,29,30,31,32,1
        };

        public GOSTCryptographer(string key)
        {
            Key = key;
        }

        protected BitArray _key;
        public string Key
        {
            set
            {
                _key = new BitArray(Encoding.UTF8.GetBytes(value));
                _key.Length = 256;
            }
        }
        public string Decrypt(string text)
        {
            var X = GenerateSubkeys();
            int numberOfBlocks = (text.Length / 32 + 1) * 32;
            var input = new BitArray(Encoding.UTF8.GetBytes(text));
            input.Length = numberOfBlocks * 8;

            for (int round = 0; round < 32; round++)
            {
                //A
            }
            throw new NotImplementedException();
        }

        protected BitArray[] GenerateSubkeys()
        {
            var K = new BitArray[8];
            var X = new BitArray[32];
            int i, j;

            for (i = 0; i < K.Length; i++)
            {
                K[i] = new BitArray(32);

                for (j = 0; j < K[i].Length; j++)
                {
                    K[i][j] = _key[(_key.Length / K.Length) * i + j];
                }
            }

            j = 0;
            for (i = 0; i < 24; i++)
            {
                X[i] = K[j];
                j++;
                if (j == 8) j = 0;
            }

            j = 7;
            for (i = 24; i < 32; i++)
            {
                X[i] = K[j];
                j--;
            }
            return X;
        }

        public string Encrypt(string text)
        {
            throw new NotImplementedException();
        }

    }
}

