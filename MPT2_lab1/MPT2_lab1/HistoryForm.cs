using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPT2_lab1 {
    public partial class HistoryForm : Form {
        private History? history;

        public HistoryForm() {
            InitializeComponent();
        }

        public void UpdateUI() {
            if (history is null) return;

            StringBuilder sb = new();
            if (history.Empty)
                sb.Append("В истории ничего нет.\nИспользуйте кнопку Execute для добавления новых записей");
            for (int i = 0; i < history.Count; i++) {
                if (i > 0) sb.Append("~~~~~~~~~~~~~~~~\n");
                sb.Append(history.Item(i));
            }
            richTextBox.Text = sb.ToString();

            fileSizeLabel.Text = "Размер\nфайла:\n" + history.FileSize() + " b.";
        }

        public void LoadHistory(History hist) {
            history = hist;
            UpdateUI();
        }

        private void Cleaner_Click(object sender, EventArgs e) {
            history?.Clear();
            UpdateUI();
        }

        public string HistoryText => richTextBox.Text; // для тестирования
    }
}
