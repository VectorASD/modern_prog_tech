using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Calculator {
    public class History {
        private readonly string FileName;

        class Record(string input, string output, DateTime? _time = null) {
            private string Input => input;
            private string Output => output;



            private readonly DateTime time = _time ?? DateTime.Now;

            public string Format() {
                return time.ToString("yyyy-MM-dd HH:mm:ss") + "\n" + Input + "\n= " + Output + "\n";
            }
            public void Serialize(BinaryWriter writer) {
                writer.Write((byte) 0xef);
                writer.Write(Input);
                writer.Write(Output);
                writer.Write(time.Ticks);
                writer.Write((byte) time.Kind);
                writer.Write((byte) 0xff);
            }
            public static Record Deserialize(BinaryReader reader) {
                byte marker;
                if ((marker = reader.ReadByte()) != 0xef)
                    throw new FormatException("Ожидался стартовый маркер 0xef, а не 0x" + string.Format("{0:x2}", marker));
                string input = reader.ReadString();
                string output = reader.ReadString();
                long ticks = reader.ReadInt64();
                DateTimeKind kind = (DateTimeKind)reader.ReadByte();
                if ((marker = reader.ReadByte()) != 0xff)
                    throw new FormatException("Ожидался концевой маркер 0xff, а не 0x" + string.Format("{0:x2}", marker));
                return new(input, output, new(ticks, kind));
            }
        }



        private readonly List<Record> records = [];
        private readonly System.Timers.Timer timer;
        private Record? last_record;

        public History(string fileName = "history.dat") {
            timer = new(5000) {
                AutoReset = false // одноразовый таймер, т.е. Elapsed не будет автоматически его снова запускать
            };
            timer.Elapsed += Timer_Elapsed;
            // этот Timer базируется на Threading.Timer... похож на тот, что в QNX



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





        public void AddRecord(string input, string output, DateTime? time = null) {
            last_record = new(input, output, time);

            timer.Stop(); // сбрасываем старые недобранные 5 секунд
            timer.Start(); // снова отсчитываем время
        }
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e) {
            if (last_record is null) return;

            records.Add(last_record);
            using BinaryWriter writer = new(File.Open(FileName, FileMode.Append));
            records.Last().Serialize(writer);
            last_record = null;
        }

        public void MomentalAddRecord(string input, string output, DateTime? time = null) { // Для тестирования
            records.Add(new(input, output, time));
            using BinaryWriter writer = new(File.Open(FileName, FileMode.Append));
            records.Last().Serialize(writer);
            last_record = null;
        }



        public bool Empty => records.Count == 0;
        public int Count => records.Count;
        public string Item(int i) => records[i].Format();
        public string? LastRecord => last_record?.Format();



        public void Clear() {
            records.Clear();
            try { File.Delete(FileName); }
            catch { }
        }

        public long FileSize() {
            try { return new FileInfo(FileName).Length; }
            catch { return 0; }
        }
    }
}
