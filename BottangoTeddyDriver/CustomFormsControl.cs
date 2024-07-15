using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.Design;
using System.Collections.Generic;

namespace BottangoTeddyDriver
{
    public class DeerLineChart : System.Windows.Forms.Control
    {
        /// <summary>
        ///    Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components;

        private bool allowUserEdit = true;
        private bool draggingTimeline = false;
        private bool draggingCursor = false;
        private Brush baseBackground = null;

        private double zoomLevel = 0.05f;
        public int headerHeight = 30; //in px

        public float[] samples;
        private Point dragTimelineStart;
        private Point dragCursorStart;
        private int scrollOffset = 0; //how many samples are to the left of the visible area


        List<KeyFrame> frames = new List<KeyFrame>();
        private List<int> linestodraw = new List<int>();
        private int maxSamplesVisible;
        private int actualSamplesVisible;
        private PointF[] curvePoints;

        public float samplesSpacing { get { return (float)ZoomLevel; } } // how many pixels between samples

        private int _currentSample;
        private PointF[][] frameCurves;

        public int CurrentSample
        {
            get { return _currentSample; }
            set
            {
                if (_currentSample != value)
                {
                    _currentSample = value;
                    Invalidate(HeaderRectangle);
                    Update();
                }
            }
        }

        public int SampleRate { get; set; }

        public bool FramesVisible { get; set; } = true;
        public bool MarkersVisible { get; set; } = true;


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle HeaderRectangle
        {
            get
            {
                return new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, headerHeight);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle GraphRectangle
        {
            get
            {
                return new Rectangle(ClientRectangle.X, ClientRectangle.Y + headerHeight, ClientRectangle.Width, ClientRectangle.Height - headerHeight);
            }
        }

        [ Category("Deer"), DefaultValue(1) ]
        public double ZoomLevel
        {
            get
            {
                return zoomLevel;
            }
            set
            {
                if (zoomLevel != value)
                {
                    zoomLevel = value;
                    Invalidate();
                }
            }
        }

        public float PeakTreshold { get; set; }

        public DeerLineChart()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.UserPaint, true);

            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            Debug.Assert(GetStyle(ControlStyles.ResizeRedraw), "Should be redraw!");
        }

        /// <summary>
        ///    Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        internal void Clear()
        {
            frames = new List<KeyFrame>();
            linestodraw = new List<int>();
            //scrollOffset = 0;

            this.samples = null;
        }

        /// <summary>
        ///    Required method for Designer support - do not modify
        ///    the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ForeColor = System.Drawing.Color.White;
            this.BackColor = System.Drawing.Color.Black;
            this.Size = new System.Drawing.Size(100, 23);
            this.Text = "DeerLineChart";
        }

        public void AddFrame(KeyFrame frame)
        {
            frames.Add(frame);
        }

        public void SetFrames(List<KeyFrame> basd)
        {
            frames = basd;
        }

        public void AddFrames(List<KeyFrame> badFrames)
        {
            frames.AddRange(badFrames);

            frames.Sort((a, b) => a.Time.CompareTo(b.Time));
        }

        public void AddMarkers(List<int> badSyncs)
        {
            linestodraw.AddRange(badSyncs);
        }

        public void ScrollToSample(int v)
        {
            if (samples == null) v = 0;
            else
            {
                v = Math.Min(Math.Max(v, 0), samples.Length - 1);
            }

            if(scrollOffset != v)
            {
                scrollOffset = v;
                _currentSample = v;
                Invalidate();
            }
        }

        public void SetSamples(float[] samples)
        {
            this.samples = samples;
            Invalidate();
        }




        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!allowUserEdit)
            {
                return;
            }

            bool x = Focus();


            switch (e.Button)
            {
                case MouseButtons.Left: //drag timeline
                    Capture = true;
                    draggingTimeline = true;
                    dragTimelineStart = new Point(e.X, e.Y);
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right: //drag cursor
                    Capture = true;
                    draggingCursor = true;
                    dragCursorStart = new Point(e.X, e.Y);
                    dragCursor(e);
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!allowUserEdit || (!draggingTimeline && !draggingCursor))
            {
                return;
            }

            if (draggingTimeline)
            {
                int dragamount = e.X - dragTimelineStart.X;

                float pixelamount = (float)(dragamount / ZoomLevel);

                int oldOffset = scrollOffset;

                scrollOffset = Math.Max(scrollOffset - (int)pixelamount, 0);

                if (!(oldOffset == 0 && pixelamount > 0)) // dont refresh if dragging left from 0
                {
                    if (Math.Abs(pixelamount) >= 1)
                    {
                        dragTimelineStart = new Point(e.X, e.Y);
                        Invalidate();
                        Update();
                    }
                }
            }
            else if (draggingCursor)
            {
                dragCursor(e);
            }
        }

        void dragCursor(MouseEventArgs e)
        {
            int whichsampleonscreen = (int)Math.Round(scrollOffset + e.X / ZoomLevel);

            int oldSample = _currentSample;

            _currentSample = Math.Max(whichsampleonscreen, 0);

            if (_currentSample != oldSample) // dont refresh if dragging left from 0
            {
                //if (Math.Abs(pixelamount) >= 1)
                {
                    dragCursorStart = new Point(e.X, e.Y);
                    Invalidate(HeaderRectangle);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!allowUserEdit || (!draggingTimeline && !draggingCursor))
            {
                Capture = false;
                return;
            }

            switch (e.Button)
            {
                case MouseButtons.Left: //drag timeline
                    draggingTimeline = false;
                    break;
                case MouseButtons.Right: //drag cursor
                    draggingCursor = false;
                    break;
                default:
                    break;
            }

            Capture = (draggingTimeline || draggingCursor);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int numberOfScrolls = e.Delta / 120; //magic number for some reason thx windows

            double newZoom = ZoomLevel;

            if (numberOfScrolls > 0)
            {
                newZoom *= 2;
            }
            else if (numberOfScrolls < 0)
            {
                newZoom /= 2;
            }
            else
            {
                return;
            }


            newZoom = Math.Max(newZoom, 0.0001f);

            if (ZoomLevel != newZoom)
            {
                ZoomLevel = newZoom;

                if (numberOfScrolls > 0) scrollOffset += (int)(e.X / ZoomLevel);
                else scrollOffset -= (int)(e.X / ZoomLevel / 2);

                scrollOffset = Math.Max(scrollOffset, 0);


                Console.WriteLine("Zoom: " + ZoomLevel);
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            
            if (!Focused) return;

            switch (e.KeyCode)
            {
                case Keys.Right:
                    Console.WriteLine("right");
                    break;
                default:
                    base.OnPreviewKeyDown(e);
                    break;
            }

        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            if (baseBackground == null) baseBackground = new SolidBrush(BackColor);

            if (e.ClipRectangle.Height > HeaderRectangle.Height) DrawGrid(e);

            maxSamplesVisible = (int)Math.Round((GraphRectangle.Width / ZoomLevel)); //how many samples could fit on the screen





            DrawHeader(e); // cursor

            if (e.ClipRectangle.Height <= HeaderRectangle.Height) return;

            DrawGrid(e); // vertical lines

            

            DrawLabels(e); // draw bottom labels (shows samplenumber)

            if (FramesVisible) DrawFrames(e);
            else DrawFrameGraphs(e);

            if (MarkersVisible) DrawMarkerLines(e);

            if (samples != null && samples.Length >= 2)
            {
                actualSamplesVisible = Math.Min(maxSamplesVisible, (samples.Length - 1) - scrollOffset);

                if (actualSamplesVisible < 2)
                {
                    e.Graphics.DrawString("Not enough data points, try zooming out.", Font, new SolidBrush(Color.Black), 0, 0);
                    return;
                }
                else
                {
                    if (ZoomLevel >= 0.04)
                    {
                        GenerateCurvePoints();
                        DrawCurve(e);
                    }
                }
            }



            //Console.WriteLine("Sample: " + samples[scrollOffset]);
        }

        private void DrawHeader(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(baseBackground, HeaderRectangle);

            float x = samplesSpacing * (CurrentSample - scrollOffset);

            //x = HeaderRectangle.Width / 2;
            
            Brush blueBrush = new SolidBrush(Focused ? Color.Blue : Color.Red);
            DrawDownTriangle(e, blueBrush, x, HeaderRectangle.Bottom, HeaderRectangle.Height);

            string label = CurrentSample.ToString();
            if (samples != null && samples.Length > CurrentSample) label += $" {samples[CurrentSample]*100:F}%";
            if (SampleRate > 0) label += string.Format("\n{0:0.0##}s", (float)CurrentSample / SampleRate);
            DrawTextBottom(e, label, x + HeaderRectangle.Bottom, HeaderRectangle.Bottom);
        }

        /// <summary>
        /// Draws a triangle pointing down defined by its lowest point and height
        /// </summary>
        /// <param name="e"></param>
        /// <param name="brush"></param>
        /// <param name="x">X coordinate of the bottom edge</param>
        /// <param name="y">Y coordinate of the bottom edge</param>
        /// <param name="size">Height of the triangle</param>
        private void DrawDownTriangle(PaintEventArgs e, Brush brush, float x, float y, float size)
        {
            PointF[] points = { new PointF(x - size / 2, y - size), new PointF(x + size / 2, y - size), new PointF(x, y) };

            e.Graphics.FillPolygon(brush, points, FillMode.Alternate);
        }

        private void DrawCurve(PaintEventArgs e)
        {
            Pen redPen = new Pen(Color.Red, 1.5f);

            e.Graphics.DrawCurve(redPen, curvePoints, 0.2f);

            Brush blackBrush = new SolidBrush(Color.Black);
            for (int i = 0; i < actualSamplesVisible; i++)
            {
                DrawCenteredCircle(e, blackBrush, curvePoints[i].X, curvePoints[i].Y, (float)(ZoomLevel / 10));
            }
        }

        private void DrawMarkerLines(PaintEventArgs e)
        {
            Pen bluePen = new Pen(Color.Blue, 3f);

            List<float> visibleLines = new List<float>();

            foreach (var sample in linestodraw)
            {
                float x = samplesSpacing * (sample - scrollOffset);
                if (x > 0 && x < GraphRectangle.Width)
                {
                    visibleLines.Add(x);
                }
            }

            if (visibleLines.Count < 1000)
            {
                foreach (var line in visibleLines)
                {
                    e.Graphics.DrawLine(bluePen, line, GraphRectangle.Y, line, GraphRectangle.Bottom);
                }               
            }
        }

        private void DrawFrames(PaintEventArgs e)
        {
            float rectHeight = GraphRectangle.Y + GraphRectangle.Height * 0.8f;
            float labelHeight = GraphRectangle.Y + GraphRectangle.Height * 0.9f;
            Brush labelBgBrush = new SolidBrush(Color.FromArgb(200, Color.White));
            Brush[] bruhs = {
                    new SolidBrush(Color.FromArgb(100, Color.Red)),
                    new SolidBrush(Color.FromArgb(100, Color.Green)),
                    new SolidBrush(Color.FromArgb(100, Color.Blue)),
                    new SolidBrush(Color.FromArgb(100, Color.Yellow)),
                    new SolidBrush(Color.FromArgb(100, Color.Purple)),
                    new SolidBrush(Color.FromArgb(100, Color.DarkOrange)),
                    new SolidBrush(Color.FromArgb(100, Color.DarkViolet))
                };

            List<KeyFrame> visibleMarkers = new List<KeyFrame>();

            foreach (var frame in frames)
            {
                float firstX = samplesSpacing * (frame.positions[0] - scrollOffset);
                float lastX = samplesSpacing * (frame.positions[KeyFrame.ChannelCount] - scrollOffset);
                if (lastX >= 0 && firstX <= GraphRectangle.Width)
                {
                    visibleMarkers.Add(frame);
                }
            }

            if (visibleMarkers.Count < 1000)
            {
                foreach (var marker in visibleMarkers)
                {
                    for (int i = 0; i < KeyFrame.ChannelCount; i++)
                    {
                        int startSample = marker.positions[i];
                        int endSample = marker.positions[i + 1];

                        float x1 = samplesSpacing * (startSample - scrollOffset);
                        float x2 = samplesSpacing * (endSample - scrollOffset);
                        float width = samplesSpacing * (endSample - startSample);

                        if (x2 < 0 || x1 > GraphRectangle.Width)
                        {
                            continue;
                        }

                        e.Graphics.FillRectangle(bruhs[i], x1, GraphRectangle.Y, width, rectHeight);

                        if (ZoomLevel > 1.5f)
                        {
                            float bruh = marker.Values[i] * 100;
                            string label = string.Format("index:{0}\n{1:000.##}%", i, bruh);
                            DrawCenteredText(e, label, x1 + width / 2, labelHeight, labelBgBrush);
                        }
                    }
                }
            }
        }

        private void DrawLabels(PaintEventArgs e, int labelCount = 10)
        {
            float labelOffset = maxSamplesVisible / (float)labelCount; //samples between each label

            Pen grayPen = new Pen(Color.FromArgb(150, Color.Gray));

            for (int i = 0; i < labelCount; i++)
            {
                int sampleIndex = (int)(i * labelOffset);
                //if (sampleIndex < actualSamplesVisible)
                {
                    int thisSample = scrollOffset + sampleIndex;
                    string label = thisSample.ToString();
                    if(SampleRate > 0) label += string.Format("\n{0:0.0##}s",(float)thisSample /SampleRate);
                    e.Graphics.DrawLine(grayPen, sampleIndex * samplesSpacing, GraphRectangle.Y, sampleIndex * samplesSpacing, GraphRectangle.Bottom);
                    DrawTextBottom(e, label, sampleIndex * samplesSpacing, GraphRectangle.Bottom);
                }
            }
        }

        private void DrawFrameGraphs(PaintEventArgs e)
        {
            if (frames == null || frames.Count == 0) return;
            frameCurves = new PointF[KeyFrame.ChannelCount][];

            for (int f = 0; f < KeyFrame.ChannelCount; f++)
            {
                List<PointF> visiblePoints = new List<PointF>();
                float top = GraphRectangle.Y + GraphRectangle.Height * ((float)f / frameCurves.Length)   +10;
                float bottom = GraphRectangle.Y + GraphRectangle.Height * ((float)(f + 1) / frameCurves.Length)   -10;

                for (int i = 0; i < frames.Count; i++)
                {
                    int sample = frames[i].positions[0];

                    float xpos = samplesSpacing * (sample - scrollOffset);
                    
                    //if (xpos > 0 && xpos < GraphRectangle.Width)
                    if (xpos > -GraphRectangle.Width / 2 && xpos < GraphRectangle.Width * 1.5)
                    {
                        float val = frames[i].Values[f];
                        float ypos = map(val, 1, 0, top, bottom);

                        var point = new PointF(xpos, ypos);
                        visiblePoints.Add(point);
                    }
                }

                visiblePoints.Add(new PointF(GraphRectangle.Width, bottom));
                visiblePoints.Add(new PointF(0, bottom));

                frameCurves[f] = visiblePoints.ToArray();
            }

            Brush[] bruhs = {
                new SolidBrush(Color.Red),
                new SolidBrush(Color.Green),
                new SolidBrush(Color.Blue),
                new SolidBrush(Color.Yellow),
                new SolidBrush(Color.Purple),
                new SolidBrush(Color.DarkOrange),
                new SolidBrush(Color.DarkViolet)
            };


            for (int f = 0; f < frameCurves.Length; f++)
            {
                if (frameCurves[f].Length > 1)
                    e.Graphics.FillPolygon(bruhs[f], frameCurves[f]);
                    //e.Graphics.DrawCurve(new Pen(bruhs[f], 2), frameCurves[f], 0);
            }
        }

        private void GenerateCurvePoints()
        {
            curvePoints = new PointF[actualSamplesVisible + 1];

            for (int i = 0; i < actualSamplesVisible + 1; i++)
            {
                int sampleid = scrollOffset + i;
                float val = samples[sampleid];
                float ypos = map(val, -1, 1, GraphRectangle.Bottom, GraphRectangle.Y);
                curvePoints[i] = new PointF(samplesSpacing * i, ypos);
            }
        }

        private void DrawGrid(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(baseBackground, GraphRectangle);
            //e.Graphics.Flush();

            Pen pen = new Pen(Color.Black, 1f);

            int divisions = 20;

            for (int i = 0; i < divisions; i++)
            {
                float y = GraphRectangle.Y +  i / (float)divisions * GraphRectangle.Height;
                e.Graphics.DrawLine(pen, GraphRectangle.X, y, GraphRectangle.Width, y);
            }

            Pen treshPen = new Pen(Color.Green, 2f);

            float treshY = GraphRectangle.Y + GraphRectangle.Height * (0.5f - PeakTreshold /2);
            e.Graphics.DrawLine(treshPen, GraphRectangle.X, treshY, GraphRectangle.Width, treshY);


            Pen midPen = new Pen(Color.Red, 2f);

            float midY = GraphRectangle.Y + GraphRectangle.Height * 0.5f;
            e.Graphics.DrawLine(midPen, GraphRectangle.X, midY, GraphRectangle.Width, midY);
        }

        private void DrawTextBottom(PaintEventArgs e, string text, float x, float y)
        {
            RectangleF textRect = new RectangleF();

            SizeF textSize = e.Graphics.MeasureString(text, Font);
            textRect.Width = textSize.Width;
            textRect.Height = textSize.Height;
            textRect.X = x;// (GraphRectangle.Width - textRect.Width) / 2;
            textRect.Y = (y - textRect.Height);

            if (text != null && text.Length > 0)
            {
                e.Graphics.DrawString(text, Font, new SolidBrush(Color.Black), textRect);
            }
        }

        private void DrawCenteredText(PaintEventArgs e, string text, float x, float y, Brush backgroundFillBrush = null)
        {
            RectangleF textRect = new RectangleF();

            SizeF textSize = e.Graphics.MeasureString(text, Font);
            textRect.Width = textSize.Width;
            textRect.Height = textSize.Height;
            textRect.X = x - textRect.Width / 2;
            textRect.Y = y - textRect.Height / 2;

            if (backgroundFillBrush != null)
            {
                e.Graphics.FillRectangle(backgroundFillBrush, textRect);
            }

            if (text != null && text.Length > 0)
            {
                e.Graphics.DrawString(text, Font, new SolidBrush(Color.Black), textRect);
            }
        }

        private void DrawCenteredCircle(PaintEventArgs e, Brush brush, float cx, float cy, float size)
        {
            float x = cx - size / 3;
            float y = cy - size / 3;
            e.Graphics.FillEllipse(brush, x, y, size, size);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (baseBackground != null)
            {
                baseBackground.Dispose();
                baseBackground = null;
            }
        }

        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (baseBackground != null)
            {
                baseBackground.Dispose();
                baseBackground = null;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (baseBackground != null)
            {
                baseBackground.Dispose();
                baseBackground = null;
            }
        }

        public static float map(float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
        public static int map(int x, int in_min, int in_max, int out_min, int out_max)
        {
            return (int)map((float)x, in_min, in_max, out_min, out_max);
        }
    }
}
