using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ConsoleApp.UAbonentList;

namespace PhoneBook {
    public partial class EditForm : Form {
        public string StartFIO { get; }
        public string StartPhone { get; }

        public string CurrentFIO {
            get => FIO_TextBox.Text;
            set { FIO_TextBox.Text = value; }
        }
        public string CurrentPhone {
            get => phone_TextBox.Text;
            set { phone_TextBox.Text = value; }
        }

        public Record? ReadyRecord { get; private set; }

        public EditForm(string start_fio = "?", string start_phone = "?") {
            InitializeComponent();
            CurrentFIO = StartFIO = start_fio;
            CurrentPhone = StartPhone = start_phone;
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e) {
            Record? record = MainForm.Validator(FIO_TextBox, phone_TextBox, "изменения");
            if (record is null) return;

            if (record.Name == StartFIO && record.Phone == StartPhone) {
                MessageBox.Show("Ничего не изменилось", "Ошибка изменения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReadyRecord = record;
            Close();
        }

        private void TextBox_TextChanged(object sender, EventArgs e) {
            if (sender is TextBox textBox) MainForm.GetNormalisedText(textBox);
        }
    }
}
