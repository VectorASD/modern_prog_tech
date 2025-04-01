using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.tokens {
    public class MessageToken(string message) : AEditor {
        private readonly string message = message;

        public override string Text {
            get => message;
            set => throw new NotImplementedException();
        }
    }
}
