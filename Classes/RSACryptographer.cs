using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab1_Encryption_.Classes
{
    class RSACryptographer : ICryptographer
    {
        protected ulong p;
        protected ulong q;
        protected ulong n; 
        protected ulong Euler;
        protected ulong d;
        protected ulong e;

        public void SetKeys(ulong p, ulong q)
        {
            if (!(IsSimple(p)&&IsSimple(q)))
            {
                throw new ArgumentException("Args is not simple.");
            }
            this.p = p;
            this.q = q;

            n = p * q;
            Euler = (p - 1) * (q - 1);
            d = Calculate_d(Euler);
            e = Calculate_e(d, Euler);
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
            var result = new List<ulong>();

            BigInteger b;

            for(int i = 0; i < data.Length; i++)
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

        public byte[] Decrypt(byte[] data)
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
    }
}
