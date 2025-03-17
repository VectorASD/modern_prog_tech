using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp {
    public class BigDecimal:
        ANumber,
        IEquatable<BigDecimal>,
        IComparable<BigDecimal> {

        public static readonly BigDecimal Zero = new();
        public static readonly BigDecimal One = new(BigInt.One);
        public static readonly BigDecimal MinusOne = new(BigInt.MinusOne);

        public static readonly char DOT_CHAR = '.';

        // Реальное значение числа: Number / NumberSystem ^ CountAfterDot

        private readonly BigInt number;
        public int CountAfterDot { get; }

        public BigDecimal(BigInteger value, int numSys = 10, int countAfterDot = 0) {
            ArgumentOutOfRangeException.ThrowIfNegative(countAfterDot);

            int digitCount = CountDigits(value, numSys);
            TrimRight(ref countAfterDot, ref value, ref digitCount, numSys);

            number = new BigInt(value, numSys, digitCount);
            CountAfterDot = countAfterDot;
        }
        public BigDecimal():
            this(BigInt.Zero) {
        }

        // ~~~ Конвертеры-повышайки ~~~
        public BigDecimal(BigInt value, int countAfterDot = 0) {
            number = value;
            CountAfterDot = countAfterDot;
        }
        public override BigDecimal GetDecimal => this;
        public override BigRational GetRational => new(this);
        public override BigComplex GetComplex => new(this);



        public BigInteger Number => number.Number;
        public int NumberSystem => number.NumberSystem;

        private BigInteger MakeItHaveThisAmountOfFloatDigits(int countAfterDot) {
            int diff = countAfterDot - CountAfterDot;
            ArgumentOutOfRangeException.ThrowIfNegative(diff);

            if (diff == 0)
                return number.Number; // число уже с нужным количеством цифр после точки

            return number.Number * BigInteger.Pow(number.NumberSystem, diff);
        }



        public bool Equals(BigDecimal? other) => CompareTo(other) == 0;
        public override bool Equals(object? obj) => Equals(obj as BigDecimal);

        public int CompareTo(BigDecimal? other) {
            if (other is null) return 1; // Всё, что угодно, больше, чем null

            BigInteger a, b;
            if (number.NumberSystem == other.NumberSystem) {
                int countAfterDot = Math.Max(CountAfterDot, other.CountAfterDot);
                a = MakeItHaveThisAmountOfFloatDigits(countAfterDot);
                b = other.MakeItHaveThisAmountOfFloatDigits(countAfterDot);
            } else {
                a = number.Number * BigInteger.Pow(other.NumberSystem, other.CountAfterDot);
                b = other.Number * BigInteger.Pow(number.NumberSystem, CountAfterDot);
            }
            return a.CompareTo(b);
        }

        private int CachedHash = 0;
        public override int GetHashCode() {
            if (CachedHash == 0)
                CachedHash = 31 * (31 + number.GetHashCode()) + CountAfterDot;
            return CachedHash;
        }



        public override bool IsZero => number.IsZero;

        public static ANumber operator +(BigDecimal a, BigDecimal b) {
            int numSys = a.number.NumberSystem;
            if (numSys == b.number.NumberSystem) {
                int countAfterDot = Math.Max(a.CountAfterDot, b.CountAfterDot);
                BigInteger a_num = a.MakeItHaveThisAmountOfFloatDigits(countAfterDot);
                BigInteger b_num = b.MakeItHaveThisAmountOfFloatDigits(countAfterDot);
                return new BigDecimal(new BigInt(a_num + b_num, numSys), countAfterDot);
            }
            return new BigRational(a) + new BigRational(b);
        }
        public static ANumber operator -(BigDecimal a, BigDecimal b) {
            int numSys = a.number.NumberSystem;
            if (numSys == b.number.NumberSystem) {
                int countAfterDot = Math.Max(a.CountAfterDot, b.CountAfterDot);
                BigInteger a_num = a.MakeItHaveThisAmountOfFloatDigits(countAfterDot);
                BigInteger b_num = b.MakeItHaveThisAmountOfFloatDigits(countAfterDot);
                return new BigDecimal(new BigInt(a_num - b_num, numSys), countAfterDot);
            }
            return new BigRational(a) - new BigRational(b);
        }
        public static ANumber operator *(BigDecimal a, BigDecimal b) {
            int numSys = a.number.NumberSystem;
            if (numSys == b.number.NumberSystem) {
                int countAfterDot = Math.Max(a.CountAfterDot, b.CountAfterDot);
                BigInteger a_num = a.MakeItHaveThisAmountOfFloatDigits(countAfterDot);
                BigInteger b_num = b.MakeItHaveThisAmountOfFloatDigits(countAfterDot);
                return new BigDecimal(new BigInt(a_num * b_num, numSys), countAfterDot * 2);
            }
            return new BigRational(a) * new BigRational(b);
        }
        public static ANumber operator /(BigDecimal a, BigDecimal b) =>
            new BigRational(a) / new BigRational(b);

        public static BigDecimal operator +(BigDecimal a) => a;
        public static BigDecimal operator -(BigDecimal a) => new(-a.number, a.CountAfterDot);

        public override BigRational Inverse() =>
            new BigRational(this).Inverse();
        public override BigDecimal Square() =>
            new(BigInteger.Pow(number.Number, 2), CountAfterDot * 2);

        public static bool TryParse(string stringValue, out BigDecimal result, int numSys = 10) {
            int dot_idx = stringValue.IndexOf(DOT_CHAR);
            bool valid;
            BigInt int_result;
            if (dot_idx == -1) {
                valid = BigInt.TryParse(stringValue, out int_result, numSys);
                if (!valid) { result = Zero; return false; }
                result = new BigDecimal(int_result);
                return true;
            }
            if (stringValue.IndexOf(DOT_CHAR, dot_idx + 1) != -1) {
                result = Zero; return false; }

            string withoutDot = stringValue[..dot_idx] + stringValue[(dot_idx+1)..];
            valid = BigInt.TryParse(withoutDot, out int_result, numSys);
            if (!valid) { result = Zero; return false; }

            int countAfterDot = withoutDot.Length - dot_idx;
            result = new BigDecimal(int_result, countAfterDot);
            return true;
        }
        public static BigDecimal Parse(string stringValue, int numSys = 10) {
            if (!TryParse(stringValue, out var result, numSys))
                throw new FormatException("Недопустимый формат BigDecimal в " + numSys + "-ричной системе счисления:\n" + stringValue);
            return result;
        }

        public override void ToString(StringBuilder sb) {
            string withoutDot = number.ToString();
            int dot_idx = withoutDot.Length - CountAfterDot;
            sb.Append($"{withoutDot[..dot_idx]}{DOT_CHAR}{withoutDot[dot_idx..]}");
        }

        public override string Raw => $"{number.Number} (digits: {number.DigitCount}) / {number.NumberSystem} ^ {CountAfterDot}";
    }
}
