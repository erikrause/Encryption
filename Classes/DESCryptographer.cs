using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Encryption_.Classes
{
    class DESCryptographer : ICryptographer
    {
        public static readonly int[] pc_1 ={ 57, 49, 41, 33, 25, 17, 9, 1, 58, 50,
                                             42, 34, 26, 18, 10, 2, 59, 51, 43, 35,
                                             27, 19, 11, 3, 60, 52, 44, 36, 63, 55,
                                             47, 39, 31, 23, 15, 7, 62, 54, 46, 38,
                                             30, 22, 14, 6, 61, 53, 45, 37, 29, 21,
                                             13, 5, 28, 20, 12, 4 };

        public static readonly int[,] s1 ={ { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9,
                                               0, 7 },
                                             { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9,
                                               5, 3, 8 },
                                             { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3,
                                               10, 5, 0 },
                                             { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10,
                                               0, 6, 13 } };
        public DESCryptographer(string key)
        {
            Key = key;
        }

        protected string Key;
        public string Decrypt(string text)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string text)
        {
            throw new NotImplementedException();
        }

    }
}

