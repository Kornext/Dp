using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom_1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            //MessageBox.Show(Convert.ToString(args.Length));
            //foreach (string ar in args)
            //{
            //    MessageBox.Show(ar);
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }

    static class Data
    {
        public static string Value { get; set; }
    }

    static class scl
    {
        public static int height { get; set; }
        public static int width { get; set; }
    }

    static class rotate
    {
        public static int angl { get; set; }
    }

    static class color
    {
        public static byte R { get; set; }
        public static byte G { get; set; }
        public static byte B { get; set; }
    }
}
