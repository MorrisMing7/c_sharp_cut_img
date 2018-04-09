namespace 切
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.picBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openFileToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomRateTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.cutSizeTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.fileNameTextBox = new System.Windows.Forms.ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.BackColor = System.Drawing.Color.Thistle;
            this.picBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picBox.ImageLocation = "";
            this.picBox.Location = new System.Drawing.Point(223, 46);
            this.picBox.Margin = new System.Windows.Forms.Padding(0);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(531, 347);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            this.picBox.Paint += new System.Windows.Forms.PaintEventHandler(this.picBox_Paint);
            this.picBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseClick);
            this.picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseDown);
            this.picBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseMove);
            this.picBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem2,
            this.zoomRateTextBox,
            this.cutSizeTextBox,
            this.fileNameTextBox});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1116, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openFileToolStripMenuItem2
            // 
            this.openFileToolStripMenuItem2.Name = "openFileToolStripMenuItem2";
            this.openFileToolStripMenuItem2.Size = new System.Drawing.Size(68, 23);
            this.openFileToolStripMenuItem2.Text = "打开文件";
            this.openFileToolStripMenuItem2.Click += new System.EventHandler(this.openFileToolStripMenuItem2_Click);
            // 
            // zoomRateTextBox
            // 
            this.zoomRateTextBox.Name = "zoomRateTextBox";
            this.zoomRateTextBox.ReadOnly = true;
            this.zoomRateTextBox.Size = new System.Drawing.Size(320, 23);
            // 
            // cutSizeTextBox
            // 
            this.cutSizeTextBox.Name = "cutSizeTextBox";
            this.cutSizeTextBox.Size = new System.Drawing.Size(110, 23);
            this.cutSizeTextBox.Text = "Input cutsize here";
            this.cutSizeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cutSizeTextBox_KeyDown);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.ReadOnly = true;
            this.fileNameTextBox.Size = new System.Drawing.Size(600, 23);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1116, 761);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.picBox);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox zoomRateTextBox;
        private System.Windows.Forms.ToolStripTextBox cutSizeTextBox;
        private System.Windows.Forms.ToolStripTextBox fileNameTextBox;
    }
}

