using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    enum MemoryState { OFF, ON };

    public class Memory {
        private ANumber? number; // все классы, производные от ANumber НЕИЗМЕНЯЕМЫЕ, т.е. копирований НЕ будет
        private MemoryState state = MemoryState.OFF;

        public ANumber? Number {
            get => number;
            set {
                number = value;
                state = value is null || value.IsZero ? MemoryState.OFF : MemoryState.ON;
            }
        }

        public void Add(ANumber item) {
            if (state == MemoryState.OFF) Number = item;
            else if (Number is not null) Number += item;
        }

        public void Clear() {
            Number = null;
        }

        // private const string on = "ON";
        // private const string off = "OFF";
        // public string State => state == MemoryState.ON ? on : off;

        // private const string on = "(M)"; // а это уже по требованиям самой РГР
        private const string off = "(  )";
        public string State => state == MemoryState.ON ? $"(M: {number})" : off;

        public ANumber ReadNum() {
            if (number is null) throw new Exception("Считывание значения из выключенной памяти");
            return number;
        }
    }
}
