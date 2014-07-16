namespace GifPaper
{
    partial class SpritePanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SpritePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "SpritePanel";
            this.Size = new System.Drawing.Size(60, 60);
            this.Load += new System.EventHandler(this.SpritePanel_Load);
            this.Click += new System.EventHandler(this.SpritePanel_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SpritePanel_Paint);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SpritePanel_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
