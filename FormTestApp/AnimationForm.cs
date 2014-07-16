using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GifPaper
{
    public partial class AnimationForm : Form
    {
        public int AnimationIndex = 0;
        private List<Bitmap> bitmaps;

        public AnimationForm()
        {
            InitializeComponent();
        }

        internal void SetBitmap(List<Bitmap> bitmaps)
        {
            this.bitmaps = bitmaps;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bitmaps == null)
            {
                return;
            }


            if (AnimationIndex > bitmaps.Count - 1)
            {
                AnimationIndex = bitmaps.Count - 1;
            }

            panel1.BackgroundImage = bitmaps[AnimationIndex];
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Invalidate();
            AnimationIndex++;
            if (AnimationIndex == bitmaps.Count)
            {
                AnimationIndex = 0;
            }
        }

        private void AnimationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            e.Cancel = true;
            Hide();
        }
    }
}
