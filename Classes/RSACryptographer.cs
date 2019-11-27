using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace lab1_Encryption_.Classes
{
    class RSACryptographer : ICryptographer
    {
        public ulong p;
        public ulong q;
        public ulong n; 
        protected ulong Euler;
        protected ulong d;
        public ulong e;

        public RSACryptographer(ulong p, ulong q)
        {
            SetKeys(p, q);
        }
        public void SetKeys(ulong p, ulong q)
        {
            this.p = p;
            this.q = q;

            if (CheckKeys())
            {
                n = p * q;
                Euler = (p - 1) * (q - 1);
                d = Calculate_d(Euler);
                e = Calculate_e(d, Euler);
            }
            else
            {
                this.p = 0;
                this.q = 0;
                e = 0;
                n = 0;
                d = 0;
                Euler = 0;
            }
        }

        public bool CheckKeys()
        {
            return IsSimple(p) && IsSimple(q);
        }

        private ulong Calculate_d(ulong Euler)
        {
            ulong d = Euler - 1;

            for (ulong i = 2; i <= Euler; i++)
                if ((Euler % i == 0) && (d % i == 0)) //если имеют общие делители
                {
                    d--;
                    i = 1;
                }

            return d;
        }

        private ulong Calculate_e(ulong d, ulong Euler)
        {
            ulong e = 10;

            while (true)
            {
                if ((e * d) % Euler == 1)
                    break;
                else
                    e++;
            }

            return e;
        }

        public byte[] Encrypt(byte[] data)
        {
            if (CheckKeys())
            {
                var result = new List<ulong>();

                BigInteger b;

                for (int i = 0; i < data.Length; i++)
                {
                    b = new BigInteger(data[i]);
                    b = BigInteger.Pow(b, (int)e);  // try ModPow

                    b = b % n;  // check

                    result.Add((ulong)b);
                }

                #region UInt64[] to byte[]

                List<byte> resultBytes = new List<byte>();

                foreach (ulong value in result)
                {
                    var bytes = BitConverter.GetBytes(value);
                    resultBytes.AddRange(bytes);
                }

                #endregion

                return resultBytes.ToArray();
            }
            else
            {
                return Encoding.ASCII.GetBytes("ERROR: Keys is not simple.");
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            if (CheckKeys())
            {
                #region byte[] to UInt64[]

                List<ulong> dataLong = new List<ulong>();

                for (int i = 0; i < data.Length; i += 8)
                {
                    var value = BitConverter.ToUInt32(data, i);
                    dataLong.Add(value);
                }

                #endregion

                var result = new List<byte>();

                BigInteger b;

                for (int i = 0; i < dataLong.Count; i++)
                {
                    b = new BigInteger(dataLong[i]);
                    b = BigInteger.Pow(b, (int)d);  // try ModPow

                    b = b % n;  // check n_

                    result.Add((byte)b);
                }

                return result.ToArray();
            }
            else
            {
                return Encoding.ASCII.GetBytes("ERROR: Keys is not simple.");
            }
        }
        /// <summary>
        /// Проверяет, простое ли число
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsSimple(ulong n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            for (ulong i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }
        /// <summary>
        /// Генератор массива простых чисел.
        /// </summary>
        /// <param name="toGenerate"> Количество простых чисел.</param>
        /// <returns></returns>
        public static List<int> GeneratePrimes(int toGenerate)
        {
            var primes = new List<int>();
            primes.Add(2);
            primes.Add(3);
            while (primes.Count < toGenerate)
            {
                int nextPrime = (int)(primes[primes.Count - 1]) + 2;
                while (true)
                {
                    bool isPrime = true;
                    foreach (int n in primes)
                    {
                        if (nextPrime % n == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                    {
                        break;
                    }
                    else
                    {
                        nextPrime += 2;
                    }
                }
                primes.Add(nextPrime);
            }
            return primes;
        }
        public static List<int> GeneratePrimesNaive(int n)
        {
            List<int> primes = new List<int>();
            primes.Add(2);
            int nextPrime = 3;
            while (primes.Count < n)
            {
                int sqrt = (int)Math.Sqrt(nextPrime);
                bool isPrime = true;
                for (int i = 0; (int)primes[i] <= sqrt; i++)
                {
                    if (nextPrime % primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(nextPrime);
                }
                nextPrime += 2;
            }
            return primes;
        }
    }
}
