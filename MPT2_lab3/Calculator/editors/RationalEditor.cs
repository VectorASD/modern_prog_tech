using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.editors {
    public class RationalEditor : IEditor {

        private readonly DecimalEditor left = new();
        private          DecimalEditor? right;
        private int numSys = 10;

        public bool IsDivided => right is not null;
        public string Text {
            get => IsDivided ? $"{left}{BigRational.DIV_CHAR}{right}"
                             : left.Text;
            set {
                int pos = value.IndexOf(BigRational.DIV_CHAR);
                if (pos == -1) { left.Text = value; right = null; return; }
                if (value.IndexOf(BigRational.DIV_CHAR, pos + 1) != -1) throw new FormatException("Два и более делителя недопустимо");
                left.Text = value[..pos];
                right = new(value[(pos + 1)..]);
            }
        }
        public RationalEditor(string? init_text = null) {
            if (init_text is not null) Text = init_text;
        }

        public ANumber Value =>
            IsDivided ? BigRational.Parse(Text, numSys)
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
        public bool IsZero => Value.IsZero; // unused

        public string AddSign(int index, out int delta) {
            int len = left.Length + 1;
            if (index < len || right is null) left.AddSign(index, out delta);
            else right.AddSign(index - len, out delta);
            return Text;
        }

        public string AddDigit(int digit, bool shift, int index, out int delta) { // unused
            int len = left.Length + 1;
            if (index < len || right is null) left.AddDigit(digit, shift, index, out delta);
            else right.AddDigit(digit, shift, index - len, out delta);
            return Text;
        }

        public string AddZero(bool shift, int index, out int delta) => AddDigit(0, shift, index, out delta); // unused

        public string Backspace(int index, out int delta) { // unused
            int len = left.Length + 1;
            if (index < len || right is null) left.Backspace(index, out delta);
            else right.Backspace(index - len, out delta);
            return Text;
        }

        public string Clear() { // unused
            left.Clear();
            right = null;
            return Text;
        }

        private void RemoveDivider(out int delta) {
            delta = -1;
            if (right is null) return;

            bool negative_right = right is not null && right.IsNegative;
            if (negative_right) right?.AddSign(0, out _);

            try { left.Text += right?.Text; }
            catch (FormatException) {
                if (negative_right) right?.AddSign(0, out _); // возвращаем минус
                delta = 0; return;
            }

            if (negative_right) {
                delta = left.IsNegative ? -2 : 0;
                left.AddSign(0, out _);
            }

            right = null;
        }

        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) {
            delta = 0;
            int len = left.Length + 1;

            if (keyCode == Keys.Divide) {
                delta = IsDivided ? (index < len ? 1 : 0) : 1;
                if (index >= len) index--; // добавление '/' в правом числе, т.е. перемещение '/' ещё правее
                //if (left.IsNegative && index == 0) { index++; delta++; } // попытка добавить '/' перед минусом

                bool negative_right = right is not null && right.IsNegative;
                if (negative_right) {
                    right?.AddSign(0, out _);
                    if (index >= len) { index--; delta--; }
                }
                string text = IsDivided ? $"{left}{right}" : left.Text;
                DecimalEditor new_right;
                try {
                    new_right = new(text[index..]);
                    left.Text = text[..index];
                } catch (FormatException) { // при перемещении '/', выходит две точки, игнорируем действие
                    if (negative_right) right?.AddSign(0, out _); // возврат минуса назад
                    delta = 0; return Text;
                }
                right = new_right;
                if (negative_right) right.AddSign(0, out _);
                return Text;
            }

            if (index < len || right is null) {
                if (keyCode == Keys.Delete && index == len - 1) { RemoveDivider(out _); return Text; }
                left.Handler(keyCode, shift, ctrl, alt, index, out delta);
            } else {
                if (keyCode == Keys.Back && index == len) { RemoveDivider(out delta); return Text; }
                right.Handler(keyCode, shift, ctrl, alt, index - len, out delta);
            }
            return Text;
        }
    }
}
