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
            inputRichTextBox = new RichTextBox();
            outputLabel = new Label();
            numSysTrackBar = new TrackBar();
            numSysNumericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numSysTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSysNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // inputRichTextBox
            // 
            inputRichTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            inputRichTextBox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            inputRichTextBox.ForeColor = Color.RoyalBlue;
            inputRichTextBox.Location = new Point(12, 12);
            inputRichTextBox.Name = "inputRichTextBox";
            inputRichTextBox.Size = new Size(984, 236);
            inputRichTextBox.TabIndex = 0;
            inputRichTextBox.Text = "";
            inputRichTextBox.TextChanged += InputRichTextBox_TextChanged;
            inputRichTextBox.KeyDown += InputRichTextBox_KeyDown;
            inputRichTextBox.KeyPress += InputRichTextBox_KeyPress;
            // 
            // outputLabel
            // 
            outputLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            outputLabel.AutoSize = true;
            outputLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            outputLabel.Location = new Point(12, 302);
            outputLabel.Name = "outputLabel";
            outputLabel.Size = new Size(21, 100);
            outputLabel.TabIndex = 3;
            outputLabel.Text = "?\r\n?\r\n?\r\n?";
            // 
            // numSysTrackBar
            // 
            numSysTrackBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            numSysTrackBar.LargeChange = 1;
            numSysTrackBar.Location = new Point(12, 254);
            numSysTrackBar.Maximum = 16;
            numSysTrackBar.Minimum = 2;
            numSysTrackBar.Name = "numSysTrackBar";
            numSysTrackBar.Size = new Size(931, 45);
            numSysTrackBar.TabIndex = 1;
            numSysTrackBar.Value = 10;
            numSysTrackBar.ValueChanged += NumSysTrackBar_ValueChanged;
            // 
            // numSysNumericUpDown
            // 
            numSysNumericUpDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            numSysNumericUpDown.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            numSysNumericUpDown.ForeColor = Color.LimeGreen;
            numSysNumericUpDown.Location = new Point(949, 254);
            numSysNumericUpDown.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            numSysNumericUpDown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numSysNumericUpDown.Name = "numSysNumericUpDown";
            numSysNumericUpDown.Size = new Size(47, 33);
            numSysNumericUpDown.TabIndex = 2;
            numSysNumericUpDown.TextAlign = HorizontalAlignment.Center;
            numSysNumericUpDown.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numSysNumericUpDown.ValueChanged += NumSysNumericUpDown_ValueChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 411);
            Controls.Add(numSysNumericUpDown);
            Controls.Add(numSysTrackBar);
            Controls.Add(outputLabel);
            Controls.Add(inputRichTextBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Калькулятор";
            ((System.ComponentModel.ISupportInitialize)numSysTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSysNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox inputRichTextBox;
        private Label outputLabel;
        private TrackBar numSysTrackBar;
        private NumericUpDown numSysNumericUpDown;
    }
}
