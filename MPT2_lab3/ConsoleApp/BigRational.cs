using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp {
    public class BigRational:
        ANumber,
        IEquatable<BigRational>,
        IComparable<BigRational> {

        public static readonly BigRational Zero = new();
        public static readonly BigRational Two = new(new BigInteger(2));
        public static readonly BigRational One = new(BigInteger.One);
        public static readonly BigRational MinusOne = new(BigInteger.MinusOne);

        public static readonly char DIV_CHAR = '/';

        // Реальное значение числа: numerator / denominator

        private readonly BigInteger numerator;
        private readonly BigInteger denominator;

        public BigInteger Numerator => numerator;
        public BigInteger Denominator => denominator;

        public BigRational() :
            this(BigInteger.Zero, BigInteger.One) {
        }
        public BigRational(BigInteger value) :
            this(value, BigInteger.One) {
        }
        public BigRational(BigInteger num, BigInteger denom) {
            int sign = denom.Sign;
            if (sign == 0) {
                // особый случай: 0/0=0, а не бусконечность, т.е. не будет DivideByZeroException
                // if (num.IsZero) { numerator = BigInteger.Zero; denominator = BigInteger.One; return; }
                
                // но решено, что лучше этот случай НЕ рассматривать, поскольку, иначе, можно в комплексных числах делить на 0 когда угодно
                
                throw new DivideByZeroException();
            }
            // забавно, что это единственное место с делением на 0
            // и сюдя можно попасть откуда угодно, где это же деление на 0

            var gcd = BigInteger.GreatestCommonDivisor(num, denom); // >= 1
            if (sign == -1) gcd = -gcd;

            numerator = num / gcd;
            denominator = denom / gcd; // Всегда положительное!
        }

        // ~~~ Конвертеры-повышайки ~~~
        public BigRational(BigInt number) {
            numerator = number.Number;
            denominator = BigInteger.One;
        }
        public BigRational(BigDecimal number) :
            this(number.Number, BigInteger.Pow(number.NumberSystem, number.CountAfterDot)) {
        }
        public override BigRational GetRational => this;
        public override BigComplex GetComplex => new(this);



        public bool Equals(BigRational? other) => CompareTo(other) == 0;
        public override bool Equals(object? obj) => Equals(obj as BigRational);

        public int CompareTo(BigRational? other) {
            if (other is null) return 1; // Всё, что угодно, больше, чем null

            if (denominator == other.denominator)
                return numerator.CompareTo(other.numerator);
            // num/denom [op] num2/denom2 (домножаем на denom*denom2)
            // num*denom2 [op] num2*denom
            return (numerator * other.denominator).CompareTo(other.numerator * denominator);
        }

        private int CachedHash = 0;
        public override int GetHashCode() {
            if (CachedHash == 0)
                CachedHash = 31 * (31 + numerator.GetHashCode()) + denominator.GetHashCode();
            return CachedHash;
        }



        public override bool IsZero => numerator.IsZero;
        public static BigRational operator +(BigRational a, BigRational b) =>
            new(a.numerator * b.denominator + b.numerator * a.denominator, a.denominator * b.denominator);
        public static BigRational operator -(BigRational a, BigRational b) =>
            new(a.numerator * b.denominator - b.numerator * a.denominator, a.denominator * b.denominator);
        public static BigRational operator *(BigRational a, BigRational b) =>
            new(a.numerator * b.numerator, a.denominator * b.denominator);
        public static BigRational operator /(BigRational a, BigRational b) =>
            new(a.numerator * b.denominator, a.denominator * b.numerator);

        public static BigRational operator +(BigRational a) => a;
        public static BigRational operator -(BigRational a) => new(-a.numerator, a.denominator);

        public override BigRational Inverse() =>
            new(denominator, numerator);
        public override BigRational Square() =>
            new(numerator * numerator, denominator * denominator);

        public static bool TryParse(string stringValue, out BigRational result, int numSys = 10) {
            if (string.IsNullOrEmpty(stringValue)) { result = Zero; return false; }

            int div_idx = stringValue.IndexOf(DIV_CHAR);
            bool valid;
            if (div_idx == -1) {
                valid = BigDecimal.TryParse(stringValue, out BigDecimal decimal_result, numSys);
                if (!valid) { result = Zero; return false; }
                result = new BigRational(decimal_result);
                return true;
            }
            if (stringValue.IndexOf(DIV_CHAR, div_idx + 1) != -1) {
                result = Zero; return false; }

            valid = BigDecimal.TryParse(stringValue[..div_idx], out BigDecimal num_result, numSys);
            if (!valid) { result = Zero; return false; }

            valid = BigDecimal.TryParse(stringValue[(div_idx + 1)..], out BigDecimal denom_result, numSys);
            if (!valid) { result = Zero; return false; }

            result = new BigRational(num_result) / new BigRational(denom_result);
            return true;
        }
        public static BigRational Parse(string stringValue, int numSys = 10) {
            if (!TryParse(stringValue, out var result, numSys))
                throw new FormatException("Недопустимый формат BigRational в " + numSys + "-ричной системе счисления:\n" + stringValue);
            return result;
        }

        public override void ToString(StringBuilder sb, int _ = 0) {
            sb.Append(numerator);
            if (denominator == BigInteger.One) return;
            sb.Append(DIV_CHAR);
            sb.Append(denominator);
        }

        public override string Raw => this.ToString().Replace(DIV_CHAR.ToString(), $" {DIV_CHAR} "); // $"{numerator} / {denominator}";
    }
}
