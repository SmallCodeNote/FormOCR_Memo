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

        public string[] GenerateItems(int count)
        {
            string[] items = new string[count];
            for (int i = 1; i <= count; i++) { items[i-1] = i.ToString(); }
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
                            OCR.SrcImageList.Add(img);
                        }
                    }
                    
                }
            }
            else
            {
                OCR.TargetPageIndex = 0;
                System.Drawing.Bitmap img = new System.Drawing.Bitmap(ofd.FileName);
                OCR.SrcImageList.Add(img);
            }
            
            toolStripComboBox_PageSelector.Items.Clear();
            string[] items = GenerateItems(OCR.SrcImageList.Count);
            toolStripComboBox_PageSelector.Items.AddRange(items);
            toolStripComboBox_PageSelector.Text = items[0];
            toolStripLabel_PageMax.Text = " / " + OCR.SrcImageList.Count.ToString();


            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;

            if (pageIndex < 0) return;
            OCR.FindCellRects(pageIndex);
            pictureBoxUpdate(pictureBox_Image, new System.Drawing.Bitmap(OCR.SrcImageList[pageIndex]));

            toolStripComboBox_ProcessImageSelector.Items.Clear();

            string[] Items = OCR.ProcessNameList[pageIndex].ToArray();
            toolStripComboBox_ProcessImageSelector.Items.AddRange(Items);
            toolStripComboBox_ProcessImageSelector.Text = (string)Items[0];
            
            if (toolStripComboBox_RunMode.Text == "Check") return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TEXT|*.txt";
            sfd.FileName = Path.GetFileNameWithoutExtension(OCR.TargetFilePath);

            if (sfd.ShowDialog() != DialogResult.OK) return;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1_Resize(sender, e);
            splitContainer1.SplitterDistance = splitContainer1.Width - 200;

            if (File.Exists(Path.Combine(thisExeDirPath, "toolStripTextBox_IndexSelect.txt")))
                toolStripTextBox_IndexSelect.Text = File.ReadAllText(Path.Combine(thisExeDirPath, "toolStripTextBox_IndexSelect.txt"));

            if (File.Exists(Path.Combine(thisExeDirPath, "toolStripTextBox_WhiteList.txt")))
                toolStripTextBox_WhiteList.Text = File.ReadAllText(Path.Combine(thisExeDirPath, "toolStripTextBox_WhiteList.txt"));


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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(Path.Combine(thisExeDirPath, "toolStripTextBox_WhiteList.txt"), toolStripTextBox_WhiteList.Text);
            File.WriteAllText(Path.Combine(thisExeDirPath, "toolStripTextBox_IndexSelect.txt"), toolStripTextBox_IndexSelect.Text);

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

        private void pictureBox_Image_MouseUp(object sender, MouseEventArgs e)
        {
            if (OCR.CellRectCorners == null) return;
            Point clickedPoint = new Point(e.X, e.Y);
            bool find = false;

            for (int i = 0; i < OCR.CellRectCorners.Count; i++)
            {
                var rect = OCR.CellRectCorners[i];

                if (Cv2.PointPolygonTest(rect, clickedPoint, false) >= 0)
                {
                    pictureBox_SelectedCellImage.Visible = true;
                    label_CellIndex.Visible = true;
                    textBox_CellText.Visible = true;

                    pictureBoxUpdate(pictureBox_SelectedCellImage, new System.Drawing.Bitmap(OCR.CellImage[i]));
                    label_CellIndex.Text = i.ToString();
                    textBox_CellText.Text = OCR.CellText[i];

                    find = true;
                }
            }

            if (!find)
            {
                pictureBox_SelectedCellImage.Visible = false;
                label_CellIndex.Visible = false;
                textBox_CellText.Visible = false;
            }
        }

        private void textBox_CellText_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(label_CellIndex.Text, out int CellIndex))
            {
                OCR.CellText[CellIndex] = textBox_CellText.Text;
            }
        }

        private void toolStripButton_SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TEXT|*.txt";
            sfd.FileName = Path.GetFileNameWithoutExtension(OCR.TargetFilePath);
            if (sfd.ShowDialog() != DialogResult.OK) return;

            //OCR.SaveCells(sfd.FileName, OCR.TargetFilePath);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.splitContainer2.SplitterDistance = 200;
        }

        private void toolStripComboBox_BackgroundImageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainPictureBoxUpdate();
        }

        private void MainPictureBoxUpdate()
        {
            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            int viewIndex = toolStripComboBox_ProcessImageSelector.SelectedIndex;

            if (toolStripComboBox_ProcessImageSelector.SelectedIndex < 0 || OCR.ProcessImageList[pageIndex].Count <= viewIndex) return;
            pictureBoxUpdate(pictureBox_Image, new System.Drawing.Bitmap(OCR.ProcessImageList[pageIndex][viewIndex]));
        }

        private void trackBar_HoughRho_ValueChanged(object sender, EventArgs e)
        {
            double value = trackBar_HoughRho.Value / 10.0;
            label_HoughRho.Text = "Rho : " + value.ToString();
            OCR.HoughRho = value;
            OCR.FindCellRects();
            MainPictureBoxUpdate();
        }

        private void trackBar_HoughTheta_ValueChanged(object sender, EventArgs e)
        {
            double value = trackBar_HoughTheta.Value / 10.0;
            label_HoughTheta.Text = "Theta : " + value.ToString();
            OCR.HoughTheta = Math.PI / 360.0 * value;

            OCR.FindCellRects(); MainPictureBoxUpdate();
        }

        private void trackBar_HoughThreshold_ValueChanged(object sender, EventArgs e)
        {
            double value = trackBar_HoughThreshold.Value;
            label_HoughThreshold.Text = "Threshold : " + value.ToString();
            OCR.HoughThreshold = (int)(value);

            OCR.FindCellRects(); MainPictureBoxUpdate();
        }

        private void trackBar_HoughMinLineLength_ValueChanged(object sender, EventArgs e)
        {
            double value = trackBar_HoughMinLineLength.Value;
            label_HoughMinLineLength.Text = "MinLineLength : " + value.ToString();
            OCR.HoughMinLineLength = value;

            OCR.FindCellRects(); MainPictureBoxUpdate();
        }

        private void trackBar_HoughMaxLineGap_ValueChanged(object sender, EventArgs e)
        {
            double value = trackBar_HoughMaxLineGap.Value;
            label_HoughMaxLineGap.Text = "MaxLineGap : " + value.ToString();
            OCR.HoughMaxLineGap = value;

            OCR.FindCellRects(); MainPictureBoxUpdate();
        }

        private void trackBar_HoughRangeD_ValueChanged(object sender, EventArgs e)
        {
            double value = trackBar_HoughRangeD.Value / 10.0;
            label_HoughRangeD.Text = "RangeD : " + value.ToString();
            OCR.HoughRangeD = value;

            OCR.FindCellRects(); MainPictureBoxUpdate();
        }

        private void trackBar_CellAreaMin_ValueChanged(object sender, EventArgs e)
        {
            int value = trackBar_CellAreaMin.Value;
            label_CellAreaMin.Text = "CellAreaMin : " + value.ToString();
            OCR.CellAreaMin = value;

            OCR.FindCellRects(); MainPictureBoxUpdate();
        }

        private void toolStripTextBox_WhiteList_TextChanged(object sender, EventArgs e)
        {
            OCR.WhiteList = toolStripTextBox_WhiteList.Text;
        }

        private void toolStripComboBox_PageSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ProcessImageSelector = toolStripComboBox_ProcessImageSelector.Text;

            int pageIndex = toolStripComboBox_PageSelector.SelectedIndex;
            if (pageIndex < 0) return;

            OCR.FindCellRects(pageIndex);
            pictureBoxUpdate(pictureBox_Image, new System.Drawing.Bitmap(OCR.SrcImageList[0]));

            toolStripComboBox_ProcessImageSelector.Items.Clear();
            
            string[] Items = OCR.ProcessNameList[pageIndex].ToArray();

            toolStripComboBox_ProcessImageSelector.Items.AddRange(Items);
            toolStripComboBox_ProcessImageSelector.Text = (string)Items[0];

             toolStripComboBox_ProcessImageSelector.Text = ProcessImageSelector;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox_Image_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripLabel_MousePosition.Text = e.Location.ToString();
        }

        private void trackBar_CellAreaMax_ValueChanged(object sender, EventArgs e)
        {
            int value = trackBar_CellAreaMax.Value * 100;
            label_CellAreaMax.Text = "CellAreaMax : " + value.ToString();
            OCR.CellAreaMax = value;

            OCR.FindCellRects(); MainPictureBoxUpdate();
        }
    }
}
