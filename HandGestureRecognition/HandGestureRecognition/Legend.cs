using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HandGestureRecognition
{
    public partial class Legend : Form
    {
        private int frameCount = 0;

        public Legend()
        {
            InitializeComponent();
        }

        public int FingerCount
        {
            set
            {
                this.fingerLabel.Text = value.ToString();
            }
        }

        public int FrameCount
        {
            get
            {
                return frameCount;
            }
            set
            {
                frameCount = value;
                this.frameLabel.Text = value.ToString();
            }
        }
    }
}
