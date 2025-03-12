using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NumberSystemControlLibrary {
    public static class BigIntegerExtensions {
        public static BigInteger DivRoundNearest(this BigInteger dividend, BigInteger divisor) {
            if (divisor == 0) throw new DivideByZeroException();

            BigInteger quotient = BigInteger.DivRem(dividend, divisor, out BigInteger remainder);
            if (remainder < 0) { // заменяем rem на mod
                quotient--;
                remainder += divisor;
            }

            // Определяем, нужно ли округлять вверх
            if (remainder * 2 >= divisor) return quotient + 1;
            return quotient;
        }
    }
}
