namespace FormOCR
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_OpenFile = new System.Windows.Forms.ToolStripButton();
            this.pictureBox_Image = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_WhiteList = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_IndexSelect = new System.Windows.Forms.ToolStripTextBox();
            this.pictureBox_SelectedCellImage = new System.Windows.Forms.PictureBox();
            this.label_CellIndex = new System.Windows.Forms.Label();
            this.textBox_CellText = new System.Windows.Forms.TextBox();
            this.toolStripComboBox_RunMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton_SaveFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SelectedCellImage)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(855, 403);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(855, 453);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_OpenFile,
            this.toolStripComboBox_RunMode,
            this.toolStripButton_SaveFile});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(236, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripButton_OpenFile
            // 
            this.toolStripButton_OpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_OpenFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_OpenFile.Image")));
            this.toolStripButton_OpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_OpenFile.Name = "toolStripButton_OpenFile";
            this.toolStripButton_OpenFile.Size = new System.Drawing.Size(58, 22);
            this.toolStripButton_OpenFile.Text = "OpenFile";
            this.toolStripButton_OpenFile.Click += new System.EventHandler(this.toolStripButton_OpenFile_Click);
            // 
            // pictureBox_Image
            // 
            this.pictureBox_Image.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Image.Name = "pictureBox_Image";
            this.pictureBox_Image.Size = new System.Drawing.Size(384, 320);
            this.pictureBox_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_Image.TabIndex = 0;
            this.pictureBox_Image.TabStop = false;
            this.pictureBox_Image.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Image_MouseUp);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox_Image);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.textBox_CellText);
            this.splitContainer1.Panel2.Controls.Add(this.label_CellIndex);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox_SelectedCellImage);
            this.splitContainer1.Size = new System.Drawing.Size(855, 403);
            this.splitContainer1.SplitterDistance = 666;
            this.splitContainer1.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBox_WhiteList,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolStripTextBox_IndexSelect});
            this.toolStrip2.Location = new System.Drawing.Point(3, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(757, 25);
            this.toolStrip2.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(62, 22);
            this.toolStripLabel1.Text = "WhiteList :";
            // 
            // toolStripTextBox_WhiteList
            // 
            this.toolStripTextBox_WhiteList.Name = "toolStripTextBox_WhiteList";
            this.toolStripTextBox_WhiteList.Size = new System.Drawing.Size(300, 25);
            this.toolStripTextBox_WhiteList.Text = "0123456789";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(73, 22);
            this.toolStripLabel2.Text = "IndexSelect :";
            // 
            // toolStripTextBox_IndexSelect
            // 
            this.toolStripTextBox_IndexSelect.Name = "toolStripTextBox_IndexSelect";
            this.toolStripTextBox_IndexSelect.Size = new System.Drawing.Size(300, 25);
            // 
            // pictureBox_SelectedCellImage
            // 
            this.pictureBox_SelectedCellImage.Location = new System.Drawing.Point(3, 147);
            this.pictureBox_SelectedCellImage.Name = "pictureBox_SelectedCellImage";
            this.pictureBox_SelectedCellImage.Size = new System.Drawing.Size(100, 50);
            this.pictureBox_SelectedCellImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_SelectedCellImage.TabIndex = 0;
            this.pictureBox_SelectedCellImage.TabStop = false;
            // 
            // label_CellIndex
            // 
            this.label_CellIndex.AutoSize = true;
            this.label_CellIndex.Location = new System.Drawing.Point(3, 10);
            this.label_CellIndex.Name = "label_CellIndex";
            this.label_CellIndex.Size = new System.Drawing.Size(35, 12);
            this.label_CellIndex.TabIndex = 1;
            this.label_CellIndex.Text = "label1";
            // 
            // textBox_CellText
            // 
            this.textBox_CellText.Location = new System.Drawing.Point(5, 25);
            this.textBox_CellText.Multiline = true;
            this.textBox_CellText.Name = "textBox_CellText";
            this.textBox_CellText.Size = new System.Drawing.Size(168, 116);
            this.textBox_CellText.TabIndex = 2;
            this.textBox_CellText.TextChanged += new System.EventHandler(this.textBox_CellText_TextChanged);
            // 
            // toolStripComboBox_RunMode
            // 
            this.toolStripComboBox_RunMode.AutoSize = false;
            this.toolStripComboBox_RunMode.Items.AddRange(new object[] {
            "Check",
            "Run"});
            this.toolStripComboBox_RunMode.Name = "toolStripComboBox_RunMode";
            this.toolStripComboBox_RunMode.Size = new System.Drawing.Size(80, 25);
            this.toolStripComboBox_RunMode.Text = "Check";
            // 
            // toolStripButton_SaveFile
            // 
            this.toolStripButton_SaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_SaveFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_SaveFile.Image")));
            this.toolStripButton_SaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_SaveFile.Name = "toolStripButton_SaveFile";
            this.toolStripButton_SaveFile.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_SaveFile.Text = "SaveFile";
            this.toolStripButton_SaveFile.Click += new System.EventHandler(this.toolStripButton_SaveFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 453);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "Form1";
            this.Text = "FormOCR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SelectedCellImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_OpenFile;
        private System.Windows.Forms.PictureBox pictureBox_Image;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_WhiteList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_IndexSelect;
        private System.Windows.Forms.PictureBox pictureBox_SelectedCellImage;
        private System.Windows.Forms.TextBox textBox_CellText;
        private System.Windows.Forms.Label label_CellIndex;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_RunMode;
        private System.Windows.Forms.ToolStripButton toolStripButton_SaveFile;
    }
}

