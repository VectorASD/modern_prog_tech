using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NumberSystemControlLibrary {
    // Частично позаимствовано у PFZ_Math
    // Все numSys и поддержка A-F цифр введена уже мною
    // Теперь это не совсем BigDecimal, а нечто среднее между BigDecimal и BigRational

    public class BigDecimal:
        IEquatable<BigDecimal>,
        IComparable<BigDecimal> {

        public static readonly BigDecimal Zero = new(0);
        public static readonly BigDecimal One = new(1);
        public static readonly BigDecimal Two = new(2);



        public void Serialize(BinaryWriter writer) {
            byte[] value = (IsNegative ? -RawValue : RawValue).ToByteArray();
            writer.Write(value.Length);
            writer.Write(value);
            writer.Write(NumberSystem);
            writer.Write(CountAfterDot);
        }
        public static BigDecimal Deserialize(BinaryReader reader) {
            int count = reader.ReadInt32();
            BigInteger value = new(reader.ReadBytes(count));
            int numSys = reader.ReadInt32();
            int countAfterDot = reader.ReadInt32();
            return new(value, countAfterDot, numSys);
        }



        public BigInteger RawValue { get; }
        public int NumberSystem { get; }
        public int CountAfterDot { get; }
        public bool IsNegative { get; }

        public int DigitCount { get; }



        public BigDecimal(BigInteger value, int numSys = 10) :
            this(value, 0, numSys) {
        }

        public BigDecimal(BigInteger value, int countAfterDot, int numSys) {
            ArgumentOutOfRangeException.ThrowIfNegative(countAfterDot);

            IsNegative = value < 0;
            if (IsNegative)
                value = -value;

            int digitCount = CountDigits(value, numSys);
            TrimRight(ref countAfterDot, ref value, ref digitCount, numSys);

            RawValue = value;
            NumberSystem = numSys;
            DigitCount = digitCount;
            CountAfterDot = countAfterDot;
        }



        // Реальное значение числа: _rawValue / (_numberSystem ^ _countAfterDot) * (-1 ^ _isNegative);
        public string Raw => (IsNegative ? "-" : "") + $"{RawValue} / {NumberSystem} ^ {CountAfterDot}";
        public string Format(string name) => $"{name}: {this}   ({Raw})";



        // Исходя из этой формулы, становится совершенно очевидно, что при уменьшении _numberSystem,
        // будет увеличиваться _countAfterDot так, чтобы их степень была НЕ меньше исходной.
        // Во сколько раз увеличивается делитель _rawValue, во столько и увеличивается сам _rawValue

        public BigDecimal ToNumberSystem(int numSys) { // гвоздь всей программы
            // double log_divider = BigInteger.Log(BigInteger.Pow(_numberSystem, _countAfterDot)) / BigInteger.Log(numSys);
            // Выносим степень из под логарифма:
            double log_divider = CountAfterDot * BigInteger.Log(NumberSystem) / Math.Log(numSys); // опа, пропорция! неожиданно
            int newCountAfterDot = Convert.ToInt32(Math.Ceiling(log_divider));
            // _rawValue * (_numberSystem ^ _countAfterDot) / (numSys ^ newCountAfterDot)

            // Пример с приминением python:
            // 1234567890123456789 / (10 ** 10) -> 123456789.01234567
            // 1234567890123456789 * (2 ** 33) / (10 ** 10) -> 1.0604857425543936e+18
            // round(1.0604857425543936e+18) -> 1060485742554393600
            // 1060485742554393600 / (2 ** 33) -> 123456789.01234567 (абсолютное тоже самое число, что и при 10 ** 10)
            BigInteger newValue = (RawValue * BigInteger.Pow(numSys, newCountAfterDot)).DivRoundNearest(BigInteger.Pow(NumberSystem, CountAfterDot));

            return new BigDecimal(IsNegative ? -newValue : newValue, newCountAfterDot, numSys);
        }



        // Есть ли более быстрый подход, который работает с BigIntegers?
        // Похоже, что Log10 вовсе не быстрее.
        // К тому же, теперь здесь основание Log задаётся через numSys
        private static int CountDigits(BigInteger value, int numSys = 10) {
            int count = 0;

            while (value > 0) {
                count++;
                value /= numSys;
            }

            return count;
        }
        private static void TrimRight(ref int countAfterDot, ref BigInteger value, ref int digitCount, int numSys = 10) {
            // Допустим, при numSys = 10, ввели число 123.4000, получилось value = 1234000, countAfterDot = 4
            // Это тоже самое, что просто value = 1234, countAfterDot = 1, т.к. оно также значит 123.4
            while (countAfterDot > 0 && (value % numSys) == 0) {
                countAfterDot--;
                value /= numSys;
                digitCount--;
            }
        }



        // ~~~ Парсер (строка в BigDecimal) ~~~

        public static int ParseChar(char letter) {
            if (letter >= '0' && letter <= '9') return letter - '0';
            if (letter >= 'A' && letter <= 'Z') return letter - 'A' + 10;
            if (letter >= 'a' && letter <= 'z') return letter - 'a' + 10;
            return -1;
        }
        public static bool TryParse(string stringValue, out BigDecimal result, int numSys = 10) {
            if (string.IsNullOrEmpty(stringValue)) {
                result = new BigDecimal(0, numSys);
                return false;
            }

            int i = 0;
            int length = stringValue.Length;
            BigInteger bigInteger = 0;

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

            // Считываем целую часть до точки
            // Обратите внимание, что этот код позволяет считать .5 так же, как и 0.5.
            // Это особенность, а не ошибка.
            while (i < length) {
                char c = stringValue[i++];
                if (c == '.') break;

                int digit = ParseChar(c);
                if (digit == -1 || digit >= numSys) {
                    result = new BigDecimal(0, numSys);
                    return false;
                }

                bigInteger *= numSys;
                bigInteger += digit;
            }

            // Считываем дробную часть до самого конца.
            // Если после точки ничего нет, не страшно
            int countAfterDot = 0;
            while (i < length) {
                char c = stringValue[i++];

                int digit = ParseChar(c);
                if (digit == -1 || digit >= numSys) {
                    result = new BigDecimal(0, numSys);
                    return false;
                }

                bigInteger *= numSys;
                bigInteger += digit;

                countAfterDot++;
            }

            result = new BigDecimal(isNegative ? -bigInteger : bigInteger, countAfterDot, numSys);
            return true;
        }

        public static BigDecimal Parse(string stringValue, int numSys = 10) {
            if (!TryParse(stringValue, out var result, numSys))
                throw new FormatException("Недопустимый формат BigDecimal в " + numSys + "-ричной системе счисления:\n" + stringValue);
            return result;
        }



        // ~~~ Конвертация (BigDecimal в строку) ~~~

        public static char StringifyDigit(int digit) {
            if (digit < 10) return (char)('0' + digit);
            return (char)('a' + (digit - 10));
        }

        public void ToString(StringBuilder sb) {
            ArgumentNullException.ThrowIfNull(sb);

            if (RawValue == 0) {
                sb.Append('0');
                return;
            }

            if (IsNegative) sb.Append('-');


            bool dotWritten = false;
            if (CountAfterDot >= DigitCount) {
                sb.Append("0.");
                dotWritten = true;

                var decimalIndexFromEnd = CountAfterDot - 1;
                while (decimalIndexFromEnd >= DigitCount) {
                    sb.Append('0');
                    decimalIndexFromEnd--;
                }
            }

            var divisorIndex = DigitCount - CountAfterDot;
            var divisor = BigInteger.Pow(NumberSystem, DigitCount - 1);

            for (int i = 0; i < DigitCount; i++) {
                if (!dotWritten && i == divisorIndex)
                    sb.Append('.');

                int digitValue = (int) ((RawValue / divisor) % NumberSystem);
                sb.Append(StringifyDigit(digitValue));
                divisor /= NumberSystem;
            }
        }
        public override string ToString() {
            if (RawValue == 0) return "0";

            StringBuilder sb = new();
            ToString(sb);
            return sb.ToString();
        }



        // ~~~ IEquatable ~~~

        public bool Equals(BigDecimal? other) =>
            other is not null && this == other;




        // ~~~ IComparable ~~~

        public int CompareTo(BigDecimal? other) {
            if (other is null) return 1;

            if (IsNegative != other.IsNegative) {
                if (IsNegative) return -1;
                return 1;
            }

            var intA = this.RawValue * BigInteger.Pow(other.NumberSystem, other.CountAfterDot);
            var intB = other.RawValue * BigInteger.Pow(this.NumberSystem, this.CountAfterDot);
            // не надёжный алгоритм после конвертирования системы счисления

            if (IsNegative)
                return intB.CompareTo(intA);

            return intA.CompareTo(intB);
        }



        // ~~~ Дополнение для хеш-таблиц, сортировок и т.п. ~~~

        private int CachedHash = 0;
        public override int GetHashCode() {
            if (CachedHash == 0)
                CachedHash = 31 * (31 * (31 *
                    (31 + RawValue.GetHashCode()) +
                    IsNegative.GetHashCode()) +
                    NumberSystem.GetHashCode()) +
                    CountAfterDot.GetHashCode();
            return CachedHash;
        }

        public override bool Equals(object? obj) {
            if (obj is not BigDecimal num) return false;
            return Equals(num);
        }
        public static bool operator ==(BigDecimal a, BigDecimal b) =>
            a.IsNegative == b.IsNegative &&
            a.RawValue * BigInteger.Pow(b.NumberSystem, b.CountAfterDot) ==
                b.RawValue * BigInteger.Pow(a.NumberSystem, a.CountAfterDot);
        public static bool operator !=(BigDecimal a, BigDecimal b) =>
            !(a == b);

        public static bool operator >(BigDecimal a, BigDecimal b) =>
            a.CompareTo(b) > 0;
        public static bool operator >=(BigDecimal a, BigDecimal b) =>
            a.CompareTo(b) >= 0;
        public static bool operator <(BigDecimal a, BigDecimal b) =>
            a.CompareTo(b) < 0;
        public static bool operator <=(BigDecimal a, BigDecimal b) =>
            a.CompareTo(b) <= 0;
    }
}
