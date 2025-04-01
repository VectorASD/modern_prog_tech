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

        // Процессор не теряли? А ведь это его часть:
        public override ANumber Value =>
            Operation.item switch {
                MathItem.Add      => LeftOperand.Value + RightOperand.Value,
                MathItem.Subtract => LeftOperand.Value - RightOperand.Value,
                MathItem.Multiply => LeftOperand.Value * RightOperand.Value,
                MathItem.Divide   => LeftOperand.Value / RightOperand.Value,
                _ => throw new NotImplementedException($"Незарегестрированная бинарная операция: {TokenList.Format(Operation)}")
            };
        // (все реализации Value для IEditor - это процессор)
    }
}
