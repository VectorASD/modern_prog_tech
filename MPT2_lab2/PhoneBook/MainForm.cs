using ConsoleApp;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using static ConsoleApp.UAbonentList;
using static System.Net.Mime.MediaTypeNames;

namespace PhoneBook
{
    public partial class MainForm : Form {
        private readonly UAbonentList contacts = new();
        private Record? find_mode;

        public MainForm() {
            InitializeComponent();

            try { contacts.Load(); } catch (UnpackingError err) {
                MessageBox.Show(err.InnerException?.Message, err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // if (contacts.Empty) contacts.AddRecord("cat", "meow");
        }

        private void MainForm_Load(object sender, EventArgs e) {
            UpdateUI();
        }



        private void UpdateUI() {
            noRecordsWarn.Visible = contacts.Empty;
            fileSize.Text = $"Размер файла:\n{contacts.FileSize} b.";

            contactsListBox.Items.Clear();
            foreach (var name in contacts.Keys)
                foreach (var record in contacts[name])
                    if (find_mode is null || find_mode.Search(record))
                        contactsListBox.Items.Add(record);
        }



        // Правый набор кнопок

        private void ClearButton_Click(object sender, EventArgs e) {
            for (int n = 1; n <= 3; n++) {
                var result = MessageBox.Show("Точно собираете удалить всё?", $"Подтверждение {n}/3", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes) return;
            }
            contacts.Clear();
            UpdateUI();
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            MessageBox.Show("Уже сохранено", "...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeleteButton_Click(object sender, EventArgs e) {
            Record? record = (Record?) contactsListBox.SelectedItem;
            if (record is null) {
                MessageBox.Show("Запись не выбрана", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //MessageBox.Show("Selected: " + record, "Проверка события", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            contacts.RemoveRecord(record);
            UpdateUI();
        }

        private void EditButton_Click(object sender, EventArgs e) {
            Record? record = (Record?) contactsListBox.SelectedItem;
            if (record is null) {
                MessageBox.Show("Запись не выбрана", "Ошибка изменения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            EditForm editForm = new(record.Name, record.Phone);
            editForm.ShowDialog(this);

            if (editForm.ReadyRecord is Record edited) {
                contacts.RemoveRecord(record);
                contacts.AddRecord(edited);
                UpdateUI();
                contactsListBox.SelectedItem = edited;
            }
        }



        // Право-нижний набор кнопок

        public static string GetNormalisedText(TextBox textBox) {
            var textInfo = new CultureInfo("ru-RU").TextInfo;

            var text = textBox.Text;
            var leftTrimmed = text.Length - text.TrimStart().Length;
            var newText = textInfo.ToTitleCase(text.TrimStart());

            int start = textBox.SelectionStart;
            textBox.Text = newText; // сбрасывает SelectionStart
            textBox.SelectionStart = Math.Max(0, start - leftTrimmed);

            return textBox.Text.TrimEnd();
        }
        public static Record? Validator(TextBox FIO_TextBox, TextBox phone_TextBox, string action = "добавления") {
            string name = GetNormalisedText(FIO_TextBox);
            string phone = GetNormalisedText(phone_TextBox);
            List<string> voids = [];
            if (name.Length == 0) voids.Add("'ФИО'");
            if (phone.Length == 0) voids.Add("'номер'");
            if (voids.Count > 0) {
                MessageBox.Show("В поле " + string.Join(" и ", voids) + " пусто", $"Ошибка {action}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return new(name, phone);
        }

        private void TextBox_TextChanged(object sender, EventArgs e) {
            if (find_mode is not null) {
                string name = GetNormalisedText(FIO_TextBox);
                string phone = GetNormalisedText(phone_TextBox);
                find_mode = new Record(name, phone);
                UpdateUI();
                return;
            }

            if (sender is TextBox textBox) GetNormalisedText(textBox);
        }

        private void AddButton_Click(object sender, EventArgs e) {
            Record? record = Validator(FIO_TextBox, phone_TextBox);
            if (record is not null) {
                contacts.AddRecord(record);
                UpdateUI();
            }
        }

        private void FindButton_Click(object sender, EventArgs e) {
            if (find_mode is null) {
                string name = GetNormalisedText(FIO_TextBox);
                string phone = GetNormalisedText(phone_TextBox);
                find_mode = new Record(name, phone);
            } else find_mode = null;
            findButton.Text = find_mode is null ? "Поиск" : "Сбросить поиск";
            UpdateUI();
        }



        // Ещё кнопочка, для галочки

        private void CreateButton_Click(object sender, EventArgs e) {
            Record record = new("Undefined", "Undefined");
            contacts.AddRecord(record);
            UpdateUI();
            contactsListBox.SelectedItem = record;
        }
    }
}
