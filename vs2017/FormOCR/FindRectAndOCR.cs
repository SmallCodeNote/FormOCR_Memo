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
        public bool UpdateRunFlag = false;
        public string TargetFilePath = "";

        public string LangPath;

        private List<System.Drawing.Bitmap> SrcImageList;
        public int pageCount { get { return SrcImageList.Count; } }

        public List<List<System.Drawing.Bitmap>> ProcessImageList;
        public List<List<string>> ProcessNameList;

        public List<List<Point[]>> CellRectCorners;
        public List<List<string>> CellText;
        public List<List<System.Drawing.Bitmap>> CellImage;

        public int CellAreaMin = 1500;
        public int CellAreaMax = 40000;


        public FindRectAndOCR()
        {
            InitializeList();
        }

        public void InitializeList()
        {
            SrcImageList = new List<System.Drawing.Bitmap>();

            ProcessImageList = new List<List<System.Drawing.Bitmap>>();
            ProcessNameList = new List<List<string>>();

            CellRectCorners = new List<List<Point[]>>();
            CellText = new List<List<string>>();
            CellImage = new List<List<System.Drawing.Bitmap>>();
        }

        public void ResetList()
        {
            if (SrcImageList != null) ClearImgList(SrcImageList);
            if (ProcessImageList != null) ClearImgList(ProcessImageList);
            if (CellImage != null) ClearImgList(CellImage);

            InitializeList();
        }

        public void SrcImageAdd(System.Drawing.Bitmap bitmap)
        {
            SrcImageList.Add(bitmap);

            ProcessImageList.Add(new List<System.Drawing.Bitmap>());
            ProcessNameList.Add(new List<string>());

            CellRectCorners.Add(new List<Point[]>());
            CellText.Add(new List<string>());
            CellImage.Add(new List<System.Drawing.Bitmap>());
        }

        public System.Drawing.Bitmap getSrcImage(int pageIndex)
        {
            pageIndex = pageIndex < 0 ? 0 : pageIndex;
            pageIndex = pageIndex >= pageCount ? pageCount - 1 : pageIndex;

            return new System.Drawing.Bitmap(SrcImageList[pageIndex]);
        }

        private void ClearImgList(List<System.Drawing.Bitmap> bitmaps)
        {
            if (bitmaps != null)
            {
                foreach (var imgc in bitmaps) { imgc.Dispose(); }
                bitmaps.Clear();
            }
        }

        private void ClearImgList(List<List<System.Drawing.Bitmap>> bitmapLists)
        {
            if (bitmapLists != null)
            {
                foreach (var bitmaps in bitmapLists) { ClearImgList(bitmaps); }
                bitmapLists.Clear();
            }
        }

        public void FindCellRects(int pageIndex)
        {
            pageIndex = pageIndex < 0 ? pageCount - 1 : pageIndex;

            if (!UpdateRunFlag) return;
            if (pageIndex < 0) return;
            if (pageIndex >= SrcImageList.Count) return;

            if (pageIndex >= ProcessImageList.Count) ProcessImageList.Add(new List<System.Drawing.Bitmap>());
            if (pageIndex >= ProcessNameList.Count) ProcessNameList.Add(new List<string>());

            Point[][] contours; HierarchyIndex[] hierarchy;

            List<System.Drawing.Bitmap> imgList = new List<System.Drawing.Bitmap>();
            List<string> nameList = new List<string>();

            using (Mat img = BitmapConverter.ToMat(SrcImageList[pageIndex]))
            {
                nameList.Add("Source"); imgList.Add(new System.Drawing.Bitmap(SrcImageList[pageIndex]));

                using (Mat gray = new Mat())
                using (Mat edges = new Mat())
                using (Mat dst = new Mat())
                using (Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3)))
                {
                    nameList.Add("CvtColor"); Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY); imgList.Add(BitmapConverter.ToBitmap(gray));
                    //nameList.Add("Canny"); Cv2.Canny(gray, edges, 1, 100, 3); imgList.Add(BitmapConverter.ToBitmap(edges));
                    //nameList.Add("Dilate"); Cv2.Dilate(gray, gray, kernel); imgList.Add(BitmapConverter.ToBitmap(gray));
                    //nameList.Add("Erode"); Cv2.Erode(gray, gray, kernel); imgList.Add(BitmapConverter.ToBitmap(gray));
                    nameList.Add("Threshold"); Cv2.Threshold(gray, edges, 0, 255, ThresholdTypes.Otsu); imgList.Add(BitmapConverter.ToBitmap(edges));
                    nameList.Add("BitwiseNot"); Cv2.BitwiseNot(edges, dst); imgList.Add(BitmapConverter.ToBitmap(dst));
                    //nameList.Add("CvtColor"); Cv2.CvtColor(dst, gray, ColorConversionCodes.BGR2GRAY); imgList.Add(BitmapConverter.ToBitmap(gray));
                    //nameList.Add("Erode"); Cv2.Erode(dst, dst, kernel); imgList.Add(BitmapConverter.ToBitmap(dst));
                    nameList.Add("HoughLineP"); Cv2_GetHoughLineP_Image(dst); imgList.Add(BitmapConverter.ToBitmap(dst));
                    //nameList.Add("Erode"); Cv2.Erode(dst, dst, kernel); imgList.Add(BitmapConverter.ToBitmap(dst));
                    nameList.Add("Dilate"); Cv2.Dilate(dst, dst, kernel); imgList.Add(BitmapConverter.ToBitmap(dst));
                    Cv2.FindContours(dst, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
                }

                FindCellRects(contours, img, pageIndex);
                nameList.Add("DrawRect"); imgList.Add(drawRects(BitmapConverter.ToBitmap(img), pageIndex));
            }


            int iLast = ProcessNameList[pageIndex].Count() - 1;
            if (iLast >= 0 && ProcessNameList[pageIndex][iLast] == "OCR")
            {
                nameList.Add("OCR"); imgList.Add(new System.Drawing.Bitmap(ProcessImageList[pageIndex][iLast]));
            };

            ClearImgList(ProcessImageList[pageIndex]);
            ProcessImageList[pageIndex] = imgList;

            ProcessNameList[pageIndex] = nameList;
        }

        public void FindCellRects(Point[][] contours, Mat img, int pageIndex)
        {
            List<RotatedRect> CellRects = new List<RotatedRect>();

            ClearImgList(CellImage[pageIndex]);
            CellImage[pageIndex] = new List<System.Drawing.Bitmap>();
            //if (pageIndex >= CellText.Count) CellText[pageIndex] = new List<string>();

            CellRects = FindCellRects_SelectByAreaSize(contours, CellRects);
            FindCellRects_SortCellRects(CellRects);
            FindCellRects_UpdateCellRectCorners(CellRects, pageIndex);

            FindCellRects_CropCellImage(img, pageIndex);
        }

        private List<RotatedRect> FindCellRects_SelectByAreaSize(Point[][] contours, List<RotatedRect> CellRects)
        {
            for (int i = 0; i < contours.Length; i++)
            {
                if (Cv2.ContourArea(contours[i]) < CellAreaMin) continue;
                if (Cv2.ContourArea(contours[i]) > CellAreaMax) continue;

                RotatedRect rect = Cv2.MinAreaRect(contours[i]);
                CellRects.Add(rect);
            }

            return CellRects.OrderBy(r => r.Center.Y).ThenBy(r => r.Center.X).ToList();
        }

        private void FindCellRects_SortCellRects(List<RotatedRect> CellRects)
        {
            for (int i = 0; i < CellRects.Count; i++)
            {
                RotatedRect rP = CellRects[i];
                float rT = rP.Center.Y + rP.Size.Height / 4.0f;
                float rB = rP.Center.Y - rP.Size.Height / 4.0f;

                var L = CellRects.Skip(i)
                    .Where(r => r.Center.Y <= rT && r.Center.Y >= rB
                        && r.Center.X < rP.Center.X).OrderBy(r => r.Center.X)
                    .ToList();

                if (L.Count > 0)
                {
                    replaceRectsElement(CellRects, L[0], rP, i);
                    CellRects[i] = L[0];
                }
            }
        }

        private void FindCellRects_UpdateCellRectCorners(List<RotatedRect> CellRects, int pageIndex)
        {
            CellRectCorners[pageIndex] = new List<Point[]>();

            for (int i = 0; i < CellRects.Count; i++)
            {
                RotatedRect rect = CellRects[i];
                Point[] rectPoints = Cv2.BoxPoints(rect).Select(p => new Point((int)p.X, (int)p.Y)).ToArray();
                CellRectCorners[pageIndex].Add(rectPoints);
            }
        }

        private void replaceRectsElement(List<RotatedRect> CellRects, RotatedRect RectTarget, RotatedRect RectNew, int iStart = 0)
        {
            for (int i = iStart; i < CellRects.Count; i++)
            {
                if (CellRects[i] == RectTarget)
                {
                    CellRects[i] = RectNew;
                    return;
                }
            }
        }

        private void FindCellRects_CropCellImage(Mat img, int pageIndex)
        {
            for (int i = 0; i < CellRectCorners[pageIndex].Count; i++)
            {
                Rect boundingRect = Cv2.BoundingRect(CellRectCorners[pageIndex][i]);
                try
                {
                    using (Mat cropped = new Mat(img, boundingRect))
                    {
                        System.Drawing.Bitmap bmp = BitmapConverter.ToBitmap(cropped);
                        CellImage[pageIndex].Add(bmp);
                        if (CellText[pageIndex].Count < CellImage[pageIndex].Count)
                        {
                            CellText[pageIndex].Add("");
                        }
                    }
                }
                catch { }
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
                    src.Line(line.P1, line.P2, Scalar.White, 4);
                }
            }
        }

        public System.Drawing.Bitmap drawRects(System.Drawing.Bitmap bitmap, int pageIndex)
        {
            Mat img = BitmapConverter.ToMat(bitmap);
            Mat imgColor = new Mat();
            Cv2.CvtColor(img, imgColor, ColorConversionCodes.BGRA2BGR);

            Random rnd = new Random(0);

            var croppedImages = new List<Mat>();

            for (int i = 0; i < CellRectCorners[pageIndex].Count; i++)
            {
                int B = (int)(rnd.NextDouble() * 255.0);
                int G = (int)(rnd.NextDouble() * 255.0);
                int R = (int)(rnd.NextDouble() * 255.0);
                try
                {
                    Scalar color = new Scalar(B, G, R);
                    Cv2.DrawContours(imgColor, CellRectCorners[pageIndex], i, color, 2);

                    Point bottomLeft = CellRectCorners[pageIndex][i].OrderByDescending(p => p.Y).ThenBy(p => p.X).First();

                    Cv2.PutText(imgColor, i.ToString() + ":" + CellText[pageIndex][i], bottomLeft, HersheyFonts.HersheySimplex, 0.8, color, 2);
                }
                catch { }
            }
            return BitmapConverter.ToBitmap(imgColor);
        }

        public void addProcessImageResultOCR(int pageIndex, string cellIndexList)
        {
            List<string> names = ProcessNameList[pageIndex];
            System.Drawing.Bitmap img = ProcessImageList[pageIndex][0];

            if (names[names.Count - 1] == "DrawRect")
            {
                ProcessNameList[pageIndex].Add("OCR"); ProcessImageList[pageIndex].Add(drawResultOCR(img, pageIndex, cellIndexList));
            }
            else if (names[names.Count - 1] == "OCR")
            {
                ProcessImageList[pageIndex][names.Count - 1] = drawResultOCR(img, pageIndex, cellIndexList);
            }
        }

        public System.Drawing.Bitmap drawResultOCR(System.Drawing.Bitmap bitmap, int pageIndex, string cellIndexList)
        {
            Mat imgColor = BitmapConverter.ToMat(bitmap);
            int[] cellIndex = cellIndexList.Trim(',').Split(',').Select(int.Parse).OrderBy(i => i).ToArray();

            for (int i = 0; i < CellRectCorners[pageIndex].Count; i++)
            {
                int B = 255, G = 0, R = 0, A = 128;
                try
                {
                    Scalar color = new Scalar(B, G, R, A);
                    Point bottomLeft = CellRectCorners[pageIndex][i].OrderByDescending(p => p.Y).ThenBy(p => p.X).First();

                    if (cellIndex.Where(ci => ci == i).ToArray().Length > 0)
                    {
                        Cv2.DrawContours(imgColor, CellRectCorners[pageIndex], i, color, 2);
                    }

                    Cv2.PutText(imgColor, CellText[pageIndex][i], bottomLeft, HersheyFonts.HersheySimplex, 0.5, color, 1);
                }
                catch { }
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

        public void RunOCR(int pageIndex, string whiteList, string cellIndexList, string LngStr = "eng")
        {
            int[] cellIndex = cellIndexList.Trim(',').Split(',').Select(int.Parse).OrderBy(i => i).ToArray();

            for (int i = 0; i < cellIndex.Length; i++)
            {
                Tesseract.BitmapToPixConverter cvt = new Tesseract.BitmapToPixConverter();
                System.Drawing.Bitmap img = CellImage[pageIndex][cellIndex[i]];
                using (var tesseract = new Tesseract.TesseractEngine(LangPath, LngStr))
                {
                    if (whiteList != "") tesseract.SetVariable("tessedit_char_whitelist", whiteList);

                    var pix = cvt.Convert(img);
                    Tesseract.Page page = tesseract.Process(pix);
                    CellText[pageIndex][cellIndex[i]] = page.GetText().Replace("\n", "").Replace("\r", "").Replace(" ", "");
                }
            }
        }

        public void SaveCells(string saveFileName, string cellIndexList)
        {
            try
            {
                int[] cells = cellIndexList.Split(',').Select(int.Parse).ToArray();

                if (!File.Exists(saveFileName))
                { File.WriteAllText(saveFileName, "filename\tpageIndex\t" + string.Join("\t", cells.Select(i => i.ToString()).ToArray()) + "\r\n"); }

                for (int pageIndex = 0; pageIndex < CellText.Count; pageIndex++)
                {
                    List<string> reportList = new List<string>();
                    foreach (var i in cells)
                    {
                        reportList.Add(CellText[pageIndex][i]);
                    }

                    string Line = Path.GetFileNameWithoutExtension(TargetFilePath) + "\t" + pageIndex.ToString() + "\t" + string.Join("\t", reportList);
                    File.AppendAllText(saveFileName, Line + "\r\n");
                }
            }
            catch { }
        }
    }
}
