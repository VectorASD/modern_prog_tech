using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Calculator.tokens.MathItem;

namespace Calculator.tokens {
    public enum MathItem {
        Default,  // ""
        Add,      // "+"
        Subtract, // "-"
        Multiply, // "*"
        Divide,   // "/"
        Inverse,  // "1/"
        Square,   // "Sqr"
        L_Sqbr,   // "("
        R_Sqbr,   // ")"
        Result,   // "="
    }

    public class MathToken(MathItem item) : AEditor {
        public readonly MathItem item = item; // его суть в неизменяемости

        public static readonly MathToken Default  = new(MathItem.Default);
        public static readonly MathToken add      = new(MathItem.Add);
        public static readonly MathToken subtract = new(MathItem.Subtract);
        public static readonly MathToken multiply = new(MathItem.Multiply);
        public static readonly MathToken divide   = new(MathItem.Divide);
        public static readonly MathToken inverse  = new(MathItem.Inverse);
        public static readonly MathToken square   = new(MathItem.Square);
        public static readonly MathToken L_sqbr   = new(MathItem.L_Sqbr);
        public static readonly MathToken R_sqbr   = new(MathItem.R_Sqbr);
        public static readonly MathToken result   = new(MathItem.Result);

        public override string Text {
            get => item.GetText();
            set => throw new NotImplementedException();
        }

        public string Quoted =>
            '"' + Text.Replace("\\", "\\\\").Replace("\"", "\\\"") + '"';
    }

    public static class MathTokenExtensions {
        private static readonly string[] tokenStrings = ["", "+", "-", "*", "/", "1/", "Sqr", "(", ")", "="];

        public static string GetText(this MathItem token) {
            int index = (int) token;
            if (index < 0 || index >= tokenStrings.Length) return index.ToString();
            return tokenStrings[index];
        }

        private static readonly MathItem[] partsOfNumber = [Subtract, Inverse, Divide];
        private static readonly MathItem[] binaryOperations = [Multiply, Divide];
        private static readonly MathItem[] binaryFirstOperations = [Add, Subtract, Multiply, Divide];
        private static readonly MathItem[] binarySecondOperations = [Add, Subtract];
        private static readonly MathItem[] unaryOperations = [Inverse, Square];
        private static readonly MathItem[] brackets = [L_Sqbr, R_Sqbr];



        public static bool IsPartOfNumber(this IEditor token) =>
            token is MathToken math && partsOfNumber.Contains(math.item);
        public static bool IsBinaryOperation(this IEditor token) =>
            token is MathToken math && binaryOperations.Contains(math.item);
        public static bool IsBinaryFirstOperation(this IEditor token) =>
            token is MathToken math && binaryFirstOperations.Contains(math.item);
        public static bool IsBinarySecondOperation(this IEditor token) =>
            token is MathToken math && binarySecondOperations.Contains(math.item);
        public static bool IsUnaryOperation(this IEditor token) =>
            token is MathToken math && unaryOperations.Contains(math.item);
        public static bool IsBracket(this IEditor token) =>
            token is MathToken math && brackets.Contains(math.item);
    }
}
