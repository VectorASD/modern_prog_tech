using Calculator.editors;
using ConsoleApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.tokens {
    public class TokenList : IEditor, IEnumerable<IEditor> {
        private readonly List<IEditor> tokens = [];
        private readonly List<int> Qsum = [0]; // кумулятивная сумма

        public IEnumerator<IEditor> GetEnumerator() => tokens.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => tokens.GetEnumerator();
        public int Count => tokens.Count;
        public IEditor this[int key] {
            get => tokens[key];
            set => throw new NotImplementedException();
        }



        public string Text {
            get => string.Concat(tokens);
            set => throw new NotImplementedException();
        }

        public ANumber Value => tokens.Count == 0 ? BigInt.Zero : tokens.First().Value;

        private int numSys = 10;
        public int NumSys {
            get { return numSys; }
            set { numSys = value; }
        }
        public int Length => tokens.Sum(x => x.Length);
        public bool IsNegative => throw new NotImplementedException();

        public bool IsZero => throw new NotImplementedException();



        private bool CombineTokenPair(int idx) {
            if (idx < 1 || idx >= tokens.Count) return false;
            IEditor left = tokens[idx - 1];
            IEditor right = tokens[idx];
            if (left is SpaceToken space && right is SpaceToken) {
                space.Increment(right.Length);
                Resize(idx - 1, right.Length, false);
                RemoveAt(idx, false);
                UpdateToken(idx - 1);
                return true;
            }

            ComplexEditor? L_complex;
            if (MathToken.PartOfNumber(left)) L_complex = new(left.Text);
            else if (left is ComplexEditor complex) L_complex = complex;
            else L_complex = null;

            ComplexEditor? R_complex;
            if (MathToken.PartOfNumber(right)) R_complex = new(right.Text);
            else if (right is ComplexEditor complex) R_complex = complex;
            else R_complex = null;

            if (L_complex is not null && R_complex is not null) {
                if (L_complex.Combine(R_complex, out ComplexEditor result)) {
                    tokens[idx - 1] = result;
                    Resize(idx - 1, result.Length - L_complex.Length, false);
                    RemoveAt(idx, false);
                    UpdateToken(idx - 1);
                    return true;
                }
                return false;
            }

            // MessageBox.Show("tokens: " + left + "+" + right);
            return false;
        }

        private static bool Complex_to_MathToken(ComplexEditor complex, out MathToken result) {
            string text = complex.Length > 2 ? "" : complex.Text;
            MathToken? token = text switch {
                "-" => MathToken.subtract,
                "/" => MathToken.divide,
                "1/" => MathToken.inverse,
                _ => null,
            };
            result = token ?? MathToken.Default;
            return token is not null;
        }

        private void UpdateToken(int idx) {
            if (idx < 0 || idx >= tokens.Count) return;
            IEditor token = tokens[idx];

            if (token is ComplexEditor complex && Complex_to_MathToken(complex, out MathToken new_token))
                tokens[idx] = new_token;

            if (!CombineTokenPair(idx))
                CombineTokenPair(idx + 1);
        }



        public void Add(IEditor token) {
            tokens.Add(token);
            Qsum.Add(Qsum.Last() + token.Length);

            UpdateToken(0);
        }
        public void Resize(int idx, int size_delta, bool update = true) {
            for (int i = idx + 1; i < Qsum.Count; i++)
                Qsum[i] += size_delta;
            if (Qsum[idx] == Qsum[idx + 1]) RemoveAt(idx);
            else if (update) UpdateToken(idx);
        }
        public void InsertRange(int idx, IEnumerable<IEditor> tokens) {
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
        public void RemoveAt(int idx, bool combine = true) {
            int size = tokens[idx].Length;
            tokens.RemoveAt(idx);
            int i = idx;
            Qsum.RemoveAt(++i);
            if (size != 0)
                for (; i < Qsum.Count; i++)
                    Qsum[i] -= size;
            if (combine) CombineTokenPair(idx);
        }

        public int Index2Idx(int index, bool space) {
            int idx = Qsum.BinarySearch(index);
            if (idx < 0) idx = ~idx - 1;
            else if (idx > 0 && tokens[idx - 1] is not SpaceToken ^ space) idx--;
            return Math.Clamp(idx, 0, tokens.Count - 1);
        }
        public int Index2Idx_type2(int index, bool left) {
            int idx = Qsum.BinarySearch(index);
            if (idx < 0) idx = ~idx - 1;
            else if (idx > 0 && left) idx--;
            return Math.Clamp(idx, 0, tokens.Count - 1);
        }
        public int Qsum_get(int index) => Qsum[index];



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
        public string Clear() {
            foreach (var token in tokens) token.Clear();
            tokens.Clear();

            Qsum.Clear();
            Qsum.Add(0);

            return Text;
        }



        // эта логика уже прописывается в TokenEditor, ибо в том его и суть:

        public string AddSign(int index, out int delta) => throw new NotImplementedException();
        public string AddDigit(int digit, bool shift, int index, out int delta) => throw new NotImplementedException();
        public string AddZero(bool shift, int index, out int delta) => throw new NotImplementedException();
        public string Backspace(int index, out int delta) => throw new NotImplementedException();
        public string Handler(Keys keyCode, bool shift, bool ctrl, bool alt, int index, out int delta) =>
            throw new NotImplementedException();
    }
}
