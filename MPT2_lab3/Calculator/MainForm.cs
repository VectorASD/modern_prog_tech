using Calculator.editors;
using ConsoleApp;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Calculator {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            UpdateUI();
            inputRichTextBox.Text = editor.Text;
        }



        private readonly TokenEditor editor = new();

        private void InputRichTextBox_TextChanged(object sender, EventArgs e) {
            // if (sender is not RichTextBox richTextBox) return;


        }

        private void UpdateUI() {
            try {
                ANumber value = editor.Value;
                outputLabel.Text = "Type: " + value.GetType().Name + "\nRaw: " + value.Raw + "\n" + editor.Debug();
            } catch (Exception err) {
                outputLabel.Text = "Error: " + err.Message + "\n" + editor.Debug();
            }
        }

        // ѕо прежнему, сначала KeyPress, потом KeyDown
        // e.Handled блокирует ввод только в KeyPress
        // тем ни менее, свойство Text обновл€етс€ только после KeyDown
        // KeyPress Ќ≈ чувствителен к спец-символам (Ctrl, Shift, Alt, Esc, NumLock, Capital...)
        // KeyPress работает с фактическими char, а KeyDown - с физическими клавишами
        // ≈сли убрать все MessageBox.Show, то пор€док KeyPress и KeyDown будто помен€етс€ :/

        private void InputRichTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            // if (sender is not RichTextBox richTextBox) return;

            // MessageBox.Show("KeyPress: '" + richTextBox.Text + "', '" + e.KeyChar + "'");

            e.Handled = true;
        }

        private static readonly Keys[] arrows = [Keys.Left, Keys.Right, Keys.Up, Keys.Down];
        private static readonly Keys[] modifier_keys = [Keys.ControlKey, Keys.ShiftKey, Keys.Home];
        private static readonly Keys[] remove_keys = [Keys.Back, Keys.Delete, Keys.X];
        private static readonly Dictionary<char, Keys> char2keys = new() {
            {'0', Keys.D0}, {'1', Keys.D1}, {'2', Keys.D2}, {'3', Keys.D3}, {'4', Keys.D4},
            {'5', Keys.D5}, {'6', Keys.D6}, {'7', Keys.D7}, {'8', Keys.D8}, {'9', Keys.D9},
            {'a', Keys.A}, {'b', Keys.B}, {'c', Keys.C}, {'d', Keys.D}, {'e', Keys.E}, {'f', Keys.F},
            {'A', Keys.A | Keys.Shift}, {'B', Keys.B | Keys.Shift}, {'C', Keys.C | Keys.Shift},
            {'D', Keys.D | Keys.Shift}, {'E', Keys.E | Keys.Shift}, {'F', Keys.F | Keys.Shift},
            {'-', Keys.OemMinus}, {'\b', Keys.Back}, {'\x7f', Keys.Delete},
            {',', Keys.Oemcomma}, {'.', Keys.OemPeriod}, {'/', Keys.Oem2}, {'i', Keys.I},
            {' ', Keys.Space},
        };

        private void Remover(RichTextBox richTextBox, int count) {
            int start = richTextBox.SelectionStart + count;
            while (count > 0) {
                /*string text =*/ editor.Handler(Keys.Back, false, false, false, start, out int delta);
                // MessageBox.Show($"start: {start}\ndelta: {delta}\ntext: {text}");
                if (delta >= 0) break; // Ќа вс€кий случай, от вечного зацикливани€ ;'-}
                start += delta;
                count += delta;
            }
            richTextBox.Text = editor.Text;
            richTextBox.SelectionStart = start;
        }

        private static void CopyText(RichTextBox richTextBox) {
            string text = richTextBox.SelectedText;
            if (string.IsNullOrEmpty(text)) return;
            Clipboard.SetText(text);
        }

        private void PasteText(RichTextBox richTextBox, string text) {
            int start = richTextBox.SelectionStart;
            text = text.Replace("+i", "i").Replace("-i", "i-");
            foreach (char letter in text) {
                if (!char2keys.TryGetValue(letter, out Keys value)) continue;
                KeyEventArgs keyData = new(value);
                Keys modifiers = keyData.Modifiers;

                bool shift = ((modifiers & Keys.Shift) != 0);
                Keys keyCode = keyData.KeyCode;
                editor.Handler(keyCode, shift, false, false, start, out int delta);
                start += delta;
            }
            richTextBox.Text = editor.Text;
            richTextBox.SelectionStart = start;
        }

        private void InputRichTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (sender is not RichTextBox rich) return;

            // MessageBox.Show("KeyDown: '" + rich.Text + "', " + e.KeyValue + ", '" + e.KeyData + "', " + e.KeyCode);

            Keys modifiers = e.Modifiers;
            bool shift = ((modifiers & Keys.Shift) != 0) ^ Console.CapsLock;
            bool ctrl = (modifiers & Keys.Control) != 0;
            bool alt = (modifiers & Keys.Alt) != 0; // других модификаторов просто нет

            Keys keyCode = e.KeyCode;

            if (arrows.Contains(keyCode)) { } // встроенное перемещение курсора и выделение текста клавиатурой
            else if (modifier_keys.Contains(keyCode)) { } // игнорируем обработку клавиш-модификаторов (иначе ctrl сразу сбросит выделение)
            else if (ctrl && (keyCode == Keys.C || keyCode == Keys.A)) { } // встроенный Ctrl + A и Ctrl + C
            else if (rich.SelectionLength > 0 && remove_keys.Contains(keyCode)) { // удаление выделенного текста
                if (keyCode == Keys.X) CopyText(rich);
                int count = rich.SelectionLength;
                Remover(rich, count);
                e.Handled = true;
            } else if (ctrl && keyCode == Keys.V) { // сво€ вставка Ctrl + V
                int count = rich.SelectionLength;
                if (count > 1) Remover(rich, count); // комбинаци€ удалени€ выделенного текста со вставкой

                PasteText(rich, Clipboard.GetText());
                e.Handled = true;
            } else { // обычное развитие событий
                int count = rich.SelectionLength;
                if (count > 1) Remover(rich, count); // комбинаци€ удалени€ выделенного текста с обычным развитием событий

                int start = rich.SelectionStart;
                rich.Text = editor.Handler(keyCode, shift, ctrl, alt, start, out int delta);
                rich.SelectionStart = Math.Max(0, start + delta);
                e.Handled = true; // блокирует встроенное дополнительное (и лишнее) управление SelectionStart
            }

            UpdateUI();
        }

        private void NumSysTrackBar_ValueChanged(object sender, EventArgs e) {
            if (sender is not TrackBar trackBar) return;

            int value = trackBar.Value;
            numSysNumericUpDown.Value = value;
            editor.NumSys = value;
            UpdateUI();
        }

        private void NumSysNumericUpDown_ValueChanged(object sender, EventArgs e) {
            if (sender is not NumericUpDown numericUpDown) return;

            int value = Convert.ToInt32(Math.Floor(numericUpDown.Value));
            numSysTrackBar.Value = value;
            editor.NumSys = value;
            UpdateUI();
        }
    }
}
