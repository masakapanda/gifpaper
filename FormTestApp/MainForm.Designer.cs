namespace GifPaper
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gifSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnimationViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonInsertFrame = new System.Windows.Forms.Button();
            this.buttonRemoveFrame = new System.Windows.Forms.Button();
            this.palettePanel1 = new GifPaper.PalettePanel();
            this.spritePanel1 = new GifPaper.SpritePanel();
            this.scribblePanel1 = new GifPaper.ScribblePanel();
            this.OpenFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ToolToolStripMenuItem,
            this.FrameToolStripMenuItem,
            this.WindowToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(710, 26);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gifSaveToolStripMenuItem,
            this.OpenFileToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.FileToolStripMenuItem.Text = "ファイル";
            // 
            // gifSaveToolStripMenuItem
            // 
            this.gifSaveToolStripMenuItem.Name = "gifSaveToolStripMenuItem";
            this.gifSaveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gifSaveToolStripMenuItem.Text = "GIF保存";
            this.gifSaveToolStripMenuItem.Click += new System.EventHandler(this.gifSaveToolStripMenuItem_Click);
            // 
            // ToolToolStripMenuItem
            // 
            this.ToolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PenToolStripMenuItem,
            this.FillToolStripMenuItem});
            this.ToolToolStripMenuItem.Name = "ToolToolStripMenuItem";
            this.ToolToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
            this.ToolToolStripMenuItem.Text = "ツール";
            // 
            // PenToolStripMenuItem
            // 
            this.PenToolStripMenuItem.Checked = true;
            this.PenToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PenToolStripMenuItem.Name = "PenToolStripMenuItem";
            this.PenToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.PenToolStripMenuItem.Text = "ペン";
            this.PenToolStripMenuItem.Click += new System.EventHandler(this.PenToolStripMenuItem_Click);
            // 
            // FillToolStripMenuItem
            // 
            this.FillToolStripMenuItem.Name = "FillToolStripMenuItem";
            this.FillToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.FillToolStripMenuItem.Text = "塗りつぶし";
            this.FillToolStripMenuItem.Click += new System.EventHandler(this.FillToolStripMenuItem_Click);
            // 
            // FrameToolStripMenuItem
            // 
            this.FrameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InsertFrameToolStripMenuItem,
            this.RemoveFrameToolStripMenuItem,
            this.CopyFrameToolStripMenuItem,
            this.PasteFrameToolStripMenuItem});
            this.FrameToolStripMenuItem.Name = "FrameToolStripMenuItem";
            this.FrameToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
            this.FrameToolStripMenuItem.Text = "フレーム";
            // 
            // InsertFrameToolStripMenuItem
            // 
            this.InsertFrameToolStripMenuItem.Name = "InsertFrameToolStripMenuItem";
            this.InsertFrameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.InsertFrameToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.InsertFrameToolStripMenuItem.Text = "空白フレームを挿入";
            this.InsertFrameToolStripMenuItem.Click += new System.EventHandler(this.InsertFrameToolStripMenuItem_Click);
            // 
            // RemoveFrameToolStripMenuItem
            // 
            this.RemoveFrameToolStripMenuItem.Name = "RemoveFrameToolStripMenuItem";
            this.RemoveFrameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.RemoveFrameToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.RemoveFrameToolStripMenuItem.Text = "削除";
            this.RemoveFrameToolStripMenuItem.Click += new System.EventHandler(this.RemoveFrameToolStripMenuItem_Click);
            // 
            // CopyFrameToolStripMenuItem
            // 
            this.CopyFrameToolStripMenuItem.Name = "CopyFrameToolStripMenuItem";
            this.CopyFrameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyFrameToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.CopyFrameToolStripMenuItem.Text = "コピー";
            this.CopyFrameToolStripMenuItem.Click += new System.EventHandler(this.CopyFrameToolStripMenuItem_Click);
            // 
            // PasteFrameToolStripMenuItem
            // 
            this.PasteFrameToolStripMenuItem.Name = "PasteFrameToolStripMenuItem";
            this.PasteFrameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteFrameToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.PasteFrameToolStripMenuItem.Text = "ペースト";
            this.PasteFrameToolStripMenuItem.Click += new System.EventHandler(this.PasteFrameToolStripMenuItem_Click);
            // 
            // WindowToolStripMenuItem
            // 
            this.WindowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AnimationViewToolStripMenuItem});
            this.WindowToolStripMenuItem.Name = "WindowToolStripMenuItem";
            this.WindowToolStripMenuItem.Size = new System.Drawing.Size(80, 22);
            this.WindowToolStripMenuItem.Text = "ウィンドウ";
            // 
            // AnimationViewToolStripMenuItem
            // 
            this.AnimationViewToolStripMenuItem.Name = "AnimationViewToolStripMenuItem";
            this.AnimationViewToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.AnimationViewToolStripMenuItem.Text = "アニメ表示";
            this.AnimationViewToolStripMenuItem.Click += new System.EventHandler(this.AnimationViewToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
            this.HelpToolStripMenuItem.Text = "ヘルプ";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.spritePanel1);
            this.panel1.Location = new System.Drawing.Point(42, 629);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(573, 80);
            this.panel1.TabIndex = 8;
            // 
            // buttonInsertFrame
            // 
            this.buttonInsertFrame.Location = new System.Drawing.Point(12, 629);
            this.buttonInsertFrame.Name = "buttonInsertFrame";
            this.buttonInsertFrame.Size = new System.Drawing.Size(24, 21);
            this.buttonInsertFrame.TabIndex = 10;
            this.buttonInsertFrame.Text = "+";
            this.buttonInsertFrame.UseVisualStyleBackColor = true;
            this.buttonInsertFrame.Click += new System.EventHandler(this.InsertFrameToolStripMenuItem_Click);
            // 
            // buttonRemoveFrame
            // 
            this.buttonRemoveFrame.Location = new System.Drawing.Point(12, 656);
            this.buttonRemoveFrame.Name = "buttonRemoveFrame";
            this.buttonRemoveFrame.Size = new System.Drawing.Size(24, 21);
            this.buttonRemoveFrame.TabIndex = 11;
            this.buttonRemoveFrame.Text = "-";
            this.buttonRemoveFrame.UseVisualStyleBackColor = true;
            this.buttonRemoveFrame.Click += new System.EventHandler(this.RemoveFrameToolStripMenuItem_Click);
            // 
            // palettePanel1
            // 
            this.palettePanel1.Location = new System.Drawing.Point(618, 35);
            this.palettePanel1.Name = "palettePanel1";
            this.palettePanel1.Size = new System.Drawing.Size(80, 555);
            this.palettePanel1.TabIndex = 12;
            this.palettePanel1.OnChangeValue += new System.EventHandler(this.palettePanel1_OnChangeValue);
            this.palettePanel1.Load += new System.EventHandler(this.palettePanel1_Load);
            // 
            // spritePanel1
            // 
            this.spritePanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.spritePanel1.bitmaps = null;
            this.spritePanel1.Index = 0;
            this.spritePanel1.Location = new System.Drawing.Point(0, 3);
            this.spritePanel1.Name = "spritePanel1";
            this.spritePanel1.Size = new System.Drawing.Size(570, 60);
            this.spritePanel1.TabIndex = 7;
            this.spritePanel1.OnChangeValue += new System.EventHandler(this.spritePanel1_OnChangeValue);
            this.spritePanel1.Load += new System.EventHandler(this.spritePanel1_Load);
            this.spritePanel1.Click += new System.EventHandler(this.spritePanel1_Click);
            // 
            // scribblePanel1
            // 
            this.scribblePanel1.Location = new System.Drawing.Point(12, 35);
            this.scribblePanel1.Name = "scribblePanel1";
            this.scribblePanel1.Size = new System.Drawing.Size(600, 588);
            this.scribblePanel1.TabIndex = 0;
            this.scribblePanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scribblePanel1_MouseUp);
            // 
            // OpenFileToolStripMenuItem
            // 
            this.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem";
            this.OpenFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.OpenFileToolStripMenuItem.Text = "開く";
            this.OpenFileToolStripMenuItem.Click += new System.EventHandler(this.OpenFileToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(710, 721);
            this.Controls.Add(this.palettePanel1);
            this.Controls.Add(this.buttonRemoveFrame);
            this.Controls.Add(this.buttonInsertFrame);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.scribblePanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "GifPaper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private ScribblePanel scribblePanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private SpritePanel spritePanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem gifSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.Button buttonInsertFrame;
        private System.Windows.Forms.Button buttonRemoveFrame;
        private System.Windows.Forms.ToolStripMenuItem WindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnimationViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FillToolStripMenuItem;
        private PalettePanel palettePanel1;
        private System.Windows.Forms.ToolStripMenuItem OpenFileToolStripMenuItem;
    }
}

