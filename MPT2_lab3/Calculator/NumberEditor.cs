using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    public class NumberEditor(string init_text = ""): IEditor {

        private readonly List<char> text = [.. init_text];
        private bool negative = false;
        private int numSys = 10;

        public string Text {
            get => (negative ? "-" : "") + new string([.. text]);
            set {
                negative = value.StartsWith('-');
                text.Clear();
                text.AddRange(negative ? value.Skip(1) : value); // оба ответвления соответствуют IEnumerable<char>
            }
        }
        public ANumber Value =>
            text.Count == 0 ? BigInt.Zero
                            : BigInt.Parse(Text, numSys);
        public int NumSys { get { return numSys; } set { numSys = value; } }
        public int Length => text.Count + (negative ? 1 : 0);
        public bool IsNegative => negative;



        public override string ToString() => Text;
        public bool IsZero => Value.IsZero;

        // private bool FirstZero => text.Count > 0 && text[0] == '0';

        public string AddSign(int index, out int delta) {
            negative = !negative;
            delta = negative ? 1 : (index <= 0 ? 0 : - 1);
            return Text;
        }

        public string AddDigit(int digit, bool shift, int index, out int delta) {
            // исправление позиции при наличии минуса в числе
            delta = negative && index == 0 ? 2 : 1;
            if (negative) index = Math.Max(0, index - 1);

            int letter = digit switch {
                < 10 => digit + '0',
                _ => digit - 10 + (shift ? 'A' : 'a')
            };
            text.Insert(index, (char) letter);

            return Text;
        }

        public string AddZero(bool shift, int index, out int delta) => AddDigit(0, shift, index, out delta); // unused

        public string Backspace(int index, out int delta) {
            if (negative && index == 0) return AddSign(index, out delta);

            if (negative) index--;
            if (index < 0 || index >= text.Count) {delta = 0; return Text; }

            text.RemoveAt(index);
            delta = -1;
            return Text;
        }

        public string Clear() { // unused
            negative = false;
            text.Clear();
            return Text;
        }

        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) {
            delta = 0;
            if (keyCode >= Keys.D0 && keyCode <= Keys.D9)
                return AddDigit(keyCode - Keys.D0, shift, index, out delta);
            if (keyCode >= Keys.A && keyCode <= Keys.F)
                return AddDigit(keyCode - Keys.A + 10, shift, index, out delta);
            if (keyCode == Keys.Left) { delta = -1; return Text; }
            if (keyCode == Keys.Right) { delta = 1; return Text; }
            if (keyCode == Keys.OemMinus)
                return AddSign(index, out delta);
            if (keyCode == Keys.Back)
                return Backspace(index - 1, out delta);
            if (keyCode == Keys.Delete)
                return Backspace(index, out _);

            // if (keyCode != Keys.ControlKey && keyCode != Keys.ShiftKey && keyCode != Keys.Home) MessageBox.Show("Handler: " + keyCode);

            delta = 0;
            return Text;
        }
    }
}
