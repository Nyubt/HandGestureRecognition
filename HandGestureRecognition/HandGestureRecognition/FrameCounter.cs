using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HandGestureRecognition
{
    public class FrameCounter
    {
        PointF[] positions = new PointF[2];

        public void Add(int fingerCount, PointF currentPosition)
        {
            if (this.FingerCount != fingerCount)
            {
                Reset();
                this.FingerCount = fingerCount;
            }

            positions[0] = positions[1];
            positions[1] = currentPosition;
        }

        public void Reset()
        {
            positions[0] = positions[1] = PointF.Empty;
        }

        public bool IsValid
        {
            get
            {
                return positions[0] != PointF.Empty && positions[1] != PointF.Empty;
            }
        }

        public int FingerCount { get; set; }

        public PointF LastPosition { get { return positions[0]; } }

        public PointF CurrentPosition { get { return positions[1]; } }

        public float DifX
        {
            get
            {
                return Math.Abs(CurrentPosition.X - LastPosition.X);
            }
        }

        public float DifY
        {
            get
            {
                return Math.Abs(CurrentPosition.Y - LastPosition.Y);
            }
        }
    }

}
