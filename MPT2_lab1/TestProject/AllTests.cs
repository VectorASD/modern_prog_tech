using MPT2_lab1;
using NumberSystemControlLibrary;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

// Assert.AreEqual(Path.GetFullPath("history.dat"), "?");
// C:\Users\VectorASD\source\repos\MPT2_lab1\TestProject\bin\Debug\net8.0-windows\history.dat
// Оказывается, проект тестирования не может затронуть history.dat от основного проекта, т.к. путь будет другой

// Рекомендации Unit-тестирования: https://learn.microsoft.com/ru-ru/dotnet/core/testing/unit-testing-best-practices

namespace TestProject {
    [TestClass]
    public sealed class ConverterTest { // тестируется класс BigDecimal

        // Конструктор автоматически включается в себя CountDigits и TrimRight

        [TestMethod]
        [DataRow("0",   null, "0 10 0 False 0")]
        [DataRow("123", null, "123 10 0 False 3")]
        [DataRow("-5",  null, "5 10 0 True 1")]
        [DataRow("123",   16, "123 16 0 False 2")]
        [DataRow("123",    2, "123 2 0 False 7")]
        public void Constructor1(string input, int? numSys, string expected) {
            BigInteger value = BigInteger.Parse(input);
            BigDecimal target = numSys is int _numSys ? new(value, _numSys) : new(value);

            string actual = target.RawValue + " " + target.NumberSystem + " " + target.CountAfterDot + " " + target.IsNegative + " " + target.DigitCount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("0",      0, "0 10 0 False 0")]
        [DataRow("123400", 0, "123400 10 0 False 6")]
        [DataRow("123400", 1, "12340 10 0 False 5")]
        [DataRow("123400", 2, "1234 10 0 False 4")]
        [DataRow("123400", 3, "1234 10 1 False 4")]
        [DataRow("123400", 4, "1234 10 2 False 4")]
        public void Constructor2(string input, int countAfterDot, string expected) {
            BigInteger value = BigInteger.Parse(input);
            int numSys = 10;
            BigDecimal target = new(value, countAfterDot, numSys);

            string actual = target.RawValue + " " + target.NumberSystem + " " + target.CountAfterDot + " " + target.IsNegative + " " + target.DigitCount;

            Assert.AreEqual(expected, actual);
        }

        // Format - это сразу пара из методов ToString и Raw с простейшей конкатенацией

        [TestMethod]
        [DataRow("0",   null, "p1: 0   (0 / 10 ^ 0)")]
        [DataRow("123", null, "p1: 123   (123 / 10 ^ 0)")]
        [DataRow("-5",  null, "p1: -5   (-5 / 10 ^ 0)")]
        [DataRow("123",   16, "p1: 7b   (123 / 16 ^ 0)")]
        [DataRow("123",    2, "p1: 1111011   (123 / 2 ^ 0)")]
        public void Format1(string input, int? numSys, string expected) {
            BigInteger value = BigInteger.Parse(input);
            BigDecimal target = numSys is int _numSys ? new(value, _numSys) : new(value);
            const string name = "p1";

            string actual = target.Format(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("0",   0, 10, "p1: 0   (0 / 10 ^ 0)")]
        [DataRow("123", 0, 10, "p1: 123   (123 / 10 ^ 0)")]
        [DataRow("-5",  0, 10, "p1: -5   (-5 / 10 ^ 0)")]
        [DataRow("123", 0, 16, "p1: 7b   (123 / 16 ^ 0)")]
        [DataRow("123", 0,  2, "p1: 1111011   (123 / 2 ^ 0)")]
        public void Format2(string input, int countAfterDot, int numSys, string expected) {
            BigInteger value = BigInteger.Parse(input);
            BigDecimal target = new(value, countAfterDot, numSys);
            const string name = "p1";

            string actual = target.Format(name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("0", 10, "0 10 0 False 0")]
        [DataRow(".", 10, "0 10 0 False 0")]
        [DataRow("1.", 10, "1 10 0 False 1")]
        [DataRow(".5", 10, "5 10 1 False 1")]
        [DataRow("1.5", 10, "15 10 1 False 2")]
        [DataRow("15", 10, "15 10 0 False 2")]
        [DataRow("1001.01", 2, "37 2 2 False 6")]
        // стресс-тест
        [DataRow("13c232f189a321d82f21cb39d03f2e1ab12c32131e32bd1a2e3123", 16, "8128212405434575799330708450663132532279338437924501764281610531 16 0 False 54")]
        public void Parse(string stringValue, int numSys, string expected) {
            BigDecimal target = BigDecimal.Parse(stringValue, numSys);

            string actual = target.RawValue + " " + target.NumberSystem + " " + target.CountAfterDot + " " + target.IsNegative + " " + target.DigitCount;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("123", 3, DisplayName = "Неверная система счисления")]
        [DataRow("", 10, DisplayName = "Пустая строка")]
        [DataRow("12.34.56", 10, DisplayName = "Слишком много точек")]
        [DataRow("123%4", 10, DisplayName = "Посторонние символы")]
        // [ExpectedException(typeof(FormatException))]
        // Не рекомендуется: (MSTEST0006) Prefer 'Assert.ThrowsException/ThrowsExceptionAsync' over '[ExpectedException]'
        public void ParseException(string stringValue, int numSys) {
            void actual() => BigDecimal.Parse(stringValue, numSys);

            Assert.ThrowsException<FormatException>(actual);
        }

        [TestMethod]
        [DataRow("123", 10, 16, "7b")]
        [DataRow("7b", 16, 10, "123")]
        [DataRow("123.45", 8, 10, "83.58")]
        // стресс-тесты
        [DataRow("13c232f189a321d82f2.1cb39d03f2e1ab12c32131e32bd1a2e3123", 16, 10, "5831706399457996014322.1121156820908086198153300388460573728901185")]
        [DataRow("5831706399457996014322.1121156820908086198153300388460573728901185", 10, 16, "13c232f189a321d82f2.1cb39d03f2e1ab12c32131e32bd1a2e3123")]
        // Без лишнего доп-символа получилось. Повезло, повезло
        public void ToNumberSystem(string stringValue, int numSys, int numSys2, string expected) {
            BigDecimal target = BigDecimal.Parse(stringValue, numSys);

            target = target.ToNumberSystem(numSys2);

            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }





    [TestClass]
    public sealed class AboutTest { // тестируется форма AboutForm
        private static string LabelCollector(Control item) {
            StringBuilder sb = new();
            foreach (Control control in item.Controls.Cast<Control>())
                if (control is Label label) {
                    sb.Append(label.Text);
                    sb.Append('\0');
                }
            return sb.ToString();
        }

        private static string StringHash(string text) {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            byte[] hash = SHA256.HashData(bytes);

            return bytes.Length + "_" + BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
        
        [TestMethod]
        public void CheckText() {
            string expected = "456_390f47d391fc1ea08ba1c4185fe2f32d80507bd83919f246d3211097d5c8db62";
            AboutForm form = new();
            string text = LabelCollector(form);

            string actual = StringHash(text);

            Assert.AreEqual(expected, actual);
        }
    }





    [TestClass]
    public sealed class HistoryTest { // тестируется класс History

        private static void DeleteFile(string fileName = "history.dat") {
            File.Delete(fileName); // Оказывается, не генерирует исключения FileNotFoundException
            // try { File.Delete(fileName); }
            // catch (FileNotFoundException) { }
        }

        private static string CheckFile(string fileName = "history.dat") {
            FileInfo fileInfo = new(fileName);
            using FileStream fileStream = fileInfo.Open(FileMode.Open);
            byte[] hash = SHA256.HashData(fileStream);

            return fileInfo.Length + "_" + BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private static string CheckHistoryForm(History history) {
            HistoryForm form = new();
            form.LoadHistory(history);
            return form.HistoryText;
        }

        [TestMethod]
        public void Serialization() {
            string fileName = "test1.dat";
            string expected = "41_e324a4a99869cb6482330bdb213de4bb530e7327308350ab4530f54b468ec5a9";
            DateTime zero_time = new(0); // текущее время (в AddRecord по умолчанию) всегда бы создавало новый хеш

            DeleteFile(fileName);
            Assert.IsFalse(File.Exists(fileName)); // Доп.проверка на существование файла

            History history = new(fileName);
            BigDecimal p1 = BigDecimal.Parse("1234.560", 10);
            BigDecimal p2 = p1.ToNumberSystem(16);

            history.AddRecord(p1, p2, zero_time);
            Assert.IsTrue(File.Exists(fileName)); // Доп.проверка на существование файла

            string actual = CheckFile(fileName);

            Assert.AreEqual(expected, actual);

            DeleteFile(fileName);
            Assert.IsFalse(File.Exists(fileName)); // Доп.проверка на существование файла
        }

        [TestMethod]
        public void HistoryText() {
            string fileName = "test2.dat";
            string expected = "2025-03-13 04:27:54\n" +
                "p1: 1234.56   (123456 / 10 ^ 2)\n" +
                "p2: 4d2.8f   (316047 / 16 ^ 2)\n";
            DateTime zero_time = new(638774368748909394); // DateTimeKind не влияет на вывод времени

            DeleteFile(fileName);

            History history = new(fileName);
            BigDecimal p1 = BigDecimal.Parse("1234.560", 10);
            BigDecimal p2 = p1.ToNumberSystem(16);
            history.AddRecord(p1, p2, zero_time);

            string actual = CheckHistoryForm(history);
            // actual = BitConverter.ToString(Encoding.Unicode.GetBytes(actual));

            Assert.AreEqual(expected, actual);

            DeleteFile(fileName);
        }

        [TestMethod]
        public void Deserialization() {
            string fileName = "test3.dat";

            DeleteFile(fileName);

            History history = new(fileName);
            BigDecimal p1 = BigDecimal.Parse("1234.560", 10);
            BigDecimal p2 = p1.ToNumberSystem(16);
            history.AddRecord(p1, p2); // Не имеет значение, какое сейчас время

            string expected = CheckHistoryForm(history);

            History history2 = new(fileName); // явная загрузка из того же файла
            string actual = CheckHistoryForm(history2);

            Assert.AreEqual(expected, actual);

            DeleteFile(fileName);
        }
    }
}
