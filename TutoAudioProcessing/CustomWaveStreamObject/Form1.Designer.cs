namespace CustomWaveStreamObject
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
            this.startTone = new System.Windows.Forms.Button();
            this.stopTone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startTone
            // 
            this.startTone.Location = new System.Drawing.Point(50, 58);
            this.startTone.Name = "startTone";
            this.startTone.Size = new System.Drawing.Size(191, 23);
            this.startTone.TabIndex = 0;
            this.startTone.Text = "Start Tone";
            this.startTone.UseVisualStyleBackColor = true;
            this.startTone.Click += new System.EventHandler(this.startTone_Click);
            // 
            // stopTone
            // 
            this.stopTone.Location = new System.Drawing.Point(50, 109);
            this.stopTone.Name = "stopTone";
            this.stopTone.Size = new System.Drawing.Size(191, 23);
            this.stopTone.TabIndex = 1;
            this.stopTone.Text = "Stop Tone";
            this.stopTone.UseVisualStyleBackColor = true;
            this.stopTone.Click += new System.EventHandler(this.stopTone_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 266);
            this.Controls.Add(this.stopTone);
            this.Controls.Add(this.startTone);
            this.Name = "Form1";
            this.Text = "WaveStream";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startTone;
        private System.Windows.Forms.Button stopTone;
    }
}

