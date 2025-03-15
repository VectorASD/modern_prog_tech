namespace PhoneBook
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            contactsListBox = new ListBox();
            clearButton = new Button();
            saveButton = new Button();
            deleteButton = new Button();
            editButton = new Button();
            fileSize = new Label();
            noRecordsWarn = new Label();
            createButton = new Button();
            label1 = new Label();
            FIO_TextBox = new TextBox();
            label2 = new Label();
            phone_TextBox = new TextBox();
            findButton = new Button();
            addButton = new Button();
            SuspendLayout();
            // 
            // contactsListBox
            // 
            contactsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            contactsListBox.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            contactsListBox.FormattingEnabled = true;
            contactsListBox.ItemHeight = 14;
            contactsListBox.Location = new Point(12, 40);
            contactsListBox.MinimumSize = new Size(200, 200);
            contactsListBox.Name = "contactsListBox";
            contactsListBox.Size = new Size(440, 256);
            contactsListBox.TabIndex = 0;
            // 
            // clearButton
            // 
            clearButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            clearButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            clearButton.Location = new Point(458, 40);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(119, 36);
            clearButton.TabIndex = 1;
            clearButton.Text = "Очистить";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += ClearButton_Click;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            saveButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Strikeout, GraphicsUnit.Point, 204);
            saveButton.Location = new Point(458, 82);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(119, 36);
            saveButton.TabIndex = 2;
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            deleteButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            deleteButton.Location = new Point(458, 124);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(119, 36);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "Удалить";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += DeleteButton_Click;
            // 
            // editButton
            // 
            editButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            editButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            editButton.Location = new Point(458, 166);
            editButton.Name = "editButton";
            editButton.Size = new Size(119, 36);
            editButton.TabIndex = 4;
            editButton.Text = "Изменить";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += EditButton_Click;
            // 
            // fileSize
            // 
            fileSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            fileSize.AutoSize = true;
            fileSize.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            fileSize.Location = new Point(458, 205);
            fileSize.Name = "fileSize";
            fileSize.Size = new Size(21, 25);
            fileSize.TabIndex = 5;
            fileSize.Text = "?";
            // 
            // noRecordsWarn
            // 
            noRecordsWarn.AutoSize = true;
            noRecordsWarn.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            noRecordsWarn.Location = new Point(26, 54);
            noRecordsWarn.Name = "noRecordsWarn";
            noRecordsWarn.Size = new Size(165, 25);
            noRecordsWarn.TabIndex = 6;
            noRecordsWarn.Text = "Пока нет записей";
            // 
            // createButton
            // 
            createButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            createButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            createButton.Location = new Point(12, 305);
            createButton.Name = "createButton";
            createButton.Size = new Size(85, 26);
            createButton.TabIndex = 7;
            createButton.Text = "Создать";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += CreateButton_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(12, 349);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 9;
            label1.Text = "ФИО";
            // 
            // FIO_TextBox
            // 
            FIO_TextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FIO_TextBox.Location = new Point(12, 367);
            FIO_TextBox.Name = "FIO_TextBox";
            FIO_TextBox.Size = new Size(279, 23);
            FIO_TextBox.TabIndex = 10;
            FIO_TextBox.TextChanged += TextBox_TextChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(306, 349);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 11;
            label2.Text = "Номер";
            // 
            // phone_TextBox
            // 
            phone_TextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            phone_TextBox.Location = new Point(306, 367);
            phone_TextBox.Name = "phone_TextBox";
            phone_TextBox.Size = new Size(146, 23);
            phone_TextBox.TabIndex = 12;
            phone_TextBox.TextChanged += TextBox_TextChanged;
            // 
            // findButton
            // 
            findButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            findButton.Font = new Font("Segoe UI", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            findButton.Location = new Point(458, 393);
            findButton.Name = "findButton";
            findButton.Size = new Size(154, 36);
            findButton.TabIndex = 13;
            findButton.Text = "Найти";
            findButton.UseVisualStyleBackColor = true;
            findButton.Click += FindButton_Click;
            // 
            // addButton
            // 
            addButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            addButton.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            addButton.Location = new Point(458, 349);
            addButton.Name = "addButton";
            addButton.Size = new Size(119, 36);
            addButton.TabIndex = 14;
            addButton.Text = "Добавить";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += AddButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 441);
            Controls.Add(addButton);
            Controls.Add(findButton);
            Controls.Add(phone_TextBox);
            Controls.Add(label2);
            Controls.Add(FIO_TextBox);
            Controls.Add(label1);
            Controls.Add(createButton);
            Controls.Add(noRecordsWarn);
            Controls.Add(fileSize);
            Controls.Add(editButton);
            Controls.Add(deleteButton);
            Controls.Add(saveButton);
            Controls.Add(clearButton);
            Controls.Add(contactsListBox);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(400, 400);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Телефонная книга";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox contactsListBox;
        private Button clearButton;
        private Button saveButton;
        private Button deleteButton;
        private Button editButton;
        private Label fileSize;
        private Label noRecordsWarn;
        private Button createButton;
        private Label label1;
        private TextBox FIO_TextBox;
        private Label label2;
        private TextBox phone_TextBox;
        private Button findButton;
        private Button addButton;
    }
}
