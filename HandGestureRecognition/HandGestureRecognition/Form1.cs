using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV.Structure;
using Emgu.CV;
using HandGestureRecognition.SkinDetector;
using Emgu.CV.CvEnum;
using System.Runtime.InteropServices;
using System.Threading;

namespace HandGestureRecognition
{
    public partial class Form1 : Form
    {

        YCrCbSkinDetector skinDetector;

        Image<Bgr, Byte> currentFrame;
        Image<Bgr, Byte> currentFrameCopy;
                
        Capture grabber;
        AdaptiveSkinDetector detector;
        
        int frameWidth;
        int frameHeight;

        List<int> gesturenumber = new List<int>();

        Hsv hsv_min;
        Hsv hsv_max;
        Ycc YCrCb_min;
        Ycc YCrCb_max;
        
        Seq<Point> hull;
        Seq<Point> filteredHull;
        Seq<MCvConvexityDefect> defects;
        MCvConvexityDefect[] defectArray;
        Rectangle handRect;
        MCvBox2D box;
        Ellipse ellip;

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        static IntPtr NULL = IntPtr.Zero;
        const uint WM_KEYDOWN = 0x0100;
        const uint WM_KEYUP = 0x0101;
        const uint WM_SYSKEYDOWN = 0x0104;
        const uint WM_SYSKEYUP = 0x0105;

        const int VK_NEXT = 0x22;
        const int VK_PRIOR = 0x21;
        const int VK_DOWN = 0x28;
        const int VK_UP = 0x26;
        const int VK_LEFT = 0x25;
        const int VK_RIGHT = 0x27;
        const int VK_BACK = 0x08;
        const int VK_MENU = 0x12;
        const int VK_BROWSER_FORWARD = 0xA7;
        const int VK_BROWSER_BACK = 0xA6;
        const int VK_HOME = 0x24;

        const int CHANGE_THRESHOLD = 20;
        
        int [] numardedegete = new int[5];
        int indice = 0;
        int numarframe = 0;

        FrameCounter counter = new FrameCounter();

        Legend legend;

        public Form1()
        {
            InitializeComponent();
            grabber = new Emgu.CV.Capture(1);
            //grabber = new Capture();

            //grabber.SetCaptureProperty(CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 640);
            //grabber.SetCaptureProperty(CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 480);

            grabber.QueryFrame();
            frameWidth = grabber.Width;
            frameHeight = grabber.Height;
            detector = new AdaptiveSkinDetector(1, AdaptiveSkinDetector.MorphingMethod.NONE);
            hsv_min = new Hsv(0, 45, 0);
            hsv_max = new Hsv(20, 255, 255);
            //YCrCb_min = new Ycc(0, 131, 80);
            //YCrCb_max = new Ycc(255, 185, 135);
            Ymin.Value = 0;
            Cbmin.Value = 131;
            Crmin.Value = 80;
            Ymax.Value = 255;
            Cbmax.Value = 185;
            Crmax.Value = 135;
            YCrCb_min = new Ycc(0, 131, 80);
            YCrCb_max = new Ycc(150, 185, 135);
            box = new MCvBox2D();
            ellip = new Ellipse();

            legend = new Legend();

            Application.Idle += new EventHandler(FrameGrabber);                        
        }

        int Numbering(List<int> a)
        {
            int z = 1;
            for (int i = 1; i < a.Count; i++)
            {
                if (a[i] == a[i - 1] && a[i]!=0)
                {
                    z++;
                }
                else z = 1;
            }
            return z;
        }

        void DrawPoint(PointF p)
        {
            currentFrame.Draw(new CircleF(p, 3), new Bgr(Color.Gold), 2);
        }

        double Angle(PointF startPoint, PointF endPoint, PointF depthPoint)
        {
            double a = Math.Sqrt((endPoint.X - startPoint.X) * (endPoint.X - startPoint.X) + (endPoint.Y - startPoint.Y) * (endPoint.Y - startPoint.Y));
            double b = Math.Sqrt((depthPoint.X - startPoint.X) * (depthPoint.X - startPoint.X) + (depthPoint.Y - startPoint.Y) * (depthPoint.Y - startPoint.Y));
            double c = Math.Sqrt((endPoint.X - depthPoint.X) * (endPoint.X - depthPoint.X) + (endPoint.Y - depthPoint.Y) * (endPoint.Y - depthPoint.Y));
            return Math.Acos((b * b + c * c - a * a) / (2 * b * c)) * 57;
        }

        void FrameGrabber(object sender, EventArgs e)
        {
            legend.FrameCount++;

            currentFrame = grabber.QueryFrame();
            if (currentFrame != null)
            {
                currentFrameCopy = currentFrame.Copy();

                //if using opencv adaptive skin detector
                //Image<Gray,Byte> skin = new Image<Gray,byte>(currentFrameCopy.Width,currentFrameCopy.Height);                
                //detector.Process(currentFrameCopy, skin);                

                skinDetector = new YCrCbSkinDetector();

                YCrCb_min = new Ycc(Ymin.Value, Cbmin.Value, Crmin.Value);
                YCrCb_max = new Ycc(Ymax.Value, Cbmax.Value, Crmax.Value);

                Image<Gray, Byte> gray = currentFrameCopy.Convert<Gray, Byte>();
                using (var storage = new MemStorage())
                {
                    var canny = gray.Canny(new Gray(30), new Gray(30));
                    var contour = FindBiggestContour(canny, storage);
                    canny.Draw(contour, new Gray(255), 3);

                    imageBoxCanny.Image = canny;
                }

                Image<Gray, Byte> skin = skinDetector.FilterRange(currentFrameCopy, YCrCb_min, YCrCb_max);
                imageBoxFilter.Image = skin;

                skin = skinDetector.ErodeDilate(skin);
                imageBoxSkin.Image = skin;

                ExtractContourAndHull(skin);
                gesturenumber.Add(DrawAndComputeFingersNum());
                imageBoxFrameGrabber.Image = currentFrame;

                /*
                for (int i = 0; i < skin.Width; i++)
                {
                    int radius = 30;
                    int Length = 40;
                    for(int j = 0; j < skin.Height; j++)
                    {
                        int perimetru = 0;
                        double aria = 0;
                        int x_val;
                        int y_val;

                        if (skin.Data[j, i, 0] == 255)
                        {
                            y_val = j - Length / 2;
                            if (y_val < 0 || y_val >= skin.Height)
                                continue;
                            for (x_val = i - Length / 2; x_val < i + Length / 2; x_val++)
                            {
                                if (x_val < 0 || x_val >= skin.Width)
                                    continue;
                                if (skin.Data[y_val, x_val, 0] == 255)
                                {
                                    perimetru++;
                                }
                            }
                            y_val = j + Length / 2;
                            if (y_val < 0 || y_val >= skin.Height)
                                continue;
                            for (x_val = i - Length / 2; x_val < i + Length / 2; x_val++)
                            {
                                if (x_val < 0 || x_val >= skin.Width)
                                    continue;
                                if (skin.Data[y_val, x_val, 0] == 255)
                                {
                                    perimetru++;
                                }
                            }
                            x_val = j - Length / 2;
                            if (x_val < 0 || x_val >= skin.Width)
                                continue;
                            for (y_val = i - Length / 2; y_val < i + Length / 2; y_val++)
                            {
                                if (y_val < 0 || y_val >= skin.Height)
                                    continue;
                                if (skin.Data[y_val, x_val, 0] == 255)
                                {
                                    perimetru++;
                                }
                            }
                            x_val = j + Length / 2;
                            if (x_val < 0 || x_val >= skin.Width)
                                continue;
                            for (y_val = i - Length / 2; y_val < i + Length / 2; y_val++)
                            {
                                if (y_val < 0 || y_val >= skin.Height)
                                    continue;
                                if (skin.Data[y_val, x_val, 0] == 255)
                                {
                                    perimetru++;
                                }
                            }
                        }

                        if (perimetru < Length / 2 || perimetru > 2 * Length)
                            continue;
                        
                            for (x_val = i-radius; x_val <= i+radius; x_val++)
                            {
                                if (x_val < 0 || x_val>=skin.Width)
                                    continue;
                                for (y_val = j-radius; y_val <= j+radius; y_val++)
                                {
                                    if (y_val < 0 || y_val>=skin.Height)
                                        continue;
                                    if (((x_val - i) * (x_val - i) + (y_val - j) * (y_val - j)) <= radius*radius && skin.Data[y_val, x_val, 0] == 255)
                                    {
                                        aria++;
                                    }
                                }
                            }


                        if (perimetru < 2 * Length && aria == (3.14) * radius * radius)
                        {
                            currentFrame.Draw(new CircleF(new PointF((float)i, (float)j), 5f), new Bgr(Color.BlueViolet), 5);
                        }
                    }
                }*/
            }
            if(Numbering(gesturenumber) == 3) gesturenumber.Clear();

            numarframe++;
        }

        Contour<Point> FindBiggestContour(Image<Gray, byte> skin, MemStorage storage)
        {
            Contour<Point> contours = skin.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST, storage);
            Contour<Point> biggestContour = null;

            Double Result1 = 0;
            Double Result2 = 0;
            while (contours != null)
            {
                Result1 = contours.Area;
                if (Result1 > Result2)
                {
                    Result2 = Result1;
                    biggestContour = contours;
                }
                contours = contours.HNext;
            }

            return biggestContour;
        }
         
      
        private void ExtractContourAndHull(Image<Gray, byte> skin)
        {
            using (MemStorage storage = new MemStorage())
            {
                Contour<Point> biggestContour = FindBiggestContour(skin, storage);

                if (biggestContour != null)
                {
                    currentFrame.Draw(biggestContour, new Bgr(Color.DarkViolet), 2);
                    Contour<Point> currentContour = biggestContour.ApproxPoly(biggestContour.Perimeter * 0.02, storage);
                    //currentFrame.Draw(currentContour, new Bgr(Color.LimeGreen), 2);
                    biggestContour = currentContour;

                    hull = biggestContour.GetConvexHull(Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE);
                    box = biggestContour.GetMinAreaRect();
                    PointF[] points = box.GetVertices();
                    //handRect = box.MinAreaRect();
                    //currentFrame.Draw(handRect, new Bgr(200, 0, 0), 1);

                    Point[] ps = new Point[points.Length];
                    for (int i = 0; i < points.Length; i++)
                        ps[i] = new Point((int)points[i].X, (int)points[i].Y);

                    currentFrame.DrawPolyline(hull.ToArray(), true, new Bgr(200, 125, 75), 2);
                    currentFrame.Draw(new CircleF(new PointF(box.center.X, box.center.Y), 3), new Bgr(200, 125, 75), 2);

                    //ellip.MCvBox2D= CvInvoke.cvFitEllipse2(biggestContour.Ptr);
                    //currentFrame.Draw(new Ellipse(ellip.MCvBox2D), new Bgr(Color.LavenderBlush), 3);

                    PointF center;
                    float radius;
                    //CvInvoke.cvMinEnclosingCircle(biggestContour.Ptr, out  center, out  radius);
                    //currentFrame.Draw(new CircleF(center, radius), new Bgr(Color.Gold), 2);

                    //currentFrame.Draw(new CircleF(new PointF(ellip.MCvBox2D.center.X, ellip.MCvBox2D.center.Y), 3), new Bgr(100, 25, 55), 2);
                    //currentFrame.Draw(ellip, new Bgr(Color.DeepPink), 2);

                    //CvInvoke.cvEllipse(currentFrame, new Point((int)ellip.MCvBox2D.center.X, (int)ellip.MCvBox2D.center.Y), new System.Drawing.Size((int)ellip.MCvBox2D.size.Width, (int)ellip.MCvBox2D.size.Height), ellip.MCvBox2D.angle, 0, 360, new MCvScalar(120, 233, 88), 1, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, 0);
                    //currentFrame.Draw(new Ellipse(new PointF(box.center.X, box.center.Y), new SizeF(box.size.Height, box.size.Width), box.angle), new Bgr(0, 0, 0), 2);

                    //

                    filteredHull = new Seq<Point>(storage);
                    for (int i = 0; i < hull.Total; i++)
                    {
                        if (Math.Sqrt(Math.Pow(hull[i].X - hull[i + 1].X, 2) + Math.Pow(hull[i].Y - hull[i + 1].Y, 2)) > box.size.Width / 10)
                        {
                            filteredHull.Push(hull[i]);
                        }
                    }
                    
                    defects = biggestContour.GetConvexityDefacts(storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE);
                    defectArray = defects.ToArray();
                }
            }
        }

        private int DrawAndComputeFingersNum()
        {
            int fingerNum = 0;
            string str = "";

            #region hull drawing
            //for (int i = 0; i < filteredHull.Total; i++)
            //{
            //    PointF hullPoint = new PointF((float)filteredHull[i].X,
            //                                  (float)filteredHull[i].Y);
            //    CircleF hullCircle = new CircleF(hullPoint, 4);
            //    currentFrame.Draw(hullCircle, new Bgr(Color.Aquamarine), 2);
            //}
            #endregion

            #region defects drawing
            if (defects != null)
            {
                float CX = 0;
                float CY = 0;

                for (int i = 0; i < defects.Total; i++)
                {

                    PointF startPoint = new PointF((float)defectArray[i].StartPoint.X, (float)defectArray[i].StartPoint.Y);

                    PointF depthPoint = new PointF((float)defectArray[i].DepthPoint.X, (float)defectArray[i].DepthPoint.Y);

                    PointF endPoint = new PointF((float)defectArray[i].EndPoint.X, (float)defectArray[i].EndPoint.Y);

                    LineSegment2D startDepthLine = new LineSegment2D(defectArray[i].StartPoint, defectArray[i].DepthPoint);

                    LineSegment2D depthEndLine = new LineSegment2D(defectArray[i].DepthPoint, defectArray[i].EndPoint);

                    CircleF startCircle = new CircleF(startPoint, 5f);

                    CircleF depthCircle = new CircleF(depthPoint, 5f);

                    CircleF endCircle = new CircleF(endPoint, 5f);

                    //experiment
                    //if ((startCircle.Center.Y < box.center.Y || depthCircle.Center.Y < box.center.Y) && (startCircle.Center.Y < depthCircle.Center.Y) && 
                    //    (Math.Sqrt(Math.Pow(startCircle.Center.X - depthCircle.Center.X, 2) + Math.Pow(startCircle.Center.Y - depthCircle.Center.Y, 2)) > box.size.Height / 3.5))
                    //{
                    //    fingerNum++;
                    //    CX += startCircle.Center.X;
                    //    CY += startCircle.Center.Y;
                    //    //currentFrame.Draw(startDepthLine, new Bgr(Color.Green), 2);
                    //    //currentFrame.Draw(depthEndLine, new Bgr(Color.Magenta), 2);
                    //}                    

                    double a = Math.Sqrt((endPoint.X - startPoint.X) * (endPoint.X - startPoint.X) + (endPoint.Y - startPoint.Y) * (endPoint.Y - startPoint.Y));
                    double b = Math.Sqrt((depthPoint.X - startPoint.X) * (depthPoint.X - startPoint.X) + (depthPoint.Y - startPoint.Y) * (depthPoint.Y - startPoint.Y));
                    double c = Math.Sqrt((endPoint.X - depthPoint.X) * (endPoint.X - depthPoint.X) + (endPoint.Y - depthPoint.Y) * (endPoint.Y - depthPoint.Y));
                    double angle = Math.Acos((b * b + c * c - a * a) / (2 * b * c)) * 57;
                    if (angle <= 90)
                    {
                        if (b > box.size.Height / 5 && c > box.size.Height / 5)
                        {
                            fingerNum += 1;
                            currentFrame.Draw(depthCircle, new Bgr(Color.Yellow), 3);
                            currentFrame.Draw(startDepthLine, new Bgr(Color.Green), 2);
                        }
                        //cv2.circle(crop_img,far,1,[0,0,255],-1)
                        //cv2.line(crop_img,start,end,[0,255,0],2)
                    }
                    //currentFrame.Draw(startCircle, new Bgr(Color.Red), 2);
                    //currentFrame.Draw(depthCircle, new Bgr(Color.Yellow), 5);
                    //currentFrame.Draw(endCircle, new Bgr(Color.DarkBlue), 4);
                }

                if (fingerNum > 0) fingerNum++;

                if (fingerNum == 0 && hull.Total > 0)
                {
                    if (Angle(hull[hull.Total - 1], hull[1], hull[0]) <= 45)
                    {
                        fingerNum = 1;
                        DrawPoint(hull[hull.Total - 1]);
                        DrawPoint(hull[0]);
                        DrawPoint(hull[1]);
                    }

                    for (int i = 1; i < hull.Total - 1; i++)
                    {
                        if (Angle(hull[i - 1], hull[i + 1], hull[i]) <= 45)
                        {
                            fingerNum = 1;
                            DrawPoint(hull[i - 1]);
                            DrawPoint(hull[i]);
                            DrawPoint(hull[i + 1]);
                        }
                    }
                    if (Angle(hull[hull.Total - 2], hull[0], hull[hull.Total - 1]) <= 45)
                    {
                        fingerNum = 1;
                        DrawPoint(hull[hull.Total - 2]);
                        DrawPoint(hull[0]);
                        DrawPoint(hull[hull.Total - 1]);
                    }
                }

                if (indice > 4) indice = 0;
                numardedegete[indice] = fingerNum;
                indice++;

                //    PointF Cgreu = new PointF(CX / fingerNum, CY / fingerNum);
                //    PointF FarthestPoint = Cgreu;

                //    for (int i = 0; i < defects.Total; i++)
                //    {                    
                //        PointF startPoint = new PointF((float)defectArray[i].StartPoint.X, (float)defectArray[i].StartPoint.Y);
                //        float X1 = startPoint.X - Cgreu.X;
                //        float Y1 = startPoint.Y - Cgreu.Y;
                //        float X2 = FarthestPoint.X - Cgreu.X;
                //        float Y2 = FarthestPoint.Y - Cgreu.Y;
                //        if (Math.Sqrt(X1 * X1 + Y1 * Y1) > Math.Sqrt(X2 * X2 + Y2 * Y2))
                //        {
                //            FarthestPoint = new PointF(startPoint.X, startPoint.Y);
                //        }
                //    }

                //    currentFrame.Draw(new CircleF(FarthestPoint, 3), new Bgr(Color.Thistle), 2);

                //    float a = FarthestPoint.Y - Cgreu.Y;
                //    float b = FarthestPoint.X - Cgreu.X;

                //    if (a < 0)
                //    {
                //        if (b < 0)
                //        {
                //            if (a < b)
                //            {
                //                str = "UP";
                //            }
                //            else
                //                str = "LEFT";
                //        }
                //        else
                //        {
                //            if (a < b)
                //            {
                //                str = "UP";
                //            }
                //            else
                //                str = "RIGHT";
                //        }

                //    }
                //    else
                //    {
                //        if (b < 0)
                //        {
                //            if (a > b)
                //            {
                //                str = "DOWN";
                //            }
                //            else
                //                str = "LEFT";
                //        }
                //        else
                //        {
                //            if (a > b)
                //            {
                //                str = "DOWN";
                //            }
                //            else
                //                str = "RIGHT";
                //        }
                //    }
            }
            #endregion

            int numarrotungit = (int) Math.Round(numardedegete.Sum() / 5.0);
            legend.FingerCount = numarrotungit;

            MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_DUPLEX, 5d, 5d);
            currentFrame.Draw((numarrotungit).ToString(), ref font, new Point(50, 150), new Bgr(Color.White));
            //currentFrame.Draw(fingerNum.ToString(), ref font, new Point(50, 150), new Bgr(Color.White));
            currentFrame.Draw(str, ref font, new Point(50, 120), new Bgr(Color.Pink));

            if (numarframe % 10 == 0)
            {
                counter.Add(numarrotungit, box.center);

                if (counter.IsValid && counter.FingerCount == 5)
                {
                    textBox1.Text = counter.LastPosition.ToString();
                    textBox2.Text = counter.CurrentPosition.ToString();

                    if (counter.LastPosition.Y < counter.CurrentPosition.Y && counter.DifX < counter.DifY && counter.DifY > CHANGE_THRESHOLD)
                    {
                        SendKey(VK_NEXT);
                    }
                    else if (counter.LastPosition.Y > counter.CurrentPosition.Y && counter.DifX < counter.DifY && counter.DifY > CHANGE_THRESHOLD)
                    {
                        SendKey(VK_PRIOR);
                    }
                    if (counter.LastPosition.X < counter.CurrentPosition.X && counter.DifX > counter.DifY && counter.DifX > CHANGE_THRESHOLD)
                    {
                        SendKey(VK_RIGHT);
                    }
                    else if (counter.LastPosition.X > counter.CurrentPosition.X && counter.DifX > counter.DifY && counter.DifX > CHANGE_THRESHOLD)
                    {
                        SendKey(VK_LEFT);
                    }

                    numarrotungit = 0;
                }
                else if (counter.FingerCount == 2 && counter.IsValid)
                {
                    if (counter.LastPosition.X < counter.CurrentPosition.X && counter.DifX > counter.DifY && counter.DifX > CHANGE_THRESHOLD)
                    {
                        SendKey(VK_BROWSER_FORWARD);
                    }   
                    else if (counter.LastPosition.X > counter.CurrentPosition.X && counter.DifX > counter.DifY && counter.DifX > CHANGE_THRESHOLD)
                    {
                        SendKey(VK_BACK);
                    }
                    if (counter.LastPosition.Y < counter.CurrentPosition.Y && counter.DifX < counter.DifY && counter.DifY > CHANGE_THRESHOLD)
                    {
                        SendKey(VK_DOWN);
                    }
                    else if (counter.LastPosition.Y > counter.CurrentPosition.Y && counter.DifX < counter.DifY && counter.DifY > CHANGE_THRESHOLD)
                    {
                        SendKey(VK_UP);
                    }
                }
                else if (counter.FingerCount == 1 && counter.IsValid)
                {
                    SendKey(VK_HOME);
                }

                numarframe = 1;
            }

            return fingerNum;
        }

        private void SendKey(int keyCode, bool alt = false)
        {
            int lParam = 0;
            IntPtr childWindow = CurrentWindow();

            if (childWindow == IntPtr.Zero)
                return;

            if (alt)
            {
                PostMessage(childWindow, WM_SYSKEYDOWN, keyCode, 0x1 << 29);
            } 
            else
            {
                PostMessage(childWindow, WM_KEYDOWN, keyCode, lParam);
            }
        }

        private IntPtr CurrentWindow()
        {
            IntPtr childWindow = PowerPoint();
            if (childWindow == IntPtr.Zero)
            {
                childWindow = Explorer();
            }

            return childWindow;
        }

        private static IntPtr Explorer()
        {
            IntPtr parentWindow = FindWindowEx(NULL, NULL, "IEFrame", null);
            IntPtr childWindow = FindWindowEx(parentWindow, NULL, "Frame Tab", null);
            childWindow = FindWindowEx(childWindow, NULL, "TabWindowClass", null);
            childWindow = FindWindowEx(childWindow, NULL, "Shell DocObject View", null);
            childWindow = FindWindowEx(childWindow, NULL, "Internet Explorer_Server", null);
            return childWindow;
        }

        private static IntPtr PowerPoint()
        {
            IntPtr parentWindow = FindWindowEx(NULL, NULL, "screenClass", null);
            IntPtr childWindow = FindWindowEx(parentWindow, NULL, "paneClassDC", null);
            return childWindow;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            legend.Show();
        }
                                      
    }

    public class YCrCbSkinDetector : IColorSkinDetector
    {
        public override Image<Gray, byte> DetectSkin(Image<Bgr, byte> Img, IColor min, IColor max)
        {
            var skin = FilterRange(Img, min, max);
            return ErodeDilate(skin);
        }

        public Image<Gray, byte> FilterRange(Image<Bgr, byte> Img, IColor min, IColor max)
        {
            Image<Ycc, Byte> currentYCrCbFrame = Img.Convert<Ycc, Byte>();
            Image<Gray, byte> skin = new Image<Gray, byte>(Img.Width, Img.Height);
            skin = currentYCrCbFrame.InRange((Ycc)min, (Ycc)max);
            return skin;
        }

        public Image<Gray, byte> ErodeDilate(Image<Gray, byte> skin)
        {
            StructuringElementEx rect_6 = new StructuringElementEx(6, 6, 3, 3, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            CvInvoke.cvDilate(skin, skin, rect_6, 2);

            StructuringElementEx rect_12 = new StructuringElementEx(12, 12, 6, 6, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            CvInvoke.cvErode(skin, skin, rect_12, 1);
            //StructuringElementEx rect_6 = new StructuringElementEx(6, 6, 3, 3, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            //CvInvoke.cvDilate(skin, skin, rect_6, 2);
            return skin;
        }

    }
}