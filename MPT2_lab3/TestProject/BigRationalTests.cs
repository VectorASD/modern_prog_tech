using ConsoleApp;
using System;
using System.Numerics;

namespace TestProject {
    [TestClass]
    public sealed class BigRationalTests {
        [TestMethod]
        public void Constructor0() {
            string expected = "0";

            BigRational target = new();

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, "0")]
        [DataRow(1, "1")]
        [DataRow(-1, "-1")]
        [DataRow(123, "123")]
        public void Constructor1(int number, string expected) {
            BigInteger value = new(number);

            BigRational target = new(value);

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(0, 2, "0")]
        [DataRow(1, 2, "1 / 2")]
        [DataRow(-2, 4, "-1 / 2")]
        [DataRow(365, -20, "-73 / 4")]
        [DataRow(-15, -10, "3 / 2")] // Минус всегда переходит в numerator - подтверждено
        public void Constructor2(int num, int denom, string expected) {
            BigInteger numerator = new(num);
            BigInteger denomerator = new(denom);

            BigRational target = new(numerator, denomerator);

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(4, 1, false, -1)]
        [DataRow(5, 1, true, 0)]
        [DataRow(6, 1, false, 1)]
        [DataRow(-8, -2, false, -1)]
        [DataRow(-10, -2, true, 0)]
        [DataRow(-12, -2, false, 1)]
        public void Equals_Compare(int numerator, int denomerator, bool eq_expected, int comp_expected) {
            BigRational target = new(new BigInteger(numerator), new BigInteger(denomerator));
            BigRational five = new(5, 1);
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
            BigRational target = new(value);

            bool actual = target.IsZero;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 2, 82, 3, "209 / 6")]
        [DataRow(151, 3, 8, 1, "175 / 3")]
        [DataRow(-17, 2, -185, 3, "-421 / 6")]
        [DataRow(20, 1, -55, 3, "5 / 3")]
        [DataRow(-55, 3, 20, 3, "-35 / 3")]
        public void Sum(int L_numerator, int L_denomerator, int R_numerator, int R_denomerator, string expected) {
            BigRational L = new(new BigInteger(L_numerator), new BigInteger(L_denomerator));
            BigRational R = new(new BigInteger(R_numerator), new BigInteger(R_denomerator));

            BigRational target = L + R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 2, 82, 3, "-119 / 6")]
        [DataRow(151, 3, 8, 1, "127 / 3")]
        [DataRow(-17, 2, -185, 3, "319 / 6")]
        [DataRow(20, 1, -55, 3, "115 / 3")]
        [DataRow(-55, 3, 20, 3, "-25")]
        public void Subtract(int L_numerator, int L_denomerator, int R_numerator, int R_denomerator, string expected) {
            BigRational L = new(new BigInteger(L_numerator), new BigInteger(L_denomerator));
            BigRational R = new(new BigInteger(R_numerator), new BigInteger(R_denomerator));

            BigRational target = L - R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 2, 82, 3, "205")]
        [DataRow(151, 3, 8, 1, "1208 / 3")]
        [DataRow(-17, 2, -185, 3, "3145 / 6")]
        [DataRow(20, 1, -55, 3, "-1100 / 3")]
        [DataRow(-55, 3, 20, 3, "-1100 / 9")]
        public void Mul(int L_numerator, int L_denomerator, int R_numerator, int R_denomerator, string expected) {
            BigRational L = new(new BigInteger(L_numerator), new BigInteger(L_denomerator));
            BigRational R = new(new BigInteger(R_numerator), new BigInteger(R_denomerator));

            BigRational target = L * R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(15, 2, 82, 3, "45 / 164")]
        [DataRow(151, 3, 8, 1, "151 / 24")]
        [DataRow(-17, 2, -185, 3, "51 / 370")]
        [DataRow(20, 1, -55, 3, "-12 / 11")]
        [DataRow(-55, 3, 20, 3, "-11 / 4")]
        public void Div(int L_numerator, int L_denomerator, int R_numerator, int R_denomerator, string expected) {
            BigRational L = new(new BigInteger(L_numerator), new BigInteger(L_denomerator));
            BigRational R = new(new BigInteger(R_numerator), new BigInteger(R_denomerator));

            BigRational target = L / R;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DivException() {
            BigRational L = new(153, 2);
            BigRational R = BigRational.Zero;

            void action() => _ = L / R;

            Assert.ThrowsException<DivideByZeroException>(action);
        }

        // унарный плюс сквозной (всегда возвращает this), так что тестировать его нет смысла

        [TestMethod]
        [DataRow(-185, 2, "185 / 2")]
        [DataRow( -14, 3, "14 / 3")]
        [DataRow(   3, 2, "-3 / 2")]
        [DataRow(  12, 5, "-12 / 5")]
        [DataRow( 181, 2, "-181 / 2")]
        public void UnarMinus(int numerator, int denomerator, string expected) {
            BigRational value = new(new BigInteger(numerator), new BigInteger(denomerator));

            BigRational target = -value;

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(159, 1000, "1000 / 159")]
        [DataRow(-205, 10, "-2 / 41")]
        [DataRow(3, 1, "1 / 3")]
        [DataRow(-5, 100, "-20")]
        public void Inverse(int numerator, int denomerator, string expected) {
            BigRational value = new(new BigInteger(numerator), new BigInteger(denomerator));

            BigRational target = value.Inverse();

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InverseException() {
            BigRational value = BigRational.Zero;

            void action() => value.Inverse();

            Assert.ThrowsException<DivideByZeroException>(action);
        }

        [TestMethod]
        [DataRow(0, 123, "0")]
        [DataRow(151, 2, "22801 / 4")]
        [DataRow(20, -3, "400 / 9")]
        [DataRow(-3, -10, "9 / 100")]
        [DataRow(-5, 2, "25 / 4")] // подтверждено, что знак минуса в любом случае пропадает
        public void Square(int numerator, int denomerator, string expected) {
            BigRational value = new(new BigInteger(numerator), new BigInteger(denomerator));

            BigRational target = value.Square();

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("10/101010001", 2, "2 / 337")]
        [DataRow("dead/beef", 16, "57005 / 48879")]
        public void Parse(string input, int numSys, string expected) {
            BigRational target = BigRational.Parse(input, numSys);

            string actual = target.Raw;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("", 10, DisplayName = "Пустая строка")]
        [DataRow(null, 10, DisplayName = "null-строка")]
        [DataRow("dead/beef", 15, DisplayName = "Не хватает системы счисления")]
        [DataRow("dead-be/ef", 16, DisplayName = "Минус в середине")]
        [DataRow("Опааа!!!", 10, DisplayName = "Незарегистрированные символы")]
        public void Parse_FormatException(string input, int numSys) {
            void action() => BigRational.Parse(input, numSys);

            Assert.ThrowsException<FormatException>(action);
        }

        [TestMethod]
        [DataRow("0/-", 2, DisplayName = "В идеале 0/0 = 0, а не ошибка, но тогда можно будет делить комплексные числа на 0, по этому этот вариант теперь тоже выдаёт ошибку")]
        [DataRow("-/-", 8, DisplayName = "... и этот тоже")]
        public void Parse_DivideByZeroException(string input, int numSys) {
            void action() => BigRational.Parse(input, numSys);

            Assert.ThrowsException<DivideByZeroException>(action);
        }

        // Тестировать ToString не имеет смысла, т.к. он уже вложен в Raw,
        // который и которым протестирвано уже ? раз!
    }
}
