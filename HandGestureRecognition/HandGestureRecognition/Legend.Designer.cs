namespace HandGestureRecognition
{
    partial class Legend
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
            this.fingerLabel = new System.Windows.Forms.Label();
            this.frameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fingerLabel
            // 
            this.fingerLabel.AutoSize = true;
            this.fingerLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fingerLabel.Location = new System.Drawing.Point(12, 9);
            this.fingerLabel.Name = "fingerLabel";
            this.fingerLabel.Size = new System.Drawing.Size(54, 65);
            this.fingerLabel.TabIndex = 1;
            this.fingerLabel.Text = "0";
            // 
            // frameLabel
            // 
            this.frameLabel.AutoSize = true;
            this.frameLabel.Location = new System.Drawing.Point(13, 78);
            this.frameLabel.Name = "frameLabel";
            this.frameLabel.Size = new System.Drawing.Size(13, 13);
            this.frameLabel.TabIndex = 2;
            this.frameLabel.Text = "0";
            // 
            // Legend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(73, 101);
            this.Controls.Add(this.frameLabel);
            this.Controls.Add(this.fingerLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1, 1);
            this.Name = "Legend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Legend";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fingerLabel;
        private System.Windows.Forms.Label frameLabel;
    }
}