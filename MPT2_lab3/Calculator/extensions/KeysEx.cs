using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.extensions {
    public class KeysEx {
        // между последней клавишей (OemClear = 0xFE)
        // и первым модификатором (Shift = 0x00010000)
        // очень много свободного места 👍

        public const Keys AddInv = (Keys) 0x1000;
        public const Keys AddSubtract = (Keys) 0x1001;
        public const Keys CE = (Keys) 0x1002;
        public const Keys C = (Keys) 0x1003;
    }
}
