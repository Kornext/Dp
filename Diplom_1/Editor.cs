using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Diplom_1
{
    public partial class Editor : Form
    {
        public Editor(Bitmap image1, string path_files)
        {
            InitializeComponent();
            image1_ed = image1;
            fullPath = path_files;
            initialization();
        }

        #region Основные переменные
        List<Point> listPoints = new List<Point>();
        private Point line;
        private Point mousePos1;
        private Point mousePos2;
        private Point first;
        private Point last;
        private Point orig;
        private Point end;
        private DraggedFragment draggedFragment;

        private Bitmap image1_ed;
        private Image imgPrev;
        private Image imgNow;
        private Image img_graphics;
        public Image imgOriginal;

        string fullPath;
        double[] zna4 = new double[19] { 7.45, 5.960, 4.768, 3.814, 3.051, 2.441, 1.953, 1.562, 1.25, 1, 0.75, 0.562, 0.421, 0.316, 0.237, 0.177, 0.133, 0.1, 0.075 }; //+-25%
        int index_mass = 9;//индекс массива масштабирования
        int index_menu = 1; // по умолчанию 1.
        int eX, eY;

        Color CurrentColor = Color.Black; //Текущий цвет. По умолчанию он черный
        Rectangle selRect;
        Pen pen = new Pen(Color.Black, 2f) { DashStyle = DashStyle.Solid };
        Pen rect_border = new Pen(Brushes.Black, 1f) { DashStyle = DashStyle.Solid };
        Brush rect_fill = new SolidBrush(Color.Black);
        Graphics grBmp;
        bool dragged = false, lobz = false;
        bool flag = true; //true -белый, Флаг копирования или вырезани

        enum filtr { glass, nlCor, wave, gCor, mono, contrast, brig, noise };
        filtr nextF;
        #endregion   

        private void initialization() 
        {
            pictureBox1.Image = image1_ed;
            imgPrev = image1_ed;
            imgOriginal = image1_ed;
            noneToolStripMenuItem.Text = Path.GetFileName(fullPath);
            draggedFragment = null;

            int h = imgOriginal.Height;
            int w = imgOriginal.Width;
            imgPrev = pictureBox1.Image;
            Size sz = new Size(w, h);
            Bitmap bmp = new Bitmap(imgPrev, sz);
            pictureBox1.Image = bmp;

            imgNow = pictureBox1.Image;
            pictureBox2.BackColor = CurrentColor;
            comboBox2.SelectedItem = "1";
            toolStripButton1.CheckOnClick = true;
            grBmp = Graphics.FromImage(pictureBox1.Image);
            radioButton1.Checked = true;
            flag = true;
            
        }

        public static class Matrix
        {

            public static double[,] Sobel3x3Horizontal
            {
                get
                {
                    return new double[,]
                    { { -1,  0,  1, },
                  { -2,  0,  2, },
                  { -1,  0,  1, }, };
                }
            }
            public static double[,] Sobel3x3Vertical
            {
                get
                {
                    return new double[,]
                    { {  1,  2,  1, },
                  {  0,  0,  0, },
                  { -1, -2, -1, }, };
                }
            }
        }

        #region Работа с файлами    
        void DownloadImg()
        {
            Stream myStream;
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.InitialDirectory = "d:\\";
            openDialog.Filter = "*Image Files(*.BMP;*.JPG)|*.BMP;*.JPG";
            openDialog.FilterIndex = 2;
            openDialog.RestoreDirectory = true;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (imgOriginal != null)
                        imgOriginal.Dispose();
                    if (imgPrev != null)
                        imgPrev.Dispose();
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    if (imgNow != null)
                        imgNow.Dispose();
                    if ((myStream = openDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            fullPath = openDialog.FileName;
                            pictureBox1.Image = Image.FromFile(openDialog.FileName);
                            imgOriginal = pictureBox1.Image;
                            imgNow = imgOriginal;
                            noneToolStripMenuItem.Text = Path.GetFileName(openDialog.FileName);
                            фильтрыToolStripMenuItem.Enabled = true;
                            сохранитькакToolStripMenuItem.Enabled = true;
                            очиститьToolStripMenuItem.Enabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void SaveAs()
        {
            Stream myStream;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Image Files(*.bmp)|*.BMP|Image Files(*.jpg)|*.jpg|Image Files(*.jpeg)|*.jpeg|Image Files(*.png)|*.png";
            saveDialog.FilterIndex = 2;
            saveDialog.RestoreDirectory = true;
            try
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            MessageBox.Show(Path.GetDirectoryName(saveDialog.FileName) + @"\" + Path.GetFileName(saveDialog.FileName));
                            pictureBox1.Image.Save(Path.GetFileName(saveDialog.FileName));
                        }
                        string tmp = Path.GetDirectoryName(saveDialog.FileName) + @"\" + Path.GetFileName(saveDialog.FileName);
                        if (File.Exists(tmp))
                            File.Delete(tmp);
                        File.Move(Path.GetFileName(saveDialog.FileName), tmp);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Save()
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
            bmp1.Save(fullPath, pictureBox1.Image.RawFormat);
            bmp1.Dispose();
        }
        #endregion

        #region Панель

        private void ClosePanel()
        {
            ParamsPanel.Visible = false;
            if (trackBar1.Maximum == 255)
                trackBar1.Maximum = 100;
        }

        private void ViewPanel(bool flag)
        {
            ParamsPanel.Visible = true;
            trackBar1.Focus();
            if(flag)
                trackBar2.Visible = true;
            else
                trackBar2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClosePanel();
            switch (nextF)
            {
                case filtr.glass:
                    Filtr_Glass(trackBar1.Value * 0.01);
                    break;
                case filtr.gCor:
                    Filtr_GamaCorrection(trackBar2.Value * 0.5, trackBar1.Value * 0.25);
                    break;
                case filtr.wave:
                    Filtr_Wave(trackBar2.Value, trackBar1.Value);
                    break;
                case filtr.nlCor:
                    Filtr_NonlinearCorrection(trackBar1.Value);
                    break;
                case filtr.mono:
                    BlackAndWhite(trackBar1.Value * 2.55);
                    break;
                case filtr.contrast:
                    Contrast(trackBar1.Value - 50);
                    break;
                case filtr.brig:
                    Brightness(trackBar1.Value - 50);
                    break;
                case filtr.noise:
                    {
                        Noise(Convert.ToInt16(trackBar1.Value / 25) + 2);
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClosePanel();
        }

        #endregion

        #region Фильтры (функции)
        public void DoubleConvolutionFilter(double[,] xFilterMatrix, double[,] yFilterMatrix, double factor = 1, int bias = 0)
        {
            Bitmap sourceBitmap = new Bitmap(pictureBox1.Image);
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];
            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);
            double blueX = 0.0;
            double greenX = 0.0;
            double redX = 0.0;
            double blueY = 0.0;
            double greenY = 0.0;
            double redY = 0.0;
            double blueTotal = 0.0;
            double greenTotal = 0.0;
            double redTotal = 0.0;
            int filterOffset = 1;
            int calcOffset = 0;
            int byteOffset = 0;
            for (int offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blueX = greenX = redX = 0;
                    blueY = greenY = redY = 0;
                    blueTotal = greenTotal = redTotal = 0.0;
                    byteOffset = offsetY * sourceData.Stride + offsetX * 4;
                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);
                            blueX += (double)(pixelBuffer[calcOffset]) * xFilterMatrix[filterY + filterOffset, filterX + filterOffset];
                            greenX += (double)(pixelBuffer[calcOffset + 1]) * xFilterMatrix[filterY + filterOffset, filterX + filterOffset];
                            redX += (double)(pixelBuffer[calcOffset + 2]) * xFilterMatrix[filterY + filterOffset, filterX + filterOffset];
                            blueY += (double)(pixelBuffer[calcOffset]) * yFilterMatrix[filterY + filterOffset, filterX + filterOffset];
                            greenY += (double)(pixelBuffer[calcOffset + 1]) * yFilterMatrix[filterY + filterOffset, filterX + filterOffset];
                            redY += (double)(pixelBuffer[calcOffset + 2]) * yFilterMatrix[filterY + filterOffset, filterX + filterOffset];
                        }
                    }
                    blueTotal = Math.Sqrt((blueX * blueX) + (blueY * blueY));
                    greenTotal = Math.Sqrt((greenX * greenX) + (greenY * greenY));
                    redTotal = Math.Sqrt((redX * redX) + (redY * redY));

                    if (blueTotal > 255)
                    { blueTotal = 255; }
                    else if (blueTotal < 0)
                    { blueTotal = 0; }

                    if (greenTotal > 255)
                    { greenTotal = 255; }
                    else if (greenTotal < 0)
                    { greenTotal = 0; }

                    if (redTotal > 255)
                    { redTotal = 255; }
                    else if (redTotal < 0)
                    { redTotal = 0; }

                    resultBuffer[byteOffset] = (byte)(blueTotal);
                    resultBuffer[byteOffset + 1] = (byte)(greenTotal);
                    resultBuffer[byteOffset + 2] = (byte)(redTotal);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }
            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);
            pictureBox1.Image = resultBitmap;
            imgNow = pictureBox1.Image;
        }
        public void contur()
        {
            Bitmap image_gray = new Bitmap(pictureBox1.Image);
            Graphics g = Graphics.FromImage(image_gray);
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {new float[] {0.3f, 0.3f, 0.3f, 0, 0},
            new float[] {0.59f, 0.59f, 0.59f, 0, 0},
            new float[] {0.11f, 0.11f, 0.11f, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 1}
            });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(pictureBox1.Image,
            new Rectangle(0, 0, pictureBox1.Image.Width, pictureBox1.Image.Height), 0, 0,
            pictureBox1.Image.Width, pictureBox1.Image.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            //оконтуривание 
            Bitmap contoured = new Bitmap(image_gray);
            Bitmap b = new Bitmap(image_gray);
            for (int i = 1; i < (image_gray.Height - 1); i++)
                for (int j = 1; j < (image_gray.Width - 1); j++)
                {
                    float new_x = 0;
                    int currA = b.GetPixel(j, i).B;
                    int currB = b.GetPixel(j, i + 1).B;
                    int currC = b.GetPixel(j + 1, i).B;
                    int currD = b.GetPixel(j + 1, i + 1).B;
                    new_x = Math.Abs(currA - currD) + Math.Abs(currB - currC);
                    if (new_x * new_x > 128)
                        contoured.SetPixel(j, i, Color.White);
                    else
                        contoured.SetPixel(j, i, Color.Black);
                }
            pictureBox1.Image = contoured;
            imgNow = pictureBox1.Image;
        }
        public void smoothing()
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            int[,] arrR = new int[image.Width, image.Height];
            int[,] arrG = new int[image.Width, image.Height];
            int[,] arrB = new int[image.Width, image.Height];

            //получаем массивы значений цвета начальных пикселей изображения
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    arrR[i, j] = image.GetPixel(i, j).R;
                    arrG[i, j] = image.GetPixel(i, j).G;
                    arrB[i, j] = image.GetPixel(i, j).B;
                }
            }

            for (int i = 1; i < image.Width - 1; i++)
            {
                for (int j = 1; j < image.Height - 1; j++)
                {
                    int arrRsum = 0;
                    int arrGsum = 0;
                    int arrBsum = 0;
                    int arrSrR = 0;
                    int arrSrG = 0;
                    int arrSrB = 0;
                    //вычисляем сумму интенсивности пикселей для (RGB) и находим среднее
                    for (int x = -1; x < 2; x++)
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            arrRsum += arrR[i + x, j + y];
                            arrGsum += arrG[i + x, j + y];
                            arrBsum += arrB[i + x, j + y];
                        }
                    }
                    arrSrR = arrRsum / 9;
                    arrSrG = arrGsum / 9;
                    arrSrB = arrBsum / 9;
                    image.SetPixel(i, j, Color.FromArgb(arrSrR, arrSrG, arrSrB));
                }
            }
            pictureBox1.Image = image;
            imgNow = pictureBox1.Image;
        }
        public void Filtr_negative()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image, pictureBox1.Image.Width, pictureBox1.Image.Height);
            Color color = new Color();
            int R = 0, G = 0, B = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    color = bmp.GetPixel(i, j);
                    R = 255 - color.R;
                    G = 255 - color.G;
                    B = 255 - color.B;
                    bmp.SetPixel(i, j, Color.FromArgb(R, G, B));
                }
            }
            pictureBox1.Image = bmp;
            imgNow = pictureBox1.Image;
        }
        public void Filtr_med()
        {
            Bitmap source = new Bitmap(pictureBox1.Image);
            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];
            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            source.UnlockBits(sourceData);
            int filterOffset = (11 - 1) / 2;
            int calcOffset = 0;
            int byteOffset = 0;
            List<int> neighbourPixels = new List<int>();
            byte[] middlePixel;
            for (int offsetY = filterOffset; offsetY < source.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < source.Width - filterOffset; offsetX++)
                {
                    byteOffset = offsetY * sourceData.Stride + offsetX * 4;
                    neighbourPixels.Clear();
                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);
                            neighbourPixels.Add(BitConverter.ToInt32(pixelBuffer, calcOffset));
                        }
                    }
                    neighbourPixels.Sort();
                    middlePixel = BitConverter.GetBytes(neighbourPixels[filterOffset]);
                    resultBuffer[byteOffset] = middlePixel[0];
                    resultBuffer[byteOffset + 1] = middlePixel[1];
                    resultBuffer[byteOffset + 2] = middlePixel[2];
                    resultBuffer[byteOffset + 3] = middlePixel[3];
                }
            }
            Bitmap result = new Bitmap(source.Width, source.Height);
            BitmapData resultData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            result.UnlockBits(resultData);
            pictureBox1.Image = result;
            imgNow = pictureBox1.Image;
        }
        private void BlackAndWhite(double P)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            Bitmap result = new Bitmap(bmp.Width, bmp.Height);
            Color color = new Color();
            for (int j = 0; j < bmp.Height; j++)
            {
                for (int i = 0; i < bmp.Width; i++)
                {
                    color = bmp.GetPixel(i, j);
                    double k = ((color.R + color.G + color.B) / 3);
                    result.SetPixel(i, j, (k <= P ? Color.Black : Color.White));
                }
            }
            pictureBox1.Image = result;
            imgNow = pictureBox1.Image;
        }
        private void MakeGray(Bitmap bmp)
        {
            // Задаём формат Пикселя.
            PixelFormat pxf = PixelFormat.Format24bppRgb;

            // Получаем данные картинки.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            //Блокируем набор данных изображения в памяти
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            // Получаем адрес первой линии.
            IntPtr ptr = bmpData.Scan0;

            // Задаём массив из Byte и помещаем в него надор данных.
            // int numBytes = bmp.Width * bmp.Height * 3; 
            //На 3 умножаем - поскольку RGB цвет кодируется 3-мя байтами
            //Либо используем вместо Width - Stride
            int numBytes = bmpData.Stride * bmp.Height;
            int widthBytes = bmpData.Stride;
            byte[] rgbValues = new byte[numBytes];

            // Копируем значения в массив.
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            // Перебираем пикселы по 3 байта на каждый и меняем значения
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                int value = rgbValues[counter] + rgbValues[counter + 1] + rgbValues[counter + 2];
                byte color_b = 0;
                color_b = Convert.ToByte(value / 3);
                rgbValues[counter] = color_b;
                rgbValues[counter + 1] = color_b;
                rgbValues[counter + 2] = color_b;
            }
            // Копируем набор данных обратно в изображение
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            // Разблокируем набор данных изображения в памяти.
            bmp.UnlockBits(bmpData);
        }
        private void Filtr_Glass(double z)
        {
            Random rand = new Random();
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            Bitmap newBmp = new Bitmap(imgPrev);
            int x, y;
            for (int j = 0; j < pictureBox1.Image.Size.Height; j++)
            {
                for (int i = 0; i < pictureBox1.Image.Size.Width; i++)
                {
                    y = (int)(j + (rand.NextDouble() - z) * 10);
                    x = (int)(i + (rand.NextDouble() - z) * 10);
                    if (y < 0) y = 0;
                    if (x < 0) x = 0;
                    if (x >= pictureBox1.Image.Size.Width) x = pictureBox1.Image.Size.Width - 1;
                    if (y >= pictureBox1.Image.Size.Height) y = pictureBox1.Image.Size.Height - 1;

                    newBmp.SetPixel(i, j, bmp.GetPixel(x, y));
                }
                pictureBox1.Image = newBmp;
                imgNow = pictureBox1.Image;
            }

        }
        private void Filtr_Wave(double c, double z)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            Bitmap newBmp = new Bitmap(imgPrev);
            int x, y;
            for (int j = 0; j < pictureBox1.Image.Size.Height; j++)
            {
                for (int i = 0; i < pictureBox1.Image.Size.Width; i++)
                {
                    y = j;
                    x = (int)(i + c * Math.Sin(j * 2 * Math.PI / z));

                    if (y < 0) y = 0;
                    if (x < 0) x = 0;
                    if (x >= pictureBox1.Image.Size.Width) x = pictureBox1.Image.Size.Width - 1;
                    if (y >= pictureBox1.Image.Size.Height) y = pictureBox1.Image.Size.Height - 1;

                    newBmp.SetPixel(i, j, bmp.GetPixel(x, y));

                }
                pictureBox1.Image = newBmp;
                imgNow = pictureBox1.Image;
            }
        }
        #endregion

        #region Коррекция (функции)
        private void Filtr_NonlinearCorrection(double z)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            int r = 0, g = 0, b = 0;
            Color pixel;
            for (int j = 0; j < pictureBox1.Image.Size.Height; j++)
            {
                for (int i = 0; i < pictureBox1.Image.Size.Width; i++)
                {
                    pixel = bmp.GetPixel(i, j);
                    r = (byte)(z * Math.Log(1 + pixel.R));
                    g = (byte)(z * Math.Log(1 + pixel.G));
                    b = (byte)(z * Math.Log(1 + pixel.B));

                    pixel = Color.FromArgb(r, g, b);

                    bmp.SetPixel(i, j, pixel);

                }
                pictureBox1.Image = bmp;
                imgNow = pictureBox1.Image;
            }

        }
        private void Filtr_GamaCorrection(double c, double z)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            int r = 0, g = 0, b = 0;
            Color pixel;
            for (int j = 0; j < pictureBox1.Image.Size.Height; j++)
            {
                for (int i = 0; i < pictureBox1.Image.Size.Width; i++)
                {
                    pixel = bmp.GetPixel(i, j);
                    r = (byte)(c * Math.Pow(pixel.R, z));
                    g = (byte)(c * Math.Pow(pixel.G, z));
                    b = (byte)(c * Math.Pow(pixel.B, z));

                    pixel = Color.FromArgb(r, g, b);

                    bmp.SetPixel(i, j, pixel);

                }
                pictureBox1.Image = bmp;
                imgNow = pictureBox1.Image;
            }

        }
        public void Contrast(int poz)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            int lenght = 50;
            int R, G, B;
            int N;
            uint point = 0;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    point = (uint)(bmp.GetPixel(j, i).ToArgb());

                    N = (100 / lenght) * poz; //кол-во процентов
                    if (N >= 0)
                    {
                        if (N == 100) N = 99;
                        R = (int)((((point & 0x00FF0000) >> 16) * 100 - 128 * N) / (100 - N));
                        G = (int)((((point & 0x0000FF00) >> 8) * 100 - 128 * N) / (100 - N));
                        B = (int)(((point & 0x000000FF) * 100 - 128 * N) / (100 - N));
                    }
                    else
                    {
                        R = (int)((((point & 0x00FF0000) >> 16) * (100 - (-N)) + 128 * (-N)) / 100);
                        G = (int)((((point & 0x0000FF00) >> 8) * (100 - (-N)) + 128 * (-N)) / 100);
                        B = (int)(((point & 0x000000FF) * (100 - (-N)) + 128 * (-N)) / 100);
                    }
                    //контролируем переполнение переменных
                    if (R < 0) R = 0;
                    if (R > 255) R = 255;
                    if (G < 0) G = 0;
                    if (G > 255) G = 255;
                    if (B < 0) B = 0;
                    if (B > 255) B = 255;
                    point = 0xFF000000 | ((uint)R << 16) | ((uint)G << 8) | ((uint)B);
                    bmp.SetPixel(j, i, Color.FromArgb((int)point));
                }
            pictureBox1.Image = bmp;
            imgNow = pictureBox1.Image;
        }
        public void Brightness(int poz)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            int lenght = 50;
            int R, G, B;
            int N;
            uint point = 0;
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    point = (uint)(bmp.GetPixel(j, i).ToArgb());

                    N = (100 / lenght) * poz; //кол-во процентов
                    R = (int)(((point & 0x00FF0000) >> 16) + N * 128 / 100);
                    G = (int)(((point & 0x0000FF00) >> 8) + N * 128 / 100);
                    B = (int)((point & 0x000000FF) + N * 128 / 100);
                    //контролируем переполнение переменных
                    if (R < 0) R = 0;
                    if (R > 255) R = 255;
                    if (G < 0) G = 0;
                    if (G > 255) G = 255;
                    if (B < 0) B = 0;
                    if (B > 255) B = 255;
                    point = 0xFF000000 | ((uint)R << 16) | ((uint)G << 8) | ((uint)B);
                    bmp.SetPixel(j, i, Color.FromArgb((int)point));
                }
            pictureBox1.Image = bmp;
            imgNow = pictureBox1.Image;
        }
        public void Noise(int Size)
        {
            Bitmap TempBitmap = new Bitmap(pictureBox1.Image);
            Bitmap NewBitmap = null;
            NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
            Random TempRandom = new Random();
            int ApetureMin = -(Size / 2);
            int ApetureMax = (Size / 2);
            for (int x = 0; x < NewBitmap.Width; ++x)
            {
                for (int y = 0; y < NewBitmap.Height; ++y)
                {
                    List<int> RValues = new List<int>();
                    List<int> GValues = new List<int>();
                    List<int> BValues = new List<int>();
                    for (int x2 = ApetureMin; x2 < ApetureMax; ++x2)
                    {
                        int TempX = x + x2;
                        if (TempX >= 0 && TempX < NewBitmap.Width)
                        {
                            for (int y2 = ApetureMin; y2 < ApetureMax; ++y2)
                            {
                                int TempY = y + y2;
                                if (TempY >= 0 && TempY < NewBitmap.Height)
                                {
                                    Color TempColor = TempBitmap.GetPixel(TempX, TempY);
                                    RValues.Add(TempColor.R);
                                    GValues.Add(TempColor.G);
                                    BValues.Add(TempColor.B);
                                }
                            }
                        }
                    }
                    RValues.Sort();
                    GValues.Sort();
                    BValues.Sort();
                    Color MedianPixel = Color.FromArgb(RValues[RValues.Count / 2],
                    GValues[GValues.Count / 2],
                    BValues[BValues.Count / 2]);
                    NewBitmap.SetPixel(x, y, MedianPixel);
                }
            }
            pictureBox1.Image = NewBitmap;
            imgNow = pictureBox1.Image;
        }
        #endregion

        #region Преобразования (функции)
        private Bitmap rotateImage(Bitmap input, float angle)
        {
            Bitmap result = new Bitmap(input.Width, input.Height);
            Graphics g = Graphics.FromImage(result);
            g.TranslateTransform((float)input.Width / 2, (float)input.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(float)input.Width / 2, -(float)input.Height / 2);
            g.DrawImage(input, new Point(0, 0));
            return result;
        }
        #endregion

        #region Верхнее меню

            #region Файл

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadImg();
        }

        private void сохранитькакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgOriginal = null;
            imgPrev = null;
            noneToolStripMenuItem.Text = "(none)";
            pictureBox1.Image = null;
            сохранитькакToolStripMenuItem.Enabled = false;
            очиститьToolStripMenuItem.Enabled = false;
            фильтрыToolStripMenuItem.Enabled = false;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

            #region Фильтры
        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextF = filtr.glass;
            ViewPanel(false); //нет второй трекбара
        }

        private void волнаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextF = filtr.wave;
            ViewPanel(true);
        }

        private void чернобелоеИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            MakeGray(bmp);
            pictureBox1.Image = bmp;
            imgNow = pictureBox1.Image;
        }

        private void монохромToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextF = filtr.mono;
            ViewPanel(false);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            Filtr_med();
        }

        private void негативToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            Filtr_negative();
        }

        private void сглаживающийФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(imgPrev);
            smoothing();
        }

        private void выделениеКонтуровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            contur();
        }

        private void выделениеКонтуровСобельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            DoubleConvolutionFilter(Matrix.Sobel3x3Horizontal, Matrix.Sobel3x3Vertical, 1.0, 0);
        }

        #endregion

            #region Коррекция
        private void нелинейнаяКоррекцияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            nextF = filtr.nlCor;
            ViewPanel(false);
        }

        private void гаммаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            nextF = filtr.gCor;
            ViewPanel(true);
        }

        private void яркостьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            trackBar1.Value = 50;
            nextF = filtr.brig;
            ViewPanel(false);
        }

        private void контрастностьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            trackBar1.Value = 50;
            nextF = filtr.contrast;
            ViewPanel(false);
        }

        private void подавлениеШумаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            nextF = filtr.noise;
            ViewPanel(false);
        }
        #endregion

            #region Преобразования
        private void измененияРазмераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scl.height = imgOriginal.Height;
            scl.width = imgOriginal.Width;
            Scale f = new Scale();
            f.ShowDialog();
            f.Dispose();
            imgPrev = pictureBox1.Image;
            Size sz = new Size(scl.width, scl.height);
            Bitmap bmp = new Bitmap(imgPrev, sz);
            pictureBox1.Image = bmp;
            imgNow = pictureBox1.Image;
            index_mass = 9;
        }

        private void поворотВправо90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Image img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipXY);
            pictureBox1.Image = img;
            imgNow = pictureBox1.Image;
        }

        private void поворотВлево90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Image img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.Rotate270FlipXY);
            pictureBox1.Image = img;
            imgNow = pictureBox1.Image;
        }

        private void отражениеПоОсиХToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Image img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox1.Image = img;
            imgNow = pictureBox1.Image;
        }

        private void отражениеПоОсиYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Image img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Image = img;
            imgNow = pictureBox1.Image;
        }

        private void поворотНаПроизвольныйУголToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Rotation rot = new Rotation();
            rot.ShowDialog();
            rot.Dispose();
            pictureBox1.Image = rotateImage(bmp, rotate.angl);
            imgNow = pictureBox1.Image;
        }

        #endregion

            #region Отмена
        private void последнееToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imgPrev; //КОСЯК!!!!
            int h = pictureBox1.Image.Height;
            int w = pictureBox1.Image.Width;
            Size sz = new Size(w, h);
            Bitmap bmp = new Bitmap(imgPrev, sz);
            pictureBox1.Image = bmp;
            imgNow = pictureBox1.Image;
        }

        private void всеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imgOriginal;
            int h = pictureBox1.Image.Height;
            int w = pictureBox1.Image.Width;
            Size sz = new Size(w, h);
            Bitmap bmp = new Bitmap(imgOriginal, sz);
            pictureBox1.Image = bmp;
            imgNow = pictureBox1.Image;
        }
        #endregion

        #endregion

        #region Рисовка PictureBox

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (index_menu == 5)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                CurrentColor = bmp.GetPixel(e.X, e.Y);
                pictureBox2.BackColor = CurrentColor;
                pen.Color = CurrentColor;
                rect_border.Color = CurrentColor;
                rect_fill = new SolidBrush(CurrentColor);
            }
            if (index_menu == 6)
            {
                Point start = new Point(e.X, e.Y);
                brush(start);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (index_menu == 1)
            {
                //юзер кликнул мышью мимо фрагмента?
                if (draggedFragment != null && !draggedFragment.Rect.Contains(e.Location))
                {   //DraggedFragmet.Rect - прямоугольник с обьектом 
                    //Contains - проверка на содержание кооридант в этом прямоугольнике
                    //e.Location - получаем коориданты щелчка мыши
                    //уничтожаем фрагмент
                    draggedFragment = null;
                    pictureBox1.Invalidate();
                }
            }
            else if(index_menu == 2)
            {
                eX = e.X;
                eY = e.Y;
            }
            else if(index_menu == 3)
            {
                pictureBox1.Paint -= pictureBox1_Paint;
                pictureBox1.Paint += Selection_Paint;
                orig = e.Location;
            }
            else if(index_menu == 4)
            {
                img_graphics = pictureBox1.Image;
                grBmp = Graphics.FromImage(img_graphics);
                eX = e.X;
                eY = e.Y;
            }
            else if(index_menu == 7)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    dragged = true;
                    listPoints.Clear();
                    first = e.Location;
                    listPoints.Add(e.Location);
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (index_menu == 1)
            {
                if (e.Button == MouseButtons.Left) //если нажата левая кнопка мыши (зажата)
                {
                    //юзер тянет фрагмент?
                    if (draggedFragment != null)//если есть фрагмент
                    {
                        //сдвигаем фрагмент
                        draggedFragment.Location.Offset(e.Location.X - mousePos2.X, e.Location.Y - mousePos2.Y);
                        mousePos1 = e.Location;
                    }
                    //сдвигаем выделенную область
                    mousePos2 = e.Location;
                    pictureBox1.Invalidate();
                }
                else
                {
                    mousePos1 = mousePos2 = e.Location;
                }
            }
            else if (index_menu == 2)
            {
                if (e.Button == MouseButtons.Left)
                {
                    grBmp.FillRectangle(pen.Brush, e.X, e.Y, 1, 1);
                    grBmp.DrawLine(pen, eX, eY, e.X, e.Y);
                    pictureBox1.Invalidate();
                    eX = e.X;
                    eY = e.Y;
                }
            }
            else if (index_menu == 3)
            {
                selRect = GetSelRectangle(orig, e.Location);
                if (e.Button == MouseButtons.Left)
                    pictureBox1.Invalidate();
            }
            else if (index_menu == 4)
            {
                if (e.Button == MouseButtons.Left)
                {
                    line.X = e.X;
                    line.Y = e.Y;
                    pictureBox1.Invalidate();
                }
            }
            else if(index_menu == 7)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Brush rf = new SolidBrush(Color.Black);
                    listPoints.Add(e.Location);
                    grBmp.FillRectangle(rf, e.X, e.Y, 1, 1);
                    pictureBox1.Invalidate();
                    eX = e.X;
                    eY = e.Y;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (index_menu == 1)
            {
                if (draggedFragment != null)
                {
                    if (flag == true)
                    {
                        draggedFragment.flag = true;
                    }
                    else
                    {
                        draggedFragment.flag = false;
                    }
                }

                //пользователь выделил фрагмент и отпустил мышь?
                if (mousePos1 != mousePos2)
                {
                    //создаем DraggedFragment
                    var rect = GetRect(mousePos1, mousePos2);//создаем прямоугольник по 2 точкам
                    draggedFragment = new DraggedFragment()
                    {
                        SourceRect = rect,
                        Location = rect.Location,
                        flag = true

                    };
                    //выделяем данную область. 
                }
                else
                {
                    //пользователь сдвинул фрагмент и отпутил мышь?
                    if (draggedFragment != null)
                    {
                        //фиксируем изменения в исходном изображении
                        draggedFragment.Fix(pictureBox1.Image);//метод Fix Класса Dragged.
                                                               //уничтожаем фрагмент
                        draggedFragment = null;
                        mousePos1 = mousePos2 = e.Location;//обнуляем коорды мыши
                    }
                }
                pictureBox1.Invalidate();
            }
            else if(index_menu == 3)
            {
                pictureBox1.Paint -= Selection_Paint;
                pictureBox1.Paint += pictureBox1_Paint;
                end = e.Location;
                pictureBox1.Invalidate();
            }
            else if(index_menu == 4)
            {
                grBmp.DrawLine(pen, eX, eY, line.X, line.Y);
                pictureBox1.Image = img_graphics;
                imgNow = pictureBox1.Image;

            }
            else if(index_menu == 7)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    last = e.Location;
                    if ((last.X >= first.X - 15 && last.X <= first.X + 15) && (last.Y >= first.Y - 15 && last.Y <= first.Y + 15))
                    {
                        listPoints.Add(e.Location);
                        cut();
                        lobz = false;
                        dragged = false;
                        Cursor = Cursors.Arrow;
                    }
                    else
                    {
                        lobz = false;
                        Cursor = Cursors.Arrow;
                        MessageBox.Show("Вырежьте фигуру от начальной точки с возвратом к ней же!");
                    }
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (index_menu == 1)
            {
                if (draggedFragment != null)
                {
                    if (flag == true)
                    {
                        //рисуем выеразанное белое место
                        e.Graphics.SetClip(draggedFragment.SourceRect);
                        e.Graphics.Clear(Color.White);
                    }

                    //рисуем сдвинутый фрагмент
                    e.Graphics.SetClip(draggedFragment.Rect);
                    e.Graphics.DrawImage(pictureBox1.Image, draggedFragment.Location.X - draggedFragment.SourceRect.X, draggedFragment.Location.Y - draggedFragment.SourceRect.Y);

                    //рисуем рамку
                    //e.Graphics.ResetClip();//рамка остается после отжатия кнопки мыши
                    ControlPaint.DrawFocusRectangle(e.Graphics, draggedFragment.Rect);
                }
                else
                {
                    //если выделяется область рисуем рамку с изменяющимся ее размером
                    if (mousePos1 != mousePos2)
                        ControlPaint.DrawFocusRectangle(e.Graphics, GetRect(mousePos1, mousePos2));
                }
            }
            else if(index_menu == 3)
            {
                e.Graphics.DrawRectangle(rect_border, selRect);
                if(flag == true)
                    e.Graphics.FillRectangle(rect_fill, selRect);
                if (flag == true)
                    grBmp.FillRectangle(rect_fill, selRect);
                else
                    grBmp.DrawRectangle(rect_border, selRect);
            }
            else if(index_menu == 4)
            {
                e.Graphics.DrawLine(pen, eX, eY, line.X, line.Y);
            }
        }

        private void Selection_Paint(object sender, PaintEventArgs e)
        {
            if (index_menu == 3)
            {
                e.Graphics.DrawRectangle(pen, selRect);
            }
        }

        void cut()
        {
            //int minX, maxX, minY, maxY;
            //minX = listPoints[0].X;
            //maxX = listPoints[0].X;
            //minY = listPoints[0].Y;
            //maxY = listPoints[0].Y;
            //for (int i=0; i<listPoints.Count; i++)
            //{
            //    if(listPoints[i].X < minX)
            //        minX = listPoints[i].X;
            //    if (listPoints[i].X > maxX)
            //        maxX = listPoints[i].X;
            //    if (listPoints[i].Y < minY)
            //        minY = listPoints[i].Y;
            //    if (listPoints[i].Y > maxY)
            //        maxY = listPoints[i].Y;
            //}
            Bitmap bmp = new Bitmap(600, 400);
            Graphics g = Graphics.FromImage(bmp);
            using (TextureBrush br = new TextureBrush(pictureBox1.Image))
            {
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddLines(listPoints.ToArray());
                    g.FillPath(br, gp);
                }
            }
            pictureBox1.Image = bmp;
        }

        Rectangle GetSelRectangle(Point orig, Point location)
        {
            int deltaX = location.X - orig.X, deltaY = location.Y - orig.Y;
            Size s = new Size(Math.Abs(deltaX), Math.Abs(deltaY));
            Rectangle rect = new Rectangle();
            if (deltaX >= 0 & deltaY >= 0)
                rect = new Rectangle(orig, s);
            if (deltaX < 0 & deltaY > 0)
                rect = new Rectangle(location.X, orig.Y, s.Width, s.Height);
            if (deltaX < 0 & deltaY < 0)
                rect = new Rectangle(location, s);
            if (deltaX > 0 & deltaY < 0)
                rect = new Rectangle(orig.X, location.Y, s.Width, s.Height);
            return rect;
        }

        Rectangle GetRect(Point p1, Point p2)
        {
            var x1 = Math.Min(p1.X, p2.X);
            var x2 = Math.Max(p1.X, p2.X);
            var y1 = Math.Min(p1.Y, p2.Y);
            var y2 = Math.Max(p1.Y, p2.Y);
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        class DraggedFragment
        {
            public Rectangle SourceRect;//фргмент в исходном изображении
            public Point Location;//положение сдвинутого фрагмента, точка верхнего левого угла
            public bool flag;

            public Rectangle Rect
            {
                get { return new Rectangle(Location, SourceRect.Size); }
            }

            public void Fix(Image image)
            {
                using (var clone = (Image)image.Clone())
                using (var gr = Graphics.FromImage(image))
                {
                    if (flag != false)
                    {
                        gr.SetClip(SourceRect);
                        gr.Clear(Color.White);
                    }

                    gr.SetClip(Rect);
                    gr.DrawImage(clone, Location.X - SourceRect.X, Location.Y - SourceRect.Y);//координаты верхнего левого угла
                }
            }
        }

        #endregion

        #region Среднее меню (кнопки)
        private void button3_Click(object sender, EventArgs e)
        {
            Histogram _edForm = new Histogram(pictureBox1.Image);
            _edForm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Bitmap red_eye = new Bitmap(pictureBox1.Image);
            Point orig = draggedFragment.SourceRect.Location;
            for (int i = orig.Y; i < draggedFragment.SourceRect.Height + orig.Y; i++)
                for (int j = orig.X; j < draggedFragment.SourceRect.Width + orig.X; j++)
                {
                    Color pixelColor = red_eye.GetPixel(j, i);
                    Color color = Color.FromArgb(25, pixelColor.G, pixelColor.B);
                    if (pixelColor.R > 100 && pixelColor.G < 100 && pixelColor.B < 100)
                        red_eye.SetPixel(j, i, color);
                }
            draggedFragment = null;
            pictureBox1.Image = red_eye;
            imgNow = pictureBox1.Image;

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            float width = float.Parse(comboBox2.Text);
            pen.Width = width;
            rect_border.Width = width;
        }
        #endregion

        #region Нижняя панель

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            radioButton1.Text = "Вырезать";
            radioButton2.Text = "Копировать";
            radioButton1.Checked = true;
            flag = true;
            index_menu = 1;
            toolStripButton1.Checked = true;
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton4.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            grBmp = Graphics.FromImage(pictureBox1.Image);
            index_menu = 2;
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = true;
            toolStripButton3.Checked = false;
            toolStripButton4.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            radioButton1.Text = "Заливка";
            radioButton2.Text = "Без заливки";
            radioButton1.Checked = true;
            flag = true;
            grBmp = Graphics.FromImage(pictureBox1.Image);
            index_menu = 3;
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = true;
            toolStripButton4.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //grBmp = Graphics.FromImage(pictureBox1.Image);
            index_menu = 4;
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton4.Checked = true;
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = false;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            index_menu = 5;
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton4.Checked = false;
            toolStripButton5.Checked = true;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = false;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            index_menu = 6;
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton4.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = true;
            toolStripButton7.Checked = false;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            index_menu = 7;
            toolStripButton1.Checked = false;
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton4.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton6.Checked = false;
            toolStripButton7.Checked = true;
        }

        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Editor_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                flag = true;
            else if (radioButton2.Checked == true)
                flag = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK)
                CurrentColor = colorDialog1.Color;
            pictureBox2.BackColor = CurrentColor;
            pen.Color = CurrentColor;
            rect_border.Color = CurrentColor;
            rect_fill = new SolidBrush(CurrentColor);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            color.R = CurrentColor.R;
            color.G = CurrentColor.G;
            color.B = CurrentColor.B;
            Color_model f = new Color_model();
            f.ShowDialog();
            f.Dispose();
            CurrentColor = Color.FromArgb(color.R, color.G, color.B);
            pictureBox2.BackColor = CurrentColor;
            pen.Color = CurrentColor;
            rect_border.Color = CurrentColor;
            rect_fill = new SolidBrush(CurrentColor);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Point orig = draggedFragment.SourceRect.Location;
            for (int i = orig.Y; i < draggedFragment.SourceRect.Height + orig.Y; i++)
                for (int j = orig.X; j < draggedFragment.SourceRect.Width + orig.X; j++)
                {
                    bmp.SetPixel(j, i, Color.White);
                }
            pictureBox1.Image = bmp;
            imgNow = pictureBox1.Image;
            draggedFragment = null;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap cropBmp = bmp.Clone(draggedFragment.SourceRect, bmp.PixelFormat);
            pictureBox1.Image = cropBmp;
            imgNow = pictureBox1.Image;
            draggedFragment = null;
            index_mass = 9;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Point orig = draggedFragment.SourceRect.Location;
            for (int i = orig.Y; i < draggedFragment.SourceRect.Height + orig.Y; i++)
                for (int j = orig.X; j < draggedFragment.SourceRect.Width + orig.X; j++)
                {
                    bmp.SetPixel(j, i, Color.White);
                }
            pictureBox1.Image = bmp;
            imgNow = pictureBox1.Image;
            draggedFragment = null;
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (index_mass > 0)
            {
                index_mass--;
                Image myBitmap = imgNow;
                this.pictureBox1.Size = new Size(myBitmap.Width, myBitmap.Height);
                Size nSize = new Size(Convert.ToInt32(imgNow.Width * zna4[index_mass]), Convert.ToInt32(imgNow.Height * zna4[index_mass]));
                Image gdi = new Bitmap(nSize.Width, nSize.Height);
                Graphics ZoomInGraphics = Graphics.FromImage(gdi);
                ZoomInGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                ZoomInGraphics.DrawImage(imgNow, new Rectangle(new Point(0, 0), nSize), new Rectangle(new Point(0, 0), imgNow.Size), GraphicsUnit.Pixel);
                ZoomInGraphics.Dispose();
                pictureBox1.Image = gdi;
                pictureBox1.Size = gdi.Size;
                //grBmp = Graphics.FromImage(pictureBox1.Image);
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (index_mass < 18)
            {
                index_mass++;
                Image myBitmap = imgNow;
                this.pictureBox1.Size = new Size(myBitmap.Width, myBitmap.Height);
                Size nSize = new Size(Convert.ToInt32(imgNow.Width * zna4[index_mass]), Convert.ToInt32(imgNow.Height * zna4[index_mass]));
                Image gdi = new Bitmap(nSize.Width, nSize.Height);
                Graphics ZoomInGraphics = Graphics.FromImage(gdi);
                ZoomInGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                ZoomInGraphics.DrawImage(imgNow, new Rectangle(new Point(0, 0), nSize), new Rectangle(new Point(0, 0), imgNow.Size), GraphicsUnit.Pixel);
                ZoomInGraphics.Dispose();
                pictureBox1.Image = gdi;
                pictureBox1.Size = gdi.Size;
                //grBmp = Graphics.FromImage(pictureBox1.Image);
            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK)
                CurrentColor = colorDialog1.Color;
            pictureBox2.BackColor = CurrentColor;
            pen.Color = CurrentColor;
            rect_border.Color = CurrentColor;
            rect_fill = new SolidBrush(CurrentColor);
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            color.R = CurrentColor.R;
            color.G = CurrentColor.G;
            color.B = CurrentColor.B;
            Color_model f = new Color_model();
            f.ShowDialog();
            f.Dispose();
            CurrentColor = Color.FromArgb(color.R, color.G, color.B);
            pictureBox2.BackColor = CurrentColor;
            pen.Color = CurrentColor;
            rect_border.Color = CurrentColor;
            rect_fill = new SolidBrush(CurrentColor);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            Histogram _edForm = new Histogram(pictureBox1.Image);
            _edForm.ShowDialog();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            imgPrev = pictureBox1.Image;
            Bitmap red_eye = new Bitmap(pictureBox1.Image);
            Point orig = draggedFragment.SourceRect.Location;
            for (int i = orig.Y; i < draggedFragment.SourceRect.Height + orig.Y; i++)
                for (int j = orig.X; j < draggedFragment.SourceRect.Width + orig.X; j++)
                {
                    Color pixelColor = red_eye.GetPixel(j, i);
                    Color color = Color.FromArgb(25, pixelColor.G, pixelColor.B);
                    if (pixelColor.R > 100 && pixelColor.G < 100 && pixelColor.B < 100)
                        red_eye.SetPixel(j, i, color);
                }
            draggedFragment = null;
            pictureBox1.Image = red_eye;
            imgNow = pictureBox1.Image;
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK)
                CurrentColor = colorDialog1.Color;
            pictureBox2.BackColor = CurrentColor;
            pen.Color = CurrentColor;
            rect_border.Color = CurrentColor;
            rect_fill = new SolidBrush(CurrentColor);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                flag = true;
            else if (radioButton2.Checked == true)
                flag = false;
        }

        private void brush(Point start) //заливка
        {
            Color br_clr = Color.Black;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Stack<Point> stck = new Stack<Point>();
            Color clr = bmp.GetPixel(start.X, start.Y);
            stck.Push(new Point(start.X + 1, start.Y));
            stck.Push(new Point(start.X + 1, start.Y - 1));
            stck.Push(new Point(start.X + 1, start.Y + 1));
            stck.Push(new Point(start.X - 1, start.Y));
            stck.Push(new Point(start.X - 1, start.Y - 1));
            stck.Push(new Point(start.X - 1, start.Y + 1));
            stck.Push(new Point(start.X, start.Y - 1));
            stck.Push(new Point(start.X, start.Y + 1));
            while (stck.Count > 0)
            {
                Point p = stck.Pop();
                if ((p.X > 0 && p.X < bmp.Width)&&(p.Y > 0 && p.Y < bmp.Height))
                    br_clr = bmp.GetPixel(p.X, p.Y);
                if(br_clr == clr)
                {
                    bmp.SetPixel(p.X, p.Y, CurrentColor);
                    stck.Push(new Point(p.X + 1, p.Y));
                    stck.Push(new Point(p.X + 1, p.Y - 1));
                    stck.Push(new Point(p.X + 1, p.Y + 1));
                    stck.Push(new Point(p.X - 1, p.Y));
                    stck.Push(new Point(p.X - 1, p.Y - 1));
                    stck.Push(new Point(p.X - 1, p.Y + 1));
                    stck.Push(new Point(p.X, p.Y - 1));
                    stck.Push(new Point(p.X, p.Y + 1));
                }
            }
            pictureBox1.Image = bmp;
        } 
    }
}

