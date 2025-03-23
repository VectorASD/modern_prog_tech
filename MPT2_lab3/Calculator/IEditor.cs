using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    public interface IEditor {
        public string Text { get; set; }
        public ANumber Value { get; }
        public int NumSys { get; set; }
        public int Length { get; }
        public bool IsNegative { get; }



        public bool IsZero { get; }
        public string AddSign(out int delta);
        public string AddDigit(int digit, bool shift, int index, out int delta);
        public string AddZero(bool shift, int index, out int delta);
        public string Backspace(int index, out int delta);
        public string Clear();
        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta);
    }
}
