using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.editors {
    public class ComplexEditor : IEditor {

        private readonly RationalEditor left = new();
        private          RationalEditor? right;
        private int numSys = 10;
        private bool negative = false; // касается только imaginary
        private string ImagChar => negative ? BigComplex.I_MINUS_CHAR : BigComplex.I_PLUS_CHAR;
        private int ImagSize => right is null ? 0 : ImagChar.Length;

        public bool IsImaginary => right is not null;
        public string Text {
            get => IsImaginary ? $"{left}{ImagChar}{right}"
                               : left.Text;
            set {
                int pos = value.IndexOf(BigComplex.I_PLUS_CHAR);
                bool neg = false;
                if (pos == -1) {
                    pos = value.IndexOf(BigComplex.I_MINUS_CHAR);
                    neg = true;
                }
                if (pos == -1) { left.Text = value; right = null; return; }

                int size = neg ? BigComplex.I_MINUS_CHAR.Length : BigComplex.I_PLUS_CHAR.Length;
                if (value.IndexOf(BigComplex.I_MINUS_CHAR, pos + size) != -1
                  || value.IndexOf(BigComplex.I_PLUS_CHAR, pos + size) != -1)
                    throw new FormatException("Два и более i-разделителя недопустимо");
                left.Text = value[..pos];
                right = new(value[(pos + size)..]);
                negative = neg;
            }
        }
        public ComplexEditor(string? init_text = null) {
            if (init_text is not null) Text = init_text;
        }

        public ANumber Value =>
            IsImaginary ? BigComplex.Parse(Text, numSys)
                        : left.Value;
        public int NumSys {
            get { return numSys; }
            set { numSys = value; left.NumSys = value; }
        }
        public int Length =>
            right is not null ? left.Length + ImagSize + right.Length
                              : left.Length;
        public bool IsNegative => left.IsNegative;



        public override string ToString() => Text;
        public bool IsZero => Value.IsZero; // unused

        public string AddSign(int index, out int delta) {
            int len = left.Length;
            if (index <= len || right is null) left.AddSign(index, out delta);
            else {
                int size = ImagSize;
                negative = !negative;
                delta = ImagSize - size;
            }
            return Text;
        }

        public string AddDigit(int digit, bool shift, int index, out int delta) { // unused
            int len = left.Length;
            int len2 = len + ImagSize;
            if (index <= len || right is null) left.AddDigit(digit, shift, index, out delta);
            else if (index >= len2) right.AddDigit(digit, shift, index - len2, out delta);
            else delta = 0;
            return Text;
        }

        public string AddZero(bool shift, int index, out int delta) => AddDigit(0, shift, index, out delta); // unused

        public string Backspace(int index, out int delta) { // unused
            int len = left.Length;
            int len2 = len + ImagSize;
            if (index <= len || right is null) left.Backspace(index, out delta);
            else if (index >= len2) right.Backspace(index - len2, out delta);
            else delta = 0;
            return Text;
        }

        public string Clear() { // unused
            left.Clear();
            right = null;
            negative = false;
            return Text;
        }

        private void RemoveImaginary(out int delta) {
            delta = -ImagSize;
            try { left.Text += right?.Text; }
            catch (FormatException) { delta = 0; return; }
            right = null;
            negative = false;
        }

        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) {
            delta = 0;
            int len = left.Length;
            int len2 = len + ImagSize;

            if (keyCode == Keys.I) {
                if ((IsImaginary ? len <= index : len < index) && index <= len2) return Text;
                bool is_right = index > len2;
                if (is_right) index -= ImagSize;
                string text = IsImaginary ? $"{left}{right}" : left.Text;
                RationalEditor new_right;
                bool neg = text.Length > index && text[index] == '-';
                try {
                    new_right = new(text[(neg ? index + 1 : index)..]);
                    left.Text = text[..index];
                } catch (FormatException) { // при перемещении 'i', выходит две точки, или две '/', игнорируем действие
                    delta = 0; return Text;
                }
                right = new_right;
                negative ^= neg;
                delta = is_right ? 0 : ImagSize;
                return Text;
            }
            if (keyCode == Keys.Subtract)
                return AddSign(index, out delta);

            if (index <= len || right is null) {
                if (keyCode == Keys.Delete && index == len) { RemoveImaginary(out _); return Text; }
                left.Handler(keyCode, shift, ctrl, alt, index, out delta);
            } else if (index >= len2) {
                if (keyCode == Keys.Back && index == len2) { RemoveImaginary(out delta); return Text; }
                right.Handler(keyCode, shift, ctrl, alt, index - len2, out delta);
            }
            return Text;
        }



        public static readonly ComplexEditor Void = new();

        public bool Slice(int index, out ComplexEditor L, out ComplexEditor R) {
            string text = Text;
            try {
                L = new(text[..index]);
                R = new(text[index..]);
                L.NumSys = R.NumSys = 16; // максимальная система счисления, чтобы не мешать чекеру
                try { _ = L.Value; _ = R.Value; } // чекер
                catch (DivideByZeroException) { }
                L.NumSys = R.NumSys = NumSys;
                return true;
            } catch (FormatException) { // при перемещении 'i', выходит две точки, или две '/', игнорируем действие
                L = R = Void;
                return false;
            }
        }
        public bool Combine(ComplexEditor right, out ComplexEditor result) { // ещё подошло бы название Concat, но воздержусь ;'-}
            string text = Text + right.Text;
            try {
                result = new(text) {
                    NumSys = 16 // максимальная система счисления, чтобы не мешать чекеру
                };
                try { _ = result.Value; } // чекер
                catch (DivideByZeroException) { }

                result.NumSys = Math.Max(NumSys, right.NumSys);
                if (NumSys != right.NumSys) {
                    DialogResult res = MessageBox.Show($"Комбинация чисел с разной системой счисления ({NumSys} и {right.NumSys})\nдаст новому числу систему счисления: {result.NumSys}", "Подтвердить?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (res != DialogResult.OK) return false;
                }

                return true;
            } catch (FormatException) {
                result = Void;
                return false;
            }
        }
    }
}
