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
    public partial class Scale : Form
    {
        public Scale()
        {
            InitializeComponent();
            initial();
            flag = true;
        }
        private double pw = 0, ph = 0;
        bool flag = false;

        private void initial()
        {
            double h = scl.height;
            double w = scl.width;
            if (h > w)
            {
                pw = 1;
                ph = h / w;
            }
            else if (h < w)
            {
                pw = w / h;
                ph = 1;
            }
            else
            {
                pw = 1;
                ph = 1;
            }
            textBox2.Text = Convert.ToString(w);
            textBox1.Text = Convert.ToString(h);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (flag == true && checkBox1.Checked)
            {
                int resalt;
                if (textBox1.Text == "")
                    textBox2.Text = "";
                else
                {
                    if (textBox1.Focused)
                    {
                        if (ph > pw)
                        {
                            resalt = Convert.ToInt16(Convert.ToDouble(textBox1.Text) / ph);
                        }
                        else if (ph < pw)
                        {
                            resalt = Convert.ToInt16(Convert.ToDouble(textBox1.Text) * pw);
                        }
                        else
                        {
                            resalt = Convert.ToInt16(Convert.ToDouble(textBox1.Text));
                        }
                        textBox2.Text = Convert.ToString(resalt);
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (flag == true && checkBox1.Checked)
            {
                int resalt;
                if (textBox2.Text == "")
                    textBox1.Text = "";
                else
                {
                    if (textBox2.Focused)
                    {
                        if (ph > pw)
                        {
                            resalt = Convert.ToInt16(Convert.ToDouble(textBox2.Text) * ph);
                        }
                        else if (ph < pw)
                        {
                            resalt = Convert.ToInt16(Convert.ToDouble(textBox2.Text) / pw);
                        }
                        else
                        {
                            resalt = Convert.ToInt16(Convert.ToDouble(textBox2.Text));
                        }
                        textBox1.Text = Convert.ToString(resalt);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            scl.height = Convert.ToInt16(textBox1.Text);
            scl.width = Convert.ToInt16(textBox2.Text);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
