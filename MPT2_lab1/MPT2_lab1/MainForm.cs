using NumberSystemControlLibrary;

namespace MPT2_lab1
{
    public partial class Converter : Form {
        private readonly Button[] inputs;
        private readonly History history = new();

        public Converter() {
            InitializeComponent();
            inputs = [
                input_0, input_1, input_2, input_3, input_4, input_5, input_6, input_7,
                input_8, input_9, input_A, input_B, input_C, input_D, input_E, input_F
            ];
        }

        private void Form1_Load(object sender, EventArgs e) {
            UpdateKeyboard(sourceNumber.NumSys);
        }



        private void ExitItem_Click(object sender, EventArgs e) {
            //MessageBox.Show("Подтведите что-то", "Проверка события", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            Close();
        }
        private void HistoryMenuItem_Click(object sender, EventArgs e) {
            HistoryForm form = new();
            form.LoadHistory(history);
            form.Show(this);
        }
        private void AboutMenuItem_Click(object sender, EventArgs e) {
            AboutForm form = new();
            form.ShowDialog(this);
        }



        private void InputDigit_Click(object sender, EventArgs e) {
            if (sender is not Button button) return;
            object? tag;
            if ((tag = button.Tag) == null) return;

            char letter = (tag.ToString() ?? "0")[0];
            // MessageBox.Show("Tag: " + letter, "Проверка события", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            sourceNumber.AddLetter(letter);
        }

        private void Input_BS_Click(object sender, EventArgs e) {
            sourceNumber.BackSpace();
        }

        private void Input_CE_Click(object sender, EventArgs e) {
            sourceNumber.Clear();
        }

        private void Input_Execute_Click(object sender, EventArgs e) {
            BigDecimal input;
            try { input = sourceNumber.Value; } catch (FormatException err) {
                MessageBox.Show(err.Message, "Ошибка считывания p1-числа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BigDecimal output = input.ToNumberSystem(destinationNumber.NumSys);
            destinationNumber.Value = output;
            history.AddRecord(input, output);
            // MessageBox.Show("Raw: " + input.Raw + "\n" + output.Raw, "Проверка события", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }



        private void UpdateKeyboard(int numSys) {
            int num = 0;
            foreach (var input in inputs)
                input.Enabled = (num++) < numSys;
        }
        private void SourceNumber_NumSysChanged(int numSys) => UpdateKeyboard(numSys);
    }
}
