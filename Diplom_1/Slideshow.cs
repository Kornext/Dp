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
    public partial class Slideshow : Form
    {
        public Slideshow()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
        }
        bool flag = false;

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = false;
            for(int i=0; i<textBox1.Text.Length; i++)
            {
                //if(textBox1.Text[i] != '1' || textBox1.Text[i] != '2')
                //{
                //    flag = true;
                //}
            }
            if (flag)
                MessageBox.Show("Введите данные корректно!");
            else
            {
                Data.Value = textBox1.Text;
                this.Close();
            }
            //Timer time = new Timer();
            //time.Interval = 5000;
            //time.Elapsed

        }
    }
}
