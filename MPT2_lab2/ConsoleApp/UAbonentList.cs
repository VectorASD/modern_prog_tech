using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp {
    [Serializable]
    public class UnpackingError : Exception {
        public UnpackingError() : base() { }
        public UnpackingError(string message) : base(message) { }
        public UnpackingError(string message, Exception inner) : base(message, inner) { }
    }



    public class UAbonentList(string fileName = "contacts.dat") {
        public enum Operations : byte {
            Add = 1,
            Remove,
        }

        public class Record(string name, string phone): IEquatable<Record> {
            public string Name => name;
            public string Phone => phone;

            // public string Format() {
            public override string ToString() {
                return "Имя: " + Name.PadRight(24, ' ') + " | т-ф: " + phone;
            }
            public void Serialize(BinaryWriter writer, Operations operation) {
                writer.Write((byte) 0xef);
                writer.Write((byte) operation);
                writer.Write(Name);
                writer.Write(Phone);
                writer.Write((byte) 0xff);
            }
            public static Record Deserialize(BinaryReader reader, out Operations operation) {
                byte marker;
                if ((marker = reader.ReadByte()) != 0xef)
                    throw new FormatException($"Ожидался стартовый маркер 0xef, а не 0x{marker:x2}");
                operation = (Operations) reader.ReadByte();
                string name = reader.ReadString();
                string phone = reader.ReadString();
                if ((marker = reader.ReadByte()) != 0xff)
                    throw new FormatException($"Ожидался концевой маркер 0xff, а не 0x{marker:x2}");
                return new(name, phone);
            }

            // ~~~ IEquatable ~~~
            // Без него не будет работать метод Remove класса List

            public override bool Equals(object? other) {
                if (other is not Record record) return false;
                return this == record;
            }

            public bool Equals(Record? other) =>
                other is not null && this == other;

            public static bool operator ==(Record a, Record b) =>
                a.Name == b.Name &&
                a.Phone == b.Phone;
            public static bool operator !=(Record a, Record b) =>
                !(a == b);

            // ~~~ Дополнение для хеш-таблиц, сортировок и т.п. ~~~

            private int CachedHash = 0;
            public override int GetHashCode() {
                if (CachedHash == 0)
                    CachedHash = 31 *
                        (31 + Name.GetHashCode()) +
                        Phone.GetHashCode();
                return CachedHash;
            }

            // ~~~ Поиск ~~~

            public bool Search(Record record) {
                return record.Name.Contains(Name, StringComparison.InvariantCultureIgnoreCase) &&
                    record.Phone.Contains(Phone, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        private readonly MultiMap<string, Record> contacts = new();
        private string FileName => fileName;

        public void Load() {
            try {
                using BinaryReader reader = new(File.Open(fileName, FileMode.Open));
                while (reader.BaseStream.Position < reader.BaseStream.Length) {
                    var record = Record.Deserialize(reader, out var operation);
                    switch (operation) {
                        case Operations.Add:
                            AddRecord(record, false);
                            break;
                        case Operations.Remove:
                            RemoveRecord(record, false);
                            break;
                    }
                }
            } catch (FileNotFoundException) { }
            catch (Exception err) {
                //try { File.Delete(fileName); } // его уже не починить...
                //catch { }
                throw new UnpackingError($"Ошибка считывания '{fileName}'", err);
                // MessageBox.Show(err.Message, "Ошибка считывания " + fileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show(err.InnerException?.Message, err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void AddRecord(string name, string phone, bool update = true) =>
            AddRecord(new Record(name, phone), update);

        public void AddRecord(Record record, bool update = true) {
            // Console.WriteLine("Add: " + record);
            contacts.Add(record.Name, record);

            if (update) {
                using BinaryWriter writer = new(File.Open(FileName, FileMode.Append));
                record.Serialize(writer, Operations.Add);
            }
        }

        public void RemoveRecord(string name, string phone, bool update = true) =>
            RemoveRecord(new Record(name, phone), update);

        public void RemoveRecord(Record record, bool update = true) {
            // Console.WriteLine("Remove: " + record);
            contacts.Remove(record.Name, record);

            if (update) {
                using BinaryWriter writer = new(File.Open(FileName, FileMode.Append));
                record.Serialize(writer, Operations.Remove);
            }
        }

        public int Count => contacts.Count;
        public bool Empty => contacts.Count == 0;

        public void Clear() {
            contacts.Clear();
            try { File.Delete(FileName); } catch { }
        }

        public long FileSize {
            get {
                try { return new FileInfo(FileName).Length; }
                catch { return 0; }
            }
        }

        public IEnumerable<string> Keys => contacts.Keys;
        public List<Record> this[string name] => contacts[name];

        public string Formats() { // для тестов
            // select - странно, почему нет select в linq, когда from in, where, join и т.д. там есть
            return string.Join('\n', contacts.Keys.Select(key =>
                string.Join('\n', contacts[key].Select(
                    record => record.ToString()
                ))
            ));
        }
    }
}
