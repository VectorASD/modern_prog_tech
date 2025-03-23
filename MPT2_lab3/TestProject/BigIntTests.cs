using ConsoleApp;
using System;
using System.Numerics;

namespace TestProject {
    [TestClass]
    public sealed class BigIntTests {
        [TestMethod]
        public void Constructor0() {
            string expected = "0 (sys: 10) (digits: 1)";

            BigInt target = new();

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, "0 (sys: 10) (digits: 1)")]
        [DataRow(1, "1 (sys: 10) (digits: 1)")]
        [DataRow(-1, "-1 (sys: 10) (digits: 1)")]
        [DataRow(123, "123 (sys: 10) (digits: 3)")]
        public void Constructor1(int number, string expected) {
            BigInteger value = new(number);

            BigInt target = new(value);

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, 2, "0 (sys: 2) (digits: 1)")]
        [DataRow(1, 2, "1 (sys: 2) (digits: 1)")]
        [DataRow(-1, 2, "-1 (sys: 2) (digits: 1)")]
        [DataRow(123, 10, "123 (sys: 10) (digits: 3)")]
        [DataRow(123, 2, "1111011 (sys: 2) (digits: 7)")]
        public void Constructor2(int number, int numSys, string expected) {
            BigInteger value = new(number);

            BigInt target = new(value, numSys);

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, 2, false, -1)]
        [DataRow(4, 2, false, -1)]
        [DataRow(5, 2, true, 0)]
        [DataRow(6, 2, false, 1)]
        [DataRow(7, 2, false, 1)]
        [DataRow(5, 10, true, 0)]
        public void Equals_Compare(int number, int numSys, bool eq_expected, int comp_expected) {
            BigInteger value = new(number);
            BigInt target = new(value, numSys);
            BigInt five = new(5);
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
            BigInt target = new(value);

            bool actual = target.IsZero;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 10, 8, 2, "23 (sys: 10) (digits: 2)")]
        [DataRow(15, 2, 8, 2, "10111 (sys: 2) (digits: 5)")]
        [DataRow(-5, 2, 20, 3, "120 (sys: 3) (digits: 3)")] // 1 * 9 + 2 * 3 = 15... верно!
        [DataRow(20, 2, -5, 10, "15 (sys: 10) (digits: 2)")]
        [DataRow(-14, 2, -18, 2, "-100000 (sys: 2) (digits: 6)")]
        public void Sum(int L_number, int L_numSys, int R_number, int R_numSys, string expected) {
            BigInt L = new(new BigInteger(L_number), L_numSys);
            BigInt R = new(new BigInteger(R_number), R_numSys);

            BigInt target = L + R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 10, 8, 2, "7 (sys: 10) (digits: 1)")]
        [DataRow(15, 2, 8, 2, "111 (sys: 2) (digits: 3)")]
        [DataRow(-5, 2, 20, 3, "-221 (sys: 3) (digits: 3)")] // -(2 * 9 + 2 * 3 + 1) = -25... верно!
        [DataRow(20, 2, -5, 10, "25 (sys: 10) (digits: 2)")]
        [DataRow(-14, 2, -18, 2, "100 (sys: 2) (digits: 3)")]
        public void Subtract(int L_number, int L_numSys, int R_number, int R_numSys, string expected) {
            BigInt L = new(new BigInteger(L_number), L_numSys);
            BigInt R = new(new BigInteger(R_number), R_numSys);

            BigInt target = L - R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 10, 8, 2, "120 (sys: 10) (digits: 3)")]
        [DataRow(15, 2, 8, 2, "1111000 (sys: 2) (digits: 7)")]
        [DataRow(-5, 2, 20, 3, "-10201 (sys: 3) (digits: 5)")] // -(1 * 81 + 2 * 9 + 1) = -100... верно!
        [DataRow(20, 2, -5, 10, "-100 (sys: 10) (digits: 3)")]
        [DataRow(-14, 2, -18, 2, "11111100 (sys: 2) (digits: 8)")]
        public void Mul(int L_number, int L_numSys, int R_number, int R_numSys, string expected) {
            BigInt L = new(new BigInteger(L_number), L_numSys);
            BigInt R = new(new BigInteger(R_number), R_numSys);

            BigInt target = L * R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        // Забегаем на перёд, тестируя за счёт Raw, который уже из BigRational, а не BigInt

        [TestMethod]
        [DataRow(15, 10, 8, 2, "15 / 8")]
        [DataRow(15, 2, 8, 2, "15 / 8")]
        [DataRow(-5, 2, 20, 3, "-1 / 4", DisplayName = "Проверка того, что gcd вообще работает")]
        [DataRow(20, 2, -5, 10, "-4", DisplayName = "Проверка того, что делитель непокалебим на отсутствие минуса")]
        [DataRow(-14, 2, -18, 2, "7 / 9", DisplayName = "gcd + отсутствие минуса в делителе")]
        public void Div(int L_number, int L_numSys, int R_number, int R_numSys, string expected) {
            BigInt L = new(new BigInteger(L_number), L_numSys);
            BigInt R = new(new BigInteger(R_number), R_numSys);

            BigRational target = L / R; // опааа! BigRational вместо BigInt

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DivException() {
            BigInt L = new(new BigInteger(153), 10);
            BigInt R = BigInt.Zero;

            void action() => _ = L / R;

            Assert.ThrowsException<DivideByZeroException>(action);
        }

        // унарный плюс сквозной (всегда возвращает this), так что тестировать его нет смысла

        [TestMethod]
        [DataRow(-18, "18 (sys: 10) (digits: 2)")]
        [DataRow( -1, "1 (sys: 10) (digits: 1)")]
        [DataRow(  0, "0 (sys: 10) (digits: 1)")]
        [DataRow(  1, "-1 (sys: 10) (digits: 1)")]
        [DataRow( 18, "-18 (sys: 10) (digits: 2)")]
        public void UnarMinus(int number, string expected) {
            BigInt value = new(new BigInteger(number));

            BigInt target = -value;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(-20, "-1 / 20")]
        [DataRow(-5, "-1 / 5")]
        [DataRow(3, "1 / 3")]
        [DataRow(15, "1 / 15")]
        public void Inverse(int number, string expected) {
            BigInt value = new(new BigInteger(number));

            BigRational target = value.Inverse(); // опять наперёд...

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InverseException() {
            BigInt value = BigInt.Zero;

            void action() => value.Inverse();

            Assert.ThrowsException<DivideByZeroException>(action);
        }

        [TestMethod]
        [DataRow(-20, "400 (sys: 10) (digits: 3)")]
        [DataRow(-5, "25 (sys: 10) (digits: 2)")]
        [DataRow(0, "0 (sys: 10) (digits: 1)")]
        [DataRow(3, "9 (sys: 10) (digits: 1)")]
        [DataRow(15, "225 (sys: 10) (digits: 3)")]
        public void Square(int number, string expected) {
            BigInt value = new(new BigInteger(number));

            BigInt target = value.Square();

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("0", 10, "0 (sys: 10) (digits: 1)")]
        [DataRow("101010001", 2, "101010001 (sys: 2) (digits: 9)")]
        [DataRow("-", 2, "0 (sys: 2) (digits: 1)")]
        [DataRow("deadbeef", 16, "deadbeef (sys: 16) (digits: 8)")]
        public void Parse(string input, int numSys, string expected) {
            BigInt target = BigInt.Parse(input, numSys);

            string actual = target.Raw; // за одно проверяется согласованность парсера и ToString
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("", 10, DisplayName = "Пустая строка")]
        [DataRow(null, 10, DisplayName = "null-строка")]
        [DataRow("deadbeef", 15, DisplayName = "Не хватает системы счисления")]
        [DataRow("dead-beef", 16, DisplayName = "Минус в середине")]
        [DataRow("Опааа!!!", 10, DisplayName = "Незарегистрированные символы")]
        public void ParseException(string input, int numSys) {
            void action() => BigInt.Parse(input, numSys);

            Assert.ThrowsException<FormatException>(action);
        }

        // Тестировать ToString не имеет смысла, т.к. он уже вложен в Raw,
        // который и которым протестирвано уже 43 раза!
    }
}
