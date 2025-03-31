using Calculator.tokens;
using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.editors {
    public class TokenEditor : IEditor {

        private readonly TokenList tokens = [];
        // O_o, видимо, именно IEnumerable позволяет делать такую инициализацию
        // Убирание IEnumerable подтвердило это. Тип становится конструируемым за счёт IEnumerable



        public string Text {
            get => tokens.Text;
            set => throw new NotImplementedException();
        }
        public TokenEditor(string? init_text = null) {
            // DebugInit();
            if (init_text is not null) Text = init_text;
        }

        public ANumber Value => tokens.Value;
        public void SetLastIndex(int index) => tokens.SetLastIndex(index);
        public int NumSys {
            get => tokens.NumSys;
            set => tokens.NumSys = value;
        }
        public int Length => tokens.Length;
        public bool IsNegative => throw new NotImplementedException();



        public override string ToString() => Text;
        public bool IsZero => throw new NotImplementedException();

        public string AddSign(int index, out int delta) => // unused
            Handler(Keys.Subtract, false, false, false, index, out delta);

        public string AddDigit(int digit, bool shift, int index, out int delta) { // unused
            Keys keyCode = digit < 10 ? digit + Keys.D0 : digit - 10 + Keys.A;
            return Handler(keyCode, shift, false, false, index, out delta);
        }

        public string AddZero(bool shift, int index, out int delta) => AddDigit(0, shift, index, out delta); // unused

        public string Backspace(int index, out int delta) {
            int idx = tokens.Index2Idx_type2(index);
            index -= tokens.Qsum_get(idx);
            if (index <= 0) { delta = 0; return Text; }

            IEditor token = tokens[idx];
            if (token is MathToken) {
                tokens.RemoveAt(idx);
                delta = -index;
                return Text;
            }
            int size = token.Length;
            token.Handler(Keys.Back, false, false, false, index, out delta);
            tokens.Resize(idx, token.Length - size);
            return Text;
        }

        public string Clear() { // unused
            tokens.Clear();
            return Text;
        }

        // 123.454   .-i.
        // 0......1..2...3...
        // 000000011122223333, 3 -> 2

        /*private void DebugInit() {
            AddToken(new ComplexEditor("123.454"));
            AddToken(new SpaceToken(3));
            AddToken(new ComplexEditor(".-i."));
            InsertTokens(1, [new SpaceToken(3), new ComplexEditor("123/4")]);
            RemoveToken(0);
            // Debug();
        }*/
        public string Debug() => tokens.Debug();

        private static MathToken? KeyCode_to_MathToken(Keys keyCode, bool shift) {
            if (shift) {
                if (keyCode == Keys.D9) return MathToken.L_sqbr;
                if (keyCode == Keys.D0) return MathToken.R_sqbr;
            }
            return keyCode switch {
                Keys.Add      => MathToken.add,
                Keys.Subtract => MathToken.subtract,
                Keys.Multiply => MathToken.multiply,
                Keys.Divide   => MathToken.divide,
                Keys.S        => MathToken.square,
                KeysEx.AddInv      => MathToken.inverse,
                KeysEx.AddSubtract => MathToken.subtract,
                _ => null,
            };
        }
        private static IEditor CreateToken(Keys keyCode, bool shift, bool ctrl, bool alt, out int delta) {
            if (keyCode == Keys.Space) {
                delta = 1;
                return new SpaceToken(1);
            }
            IEditor? token = KeyCode_to_MathToken(keyCode, shift);
            if (token is null) {
                token = new ComplexEditor();
                token.Handler(keyCode, shift, ctrl, alt, 0, out delta);
            } else
                delta = token.Length;
            return token;
        }

        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) {
            if (tokens.Count == 0) {
                IEditor new_token = CreateToken(keyCode, shift, ctrl, alt, out delta);
                if (new_token.Length > 0) tokens.Add(new_token);
                return Text;
            }

            delta = 0;
            if (keyCode == Keys.Back)
                return Backspace(index, out delta);
            if (keyCode == Keys.Delete)
                return Backspace(index + 1, out _);

            bool space_key = keyCode == Keys.Space;
            int idx = tokens.Index2Idx(index, space_key);
            IEditor token = tokens[idx];
            int size = token.Length;
            index -= tokens.Qsum_get(idx); // теперь от 0 до size, не включая size

            if (token is SpaceToken) {
                if (!space_key) {
                    IEditor middle = CreateToken(keyCode, shift, ctrl, alt, out delta);
                    if (middle.Length <= 0) return Text;

                    SpaceToken left = new(index);
                    SpaceToken right = new(size - index);
                    tokens.RemoveAt(idx, false);
                    tokens.InsertRange(idx, [left, middle, right]);
                    return Text;
                }
                token.Handler(keyCode, shift, ctrl, alt, index, out delta);
                tokens.Resize(idx, token.Length - size);
                return Text;
            }
            
            if (token is ComplexEditor complex) {
                MathToken? math_token = keyCode != Keys.Subtract && keyCode != Keys.Divide ? KeyCode_to_MathToken(keyCode, shift) : null;
                if ((space_key || math_token is not null) && complex.Slice(index, out ComplexEditor left, out ComplexEditor right)) {
                    tokens.RemoveAt(idx, false);
                    tokens.InsertRange(idx, [left, math_token is not null ? math_token : new SpaceToken(), right]);
                    delta = 1;
                    return Text;
                }
                token.Handler(keyCode, shift, ctrl, alt, index, out delta);
                tokens.Resize(idx, token.Length - size);
                return Text;
            }
            
            if (token is MathToken) {
                if (index > 0 && index < token.Length) return Text;

                IEditor new_token = CreateToken(keyCode, shift, ctrl, alt, out delta);
                if (new_token.Length <= 0) return Text;

                tokens.InsertRange(index <= 0 ? idx : idx + 1, [new_token]);
                return Text;
            }

            return Text;
        }

        public void Colorize(RichTextBox rich) {
            int saved_start = rich.SelectionStart;
            int saved_length = rich.SelectionLength;
            int idx = 0;
            foreach (IEditor token in tokens) {
                int start = tokens.Qsum_get(idx);
                int end = tokens.Qsum_get(++idx);
                rich.Select(start, end - start);
                rich.SelectionBackColor = token switch {
                    SpaceToken => Color.PaleTurquoise,
                    ComplexEditor => Color.SpringGreen,
                    MathToken => Color.DeepSkyBlue,
                    _ => Color.White
                };
                rich.SelectionColor = token switch {
                    MathToken => Color.Blue,
                    _ => Color.Black
                };
            }
            rich.Select(saved_start, saved_length);
        }
    }
}
