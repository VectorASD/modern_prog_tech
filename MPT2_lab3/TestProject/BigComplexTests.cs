using ConsoleApp;
using Newtonsoft.Json.Linq;
using System;
using System.Numerics;

namespace TestProject {
    [TestClass]
    public sealed class BigComplexTests {
        [TestMethod]
        public void Constructor0() {
            string expected = "0 +i* 0";

            BigComplex target = new();

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, "0 +i* 0")]
        [DataRow(1, "1 +i* 0")]
        [DataRow(-1, "-1 +i* 0")]
        [DataRow(123, "123 +i* 0")]
        public void Constructor1(int number, string expected) {
            BigRational value = new(number);

            BigComplex target = new(value);

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, 2, "0 +i* 2")]
        [DataRow(1, 2, "1 +i* 2")]
        [DataRow(-2, 4, "-2 +i* 4")]
        [DataRow(365, -20, "365 +i* -20")]
        [DataRow(-15, -10, "-15 +i* -10")]
        public void Constructor2(int real, int imag, string expected) {
            BigRational real2 = new(real);
            BigRational imaginary = new(imag);

            BigComplex target = new(real2, imaginary);

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(4, 0, false, null)]
        [DataRow(5, 0, true, 0)]
        [DataRow(6, 0, false, null)]
        [DataRow(4, 1, false, null)]
        [DataRow(5, 1, false, null)]
        [DataRow(6, 1, false, null)]
        public void Equals_Compare(int real, int imaginary, bool eq_expected, int? comp_expected) {
            BigComplex target = new(new BigRational(real), new BigRational(imaginary));
            BigComplex five = new(new BigRational(5), BigRational.Zero);
            int self_comp_expected = 0;

            // сравнение самого с собой

            bool eq_actual = target.Equals(target);
            int comp_actual = target.CompareTo(target);

            Assert.IsTrue(eq_actual);
            Assert.AreEqual(self_comp_expected, comp_actual);

            // сравнение с пятёркой

            eq_actual = target.Equals(five);
            Assert.AreEqual(eq_expected, eq_actual);

            if (comp_expected is not null) {
                comp_actual = target.CompareTo(five);

                Assert.AreEqual(comp_expected, comp_actual);
            } else {
                void action() => target.CompareTo(five);

                Assert.ThrowsException<NotImplementedException>(action);
            }
        }

        [TestMethod]
        [DataRow(-1, false)]
        [DataRow(0, true)]
        [DataRow(1, false)]
        public void IsZero(int number, bool expected) {
            BigRational value = new(number);
            BigComplex target = new(value);

            bool actual = target.IsZero;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 2, 82, 3, "97 +i* 5")]
        [DataRow(151, 3, 8, 1, "159 +i* 4")]
        [DataRow(-17, 2, -185, 3, "-202 +i* 5")]
        [DataRow(20, 1, -55, 3, "-35 +i* 4")]
        [DataRow(-55, 3, 20, 3, "-35 +i* 6")]
        public void Sum(int L_real, int L_imag, int R_real, int R_imag, string expected) {
            BigComplex L = new(new BigRational(L_real), new BigRational(L_imag));
            BigComplex R = new(new BigRational(R_real), new BigRational(R_imag));

            BigComplex target = L + R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 2, 82, 3, "-67 +i* -1")]
        [DataRow(151, 3, 8, 1, "143 +i* 2")]
        [DataRow(-17, 2, -185, 3, "168 +i* -1")]
        [DataRow(20, 1, -55, 3, "75 +i* -2")]
        [DataRow(-55, 3, 20, 3, "-75 +i* 0")]
        public void Subtract(int L_real, int L_imag, int R_real, int R_imag, string expected) {
            BigComplex L = new(new BigRational(L_real), new BigRational(L_imag));
            BigComplex R = new(new BigRational(R_real), new BigRational(R_imag));

            BigComplex target = L - R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 2, 82, 3, "1224 +i* 209")]
        [DataRow(151, 3, 8, 1, "1205 +i* 175")]
        [DataRow(-17, 2, -185, 3, "3139 +i* -421")]
        [DataRow(20, 1, -55, 3, "-1103 +i* 5")]
        [DataRow(-55, 3, 20, 3, "-1109 +i* -105")]
        public void Mul(int L_real, int L_imag, int R_real, int R_imag, string expected) {
            BigComplex L = new(new BigRational(L_real), new BigRational(L_imag));
            BigComplex R = new(new BigRational(R_real), new BigRational(R_imag));

            BigComplex target = L * R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 2, 82, 3, "1236/6733 +i* 119/6733")]
        [DataRow(151, 3, 8, 1, "1211/65 +i* -127/65")]
        [DataRow(-17, 2, -185, 3, "3151/34234 +i* -319/34234")]
        [DataRow(20, 1, -55, 3, "-1097/3034 +i* -115/3034")]
        [DataRow(-55, 3, 20, 3, "-1091/409 +i* 225/409")]
        public void Div(int L_real, int L_imag, int R_real, int R_imag, string expected) {
            BigComplex L = new(new BigRational(L_real), new BigRational(L_imag));
            BigComplex R = new(new BigRational(R_real), new BigRational(R_imag));

            BigComplex target = L / R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DivException() {
            BigComplex L = new(new BigRational(153), new BigRational(-123));
            BigComplex R = BigComplex.Zero;

            void action() => _ = L / R;

            Assert.ThrowsException<DivideByZeroException>(action);
        }

        // унарный плюс сквозной (всегда возвращает this), так что тестировать его нет смысла

        [TestMethod]
        [DataRow(-185, 2, "185 +i* 2")]
        [DataRow( -14, 3, "14 +i* 3")]
        [DataRow(   3, 2, "-3 +i* 2")]
        [DataRow(  12, -5, "-12 +i* -5")]
        [DataRow( 181, 2, "-181 +i* 2")]
        public void UnarMinus(int real, int imaginary, string expected) {
            BigComplex value = new(new BigRational(real), new BigRational(imaginary));

            BigComplex target = -value;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(159, 2, "159/25285 +i* -2/25285")]
        [DataRow(-205, 3, "-205/42034 +i* -3/42034")]
        [DataRow(3, 5, "3/34 +i* -5/34")]
        [DataRow(-5, 8, "-5/89 +i* -8/89")]
        public void Inverse(int real, int imaginary, string expected) {
            BigComplex value = new(new BigRational(real), new BigRational(imaginary));

            BigComplex target = value.Inverse();

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InverseException() {
            BigComplex value = BigComplex.Zero;

            void action() => value.Inverse();

            Assert.ThrowsException<DivideByZeroException>(action);
        }

        [TestMethod]
        [DataRow(0, 123, "-15129 +i* 0")]
        [DataRow(151, 2, "22797 +i* 604")]
        [DataRow(20, -3, "391 +i* -120")]
        [DataRow(-3, -10, "-91 +i* 60")]
        [DataRow(-5, 2, "21 +i* -20")]
        public void Square(int real, int imaginary, string expected) {
            BigComplex value = new(new BigRational(real), new BigRational(imaginary));

            BigComplex target = value.Square();

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("123/77+i17", 8, "83/63 +i* 15")]
        [DataRow("10-i101010001/10", 2, "2 +i* -337/2")]
        [DataRow("+i1010100", 2, "0 +i* 84")]
        [DataRow("dead+ibeef", 16, "57005 +i* 48879")]
        public void Parse(string input, int numSys, string expected) {
            BigComplex target = BigComplex.Parse(input, numSys);

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("", 10, DisplayName = "Пустая строка")]
        [DataRow(null, 10, DisplayName = "null-строка")]
        [DataRow("dead+ibeef", 15, DisplayName = "Не хватает системы счисления")]
        [DataRow("dead-be+ief", 16, DisplayName = "Минус в середине")]
        [DataRow("Опааа!!!", 10, DisplayName = "Незарегистрированные символы")]
        public void ParseException(string input, int numSys) {
            void action() => BigComplex.Parse(input, numSys);

            Assert.ThrowsException<FormatException>(action);
        }

        // Тестировать ToString не имеет смысла, т.к. он уже вложен в Raw,
        // который и которым протестирвано уже ? раз!
    }
}
