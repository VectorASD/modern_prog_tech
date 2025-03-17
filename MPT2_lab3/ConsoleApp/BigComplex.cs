using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp {
    public class BigComplex(BigRational real, BigRational imag) :
        ANumber,
        IEquatable<BigComplex>,
        IComparable<BigComplex> {

        public static readonly BigComplex Zero = new();
        public static readonly BigComplex One = new(BigRational.One);
        public static readonly BigComplex MinusOne = new(BigRational.MinusOne);

        public static readonly string I_PLUS_CHAR = "+i";
        public static readonly string I_MINUS_CHAR = "-i";

        // Реальное значение числа: real + i * imaginary, i = sqrt(-1)

        private readonly BigRational real = real;
        private readonly BigRational imaginary = imag;

        public BigRational Real => real;
        public BigRational Imaginary => imaginary;

        public BigComplex() :
            this(BigRational.Zero, BigRational.Zero) {
        }

        // ~~~ Конвертеры-повышайки ~~~
        public BigComplex(BigInt number):
            this(new BigRational(number.Number), BigRational.Zero) {
        }
        public BigComplex(BigDecimal number) :
            this(new BigRational(number), BigRational.Zero) {
        }
        public BigComplex(BigRational number) :
            this(number, BigRational.Zero) {
        }
        public override BigComplex GetComplex => this;



        public bool Equals(BigComplex? other) => CompareTo(other) == 0;
        public override bool Equals(object? obj) => Equals(obj as BigComplex);

        public int CompareTo(BigComplex? other) {
            if (other is null) return 1; // Всё, что угодно, больше, чем null

            if (real == other.real && imaginary == other.imaginary) return 0;

            throw new NotImplementedException("Комплексные числа НЕЛЬЗЯ сравнивать компаратором (на <, >, <=, >=)");
        }
        public static bool operator ==(BigComplex a, BigComplex b)
            => a.real == b.real && a.imaginary == b.imaginary; // обход try {} catch {} в ANumber
        public static bool operator !=(BigComplex a, BigComplex b)
            => a.real != b.real || a.imaginary != b.imaginary; // обход try {} catch {} в ANumber

        private int CachedHash = 0;
        public override int GetHashCode() {
            if (CachedHash == 0)
                CachedHash = 31 * (31 + real.GetHashCode()) + imaginary.GetHashCode();
            return CachedHash;
        }



        public override bool IsZero => real.IsZero && imaginary.IsZero; // если да, то это центр координат
        public static BigComplex operator +(BigComplex a, BigComplex b) =>
            new(a.real + b.real, a.imaginary + b.imaginary);
        public static BigComplex operator -(BigComplex a, BigComplex b) =>
            new(a.real - b.real, a.imaginary - b.imaginary);
        public static BigComplex operator *(BigComplex a, BigComplex b) =>
            new(a.real * b.real - a.imaginary * b.imaginary, a.real * b.imaginary + a.imaginary * b.real);
        public static BigComplex operator /(BigComplex a, BigComplex b) {
            BigRational div = a.imaginary.Square() + b.imaginary.Square();
            BigRational real = (a.real * b.real + a.imaginary * b.imaginary) / div;
            BigRational imag = (a.imaginary * b.real - a.real * b.imaginary) / div;
            return new(real, imag);
        }

        public static BigComplex operator +(BigComplex a) => a;
        public static BigComplex operator -(BigComplex a) => new(-a.real, a.imaginary);

        public override BigComplex Inverse() {
            // a.real = 1, a.imaginary = 0, b = this
            //BigRational div = 0.Square() + imaginary.Square();
            //BigRational real = (1 * real + 0 * imaginary) / div;
            //BigRational imag = (0 * real - 1 * imaginary) / div; Получили единичную матрицу 2x2
            //return new(real, imag);
            BigRational div = imaginary.Square();
            return new(real / div, imaginary.Inverse());
            // imaginary / div -> BigRational.One / imaginary -> imaginary.Inverse()
        }
        public override BigComplex Square() =>
            new(real.Square() - imaginary.Square(), real * imaginary * BigRational.Two);

        public static bool TryParse(string stringValue, out BigComplex result, int numSys = 10) {
            int div_idx = stringValue.IndexOf(I_PLUS_CHAR);
            bool negative = div_idx == -1;
            int char_size = I_PLUS_CHAR.Length; // даже это предусмотрел
            if (negative) {
                div_idx = stringValue.IndexOf(I_MINUS_CHAR);
                char_size = I_MINUS_CHAR.Length;
            }
            bool valid;
            if (div_idx == -1) {
                valid = BigRational.TryParse(stringValue, out BigRational rational_result, numSys);
                if (!valid) { result = Zero; return false; }
                result = new BigComplex(rational_result);
                return true;
            }

            valid = BigRational.TryParse(stringValue[..div_idx], out BigRational raal_result, numSys);
            if (!valid) { result = Zero; return false; }

            valid = BigRational.TryParse(stringValue[(div_idx + char_size)..], out BigRational imag_result, numSys);
            if (!valid) { result = Zero; return false; }
            if (negative) imag_result = -imag_result;

            result = new BigComplex(raal_result, imag_result);
            return true;
        }
        public static BigComplex Parse(string stringValue, int numSys = 10) {
            if (!TryParse(stringValue, out var result, numSys))
                throw new FormatException("Недопустимый формат BigComplex в " + numSys + "-ричной системе счисления:\n" + stringValue);
            return result;
        }

        public override void ToString(StringBuilder sb) {
            int sign = imaginary.Numerator.Sign;
            if (sign == 0) {
                real.ToString(sb);
                return;
            }

            bool positive = sign == 1;
            BigRational abs_imag = positive ? imaginary : -imaginary;

            real.ToString(sb);
            sb.Append(positive ? I_PLUS_CHAR : I_MINUS_CHAR);
            abs_imag.ToString(sb);
        }

        public override string Raw => $"{real} +i* {imaginary}";
    }
}
