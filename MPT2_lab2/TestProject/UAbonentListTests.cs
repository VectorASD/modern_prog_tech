using ConsoleApp;
using System.Security.Cryptography;
using System.Xml.Linq;
using static ConsoleApp.UAbonentList;

namespace TestProject {
    [TestClass]
    public sealed class UAbonentListTests { // тестируется класс UAbonentList, включая внутри него Record
        [TestMethod]
        public void Record_Constructor() {
            string name = "abcd";
            string phone = "89123456789";

            Record target = new(name, phone);

            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(phone, target.Phone);
        }

        [TestMethod]
        public void Record_ToString() {
            string expected = "Имя: abcd                     | т-ф: 89123456789";
            string name = "abcd";
            string phone = "89123456789";
            Record target = new(name, phone);

            string actual = target.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Record_Equals() {
            string name = "abcd";
            string phone  = "89123456789";
            string phone2 = "89123457689";
            Record target  = new(name, phone);
            Record target2 = new(name, phone2);
            Record target3 = new(name, phone);

            bool actual  = target.Equals(target2);
            bool actual2 = target.Equals(target3);
            bool actual3 = target2.Equals(target3);

            Assert.IsFalse(actual);
            Assert.IsTrue(actual2);
            Assert.IsFalse(actual3);
        }

        /*[TestMethod]
        [DataRow("abcd", "123", -915737642)]
        public void Record_GetHashCode(string name, string phone, int excepted) {
            Record target = new(name, phone);

            int actual = target.GetHashCode();

            Assert.AreEqual(excepted, actual);
        }*/

        // Вывод: GetHashCode у строк содержит какую-то дополнительную соль каждый запуск программы...
        // По этому, мой GetHashCode не тестируемый из-за вложенных туда GetHashCode строк

        [TestMethod]
        public void Record_Search() {
            string name  = "abcd";
            string name2 = "abcde";
            string phone  = "8912345";
            string phone2 = "89123457689";
            Record target  = new(name, phone);
            Record target2 = new(name, phone2);
            Record target3 = new(name2, phone2);

            bool actual  = target.Search(target2);
            bool actual2 = target2.Search(target);
            bool actual3 = target.Search(target3);
            bool actual4 = target3.Search(target);
            bool actual5 = target2.Search(target3);
            bool actual6 = target3.Search(target2);

            Assert.IsTrue(actual);
            Assert.IsFalse(actual2);
            Assert.IsTrue(actual3);
            Assert.IsFalse(actual4);
            Assert.IsTrue(actual5);
            Assert.IsFalse(actual6);
        }



        private static void DeleteFile(string fileName = "contacts.dat") {
            File.Delete(fileName);
        }

        private static string CheckFile(string fileName = "contacts.dat") {
            FileInfo fileInfo = new(fileName);
            using FileStream fileStream = fileInfo.Open(FileMode.Open);
            byte[] hash = SHA256.HashData(fileStream);

            return fileInfo.Length + "_" + BitConverter.ToString(hash).Replace("-", "").ToLower();
        }



        [TestMethod]
        [DataRow(1, "abcd",      "89123457689", "20_645b14098347f9f687fda002c34ada31e903026d8efe9e530f06fc4d28e2660c")]
        [DataRow(2, "Meow",      "Woof",        "13_7447f499d5554ce94c5293848cd82e552b9fd4e2b6b75ab2bc07db00598e5323")]
        [DataRow(3, "Undefined", "Undefined",   "23_48e9a9596bc32a43a17f2e0a2e1e88e3bfb807f59c751fb8b6a8eb38eeafc667")]
        public void Serialization(int n, string name, string phone, string expected) {
            string fileName = $"test{n}.dat"; // если бы был общий файл, то тесты бы подрались

            DeleteFile(fileName);
            Assert.IsFalse(File.Exists(fileName)); // Доп.проверка на существование файла

            UAbonentList contacts = new(fileName);

            contacts.AddRecord(name, phone);
            Assert.IsTrue(File.Exists(fileName)); // Доп.проверка на существование файла

            string actual = CheckFile(fileName);

            Assert.AreEqual(expected, actual);

            DeleteFile(fileName);
            Assert.IsFalse(File.Exists(fileName)); // Доп.проверка на существование файла
        }

        [TestMethod]
        public void Formats() { // фактически проверяет UAbonentList на способность сортировать по именам
            string fileName = "unused.dat";
            string expected = "Имя: name1                    | т-ф: phone2\nИмя: name2                    | т-ф: phone3\nИмя: name3                    | т-ф: phone1";

            DeleteFile(fileName);
            UAbonentList contacts = new(fileName);
            contacts.AddRecord("name3", "phone1");
            contacts.AddRecord("name1", "phone2");
            contacts.AddRecord("name2", "phone3");

            string actual = contacts.Formats();

            Assert.AreEqual(expected, actual);

            DeleteFile(fileName);
        }

        [TestMethod]
        public void Deserialization() {
            string fileName = "test4.dat";

            DeleteFile(fileName);

            UAbonentList contacts = new(fileName);
            contacts.AddRecord("name1", "phone1");
            string expected = contacts.Formats();

            UAbonentList contacts2 = new(fileName); // явная загрузка из того же файла
            contacts2.Load();
            string actual = contacts2.Formats();

            Assert.AreEqual(expected, actual);

            DeleteFile(fileName);
        }
    }
}
