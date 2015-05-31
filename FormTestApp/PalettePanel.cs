using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GifPaper
{
    public partial class PalettePanel : UserControl
    {
        public event EventHandler OnChangeValue;
        int colorIdx = 0;
        public Color Color;

        List<String> colors = new List<String> {
            "#000000",
            "#ffffff",
            "#fce94f",
            "#edd400",
            "#c4a000",
            "#fcaf3e",
            "#f57900",
            "#ce5c00",
            "#e9b96e",
            "#c17d11",
            "#8f5902",
            "#8ae234",
            "#73d216",
            "#4e9a06",
            "#729fcf",
            "#3465a4",
            "#204a87",
            "#ad7fa8",
            "#75507b",
            "#5c3566",
            "#ef2929",
            "#cc0000",
            "#a40000",
            "#eeeeec",
            "#d3d7cf",
            "#babdb6",
            "#888a85",
            "#555753",
            "#2e3436",
        };

        public PalettePanel()
        {
            InitializeComponent();
        }

        private void PalettePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var w = this.Width;

            int y = 0;
            foreach (String color in colors)
            {
                var c = System.Drawing.ColorTranslator.FromHtml(color);
                SolidBrush b = new SolidBrush(c);
                g.FillRectangle(b, 0, y, w, 15);
                y += 15;
            }

            g.DrawRectangle(new Pen(Color.Red), 0, colorIdx * 15, w - 1, 14);
        }

        private void PalettePanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Y / 15 < colors.Count)
            {
                colorIdx = e.Y / 15;

                var c = System.Drawing.ColorTranslator.FromHtml(colors[colorIdx]);

                //scribblePanel1.m_pen.Color = c;
                this.Color = c;
            }
            this.Invalidate();

            if (OnChangeValue != null)
            {
                OnChangeValue(sender, e);
            }
        }
    }
}
