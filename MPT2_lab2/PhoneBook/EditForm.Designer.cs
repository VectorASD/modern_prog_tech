namespace PhoneBook {
    partial class EditForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            phone_TextBox = new TextBox();
            label2 = new Label();
            FIO_TextBox = new TextBox();
            label1 = new Label();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // phone_TextBox
            // 
            phone_TextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            phone_TextBox.Location = new Point(326, 30);
            phone_TextBox.Name = "phone_TextBox";
            phone_TextBox.Size = new Size(146, 23);
            phone_TextBox.TabIndex = 16;
            phone_TextBox.TextChanged += TextBox_TextChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(326, 12);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 15;
            label2.Text = "Номер";
            // 
            // FIO_TextBox
            // 
            FIO_TextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            FIO_TextBox.Location = new Point(12, 30);
            FIO_TextBox.Name = "FIO_TextBox";
            FIO_TextBox.Size = new Size(308, 23);
            FIO_TextBox.TabIndex = 14;
            FIO_TextBox.TextChanged += TextBox_TextChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 13;
            label1.Text = "ФИО";
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            okButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            okButton.Location = new Point(12, 93);
            okButton.Name = "okButton";
            okButton.Size = new Size(136, 36);
            okButton.TabIndex = 17;
            okButton.Text = "Подтвердить";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cancelButton.Location = new Point(336, 93);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(136, 36);
            cancelButton.TabIndex = 18;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += CancelButton_Click;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 141);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(phone_TextBox);
            Controls.Add(label2);
            Controls.Add(FIO_TextBox);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EditForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Редактирование записи";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox phone_TextBox;
        private Label label2;
        private TextBox FIO_TextBox;
        private Label label1;
        private Button okButton;
        private Button cancelButton;
    }
}