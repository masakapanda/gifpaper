///////////////////////////////////////////////////////////////////////////////
// TestForm.cs - Windows Forms test dialog for WintabDN
//
// Copyright (c) 2010, Wacom Technology Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WintabDN;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace GifPaper
{
    public partial class ScribblePanel : UserControl
    {
        private CWintabContext m_logContext = null;
        private CWintabData m_wtData = null;
        private UInt32 m_maxPkts = 1;   // max num pkts to capture/display at a time

        private Int32 m_pkX = 0;
        private Int32 m_pkY = 0;
        private UInt32 m_pressure = 0;
        private UInt32 m_pkTime = 0;
        private UInt32 m_pkTimeLast = 0;

        private Point m_lastPoint = Point.Empty;
        private Graphics m_graphics;
        public Pen m_pen;
        private Pen m_backPen;
        Bitmap buffer =new Bitmap(600, 600);
        Bitmap bufferUnderlay;



        // These constants can be used to force Wintab X/Y data to map into a
        // a 10000 x 10000 grid, as an example of mapping tablet data to values
        // that make sense for your application.
        private const Int32 m_TABEXTX = 10000;
        private const Int32 m_TABEXTY = 10000;

        public void SetBuffer(Bitmap b){
            buffer = b;
            InitCanvas();
        }

        public ScribblePanel()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            ClearDisplay();
            Enable_Scribble(true);

            // Control the system cursor with the pen.
            // TODO: set to false to NOT control the system cursor with pen.
            bool controlSystemCursor = true;

            // Open a context and try to capture pen data;
            InitDataCapture(m_TABEXTX, m_TABEXTY, controlSystemCursor);

        }


        private void ScribblePanel_Load(object sender, EventArgs e)
        {
            Initialize();
        }


        ///////////////////////////////////////////////////////////////////////
        public HCTX HLogContext { get { return m_logContext.HCtx; } }

        ///////////////////////////////////////////////////////////////////////
        private void TestForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            CloseCurrentContext();
        }


        ///////////////////////////////////////////////////////////////////////
        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearDisplay();
        }

        ///////////////////////////////////////////////////////////////////////
        private CWintabContext OpenTestDigitizerContext(
            int width_I = m_TABEXTX, int height_I = m_TABEXTY, bool ctrlSysCursor = true)
        {
            bool status = false;
            CWintabContext logContext = null;

            try
            {
                // Get the default digitizing context.
                // Default is to receive data events.
                logContext = CWintabInfo.GetDefaultDigitizingContext(ECTXOptionValues.CXO_MESSAGES);

                // Set system cursor if caller wants it.
                if (ctrlSysCursor)
                {
                    logContext.Options |= (uint)ECTXOptionValues.CXO_SYSTEM;
                }

                if (logContext == null)
                {
                    TraceMsg("FAILED to get default digitizing context.\n");
                    return null;
                }

                // Modify the digitizing region.
                logContext.Name = "WintabDN Event Data Context";

                // output in a grid of the specified dimensions.
                logContext.OutOrgX = logContext.OutOrgY = 0;
                logContext.OutExtX = width_I;
                logContext.OutExtY = height_I;


                // Open the context, which will also tell Wintab to send data packets.
                status = logContext.Open();

                TraceMsg("Context Open: " + (status ? "PASSED [ctx=" + logContext.HCtx + "]" : "FAILED") + "\n");
            }
            catch (Exception ex)
            {
                TraceMsg("OpenTestDigitizerContext ERROR: " + ex.ToString());
            }

            return logContext;
        }

        ///////////////////////////////////////////////////////////////////////
        private CWintabContext OpenTestSystemContext(
            int width_I = m_TABEXTX, int height_I = m_TABEXTY, bool ctrlSysCursor = true)
        {
            bool status = false;
            CWintabContext logContext = null;

            try
            {
                logContext = CWintabInfo.GetDefaultSystemContext(ECTXOptionValues.CXO_MESSAGES);

                // Set system cursor if caller wants it.
                if (ctrlSysCursor)
                {
                    logContext.Options |= (uint)ECTXOptionValues.CXO_SYSTEM;
                }
                else
                {
                    logContext.Options &= ~(uint)ECTXOptionValues.CXO_SYSTEM;
                }

                if (logContext == null)
                {
                    TraceMsg("FAILED to get default digitizing context.\n");
                    return null;
                }

                logContext.Name = "WintabDN Event Data Context";

                WintabAxis tabletX = CWintabInfo.GetTabletAxis(EAxisDimension.AXIS_X);
                WintabAxis tabletY = CWintabInfo.GetTabletAxis(EAxisDimension.AXIS_Y);

                logContext.InOrgX = 0;
                logContext.InOrgY = 0;
                logContext.InExtX = tabletX.axMax;
                logContext.InExtY = tabletY.axMax;

                SetSystemExtents(ref logContext);
                status = logContext.Open();

                TraceMsg("Context Open: " + (status ? "PASSED [ctx=" + logContext.HCtx + "]" : "FAILED") + "\n");
            }
            catch (Exception ex)
            {
                TraceMsg("OpenTestDigitizerContext ERROR: " + ex.ToString());
            }

            return logContext;
        }


        private void InitCanvas()
        {
            m_graphics = Graphics.FromImage(buffer);
            m_graphics.DrawImageUnscaled(buffer, Point.Empty);

            m_graphics.SmoothingMode = SmoothingMode.None;

        }


        ///////////////////////////////////////////////////////////////////////
        private void Enable_Scribble(bool enable = false)
        {
            if (enable)
            {
                // Set up to capture 1 packet at a time.
                m_maxPkts = 1;

                InitCanvas();

                m_pen = new Pen(Color.Black);
                m_backPen = new Pen(Color.White);
                m_backPen.Width = 30;


                // You should now be able to scribble in the scribblePanel.
            }
            else
            {
                // Remove scribble context.
                CloseCurrentContext();

                // Turn off graphics.
                if (m_graphics != null)
                {
                    this.Invalidate();
                    m_graphics = null;
                }

            }
        }


        ///////////////////////////////////////////////////////////////////////
        // Helper functions
        //

        ///////////////////////////////////////////////////////////////////////
        private void InitDataCapture(
                int ctxWidth_I = m_TABEXTX, int ctxHeight_I = m_TABEXTY, bool ctrlSysCursor_I = true)
        {
            try
            {
                // Close context from any previous test.
                CloseCurrentContext();

                TraceMsg("Opening context...\n");

                m_logContext = OpenTestSystemContext(ctxWidth_I, ctxHeight_I, ctrlSysCursor_I);

                if (m_logContext == null)
                {
                    TraceMsg("Test_DataPacketQueueSize: FAILED OpenTestSystemContext - bailing out...\n");
                    return;
                }

                // Create a data object and set its WT_PACKET handler.
                m_wtData = new CWintabData(m_logContext);
                m_wtData.SetWTPacketEventHandler(MyWTPacketEventHandler);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        ///////////////////////////////////////////////////////////////////////
        public void CloseCurrentContext()
        {
            try
            {
                TraceMsg("Closing context...\n");
                if (m_logContext != null)
                {
                    m_logContext.Close();
                    m_logContext = null;
                    m_wtData = null;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        ///////////////////////////////////////////////////////////////////////
        void TraceMsg(string msg)
        {
            //System.Console.WriteLine(msg);
        }

        ///////////////////////////////////////////////////////////////////////
        // Sets logContext.Out
        //
        // Note: 
        // SystemParameters.VirtualScreenLeft{Top} and SystemParameters.VirtualScreenWidth{Height} 
        // don't always give correct answers.
        //
        // Uncomment the TODO code below that enumerates all system displays 
        // if you want to customize.
        // Else assume the passed-in extents were already set by call to WTInfo,
        // in which case we still have to invert the Y extent.
        private void SetSystemExtents(ref CWintabContext logContext)
        {
            // In Wintab, the tablet origin is lower left.  Move origin to upper left
            // so that it coincides with screen origin.
            logContext.OutExtY = -logContext.OutExtY;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Called when Wintab WT_PACKET events are received.
        /// </summary>
        /// <param name="sender_I">The EventMessage object sending the report.</param>
        /// <param name="eventArgs_I">eventArgs_I.Message.WParam contains ID of packet containing the data.</param>
        public void MyWTPacketEventHandler(Object sender_I, MessageReceivedEventArgs eventArgs_I)
        {
            if (m_wtData == null)
            {
                return;
            }

            try
            {
                if (m_maxPkts == 1)
                {
                    uint pktID = (uint)eventArgs_I.Message.WParam;
                    WintabPacket pkt = m_wtData.GetDataPacket((uint)eventArgs_I.Message.LParam, pktID);
                    if (pkt.pkContext != 0)
                    {
                        m_pkX = pkt.pkX;
                        m_pkY = pkt.pkY;
                        m_pressure = pkt.pkNormalPressure;

                        //Trace.WriteLine("SCREEN: pkX: " + pkt.pkX + ", pkY:" + pkt.pkY + ", pressure: " + pkt.pkNormalPressure);

                        m_pkTime = pkt.pkTime;

                        if (m_graphics == null)
                        {
                            // display data mode
                            TraceMsg("Received WT_PACKET event[" + pktID + "]: X/Y/P = " +
                                pkt.pkX + " / " + pkt.pkY + " / " + pkt.pkNormalPressure + "\n");
                        }
                        else
                        {
                            // scribble mode
                            int clientWidth = this.Width;
                            int clientHeight = this.Height;

                            // m_pkX and m_pkY are in screen (system) coordinates.

                            Point clientPoint = this.PointToClient(new Point(m_pkX, m_pkY));
                            //Trace.WriteLine("CLIENT:   X: " + clientPoint.X + ", Y:" + clientPoint.Y);

                            if (m_lastPoint.Equals(Point.Empty))
                            {
                                m_lastPoint = clientPoint;
                                m_pkTimeLast = m_pkTime;
                            }

                            m_pen.Width = (float)(m_pressure / 200);

                            m_pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                            m_pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Flat);
                                //PenLineJoin.Round;
                            //m_pen.EndLineCap = PenLineCap.Round;

                            m_backPen.Width = (float)(m_pressure / 100);
                            m_backPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                            m_backPen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Flat);

                            if (m_pressure > 0)
                            {
                                float x = (int)(clientPoint.X - m_pen.Width / 2);
                                float y = (int)(clientPoint.Y - m_pen.Width/2);
                                float w = (int)(m_pen.Width);
                                

                                /*
                                m_graphics.FillEllipse(new SolidBrush(Color.Black), new Rectangle(x, y, w,w));
                                */
                                /*
                                if (m_pkTime - m_pkTimeLast < 5)
                                {
                                    m_graphics.DrawRectangle(m_pen, clientPoint.X, clientPoint.Y, 1, 1);
                                }
                                else
                                {
                                    m_graphics.DrawLine(m_pen, clientPoint, m_lastPoint);
                                }
                                 */

                                //Pen
                                if (pkt.pkStatus == 0)
                                {
                                    m_graphics.DrawLine(m_pen, clientPoint, m_lastPoint);

                                }

                                //Eraser
                                if (pkt.pkStatus == 16)
                                {
                                    m_graphics.DrawLine(m_backPen, clientPoint, m_lastPoint);

                                }

                                this.Invalidate();

                            }

                            m_lastPoint = clientPoint;
                            m_pkTimeLast = m_pkTime;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //throw new Exception("FAILED to get packet data: " + ex.ToString());
            }
        }

        private void ClearDisplay()
        {
            InitCanvas();
            //m_graphics.Clear(Color.White);
            this.Invalidate();
        }


        private void scribblePanel_Resize(object sender, EventArgs e)
        {
            /*
            if (m_graphics != null)
            {
                m_graphics.Dispose();
                m_graphics = this.CreateGraphics();
                m_graphics.SmoothingMode = SmoothingMode.AntiAlias;
              Trace.WriteLine(
                  "ScribblePanel: X:" + scribblePanel.Left + ",Y:" +  scribblePanel.Top + 
                  ", W:" + scribblePanel.Width + ", H:" + scribblePanel.Height);
            }
                 */
        }

        private void ScribblePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            if (bufferUnderlay != null)
            {
                e.Graphics.DrawImageUnscaled(bufferUnderlay, Point.Empty);
            }

            //for underlay
            ColorMatrix cm = new ColorMatrix();
            cm.Matrix33 = 0.90f;
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            //e.Graphics.DrawImageUnscaled(buffer, Point.Empty);
            //e.Graphics.DrawImage(buffer,new Rectangle(0,0,600,600),GraphicsUnit.Pixel, cm);
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
 
    }
}
