using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.tokens {
    public class SpaceToken(int count = 1) : AEditor {
        private int count = count;

        public override string Text {
            get => "".PadRight(count);
            set => throw new NotImplementedException();
        }
        public override int Length => count;



        public void Increment(int delta) => count += delta;



        public override string Backspace(int index, out int delta) {
            delta = count > 0 ? -1 : 0;
            count += delta;
            return Text;
        }

        public override string Clear() {
            count = 0;
            return "";
        }

        public override string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) {
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
    }
}
