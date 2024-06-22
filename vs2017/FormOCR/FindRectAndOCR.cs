using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace FormOCR
{
    public class FindRectAndOCR
    {
        public string TargetFilePath = "";
        public int TargetPageIndex = 0;

        public string LangPath;
        public string LngStr = "eng";
        public string WhiteList = "0123456789";

        public List<System.Drawing.Bitmap> SrcImageList;
        public List<System.Drawing.Bitmap> ProcessImageList;
        public List<string> ProcessNameList;

        public List<Point[]> CellRects;
        public List<string> CellText;
        public List<System.Drawing.Bitmap> CellImage;

        public int CellAreaMin = 1500;

        public FindRectAndOCR()
        {
            SrcImageList = new List<System.Drawing.Bitmap>();
            ProcessImageList = new List<System.Drawing.Bitmap>();
            ProcessNameList = new List<string>();
        }

        private void ClearImgList(List<System.Drawing.Bitmap> bitmaps)
        {
            if (bitmaps != null)
            {
                foreach (var imgc in bitmaps) { imgc.Dispose(); }
                bitmaps.Clear();
            }
        }

        public void FindCellRects(int pageIndex = 0)
        {
            pageIndex = pageIndex < 0 ? 0 : pageIndex;

            Point[][] contours;
            HierarchyIndex[] hierarchy;

            ClearImgList(ProcessImageList);
            ProcessImageList = new List<System.Drawing.Bitmap>();
            ProcessNameList.Clear();

            if (SrcImageList.Count <= pageIndex) return;

            using (Mat img = BitmapConverter.ToMat(SrcImageList[pageIndex]))
            {
                ProcessNameList.Add("Source"); ProcessImageList.Add(new System.Drawing.Bitmap(SrcImageList[pageIndex]));

                using (Mat gray = new Mat())
                using (Mat edges = new Mat())
                using (Mat dst = new Mat())
                using (Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3)))
                {
                    ProcessNameList.Add("CvtColor"); Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY); ProcessImageList.Add(BitmapConverter.ToBitmap(gray));
                    //ProcessNameList.Add("Canny"); Cv2.Canny(gray, edges, 1, 100, 3); ProcessImageList.Add(BitmapConverter.ToBitmap(edges));
                    //ProcessNameList.Add("Dilate"); Cv2.Dilate(gray, gray, kernel); ProcessImageList.Add(BitmapConverter.ToBitmap(gray));
                    //ProcessNameList.Add("Erode"); Cv2.Erode(gray, gray, kernel); ProcessImageList.Add(BitmapConverter.ToBitmap(gray));
                    ProcessNameList.Add("Threshold"); Cv2.Threshold(gray, edges, 0, 255, ThresholdTypes.Otsu); ProcessImageList.Add(BitmapConverter.ToBitmap(edges));
                    ProcessNameList.Add("BitwiseNot"); Cv2.BitwiseNot(edges, dst); ProcessImageList.Add(BitmapConverter.ToBitmap(dst));
                    //ProcessNameList.Add("CvtColor"); Cv2.CvtColor(dst, gray, ColorConversionCodes.BGR2GRAY); ProcessImageList.Add(BitmapConverter.ToBitmap(gray));
                    //ProcessNameList.Add("Erode"); Cv2.Erode(dst, dst, kernel); ProcessImageList.Add(BitmapConverter.ToBitmap(dst));
                    ProcessNameList.Add("HoughLineP"); Cv2_GetHoughLineP_Image(dst); ProcessImageList.Add(BitmapConverter.ToBitmap(dst));
                    Cv2.FindContours(dst, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
                }

                CellRects = new List<Point[]>();

                ClearImgList(CellImage);
                CellImage = new List<System.Drawing.Bitmap>();

                CellText = new List<string>();

                for (int i = 0; i < contours.Length; i++)
                {
                    if (Cv2.ContourArea(contours[i]) < CellAreaMin) continue;
                    if (hierarchy[i].Next == -1) continue;

                    RotatedRect rect = Cv2.MinAreaRect(contours[i]);
                    Point[] rectPoints = Cv2.BoxPoints(rect).Select(p => new Point((int)p.X, (int)p.Y)).ToArray();
                    CellRects.Add(rectPoints);
                }

                CellRects = CellRects.OrderBy(r => r[0].Y).ThenBy(r => r[0].X).ToList();

                for (int i = 0; i < CellRects.Count; i++)
                {
                    Rect boundingRect = Cv2.BoundingRect(CellRects[i]);
                    try
                    {
                        using (Mat cropped = new Mat(img, boundingRect))
                        {
                            System.Drawing.Bitmap bmp = BitmapConverter.ToBitmap(cropped);
                            CellImage.Add(bmp);
                            CellText.Add("");
                        }
                    }
                    catch { }
                }

                ProcessNameList.Add("DrawRect"); ProcessImageList.Add(drawRect(BitmapConverter.ToBitmap(img)));

            }
        }

        public double HoughRho = 1.0;
        public double HoughTheta = Math.PI / 360;
        public int HoughThreshold = 50;
        public double HoughMinLineLength = 20;
        public double HoughMaxLineGap = 3;
        public double HoughRangeD = 10.0;

        private void Cv2_GetHoughLineP_Image(Mat src)
        {
            LineSegmentPoint[] lines = Cv2.HoughLinesP(src, HoughRho, HoughTheta, HoughThreshold, HoughMinLineLength, HoughMaxLineGap);
            src.SetTo(0);

            foreach (var line in lines)
            {
                double angle = Math.Atan2(line.P2.Y - line.P1.Y, line.P2.X - line.P1.X) * 180.0 / Math.PI;
                angle = (angle + 360) % 360 - 180;
                while (angle > 135) { angle -= 180; }
                while (angle < -45) { angle += 180; }

                if ((angle >= 0 - HoughRangeD && angle <= 0 + HoughRangeD) || (angle >= 90 - HoughRangeD && angle <= 90 + HoughRangeD))
                {
                    src.Line(line.P1, line.P2, Scalar.White, 2);
                }
            }
        }

        public System.Drawing.Bitmap drawRect(System.Drawing.Bitmap bitmap)
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
                try
                {
                    Scalar color = new Scalar(B, G, R);
                    Cv2.DrawContours(imgColor, CellRects, i, color, 2);

                    Point bottomLeft = CellRects[i].OrderByDescending(p => p.Y).ThenBy(p => p.X).First();

                    Cv2.PutText(imgColor, i.ToString() + ":" + CellText[i], bottomLeft, HersheyFonts.HersheySimplex, 0.8, color, 2);
                }
                catch { }
            }
            return BitmapConverter.ToBitmap(imgColor);
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

            using (var tesseract = new Tesseract.TesseractEngine(LangPath, LngStr))
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

        private void SaveCells(string saveFileName, string srcFileName, string pageIndexString, int pageIndex = 0)
        {
            try
            {
                int[] numbers = pageIndexString.Split(',').Select(int.Parse).ToArray();
                List<string> reportList = new List<string>();

                foreach (var i in numbers)
                {
                    reportList.Add(CellText[i]);
                }

                string Line = Path.GetFileNameWithoutExtension(srcFileName) + "\t" + pageIndex.ToString() + "\t" + string.Join("\t", reportList);
                Line = Line.Replace("\n", "").Replace("\r", "");
                File.AppendAllText(saveFileName, Line + "\r\n");
            }
            catch
            {

            }
        }
    }
}
