using Calculator.tokens;
using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.editors {
    public class TokenEditor : IEditor {
        private readonly List<IEditor> tokens = [];
        private readonly List<int> Qsum = [0]; // кумулятивная сумма
        private int numSys = 10;

        public string Text {
            get => string.Concat(tokens);
            set => throw new NotImplementedException();
        }
        public TokenEditor(string? init_text = null) {
            // DebugInit();
            if (init_text is not null) Text = init_text;
        }

        public ANumber Value => tokens.Count == 0 ? BigInt.Zero : tokens.First().Value;
        public int NumSys {
            get { return numSys; }
            set { numSys = value; }
        }
        public int Length => tokens.Sum(x => x.Length);
        public bool IsNegative => throw new NotImplementedException();



        private static MathToken[] PartOfNumber = [MathToken.subtract, MathToken.inverse, MathToken.divide];
        private bool CombineTokenPair(int idx) {
            if (idx < 1 || idx >= tokens.Count) return false;
            IEditor left = tokens[idx - 1];
            IEditor right = tokens[idx];
            if (left is SpaceToken space && right is SpaceToken) {
                space.Increment(right.Length);
                ResizeToken(idx - 1, right.Length, false);
                RemoveToken(idx, false);
                UpdateToken(idx - 1);
                return true;
            }

            ComplexEditor? L_complex;
            if (left is MathToken math && PartOfNumber.Contains(math)) L_complex = new(math.Text);
            else if (left is ComplexEditor complex) L_complex = complex;
            else L_complex = null;

            ComplexEditor? R_complex;
            if (right is MathToken math2 && PartOfNumber.Contains(math2)) R_complex = new(math2.Text);
            else if (right is ComplexEditor complex) R_complex = complex;
            else R_complex = null;

            if (L_complex is not null && R_complex is not null) {
                if (L_complex.Combine(R_complex, out ComplexEditor result)) {
                    tokens[idx - 1] = result;
                    ResizeToken(idx - 1, result.Length - L_complex.Length, false);
                    RemoveToken(idx, false);
                    UpdateToken(idx - 1);
                    return true;
                }
                return false;
            }

            // MessageBox.Show("tokens: " + left + "+" + right);
            return false;
        }
        private void UpdateToken(int idx) {
            if (idx < 0 || idx >= tokens.Count) return;
            IEditor token = tokens[idx];

            if (token is ComplexEditor complex && Complex_to_MathToken(complex, out MathToken new_token))
                tokens[idx] = new_token;

            if (!CombineTokenPair(idx))
                CombineTokenPair(idx + 1);
        }



        private void AddToken(IEditor token) {
            tokens.Add(token);
            Qsum.Add(Qsum.Last() + token.Length);

            UpdateToken(0);
        }
        private void ResizeToken(int idx, int size_delta, bool update = true) {
            for (int i = idx + 1; i < Qsum.Count; i++)
                Qsum[i] += size_delta;
            if (Qsum[idx] == Qsum[idx + 1]) RemoveToken(idx);
            else if (update) UpdateToken(idx);
        }
        private void InsertTokens(int idx, IEnumerable<IEditor> tokens) {
            int left = Qsum[idx];
            int sum = left;
            List<int> Qnew = [];
            List<IEditor> new_tokens = [];
            foreach (IEditor token in tokens) {
                int L = token.Length;
                if (L <= 0) continue; // та самая причина, почему отсюда пустых токенов НЕ бывает
                sum += L;
                new_tokens.Add(token);
                Qnew.Add(sum);
            }
            this.tokens.InsertRange(idx, new_tokens);
            int i = idx;
            Qsum.InsertRange(++i, Qnew);
            sum -= left;
            for (i += Qnew.Count; i < Qsum.Count; i++)
                Qsum[i] += sum;

            UpdateToken(idx);
            if (new_tokens.Count > 1) UpdateToken(idx + new_tokens.Count - 1);
        }
        private void RemoveToken(int idx, bool combine = true) {
            int size = tokens[idx].Length;
            tokens.RemoveAt(idx);
            int i = idx;
            Qsum.RemoveAt(++i);
            if (size != 0)
                for (; i < Qsum.Count; i++)
                    Qsum[i] -= size;
            if (combine) CombineTokenPair(idx);
        }

        private int Index2Idx(int index, bool space) {
            int idx = Qsum.BinarySearch(index);
            if (idx < 0) idx = ~idx - 1;
            else if (idx > 0 && tokens[idx - 1] is not SpaceToken ^ space) idx--;
            return Math.Clamp(idx, 0, tokens.Count - 1);
        }
        private int Index2Idx_type2(int index, bool left) {
            int idx = Qsum.BinarySearch(index);
            if (idx < 0) idx = ~idx - 1;
            else if (idx > 0 && left) idx--;
            return Math.Clamp(idx, 0, tokens.Count - 1);
        }



        public override string ToString() => Text;
        public bool IsZero => throw new NotImplementedException(); // unused

        public string AddSign(int index, out int delta) => // unused
            Handler(Keys.Subtract, false, false, false, index, out delta);

        public string AddDigit(int digit, bool shift, int index, out int delta) { // unused
            Keys keyCode = digit < 10 ? digit + Keys.D0 : digit - 10 + Keys.A;
            return Handler(keyCode, shift, false, false, index, out delta);
        }

        public string AddZero(bool shift, int index, out int delta) => AddDigit(0, shift, index, out delta); // unused

        public string Backspace(int index, out int delta) {
            int idx = Index2Idx_type2(index, true);
            index -= Qsum[idx];
            if (index <= 0) { delta = 0; return Text; }

            IEditor token = tokens[idx];
            if (token is MathToken) {
                RemoveToken(idx);
                delta = -index;
                return Text;
            }
            int size = token.Length;
            token.Handler(Keys.Back, false, false, false, index, out delta);
            ResizeToken(idx, token.Length - size);
            return Text;
        }

        public string Clear() { // unused
            foreach (var token in tokens) token.Clear();
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
        public string Debug() {
            List<int> res = [];
            for (int i = -3; i < 18; i++)
                res.Add(Qsum.BinarySearch(i));
            string text = string.Join(", ", Qsum) + '\n' + string.Join(", ", res);
            // MessageBox.Show(text);
            // Clipboard.SetText(text);
            /* Просмотр того, что выдаёт BinarySearch:
                0, 7, 10, 14
                -1, -1, -1, 0, -2, -2, -2, -2, -2, -2, 1, -3, -3, 2, -4, -4, -4, 3, -5, -5, -5   */
            return text;
        }

        private static MathToken? KeyCode_to_MathToken(Keys keyCode) {
            return keyCode switch {
                Keys.Add      => MathToken.add,
                Keys.Subtract => MathToken.subtract,
                Keys.Multiply => MathToken.multiply,
                Keys.Divide   => MathToken.divide,
                Keys.S        => MathToken.square,
                _ => null,
            };
        }
        private static bool Complex_to_MathToken(ComplexEditor complex, out MathToken result) {
            string text = complex.Length > 2 ? "" : complex.Text;
            MathToken? token = text switch {
                "-"  => MathToken.subtract,
                "/"  => MathToken.divide,
                "1/" => MathToken.inverse,
                _ => null,
            };
            result = token ?? MathToken.Default;
            return token is not null;
        }
        private static IEditor CreateToken(Keys keyCode, bool shift, bool ctrl, bool alt, out int delta) {
            if (keyCode == Keys.Space) {
                delta = 1;
                return new SpaceToken(1);
            }
            IEditor? token = KeyCode_to_MathToken(keyCode);
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
                if (new_token.Length > 0) AddToken(new_token);
                return Text;
            }

            delta = 0;
            if (keyCode == Keys.Back)
                return Backspace(index, out delta);
            if (keyCode == Keys.Delete)
                return Backspace(index + 1, out _);

            bool space_key = keyCode == Keys.Space;
            int idx = Index2Idx(index, space_key);
            IEditor token = tokens[idx];
            int size = token.Length;
            index -= Qsum[idx]; // теперь от 0 до size, не включая size

            if (token is SpaceToken) {
                if (!space_key) {
                    IEditor middle = CreateToken(keyCode, shift, ctrl, alt, out delta);
                    if (middle.Length <= 0) return Text;

                    SpaceToken left = new(index);
                    SpaceToken right = new(size - index);
                    RemoveToken(idx);
                    InsertTokens(idx, [left, middle, right]);
                    return Text;
                }
                token.Handler(keyCode, shift, ctrl, alt, index, out delta);
                ResizeToken(idx, token.Length - size);
                return Text;
            }
            
            if (token is ComplexEditor complex) {
                MathToken? math_token = keyCode != Keys.Subtract && keyCode != Keys.Divide ? KeyCode_to_MathToken(keyCode) : null;
                if ((space_key || math_token is not null) && complex.Slice(index, out ComplexEditor left, out ComplexEditor right)) {
                    RemoveToken(idx, false);
                    InsertTokens(idx, [left, math_token is not null ? math_token : new SpaceToken(), right]);
                    delta = 1;
                    return Text;
                }
                token.Handler(keyCode, shift, ctrl, alt, index, out delta);
                ResizeToken(idx, token.Length - size);
                return Text;
            }
            
            if (token is MathToken) {
                if (index > 0 && index < token.Length) return Text;

                IEditor new_token = CreateToken(keyCode, shift, ctrl, alt, out delta);
                if (new_token.Length <= 0) return Text;

                InsertTokens(index <= 0 ? idx : idx + 1, [new_token]);
                return Text;
            }

            return Text;
        }

        public void Colorize(RichTextBox rich) {
            int saved_start = rich.SelectionStart;
            int saved_length = rich.SelectionLength;
            int idx = 0;
            foreach (IEditor token in tokens) {
                int start = Qsum[idx];
                int end = Qsum[++idx];
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
