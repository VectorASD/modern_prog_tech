namespace NumberSystemControlLibrary {
    partial class NumberSystemControl {
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            numberBox = new TextBox();
            numSysLabel = new Label();
            panel1 = new Panel();
            numSysUpDown = new NumericUpDown();
            numSysTrackBar = new TrackBar();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSysUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSysTrackBar).BeginInit();
            SuspendLayout();
            // 
            // numberBox
            // 
            numberBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numberBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            numberBox.ForeColor = SystemColors.WindowText;
            numberBox.ImeMode = ImeMode.Off;
            numberBox.Location = new Point(8, 8);
            numberBox.MaxLength = int.MaxValue;
            numberBox.Name = "numberBox";
            numberBox.Size = new Size(352, 25);
            numberBox.TabIndex = 0;
            numberBox.Text = "0";
            numberBox.TextAlign = HorizontalAlignment.Right;
            numberBox.TextChanged += NumberBox_TextChanged;
            numberBox.KeyPress += NumberBox_KeyPress;
            // 
            // numSysLabel
            // 
            numSysLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            numSysLabel.AutoSize = true;
            numSysLabel.Location = new Point(5, 5);
            numSysLabel.Name = "numSysLabel";
            numSysLabel.Size = new Size(208, 15);
            numSysLabel.TabIndex = 1;
            numSysLabel.Text = "Основание с сч. исходного числа 10";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(numSysUpDown);
            panel1.Controls.Add(numSysLabel);
            panel1.Location = new Point(8, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 25);
            panel1.TabIndex = 3;
            // 
            // numSysUpDown
            // 
            numSysUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            numSysUpDown.Location = new Point(315, 1);
            numSysUpDown.Margin = new Padding(0);
            numSysUpDown.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            numSysUpDown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numSysUpDown.Name = "numSysUpDown";
            numSysUpDown.Size = new Size(37, 23);
            numSysUpDown.TabIndex = 2;
            numSysUpDown.TextAlign = HorizontalAlignment.Center;
            numSysUpDown.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numSysUpDown.ValueChanged += NumSysUpDown_ValueChanged;
            // 
            // numSysTrackBar
            // 
            numSysTrackBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numSysTrackBar.LargeChange = 1;
            numSysTrackBar.Location = new Point(8, 74);
            numSysTrackBar.Maximum = 16;
            numSysTrackBar.Minimum = 2;
            numSysTrackBar.Name = "numSysTrackBar";
            numSysTrackBar.Size = new Size(352, 45);
            numSysTrackBar.TabIndex = 4;
            numSysTrackBar.Value = 10;
            numSysTrackBar.ValueChanged += NumSysTrackBar_ValueChanged;
            // 
            // NumberSystemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            Controls.Add(numSysTrackBar);
            Controls.Add(panel1);
            Controls.Add(numberBox);
            Name = "NumberSystemControl";
            Size = new Size(368, 113);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSysUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSysTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox numberBox;
        private Label numSysLabel;
        private Panel panel1;
        private NumericUpDown numSysUpDown;
        private TrackBar numSysTrackBar;
    }
}
