namespace draw_pattern
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dispersionTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.patternSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.saveButton = new System.Windows.Forms.Button();
            this.useNoiseCheckBox = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.addGradientButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.MixPwrTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dispersionTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patternSizeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MixPwrTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(558, 382);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            // 
            // dispersionTrackBar
            // 
            this.dispersionTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dispersionTrackBar.Enabled = false;
            this.dispersionTrackBar.Location = new System.Drawing.Point(328, 388);
            this.dispersionTrackBar.Maximum = 30;
            this.dispersionTrackBar.Name = "dispersionTrackBar";
            this.dispersionTrackBar.Size = new System.Drawing.Size(104, 45);
            this.dispersionTrackBar.TabIndex = 2;
            this.dispersionTrackBar.Scroll += new System.EventHandler(this.dispersionTrackBar_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(266, 393);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dispersion";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "PatternSize";
            // 
            // patternSizeTrackBar
            // 
            this.patternSizeTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.patternSizeTrackBar.Location = new System.Drawing.Point(75, 388);
            this.patternSizeTrackBar.Maximum = 200;
            this.patternSizeTrackBar.Minimum = 10;
            this.patternSizeTrackBar.Name = "patternSizeTrackBar";
            this.patternSizeTrackBar.Size = new System.Drawing.Size(104, 45);
            this.patternSizeTrackBar.TabIndex = 4;
            this.patternSizeTrackBar.Value = 50;
            this.patternSizeTrackBar.Scroll += new System.EventHandler(this.patternSizeTrackBar_Scroll);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 16;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // useNoiseCheckBox
            // 
            this.useNoiseCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.useNoiseCheckBox.AutoSize = true;
            this.useNoiseCheckBox.Location = new System.Drawing.Point(185, 393);
            this.useNoiseCheckBox.Name = "useNoiseCheckBox";
            this.useNoiseCheckBox.Size = new System.Drawing.Size(75, 17);
            this.useNoiseCheckBox.TabIndex = 19;
            this.useNoiseCheckBox.Text = "Use Noise";
            this.useNoiseCheckBox.UseVisualStyleBackColor = true;
            this.useNoiseCheckBox.CheckedChanged += new System.EventHandler(this.useNoiseCheckBox_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(566, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(160, 382);
            this.flowLayoutPanel1.TabIndex = 20;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // addGradientButton
            // 
            this.addGradientButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addGradientButton.BackColor = System.Drawing.SystemColors.Control;
            this.addGradientButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.addGradientButton.Location = new System.Drawing.Point(612, 393);
            this.addGradientButton.Name = "addGradientButton";
            this.addGradientButton.Size = new System.Drawing.Size(75, 23);
            this.addGradientButton.TabIndex = 21;
            this.addGradientButton.Text = "add gradient";
            this.addGradientButton.UseVisualStyleBackColor = false;
            this.addGradientButton.Click += new System.EventHandler(this.addGradientButton_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 442);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Mix pwr";
            // 
            // MixPwrTrackBar
            // 
            this.MixPwrTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MixPwrTrackBar.Location = new System.Drawing.Point(75, 437);
            this.MixPwrTrackBar.Maximum = 100;
            this.MixPwrTrackBar.Name = "MixPwrTrackBar";
            this.MixPwrTrackBar.Size = new System.Drawing.Size(104, 45);
            this.MixPwrTrackBar.TabIndex = 22;
            this.MixPwrTrackBar.Scroll += new System.EventHandler(this.MixPwrTrackBar_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 494);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MixPwrTrackBar);
            this.Controls.Add(this.addGradientButton);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.useNoiseCheckBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.patternSizeTrackBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dispersionTrackBar);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(600, 150);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dispersionTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patternSizeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MixPwrTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar dispersionTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar patternSizeTrackBar;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox useNoiseCheckBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button addGradientButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar MixPwrTrackBar;
    }
}

