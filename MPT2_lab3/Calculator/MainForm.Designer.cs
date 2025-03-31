using System.Windows.Forms;

namespace Calculator
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
            inputRichTextBox = new MyRichTextBox();
            outputLabel = new Label();
            numSysTrackBar = new TrackBar();
            numSysNumericUpDown = new NumericUpDown();
            keyLabel = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            keyboard = new TableLayoutPanel();
            shiftPanel = new Panel();
            radioButton_shift = new RadioButton();
            button_shift = new Button();
            button_space = new Button();
            button_I = new Button();
            button_delete = new Button();
            button_D0 = new Button();
            button_sign = new Button();
            button_point = new Button();
            button_D1 = new Button();
            button_D2 = new Button();
            button_D3 = new Button();
            button_D4 = new Button();
            button_D5 = new Button();
            button_D6 = new Button();
            button_D7 = new Button();
            button_D8 = new Button();
            button_D9 = new Button();
            button_DA = new Button();
            button_DB = new Button();
            button_DC = new Button();
            button_DD = new Button();
            button_DE = new Button();
            button_DF = new Button();
            button_subtract = new Button();
            button_multiply = new Button();
            button_divide = new Button();
            button_add = new Button();
            button_sqr = new Button();
            button_inv = new Button();
            button_back = new Button();
            edgePanel = new Panel();
            menuStrip1 = new MenuStrip();
            editToolStripMenuItem = new ToolStripMenuItem();
            customizeToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            splitter = new MySplitContainer();
            ((System.ComponentModel.ISupportInitialize)numSysTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSysNumericUpDown).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            keyboard.SuspendLayout();
            shiftPanel.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitter).BeginInit();
            splitter.Panel1.SuspendLayout();
            splitter.Panel2.SuspendLayout();
            splitter.SuspendLayout();
            SuspendLayout();
            // 
            // inputRichTextBox
            // 
            inputRichTextBox.Dock = DockStyle.Fill;
            inputRichTextBox.Font = new Font("Courier New", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            inputRichTextBox.ForeColor = Color.RoyalBlue;
            inputRichTextBox.Location = new Point(0, 0);
            inputRichTextBox.Name = "inputRichTextBox";
            inputRichTextBox.Size = new Size(460, 119);
            inputRichTextBox.TabIndex = 0;
            inputRichTextBox.Text = "";
            inputRichTextBox.SelectionChanged += InputRichTextBox_SelectionChanged;
            inputRichTextBox.TextChanged += InputRichTextBox_TextChanged;
            inputRichTextBox.KeyDown += InputRichTextBox_KeyDown;
            inputRichTextBox.KeyPress += InputRichTextBox_KeyPress;
            // 
            // outputLabel
            // 
            outputLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            outputLabel.AutoSize = true;
            outputLabel.BackColor = Color.Transparent;
            outputLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            outputLabel.Location = new Point(12, 419);
            outputLabel.Name = "outputLabel";
            outputLabel.Size = new Size(21, 100);
            outputLabel.TabIndex = 3;
            outputLabel.Text = "?\r\n?\r\n?\r\n?";
            // 
            // numSysTrackBar
            // 
            numSysTrackBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            numSysTrackBar.LargeChange = 1;
            numSysTrackBar.Location = new Point(12, 371);
            numSysTrackBar.Maximum = 16;
            numSysTrackBar.Minimum = 2;
            numSysTrackBar.Name = "numSysTrackBar";
            numSysTrackBar.Size = new Size(410, 45);
            numSysTrackBar.TabIndex = 1;
            numSysTrackBar.Value = 10;
            numSysTrackBar.ValueChanged += NumSysTrackBar_ValueChanged;
            // 
            // numSysNumericUpDown
            // 
            numSysNumericUpDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            numSysNumericUpDown.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            numSysNumericUpDown.ForeColor = Color.LimeGreen;
            numSysNumericUpDown.Location = new Point(428, 371);
            numSysNumericUpDown.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            numSysNumericUpDown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numSysNumericUpDown.Name = "numSysNumericUpDown";
            numSysNumericUpDown.Size = new Size(47, 33);
            numSysNumericUpDown.TabIndex = 2;
            numSysNumericUpDown.TextAlign = HorizontalAlignment.Center;
            numSysNumericUpDown.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numSysNumericUpDown.ValueChanged += NumSysNumericUpDown_ValueChanged;
            // 
            // keyLabel
            // 
            keyLabel.AutoSize = true;
            keyLabel.Dock = DockStyle.Right;
            keyLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            keyLabel.Location = new Point(439, 0);
            keyLabel.Name = "keyLabel";
            keyLabel.RightToLeft = RightToLeft.No;
            keyLabel.Size = new Size(21, 50);
            keyLabel.TabIndex = 4;
            keyLabel.Text = "\r\n?";
            keyLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.BackColor = SystemColors.Control;
            flowLayoutPanel1.Controls.Add(keyLabel);
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(12, 419);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(463, 100);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // keyboard
            // 
            keyboard.ColumnCount = 6;
            keyboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            keyboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            keyboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            keyboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            keyboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            keyboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            keyboard.Controls.Add(shiftPanel, 5, 0);
            keyboard.Controls.Add(button_space, 4, 0);
            keyboard.Controls.Add(button_I, 3, 0);
            keyboard.Controls.Add(button_delete, 2, 0);
            keyboard.Controls.Add(button_D0, 1, 4);
            keyboard.Controls.Add(button_sign, 2, 4);
            keyboard.Controls.Add(button_point, 3, 4);
            keyboard.Controls.Add(button_D1, 1, 3);
            keyboard.Controls.Add(button_D2, 2, 3);
            keyboard.Controls.Add(button_D3, 3, 3);
            keyboard.Controls.Add(button_D4, 1, 2);
            keyboard.Controls.Add(button_D5, 2, 2);
            keyboard.Controls.Add(button_D6, 3, 2);
            keyboard.Controls.Add(button_D7, 1, 1);
            keyboard.Controls.Add(button_D8, 2, 1);
            keyboard.Controls.Add(button_D9, 3, 1);
            keyboard.Controls.Add(button_DA, 0, 5);
            keyboard.Controls.Add(button_DB, 1, 5);
            keyboard.Controls.Add(button_DC, 2, 5);
            keyboard.Controls.Add(button_DD, 3, 5);
            keyboard.Controls.Add(button_DE, 4, 5);
            keyboard.Controls.Add(button_DF, 5, 5);
            keyboard.Controls.Add(button_subtract, 4, 3);
            keyboard.Controls.Add(button_multiply, 4, 2);
            keyboard.Controls.Add(button_divide, 4, 1);
            keyboard.Controls.Add(button_add, 4, 4);
            keyboard.Controls.Add(button_sqr, 5, 1);
            keyboard.Controls.Add(button_inv, 5, 2);
            keyboard.Controls.Add(button_back, 1, 0);
            keyboard.Controls.Add(edgePanel, 0, 0);
            keyboard.Dock = DockStyle.Fill;
            keyboard.Location = new Point(0, 0);
            keyboard.Name = "keyboard";
            keyboard.RowCount = 6;
            keyboard.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            keyboard.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            keyboard.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            keyboard.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            keyboard.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            keyboard.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            keyboard.Size = new Size(460, 211);
            keyboard.TabIndex = 6;
            // 
            // shiftPanel
            // 
            shiftPanel.BorderStyle = BorderStyle.Fixed3D;
            shiftPanel.Controls.Add(radioButton_shift);
            shiftPanel.Controls.Add(button_shift);
            shiftPanel.Dock = DockStyle.Fill;
            shiftPanel.Location = new Point(381, 1);
            shiftPanel.Margin = new Padding(1);
            shiftPanel.Name = "shiftPanel";
            shiftPanel.Size = new Size(78, 33);
            shiftPanel.TabIndex = 1;
            // 
            // radioButton_shift
            // 
            radioButton_shift.AutoSize = true;
            radioButton_shift.BackColor = Color.DarkGreen;
            radioButton_shift.ForeColor = Color.Cornsilk;
            radioButton_shift.Location = new Point(0, 0);
            radioButton_shift.Name = "radioButton_shift";
            radioButton_shift.Size = new Size(14, 13);
            radioButton_shift.TabIndex = 29;
            radioButton_shift.UseVisualStyleBackColor = false;
            radioButton_shift.Click += RadioButtonShift_Click;
            // 
            // button_shift
            // 
            button_shift.Dock = DockStyle.Fill;
            button_shift.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_shift.ForeColor = Color.Blue;
            button_shift.Location = new Point(0, 0);
            button_shift.Margin = new Padding(1);
            button_shift.Name = "button_shift";
            button_shift.Size = new Size(74, 29);
            button_shift.TabIndex = 30;
            button_shift.Tag = Keys.D7;
            button_shift.Text = "shift";
            button_shift.UseVisualStyleBackColor = true;
            button_shift.Click += ButtonShift_Click;
            // 
            // button_space
            // 
            button_space.BackColor = Color.PaleTurquoise;
            button_space.Dock = DockStyle.Fill;
            button_space.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_space.ForeColor = Color.Black;
            button_space.Location = new Point(305, 1);
            button_space.Margin = new Padding(1);
            button_space.Name = "button_space";
            button_space.Size = new Size(74, 33);
            button_space.TabIndex = 28;
            button_space.Tag = Keys.Space;
            button_space.Text = "_";
            button_space.UseVisualStyleBackColor = false;
            button_space.Click += ButtonDigit_Click;
            // 
            // button_I
            // 
            button_I.Dock = DockStyle.Fill;
            button_I.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_I.ForeColor = Color.Red;
            button_I.Location = new Point(229, 1);
            button_I.Margin = new Padding(1);
            button_I.Name = "button_I";
            button_I.Size = new Size(74, 33);
            button_I.TabIndex = 27;
            button_I.Tag = Keys.I;
            button_I.Text = "i";
            button_I.UseVisualStyleBackColor = true;
            button_I.Click += ButtonDigit_Click;
            // 
            // button_delete
            // 
            button_delete.Dock = DockStyle.Fill;
            button_delete.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_delete.ForeColor = Color.Red;
            button_delete.Location = new Point(153, 1);
            button_delete.Margin = new Padding(1);
            button_delete.Name = "button_delete";
            button_delete.Size = new Size(74, 33);
            button_delete.TabIndex = 26;
            button_delete.Tag = Keys.Delete;
            button_delete.Text = "Delete";
            button_delete.UseVisualStyleBackColor = true;
            button_delete.Click += ButtonDigit_Click;
            // 
            // button_D0
            // 
            button_D0.Dock = DockStyle.Fill;
            button_D0.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_D0.ForeColor = Color.Blue;
            button_D0.Location = new Point(77, 141);
            button_D0.Margin = new Padding(1);
            button_D0.Name = "button_D0";
            button_D0.Size = new Size(74, 33);
            button_D0.TabIndex = 1;
            button_D0.Tag = Keys.D0;
            button_D0.Text = "0";
            button_D0.UseVisualStyleBackColor = true;
            button_D0.Click += ButtonDigit_Click;
            // 
            // button_sign
            // 
            button_sign.Dock = DockStyle.Fill;
            button_sign.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_sign.ForeColor = Color.Blue;
            button_sign.Location = new Point(153, 141);
            button_sign.Margin = new Padding(1);
            button_sign.Name = "button_sign";
            button_sign.Size = new Size(74, 33);
            button_sign.TabIndex = 2;
            button_sign.Tag = Keys.Subtract;
            button_sign.Text = "+/-";
            button_sign.UseVisualStyleBackColor = true;
            button_sign.Click += ButtonDigit_Click;
            // 
            // button_point
            // 
            button_point.Dock = DockStyle.Fill;
            button_point.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_point.ForeColor = Color.Blue;
            button_point.Location = new Point(229, 141);
            button_point.Margin = new Padding(1);
            button_point.Name = "button_point";
            button_point.Size = new Size(74, 33);
            button_point.TabIndex = 3;
            button_point.Tag = Keys.Decimal;
            button_point.Text = ",";
            button_point.UseVisualStyleBackColor = true;
            button_point.Click += ButtonDigit_Click;
            // 
            // button_D1
            // 
            button_D1.Dock = DockStyle.Fill;
            button_D1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_D1.ForeColor = Color.Blue;
            button_D1.Location = new Point(77, 106);
            button_D1.Margin = new Padding(1);
            button_D1.Name = "button_D1";
            button_D1.Size = new Size(74, 33);
            button_D1.TabIndex = 4;
            button_D1.Tag = Keys.D1;
            button_D1.Text = "1";
            button_D1.UseVisualStyleBackColor = true;
            button_D1.Click += ButtonDigit_Click;
            // 
            // button_D2
            // 
            button_D2.Dock = DockStyle.Fill;
            button_D2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_D2.ForeColor = Color.Blue;
            button_D2.Location = new Point(153, 106);
            button_D2.Margin = new Padding(1);
            button_D2.Name = "button_D2";
            button_D2.Size = new Size(74, 33);
            button_D2.TabIndex = 5;
            button_D2.Tag = Keys.D2;
            button_D2.Text = "2";
            button_D2.UseVisualStyleBackColor = true;
            button_D2.Click += ButtonDigit_Click;
            // 
            // button_D3
            // 
            button_D3.Dock = DockStyle.Fill;
            button_D3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_D3.ForeColor = Color.Blue;
            button_D3.Location = new Point(229, 106);
            button_D3.Margin = new Padding(1);
            button_D3.Name = "button_D3";
            button_D3.Size = new Size(74, 33);
            button_D3.TabIndex = 6;
            button_D3.Tag = Keys.D3;
            button_D3.Text = "3";
            button_D3.UseVisualStyleBackColor = true;
            button_D3.Click += ButtonDigit_Click;
            // 
            // button_D4
            // 
            button_D4.Dock = DockStyle.Fill;
            button_D4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_D4.ForeColor = Color.Blue;
            button_D4.Location = new Point(77, 71);
            button_D4.Margin = new Padding(1);
            button_D4.Name = "button_D4";
            button_D4.Size = new Size(74, 33);
            button_D4.TabIndex = 7;
            button_D4.Tag = Keys.D4;
            button_D4.Text = "4";
            button_D4.UseVisualStyleBackColor = true;
            button_D4.Click += ButtonDigit_Click;
            // 
            // button_D5
            // 
            button_D5.Dock = DockStyle.Fill;
            button_D5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_D5.ForeColor = Color.Blue;
            button_D5.Location = new Point(153, 71);
            button_D5.Margin = new Padding(1);
            button_D5.Name = "button_D5";
            button_D5.Size = new Size(74, 33);
            button_D5.TabIndex = 8;
            button_D5.Tag = Keys.D5;
            button_D5.Text = "5";
            button_D5.UseVisualStyleBackColor = true;
            button_D5.Click += ButtonDigit_Click;
            // 
            // button_D6
            // 
            button_D6.Dock = DockStyle.Fill;
            button_D6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_D6.ForeColor = Color.Blue;
            button_D6.Location = new Point(229, 71);
            button_D6.Margin = new Padding(1);
            button_D6.Name = "button_D6";
            button_D6.Size = new Size(74, 33);
            button_D6.TabIndex = 9;
            button_D6.Tag = Keys.D6;
            button_D6.Text = "6";
            button_D6.UseVisualStyleBackColor = true;
            button_D6.Click += ButtonDigit_Click;
            // 
            // button_D7
            // 
            button_D7.Dock = DockStyle.Fill;
            button_D7.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_D7.ForeColor = Color.Blue;
            button_D7.Location = new Point(77, 36);
            button_D7.Margin = new Padding(1);
            button_D7.Name = "button_D7";
            button_D7.Size = new Size(74, 33);
            button_D7.TabIndex = 10;
            button_D7.Tag = Keys.D7;
            button_D7.Text = "7";
            button_D7.UseVisualStyleBackColor = true;
            button_D7.Click += ButtonDigit_Click;
            // 
            // button_D8
            // 
            button_D8.Dock = DockStyle.Fill;
            button_D8.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_D8.ForeColor = Color.Blue;
            button_D8.Location = new Point(153, 36);
            button_D8.Margin = new Padding(1);
            button_D8.Name = "button_D8";
            button_D8.Size = new Size(74, 33);
            button_D8.TabIndex = 11;
            button_D8.Tag = Keys.D8;
            button_D8.Text = "8";
            button_D8.UseVisualStyleBackColor = true;
            button_D8.Click += ButtonDigit_Click;
            // 
            // button_D9
            // 
            button_D9.Dock = DockStyle.Fill;
            button_D9.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_D9.ForeColor = Color.Blue;
            button_D9.Location = new Point(229, 36);
            button_D9.Margin = new Padding(1);
            button_D9.Name = "button_D9";
            button_D9.Size = new Size(74, 33);
            button_D9.TabIndex = 12;
            button_D9.Tag = Keys.D9;
            button_D9.Text = "9";
            button_D9.UseVisualStyleBackColor = true;
            button_D9.Click += ButtonDigit_Click;
            // 
            // button_DA
            // 
            button_DA.Dock = DockStyle.Fill;
            button_DA.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_DA.ForeColor = Color.Blue;
            button_DA.Location = new Point(1, 176);
            button_DA.Margin = new Padding(1);
            button_DA.Name = "button_DA";
            button_DA.Size = new Size(74, 34);
            button_DA.TabIndex = 13;
            button_DA.Tag = Keys.A;
            button_DA.Text = "A";
            button_DA.UseVisualStyleBackColor = true;
            button_DA.Click += ButtonDigit_Click;
            // 
            // button_DB
            // 
            button_DB.Dock = DockStyle.Fill;
            button_DB.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_DB.ForeColor = Color.Blue;
            button_DB.Location = new Point(77, 176);
            button_DB.Margin = new Padding(1);
            button_DB.Name = "button_DB";
            button_DB.Size = new Size(74, 34);
            button_DB.TabIndex = 14;
            button_DB.Tag = Keys.B;
            button_DB.Text = "B";
            button_DB.UseVisualStyleBackColor = true;
            button_DB.Click += ButtonDigit_Click;
            // 
            // button_DC
            // 
            button_DC.Dock = DockStyle.Fill;
            button_DC.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_DC.ForeColor = Color.Blue;
            button_DC.Location = new Point(153, 176);
            button_DC.Margin = new Padding(1);
            button_DC.Name = "button_DC";
            button_DC.Size = new Size(74, 34);
            button_DC.TabIndex = 15;
            button_DC.Tag = Keys.C;
            button_DC.Text = "C";
            button_DC.UseVisualStyleBackColor = true;
            button_DC.Click += ButtonDigit_Click;
            // 
            // button_DD
            // 
            button_DD.Dock = DockStyle.Fill;
            button_DD.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_DD.ForeColor = Color.Blue;
            button_DD.Location = new Point(229, 176);
            button_DD.Margin = new Padding(1);
            button_DD.Name = "button_DD";
            button_DD.Size = new Size(74, 34);
            button_DD.TabIndex = 16;
            button_DD.Tag = Keys.D;
            button_DD.Text = "D";
            button_DD.UseVisualStyleBackColor = true;
            button_DD.Click += ButtonDigit_Click;
            // 
            // button_DE
            // 
            button_DE.Dock = DockStyle.Fill;
            button_DE.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_DE.ForeColor = Color.Blue;
            button_DE.Location = new Point(305, 176);
            button_DE.Margin = new Padding(1);
            button_DE.Name = "button_DE";
            button_DE.Size = new Size(74, 34);
            button_DE.TabIndex = 17;
            button_DE.Tag = Keys.E;
            button_DE.Text = "E";
            button_DE.UseVisualStyleBackColor = true;
            button_DE.Click += ButtonDigit_Click;
            // 
            // button_DF
            // 
            button_DF.Dock = DockStyle.Fill;
            button_DF.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_DF.ForeColor = Color.Blue;
            button_DF.Location = new Point(381, 176);
            button_DF.Margin = new Padding(1);
            button_DF.Name = "button_DF";
            button_DF.Size = new Size(78, 34);
            button_DF.TabIndex = 18;
            button_DF.Tag = Keys.F;
            button_DF.Text = "F";
            button_DF.UseVisualStyleBackColor = true;
            button_DF.Click += ButtonDigit_Click;
            // 
            // button_subtract
            // 
            button_subtract.Dock = DockStyle.Fill;
            button_subtract.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_subtract.ForeColor = Color.Red;
            button_subtract.Location = new Point(305, 106);
            button_subtract.Margin = new Padding(1);
            button_subtract.Name = "button_subtract";
            button_subtract.Size = new Size(74, 33);
            button_subtract.TabIndex = 19;
            button_subtract.Tag = Keys.Subtract;
            button_subtract.Text = "-";
            button_subtract.UseVisualStyleBackColor = true;
            button_subtract.Click += ButtonDigit_Click;
            // 
            // button_multiply
            // 
            button_multiply.Dock = DockStyle.Fill;
            button_multiply.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_multiply.ForeColor = Color.Red;
            button_multiply.Location = new Point(305, 71);
            button_multiply.Margin = new Padding(1);
            button_multiply.Name = "button_multiply";
            button_multiply.Size = new Size(74, 33);
            button_multiply.TabIndex = 20;
            button_multiply.Tag = Keys.Multiply;
            button_multiply.Text = "*";
            button_multiply.UseVisualStyleBackColor = true;
            button_multiply.Click += ButtonDigit_Click;
            // 
            // button_divide
            // 
            button_divide.Dock = DockStyle.Fill;
            button_divide.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_divide.ForeColor = Color.Red;
            button_divide.Location = new Point(305, 36);
            button_divide.Margin = new Padding(1);
            button_divide.Name = "button_divide";
            button_divide.Size = new Size(74, 33);
            button_divide.TabIndex = 21;
            button_divide.Tag = Keys.Divide;
            button_divide.Text = "/";
            button_divide.UseVisualStyleBackColor = true;
            button_divide.Click += ButtonDigit_Click;
            // 
            // button_add
            // 
            button_add.Dock = DockStyle.Fill;
            button_add.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_add.ForeColor = Color.Red;
            button_add.Location = new Point(305, 141);
            button_add.Margin = new Padding(1);
            button_add.Name = "button_add";
            button_add.Size = new Size(74, 33);
            button_add.TabIndex = 22;
            button_add.Tag = Keys.Add;
            button_add.Text = "+";
            button_add.UseVisualStyleBackColor = true;
            button_add.Click += ButtonDigit_Click;
            // 
            // button_sqr
            // 
            button_sqr.Dock = DockStyle.Fill;
            button_sqr.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_sqr.ForeColor = Color.Blue;
            button_sqr.Location = new Point(381, 36);
            button_sqr.Margin = new Padding(1);
            button_sqr.Name = "button_sqr";
            button_sqr.Size = new Size(78, 33);
            button_sqr.TabIndex = 23;
            button_sqr.Tag = Keys.S;
            button_sqr.Text = "Sqr";
            button_sqr.UseVisualStyleBackColor = true;
            button_sqr.Click += ButtonDigit_Click;
            // 
            // button_inv
            // 
            button_inv.Dock = DockStyle.Fill;
            button_inv.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_inv.ForeColor = Color.Blue;
            button_inv.Location = new Point(381, 71);
            button_inv.Margin = new Padding(1);
            button_inv.Name = "button_inv";
            button_inv.Size = new Size(78, 33);
            button_inv.TabIndex = 24;
            button_inv.Text = "1/";
            button_inv.UseVisualStyleBackColor = true;
            button_inv.Click += ButtonInv_Click;
            // 
            // button_back
            // 
            button_back.Dock = DockStyle.Fill;
            button_back.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button_back.ForeColor = Color.Red;
            button_back.Location = new Point(77, 1);
            button_back.Margin = new Padding(1);
            button_back.Name = "button_back";
            button_back.Size = new Size(74, 33);
            button_back.TabIndex = 25;
            button_back.Tag = Keys.Back;
            button_back.Text = "Back";
            button_back.UseVisualStyleBackColor = true;
            button_back.Click += ButtonDigit_Click;
            // 
            // edgePanel
            // 
            edgePanel.BorderStyle = BorderStyle.Fixed3D;
            edgePanel.Dock = DockStyle.Fill;
            edgePanel.Location = new Point(0, 0);
            edgePanel.Margin = new Padding(0);
            edgePanel.Name = "edgePanel";
            edgePanel.Size = new Size(76, 35);
            edgePanel.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { editToolStripMenuItem, customizeToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(487, 24);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(59, 20);
            editToolStripMenuItem.Text = "Правка";
            // 
            // customizeToolStripMenuItem
            // 
            customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            customizeToolStripMenuItem.Size = new Size(78, 20);
            customizeToolStripMenuItem.Text = "Настройка";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(65, 20);
            aboutToolStripMenuItem.Text = "Справка";
            // 
            // splitter
            // 
            splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitter.BackColor = Color.YellowGreen;
            splitter.Location = new Point(12, 27);
            splitter.Name = "splitter";
            splitter.Orientation = Orientation.Horizontal;
            // 
            // splitter.Panel1
            // 
            splitter.Panel1.BackColor = SystemColors.Control;
            splitter.Panel1.Controls.Add(inputRichTextBox);
            splitter.Panel1MinSize = 50;
            // 
            // splitter.Panel2
            // 
            splitter.Panel2.BackColor = SystemColors.Control;
            splitter.Panel2.Controls.Add(keyboard);
            splitter.Panel2MinSize = 211;
            splitter.Size = new Size(460, 338);
            splitter.SplitterDistance = 119;
            splitter.SplitterWidth = 8;
            splitter.TabIndex = 8;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(487, 528);
            Controls.Add(splitter);
            Controls.Add(outputLabel);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(numSysNumericUpDown);
            Controls.Add(numSysTrackBar);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Калькулятор";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)numSysTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSysNumericUpDown).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            keyboard.ResumeLayout(false);
            shiftPanel.ResumeLayout(false);
            shiftPanel.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitter.Panel1.ResumeLayout(false);
            splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitter).EndInit();
            splitter.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MyRichTextBox inputRichTextBox;
        private Label outputLabel;
        private TrackBar numSysTrackBar;
        private NumericUpDown numSysNumericUpDown;
        private Label keyLabel;
        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel keyboard;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem customizeToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Panel edgePanel;
        private Button button_D0;
        private Button button_sign;
        private Button button_point;
        private Button button_D1;
        private Button button_D2;
        private Button button_D3;
        private Button button_D4;
        private Button button_D5;
        private Button button_D6;
        private Button button_D7;
        private Button button_D8;
        private Button button_D9;
        private Button button_DA;
        private Button button_DB;
        private Button button_DC;
        private Button button_DD;
        private Button button_DE;
        private Button button_DF;
        private Button button_subtract;
        private Button button_multiply;
        private Button button_divide;
        private Button button_add;
        private Button button_sqr;
        private Button button_inv;
        private Button button_space;
        private Button button_I;
        private Button button_delete;
        private Button button_back;
        private RadioButton radioButton_shift;
        private Button button_shift;
        private Panel shiftPanel;
        private MySplitContainer splitter;
    }
}
