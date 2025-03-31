using Calculator.editors;
using ConsoleApp;
using System.Windows.Forms;

namespace Calculator {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            UpdateUI();
            inputRichTextBox.Text = editor.Text;
            InitDigitButtons();
        }



        private readonly TokenEditor editor = new();

        private void InputRichTextBox_TextChanged(object sender, EventArgs e) {
            // if (sender is not RichTextBox richTextBox) return;


        }

        private void UpdateUI(RichTextBox? rich = null) {
            rich ??= inputRichTextBox;
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
            {'-', Keys.Subtract}, {'\b', Keys.Back}, {'\x7f', Keys.Delete},
            {',', Keys.Oemcomma}, {'.', Keys.Decimal}, {'/', Keys.Divide}, {'i', Keys.I},
            {' ', Keys.Space},
            {'+', Keys.Add}, {'*', Keys.Multiply}, {'S', Keys.S},
        };

        private void Remover(RichTextBox richTextBox, int count) {
            int start = richTextBox.SelectionStart + count;
            while (count > 0) {
                /*string text =*/
                editor.Handler(Keys.Back, false, false, false, start, out int delta);
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
            text = text.Replace("+i", "i").Replace("-i", "i-").Replace("Sqr", "S");
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
            if (sender is not MyRichTextBox rich) return;

            // MessageBox.Show("KeyDown: '" + rich.Text + "', " + e.KeyValue + ", '" + e.KeyData + "', " + e.KeyCode);
            keyLabel.Text = "\n" + e.KeyData;

            Keys modifiers = e.Modifiers;
            bool shift = ((modifiers & Keys.Shift) != 0) ^ Console.CapsLock;
            bool ctrl = (modifiers & Keys.Control) != 0;
            bool alt = (modifiers & Keys.Alt) != 0; // других модификаторов просто нет

            Keys keyCode = e.KeyCode;
            Keys origKeyCode = keyCode;
            if (keyCode == Keys.OemPeriod) keyCode = Keys.Decimal; // NumLock mode + Delete = Decimal O_o
            else if (keyCode == Keys.OemMinus) keyCode = Keys.Subtract;
            else if (keyCode >= Keys.NumPad0 && keyCode <= Keys.NumPad9) keyCode = keyCode - Keys.NumPad0 + Keys.D0;
            else if (keyCode == Keys.Oemplus) keyCode = Keys.Add;
            else if (keyCode == Keys.Oem2) keyCode = Keys.Divide;
            else if (keyCode == Keys.D8 && shift) keyCode = Keys.Multiply; // Shift + 8 = *
            if (keyCode != origKeyCode) keyLabel.Text += "\n-> " + (keyCode | modifiers);

            if (arrows.Contains(keyCode)) return; // встроенное перемещение курсора и выделение текста клавиатурой
            if (modifier_keys.Contains(keyCode)) return; // игнорируем обработку клавиш-модификаторов (иначе ctrl сразу сбросит выделение)
            if (ctrl && (keyCode == Keys.C || keyCode == Keys.A)) return; // встроенный Ctrl + A и Ctrl + C

            try {
                rich.BeginUpdate(); // сработало!!!!!

                if (rich.SelectionLength > 0 && remove_keys.Contains(keyCode)) { // удаление выделенного текста
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

                UpdateUI(rich);
                editor.Colorize(rich);
            } finally {
                rich.EndUpdate();
            }
        }



        private Button[]? digit_buttons = null;
        private void InitDigitButtons() {
            digit_buttons = [
                button_D0, button_D1, button_D2, button_D3,
                button_D4, button_D5, button_D6, button_D7,
                button_D8, button_D9, button_DA, button_DB,
                button_DC, button_DD, button_DE, button_DF,
            ];
        }
        private void SetValue(int numSys) {
            numSysNumericUpDown.Value = numSys;
            numSysTrackBar.Value = numSys;
            editor.NumSys = numSys;
            UpdateUI();

            if (digit_buttons is null) return;
            for (int i = 0; i < 16; i++)
                digit_buttons[i].Enabled = i < numSys;
        }
        private void NumSysTrackBar_ValueChanged(object sender, EventArgs e) {
            if (sender is not TrackBar trackBar) return;

            SetValue(trackBar.Value);
        }

        private void NumSysNumericUpDown_ValueChanged(object sender, EventArgs e) {
            if (sender is not NumericUpDown numericUpDown) return;

            int value = Convert.ToInt32(Math.Floor(numericUpDown.Value));
            SetValue(value);
        }

        private void InputRichTextBox_SelectionChanged(object sender, EventArgs e) {
            if (sender is not RichTextBox rich) return;

            editor.SetLastIndex(rich.SelectionStart);
            try {
                int value = editor.NumSys;
                SetValue(value);

                numSysTrackBar.Enabled = true;
                numSysNumericUpDown.Enabled = true;
            } catch (NotImplementedException) {
                numSysTrackBar.Enabled = false;
                numSysNumericUpDown.Enabled = false;
            }
        }



        bool keyboard_shift = false;

        private void ButtonDigit_Click(object sender, EventArgs e) {
            if (sender is not Button button || button.Tag is not Keys charCode) return;

            if (keyboard_shift) charCode |= Keys.Shift;

            InputRichTextBox_KeyDown(inputRichTextBox, new KeyEventArgs(charCode));
        }

        private void ButtonShift_Click(object sender, EventArgs e) {
            keyboard_shift = !keyboard_shift;
            radioButton_shift.Checked = keyboard_shift;
        }
        private void RadioButtonShift_Click(object sender, EventArgs e) {
            if (sender is RadioButton radioButton)
                radioButton.Checked = keyboard_shift;
        }

        private void ButtonInv_Click(object sender, EventArgs e) {

        }
    }
}
