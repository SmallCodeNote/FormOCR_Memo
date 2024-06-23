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
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_WhiteList = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_IndexSelect = new System.Windows.Forms.ToolStripTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox_Image = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox_SelectedCellImage = new System.Windows.Forms.PictureBox();
            this.textBox_CellText = new System.Windows.Forms.TextBox();
            this.label_CellIndex = new System.Windows.Forms.Label();
            this.panel_Frame2 = new System.Windows.Forms.Panel();
            this.trackBar_CellAreaMin = new System.Windows.Forms.TrackBar();
            this.label_CellAreaMin = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar_HoughRangeD = new System.Windows.Forms.TrackBar();
            this.label_HoughRangeD = new System.Windows.Forms.Label();
            this.trackBar_HoughMaxLineGap = new System.Windows.Forms.TrackBar();
            this.label_HoughMaxLineGap = new System.Windows.Forms.Label();
            this.trackBar_HoughMinLineLength = new System.Windows.Forms.TrackBar();
            this.label_HoughMinLineLength = new System.Windows.Forms.Label();
            this.trackBar_HoughThreshold = new System.Windows.Forms.TrackBar();
            this.label_HoughThreshold = new System.Windows.Forms.Label();
            this.trackBar_HoughTheta = new System.Windows.Forms.TrackBar();
            this.label_HoughTheta = new System.Windows.Forms.Label();
            this.trackBar_HoughRho = new System.Windows.Forms.TrackBar();
            this.label_HoughRho = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_OpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox_RunMode = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox_ProcessImageSelector = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox_PageSelector = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel_PageMax = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_SaveFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_MousePosition = new System.Windows.Forms.ToolStripLabel();
            this.trackBar_CellAreaMax = new System.Windows.Forms.TrackBar();
            this.label_CellAreaMax = new System.Windows.Forms.Label();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SelectedCellImage)).BeginInit();
            this.panel_Frame2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CellAreaMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughRangeD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughMaxLineGap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughMinLineLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughTheta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughRho)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CellAreaMax)).BeginInit();
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
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(946, 874);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(946, 924);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBox_WhiteList,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolStripTextBox_IndexSelect,
            this.toolStripLabel_MousePosition});
            this.toolStrip2.Location = new System.Drawing.Point(3, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(857, 25);
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
            this.toolStripTextBox_WhiteList.TextChanged += new System.EventHandler(this.toolStripTextBox_WhiteList_TextChanged);
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
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(946, 874);
            this.splitContainer1.SplitterDistance = 736;
            this.splitContainer1.TabIndex = 1;
            // 
            // pictureBox_Image
            // 
            this.pictureBox_Image.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Image.Name = "pictureBox_Image";
            this.pictureBox_Image.Size = new System.Drawing.Size(384, 320);
            this.pictureBox_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_Image.TabIndex = 0;
            this.pictureBox_Image.TabStop = false;
            this.pictureBox_Image.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Image_MouseMove);
            this.pictureBox_Image.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Image_MouseUp);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            this.splitContainer2.Panel1.Controls.Add(this.textBox_CellText);
            this.splitContainer2.Panel1.Controls.Add(this.label_CellIndex);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.panel_Frame2);
            this.splitContainer2.Size = new System.Drawing.Size(206, 874);
            this.splitContainer2.SplitterDistance = 200;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox_SelectedCellImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(204, 112);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox_SelectedCellImage
            // 
            this.pictureBox_SelectedCellImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_SelectedCellImage.Name = "pictureBox_SelectedCellImage";
            this.pictureBox_SelectedCellImage.Size = new System.Drawing.Size(100, 50);
            this.pictureBox_SelectedCellImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_SelectedCellImage.TabIndex = 0;
            this.pictureBox_SelectedCellImage.TabStop = false;
            // 
            // textBox_CellText
            // 
            this.textBox_CellText.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox_CellText.Location = new System.Drawing.Point(0, 16);
            this.textBox_CellText.Multiline = true;
            this.textBox_CellText.Name = "textBox_CellText";
            this.textBox_CellText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_CellText.Size = new System.Drawing.Size(204, 65);
            this.textBox_CellText.TabIndex = 2;
            this.textBox_CellText.WordWrap = false;
            this.textBox_CellText.TextChanged += new System.EventHandler(this.textBox_CellText_TextChanged);
            // 
            // label_CellIndex
            // 
            this.label_CellIndex.AutoSize = true;
            this.label_CellIndex.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_CellIndex.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_CellIndex.Location = new System.Drawing.Point(0, 0);
            this.label_CellIndex.Name = "label_CellIndex";
            this.label_CellIndex.Size = new System.Drawing.Size(17, 16);
            this.label_CellIndex.TabIndex = 1;
            this.label_CellIndex.Text = "...";
            // 
            // panel_Frame2
            // 
            this.panel_Frame2.Controls.Add(this.trackBar_CellAreaMax);
            this.panel_Frame2.Controls.Add(this.label_CellAreaMax);
            this.panel_Frame2.Controls.Add(this.trackBar_CellAreaMin);
            this.panel_Frame2.Controls.Add(this.label_CellAreaMin);
            this.panel_Frame2.Controls.Add(this.label2);
            this.panel_Frame2.Controls.Add(this.trackBar_HoughRangeD);
            this.panel_Frame2.Controls.Add(this.label_HoughRangeD);
            this.panel_Frame2.Controls.Add(this.trackBar_HoughMaxLineGap);
            this.panel_Frame2.Controls.Add(this.label_HoughMaxLineGap);
            this.panel_Frame2.Controls.Add(this.trackBar_HoughMinLineLength);
            this.panel_Frame2.Controls.Add(this.label_HoughMinLineLength);
            this.panel_Frame2.Controls.Add(this.trackBar_HoughThreshold);
            this.panel_Frame2.Controls.Add(this.label_HoughThreshold);
            this.panel_Frame2.Controls.Add(this.trackBar_HoughTheta);
            this.panel_Frame2.Controls.Add(this.label_HoughTheta);
            this.panel_Frame2.Controls.Add(this.trackBar_HoughRho);
            this.panel_Frame2.Controls.Add(this.label_HoughRho);
            this.panel_Frame2.Controls.Add(this.label1);
            this.panel_Frame2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Frame2.Location = new System.Drawing.Point(0, 0);
            this.panel_Frame2.Name = "panel_Frame2";
            this.panel_Frame2.Size = new System.Drawing.Size(204, 596);
            this.panel_Frame2.TabIndex = 0;
            // 
            // trackBar_CellAreaMin
            // 
            this.trackBar_CellAreaMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar_CellAreaMin.Location = new System.Drawing.Point(0, 378);
            this.trackBar_CellAreaMin.Maximum = 10000;
            this.trackBar_CellAreaMin.Minimum = 50;
            this.trackBar_CellAreaMin.Name = "trackBar_CellAreaMin";
            this.trackBar_CellAreaMin.Size = new System.Drawing.Size(204, 45);
            this.trackBar_CellAreaMin.SmallChange = 100;
            this.trackBar_CellAreaMin.TabIndex = 14;
            this.trackBar_CellAreaMin.TickFrequency = 500;
            this.trackBar_CellAreaMin.Value = 100;
            this.trackBar_CellAreaMin.ValueChanged += new System.EventHandler(this.trackBar_CellAreaMin_ValueChanged);
            // 
            // label_CellAreaMin
            // 
            this.label_CellAreaMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_CellAreaMin.Location = new System.Drawing.Point(0, 366);
            this.label_CellAreaMin.Name = "label_CellAreaMin";
            this.label_CellAreaMin.Size = new System.Drawing.Size(204, 12);
            this.label_CellAreaMin.TabIndex = 15;
            this.label_CellAreaMin.Text = "CellAreaMin";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(0, 354);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "FindContours Parameter";
            // 
            // trackBar_HoughRangeD
            // 
            this.trackBar_HoughRangeD.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar_HoughRangeD.Location = new System.Drawing.Point(0, 309);
            this.trackBar_HoughRangeD.Maximum = 100;
            this.trackBar_HoughRangeD.Minimum = 1;
            this.trackBar_HoughRangeD.Name = "trackBar_HoughRangeD";
            this.trackBar_HoughRangeD.Size = new System.Drawing.Size(204, 45);
            this.trackBar_HoughRangeD.TabIndex = 11;
            this.trackBar_HoughRangeD.TickFrequency = 10;
            this.trackBar_HoughRangeD.Value = 10;
            this.trackBar_HoughRangeD.ValueChanged += new System.EventHandler(this.trackBar_HoughRangeD_ValueChanged);
            // 
            // label_HoughRangeD
            // 
            this.label_HoughRangeD.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_HoughRangeD.Location = new System.Drawing.Point(0, 297);
            this.label_HoughRangeD.Name = "label_HoughRangeD";
            this.label_HoughRangeD.Size = new System.Drawing.Size(204, 12);
            this.label_HoughRangeD.TabIndex = 12;
            this.label_HoughRangeD.Text = "RangeDgree";
            // 
            // trackBar_HoughMaxLineGap
            // 
            this.trackBar_HoughMaxLineGap.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar_HoughMaxLineGap.Location = new System.Drawing.Point(0, 252);
            this.trackBar_HoughMaxLineGap.Maximum = 25;
            this.trackBar_HoughMaxLineGap.Minimum = 1;
            this.trackBar_HoughMaxLineGap.Name = "trackBar_HoughMaxLineGap";
            this.trackBar_HoughMaxLineGap.Size = new System.Drawing.Size(204, 45);
            this.trackBar_HoughMaxLineGap.TabIndex = 9;
            this.trackBar_HoughMaxLineGap.TickFrequency = 5;
            this.trackBar_HoughMaxLineGap.Value = 10;
            this.trackBar_HoughMaxLineGap.ValueChanged += new System.EventHandler(this.trackBar_HoughMaxLineGap_ValueChanged);
            // 
            // label_HoughMaxLineGap
            // 
            this.label_HoughMaxLineGap.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_HoughMaxLineGap.Location = new System.Drawing.Point(0, 240);
            this.label_HoughMaxLineGap.Name = "label_HoughMaxLineGap";
            this.label_HoughMaxLineGap.Size = new System.Drawing.Size(204, 12);
            this.label_HoughMaxLineGap.TabIndex = 10;
            this.label_HoughMaxLineGap.Text = "MaxLineGap";
            // 
            // trackBar_HoughMinLineLength
            // 
            this.trackBar_HoughMinLineLength.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar_HoughMinLineLength.Location = new System.Drawing.Point(0, 195);
            this.trackBar_HoughMinLineLength.Maximum = 90;
            this.trackBar_HoughMinLineLength.Minimum = 1;
            this.trackBar_HoughMinLineLength.Name = "trackBar_HoughMinLineLength";
            this.trackBar_HoughMinLineLength.Size = new System.Drawing.Size(204, 45);
            this.trackBar_HoughMinLineLength.TabIndex = 7;
            this.trackBar_HoughMinLineLength.TickFrequency = 10;
            this.trackBar_HoughMinLineLength.Value = 10;
            this.trackBar_HoughMinLineLength.ValueChanged += new System.EventHandler(this.trackBar_HoughMinLineLength_ValueChanged);
            // 
            // label_HoughMinLineLength
            // 
            this.label_HoughMinLineLength.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_HoughMinLineLength.Location = new System.Drawing.Point(0, 183);
            this.label_HoughMinLineLength.Name = "label_HoughMinLineLength";
            this.label_HoughMinLineLength.Size = new System.Drawing.Size(204, 12);
            this.label_HoughMinLineLength.TabIndex = 8;
            this.label_HoughMinLineLength.Text = "MinLineLength";
            // 
            // trackBar_HoughThreshold
            // 
            this.trackBar_HoughThreshold.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar_HoughThreshold.Location = new System.Drawing.Point(0, 138);
            this.trackBar_HoughThreshold.Maximum = 250;
            this.trackBar_HoughThreshold.Minimum = 1;
            this.trackBar_HoughThreshold.Name = "trackBar_HoughThreshold";
            this.trackBar_HoughThreshold.Size = new System.Drawing.Size(204, 45);
            this.trackBar_HoughThreshold.TabIndex = 5;
            this.trackBar_HoughThreshold.TickFrequency = 50;
            this.trackBar_HoughThreshold.Value = 10;
            this.trackBar_HoughThreshold.ValueChanged += new System.EventHandler(this.trackBar_HoughThreshold_ValueChanged);
            // 
            // label_HoughThreshold
            // 
            this.label_HoughThreshold.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_HoughThreshold.Location = new System.Drawing.Point(0, 126);
            this.label_HoughThreshold.Name = "label_HoughThreshold";
            this.label_HoughThreshold.Size = new System.Drawing.Size(204, 12);
            this.label_HoughThreshold.TabIndex = 6;
            this.label_HoughThreshold.Text = "Threshold";
            // 
            // trackBar_HoughTheta
            // 
            this.trackBar_HoughTheta.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar_HoughTheta.Location = new System.Drawing.Point(0, 81);
            this.trackBar_HoughTheta.Maximum = 90;
            this.trackBar_HoughTheta.Minimum = 1;
            this.trackBar_HoughTheta.Name = "trackBar_HoughTheta";
            this.trackBar_HoughTheta.Size = new System.Drawing.Size(204, 45);
            this.trackBar_HoughTheta.TabIndex = 3;
            this.trackBar_HoughTheta.TickFrequency = 10;
            this.trackBar_HoughTheta.Value = 10;
            this.trackBar_HoughTheta.ValueChanged += new System.EventHandler(this.trackBar_HoughTheta_ValueChanged);
            // 
            // label_HoughTheta
            // 
            this.label_HoughTheta.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_HoughTheta.Location = new System.Drawing.Point(0, 69);
            this.label_HoughTheta.Name = "label_HoughTheta";
            this.label_HoughTheta.Size = new System.Drawing.Size(204, 12);
            this.label_HoughTheta.TabIndex = 4;
            this.label_HoughTheta.Text = "Theta";
            // 
            // trackBar_HoughRho
            // 
            this.trackBar_HoughRho.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar_HoughRho.Location = new System.Drawing.Point(0, 24);
            this.trackBar_HoughRho.Maximum = 50;
            this.trackBar_HoughRho.Minimum = 10;
            this.trackBar_HoughRho.Name = "trackBar_HoughRho";
            this.trackBar_HoughRho.Size = new System.Drawing.Size(204, 45);
            this.trackBar_HoughRho.TabIndex = 0;
            this.trackBar_HoughRho.TickFrequency = 10;
            this.trackBar_HoughRho.Value = 10;
            this.trackBar_HoughRho.ValueChanged += new System.EventHandler(this.trackBar_HoughRho_ValueChanged);
            // 
            // label_HoughRho
            // 
            this.label_HoughRho.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_HoughRho.Location = new System.Drawing.Point(0, 12);
            this.label_HoughRho.Name = "label_HoughRho";
            this.label_HoughRho.Size = new System.Drawing.Size(204, 12);
            this.label_HoughRho.TabIndex = 2;
            this.label_HoughRho.Text = "Rho";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hough Parameter";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_OpenFile,
            this.toolStripComboBox_RunMode,
            this.toolStripComboBox_ProcessImageSelector,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.toolStripComboBox_PageSelector,
            this.toolStripLabel_PageMax,
            this.toolStripSeparator3,
            this.toolStripButton_SaveFile});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(447, 25);
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
            // toolStripComboBox_RunMode
            // 
            this.toolStripComboBox_RunMode.AutoSize = false;
            this.toolStripComboBox_RunMode.Items.AddRange(new object[] {
            "Check",
            "Run"});
            this.toolStripComboBox_RunMode.Name = "toolStripComboBox_RunMode";
            this.toolStripComboBox_RunMode.Size = new System.Drawing.Size(80, 23);
            this.toolStripComboBox_RunMode.Text = "Check";
            // 
            // toolStripComboBox_ProcessImageSelector
            // 
            this.toolStripComboBox_ProcessImageSelector.Name = "toolStripComboBox_ProcessImageSelector";
            this.toolStripComboBox_ProcessImageSelector.Size = new System.Drawing.Size(105, 25);
            this.toolStripComboBox_ProcessImageSelector.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_BackgroundImageType_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel3.Text = "page = ";
            // 
            // toolStripComboBox_PageSelector
            // 
            this.toolStripComboBox_PageSelector.AutoSize = false;
            this.toolStripComboBox_PageSelector.Name = "toolStripComboBox_PageSelector";
            this.toolStripComboBox_PageSelector.Size = new System.Drawing.Size(50, 23);
            this.toolStripComboBox_PageSelector.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_PageSelector_SelectedIndexChanged);
            // 
            // toolStripLabel_PageMax
            // 
            this.toolStripLabel_PageMax.Name = "toolStripLabel_PageMax";
            this.toolStripLabel_PageMax.Size = new System.Drawing.Size(24, 22);
            this.toolStripLabel_PageMax.Text = "/ 1 ";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // toolStripLabel_MousePosition
            // 
            this.toolStripLabel_MousePosition.AutoSize = false;
            this.toolStripLabel_MousePosition.Name = "toolStripLabel_MousePosition";
            this.toolStripLabel_MousePosition.Size = new System.Drawing.Size(100, 22);
            this.toolStripLabel_MousePosition.Text = "...";
            // 
            // trackBar_CellAreaMax
            // 
            this.trackBar_CellAreaMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.trackBar_CellAreaMax.LargeChange = 10;
            this.trackBar_CellAreaMax.Location = new System.Drawing.Point(0, 435);
            this.trackBar_CellAreaMax.Maximum = 1000;
            this.trackBar_CellAreaMax.Minimum = 1;
            this.trackBar_CellAreaMax.Name = "trackBar_CellAreaMax";
            this.trackBar_CellAreaMax.Size = new System.Drawing.Size(204, 45);
            this.trackBar_CellAreaMax.TabIndex = 16;
            this.trackBar_CellAreaMax.TickFrequency = 50;
            this.trackBar_CellAreaMax.Value = 100;
            this.trackBar_CellAreaMax.ValueChanged += new System.EventHandler(this.trackBar_CellAreaMax_ValueChanged);
            // 
            // label_CellAreaMax
            // 
            this.label_CellAreaMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_CellAreaMax.Location = new System.Drawing.Point(0, 423);
            this.label_CellAreaMax.Name = "label_CellAreaMax";
            this.label_CellAreaMax.Size = new System.Drawing.Size(204, 12);
            this.label_CellAreaMax.TabIndex = 17;
            this.label_CellAreaMax.Text = "CellAreaMax";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 924);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "Form1";
            this.Text = "FormOCR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Image)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SelectedCellImage)).EndInit();
            this.panel_Frame2.ResumeLayout(false);
            this.panel_Frame2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CellAreaMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughRangeD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughMaxLineGap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughMinLineLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughTheta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_HoughRho)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CellAreaMax)).EndInit();
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
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_ProcessImageSelector;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_Frame2;
        private System.Windows.Forms.TrackBar trackBar_HoughRho;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_HoughRho;
        private System.Windows.Forms.TrackBar trackBar_HoughTheta;
        private System.Windows.Forms.Label label_HoughTheta;
        private System.Windows.Forms.TrackBar trackBar_HoughRangeD;
        private System.Windows.Forms.Label label_HoughRangeD;
        private System.Windows.Forms.TrackBar trackBar_HoughMaxLineGap;
        private System.Windows.Forms.Label label_HoughMaxLineGap;
        private System.Windows.Forms.TrackBar trackBar_HoughMinLineLength;
        private System.Windows.Forms.Label label_HoughMinLineLength;
        private System.Windows.Forms.TrackBar trackBar_HoughThreshold;
        private System.Windows.Forms.Label label_HoughThreshold;
        private System.Windows.Forms.TrackBar trackBar_CellAreaMin;
        private System.Windows.Forms.Label label_CellAreaMin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_PageSelector;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_PageMax;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_MousePosition;
        private System.Windows.Forms.TrackBar trackBar_CellAreaMax;
        private System.Windows.Forms.Label label_CellAreaMax;
    }
}

