using System;
using System.Diagnostics.Metrics;
using System.Windows.Forms;

namespace NumberSystemControlLibrary {
    public partial class NumberSystemControl : UserControl {
        public NumberSystemControl() {
            InitializeComponent();
            UpdateUI();
        }

        private string assignment = "?";
        private int numSys = 10;

        private void UpdateUI() {
            numSysTrackBar.Value = numSys;
            numSysUpDown.Value = numSys;
            numSysLabel.Text = "Основание с сч. " + assignment + ": " + numSys;
        }



        public string Assignment {
            get => assignment;
            set { assignment = value; UpdateUI(); }
        }

        public delegate void NumSysChangedEvnetHandler(int numSys);
        public event NumSysChangedEvnetHandler? NumSysChanged;
        public int NumSys {
            get => numSys;
            set { numSys = value; UpdateUI(); NumSysChanged?.Invoke(value); }
        }

        public BigDecimal Value {
            get => BigDecimal.Parse(numberBox.Text, numSys);
            set { numberBox.Text = value.ToString(); }
        }



        private bool CheckLetter(TextBox box, char letter) {
            if (letter == '\b') return false; // не блокируем backspace

            if (letter == '.') {
                if (box.Text.IndexOf('.') > -1) {
                    return true; // удаляем вторую точку
                }
            } else if ("0123456789abcdefABCDEF".Contains(letter)) {
                int num = BigDecimal.ParseChar(letter);
                if (num < 0 || num >= NumSys) return true; // не даём вводить цифры вне текущей системы счисления
            } else
                return true; // удаляем посторонние символы
            return false;
        }

        public void AddLetter(char letter) {
            if (CheckLetter(numberBox, letter)) return;
            numberBox.Text += letter;
        }
        public void BackSpace() {
            numberBox.Text = numberBox.Text[..^1];
        }
        public void Clear() {
            numberBox.Text = "0";
        }



        private void NumberBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (sender is not TextBox box) return;
            if ((ModifierKeys & Keys.Control) == Keys.Control) return; // Не игнорируем Ctrl+C, Ctrl+V и т.д.

            e.Handled = CheckLetter(box, e.KeyChar);
        }

        private void NumberBox_TextChanged(object sender, EventArgs e) {
            if (sender is not TextBox box) return;

            string text = box.Text;
            int position = box.SelectionStart;
            if (text.Length > 1 && text.StartsWith('0') && text[1] != '.') {
                box.Text = text.Substring(1); // box.Text setter всегда сбрасывает box.SelectionStart в 0
                box.SelectionStart = Math.Max(0, position - 1);
            } else if (text.Length == 0) {
                box.Text = "0";
                box.SelectionStart = 1;
            }
        }

        // ValueChanged НЕ попадут в рекурсию по тому, что событие НЕ вызывается при изменение на одно и тоже самое

        private void NumSysUpDown_ValueChanged(object sender, EventArgs e) {
            if (sender is not NumericUpDown upDown) return;

            // MessageBox.Show("Значение UpDown: " + upDown.Value, "Проверка события", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            NumSys = Convert.ToInt32(Math.Round(upDown.Value));
        }

        private void NumSysTrackBar_ValueChanged(object sender, EventArgs e) {
            if (sender is not TrackBar trackBar) return;

            // MessageBox.Show("Значение TrackBar: " + trackBar.Value, "Проверка события", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            NumSys = trackBar.Value;
        }
    }
}
