using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom_1
{
    public partial class Histogram : Form
    {
        public Histogram(Image source_bmp)
        {
            InitializeComponent();
            bmp = source_bmp;
            pictureBox2.Image = Histogramm(bmp);
        }
        Image bmp;

        private Image Histogramm(Image image) // гистограмма яркости
        {
            int width = 768, height = 600; //определяем размеры гистограммы

            Bitmap picture = new Bitmap(image);

            Bitmap img = new Bitmap(width, height); //создаем гистограмму
            // создаем массивы c количеством повторений для каждого из значений
            int[] R = new int[256];
            int[] G = new int[256];
            int[] B = new int[256];
            int i, j;
            Color color;
            // считываем цвета пикселей изображения
            for (i = 0; i < picture.Width; ++i)
                for (j = 0; j < picture.Height; ++j)
                {
                    color = picture.GetPixel(i, j);
                    ++R[color.R];
                    ++G[color.G];
                    ++B[color.B];
                }
            // находим самый высокий столбец, чтобы корректно масштабировать гистограмму по высоте
            int max = 0;
            for (i = 0; i < 256; ++i)
            {
                if (R[i] > max)
                    max = R[i];
                if (G[i] > max)
                    max = G[i];
                if (B[i] > max)
                    max = B[i];
            }
            // определяем коэффициент масштабирования по высоте
            double p = (double)max / height;
            // рисуем масштабированную гистограмму
            for (i = 0; i < width - 3; ++i)
            {
                for (j = height - 1; j > height - R[i / 3] / p; --j)
                {
                    img.SetPixel(i, j, Color.Red);
                }
                ++i;
                for (j = height - 1; j > height - G[i / 3] / p; --j)
                {
                    img.SetPixel(i, j, Color.Green);

                }
                ++i;
                for (j = height - 1; j > height - B[i / 3] / p; --j)
                {
                    img.SetPixel(i, j, Color.Blue);
                }
                ++i;
            }
            return img;
        }
    }
}
