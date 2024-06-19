using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using OpenCvSharp;

namespace FormOCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;

            Mat img = Cv2.ImRead(ofd.FileName);
            Mat gray = new Mat();
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);

            Mat edges = new Mat();
            Cv2.Canny(gray, edges, 1, 100, 3);
            Cv2.ImWrite("edges.png", edges);

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3));
            Cv2.Dilate(edges, edges, kernel);

            Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(edges, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            var rects = new List<Point[]>();
            for (int i = 0; i < contours.Length; i++)
            {
                if (Cv2.ContourArea(contours[i]) < 3000)
                    continue;

                if (hierarchy[i].Next == -1)
                    continue;

                RotatedRect rect = Cv2.MinAreaRect(contours[i]);
                Point[] rectPoints = Cv2.BoxPoints(rect).Select(p => new Point((int)p.X, (int)p.Y)).ToArray();
                rects.Add(rectPoints);
            }

            rects = rects.OrderBy(r => r[0].Y).ThenBy(r => r[0].X).ToList();

            Mat imgColor = new Mat();
            Cv2.CvtColor(img, imgColor, ColorConversionCodes.BGRA2BGR);

            Random rnd = new Random();

            for (int i = 0; i < rects.Count; i++)
            {
                int B = (int)(rnd.NextDouble() * 255.0);
                int G = (int)(rnd.NextDouble() * 255.0);
                int R = (int)(rnd.NextDouble() * 255.0);

                Scalar color = new Scalar(B, G, R);
                Cv2.DrawContours(imgColor, rects, i, color, 2);
                Cv2.PutText(imgColor, i.ToString(), rects[i][0], HersheyFonts.HersheySimplex, 0.8, color, 3);

                Console.WriteLine("rect:\n" + rects[i]);
            }

            string outPath = AppendIndexToFileName(ofd.FileName);
            Cv2.ImWrite(outPath, imgColor);
        }

        public string AppendIndexToFileName(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);
            int index = 1;

            while (File.Exists(Path.Combine(directory, $"{fileName}{index}{extension}")))
            {
                index++;
            }

            return Path.Combine(directory, $"{fileName}{index}{extension}");
        }

        string langPath;
        string lngStr = "jpn";

        public string RunOCR(System.Drawing.Bitmap img)
        {
            Tesseract.BitmapToPixConverter cvt = new Tesseract.BitmapToPixConverter();
            var pix = cvt.Convert(img);

            using(var tesseract = new Tesseract.TesseractEngine(langPath, lngStr))
            {
                Tesseract.Page page = tesseract.Process(pix);
                return page.GetText();
            }
        }
    }
}
