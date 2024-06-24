using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Windows.Data.Pdf;

using OpenCvSharp;
using OpenCvSharp.Extensions;
using WinFormStringCnvClass;
namespace FormOCR
{
    public partial class Form1 : Form
    {
        string thisExeDirPath;
        FindRectAndOCR OCR;

        public Form1()
        {
            InitializeComponent();
            thisExeDirPath = Path.GetDirectoryName(Application.ExecutablePath);

            OCR = new FindRectAndOCR();
            OCR.LangPath = Path.Combine(thisExeDirPath, "traineddata");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1_Resize(sender, e);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TEXT|*.txt";
            if (false && ofd.ShowDialog() == DialogResult.OK)
            {
                WinFormStringCnv.setControlFromString(this, File.ReadAllText(ofd.FileName));
            }
            else
            {
                string paramFilename = Path.Combine(thisExeDirPath, "_param.txt");
                if (File.Exists(paramFilename))
                {
                    WinFormStringCnv.setControlFromString(this, File.ReadAllText(paramFilename));
                }
            }

            OCR.UpdateRunFlag = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string FormContents = WinFormStringCnv.ToString(this);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TEXT|*.txt";

            if (false && sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, FormContents);
            }
            else
            {
                string paramFilename = Path.Combine(thisExeDirPath, "_param.txt");
                File.WriteAllText(paramFilename, FormContents);
            }
        }

        public string[] GenerateItems(int count)
        {
            string[] items = new string[count];
            for (int i = 1; i <= count; i++) { items[i - 1] = i.ToString(); }
            return items;
        }

        private void pictureBoxUpdate(PictureBox p, System.Drawing.Bitmap img)
        {
            if (p.Image != null) { p.Image.Dispose(); }
            p.Image = img;
        }

        private async void toolStripButton_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            OCR.TargetFilePath = ofd.FileName;
            OCR.ResetList();

            if (Path.GetExtension(ofd.FileName) == ".pdf")
            {
                using (var pdfStream = File.OpenRead(ofd.FileName))
                using (var winrtStream = pdfStream.AsRandomAccessStream())
                {
                    var doc = await PdfDocument.LoadFromStreamAsync(winrtStream);
                    for (var i = 0u; i < doc.PageCount; i++)
                    {
                        using (var page = doc.GetPage(i))
                        using (var memStream = new MemoryStream())
                        using (var outStream = memStream.AsRandomAccessStream())
                        {
                            await page.RenderToStreamAsync(outStream);

                            System.Drawing.Bitmap img = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(memStream);
                            OCR.SrcImageAdd(img);
                            OCR.FindCellRects(-1);
                        }
                    }
                }
            }
            else
            {

                System.Drawing.Bitmap img = new System.Drawing.Bitmap(ofd.FileName);
                OCR.SrcImageAdd(img);
                OCR.FindCellRects(-1);
            }

            int pageIndex = 0;
            if (toolStripComboBox_RunMode.Text != "Check")
            {
                for (pageIndex = 0; pageIndex < OCR.pageCount; pageIndex++)
                {
                    RunOCR(pageIndex);
                }
            }

            OCR.UpdateRunFlag = false;

            toolStripComboBox_PageSelector.Items.Clear();
            string[] items = GenerateItems(OCR.pageCount);
            toolStripComboBox_PageSelector.Items.AddRange(items);
            toolStripComboBox_PageSelector.Text = items[0];
            toolStripLabel_PageMax.Text = " / " + OCR.pageCount.ToString();

            pageIndex = toolStripComboBox_PageSelector.SelectedIndex; if (pageIndex < 0) return;

            pictureBoxUpdate(pictureBox_Image, OCR.getSrcImage(pageIndex));
            toolStripComboBox_ProcessImageSelector.Items.Clear();

            string[] Items = OCR.ProcessNameList[pageIndex].ToArray();
            toolStripComboBox_ProcessImageSelector.Items.AddRange(Items);
            toolStripComboBox_ProcessImageSelector.Text = (string)Items[0];

            OCR.UpdateRunFlag = true;

            if (toolStripComboBox_RunMode.Text != "Check") { toolStripComboBox_ProcessImageSelector.Text = Items[Items.Length - 1]; }

        }

        private void toolStripComboBox_PageSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!OCR.UpdateRunFlag) return;

            string ProcessImageSelectorText = toolStripComboBox_ProcessImageSelector.Text;

            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex; if (pageIndex < 0) return;
            OCR.FindCellRects(pageIndex);

            toolStripComboBox_ProcessImageSelector.Items.Clear();

            string[] Items = OCR.ProcessNameList[pageIndex].ToArray();

            toolStripComboBox_ProcessImageSelector.Items.AddRange(Items);
            toolStripComboBox_ProcessImageSelector.Text = (string)Items[0];

            toolStripComboBox_ProcessImageSelector.Text = ProcessImageSelectorText;

            MainPictureBoxUpdate();
        }

        private void toolStripComboBox_BackgroundImageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainPictureBoxUpdate();
        }

        private void ProcessImageListUpdate()
        {
            if (!OCR.UpdateRunFlag) return;

            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            if (pageIndex < 0) return;

            OCR.FindCellRects(pageIndex);
        }

        private void MainPictureBoxUpdate()
        {
            if (!OCR.UpdateRunFlag) return;

            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex; if (pageIndex < 0) return;
            int viewIndex = toolStripComboBox_ProcessImageSelector.SelectedIndex; if (viewIndex < 0) return;

            try
            {
                pictureBoxUpdate(pictureBox_Image, new System.Drawing.Bitmap(OCR.ProcessImageList[pageIndex][viewIndex]));
            }
            catch { }
        }


        private void pictureBox_Image_MouseUp(object sender, MouseEventArgs e)
        {
            if (OCR.CellRectCorners == null) return;
            Point clickedPoint = new Point(e.X, e.Y);
            bool find = false;

            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;

            try
            {
                for (int i = 0; i < OCR.CellRectCorners[pageIndex].Count; i++)
                {
                    var rect = OCR.CellRectCorners[pageIndex][i];

                    if (Cv2.PointPolygonTest(rect, clickedPoint, false) >= 0)
                    {
                        pictureBox_SelectedCellImage.Visible = true;
                        label_CellIndex.Visible = true;
                        textBox_CellText.Visible = true;

                        pictureBoxUpdate(pictureBox_SelectedCellImage, new System.Drawing.Bitmap(OCR.CellImage[pageIndex][i]));
                        label_CellIndex.Text = i.ToString();
                        textBox_CellText.Text = OCR.CellText[pageIndex][i];

                        if (toolStripComboBox_IndexCollectFlag.Text == "ON") { toolStripTextBox_IndexCollection.Text += i.ToString() + ","; }

                        find = true;
                    }
                }
            }
            catch { }

            if (!find)
            {
                pictureBox_SelectedCellImage.Visible = false;
                label_CellIndex.Visible = false;
                textBox_CellText.Visible = false;
            }
            else
            {
                textBox_CellText.Focus();
                textBox_CellText.SelectAll();
            }
        }

        private void toolStripButton_SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TEXT|*.txt";
            sfd.FileName = Path.GetFileNameWithoutExtension(OCR.TargetFilePath);
            if (sfd.ShowDialog() != DialogResult.OK) return;

            List<string> cellIndexList = new List<string>();
            for (int i = 0; i < dataGridView_WhiteList.Rows.Count - 1; i++)
            {
                cellIndexList.Add(dataGridView_WhiteList.Rows[i].Cells[1].Value.ToString().Trim(','));
            }

            OCR.SaveCells(sfd.FileName, string.Join(",", cellIndexList));
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = splitContainer1.Width - 200;
            this.splitContainer2.SplitterDistance = 200;
            this.splitContainer3.SplitterDistance = this.splitContainer3.Height - dataGridView_WhiteList.Rows[0].Height * 5;

            int colWidth = this.splitContainer3.Width - 70;
            dataGridView_WhiteList.Columns[0].Width = colWidth / 2;
            dataGridView_WhiteList.Columns[1].Width = colWidth / 2;
        }

        private void trackBar_Threshold_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            double value = trackBar_Threshold1.Value;
            label_Threshold1.Text = "Threshold1 : " + value.ToString();
            OCR.Threshold1 = value;

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }

        private void trackBar_HoughRho_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            double value = trackBar_HoughRho.Value / 10.0;
            label_HoughRho.Text = "Rho : " + value.ToString();
            OCR.HoughRho = value;

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }

        private void trackBar_HoughTheta_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            double value = trackBar_HoughTheta.Value / 10.0;
            label_HoughTheta.Text = "Theta : " + value.ToString();
            OCR.HoughTheta = Math.PI / 360.0 * value;

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }

        private void trackBar_HoughThreshold_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            double value = trackBar_HoughThreshold.Value;
            label_HoughThreshold.Text = "Threshold : " + value.ToString();
            OCR.HoughThreshold = (int)(value);

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }

        private void trackBar_HoughMinLineLength_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            double value = trackBar_HoughMinLineLength.Value;
            label_HoughMinLineLength.Text = "MinLineLength : " + value.ToString();
            OCR.HoughMinLineLength = value;

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }

        private void trackBar_HoughMaxLineGap_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            double value = trackBar_HoughMaxLineGap.Value;
            label_HoughMaxLineGap.Text = "MaxLineGap : " + value.ToString();
            OCR.HoughMaxLineGap = value;

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }

        private void trackBar_HoughRangeD_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            double value = trackBar_HoughRangeD.Value / 10.0;
            label_HoughRangeD.Text = "RangeD : " + value.ToString();
            OCR.HoughRangeD = value;

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }

        private void trackBar_CellAreaMin_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            int value = trackBar_CellAreaMin.Value;
            label_CellAreaMin.Text = "CellAreaMin : " + value.ToString();
            OCR.CellAreaMin = value;

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }

        private void trackBar_CellAreaMax_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            int value = trackBar_CellAreaMax.Value * 100;
            label_CellAreaMax.Text = "CellAreaMax : " + value.ToString();
            OCR.CellAreaMax = value;

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox_Image_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripLabel_MousePosition.Text = e.Location.ToString();
        }

        private void dataGridView_WhiteList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                System.Drawing.Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);

                if (e.RowIndex < dataGridView_WhiteList.RowCount - 1)
                {
                    TextRenderer.DrawText(e.Graphics,
                    (e.RowIndex + 1).ToString(),
                    e.CellStyle.Font,
                    indexRect,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                }
                e.Handled = true;
            }
        }

        private void toolStripButton_RunOCR_Click(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;

            RunOCR(pageIndex);

            toolStripComboBox_ProcessImageSelector.Items.Clear();
            string[] Items = OCR.ProcessNameList[pageIndex].ToArray();
            toolStripComboBox_ProcessImageSelector.Items.AddRange(Items);

            toolStripComboBox_ProcessImageSelector.Text = Items[Items.Length - 1];
        }

        private void RunOCR(int pageIndex)
        {
            List<string> cellIndexLists = new List<string>();

            for (int i = 0; i < dataGridView_WhiteList.Rows.Count - 1; i++)
            {
                string whiteList = dataGridView_WhiteList.Rows[i].Cells[0].Value.ToString();
                string cellIndexList = dataGridView_WhiteList.Rows[i].Cells[1].Value.ToString().Trim(',');

                OCR.RunOCR(pageIndex, whiteList, cellIndexList);

                cellIndexLists.Add(cellIndexList);
            }

            OCR.addProcessImageResultOCR(pageIndex, string.Join(",", cellIndexLists));
        }

        private void textBox_CellText_TextChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;

            List<string> cellIndexLists = new List<string>();
            for (int i = 0; i < dataGridView_WhiteList.Rows.Count - 1; i++)
            {
                string cellIndexList = dataGridView_WhiteList.Rows[i].Cells[1].Value.ToString().Trim(',');
                cellIndexLists.Add(cellIndexList);
            }

            if (int.TryParse(label_CellIndex.Text, out int cellIndex))
            {
                OCR.CellText[pageIndex][cellIndex] = textBox_CellText.Text;
                OCR.addProcessImageResultOCR(pageIndex, string.Join(",", cellIndexLists));
                MainPictureBoxUpdate();
            }
        }

        private void toolStripButton_SaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG|*.png";
            sfd.FileName = Path.GetFileNameWithoutExtension(OCR.TargetFilePath)
                + "_" + toolStripComboBox_PageSelector.Text
                + "_" + toolStripComboBox_ProcessImageSelector.SelectedIndex.ToString()
                + "_" + toolStripComboBox_ProcessImageSelector.Text;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex; if (pageIndex < 0) return;
            int viewIndex = toolStripComboBox_ProcessImageSelector.SelectedIndex; if (viewIndex < 0) return;

            OCR.ProcessImageList[pageIndex][viewIndex].Save(sfd.FileName);
        }

        private void toolStripButton_SaveAllImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG|*.png";
            sfd.FileName = Path.GetFileNameWithoutExtension(OCR.TargetFilePath);

            if (sfd.ShowDialog() != DialogResult.OK) return;

            int pageIndexMax = OCR.pageCount;

            string dirPath = Path.GetDirectoryName(sfd.FileName);
            string filename = Path.GetFileNameWithoutExtension(sfd.FileName);

            for (int pageIndex = 0; pageIndex < pageIndexMax; pageIndex++)
            {
                string pageString = (pageIndex + 1).ToString();
                int viewIndexMax = OCR.ProcessImageList[pageIndex].Count;

                for (int viewIndex = 0; viewIndex < viewIndexMax; viewIndex++)
                {
                    string viewindstr = (viewIndex + 1).ToString();
                    string viewname = OCR.ProcessNameList[pageIndex][viewIndex];

                    string savename = Path.Combine(dirPath, filename + "_" + pageString + "_" + viewindstr + "_" + viewname + Path.GetExtension(sfd.FileName));
                    OCR.ProcessImageList[pageIndex][viewIndex].Save(savename);
                }
            }
        }

        private void comboBox_ThresholdTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_ThresholdTypes.SelectedIndex)
            {
                case 0: OCR.ThresholdTypesValue = ThresholdTypes.Binary; break;
                case 1: OCR.ThresholdTypesValue = ThresholdTypes.BinaryInv; break;
                case 2: OCR.ThresholdTypesValue = ThresholdTypes.Mask; break;
                case 3: OCR.ThresholdTypesValue = ThresholdTypes.Otsu; break;
                case 4: OCR.ThresholdTypesValue = ThresholdTypes.Tozero; break;
                case 5: OCR.ThresholdTypesValue = ThresholdTypes.TozeroInv; break;
                case 6: OCR.ThresholdTypesValue = ThresholdTypes.Triangle; break;
                case 7: OCR.ThresholdTypesValue = ThresholdTypes.Trunc; break;
                default: OCR.ThresholdTypesValue = ThresholdTypes.Binary; break;
            }
        }

        private void trackBar_Threshold2_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            double value = trackBar_Threshold1.Value;
            label_Threshold2.Text = "Threshold2 : " + value.ToString();
            OCR.Threshold2 = value;

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }

        private void trackBar_HoughLineThickness_ValueChanged(object sender, EventArgs e)
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            int value = trackBar_HoughLineThickness.Value;
            label_HoughLineThickness.Text = "Thickness : " + value.ToString();
            OCR.HoughLineThickness = value;

            ProcessImageListUpdate();
            MainPictureBoxUpdate();
        }
    }
}
