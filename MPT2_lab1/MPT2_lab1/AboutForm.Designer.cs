namespace MPT2_lab1 {
    partial class AboutForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            Author = new Label();
            Group = new Label();
            Brigade = new Label();
            Teacher = new Label();
            Version = new Label();
            pictureBox1 = new PictureBox();
            Info = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Author
            // 
            Author.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Author.AutoSize = true;
            Author.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Author.ForeColor = Color.Blue;
            Author.Location = new Point(12, 39);
            Author.Name = "Author";
            Author.Size = new Size(213, 30);
            Author.TabIndex = 0;
            Author.Text = "Автор: Прилепа М.К.";
            // 
            // Group
            // 
            Group.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Group.AutoSize = true;
            Group.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Group.ForeColor = Color.Blue;
            Group.Location = new Point(12, 69);
            Group.Name = "Group";
            Group.Size = new Size(163, 30);
            Group.TabIndex = 1;
            Group.Text = "Группа: ИП-111";
            // 
            // Brigade
            // 
            Brigade.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Brigade.AutoSize = true;
            Brigade.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Brigade.ForeColor = Color.Blue;
            Brigade.Location = new Point(12, 99);
            Brigade.Name = "Brigade";
            Brigade.Size = new Size(145, 30);
            Brigade.TabIndex = 2;
            Brigade.Text = "Бригада: своя";
            // 
            // Teacher
            // 
            Teacher.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Teacher.AutoSize = true;
            Teacher.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Teacher.ForeColor = Color.Blue;
            Teacher.Location = new Point(12, 9);
            Teacher.Name = "Teacher";
            Teacher.Size = new Size(301, 30);
            Teacher.TabIndex = 3;
            Teacher.Text = "Проверил: Мирошников Д.Ю.";
            // 
            // Version
            // 
            Version.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Version.AutoSize = true;
            Version.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Version.ForeColor = Color.Blue;
            Version.Location = new Point(12, 129);
            Version.Name = "Version";
            Version.Size = new Size(119, 30);
            Version.TabIndex = 4;
            Version.Text = "Версия: 1.0";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Image = Properties.Resources.Logo;
            pictureBox1.Location = new Point(316, 293);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(256, 256);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // Info
            // 
            Info.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Info.AutoSize = true;
            Info.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Info.ForeColor = Color.Blue;
            Info.Location = new Point(12, 173);
            Info.Name = "Info";
            Info.Size = new Size(420, 100);
            Info.TabIndex = 6;
            Info.Text = "Конвертер абсолютно любого числа любой\r\nточности любой системы счисления от 2 до 16\r\nBS - Удаление последнего символа из p1\r\nCE - Очистка p1";
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Harpy;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(584, 561);
            Controls.Add(Info);
            Controls.Add(pictureBox1);
            Controls.Add(Version);
            Controls.Add(Teacher);
            Controls.Add(Brigade);
            Controls.Add(Group);
            Controls.Add(Author);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AboutForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Справка";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Author;
        private Label Group;
        private Label Brigade;
        private Label Teacher;
        private Label Version;
        private PictureBox pictureBox1;
        private Label Info;
    }
}