using Calculator.editors;
using ConsoleApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.tokens {
    public class TokenList : AEditor, IEnumerable<IEditor> {
        private readonly List<IEditor> tokens;
        private readonly List<int>? Qsum; // кумулятивная сумма

        public override string Text {
            get => string.Concat(tokens);
            set => throw new NotImplementedException();
        }
        public override ANumber Value => tokens.Count == 0 ? BigInt.Zero : tokens.First().Value;



        public TokenList() { // для токенайзера (редактора токенов)
            tokens = [];
            Qsum = [0];
        }
        public TokenList(List<IEditor> tokens) { // для парсера
            this.tokens = tokens;
            Qsum = null;
        }

        public IEnumerator<IEditor> GetEnumerator() => tokens.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => tokens.GetEnumerator();
        public int Count => tokens.Count;
        public IEditor this[int key] {
            get => tokens[key];
            set => throw new NotImplementedException();
        }



        public static string Format(IEditor x) {
            return x switch {
                TokenList => $"{x}",
                MathToken math => $"Math({math.Quoted})",
                SpaceToken => $"Space({x.Length})", // теперь недостижимо после метода Brackets
                _ => x.Text,
            };
        }
        public override string ToString() => '{' + string.Join(", ", tokens.Select(Format)) + '}';





        public int LastIndex { get; set; } = 0;
        public IEditor CurrentToken => tokens[Index2Idx(LastIndex)];
        public override int NumSys {
            get => CurrentToken.NumSys;
            set => CurrentToken.NumSys = value;
        }
        public override int Length => tokens.Sum(x => x.Length);



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
            if (left.IsPartOfNumber()) L_complex = new(left.Text);
            else if (left is ComplexEditor complex) L_complex = complex;
            else L_complex = null;

            ComplexEditor? R_complex;
            if (right.IsBinaryOperation()) R_complex = new(right.Text);
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
            Qsum!.Add(Qsum.Last() + token.Length);

            UpdateToken(tokens.Count - 1);
        }
        public void RemoveLast() {
            tokens.RemoveAt(tokens.Count - 1);
            Qsum!.RemoveAt(Qsum.Count - 1);

            UpdateToken(tokens.Count - 1);
        }

        public void Resize(int idx, int size_delta, bool update = true) {
            for (int i = idx + 1; i < Qsum!.Count; i++)
                Qsum[i] += size_delta;
            if (Qsum[idx] == Qsum[idx + 1]) RemoveAt(idx);
            else if (update) UpdateToken(idx);
        }
        public void InsertRange(int idx, IEnumerable<IEditor> tokens) {
            int left = Qsum![idx];
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
            Qsum!.RemoveAt(++i);
            if (size != 0)
                for (; i < Qsum.Count; i++)
                    Qsum[i] -= size;
            if (combine) CombineTokenPair(idx);
        }

        public int Index2Idx(int index, bool space = false) {
            if (tokens.Count == 0) throw new NotImplementedException();

            int idx = Qsum!.BinarySearch(index);
            if (idx < 0) idx = ~idx - 1;
            else if (idx > 0 && tokens[idx - 1] is ComplexEditor ^ space) idx--;
            return Math.Clamp(idx, 0, tokens.Count - 1);
        }
        public int Index2Idx_type2(int index, bool left = true) {
            int idx = Qsum!.BinarySearch(index);
            if (idx < 0) idx = ~idx - 1;
            else if (idx > 0 && left) idx--;
            return Math.Clamp(idx, 0, tokens.Count - 1);
        }
        public int Qsum_get(int index) => Qsum![index];



        public string Debug() {
            List<int> res = [];
            for (int i = -3; i < 18; i++)
                res.Add(Qsum!.BinarySearch(i));
            string text = $"{ string.Join(", ", Qsum!) }\n{ string.Join(", ", res) }\n{ Parse() }";
            // MessageBox.Show(text);
            // Clipboard.SetText(text);
            /* Просмотр того, что выдаёт BinarySearch:
                0, 7, 10, 14
                -1, -1, -1, 0, -2, -2, -2, -2, -2, -2, 1, -3, -3, 2, -4, -4, -4, 3, -5, -5, -5   */
            return text + "\n";
        }

        public override string Clear() {
            foreach (var token in tokens)
                try { token.Clear(); } catch (NotImplementedException) { }
            tokens.Clear();

            Qsum!.Clear();
            Qsum.Add(0);

            return Text;
        }





        // API парсера токенов (переделывает из линейной структуры в древовидную)

        // обход: справа на лево,   операнд: справа
        private TokenList Unary(Func<IEditor, bool> check) {
            int max = tokens.Count - 1;
            if (check(tokens[max])) throw new SyntaxErrorException($"Унарная операция {Format(tokens[max])} не может быть конечным токеном");
            
            for (int i = max-1; i >= 0; i--) {
                IEditor token = tokens[i];
                if (!check(token)) continue;
                if (token is not MathToken math) throw new ArgumentException("Unary: аргумент должен фильтровать все не MathToken");

                IEditor right = tokens[i + 1]; // из-за прохода справа на лево, гарантируется, что справа нет MathToken
                tokens[i] = new UnaryToken(math, right);
                tokens.RemoveAt(i + 1); // right
            }
            return this;
        }

        // обход: слева на право,   операнд: слева и справа
        private TokenList Binary(Func<IEditor, bool> check) {
            int max = tokens.Count - 1;
            if (check( tokens[0] )) throw new SyntaxErrorException($"Бинарная операция {Format( tokens[0] )} не может быть начальным токеном");
            if (check(tokens[max])) throw new SyntaxErrorException($"Бинарная операция {Format(tokens[max])} не может быть конечным токеном");
            
            for (int i = 1; i < max; i++) {
                IEditor token = tokens[i];
                if (!check(token)) continue;
                if (token is not MathToken math) throw new ArgumentException("Binary: аргумент должен фильтровать все не MathToken");

                IEditor left = tokens[i - 1]; // из-за прохода слева на право, гарантируется, что слева нет MathToken
                // if (left is MathToken) throw new SyntaxErrorException($"Токен {Format(left)} не может быть левым операндом");
                IEditor right = tokens[i + 1];
                if (left is MathToken) throw new SyntaxErrorException($"Токен {Format(right)} не может быть правым операндом");

                tokens[i - 1] = new BinaryToken(left, math, right);
                tokens.RemoveAt(i); // math
                tokens.RemoveAt(i); // right
                i--; max -= 2;
            }
            return this;
        }

        private IEditor SimplifyTheEnd() {
            if (tokens.Count != 1) throw new SyntaxErrorException($"Недостаточный эффект всех Unary и Binary: {this} (2 числа без оператора между ними)");
            return tokens[0];
        }
        private IEditor Simplify() {
            return tokens.Count switch {
                0 => ComplexEditor.Zero,
                1 => tokens[0],
                _ => Unary(MathTokenExtensions.IsUnaryOperation)
                   .Binary(MathTokenExtensions.IsBinaryFirstOperation)
                   .Binary(MathTokenExtensions.IsBinarySecondOperation)
                   .SimplifyTheEnd()
            };
        }

        // обход: слева на право,   захват: круглые скобочки,   удаление: пробелы и всё после '='
        private static IEditor Brackets(IEnumerator<IEditor> it) {
            List<IEditor> result = [];
            bool prev_bracket = false;
            while (it.MoveNext()) {
                IEditor item = it.Current;
                if (item is SpaceToken) continue;
                if (item is MessageToken) {
                    while (it.MoveNext()) { }
                    break;
                }

                if (prev_bracket) {
                    if (item is ComplexEditor) result.Add(MathToken.multiply); // умножение между ')' и числом
                    prev_bracket = false;
                }

                if (item == MathToken.R_sqbr) break;

                if (item == MathToken.L_sqbr) {
                    if (result.Count > 0 && result.Last() is ComplexEditor) result.Add(MathToken.multiply); // умножение между числом и '('
                    result.Add(Brackets(it));
                    prev_bracket = true;
                } else result.Add(item);
            }

            return new TokenList(result).Simplify();
        }

        public IEditor Parse() => Brackets(tokens.GetEnumerator());
    }
}
