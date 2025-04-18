﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp {
    public class BigInt(BigInteger number, int numSys, int digitCount) :
        ANumber,
        IEquatable<BigInt>,
        IComparable<BigInt> {
        // нельзя сделать readonly struct, по скольку ANumber - не интерфейс

        public static readonly BigInt Zero = new();
        public static readonly BigInt One = new(BigInteger.One);
        public static readonly BigInt MinusOne = new(BigInteger.MinusOne);

        // BigInt - просто обёртка поверх BigInteger,
        // с расширением Parse, TryParse, ToString для поддержки систем счисления.
        // Реальное значение числа: number

        private readonly BigInteger number = number;
        private readonly int numSys = numSys;
        private readonly int digitCount = digitCount;

        public BigInteger Number => number;
        public int NumberSystem => numSys;
        public int DigitCount => digitCount;

        public BigInt() :
            this(BigInteger.Zero, 10) {
        }
        public BigInt(BigInteger num, int numSys = 10):
            this(num, numSys, CountDigits(num, numSys)) {
        }

        // ~~~ Конвертеры-повышайки ~~~
        public override BigInt GetInteger => this;
        public override BigDecimal GetDecimal => new(this);
        public override BigRational GetRational => new(this);
        public override BigComplex GetComplex => new(this);



        public bool Equals(BigInt? other) => CompareTo(other) == 0;
        public override bool Equals(object? obj) => Equals(obj as BigInt);

        public int CompareTo(BigInt? other) {
            if (other is null) return 1; // Всё, что угодно, больше, чем null
            return number.CompareTo(other.number);
        }

        private int CachedHash = 0;
        public override int GetHashCode() {
            if (CachedHash == 0)
                CachedHash = 31 * (31 + number.GetHashCode()) + numSys;
            return CachedHash;
        }



        public override bool IsZero => number.IsZero;
        public static BigInt operator +(BigInt a, BigInt b) =>
            new(a.number + b.number, Math.Max(a.numSys, b.numSys));
        public static BigInt operator -(BigInt a, BigInt b) =>
            new(a.number - b.number, Math.Max(a.numSys, b.numSys));
        public static BigInt operator *(BigInt a, BigInt b) =>
            new(a.number * b.number, Math.Max(a.numSys, b.numSys));
        public static BigRational operator /(BigInt a, BigInt b) =>
            new(a.number, b.number);

        public static BigInt operator +(BigInt a) => a;
        public static BigInt operator -(BigInt a) => new(-a.number, a.numSys);

        public override BigRational Inverse() =>
            new(BigInteger.One, number);
        public override BigInt Square() =>
            new(number * number, numSys);

        public static bool TryParse(string stringValue, out BigInt result, int numSys = 10) {
            if (string.IsNullOrEmpty(stringValue)) { result = Zero; return false; }

            int i = 0;
            int length = stringValue.Length;

            // Проверяем первый символ на минус
            bool isNegative = false;
            if (stringValue[i] == '-') {
                isNegative = true;
                i++;
            }

            // Пропускаем незначимые нули
            while (i < length) {
                char c = stringValue[i];
                if (c != '0') break;
                i++;
            }

            // Считываем целую часть, недопуская точку
            BigInteger bigInteger = 0;
            while (i < length) {
                char c = stringValue[i++];

                int digit = ParseChar(c);
                if (digit == -1 || digit >= numSys) {
                    result = new BigInt(0, numSys);
                    return false;
                }

                bigInteger *= numSys;
                bigInteger += digit;
            }

            result = new BigInt(isNegative ? -bigInteger : bigInteger, numSys);
            return true;
        }
        public static BigInt Parse(string stringValue, int numSys = 10) {
            if (!TryParse(stringValue, out var result, numSys))
                throw new FormatException("Недопустимый формат BigInt в " + numSys + "-ричной системе счисления:\n" + stringValue);
            return result;
        }

        public override void ToString(StringBuilder sb, int countAfterDot = 0) {
            ArgumentNullException.ThrowIfNull(sb);

            int sign = number.Sign;
            if (sign == 0) {
                sb.Append('0');
                return;
            }

            BigInteger num = sign == -1 ? -number : number;
            if (sign == -1) sb.Append('-');

            int digitCount_m1 = digitCount - 1;
            var divisor = BigInteger.Pow(numSys, digitCount_m1);

            int i = digitCount_m1;
            for (; i >= countAfterDot; i--) {
                int digitValue = (int)((num / divisor) % numSys);
                sb.Append(StringifyDigit(digitValue));
                divisor /= numSys;
            }
            if (i == digitCount_m1) sb.Append('0');
            if (i >= 0) sb.Append(BigDecimal.DOT_CHAR);
            for (; i >= 0; i--) {
                int digitValue = (int)((num / divisor) % numSys);
                sb.Append(StringifyDigit(digitValue));
                divisor /= numSys;
            }
        }

        public override string Raw => $"{this} (sys: {numSys}) (digits: {digitCount})";
    }
}
