using ConsoleApp;

namespace Calculator
{
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            UpdateUI();
        }



        private readonly RationalEditor editor = new();

        private void InputRichTextBox_TextChanged(object sender, EventArgs e) {
            // if (sender is not RichTextBox richTextBox) return;


        }

        private void UpdateUI() {
            try {
                ANumber value = editor.Value;
                outputLabel.Text = "Type: " + value.GetType().Name + "\n" + "Raw: " + value.Raw;
            } catch (Exception err) {
                outputLabel.Text = "Error: " + err.Message;
            }
        }

        // �� ��������, ������� KeyPress, ����� KeyDown
        // e.Handled ��������� ���� ������ � KeyPress
        // ��� �� �����, �������� Text ����������� ������ ����� KeyDown
        // KeyPress �� ������������ � ����-�������� (Ctrl, Shift, Alt, Esc, NumLock, Capital...)
        // KeyPress �������� � ������������ char, � KeyDown - � ����������� ���������
        // ���� ������ ��� MessageBox.Show, �� ������� KeyPress � KeyDown ����� ���������� :/

        private void InputRichTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            // if (sender is not RichTextBox richTextBox) return;

            // MessageBox.Show("KeyPress: '" + richTextBox.Text + "', '" + e.KeyChar + "'");

            e.Handled = true;
        }

        private void InputRichTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (sender is not RichTextBox richTextBox) return;

            // MessageBox.Show("KeyDown: '" + richTextBox.Text + "', " + e.KeyValue + ", '" + e.KeyData + "', " + e.KeyCode);

            Keys modifiers = e.Modifiers;
            bool shift = ((modifiers & Keys.Shift) != 0) ^ Console.CapsLock;
            bool ctrl = (modifiers & Keys.Control) != 0;
            bool alt = (modifiers & Keys.Alt) != 0; // ������ ������������� ������ ���

            int start = richTextBox.SelectionStart;
            richTextBox.Text = editor.Handler(e.KeyCode, shift, ctrl, alt, start, out int delta);
            richTextBox.SelectionStart = Math.Max(0, start + delta);

            e.Handled = true; // ��������� ���������� �����������

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
