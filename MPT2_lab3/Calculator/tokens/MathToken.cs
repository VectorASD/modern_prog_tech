using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.tokens {
    public class MathToken(string name) : IEditor, IEquatable<MathToken> {
        private readonly string name = name; // его суть в неизменяемости



        public static readonly MathToken Default  = new("");

        public static readonly MathToken add      = new("+");
        public static readonly MathToken subtract = new("-");
        public static readonly MathToken multiply = new("*");
        public static readonly MathToken divide   = new("/");
        public static readonly MathToken inverse  = new("1/");
        public static readonly MathToken square   = new("Sqr");
        public static readonly MathToken L_sqbr   = new("(");
        public static readonly MathToken R_sqbr   = new(")");

        private static readonly MathToken[] partsOfNumber = [subtract, inverse, divide];
        public static bool PartOfNumber(IEditor token) =>
            token is MathToken math && partsOfNumber.Contains(math);

        private static readonly MathToken[] binaryOperations = [add, subtract, multiply, divide];
        public static bool BinaryOperation(IEditor token) =>
            token is MathToken math && binaryOperations.Contains(math);

        private static readonly MathToken[] unaryOperations = [inverse, square];
        public static bool UnaryOperation(IEditor token) =>
            token is MathToken math && unaryOperations.Contains(math);

        private static readonly MathToken[] brackets = [L_sqbr, R_sqbr];
        public static bool Bracket(IEditor token) =>
            token is MathToken math && brackets.Contains(math);



        public bool Equals(MathToken? other) =>
            Text == (other ?? Default).Text; // IEquatable<MathToken>
        public override bool Equals(object? obj) => Equals(obj as MathToken);
        public override int GetHashCode() => name.GetHashCode();



        public string Text {
            get => name;
            set => throw new NotImplementedException();
        }
        public ANumber Value => throw new NotImplementedException();
        public int NumSys {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public int Length => name.Length;
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
            throw new NotImplementedException();
        }

        public string Clear() {
            throw new NotImplementedException();
        }

        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) {
            throw new NotImplementedException();
        }
    }
}
