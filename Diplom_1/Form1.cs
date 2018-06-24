using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Diplom_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            open();
        }
        private string path_d; //путь каталога
        private List<string> files = new List<string>(); //пути всех файлов в каталоге
        private string pict; //путь с именем текущего изображения
        private string form_name = "KMP Viewer";

        private Bitmap image1; //основное изображение.
        private Bitmap image1_copy;//копия

        private bool Application_start = false; //Флаг для проверки, стартовало ли уже приложение. True - если да.
        private bool image_open = false; //Флаг открытия файла
        private bool flag_fullscreen = false; //Флаг обозначающий находится ли приложение в полноэкранном режиме
        private bool start_time = false; //
        private Color clr;

        private int pict_w;
        private int pict_h;
        private int index_mass = 9;//индекс массива масштабирования
        //Массив масштабирования
        private double[] zna4 = new double[19] { 7.45, 5.960, 4.768, 3.814, 3.051, 2.441, 1.953, 1.562, 1.25, 1, 0.75, 0.562, 0.421, 0.316, 0.237, 0.177, 0.133, 0.1, 0.075 }; //+-25%

        private System.Drawing.Size sz_form;
        private System.Drawing.Size sz_panel;
        private System.Threading.Timer time = null;

        #region Функции кнопок

        void PrintTime(object state) //для таймера
        {
            right();
        }

        private void delete()
        {
            image1.Dispose();
            image1_copy.Dispose();
            int indexArraysPath = files.IndexOf(pict);
            right();
            File.Delete(files[indexArraysPath]);
            files.RemoveAt(indexArraysPath);
        }

        private void expand() //Развернуть
        {
            if (flag_fullscreen == false) //на весь экран
            {
                sz_form = this.Size;
                sz_panel = panel1.Size;
                int w, h;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                w = this.Width; h = this.Height;
                flag_fullscreen = true;
                pict_edit.Visible = false;
                pict_minus.Visible = false;
                pict_plus.Visible = false;
                pict_left.Visible = false;
                pict_right.Visible = false;
                pict_expand.Visible = false;
                pict_open.Visible = false;
                pict_timer.Visible = false;
                pict_rot_lef.Visible = false;
                pict_rot_rig.Visible = false;
                pict_panel.Visible = false;
                clr = panel1.BackColor;
                panel1.BackColor = Color.Black;
                panel1.Location = new Point(0, 0);
                panel1.Size = this.Size;
                pictureBox1.Size = new Size(w, h);
                pict_h = pictureBox1.Height;
                pict_w = pictureBox1.Width;
                pictureBox1.Image = image1;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                index_mass = 9;

            }
            else //обратно
            {
                panel1.AutoScrollPosition = new Point(0, 0);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                this.Size = sz_form;
                panel1.Size = sz_panel;
                panel1.Location = new Point(0, 0);
                panel1.BackColor = clr;
                pictureBox1.Size = new Size(panel1.Width, panel1.Height);
                flag_fullscreen = false;

                pict_h = pictureBox1.Height;
                pict_w = pictureBox1.Width;
                pictureBox1.Image = image1;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                index_mass = 9;

                pict_edit.Visible = true;
                pict_minus.Visible = true;
                pict_plus.Visible = true;
                pict_left.Visible = true;
                pict_right.Visible = true;
                pict_expand.Visible = true;
                pict_open.Visible = true;
                pict_timer.Visible = true;
                pict_rot_lef.Visible = true;
                pict_rot_rig.Visible = true;
                pict_panel.Visible = true;
            }
        }

        private void right()
        {
            if (image_open == true)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Height = pict_h;
                pictureBox1.Width = pict_w;
                int indexArraysPath = files.IndexOf(pict);
                if (indexArraysPath == files.Count - 1)
                {
                    indexArraysPath = -1;
                }
                image1 = new Bitmap(files[indexArraysPath + 1]);
                pictureBox1.Image = image1;
                pict = files[indexArraysPath + 1];
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                string temp = "";
                temp = files[indexArraysPath + 1];
                int k = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] == '\\')
                        k = i;
                }
                temp = temp.Substring(k + 1);
                this.Text = form_name + "   " + temp;
                image1_copy = image1;
                index_mass = 9;
            }
        }  //Следующее изображение

        private void left()
        {
            if (image_open == true)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Height = pict_h;
                pictureBox1.Width = pict_w;
                int indexArraysPath = files.IndexOf(pict);
                if (indexArraysPath == 0)
                {
                    indexArraysPath = files.Count;
                }
                image1 = new Bitmap(files[indexArraysPath - 1]);
                pictureBox1.Image = image1;
                pict = files[indexArraysPath - 1];
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                string temp = "";
                temp = files[indexArraysPath - 1];
                int k = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] == '\\')
                        k = i;
                }
                temp = temp.Substring(k + 1);
                this.Text = form_name + "   " + temp;
                image1_copy = image1;
                index_mass = 9;
            }
        } //Предыдущее изображение

        private void plus()
        {
            if (image_open == true)
            {
                if (index_mass > 0) //если еще есть куда увеличивать
                {
                    index_mass--; //уменьшаем индекс
                    if (index_mass == 9)
                    {
                        pictureBox1.Image = null;
                        pictureBox1.Height = pict_h;
                        pictureBox1.Width = pict_w;
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox1.Image = image1;
                    }
                    else
                    {
                        pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                        double wd = pict_w * zna4[index_mass];
                        double hght = pict_h * zna4[index_mass];
                        Size size = new Size(Convert.ToInt32(wd), Convert.ToInt32(hght)); //Увеличиваем на 25%
                        pictureBox1.Size = size;
                        if (index_mass > 9)
                        {
                            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                        }
                        else
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
        } //Увеличение

        private void minus()
        {
            if (image_open == true)
            {
                if (index_mass < 18)
                {
                    index_mass++;
                    if (index_mass == 9)
                    {
                        pictureBox1.Image = null;
                        pictureBox1.Height = pict_h;
                        pictureBox1.Width = pict_w;
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox1.Image = image1;
                    }
                    else
                    {         
                        double wd = pict_w * zna4[index_mass];
                        double hght = pict_h * zna4[index_mass];
                        Size size = new Size(Convert.ToInt32(wd), Convert.ToInt32(hght)); //Уменьшаем на 25%
                        pictureBox1.Size = size;
                        if (index_mass > 9)
                        {
                            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                        }
                        else
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
        } //Уменьшение

        private void open2(string path)
        {
            try
            {
                image1 = new Bitmap(path);
                pictureBox1.Image = image1;
                path_d = Path.GetDirectoryName(path);
                string[] extensions = { ".jpg", ".png", ".bmp", ".jpeg" };
                files = Directory.GetFiles(path_d, "*.*").Where(f => extensions.Contains(System.IO.Path.GetExtension(f).ToLower())).ToList<string>();
                pict = path;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                string temp = "";
                temp = path;
                int k = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] == '\\')
                        k = i;
                }
                temp = temp.Substring(k + 1);
                this.Text = form_name + "   " + temp;
                Size size = pictureBox1.Size;
                image1_copy = new Bitmap(image1);
                pict_h = pictureBox1.Height;
                pict_w = pictureBox1.Width;
                image_open = true;
            }
            catch
            {
                MessageBox.Show("Неверный формат файла. Выберите графический файл .JPG .PNG или .BMP");
            }
        } 

        private void open()
        {
            string[] args;
            args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && Application_start == false) // если запуск производился через файл
            {
                open2(args[1]);
                pict = args[1];
            }
            else if (Application_start == true)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Открыть изображение";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    open2(ofd.FileName);
                    pict = ofd.FileName;
                }
                ofd.Dispose();
            }
            Application_start = true;
        }

        private void rotate_left()
        {
            pict_rot_lef.Image = Properties.Resources.повернуть_вправо1 as Bitmap;
            if (image_open)
            {
                image1.RotateFlip(RotateFlipType.Rotate270FlipXY);
                pictureBox1.Image = image1;
            }
        } //Поворот изображениея влево

        private void rotate_right()
        {
            pict_rot_rig.Image = Properties.Resources.повернуть_влево1 as Bitmap;
            if (image_open)
            {
                image1.RotateFlip(RotateFlipType.Rotate90FlipXY);
                pictureBox1.Image = image1;
            }
        } //Поворот изображения вправо

        #endregion

        #region Таймер
        private void pict_timer_Click(object sender, EventArgs e)
        {
            string tim;
            if (start_time == false)
            {
                pict_timer.Image = Properties.Resources.стоп as Bitmap;
                Slideshow _qForm = new Slideshow { Owner = this };
                _qForm.ShowDialog();
                tim = Data.Value;
                tim += "000";
                TimerCallback timeCB = new TimerCallback(PrintTime);
                time = new System.Threading.Timer(timeCB, null, Convert.ToUInt16(tim), Convert.ToUInt16(tim));
                start_time = true;
            }
            else
            {
                pict_timer.Image = Properties.Resources.воспроизвести as Bitmap;
                time.Dispose();
                start_time = false;
            }
        }
        #endregion

        #region Кнопки
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Right || keyData == Keys.Space)
            {
                right();
                return true; 
            }
            else if(keyData == Keys.Left)
            {
                left();
                return true;
            }
            else if (keyData == Keys.Up || keyData == Keys.Oemplus)
            {
                plus();
                return true;
            }
            else if (keyData == Keys.Down || keyData == Keys.OemMinus)
            {
                minus();
                return true;
            }
            else if (keyData == Keys.Oemcomma)
            {
                rotate_left();
                return true;
            }
            else if (keyData == Keys.OemPeriod)
            {
                rotate_right();
                return true;
            }
            else if (keyData == Keys.Escape) //Кнопка развернуть
            {
                expand();
                return true;
            }
            else
                return false;
        }

        #endregion

        #region Изменение полос прокрутки мышкой

        bool Selected = false;
        Point move;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Selected = true;
                move = new Point(e.X, e.Y);//расстояние от того места где мы тыкнули кнопкой до новой позиции мышки при перетаскивании
                this.Cursor = Cursors.Hand;
                x = e.X;
            }
        }
        int x = 0;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Selected)
            {
                this.Cursor = Cursors.Hand;
                Point drag = new Point((move.X - e.X), (move.Y - e.Y));
                panel1.AutoScrollPosition = new Point((drag.X - panel1.AutoScrollPosition.X), (drag.Y - panel1.AutoScrollPosition.Y));

            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            Selected = false;
        }
        #endregion

        #region События

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Size asd = new Size(panel1.Width, panel1.Height);
            pictureBox1.Size = asd;
            pict_h = pictureBox1.Height;
            pict_w = pictureBox1.Width;
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            expand();
        }

        private void pict_expand_Click(object sender, EventArgs e)
        {
            expand();
        }

        private void pict_open_MouseDown(object sender, MouseEventArgs e)
        {
            pict_open.Image = Properties.Resources.выбор_папки1 as Bitmap;
            if (e.Button == MouseButtons.Left)
                open();
        }

        private void pict_open_MouseUp(object sender, MouseEventArgs e)
        {
            pict_open.Image = Properties.Resources.выбор_папки as Bitmap;
        }

        private void pict_expand_MouseDown(object sender, MouseEventArgs e)
        {
            pict_expand.Image = Properties.Resources.развернуть1 as Bitmap;
        }

        private void pict_expand_MouseUp(object sender, MouseEventArgs e)
        {
            pict_expand.Image = Properties.Resources.развернуть as Bitmap;
        }

        private void pict_plus_MouseDown(object sender, MouseEventArgs e)
        {
            pict_plus.Image = Properties.Resources.увеличить1 as Bitmap;
            if (e.Button == MouseButtons.Left)
                plus();
        }

        private void pict_plus_MouseUp(object sender, MouseEventArgs e)
        {
            pict_plus.Image = Properties.Resources.увеличить as Bitmap;
        }

        private void pict_minus_MouseDown(object sender, MouseEventArgs e)
        {
            pict_minus.Image = Properties.Resources.уменьшить1 as Bitmap;
            if (e.Button == MouseButtons.Left)
                minus();
        }

        private void pict_minus_MouseUp(object sender, MouseEventArgs e)
        {
            pict_minus.Image = Properties.Resources.уменьшить as Bitmap;
        }

        private void pict_left_MouseDown(object sender, MouseEventArgs e)
        {
            pict_left.Image = Properties.Resources.влево1 as Bitmap;
            if (e.Button == MouseButtons.Left)
                left();
        }

        private void pict_left_MouseUp(object sender, MouseEventArgs e)
        {
            pict_left.Image = Properties.Resources.влево as Bitmap;
        }

        private void pict_right_MouseDown(object sender, MouseEventArgs e)
        {
            pict_right.Image = Properties.Resources.вправо1 as Bitmap;
            if (e.Button == MouseButtons.Left)
                right();
        }

        private void pict_right_MouseUp(object sender, MouseEventArgs e)
        {
            pict_right.Image = Properties.Resources.вправо as Bitmap;
        }

        private void pict_edit_MouseDown(object sender, MouseEventArgs e)
        {
            pict_edit.Image = Properties.Resources.редактирование1 as Bitmap;
            if (image_open)
            {
                Editor _edForm = new Editor(image1, pict);
                this.Hide();
                _edForm.ShowDialog();
                this.Close();
            }
        }

        private void pict_edit_MouseUp(object sender, MouseEventArgs e)
        {
            pict_edit.Image = Properties.Resources.редактирование as Bitmap;
        }

        private void pict_rot_rig_MouseDown(object sender, MouseEventArgs e)
        {
            rotate_right();
        }

        private void pict_rot_rig_MouseUp(object sender, MouseEventArgs e)
        {
            pict_rot_rig.Image = Properties.Resources.повернуть_влево as Bitmap;
        }
   
        private void pict_rot_lef_MouseDown(object sender, MouseEventArgs e)
        {
            rotate_left();
        }

        private void pict_rot_lef_MouseUp(object sender, MouseEventArgs e)
        {
            pict_rot_lef.Image = Properties.Resources.повернуть_вправо as Bitmap;
        }

        private void pict_timer_MouseDown(object sender, MouseEventArgs e)
        {
            if(start_time==false)
                pict_timer.Image = Properties.Resources.воспроизвести1 as Bitmap;
            else
                pict_timer.Image = Properties.Resources.стоп1 as Bitmap;
        }

        private void pict_timer_MouseUp(object sender, MouseEventArgs e)
        {
            if (start_time == false)
                pict_timer.Image = Properties.Resources.воспроизвести as Bitmap;
            else
                pict_timer.Image = Properties.Resources.стоп as Bitmap;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Size asd = new Size(panel1.Width, panel1.Height);
            pictureBox1.Size = asd;
            pictureBox1.Location = new Point(0, 0);
            pict_h = pictureBox1.Height;
            pict_w = pictureBox1.Width;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if(image_open)
                expand();
        }

        private void pict_delete_MouseDown(object sender, MouseEventArgs e)
        {
            pict_delete.Image = Properties.Resources.delete_click as Bitmap;
            this.delete();
        }

        private void pict_delete_MouseUp(object sender, MouseEventArgs e)
        {
            pict_delete.Image = Properties.Resources.delete as Bitmap;
        }
        #endregion
    }
}
