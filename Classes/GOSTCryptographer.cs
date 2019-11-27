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
        public static readonly byte[] IP ={ 57, 49, 41, 33, 25, 17, 9, 1, 58, 50,
                                             42, 34, 26, 18, 10, 2, 59, 51, 43, 35,
                                             27, 19, 11, 3, 60, 52, 44, 36, 63, 55,
                                             47, 39, 31, 23, 15, 7, 62, 54, 46, 38,
                                             30, 22, 14, 6, 61, 53, 45, 37, 29, 21,
                                             13, 5, 28, 20, 12, 4 };

        public static readonly byte[,] S1 ={ { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9,
                                               0, 7 },
                                             { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9,
                                               5, 3, 8 },
                                             { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3,
                                               10, 5, 0 },
                                             { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10,
                                               0, 6, 13 },
                                             { 6, 12, 7, 1, 5, 15, 13, 8, 4, 10, 9, 14, 0, 3, 11, 2 },
                                             { 4, 11, 10, 0, 7, 2, 1, 13, 3, 6, 8, 5, 9, 12, 15, 14 },
                                             { 13, 11, 4, 1, 3, 15, 5, 9, 0, 10, 14, 7, 6, 8, 2, 12},
                                             { 1, 15, 13, 0, 5, 7, 10, 4, 9, 2, 3, 14, 6, 11, 8, 12 } };

        public static readonly byte[,] S0 = new byte[8, 16];

        public static readonly byte[,] S = new byte[8, 16] { 
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
        };

        public static readonly byte[] E =
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

        protected UInt32[] _key;
        public string Key
        {
            set
            {
                //_key = new BitArray(Encoding.ASCII.GetBytes(value));
                //_key.Length = 256;
                _key = new UInt32[8];

                for (int i = 0; i < value.Length; i += 4)
                {
                    UInt32 k = value[i];
                    k = k << 8;
                    k = k | value[i + 1];
                    k = k << 8;
                    k = k | value[i + 2];
                    k = k << 8;
                    k = k | value[i + 3];
                    _key[i / 4] = k;
                }
            }
        }
        public string Encrypt(string text)
        {
            UInt64[] inputBlocks = GetBlocks(text);

            var outputBits = SimpleReplacement(inputBlocks, true);

            var outputBytes = new byte[outputBits.Length / 8];
            outputBits.CopyTo(outputBytes, 0);


            return Encoding.ASCII.GetString(outputBytes);
        }

        protected UInt64[] GetBlocks(string input)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            // Проверка открытого текста на кратность 64 битам:
            int blocksCount = 0;
            var mod = inputBytes.Length % 8;
            if (mod != 0)
            {
                blocksCount = (inputBytes.Length + (8 - mod)) / 8;
            }

            var Blocks = new UInt64[blocksCount];
            var inputUints = inputBytes.Cast<UInt64>().ToArray();      // check return
            Array.Copy(inputUints, Blocks, inputUints.Length);      // check blocks
            
            return Blocks;      // check return
        }

        protected UInt64[] SimpleReplacement(UInt64[] inputBlocks, bool isEncryption)
        {
            var result = new UInt64[inputBlocks.Length];

            for (int i = 0; i < inputBlocks.Length; i++)
            {
                result[i] = CalculateBlock(inputBlocks[i]);
            }
            return result;
        }

        protected UInt64 CalculateBlock(UInt64 block)
        {
            UInt64 result = block;

            for (int i = 0; i < 3; i++)     // почему 3??
            {
                for (int j = 0; j < 8; j++)
                {
                    result = this.MainCryptoStep(result, _key[j]);
                }
            }

            for (int j = 7; j >= 0; j--)
            {
                result = this.MainCryptoStep(result, _key[j]);
            }

            result = ((result & UInt32.MaxValue) << 32) | (result >> 32);

            return result;
        }

        protected UInt64 MainCryptoStep(UInt64 data, UInt32 keyPart)
        {
            #region шаг 0 - разбивание UInt64 на два UInt32

            var n2 = (UInt32)(data >> 32);
            var n1 = (UInt32)(data & UInt32.MaxValue);

            #endregion

            #region шаг 1 - сложение по модулю 2^32

            UInt32 step1Value = n1 + keyPart;

            #endregion

            #region шаг 2 - замена

           uint step2Value = ReplaceValues(step1Value);

            #endregion

            #region шаг 3 - сдвиг влево на 11
            
            uint step3Value = BitShift(step2Value, -11);

            #endregion

            #region шаг 4 - сложение по модулю 2

            uint step4Value = step3Value ^ n2;

            #endregion

            #region шаг 5 - сдвиг по цепочке

            n2 = n1;
            n1 = step4Value;

            #endregion

            #region шаг 6 - возврат полученного значения

            UInt64 step6Value = (UInt64)n2 << 32 | n1;

            #endregion

            return step6Value;
        }
        private uint ReplaceValues(uint step1Value)     // change table size
        {
            uint result = 0;
            for (int i = 0; i < 8; i++)
            {
                result <<= 4;
                int shift = 32 - 4 - 4 * i;
                uint index = (step1Value >> shift) & 0xf;
                step1Value = step1Value & (UInt32.MaxValue - ((UInt32)0xf << shift));
                result += S[7 - i, index];
            }
            return result;      
        }
        protected UInt32 BitShift(UInt32 block, int bias)
        {
            //int bitsInChar = sizeof(char) * 8;
            UInt32 newBlock;
            UInt32 transfer = new UInt32();
            Func<UInt32> shift;

            if (bias >= 0)
            {
                shift = delegate ()
                {
                    transfer = (UInt32)(block << (32 - bias));
                    newBlock = (UInt32)(block >> bias);
                    newBlock = (UInt32)(newBlock | transfer);
                    return newBlock;
                };

            }
            else
            {
                bias = -bias;
                shift = delegate ()
                {
                    transfer = (UInt32)(block >> (32 - bias));
                    newBlock = (UInt32)(block << bias);
                    newBlock = (UInt32)(newBlock | transfer);
                    return newBlock;
                };
            }
            newBlock = shift();

            return newBlock;        // check return.
        }


        protected BitArray Calculate(BitArray input, bool isEncryption)
        {
            var X = GenerateSubkeys(isEncryption);
            var outputBits = new BitArray(0);
            // Проверка открытого текста на кратность 64 битам:
            var mod = input.Length % 64;
            if (mod != 0)
            {
                input.Length += 64 - mod;
            }

            int numberOfBlocks = (input.Length / 64);

            for (int block = 0; block < numberOfBlocks; block++)
            {
                var A = new BitArray(32);
                var B = new BitArray(32);

                // Initializating inputs:
                for (int i = 0; i < 32; i++)
                {
                    A[i] = input[i + 64 * block];
                    B[i] = input[i + 64 * block + 32];
                }

                for (int round = 0; round < 32; round++)
                {
                    var f = Function(A, X[round], round);
                    var newA = B;
                    newA.Xor(f);
                    var newB = A;

                    A = newA;
                    B = newB;
                }
                outputBits = BitsAppend(outputBits, A);
                outputBits = BitsAppend(outputBits, B);
            }

            return outputBits;
        }
        public string Decrypt(string text)
        {
            var input = new BitArray(Encoding.ASCII.GetBytes(text));

            var outputBits = Calculate(input, false);

            var outputBytes = new byte[outputBits.Length / 8];
            outputBits.CopyTo(outputBytes, 0);

            return Encoding.ASCII.GetString(outputBytes);
        }

        protected BitArray Function(in BitArray A, in BitArray X, int round)
        {
            var newA = new BitArray(A);
            newA.Xor(X);
            var partsA = new BitArray[8];
            var outputBits = new BitArray(0);

            // DEBUG:
            byte[] bytes = new byte[newA.Count];
            newA.CopyTo(bytes, 0);
            ////////

            for (int i = 0; i < partsA.Length; i++)
            {
                partsA[i] = new BitArray(newA.Cast<bool>().Skip(i*4).Take(4).ToArray());
            }
            for (int i = 0; i < partsA.Length; i++)
            {
                var val = new byte[1];
                partsA[i].CopyTo(val, 0);
                byte[] newVal = new byte[] { S[i, val[0]] };

                partsA[i] = new BitArray(newVal);
                // From 8 bits to 4 bits:
                partsA[i] = new BitArray(partsA[i].Cast<bool>().Take(4).ToArray());
                
            }
            
            foreach (var part in partsA)
            {
                outputBits = BitsAppend(outputBits, part);
            }

            var output = BitShift(outputBits, 11);

            return output;
        }

        protected BitArray[] GenerateSubkeys(bool isEncryption)
        {
            var K = new BitArray[8];
            var X = new BitArray[32];
            int i, j;

            // K initialization:
            for (i = 0; i < K.Length; i++)
            {
                K[i] = new BitArray(32);

                for (j = 0; j < K[i].Length; j++)
                {
                    //K[i][j] = _key[(_key.Length / K.Length) * i + j];
                }
            }

            if (isEncryption)
            {
                // X initialization: 
                j = 0;
                for (i = 0; i < 24; i++)
                {
                    X[i] = K[j];
                    j++;
                    if (j == 8) j = 0;
                }

                // Reversed X initialization:
                j = 7;
                for (i = 24; i < 32; i++)
                {
                    X[i] = K[j];
                    j--;
                }
                return X;
            }
            else
            {
                // X initialization: 
                j = 0;
                for (i = 0; i < 8; i++)
                {
                    X[i] = K[j];
                    j++;
                }

                // Reversed X initialization:
                j = 7;
                for (i = 8; i < 32; i++)
                {
                    X[i] = K[j];
                    j--;
                    if (j == -1) j = 7;
                }
                return X;
            }
            
        }

        public BitArray BitsAppend(BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }
        public void BitsReverse(BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
        }
        protected BitArray BitShift(BitArray bitArray, int bias)
        {
            //int bitsInChar = sizeof(char) * 8;
            BitArray newBitArray = new BitArray(bitArray.Length);

            if (bias >= 0)
            {
                // Смещение вправо:
                for (int i = 0; i < bitArray.Length - bias; i++)
                {
                    newBitArray[i + bias] = bitArray[i];
                }
                // Перенос битов справа налево:
                for (int i = bitArray.Length - bias; i < bitArray.Length; i++)
                {
                    newBitArray[bitArray.Length - i - 1] = bitArray[i];
                }
            }
            else
            {
                bias = -bias;
                // Смещение влево:
                for (int i = 0; i < bitArray.Length - bias; i++)
                {
                    newBitArray[i] = bitArray[i + bias];
                }
                // Перенос битов:
                for (int i = bitArray.Length - bias; i < bitArray.Length; i++)
                {
                    newBitArray[i] = bitArray[bitArray.Length - i - 1];
                }
            }

            return newBitArray;
        }
    }
}

