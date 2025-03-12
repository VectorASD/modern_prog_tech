namespace MPT2_lab1
{
    partial class Converter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Converter));
            menuStrip1 = new MenuStrip();
            ExitMenuItem = new ToolStripMenuItem();
            HistoryMenuItem = new ToolStripMenuItem();
            AboutMenuItem = new ToolStripMenuItem();
            sourceNumber = new NumberSystemControlLibrary.NumberSystemControl();
            destinationNumber = new NumberSystemControlLibrary.NumberSystemControl();
            tableLayoutPanel1 = new TableLayoutPanel();
            input_Execute = new Button();
            input_BS = new Button();
            input_dot = new Button();
            input_F = new Button();
            input_E = new Button();
            input_D = new Button();
            input_C = new Button();
            input_B = new Button();
            input_A = new Button();
            input_9 = new Button();
            input_8 = new Button();
            input_7 = new Button();
            input_6 = new Button();
            input_5 = new Button();
            input_4 = new Button();
            input_3 = new Button();
            input_2 = new Button();
            input_1 = new Button();
            input_0 = new Button();
            input_CE = new Button();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ExitMenuItem, HistoryMenuItem, AboutMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(385, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // ExitMenuItem
            // 
            ExitMenuItem.Name = "ExitMenuItem";
            ExitMenuItem.Size = new Size(54, 20);
            ExitMenuItem.Text = "Выход";
            ExitMenuItem.Click += ExitItem_Click;
            // 
            // HistoryMenuItem
            // 
            HistoryMenuItem.Name = "HistoryMenuItem";
            HistoryMenuItem.Size = new Size(66, 20);
            HistoryMenuItem.Text = "История";
            HistoryMenuItem.Click += HistoryMenuItem_Click;
            // 
            // AboutMenuItem
            // 
            AboutMenuItem.Name = "AboutMenuItem";
            AboutMenuItem.Size = new Size(65, 20);
            AboutMenuItem.Text = "Справка";
            AboutMenuItem.Click += AboutMenuItem_Click;
            // 
            // sourceNumber
            // 
            sourceNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sourceNumber.Assignment = "исходного числа";
            sourceNumber.BackColor = SystemColors.ActiveCaption;
            sourceNumber.Location = new Point(8, 32);
            sourceNumber.Name = "sourceNumber";
            sourceNumber.NumSys = 10;
            sourceNumber.Size = new Size(369, 113);
            sourceNumber.TabIndex = 1;
            sourceNumber.NumSysChanged += SourceNumber_NumSysChanged;
            // 
            // destinationNumber
            // 
            destinationNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            destinationNumber.Assignment = "результата";
            destinationNumber.BackColor = SystemColors.ActiveCaption;
            destinationNumber.Location = new Point(8, 153);
            destinationNumber.Name = "destinationNumber";
            destinationNumber.NumSys = 16;
            destinationNumber.Size = new Size(369, 113);
            destinationNumber.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(input_Execute, 3, 4);
            tableLayoutPanel1.Controls.Add(input_BS, 1, 4);
            tableLayoutPanel1.Controls.Add(input_dot, 0, 4);
            tableLayoutPanel1.Controls.Add(input_F, 3, 3);
            tableLayoutPanel1.Controls.Add(input_E, 2, 3);
            tableLayoutPanel1.Controls.Add(input_D, 1, 3);
            tableLayoutPanel1.Controls.Add(input_C, 0, 3);
            tableLayoutPanel1.Controls.Add(input_B, 3, 2);
            tableLayoutPanel1.Controls.Add(input_A, 2, 2);
            tableLayoutPanel1.Controls.Add(input_9, 1, 2);
            tableLayoutPanel1.Controls.Add(input_8, 0, 2);
            tableLayoutPanel1.Controls.Add(input_7, 3, 1);
            tableLayoutPanel1.Controls.Add(input_6, 2, 1);
            tableLayoutPanel1.Controls.Add(input_5, 1, 1);
            tableLayoutPanel1.Controls.Add(input_4, 0, 1);
            tableLayoutPanel1.Controls.Add(input_3, 3, 0);
            tableLayoutPanel1.Controls.Add(input_2, 2, 0);
            tableLayoutPanel1.Controls.Add(input_1, 1, 0);
            tableLayoutPanel1.Controls.Add(input_0, 0, 0);
            tableLayoutPanel1.Controls.Add(input_CE, 2, 4);
            tableLayoutPanel1.ForeColor = Color.Blue;
            tableLayoutPanel1.Location = new Point(8, 274);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Size = new Size(368, 200);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // input_Execute
            // 
            input_Execute.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_Execute.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            input_Execute.ForeColor = Color.Black;
            input_Execute.Location = new Point(278, 162);
            input_Execute.Margin = new Padding(2);
            input_Execute.Name = "input_Execute";
            input_Execute.Size = new Size(88, 36);
            input_Execute.TabIndex = 19;
            input_Execute.Text = "Execute";
            input_Execute.UseVisualStyleBackColor = true;
            input_Execute.Click += Input_Execute_Click;
            // 
            // input_BS
            // 
            input_BS.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_BS.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_BS.ForeColor = Color.DarkRed;
            input_BS.Location = new Point(94, 162);
            input_BS.Margin = new Padding(2);
            input_BS.Name = "input_BS";
            input_BS.Size = new Size(88, 36);
            input_BS.TabIndex = 17;
            input_BS.Text = "BS";
            input_BS.UseVisualStyleBackColor = true;
            input_BS.Click += Input_BS_Click;
            // 
            // input_dot
            // 
            input_dot.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_dot.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_dot.ForeColor = Color.DarkRed;
            input_dot.Location = new Point(2, 162);
            input_dot.Margin = new Padding(2);
            input_dot.Name = "input_dot";
            input_dot.Size = new Size(88, 36);
            input_dot.TabIndex = 16;
            input_dot.Tag = ".";
            input_dot.Text = ".";
            input_dot.UseVisualStyleBackColor = true;
            input_dot.Click += InputDigit_Click;
            // 
            // input_F
            // 
            input_F.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_F.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_F.ForeColor = Color.DodgerBlue;
            input_F.Location = new Point(278, 122);
            input_F.Margin = new Padding(2);
            input_F.Name = "input_F";
            input_F.Size = new Size(88, 36);
            input_F.TabIndex = 15;
            input_F.Tag = "F";
            input_F.Text = "F";
            input_F.UseVisualStyleBackColor = true;
            input_F.Click += InputDigit_Click;
            // 
            // input_E
            // 
            input_E.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_E.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_E.ForeColor = Color.DodgerBlue;
            input_E.Location = new Point(186, 122);
            input_E.Margin = new Padding(2);
            input_E.Name = "input_E";
            input_E.Size = new Size(88, 36);
            input_E.TabIndex = 14;
            input_E.Tag = "E";
            input_E.Text = "E";
            input_E.UseVisualStyleBackColor = true;
            input_E.Click += InputDigit_Click;
            // 
            // input_D
            // 
            input_D.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_D.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_D.ForeColor = Color.DodgerBlue;
            input_D.Location = new Point(94, 122);
            input_D.Margin = new Padding(2);
            input_D.Name = "input_D";
            input_D.Size = new Size(88, 36);
            input_D.TabIndex = 13;
            input_D.Tag = "D";
            input_D.Text = "D";
            input_D.UseVisualStyleBackColor = true;
            input_D.Click += InputDigit_Click;
            // 
            // input_C
            // 
            input_C.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_C.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_C.ForeColor = Color.DodgerBlue;
            input_C.Location = new Point(2, 122);
            input_C.Margin = new Padding(2);
            input_C.Name = "input_C";
            input_C.Size = new Size(88, 36);
            input_C.TabIndex = 12;
            input_C.Tag = "C";
            input_C.Text = "C";
            input_C.UseVisualStyleBackColor = true;
            input_C.Click += InputDigit_Click;
            // 
            // input_B
            // 
            input_B.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_B.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_B.ForeColor = Color.DodgerBlue;
            input_B.Location = new Point(278, 82);
            input_B.Margin = new Padding(2);
            input_B.Name = "input_B";
            input_B.Size = new Size(88, 36);
            input_B.TabIndex = 11;
            input_B.Tag = "B";
            input_B.Text = "B";
            input_B.UseVisualStyleBackColor = true;
            input_B.Click += InputDigit_Click;
            // 
            // input_A
            // 
            input_A.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_A.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_A.ForeColor = Color.DodgerBlue;
            input_A.Location = new Point(186, 82);
            input_A.Margin = new Padding(2);
            input_A.Name = "input_A";
            input_A.Size = new Size(88, 36);
            input_A.TabIndex = 10;
            input_A.Tag = "A";
            input_A.Text = "A";
            input_A.UseVisualStyleBackColor = true;
            input_A.Click += InputDigit_Click;
            // 
            // input_9
            // 
            input_9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_9.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_9.ForeColor = Color.DodgerBlue;
            input_9.Location = new Point(94, 82);
            input_9.Margin = new Padding(2);
            input_9.Name = "input_9";
            input_9.Size = new Size(88, 36);
            input_9.TabIndex = 9;
            input_9.Tag = "9";
            input_9.Text = "9";
            input_9.UseVisualStyleBackColor = true;
            input_9.Click += InputDigit_Click;
            // 
            // input_8
            // 
            input_8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_8.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_8.ForeColor = Color.DodgerBlue;
            input_8.Location = new Point(2, 82);
            input_8.Margin = new Padding(2);
            input_8.Name = "input_8";
            input_8.Size = new Size(88, 36);
            input_8.TabIndex = 8;
            input_8.Tag = "8";
            input_8.Text = "8";
            input_8.UseVisualStyleBackColor = true;
            input_8.Click += InputDigit_Click;
            // 
            // input_7
            // 
            input_7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_7.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_7.ForeColor = Color.DodgerBlue;
            input_7.Location = new Point(278, 42);
            input_7.Margin = new Padding(2);
            input_7.Name = "input_7";
            input_7.Size = new Size(88, 36);
            input_7.TabIndex = 7;
            input_7.Tag = "7";
            input_7.Text = "7";
            input_7.UseVisualStyleBackColor = true;
            input_7.Click += InputDigit_Click;
            // 
            // input_6
            // 
            input_6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_6.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_6.ForeColor = Color.DodgerBlue;
            input_6.Location = new Point(186, 42);
            input_6.Margin = new Padding(2);
            input_6.Name = "input_6";
            input_6.Size = new Size(88, 36);
            input_6.TabIndex = 6;
            input_6.Tag = "6";
            input_6.Text = "6";
            input_6.UseVisualStyleBackColor = true;
            input_6.Click += InputDigit_Click;
            // 
            // input_5
            // 
            input_5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_5.ForeColor = Color.DodgerBlue;
            input_5.Location = new Point(94, 42);
            input_5.Margin = new Padding(2);
            input_5.Name = "input_5";
            input_5.Size = new Size(88, 36);
            input_5.TabIndex = 5;
            input_5.Tag = "5";
            input_5.Text = "5";
            input_5.UseVisualStyleBackColor = true;
            input_5.Click += InputDigit_Click;
            // 
            // input_4
            // 
            input_4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_4.ForeColor = Color.DodgerBlue;
            input_4.Location = new Point(2, 42);
            input_4.Margin = new Padding(2);
            input_4.Name = "input_4";
            input_4.Size = new Size(88, 36);
            input_4.TabIndex = 4;
            input_4.Tag = "4";
            input_4.Text = "4";
            input_4.UseVisualStyleBackColor = true;
            input_4.Click += InputDigit_Click;
            // 
            // input_3
            // 
            input_3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_3.ForeColor = Color.DodgerBlue;
            input_3.Location = new Point(278, 2);
            input_3.Margin = new Padding(2);
            input_3.Name = "input_3";
            input_3.Size = new Size(88, 36);
            input_3.TabIndex = 3;
            input_3.Tag = "3";
            input_3.Text = "3";
            input_3.UseVisualStyleBackColor = true;
            input_3.Click += InputDigit_Click;
            // 
            // input_2
            // 
            input_2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_2.ForeColor = Color.DodgerBlue;
            input_2.Location = new Point(186, 2);
            input_2.Margin = new Padding(2);
            input_2.Name = "input_2";
            input_2.Size = new Size(88, 36);
            input_2.TabIndex = 2;
            input_2.Tag = "2";
            input_2.Text = "2";
            input_2.UseVisualStyleBackColor = true;
            input_2.Click += InputDigit_Click;
            // 
            // input_1
            // 
            input_1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_1.ForeColor = Color.DodgerBlue;
            input_1.Location = new Point(94, 2);
            input_1.Margin = new Padding(2);
            input_1.Name = "input_1";
            input_1.Size = new Size(88, 36);
            input_1.TabIndex = 1;
            input_1.Tag = "1";
            input_1.Text = "1";
            input_1.UseVisualStyleBackColor = true;
            input_1.Click += InputDigit_Click;
            // 
            // input_0
            // 
            input_0.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_0.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_0.ForeColor = Color.DodgerBlue;
            input_0.Location = new Point(2, 2);
            input_0.Margin = new Padding(2);
            input_0.Name = "input_0";
            input_0.Size = new Size(88, 36);
            input_0.TabIndex = 0;
            input_0.Tag = "0";
            input_0.Text = "0";
            input_0.UseVisualStyleBackColor = true;
            input_0.Click += InputDigit_Click;
            // 
            // input_CE
            // 
            input_CE.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            input_CE.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            input_CE.ForeColor = Color.DarkRed;
            input_CE.Location = new Point(186, 162);
            input_CE.Margin = new Padding(2);
            input_CE.Name = "input_CE";
            input_CE.Size = new Size(88, 36);
            input_CE.TabIndex = 18;
            input_CE.Text = "CE";
            input_CE.UseVisualStyleBackColor = true;
            input_CE.Click += Input_CE_Click;
            // 
            // Converter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(385, 481);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(destinationNumber);
            Controls.Add(sourceNumber);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(300, 482);
            Name = "Converter";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Конвертер";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem ExitMenuItem;
        private ToolStripMenuItem HistoryMenuItem;
        private ToolStripMenuItem AboutMenuItem;
        private NumberSystemControlLibrary.NumberSystemControl sourceNumber;
        private NumberSystemControlLibrary.NumberSystemControl destinationNumber;
        private TableLayoutPanel tableLayoutPanel1;
        private Button input_0;
        private Button input_Execute;
        private Button input_CE;
        private Button input_BS;
        private Button input_dot;
        private Button input_F;
        private Button input_E;
        private Button input_D;
        private Button input_C;
        private Button input_B;
        private Button input_A;
        private Button input_9;
        private Button input_8;
        private Button input_7;
        private Button input_6;
        private Button input_5;
        private Button input_4;
        private Button input_3;
        private Button input_2;
        private Button input_1;
    }
}
