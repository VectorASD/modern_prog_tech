using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    public class DecimalEditor: IEditor {

        private readonly NumberEditor left = new();
        private          NumberEditor? right;
        private int numSys = 10;

        public bool IsDotted => right is not null;
        public string Text {
            get => IsDotted ? $"{left}{BigDecimal.DOT_CHAR}{right}"
                            : left.Text;
            set {
                int pos = value.IndexOf(BigDecimal.DOT_CHAR);
                if (pos == -1) { left.Text = value; right = null; return; }
                if (value.IndexOf(BigDecimal.DOT_CHAR, pos + 1) != -1) throw new FormatException("Две и более точек недопустимо");
                left.Text = value[..pos];
                right = new(value[(pos + 1)..]);
            }
        }
        public DecimalEditor(string? init_text = null) {
            if (init_text is not null) Text = init_text;
        }

        public ANumber Value =>
            IsDotted ? BigDecimal.Parse(Text, numSys)
                     : left.Value;
        public int NumSys {
            get { return numSys; }
            set { numSys = value; left.NumSys = value; }
        }
        public int Length =>
            right is not null ? left.Length + 1 + right.Length
                              : left.Length;
        public bool IsNegative => left.IsNegative;



        public override string ToString() => Text;
        public bool IsZero => Value.IsZero;

        public string AddSign(int index, out int delta) {
            left.AddSign(index, out delta);
            return Text;
        }

        public string AddDigit(int digit, bool shift, int index, out int delta) { // unused
            int len = left.Length + 1;
            if (index < len || right is null) left.AddDigit(digit, shift, index, out delta);
            else                             right.AddDigit(digit, shift, index - len, out delta);
            return Text;
        }

        public string AddZero(bool shift, int index, out int delta) => AddDigit(0, shift, index, out delta); // unused

        public string Backspace(int index, out int delta) { // unused
            int len = left.Length + 1;
            if (index < len || right is null) left.Backspace(index, out delta);
            else                             right.Backspace(index - len, out delta);
            return Text;
        }

        public string Clear() { // unused
            left.Clear();
            right = null;
            return Text;
        }

        private void RemoveDot() {
            if (right is null) return;

            left.Text += right.Text;
            right = null;
        }

        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) {
            delta = 0;
            int len = left.Length + 1;

            if (keyCode == Keys.OemPeriod || keyCode == Keys.Oemcomma) {
                delta = IsDotted ? (index < len ? 1 : 0) : 1;
                if (index >= len) index--; // добавление точки в правом числе, т.е. перемещение точки ещё правее
                if (left.IsNegative && index == 0) { index++; delta++; } // попытка добавить точку перед минусом

                string text = IsDotted ? $"{left}{right}" : left.Text;
                left.Text = text[..index];
                right = new(text[index..]);
                return Text;
            }
            if (keyCode == Keys.OemMinus)
                return AddSign(index, out delta);

            if (index < len || right is null) {
                if (keyCode == Keys.Delete && index == len - 1) { RemoveDot(); return Text; }
                left.Handler(keyCode, shift, ctrl, alt, index, out delta);
            } else {
                if (keyCode == Keys.Back && index == len) { RemoveDot(); delta = -1; return Text; }
                right.Handler(keyCode, shift, ctrl, alt, index - len, out delta);
            }
            return Text;
        }
    }
}
