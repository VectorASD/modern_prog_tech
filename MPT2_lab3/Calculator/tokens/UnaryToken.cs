using Calculator.editors;
using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.tokens {
    public class UnaryToken (MathToken operation, IEditor operand) : AEditor {
        public MathToken Operation { get; } = operation;
        public IEditor Operand { get; } = operand;



        public override string Text {
            get => $"Unary({Operation.Quoted}, {Operand})";
            set => throw new NotImplementedException();
        }

        // Процессор не теряли? А ведь это его часть:
        public override ANumber Value =>
            Operation.item switch {
                MathItem.Inverse => Operand.Value.Inverse(),
                MathItem.Square  => Operand.Value.Square(),
                _ => throw new NotImplementedException($"Незарегестрированная бинарная операция: {TokenList.Format(Operation)}")
            };
        // (все реализации Value для IEditor - это процессор)
    }
}
