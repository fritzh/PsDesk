namespace PsDeskController
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AllUpButton = new System.Windows.Forms.Button();
            this.AllDownButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.UpButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.NextChangeLabel = new System.Windows.Forms.Label();
            this.upDownEnabled = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.downTimeSpinner = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.upTimeSpinner = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.CalibrateButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.downTimeSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upTimeSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // AllUpButton
            // 
            this.AllUpButton.Location = new System.Drawing.Point(12, 25);
            this.AllUpButton.Name = "AllUpButton";
            this.AllUpButton.Size = new System.Drawing.Size(118, 41);
            this.AllUpButton.TabIndex = 0;
            this.AllUpButton.Text = "All the way up";
            this.AllUpButton.UseVisualStyleBackColor = true;
            this.AllUpButton.Click += new System.EventHandler(this.AllUpButton_Click);
            // 
            // AllDownButton
            // 
            this.AllDownButton.Location = new System.Drawing.Point(12, 177);
            this.AllDownButton.Name = "AllDownButton";
            this.AllDownButton.Size = new System.Drawing.Size(118, 41);
            this.AllDownButton.TabIndex = 1;
            this.AllDownButton.Text = "All the way down";
            this.AllDownButton.UseVisualStyleBackColor = true;
            this.AllDownButton.Click += new System.EventHandler(this.AllDownButton_Click);
            // 
            // DownButton
            // 
            this.DownButton.Location = new System.Drawing.Point(12, 148);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(118, 23);
            this.DownButton.TabIndex = 2;
            this.DownButton.Text = "Down";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DownButton_MouseDown);
            this.DownButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DownButton_MouseUp);
            // 
            // UpButton
            // 
            this.UpButton.Location = new System.Drawing.Point(12, 72);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(118, 23);
            this.UpButton.TabIndex = 3;
            this.UpButton.Text = "Up";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UpButton_MouseDown);
            this.UpButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UpButton_MouseUp);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(12, 101);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(118, 41);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "STOP!";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(12, 9);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(16, 13);
            this.StatusLabel.TabIndex = 5;
            this.StatusLabel.Text = "...";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.NextChangeLabel);
            this.groupBox1.Controls.Add(this.upDownEnabled);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.downTimeSpinner);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.upTimeSpinner);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(136, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 123);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Automatic up-down";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Next change:";
            // 
            // NextChangeLabel
            // 
            this.NextChangeLabel.Location = new System.Drawing.Point(87, 92);
            this.NextChangeLabel.Name = "NextChangeLabel";
            this.NextChangeLabel.Size = new System.Drawing.Size(87, 23);
            this.NextChangeLabel.TabIndex = 7;
            this.NextChangeLabel.Text = "(Disabled)";
            // 
            // upDownEnabled
            // 
            this.upDownEnabled.AutoSize = true;
            this.upDownEnabled.Location = new System.Drawing.Point(64, 72);
            this.upDownEnabled.Name = "upDownEnabled";
            this.upDownEnabled.Size = new System.Drawing.Size(92, 17);
            this.upDownEnabled.TabIndex = 6;
            this.upDownEnabled.Text = "Auto up-down";
            this.upDownEnabled.UseVisualStyleBackColor = true;
            this.upDownEnabled.CheckedChanged += new System.EventHandler(this.upDownEnabled_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "minutes.";
            // 
            // downTimeSpinner
            // 
            this.downTimeSpinner.Location = new System.Drawing.Point(64, 45);
            this.downTimeSpinner.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.downTimeSpinner.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.downTimeSpinner.Name = "downTimeSpinner";
            this.downTimeSpinner.Size = new System.Drawing.Size(51, 20);
            this.downTimeSpinner.TabIndex = 4;
            this.downTimeSpinner.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "down for";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "minutes,";
            // 
            // upTimeSpinner
            // 
            this.upTimeSpinner.Location = new System.Drawing.Point(64, 19);
            this.upTimeSpinner.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.upTimeSpinner.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upTimeSpinner.Name = "upTimeSpinner";
            this.upTimeSpinner.Size = new System.Drawing.Size(51, 20);
            this.upTimeSpinner.TabIndex = 1;
            this.upTimeSpinner.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Up for";
            // 
            // CalibrateButton
            // 
            this.CalibrateButton.Location = new System.Drawing.Point(235, 195);
            this.CalibrateButton.Name = "CalibrateButton";
            this.CalibrateButton.Size = new System.Drawing.Size(75, 23);
            this.CalibrateButton.TabIndex = 7;
            this.CalibrateButton.Text = "Calibrate";
            this.CalibrateButton.UseVisualStyleBackColor = true;
            this.CalibrateButton.Click += new System.EventHandler(this.CalibrateButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 230);
            this.Controls.Add(this.CalibrateButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.UpButton);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.AllDownButton);
            this.Controls.Add(this.AllUpButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "PsDesk";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.downTimeSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upTimeSpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AllUpButton;
        private System.Windows.Forms.Button AllDownButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox upDownEnabled;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown downTimeSpinner;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown upTimeSpinner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label NextChangeLabel;
        private System.Windows.Forms.Button CalibrateButton;
    }
}

