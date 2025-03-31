using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.tokens {
    public class SpaceToken(int count = 1) : IEditor {
        private int count = count;

        public string Text {
            get => "".PadRight(count);
            set => throw new NotImplementedException();
        }
        public ANumber Value => throw new NotImplementedException();
        public int NumSys {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public int Length => count;
        public bool IsNegative => false;



        public override string ToString() => Text;
        public bool IsZero => true;

        public string AddDigit(int digit, bool shift, int index, out int delta) {
            throw new NotImplementedException();
        }

        public string AddSign(int index, out int delta) {
            throw new NotImplementedException();
        }

        public string AddZero(bool shift, int index, out int delta) {
            throw new NotImplementedException();
        }

        public string Backspace(int index, out int delta) {
            delta = count > 0 ? -1 : 0;
            count += delta;
            return Text;
        }

        public string Clear() {
            count = 0;
            return "";
        }

        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) {
            delta = 0;
            switch (keyCode) {
                case Keys.Space: count++; delta = 1; break;
                case Keys.Back:
                    if (count > 0 && index > 0) { count--; delta = -1; }
                    break;
                case Keys.Delete:
                    if (count > 0 && index < count) count--;
                    break;
            }
            return Text;
        }

        public void Increment(int delta) => count += delta;
    }
}
