
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Ink;
using Microsoft.Ink;

namespace GifPaper
{
    public partial class ScribblePanel : UserControl
    {
        private Point m_lastPoint = Point.Empty;
        private Graphics m_graphics;
        public Pen m_pen;
        private Pen m_backPen;
        Bitmap buffer =new Bitmap(600, 600);
        Bitmap bufferUnderlay;
        bool drawing = false;
        InkOverlay ink = new InkOverlay();

        public void SetColor(Color c)
        {
            ink.DefaultDrawingAttributes.Color = c;
        }

        public event EventHandler OnDrawStroke;


        public void SetBuffer(Bitmap b){
            buffer = b;
            InitCanvas();

            this.Width = b.Width;
            this.Height = b.Height;
        }

        public ScribblePanel()
        {
            InitializeComponent();
            ink.AttachedControl = this;
            ink.AttachMode = InkOverlayAttachMode.InFront;
            ink.Enabled = true;
            ink.Stroke += Ink_Stroke;
            ink.EraserMode = InkOverlayEraserMode.PointErase;
            ink.DefaultDrawingAttributes.AntiAliased = false;
        }

        private void Ink_Stroke(object sender, InkCollectorStrokeEventArgs e)
        {
            ink.Renderer.Draw(buffer, ink.Ink.Strokes);
            ink.Enabled = false;
            ink.Ink = new Microsoft.Ink.Ink();
            ink.Enabled = true;
            this.Invalidate();

            this.OnDrawStroke(sender, e);
        }

        public void Initialize()
        {
            ClearDisplay();
            Enable_Scribble(true);
        }


        private void ScribblePanel_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        ///////////////////////////////////////////////////////////////////////
        private void TestForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
        }


        ///////////////////////////////////////////////////////////////////////
        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearDisplay();
        }



        private void InitCanvas()
        {
            //FIXME 素早く操作すると、ここでbufferがすでに使われているとエラーが出て落ちる
            m_graphics = Graphics.FromImage(buffer);
            m_graphics.DrawImageUnscaled(buffer, Point.Empty);

            m_graphics.SmoothingMode = SmoothingMode.None;
            this.Width = buffer.Width;
            this.Height = buffer.Height;

        }


        ///////////////////////////////////////////////////////////////////////
        private void Enable_Scribble(bool enable = false)
        {

                InitCanvas();

                m_pen = new Pen(Color.Black);
                m_backPen = new Pen(Color.White);
                m_backPen.Width = 30;



        }



        private void ClearDisplay()
        {
            InitCanvas();
            this.Invalidate();
        }


        private void scribblePanel_Resize(object sender, EventArgs e)
        {

        }

        private void ScribblePanel_Paint(object sender, PaintEventArgs e)
        {

            if (bufferUnderlay != null)
            {
                e.Graphics.DrawImageUnscaled(bufferUnderlay, Point.Empty);
            }

            //for underlay
            ColorMatrix cm = new ColorMatrix();
            cm.Matrix33 = 0.90f;
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            e.Graphics.DrawImage(buffer, new Rectangle(0, 0, 600, 600), 0, 0, 600, 600, GraphicsUnit.Pixel, ia);
        }


        internal void SetUnderlayBuffer(Bitmap bitmap)
        {
            bufferUnderlay = bitmap;
            InitCanvas();
        }

        //Flood Fill by
        //http://rosettacode.org/wiki/Bitmap/Flood_fill#C.23
        private static bool ColorMatch(Color a, Color b)
        {
            return (a.ToArgb() & 0xffffff) == (b.ToArgb() & 0xffffff);
        }

        static void FloodFill(Bitmap bmp, Point pt, Color targetColor, Color replacementColor)
        {
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(pt);
            while (q.Count > 0)
            {
                Point n = q.Dequeue();
                if (!ColorMatch(bmp.GetPixel(n.X, n.Y), targetColor))
                    continue;
                Point w = n, e = new Point(n.X + 1, n.Y);
                while ((w.X > 0) && ColorMatch(bmp.GetPixel(w.X, w.Y), targetColor))
                {
                    bmp.SetPixel(w.X, w.Y, replacementColor);
                    if ((w.Y > 0) && ColorMatch(bmp.GetPixel(w.X, w.Y - 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y - 1));
                    if ((w.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(w.X, w.Y + 1), targetColor))
                        q.Enqueue(new Point(w.X, w.Y + 1));
                    w.X--;
                }
                while ((e.X < bmp.Width - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y), targetColor))
                {
                    bmp.SetPixel(e.X, e.Y, replacementColor);
                    if ((e.Y > 0) && ColorMatch(bmp.GetPixel(e.X, e.Y - 1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y - 1));
                    if ((e.Y < bmp.Height - 1) && ColorMatch(bmp.GetPixel(e.X, e.Y + 1), targetColor))
                        q.Enqueue(new Point(e.X, e.Y + 1));
                    e.X++;
                }
            }
        }

        private void ScribblePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                m_pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                m_pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Flat);
                m_graphics.DrawLine(m_pen, e.Location, m_lastPoint);
                this.Invalidate();
            }
            m_lastPoint = e.Location;
        }

        private void ScribblePanel_MouseDown(object sender, MouseEventArgs e)
        {
            drawing = true;
        }

        private void ScribblePanel_MouseUp(object sender, MouseEventArgs e)
        {
            m_graphics.DrawLine(m_pen, e.Location, m_lastPoint);
            this.Invalidate();
            drawing = false;

        }

        private void inkPicture1_Stroke(object sender, InkCollectorStrokeEventArgs e)
        {

            /*
            inkPicture1.Renderer.Draw(buffer, inkPicture1.Ink.Strokes);
            this.Invalidate();
            inkPicture1.InkEnabled = false;
            inkPicture1.Ink = new Microsoft.Ink.Ink();
            inkPicture1.InkEnabled = true;

            inkPicture1.
            inkPicture1.DrawImage(buffer, new Point(0, 0));
            inkPicture1.Invalidate();
            */
            /*
            Graphics graphicsSrc = inkPicture1.CreateGraphics();
            graphicsSrc.DrawImage(m_graphics, new Point(0,0));
            /*
            */
        }

        private void inkPicture1_Painted(object sender, PaintEventArgs e)
        {
        }
    }
}
