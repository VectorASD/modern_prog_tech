using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.tokens {
    public class MathToken(string name) : AEditor {
        private readonly string name = name; // его суть в неизменяемости

        public override string Text {
            get => name;
            set => throw new NotImplementedException();
        }



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

        private static readonly MathToken[] binaryFirstOperations = [multiply, divide];
        public static bool BinaryFirstOperation(IEditor token) =>
            token is MathToken math && binaryFirstOperations.Contains(math);

        private static readonly MathToken[] binarySecondOperations = [add, subtract];
        public static bool BinarySecondOperation(IEditor token) =>
            token is MathToken math && binarySecondOperations.Contains(math);


        private static readonly MathToken[] unaryOperations = [inverse, square];
        public static bool UnaryOperation(IEditor token) =>
            token is MathToken math && unaryOperations.Contains(math);

        private static readonly MathToken[] brackets = [L_sqbr, R_sqbr];
        public static bool Bracket(IEditor token) =>
            token is MathToken math && brackets.Contains(math);



        public string Quoted => '"' + Text.Replace("\\", "\\\\").Replace("\"", "\\\"") + '"';
    }
}
