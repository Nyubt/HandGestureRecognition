namespace HandGestureRecognition
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
            this.components = new System.ComponentModel.Container();
            this.imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            this.Crmin = new System.Windows.Forms.TrackBar();
            this.Cbmin = new System.Windows.Forms.TrackBar();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Crmax = new System.Windows.Forms.TrackBar();
            this.Cbmax = new System.Windows.Forms.TrackBar();
            this.Ymax = new System.Windows.Forms.TrackBar();
            this.imageBoxSkin = new Emgu.CV.UI.ImageBox();
            this.Ymin = new System.Windows.Forms.TrackBar();
            this.imageBoxFilter = new Emgu.CV.UI.ImageBox();
            this.imageBoxCanny = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Crmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Crmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ymax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxSkin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ymin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCanny)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBoxFrameGrabber
            // 
            this.imageBoxFrameGrabber.Location = new System.Drawing.Point(3, 3);
            this.imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            this.imageBoxFrameGrabber.Size = new System.Drawing.Size(320, 240);
            this.imageBoxFrameGrabber.TabIndex = 2;
            this.imageBoxFrameGrabber.TabStop = false;
            // 
            // Crmin
            // 
            this.Crmin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Crmin.Location = new System.Drawing.Point(655, 293);
            this.Crmin.Maximum = 255;
            this.Crmin.Name = "Crmin";
            this.Crmin.Size = new System.Drawing.Size(162, 35);
            this.Crmin.TabIndex = 5;
            // 
            // Cbmin
            // 
            this.Cbmin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Cbmin.Location = new System.Drawing.Point(655, 176);
            this.Cbmin.Maximum = 255;
            this.Cbmin.Name = "Cbmin";
            this.Cbmin.Size = new System.Drawing.Size(162, 35);
            this.Cbmin.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(655, 36);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(162, 20);
            this.textBox2.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(655, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(162, 20);
            this.textBox1.TabIndex = 7;
            // 
            // Crmax
            // 
            this.Crmax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Crmax.Location = new System.Drawing.Point(655, 344);
            this.Crmax.Maximum = 255;
            this.Crmax.Name = "Crmax";
            this.Crmax.Size = new System.Drawing.Size(162, 35);
            this.Crmax.TabIndex = 6;
            // 
            // Cbmax
            // 
            this.Cbmax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Cbmax.Location = new System.Drawing.Point(655, 227);
            this.Cbmax.Maximum = 255;
            this.Cbmax.Name = "Cbmax";
            this.Cbmax.Size = new System.Drawing.Size(162, 35);
            this.Cbmax.TabIndex = 5;
            // 
            // Ymax
            // 
            this.Ymax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Ymax.Location = new System.Drawing.Point(655, 113);
            this.Ymax.Maximum = 255;
            this.Ymax.Name = "Ymax";
            this.Ymax.Size = new System.Drawing.Size(162, 35);
            this.Ymax.TabIndex = 4;
            // 
            // imageBoxSkin
            // 
            this.imageBoxSkin.Location = new System.Drawing.Point(3, 250);
            this.imageBoxSkin.Name = "imageBoxSkin";
            this.imageBoxSkin.Size = new System.Drawing.Size(320, 240);
            this.imageBoxSkin.TabIndex = 2;
            this.imageBoxSkin.TabStop = false;
            // 
            // Ymin
            // 
            this.Ymin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Ymin.Location = new System.Drawing.Point(655, 62);
            this.Ymin.Maximum = 255;
            this.Ymin.Name = "Ymin";
            this.Ymin.Size = new System.Drawing.Size(162, 35);
            this.Ymin.TabIndex = 3;
            // 
            // imageBoxFilter
            // 
            this.imageBoxFilter.Location = new System.Drawing.Point(329, 3);
            this.imageBoxFilter.Name = "imageBoxFilter";
            this.imageBoxFilter.Size = new System.Drawing.Size(320, 240);
            this.imageBoxFilter.TabIndex = 9;
            this.imageBoxFilter.TabStop = false;
            // 
            // imageBoxCanny
            // 
            this.imageBoxCanny.Location = new System.Drawing.Point(329, 250);
            this.imageBoxCanny.Name = "imageBoxCanny";
            this.imageBoxCanny.Size = new System.Drawing.Size(320, 240);
            this.imageBoxCanny.TabIndex = 10;
            this.imageBoxCanny.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 524);
            this.Controls.Add(this.imageBoxCanny);
            this.Controls.Add(this.imageBoxFilter);
            this.Controls.Add(this.Cbmin);
            this.Controls.Add(this.Ymin);
            this.Controls.Add(this.Crmin);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Crmax);
            this.Controls.Add(this.Cbmax);
            this.Controls.Add(this.Ymax);
            this.Controls.Add(this.imageBoxFrameGrabber);
            this.Controls.Add(this.imageBoxSkin);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Crmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Crmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cbmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ymax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxSkin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ymin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCanny)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber;
        private System.Windows.Forms.SplitContainer splitContainerFrames;
        private Emgu.CV.UI.ImageBox imageBoxSkin;
        private System.Windows.Forms.TrackBar Crmin;
        private System.Windows.Forms.TrackBar Cbmin;
        private System.Windows.Forms.TrackBar Crmax;
        private System.Windows.Forms.TrackBar Cbmax;
        private System.Windows.Forms.TrackBar Ymax;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TrackBar Ymin;
        private Emgu.CV.UI.ImageBox imageBoxFilter;
        private Emgu.CV.UI.ImageBox imageBoxCanny;
    }
}

