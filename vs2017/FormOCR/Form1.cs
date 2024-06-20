using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Windows.Data.Pdf;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace FormOCR
{
    public partial class Form1 : Form
    {
        string thisExeDirPath;
        string langPath;
        string lngStr = "eng";

        public Form1()
        {
            InitializeComponent();
            thisExeDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            langPath = Path.Combine(thisExeDirPath, "traineddata");
        }

        public string AppendIndexToFileName(string filePath, int pageIndex)
        {
            string directory = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);
            int index = 1;

            while (File.Exists(Path.Combine(directory, $"{fileName}_{pageIndex}_{index}{extension}")))
            {
                index++;
            }
            return Path.Combine(directory, $"{fileName}_{pageIndex}_{index}{extension}");
        }

        public string RunOCR(System.Drawing.Bitmap img, string whiteList = "")
        {
            Tesseract.BitmapToPixConverter cvt = new Tesseract.BitmapToPixConverter();

            using (var tesseract = new Tesseract.TesseractEngine(langPath, lngStr))
            {
                if (whiteList != "") tesseract.SetVariable("tessedit_char_whitelist", whiteList);

                var pix = cvt.Convert(img);
                Tesseract.Page page = tesseract.Process(pix);
                return (page.GetText());
            }
        }

        public string[] RunOCR(System.Drawing.Bitmap[] imgs, string whiteList = "")
        {
            List<string> cellString = new List<string>();

            foreach (var img in imgs)
            {
                cellString.Add(RunOCR(img, whiteList));
            }
            return cellString.ToArray();
        }

        private void pictureBoxUpdate(PictureBox p, System.Drawing.Bitmap img)
        {
            if (p.Image != null) { p.Image.Dispose(); }
            p.Image = img;
        }


        List<Point[]> CellRects;
        List<string> CellText;
        List<System.Drawing.Bitmap> CellImage;
        string TargetFilePath = "";
        int TargetPageIndex = 0;

        public void FindCellRectsAndRunOCR(System.Drawing.Bitmap bitmap, string FileName, int pageIndex = 0, int ContourAreaMax = 1500)
        {
            Point[][] contours;
            HierarchyIndex[] hierarchy;

            using (Mat img = BitmapConverter.ToMat(bitmap))
            {
                using (Mat gray = new Mat())
                using (Mat edges = new Mat())
                using (Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3)))
                {
                    Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);
                    Cv2.Canny(gray, edges, 1, 100, 3);
                    Cv2.Dilate(edges, edges, kernel);
                    Cv2.FindContours(edges, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
                }

                CellRects = new List<Point[]>();
                CellImage = new List<System.Drawing.Bitmap>();
                CellText = new List<string>();

                for (int i = 0; i < contours.Length; i++)
                {
                    if (Cv2.ContourArea(contours[i]) < ContourAreaMax) continue;
                    if (hierarchy[i].Next == -1) continue;

                    RotatedRect rect = Cv2.MinAreaRect(contours[i]);
                    Point[] rectPoints = Cv2.BoxPoints(rect).Select(p => new Point((int)p.X, (int)p.Y)).ToArray();
                    CellRects.Add(rectPoints);
                }

                CellRects = CellRects.OrderBy(r => r[0].Y).ThenBy(r => r[0].X).ToList();

                for (int i = 0; i < CellRects.Count; i++)
                {
                    Rect boundingRect = Cv2.BoundingRect(CellRects[i]);
                    using (Mat cropped = new Mat(img, boundingRect))
                    {
                        System.Drawing.Bitmap bmp = BitmapConverter.ToBitmap(cropped);
                        CellImage.Add(bmp);
                        CellText.Add(RunOCR(bmp, toolStripTextBox_WhiteList.Text));
                    }
                }
            }
        }

        private System.Drawing.Bitmap drawPage(System.Drawing.Bitmap bitmap)
        {
            Mat img = BitmapConverter.ToMat(bitmap);
            Mat imgColor = new Mat();
            Cv2.CvtColor(img, imgColor, ColorConversionCodes.BGRA2BGR);

            Random rnd = new Random();

            var croppedImages = new List<Mat>();

            for (int i = 0; i < CellRects.Count; i++)
            {
                int B = (int)(rnd.NextDouble() * 255.0);
                int G = (int)(rnd.NextDouble() * 255.0);
                int R = (int)(rnd.NextDouble() * 255.0);

                Scalar color = new Scalar(B, G, R);
                Cv2.DrawContours(imgColor, CellRects, i, color, 2);

                Point bottomLeft = CellRects[i].OrderByDescending(p => p.Y).ThenBy(p => p.X).First();

                Cv2.PutText(imgColor, i.ToString() + ":" + CellText[i], bottomLeft, HersheyFonts.HersheySimplex, 0.8, color, 2);

                Rect boundingRect = Cv2.BoundingRect(CellRects[i]);
                Mat cropped = new Mat(img, boundingRect);
                croppedImages.Add(cropped);

                Console.WriteLine("CellRects: " + CellRects[i]);
            }
            return BitmapConverter.ToBitmap(imgColor);
        }

        private async void toolStripButton_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            TargetFilePath = ofd.FileName;


            if (Path.GetExtension(ofd.FileName) == "pdf")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "TEXT|*.txt";
                sfd.FileName = Path.GetFileNameWithoutExtension(TargetFilePath);

                if (toolStripComboBox_RunMode.Text != "Check" && sfd.ShowDialog() != DialogResult.OK) return;

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
                            FindCellRectsAndRunOCR(img, ofd.FileName, (int)i);
                            pictureBoxUpdate(pictureBox_Image, drawPage(img));
                        }

                        if (toolStripComboBox_RunMode.Text == "Check")
                        {
                            break;
                        }
                        else
                        {
                            SaveCells(sfd.FileName, TargetFilePath,(int)i);
                        }
                    }
                }
            }
            else
            {
                TargetPageIndex = 0;

                System.Drawing.Bitmap img = new System.Drawing.Bitmap(ofd.FileName);
                FindCellRectsAndRunOCR(img, ofd.FileName);
                pictureBoxUpdate(pictureBox_Image, drawPage(img));
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = splitContainer1.Width - 200;

            if (File.Exists(Path.Combine(thisExeDirPath, "toolStripTextBox_IndexSelect.txt")))
                toolStripTextBox_IndexSelect.Text = File.ReadAllText(Path.Combine(thisExeDirPath, "toolStripTextBox_IndexSelect.txt"));

            if (File.Exists(Path.Combine(thisExeDirPath, "toolStripTextBox_WhiteList.txt")))
                toolStripTextBox_WhiteList.Text = File.ReadAllText(Path.Combine(thisExeDirPath, "toolStripTextBox_WhiteList.txt"));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(Path.Combine(thisExeDirPath, "toolStripTextBox_WhiteList.txt"), toolStripTextBox_WhiteList.Text);
            File.WriteAllText(Path.Combine(thisExeDirPath, "toolStripTextBox_IndexSelect.txt"), toolStripTextBox_IndexSelect.Text);
        }

        private void pictureBox_Image_MouseUp(object sender, MouseEventArgs e)
        {
            Point clickedPoint = new Point(e.X, e.Y);
            bool find = false;

            for (int i = 0; i < CellRects.Count; i++)
            {
                var rect = CellRects[i];

                if (Cv2.PointPolygonTest(rect, clickedPoint, false) >= 0)
                {
                    pictureBox_SelectedCellImage.Visible = true;
                    label_CellIndex.Visible = true;
                    textBox_CellText.Visible = true;

                    pictureBoxUpdate(pictureBox_SelectedCellImage, new System.Drawing.Bitmap(CellImage[i]));
                    label_CellIndex.Text = i.ToString();
                    textBox_CellText.Text = CellText[i];

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
                CellText[CellIndex] = textBox_CellText.Text;
            }
        }

        private void SaveCells(string saveFileName, string srcFileName, int pageIndex = 0)
        {
            try
            {
                int[] numbers = toolStripTextBox_IndexSelect.Text.Split(',')
                                          .Select(int.Parse)
                                          .ToArray();

                List<string> reportList = new List<string>();

                foreach (var i in numbers)
                {
                    reportList.Add(CellText[i]);
                }

                string Line = Path.GetFileNameWithoutExtension(srcFileName) + "\t" + pageIndex.ToString() + "\t" + string.Join("\t", reportList) ;
                Line= Line.Replace("\r\n","");
                File.AppendAllText(saveFileName, Line + "\r\n");

            }
            catch
            {

            }
        }
        private void toolStripButton_SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TEXT|*.txt";
            sfd.FileName = Path.GetFileNameWithoutExtension(TargetFilePath);
            if (sfd.ShowDialog() != DialogResult.OK) return;

            SaveCells(sfd.FileName, TargetFilePath);

        }
    }
}
