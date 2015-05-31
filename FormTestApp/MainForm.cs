using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using BumpKit;
using System.Threading.Tasks;
using Gif.Components;

namespace GifPaper
{
    public partial class MainForm : Form
    {

        List<Bitmap> bitmaps = new List<Bitmap> { };
        int bitmapsIdx = 0;


        private Bitmap createWhiteBitmap()
        {
            var b = new Bitmap(600, 600);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            return b;
        }


        public MainForm()
        {
            InitializeComponent();

            for (int i = 0; i < 4; i++)
            {
                bitmaps.Add(createWhiteBitmap());
            }
            spritePanel1.bitmaps = bitmaps;
            spritePanel1.Index = 0;

            scribblePanel1.SetBuffer(bitmaps[bitmapsIdx]);
            scribblePanel1.SetUnderlayBuffer(bitmaps[bitmapsIdx]);

            this.FormClosing += new FormClosingEventHandler(TestForm_FormClosing);
            var task = Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(1000);
                scribblePanel1.Initialize();
            });
        }

        private void TestForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            CloseCurrentContext();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bitmaps[bitmapsIdx]);
            g.Clear(Color.White);

            scribblePanel1.Invalidate();
            spritePanel1.Invalidate();
        }


        private void CloseCurrentContext()
        {

            //scribblePanel1.CloseCurrentContext();
        }


        private void ClearDisplay()
        {
            scribblePanel1.Invalidate();
        }


        private void panelPallet_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelPallet_MouseClick(object sender, MouseEventArgs e)
        {


        }



        private void scribblePanel1_MouseUp(object sender, MouseEventArgs e)
        {
            spritePanel1.Invalidate();
        }


        AnimationForm animation = new AnimationForm();

        private void MainForm_Load(object sender, EventArgs e)
        {
            animation.Show();
            animation.SetBitmap(bitmaps);
        }

        private void spritePanel1_Load(object sender, EventArgs e)
        {

        }

        private void spritePanel1_Click(object sender, EventArgs e)
        {

        }

        private void spritePanel1_OnChangeValue(object sender, EventArgs e)
        {
            UpdateIndex();

        }

        private void UpdateIndex()
        {
            spritePanel1.Invalidate();
            int bitmapsIdx = spritePanel1.Index;
            scribblePanel1.SetBuffer(bitmaps[bitmapsIdx]);

            var underlayIdx = bitmapsIdx - 1;
            if (underlayIdx < 0)
            {
                underlayIdx = bitmaps.Count - 1;
            }

            scribblePanel1.SetUnderlayBuffer(bitmaps[underlayIdx]);
            scribblePanel1.Invalidate();
        }

        private void gifSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = "untitled.gif";
            sfd.InitialDirectory = @"C:\";
            sfd.Filter =
                "GIFファイル(*.gif)|*.gif|すべてのファイル(*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveGif(sfd);
            }
        }


        private void SaveGif(SaveFileDialog sfd)
        {
            AnimatedGifEncoder e = new AnimatedGifEncoder();
            e.Start(sfd.FileName);
            e.SetDelay(200);
            //-1:no repeat,0:always repeat
            e.SetRepeat(0);

            foreach (Bitmap bitmap in bitmaps)
            {
                e.AddFrame(bitmap);
            }
            
            e.Finish();
        }

        private void SaveGifBumpKit(SaveFileDialog sfd)
        {
            using (var gif = File.OpenWrite(sfd.FileName))
            {
                using (var encoder = new GifEncoder(gif, 600,600,0))
                {
                    foreach (Bitmap bitmap in bitmaps)
                    {
                        encoder.FrameDelay = TimeSpan.FromMilliseconds(200);
                        encoder.AddFrame(bitmap);
                    }
                }

            }
        }


        private void InsertFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spritePanel1.InsertFrame(createWhiteBitmap());
            UpdateIndex();
        }

        private void RemoveFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spritePanel1.RemoveFrame();
            UpdateIndex();
        }

        private void CopyFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bitmapsIdx = spritePanel1.Index;
            System.Windows.Forms.Clipboard.SetImage(bitmaps[bitmapsIdx]);
            UpdateIndex();
        }

        private void PasteFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bitmapsIdx = spritePanel1.Index;
            bitmaps[bitmapsIdx] = new Bitmap(System.Windows.Forms.Clipboard.GetImage()); ;
            UpdateIndex();

        }

        private void InitTabletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scribblePanel1.Initialize();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void AnimationViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            animation.Show();
        }


        private void PenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenToolStripMenuItem.Checked = false;
            FillToolStripMenuItem.Checked = false;
            PenToolStripMenuItem.Checked = true;
        }

        private void FillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenToolStripMenuItem.Checked = false;
            FillToolStripMenuItem.Checked = false;
            FillToolStripMenuItem.Checked = true;
 
        }

        private void palettePanel1_OnChangeValue(object sender, EventArgs e)
        {
            scribblePanel1.m_pen.Color = palettePanel1.Color;
        }

        private void palettePanel1_Load(object sender, EventArgs e)
        {

        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                OpenGif(ofd);
            }
        }

        private void OpenGif(OpenFileDialog ofd)
        {
            GifDecoder decoder = new GifDecoder();
            decoder.Read(File.OpenRead(ofd.FileName));
            var frame = decoder.GetFrameCount();

            List<Bitmap> loaded = new List<Bitmap> { };
            for (int i = 0; i < frame; i++)
            {
                var image = decoder.GetFrame(i);
                loaded.Add(new Bitmap(image));
            }

            this.bitmaps = loaded;
            spritePanel1.bitmaps = bitmaps;
            spritePanel1.Index = 0;

            scribblePanel1.SetBuffer(bitmaps[bitmapsIdx]);
            scribblePanel1.SetUnderlayBuffer(bitmaps[bitmapsIdx]);
            animation.SetBitmap(bitmaps);
        }
    }
}
