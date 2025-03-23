using ConsoleApp;
using System;
using System.Numerics;

namespace TestProject {
    [TestClass]
    public sealed class BigDecimalTests {
        [TestMethod]
        public void Constructor0() {
            string expected = "0 (digits: 1) / 10 ^ 0";

            BigDecimal target = new();

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, "0 (digits: 1) / 10 ^ 0")]
        [DataRow(1, "1 (digits: 1) / 10 ^ 0")]
        [DataRow(-1, "-1 (digits: 1) / 10 ^ 0")]
        [DataRow(123, "123 (digits: 3) / 10 ^ 0")]
        public void Constructor1(int number, string expected) {
            BigInteger value = new(number);

            BigDecimal target = new(value);

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, 2, 0, "0 (digits: 1) / 2 ^ 0")]
        [DataRow(1, 2, 0, "1 (digits: 1) / 2 ^ 0")]
        [DataRow(-1, 2, 0, "-1 (digits: 1) / 2 ^ 0")]
        [DataRow(123, 10, 1, "12.3 (digits: 3) / 10 ^ 1")]
        [DataRow(123, 2, 3, "1111.011 (digits: 7) / 2 ^ 3")]
        public void Constructor2_3(int number, int numSys, int countAfterDot, string expected) {
            BigInteger value = new(number);

            BigDecimal target = new(value, numSys, countAfterDot);

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(4, 2, false, -1)]
        [DataRow(5, 2, true, 0)]
        [DataRow(6, 2, false, 1)]
        [DataRow(4, 10, false, -1)]
        [DataRow(5, 10, true, 0)]
        [DataRow(6, 10, false, 1)]
        public void Equals_Compare(int number, int numSys, bool eq_expected, int comp_expected) {
            BigInteger value = new(number);
            BigDecimal target = new(value, numSys);
            BigDecimal five = new(5, 10);
            int self_comp_expected = 0;

            // сравнение самого с собой

            bool eq_actual = target.Equals(target);
            int comp_actual = target.CompareTo(target);

            Assert.IsTrue(eq_actual);
            Assert.AreEqual(self_comp_expected, comp_actual);

            // сравнение с пятёркой

            eq_actual = target.Equals(five);
            comp_actual = target.CompareTo(five);

            Assert.AreEqual(eq_expected, eq_actual);
            Assert.AreEqual(comp_expected, comp_actual);
        }

        [TestMethod]
        [DataRow(-1, false)]
        [DataRow(0, true)]
        [DataRow(1, false)]
        public void IsZero(int number, bool expected) {
            BigInteger value = new(number);
            BigDecimal target = new(value);

            bool actual = target.IsZero;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(-14, 2, 0, -185, 2, 1, "-1101010.1 (digits: 8) / 2 ^ 1")]
        [DataRow(15, 2, 0, 81, 2, 1, "110111.1 (digits: 7) / 2 ^ 1")]
        [DataRow(151, 10, 1, 8, 2, 0, "231 / 10")]
        [DataRow(20, 2, 0, -55, 10, 1, "29 / 2")]
        [DataRow(-55, 2, 1, 20, 3, 0, "-15 / 2")]
        public void Sum(int L_number, int L_numSys, int L_countAfterDot, int R_number, int R_numSys, int R_countAfterDot, string expected) {
            BigDecimal L = new(new BigInteger(L_number), L_numSys, L_countAfterDot);
            BigDecimal R = new(new BigInteger(R_number), R_numSys, R_countAfterDot);

            ANumber target = L + R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(-14, 2, 0, -185, 2, 1, "1001110.1 (digits: 8) / 2 ^ 1")]
        [DataRow(15, 2, 0, 81, 2, 1, "-11001.1 (digits: 6) / 2 ^ 1")]
        [DataRow(151, 10, 1, 8, 2, 0, "71 / 10")]
        [DataRow(20, 2, 0, -55, 10, 1, "51 / 2")]
        [DataRow(-55, 2, 1, 20, 3, 0, "-95 / 2")]
        public void Subtract(int L_number, int L_numSys, int L_countAfterDot, int R_number, int R_numSys, int R_countAfterDot, string expected) {
            BigDecimal L = new(new BigInteger(L_number), L_numSys, L_countAfterDot);
            BigDecimal R = new(new BigInteger(R_number), R_numSys, R_countAfterDot);

            ANumber target = L - R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(-14, 2, 0, -185, 2, 1, "10100001111.00 (digits: 13) / 2 ^ 2")]
        [DataRow(15, 2, 0, 81, 2, 1, "1001011111.10 (digits: 12) / 2 ^ 2")]
        [DataRow(151, 10, 1, 8, 2, 0, "604 / 5")]
        [DataRow(20, 2, 0, -55, 10, 1, "-110")]
        [DataRow(-55, 2, 1, 20, 3, 0, "-550")]
        public void Mul(int L_number, int L_numSys, int L_countAfterDot, int R_number, int R_numSys, int R_countAfterDot, string expected) {
            BigDecimal L = new(new BigInteger(L_number), L_numSys, L_countAfterDot);
            BigDecimal R = new(new BigInteger(R_number), R_numSys, R_countAfterDot);

            ANumber target = L * R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(-14, 2, 0, -185, 2, 1, "28 / 185")]
        [DataRow(15, 2, 0, 81, 2, 1, "10 / 27")]
        [DataRow(151, 10, 1, 8, 2, 0, "151 / 80")]
        [DataRow(20, 2, 0, -55, 10, 1, "-40 / 11")]
        [DataRow(-55, 2, 1, 20, 3, 0, "-11 / 8")]
        public void Div(int L_number, int L_numSys, int L_countAfterDot, int R_number, int R_numSys, int R_countAfterDot, string expected) {
            BigDecimal L = new(new BigInteger(L_number), L_numSys, L_countAfterDot);
            BigDecimal R = new(new BigInteger(R_number), R_numSys, R_countAfterDot);

            ANumber target = L / R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DivException() {
            BigDecimal L = new(new BigInteger(153), 10);
            BigDecimal R = BigDecimal.Zero;

            void action() => _ = L / R;

            Assert.ThrowsException<DivideByZeroException>(action);
        }

        // унарный плюс сквозной (всегда возвращает this), так что тестировать его нет смысла

        [TestMethod]
        [DataRow(-185, 1, "18.5 (digits: 3) / 10 ^ 1")]
        [DataRow( -14, 1, "1.4 (digits: 2) / 10 ^ 1")]
        [DataRow(   3, 1, "-0.3 (digits: 1) / 10 ^ 1")]
        [DataRow(  12, 1, "-1.2 (digits: 2) / 10 ^ 1")]
        [DataRow( 181, 1, "-18.1 (digits: 3) / 10 ^ 1")]
        public void UnarMinus(int number, int countAfterDot, string expected) {
            BigDecimal value = new(new BigInteger(number), 10, countAfterDot);

            BigDecimal target = -value;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(159, 3, "1000 / 159")]
        [DataRow(-205, 1, "-2 / 41")]
        [DataRow(3, 0, "1 / 3")]
        [DataRow(-5, 2, "-20")] // 1 / -0.05 = -20
        public void Inverse(int number, int countAfterDot, string expected) {
            BigDecimal value = new(new BigInteger(number), 10, countAfterDot);

            BigRational target = value.Inverse(); // опять наперёд...

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InverseException() {
            BigDecimal value = BigDecimal.Zero;

            void action() => value.Inverse();

            Assert.ThrowsException<DivideByZeroException>(action);
        }

        [TestMethod]
        [DataRow(0, 0, "0 (digits: 1) / 10 ^ 0")]
        [DataRow(1512, 2, "228.6144 (digits: 7) / 10 ^ 4")]
        [DataRow(-205, 1, "420.25 (digits: 5) / 10 ^ 2")]
        [DataRow(3, 3, "0.9 (digits: 1) / 10 ^ 6")]
        [DataRow(-5, 0, "25 (digits: 2) / 10 ^ 0")]
        public void Square(int number, int countAfterDot, string expected) {
            BigDecimal value = new(new BigInteger(number), 10, countAfterDot);

            BigDecimal target = value.Square();

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("0.", 8, "0 (digits: 1) / 8 ^ 0")]
        [DataRow(".101010001", 2, "0.101010001 (digits: 9) / 2 ^ 9")]
        [DataRow("-", 2, "0 (digits: 1) / 2 ^ 0")]
        [DataRow("dead.beef", 16, "dead.beef (digits: 8) / 16 ^ 4")]
        public void Parse(string input, int numSys, string expected) {
            BigDecimal target = BigDecimal.Parse(input, numSys);

            string actual = target.Raw; // за одно проверяется согласованность парсера и ToString
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("", 10, DisplayName = "Пустая строка")]
        [DataRow(null, 10, DisplayName = "null-строка")]
        [DataRow("dead.beef", 15, DisplayName = "Не хватает системы счисления")]
        [DataRow("dead-be.ef", 16, DisplayName = "Минус в середине")]
        [DataRow("Опааа!!!", 10, DisplayName = "Незарегистрированные символы")]
        public void ParseException(string input, int numSys) {
            void action() => BigDecimal.Parse(input, numSys);

            Assert.ThrowsException<FormatException>(action);
        }

        // Тестировать ToString не имеет смысла, т.к. он уже вложен в Raw,
        // который и которым протестирвано уже 11 раз!
    }
}
