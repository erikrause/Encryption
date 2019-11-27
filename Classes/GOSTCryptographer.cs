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
        public static readonly byte[,] S ={ { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9,
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

        // for debug:
        public static readonly byte[,] S1 = new byte[8, 16] {
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
        };

        public GOSTCryptographer(string key)
        {
            Key = key;
        }

        protected UInt32[] _keys;
        public string Key
        {
            set
            {
                //_key = new BitArray(Encoding.ASCII.GetBytes(value));
                //_key.Length = 256;
                _keys = new UInt32[8];

                var byteKey = new byte[32];

                Array.Copy(Encoding.ASCII.GetBytes(value), byteKey, value.Length);

                for (int i = 0; i < byteKey.Length; i += 4)
                {
                    UInt32 k = byteKey[i];
                    k = k << 8;
                    k = k | byteKey[i + 1];
                    k = k << 8;
                    k = k | byteKey[i + 2];
                    k = k << 8;
                    k = k | byteKey[i + 3];
                    _keys[i / 4] = k;
                }
            }
        }
        public byte[] Encrypt(byte[] data)
        {
            UInt64[] inputBlocks = GetBlocks(data);
            var outputBlocks = SimpleReplacement(inputBlocks, true);

            byte[] outputBytes = GetBytes(outputBlocks);
            return outputBytes;
        }
        public byte[] Decrypt(byte[] data)
        {
            UInt64[] inputBlocks = GetBlocks(data);
            var outputBlocks = SimpleReplacement(inputBlocks, false);
            byte[] outputBytes = GetBytes(outputBlocks);
            return outputBytes;
        }
        protected UInt64[] GetBlocks(byte[] input)
        {
            var inputBytes = input;
            // Проверка открытого текста на кратность 64 битам:
            int blocksCount = inputBytes.Length / 8;
            var mod = inputBytes.Length % 8;
            if (mod != 0)
            {
                blocksCount = (inputBytes.Length + (8 - mod)) / 8;
            }

            // Добавление нулей в конец блока:
            var inputBytesBlocks = new byte[blocksCount * 8];
            Array.Copy(inputBytes, inputBytesBlocks, inputBytes.Length);

            // From byte[] to uint[]:
            var Blocks = new UInt64[blocksCount];
            for (int i = 0; i < blocksCount; i++)
            {
                Blocks[i] = GetUint64From8Bytes(inputBytesBlocks, 8 * i);
            }


            return Blocks;      // check return
        }

        protected byte[] GetBytes(UInt64[] blocks)
        {
            byte[] bytes = new byte[blocks.Length * 8];

            for (int i = 0; i < blocks.Length; i++)
            {
                var temp = Get8BytesFromUInt64(blocks[i]);

                for (int bIndex = 0; bIndex < temp.Length; bIndex++)
                {
                    bytes[bIndex + i * 8] = temp[bIndex];
                }
            }

            return bytes;
        }
        public static UInt64 GetUint64From8Bytes(byte[] s, int startIndex)
        {
            UInt64 result = 0;
            for (int i = startIndex; i < startIndex + 8; i++)
            {
                int shift = (56 - 8 * i);
                result += ((UInt64)s[i] << shift);
            }
            return result;
        }
        public static byte[] Get8BytesFromUInt64(UInt64 s)
        {
            var result = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                int shift = (56 - 8 * i);
                result[i] = (byte)(s >> shift);
                s = s & (UInt64.MaxValue - ((UInt64)0xff << shift));
            }
            return result;
        }

        protected UInt64[] SimpleReplacement(UInt64[] inputBlocks, bool isEncryption)
        {
            var result = new UInt64[inputBlocks.Length];

            var cycleKeys = GenerateCycleKeys(_keys);
            // Reverse key on Decryption:
            if (!isEncryption)
            {
                cycleKeys = cycleKeys.Reverse().ToArray();
            }

            for (int i = 0; i < inputBlocks.Length; i++)
            {
                result[i] = CalculateBlock(inputBlocks[i], cycleKeys);
            }
            return result;
        }

        protected UInt64 CalculateBlock(UInt64 block, UInt32[] cycleKeys)
        {
            UInt64 result = block;

            foreach (var key in cycleKeys)
            {
                result = Step(result, key);
            }

            result = ((result & UInt32.MaxValue) << 32) | (result >> 32);

            return result;
        }

        protected UInt64 Step(UInt64 data, UInt32 keyPart)
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
        protected uint ReplaceValues(uint step1Value)     // change table size
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
        protected UInt32[] GenerateCycleKeys(UInt32[] keys)
        {
            UInt32[] CycleKeys = new UInt32[32];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    CycleKeys[j] = _keys[j];
                }
            }

            for (int j = 7; j >= 0; j--)
            {
                CycleKeys[j] = _keys[j];
            }

            return CycleKeys;
        }
    }
}

