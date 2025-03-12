using NumberSystemControlLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MPT2_lab1 {
    public class History {
        private readonly string FileName;

        class Record(BigDecimal input, BigDecimal output, DateTime? _time = null) {
            private BigDecimal Input { get; } = input;
            private BigDecimal Output { get; } = output;

            private readonly DateTime time = _time ?? DateTime.Now;

            public string Format() {
                return time.ToString("yyyy-MM-dd HH:mm:ss") + "\n" + Input.Format("p1") + "\n" + Output.Format("p2") + "\n";
            }
            public void Serialize(BinaryWriter writer) {
                writer.Write((byte) 0xef);
                Input.Serialize(writer);
                Output.Serialize(writer);
                writer.Write(time.Ticks);
                writer.Write((byte) time.Kind);
                writer.Write((byte) 0xff);
            }
            public static Record Deserialize(BinaryReader reader) {
                byte marker;
                if ((marker = reader.ReadByte()) != 0xef)
                    throw new FormatException("Ожидался стартовый маркер 0xef, а не 0x" + String.Format("{0:x2}", marker));
                BigDecimal input = BigDecimal.Deserialize(reader);
                BigDecimal output = BigDecimal.Deserialize(reader);
                long ticks = reader.ReadInt64();
                DateTimeKind kind = (DateTimeKind) reader.ReadByte();
                if ((marker = reader.ReadByte()) != 0xff)
                    throw new FormatException("Ожидался концевой маркер 0xff, а не 0x" + String.Format("{0:x2}", marker));
                return new(input, output, new(ticks, kind));
            }
        }



        private readonly List<Record> records = [];

        public History(string fileName = "history.dat") {
            FileName = fileName;
            try {
                using BinaryReader reader = new(File.Open(fileName, FileMode.Open));
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                    records.Add(Record.Deserialize(reader));
            }
            catch (FileNotFoundException) { }
            catch (Exception err) {
                MessageBox.Show(err.Message, "Ошибка считывания " + fileName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                try { File.Delete(fileName); } // его уже не починить...
                catch { }
            }
        }



        public void AddRecord(BigDecimal input, BigDecimal output, DateTime? time = null) {
            records.Add(new(input, output, time));
            using BinaryWriter writer = new(File.Open(FileName, FileMode.Append));
            records.Last().Serialize(writer);
        }



        public bool Empty => records.Count == 0;
        public int Count => records.Count;
        public string Item(int i) => records[i].Format();



        public void Clear() {
            records.Clear();
            try { File.Delete(FileName); } // его уже не починить...
            catch { }
        }

        public long FileSize() {
            try { return new FileInfo(FileName).Length; }
            catch { return 0; }
        }
    }
}
