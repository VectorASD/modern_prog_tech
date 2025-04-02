using Calculator;
using ConsoleApp;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestProject {
    [TestClass]
    public sealed class HistoryTests {
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
            string expected = "71_438bb8430774d11fc21eef9e2a300a2ff096a318467acbe3b43eaedcdc0a6df8";
            DateTime zero_time = new(0); // текущее время (в MomentalAddRecord по умолчанию) всегда бы создавало новый хеш

            DeleteFile(fileName);
            Assert.IsFalse(File.Exists(fileName)); // Доп.проверка на существование файла

            History history = new(fileName);
            string input = "(1/ 123213 + () - 25) * 2+i3";
            string output = "-6160648/123213-i3080324/41071";

            history.MomentalAddRecord(input, output, zero_time);
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
                "(1/ 123213 + () - 25) * 2+i3\n" +
                "= -6160648/123213-i3080324/41071\n";
            DateTime zero_time = new(638774368748909394); // DateTimeKind не влияет на вывод времени

            DeleteFile(fileName);

            History history = new(fileName);
            string input = "(1/ 123213 + () - 25) * 2+i3";
            string output = "-6160648/123213-i3080324/41071";
            history.MomentalAddRecord(input, output, zero_time);

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
            string input = "(1/ 123213 + () - 25) * 2+i3";
            string output = "-6160648/123213-i3080324/41071";
            history.MomentalAddRecord(input, output); // Не имеет значение, какое сейчас время

            string expected = CheckHistoryForm(history);

            History history2 = new(fileName); // явная загрузка из того же файла
            string actual = CheckHistoryForm(history2);

            Assert.AreEqual(expected, actual);

            DeleteFile(fileName);
        }
    }
}
