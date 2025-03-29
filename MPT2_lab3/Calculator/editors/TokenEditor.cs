using Calculator.tokens;
using ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
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



        private void AddToken(IEditor token) {
            tokens.Add(token);
            Qsum.Add(Qsum.Last() + token.Length);
        }
        private void ResizeToken(int idx, int size_delta) {
            for (int i = idx + 1; i < Qsum.Count; i++)
                Qsum[i] += size_delta;
            if (Qsum[idx] == Qsum[idx + 1]) RemoveToken(idx);
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
            Qsum.InsertRange(++idx, Qnew);
            sum -= left;
            for (idx += Qnew.Count; idx < Qsum.Count; idx++)
                Qsum[idx] += sum;
        }
        private void RemoveToken(int idx) {
            int size = tokens[idx].Length;
            tokens.RemoveAt(idx);
            int i = idx;
            Qsum.RemoveAt(++i);
            if (size != 0)
                for (; i < Qsum.Count; i++)
                    Qsum[i] -= size;
            else CombineTokenPair(idx);
        }
        private void CombineTokenPair(int idx) {
            if (idx < 1 || idx >= tokens.Count) return;
            IEditor left = tokens[idx - 1];
            IEditor right = tokens[idx];
            if (left is SpaceToken space && right is SpaceToken) {
                space.Increment(right.Length);
                ResizeToken(idx - 1, right.Length);
                RemoveToken(idx);
                return;
            }
            if (left is ComplexEditor L_complex && right is ComplexEditor R_complex) {
                if (L_complex.Combine(R_complex, out ComplexEditor result)) {
                    tokens[idx - 1] = result;
                    ResizeToken(idx - 1, result.Length - L_complex.Length);
                    RemoveToken(idx);
                }
                return;
            }

            MessageBox.Show("tokens: " + left + "+" + right);
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
            Handler(Keys.OemMinus, false, false, false, index, out delta);

        public string AddDigit(int digit, bool shift, int index, out int delta) { // unused
            Keys keyCode = digit < 10 ? digit + Keys.D0 : digit - 10 + Keys.A;
            return Handler(keyCode, shift, false, false, index, out delta);
        }

        public string AddZero(bool shift, int index, out int delta) => AddDigit(0, shift, index, out delta); // unused

        public string Backspace(int index, out int delta) {
            int idx = Index2Idx_type2(index, true);
            IEditor token = tokens[idx];
            int size = token.Length;
            index -= Qsum[idx];
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

        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) {
            if (tokens.Count == 0) AddToken(new ComplexEditor());

            if (keyCode == Keys.Back)
                return Backspace(index, out delta);
            if (keyCode == Keys.Delete)
                return Backspace(index + 1, out delta);

            bool space_key = keyCode == Keys.Space;
            int idx = Index2Idx(index, space_key);
            IEditor token = tokens[idx];
            int size = token.Length;
            index -= Qsum[idx]; // теперь от 0 до size, не включая size
            if (token is SpaceToken) {
                if (!space_key) {
                    ComplexEditor middle = new();
                    middle.Handler(keyCode, shift, ctrl, alt, 0, out delta);
                    if (middle.Length <= 0) return Text;

                    SpaceToken left = new(index);
                    SpaceToken right = new(size - index);
                    RemoveToken(idx);
                    InsertTokens(idx, [left, middle, right]);
                    return Text;
                }
            } else if (token is ComplexEditor complex) {
                if (space_key && complex.Slice(index, out ComplexEditor left, out ComplexEditor right)) {
                    RemoveToken(idx);
                    InsertTokens(idx, [left, new SpaceToken(), right]);
                    delta = 1;
                    return Text;
                }
            }

            token.Handler(keyCode, shift, ctrl, alt, index, out delta);
            ResizeToken(idx, token.Length - size);
            // Debug();
            return Text;
        }
    }
}
