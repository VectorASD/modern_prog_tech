using Calculator.tokens;
using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    // частичная реализация AEditor
    public abstract class AEditor: IEditor, IEquatable<AEditor> {
        public abstract string Text { get; set; }
        public virtual ANumber Value => throw new NotImplementedException();

        public virtual int NumSys {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }



        public virtual int Length => Text.Length;
        public virtual bool IsZero => Value.IsZero;
        public virtual bool IsNegative => throw new NotImplementedException();
        public override string ToString() => Text;



        public bool Equals(AEditor? other) =>
            other is not null && Text == other.Text; // IEquatable<AEditor>
        public override bool Equals(object? obj) => Equals(obj as MathToken);
        public override int GetHashCode() => Text.GetHashCode();



        // Реализуется только в редакторах, редко - в токенах

        public virtual string AddDigit(int digit, bool shift, int index, out int delta) => throw new NotImplementedException();
        public virtual string AddSign(int index, out int delta)                         => throw new NotImplementedException();
        public virtual string AddZero(bool shift, int index, out int delta)             => throw new NotImplementedException();
        public virtual string Backspace(int index, out int delta)                       => throw new NotImplementedException();
        public virtual string Clear()                                                   => throw new NotImplementedException();
        public virtual string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) =>
            throw new NotImplementedException();
    }
}
