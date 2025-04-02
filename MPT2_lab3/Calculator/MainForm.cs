using Calculator.editors;
using Calculator.extensions;
using ConsoleApp;
using System.Windows.Forms;

namespace Calculator {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            // отваливаетс€ из редактора:
            button_inv.Tag = KeysEx.AddInv;
            button_subtract.Tag = KeysEx.AddSubtract;
            button_CE.Tag = KeysEx.CE;
            button_C.Tag = KeysEx.C;
        }

        private void MainForm_Load(object sender, EventArgs e) {
            InitDigitButtons();
            UpdateUI();
            inputRichTextBox.Text = editor.Text;

            foreach (Control control in this.Descendants<Control>())
                if (control is Button || control is RadioButton) control.GotFocus += Control_GotFocus;
        }
        private void Control_GotFocus(object? sender, EventArgs e) {
            inputRichTextBox.Focus();
        }



        private readonly TokenEditor editor = new();
        private readonly Memory memory = new();

        private void UpdateUI() {
            /* string first, second;
            try {
                ANumber value = editor.Value;
                first = "(Value) Type: " + value.GetType().Name + "\nRaw: " + value.Raw;
            } catch (Exception err) { first = "(Value) Error: " + err.Message; }*/

            if (debug_mode) {
                string second;
                try {
                    second = "(Debug) " + editor.Debug();
                } catch (Exception err) { second = "(Debug) Error: " + err.Message; }

                // outputLabel.Text = first + "\n" + second;
                outputLabel.Text = second;
            } else outputLabel.Text = "";



            memoryState.Text = memory.State;
            button_MR.Enabled = memory.Number is not null;
            debugButton.Text = "Debug (" + (debug_mode ? "on" : "off") + ")";
            debugButton.ForeColor = debug_mode ? Color.Green : Color.Gray;

            if (digit_buttons is not null)
                for (int i = 10; i < 16; i++)
                    digit_buttons[i].Text = keyboard_shift ?
                        digit_buttons[i].Text.ToUpper() :
                        digit_buttons[i].Text.ToLower();
        }

        private void UpdateColor() {
            MyRichTextBox rich = inputRichTextBox;
            rich.Updater(() => editor.Colorize(rich));
        }





        // ~~~ ќбработка клавиш физической клавиатуры

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
            {'(', Keys.D9 | Keys.Shift}, {')', Keys.D0 | Keys.Shift},
        };

        private void Remover(MyRichTextBox richTextBox, int count, bool check = true) {
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
            richTextBox.SetSelection(start, 0);
            if (check) CheckSelection(richTextBox);
        }

        private static void CopyText(RichTextBox richTextBox) {
            string text = richTextBox.SelectedText;
            if (string.IsNullOrEmpty(text)) return;
            Clipboard.SetText(text);
        }

        private void PasteText(MyRichTextBox richTextBox, string text) {
            int start = richTextBox.SelectionStart;
            text = text.Replace("+i", "i").Replace("-i", "i-").Replace("Sqr", "S");
            foreach (char letter in text) {
                if (!char2keys.TryGetValue(letter, out Keys value)) continue;
                KeyEventArgs keyData = new(value);
                Keys modifiers = keyData.Modifiers;

                bool shift = ((modifiers & Keys.Shift) != 0);
                Keys keyCode = (Keys)keyData.KeyValue; // e.KeyCode потер€ет значени€ из моего KeysEx
                editor.Handler(keyCode, shift, false, false, start, out int delta);
                start += delta;
            }
            richTextBox.Text = editor.Text;
            richTextBox.SetSelection(start, 0);
            CheckSelection(richTextBox);
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

        private void InputRichTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (sender is not MyRichTextBox rich) return;

            // MessageBox.Show("KeyDown: '" + rich.Text + "', " + e.KeyValue + ", '" + e.KeyData + "', " + e.KeyCode);
            keyLabel.Text = "\n" + e.KeyData;

            Keys modifiers = e.Modifiers;
            bool shift = ((modifiers & Keys.Shift) != 0) ^ Console.CapsLock;
            bool ctrl = (modifiers & Keys.Control) != 0;
            bool alt = (modifiers & Keys.Alt) != 0; // других модификаторов просто нет

            Keys keyCode = (Keys)e.KeyValue; // e.KeyCode потер€ет значени€ из моего KeysEx
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

            if (keyCode == Keys.Enter) {
                ButtonResult_Click(123, new());
                e.Handled = true;
                return;
            }
            if (keyCode == KeysEx.C) {
                DialogResult result = MessageBox.Show("Ёто действие приведЄт к полной очистке всего содержимого!", "”верены?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;
            }

            rich.Updater(() => { // сработало!!!!!
                if (keyCode == KeysEx.C && result_mode) ButtonResult_Click(123, new());

                if (rich.SelectionLength > 0 && remove_keys.Contains(keyCode)) { // удаление выделенного текста
                    if (keyCode == Keys.X) CopyText(rich);
                    int count = rich.SelectionLength;
                    Remover(rich, count);
                    e.Handled = true;
                } else if (ctrl && keyCode == Keys.V) { // сво€ вставка Ctrl + V
                    int count = rich.SelectionLength;
                    if (count > 0) Remover(rich, count, false); // комбинаци€ удалени€ выделенного текста со вставкой

                    PasteText(rich, Clipboard.GetText());
                    e.Handled = true;
                } else { // обычное развитие событий
                    int count = rich.SelectionLength;
                    if (count > 0) Remover(rich, count, false); // комбинаци€ удалени€ выделенного текста с обычным развитием событий

                    int start = rich.SelectionStart;
                    rich.Text = editor.Handler(keyCode, shift, ctrl, alt, start, out int delta);
                    rich.SetSelectionStart(start + delta);
                    CheckSelection(rich);
                    e.Handled = true; // блокирует встроенное дополнительное (и лишнее) управление SelectionStart
                }

                UpdateUI();
                editor.Colorize(rich);
            });
        }



        // ~~~  оманды ввода системы счислени€

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
            try { editor.NumSys = numSys; } catch (NotImplementedException) { }

            UpdateUI();
            UpdateColor();

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

        private void CheckSelection(MyRichTextBox rich) {
            editor.SetLastIndex(rich.SelectionStart);
            try {
                int value = editor.NumSys;
                SetValue(value);

                numSysTrackBar.Visible = numSysNumericUpDown.Visible = true;
            } catch (NotImplementedException) {
                SetValue(16);
                numSysTrackBar.Visible = numSysNumericUpDown.Visible = false;
                UpdateColor();
            }
        }
        private void InputRichTextBox_SelectionChanged(object sender, EventArgs e) {
            if (sender is not MyRichTextBox rich || rich.updateMode || rich.SelectionLength > 0) return;

            CheckSelection(rich);
        }



        // ~~~  оманды виртуальной клавиатуры

        bool keyboard_shift = false;
        bool result_mode = false;
        bool debug_mode = false;

        private void ButtonDigit_Click(object sender, EventArgs e) {
            if (sender is not Button button || button.Tag is not Keys charCode) return;

            if (keyboard_shift && charCode >= Keys.A && charCode <= Keys.F) charCode |= Keys.Shift;

            InputRichTextBox_KeyDown(inputRichTextBox, new KeyEventArgs(charCode));
        }

        private void ButtonShift_Click(object sender, EventArgs e) {
            keyboard_shift = !keyboard_shift;
            radioButton_shift.Checked = keyboard_shift;
            UpdateUI();
        }
        private void ButtonResult_Click(object sender, EventArgs e) {
            result_mode = !result_mode;
            radioButton_result.Checked = result_mode;

            inputRichTextBox.Updater(() => {
                editor.SwitchResultMode(result_mode);
                inputRichTextBox.Text = editor.Text;
                UpdateUI();
                UpdateColor();
            });
        }

        private void Button_MC_Click(object sender, EventArgs e) {
            memory.Clear();
            UpdateUI();
        }
        private void Button_MS_Click(object sender, EventArgs e) {
            if (editor.CurrentNumber(out ANumber number))
                memory.Number = number;
            UpdateUI();
        }
        private void Button_MR_Click(object sender, EventArgs e) {
            ANumber? number = memory.Number;
            if (number is not null)
                PasteText(inputRichTextBox, number.ToString());
        }
        private void Button_Mplus_Click(object sender, EventArgs e) {
            if (editor.CurrentNumber(out ANumber number))
                memory.Add(number);
            UpdateUI();
        }



        // ~~~ ћенюшные команды

        private void ButtonDebug_Click(object sender, EventArgs e) {
            debug_mode = !debug_mode;
            UpdateUI();
        }

        private void CopyButton_Click(object sender, EventArgs e) {
            string text = inputRichTextBox.SelectedText;
            if (text.Length > 0) Clipboard.SetText(text);
            else MessageBox.Show("—начала выделите текст");
        }

        private void PasteButton_Click(object sender, EventArgs e) {
            InputRichTextBox_KeyDown(inputRichTextBox, new KeyEventArgs(Keys.Control | Keys.V));
        }

        private void AboutMenuItem_Click(object sender, EventArgs e) {
            AboutForm form = new();
            form.ShowDialog(this);
        }
    }
}
