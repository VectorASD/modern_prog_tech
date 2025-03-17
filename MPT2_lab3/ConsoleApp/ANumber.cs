using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp {
    public abstract class ANumber:
        IEquatable<ANumber>,
        IComparable<ANumber> {

        // ~~~ Конвертеры-повышайки ~~~

        // BigInt -> BigDecimal -> BigRational -> BigComplex
        public virtual BigInt GetInteger => throw new NotImplementedException();
        public virtual BigDecimal GetDecimal => throw new NotImplementedException();
        public virtual BigRational GetRational => throw new NotImplementedException();
        public virtual BigComplex GetComplex => throw new NotImplementedException();



        // ~~~ IEquatable<INumber> ~~~
        public bool Equals(ANumber? other) => CompareTo(other) == 0;

        public override bool Equals(object? other) => other is ANumber number && Equals(number);

        public override abstract int GetHashCode();



        // ~~~ IComparable<INumber> ~~~

        public int CompareTo(ANumber? other) {
            if (other is null) return 1; // Всё, что угодно, больше, чем null

            if (this is BigComplex c_left) return c_left.CompareTo(other.GetComplex);
            if (other is BigComplex c_right) return GetComplex.CompareTo(c_right);
            if (this is BigRational r_left) return r_left.CompareTo(other.GetRational);
            if (other is BigRational r_right) return GetRational.CompareTo(r_right);
            if (this is BigDecimal d_left) return d_left.CompareTo(other.GetDecimal);
            if (other is BigDecimal d_right) return GetDecimal.CompareTo(d_right);
            return GetInteger.CompareTo(other.GetInteger);
        }

        public static bool operator ==(ANumber a, ANumber b) {
            try { return a.CompareTo(b) == 0; }
            catch (NotImplementedException) { return false; } // в случае комплексных чисел
        }
        public static bool operator !=(ANumber a, ANumber b) {
            try { return a.CompareTo(b) != 0; }
            catch (NotImplementedException) { return true; } // в случае комплексных чисел
        }
        public static bool operator >(ANumber a, ANumber b) => a.CompareTo(b) > 0;
        public static bool operator >=(ANumber a, ANumber b) => a.CompareTo(b) >= 0;
        public static bool operator <(ANumber a, ANumber b) => a.CompareTo(b) < 0;
        public static bool operator <=(ANumber a, ANumber b) => a.CompareTo(b) <= 0;



        // ~~~ Остальное ~~~

        public abstract bool IsZero { get; }

        public static ANumber operator +(ANumber a, ANumber b) {
            if (a is BigComplex c_left) return c_left + b.GetComplex;
            if (b is BigComplex c_right) return a.GetComplex + c_right;
            if (a is BigRational r_left) return r_left + b.GetRational;
            if (b is BigRational r_right) return a.GetRational + r_right;
            if (a is BigDecimal d_left) return d_left + b.GetDecimal;
            if (b is BigDecimal d_right) return a.GetDecimal + d_right;
            return a.GetInteger + b.GetInteger;
        }

        public static ANumber operator -(ANumber a, ANumber b) {
            if (a is BigComplex c_left) return c_left - b.GetComplex;
            if (b is BigComplex c_right) return a.GetComplex - c_right;
            if (a is BigRational r_left) return r_left - b.GetRational;
            if (b is BigRational r_right) return a.GetRational - r_right;
            if (a is BigDecimal d_left) return d_left - b.GetDecimal;
            if (b is BigDecimal d_right) return a.GetDecimal - d_right;
            return a.GetInteger - b.GetInteger;
        }

        public static ANumber operator *(ANumber a, ANumber b) {
            if (a is BigComplex c_left) return c_left * b.GetComplex;
            if (b is BigComplex c_right) return a.GetComplex * c_right;
            if (a is BigRational r_left) return r_left * b.GetRational;
            if (b is BigRational r_right) return a.GetRational * r_right;
            if (a is BigDecimal d_left) return d_left * b.GetDecimal;
            if (b is BigDecimal d_right) return a.GetDecimal * d_right;
            return a.GetInteger * b.GetInteger;
        }

        public static ANumber operator /(ANumber a, ANumber b) {
            if (a is BigComplex c_left) return c_left / b.GetComplex;
            if (b is BigComplex c_right) return a.GetComplex / c_right;
            if (a is BigRational r_left) return r_left / b.GetRational;
            if (b is BigRational r_right) return a.GetRational / r_right;
            if (a is BigDecimal d_left) return d_left / b.GetDecimal;
            if (b is BigDecimal d_right) return a.GetDecimal / d_right;
            return a.GetInteger / b.GetInteger;
        }

        public static ANumber operator +(ANumber a) => a;
        public static ANumber operator -(ANumber a) {
            return a switch {
                BigInt i => -i,
                BigDecimal d => -d,
                BigRational r => -r,
                BigComplex c => -c,
                _ => throw new NotImplementedException()
            };
        }

        public abstract ANumber Square();
        public abstract ANumber Inverse();

        // public static abstact bool TryParse(string stringValue, out T result, int numSys); static и abstract конфликтуют
        // public static abstact T Parse(string stringValue, int numSys);                     static и abstract конфликтуют
        public abstract void ToString(StringBuilder sb);
        public override string ToString() {
            if (IsZero) return "0";

            StringBuilder sb = new();
            ToString(sb);
            return sb.ToString();
        }

        public abstract string Raw { get; }



        // ~~~ Утилиты ~~~

        internal static int ParseChar(char letter) {
            if (letter >= '0' && letter <= '9') return letter - '0';
            if (letter >= 'A' && letter <= 'Z') return letter - 'A' + 10;
            if (letter >= 'a' && letter <= 'z') return letter - 'a' + 10;
            return -1;
        }
        internal static char StringifyDigit(int digit) {
            if (digit < 10) return (char)('0' + digit);
            return (char)('a' + (digit - 10));
        }
        internal static int CountDigits(BigInteger value, int numSys = 10) {
            if (value.IsZero) return 1;

            int count = 0;

            while (!value.IsZero) { // все итерации цикла быстрее, чем BigInteger.Log
                count++;
                value /= numSys;
            }

            return count;
        }
        internal static void TrimRight(ref int countAfterDot, ref BigInteger value, ref int digitCount, int numSys = 10) {
            // Допустим, при numSys = 10, ввели число 123.4000, получилось value = 1234000, countAfterDot = 4
            // Это тоже самое, что просто value = 1234, countAfterDot = 1, т.к. оно также значит 123.4
            while (countAfterDot > 0 && (value % numSys) == 0) {
                countAfterDot--;
                value /= numSys;
                digitCount--;
            }
        }
    }
}
