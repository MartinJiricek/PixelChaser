using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelChaser
{
    public class RandomGenerator
    {
        readonly System.Security.Cryptography.RNGCryptoServiceProvider csp;

        public RandomGenerator()
        {
            csp = new System.Security.Cryptography.RNGCryptoServiceProvider();
        }

        public int Next(int minValue, int maxExclusiveValue)
        {

            if (minValue > maxExclusiveValue)
            {
                int tmp = minValue;
                minValue = maxExclusiveValue;
                maxExclusiveValue = tmp;
            }
            else if (minValue == maxExclusiveValue)
                return minValue;

            long diff = (long)maxExclusiveValue - minValue;
            long upperBound = uint.MaxValue / diff * diff;

            uint ui;
            do
            {
                ui = GetRandomUInt();
            } while (ui >= upperBound);
            return (int)(minValue + (ui % diff));
        }

        private uint GetRandomUInt()
        {
            var randomBytes = GenerateRandomBytes(sizeof(uint));
            return BitConverter.ToUInt32(randomBytes, 0);
        }

        private byte[] GenerateRandomBytes(int bytesNumber)
        {
            byte[] buffer = new byte[bytesNumber];
            csp.GetBytes(buffer);
            return buffer;
        }
    }
}
