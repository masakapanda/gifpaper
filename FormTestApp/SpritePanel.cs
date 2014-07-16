using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GifPaper
{
    public partial class SpritePanel : UserControl
    {
        public event EventHandler OnChangeValue;
        public List<Bitmap> bitmaps { get; set; }
        public SpritePanel()
        {
            InitializeComponent();
        }

        private void SpritePanel_Load(object sender, EventArgs e)
        {

        }

        private void SpritePanel_Click(object sender, EventArgs e)
        {
                
        }

        private void SpritePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X / 60 < bitmaps.Count)
            {
                Index = e.X / 60;
                if (OnChangeValue != null)
                {
                    OnChangeValue(sender, e);
                }
                Invalidate();
            }
        }

        public int Index { get; set; }
        private Pen frameBorder = new Pen(new SolidBrush(Color.Gray));


        private void SpritePanel_Paint(object sender, PaintEventArgs e)
        {
            int i = 0;

            if (bitmaps != null)
            {
                foreach (Bitmap bitmap in bitmaps)
                {
                    var rect = new Rectangle(i * 60, 0, 60, 60);
                    var rect2 = new Rectangle(i * 60, 0, 59, 59);
                    e.Graphics.DrawImage(bitmap, rect);
                    e.Graphics.DrawRectangle(frameBorder, rect2);
                    i++;
                }

                e.Graphics.DrawRectangle(new Pen(Color.Red), Index * 60, 0, 59, 59);

            }
        }


        internal void InsertFrame(Bitmap bitmap)
        {
            bitmaps.Insert(Index, bitmap);
            Width = bitmaps.Count * 60;
            Invalidate();
        }

        internal void RemoveFrame()
        {
            if (bitmaps.Count == 1)
            {
                System.Windows.Forms.MessageBox.Show("画像が一枚の時は削除できません。");
                return;
            }
            bitmaps.RemoveAt(Index);

            //リストの最後を削除した場合
            if (bitmaps.Count == Index)
            {
                Index = bitmaps.Count - 1;
            }
            Invalidate();
        }
    }
}
