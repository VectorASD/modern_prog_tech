namespace Calculator {
    partial class HistoryForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryForm));
            richTextBox = new RichTextBox();
            Cleaner = new Button();
            fileSizeLabel = new Label();
            SuspendLayout();
            // 
            // richTextBox
            // 
            richTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox.BackColor = Color.White;
            richTextBox.Location = new Point(12, 12);
            richTextBox.Name = "richTextBox";
            richTextBox.Size = new Size(903, 449);
            richTextBox.TabIndex = 0;
            richTextBox.Text = "";
            // 
            // Cleaner
            // 
            Cleaner.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Cleaner.BackColor = Color.AntiqueWhite;
            Cleaner.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Cleaner.ForeColor = Color.Red;
            Cleaner.Location = new Point(921, 12);
            Cleaner.Name = "Cleaner";
            Cleaner.Size = new Size(75, 75);
            Cleaner.TabIndex = 1;
            Cleaner.Text = "Стереть файл с историей";
            Cleaner.UseVisualStyleBackColor = false;
            Cleaner.Click += Cleaner_Click;
            // 
            // fileSizeLabel
            // 
            fileSizeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            fileSizeLabel.AutoSize = true;
            fileSizeLabel.Location = new Point(921, 90);
            fileSizeLabel.Name = "fileSizeLabel";
            fileSizeLabel.Size = new Size(12, 15);
            fileSizeLabel.TabIndex = 2;
            fileSizeLabel.Text = "?";
            // 
            // HistoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(1008, 473);
            Controls.Add(fileSizeLabel);
            Controls.Add(Cleaner);
            Controls.Add(richTextBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HistoryForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "История";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox;
        private Button Cleaner;
        private Label fileSizeLabel;
    }
}