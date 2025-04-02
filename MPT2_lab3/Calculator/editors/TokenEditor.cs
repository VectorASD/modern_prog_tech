using Calculator.extensions;
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
        public ANumber Value => Parse().Value;

        public void SetLastIndex(int index) => tokens.LastIndex = index;
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
            if (token is null)
                token = new ComplexEditor(keyCode, shift, ctrl, alt, out delta);
            else
                delta = token.Length;
            return token;
        }

        public void HandlerWrap(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta, out bool update_result) {
            update_result = false;
            if (result_mode) { // не даём редактировать/обрабатывать токены результата
                int lock_pos = tokens.Qsum_get(result_token_idx);
                if (index > lock_pos) { delta = lock_pos - index; return; }
                update_result = true;
            }

            if (tokens.Count == 0) {
                IEditor new_token = CreateToken(keyCode, shift, ctrl, alt, out delta);
                if (new_token.Length > 0) tokens.Add(new_token);
                return;
            } else if (tokens[0] == result_token) {
                IEditor new_token = CreateToken(keyCode, shift, ctrl, alt, out delta);
                if (new_token.Length > 0) tokens.InsertRange(0, [new_token]);
                return;
            }

            delta = 0;
            if (keyCode == Keys.Back) {
                Backspace(index, out delta); return; }
            if (keyCode == Keys.Delete) {
                Backspace(index + 1, out _); return; }
            if (keyCode == KeysEx.C) {
                Clear(); return; }

            bool space_key = keyCode == Keys.Space;
            int idx = tokens.Index2Idx(index, space_key);
            IEditor token = tokens[idx];
            if (token == result_token) token = tokens[--idx];
            int size = token.Length;
            index -= tokens.Qsum_get(idx); // теперь от 0 до size, не включая size

            if (keyCode == KeysEx.CE) {
                int token_length;
                try {
                    if (token.IsZero) token_length = 0;
                    else { token.Clear(); token_length = token.Length; }
                } catch (NotImplementedException) { token_length = 0; }
                delta = token_length - index;
                tokens.Resize(idx, token_length - size);
                return;
            }

            if (token is SpaceToken) {
                if (!space_key) {
                    IEditor middle = CreateToken(keyCode, shift, ctrl, alt, out delta);
                    if (middle.Length <= 0) return;

                    SpaceToken left = new(index);
                    SpaceToken right = new(size - index);
                    tokens.RemoveAt(idx, false);
                    tokens.InsertRange(idx, [left, middle, right]);
                    return;
                }
                token.Handler(keyCode, shift, ctrl, alt, index, out delta);
                tokens.Resize(idx, token.Length - size);
                return;
            }
            
            if (token is ComplexEditor complex) {
                MathToken? math_token = keyCode != Keys.Subtract && keyCode != Keys.Divide ? KeyCode_to_MathToken(keyCode, shift) : null;
                if ((space_key || math_token is not null) && complex.Slice(index, out ComplexEditor left, out ComplexEditor right)) {
                    tokens.RemoveAt(idx, false);
                    IEditor new_token = math_token is not null ? math_token : new SpaceToken();
                    tokens.InsertRange(idx, [left, new_token, right]);
                    delta = new_token.Length;
                    return;
                }
                token.Handler(keyCode, shift, ctrl, alt, index, out delta);
                tokens.Resize(idx, token.Length - size);
                return;
            }
            
            if (token is MathToken) {
                if (index > 0 && index < token.Length) return;

                IEditor new_token = CreateToken(keyCode, shift, ctrl, alt, out delta);
                if (new_token.Length <= 0) return;

                tokens.InsertRange(index <= 0 ? idx : idx + 1, [new_token]);
                return;
            }

            update_result = false;
            return;
        }
        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) {
            HandlerWrap(keyCode, shift, ctrl, alt, index, out delta, out bool update_result);
            if (update_result) PrintResult();
            return Text;
        }





        private class ColorToken(RichTextBox rich, int start, int length) {
            public void Red(int current) {
                if (start <= current && current - start <= length) return;

                rich.Select(start, length);
                rich.SelectionBackColor = Color.Red;
            }
        }
        public void Colorize(RichTextBox rich) {
            // теперь изменение SelectionStart/SelectionLength не даст визуального эффекта
            // за счёт MyRichTextBox с включенным методом Updater

            int idx = 0;

            IEditor? prev_token = null;
            ColorToken? last_token = null;
            int level = 0;
            List<ColorToken> chain = [];
            int current = tokens.LastIndex;

            foreach (IEditor token in tokens) {
                int start = tokens.Qsum_get(idx);
                int end = tokens.Qsum_get(++idx);
                int length = end - start;

                bool is_result = result_mode && idx > result_token_idx && idx < tokens.Count;

                if (token == MathToken.L_sqbr) level++;
                if (token == MathToken.R_sqbr) level--;

                bool error = false;
                if (token is not SpaceToken) {
                    if (prev_token is null) {
                        if (token.IsBinaryOperation()) error = true;
                    } else {
                        if (prev_token.IsBinaryOperation() && token.IsBinaryOperation()) error = true;
                        if (prev_token is ComplexEditor && token is ComplexEditor) error = true;
                    }
                    // if (prev_token == MathToken.L_sqbr && token == MathToken.R_sqbr) error = true;
                    if (level < 0) { level = 0; error = true; }

                    if (token.IsUnaryOperation()) chain.Add(new ColorToken(rich, start, length));
                    else {
                        if (token is not ComplexEditor && token != MathToken.L_sqbr)
                            foreach (var err_token in chain) err_token.Red(current);
                        chain.Clear();
                    }

                    prev_token = token;
                    last_token = token is MathToken && !token.IsBracket() ? new ColorToken(rich, start, length) : null;
                }

                if (error && start <= current && current <= end) error = false;

                rich.Select(start, length);
                rich.SelectionBackColor =
                    is_result ? Color.White :
                    error ? Color.Red :
                    token switch {
                        SpaceToken => Color.PaleTurquoise,
                        ComplexEditor => Color.SpringGreen,
                        MathToken => Color.DeepSkyBlue,
                        _ => Color.White
                    };
                rich.SelectionColor =
                    is_result ? Color.Gray :
                    token switch {
                        MathToken => Color.Blue,
                        MessageToken => Color.Red,
                        _ => Color.Black
                    };
            }

            foreach (var err_token in chain) err_token.Red(current);
            last_token?.Red(current);
        }



        public bool CurrentToken(out IEditor token) {
            try {
                token = tokens.CurrentToken;
                return true;
            } catch (NotImplementedException) {
                MessageBox.Show("Нет токенов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                token = ComplexEditor.Empty;
                return false;
            }
        }
        public bool CurrentNumber(out ANumber number) {
            if (!CurrentToken(out IEditor i_token)) {
                number = BigInt.Zero; return false;
            }
            if (i_token is not ComplexEditor token) {
                MessageBox.Show($"Операции в памяти допустимы только с числовыми токенами. Текущий выбранный тип: {i_token.GetType().Name}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                number = BigInt.Zero; return false;
            }
            try { number = token.Value; }
            catch (Exception e) {
                MessageBox.Show(e.Message, "Ошибка формата", MessageBoxButtons.OK, MessageBoxIcon.Error);
                number = BigInt.Zero; return false;
            }
            return true;
        }

        public IEditor Parse() => tokens.Parse();



        private bool result_mode = false;
        private static readonly MessageToken result_token = new(" ");
        private int result_token_idx = -1;

        private void PrintResult() {
            if (result_mode) RemoveResult();

            result_token_idx = tokens.Count;
            tokens.Add(result_token); // имитатор пробела ради своего Syntax Hightlight
            tokens.Add(MathToken.result);
            tokens.Add(new SpaceToken());
            IEditor result;
            try {
                result = new ComplexEditor(Value);
            } catch (Exception err) {
                result = new MessageToken(err.Message);
            }
            tokens.Add(result);
            result_mode = true;
        }
        private void RemoveResult() {
            if (!result_mode) return;

            tokens.RemoveLast(); // result
            tokens.RemoveLast(); // new SpaceToken()
            tokens.RemoveLast(); // MathToken.result
            tokens.RemoveLast(); // first_result
            result_mode = false;
            result_token_idx = -1;
        }
        public void SwitchResultMode(bool mode) {
            if (mode) PrintResult();
            else RemoveResult();
        }
    }
}
