using Calculator.editors;
using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.tokens {
    public class BinaryToken (IEditor L_operand, MathToken operation, IEditor R_operand) : AEditor {
        public IEditor LeftOperand { get; } = L_operand;
        public MathToken Operation { get; } = operation;
        public IEditor RightOperand { get; } = R_operand;



        public override string Text {
            get => $"Binary({LeftOperand}, {Operation.Quoted}, {RightOperand})";
            set => throw new NotImplementedException();
        }
    }
}
