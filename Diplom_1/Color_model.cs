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
    public partial class Color_model : Form
    {
        public Color_model()
        {
            InitializeComponent();
            ad();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private bool updating = false;

        #region Windows Form Designer generated code
        private void ad()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
            this.redUD.Maximum = new System.Decimal(new int[] {
                                                                  255,
                                                                  0,
                                                                  0,
                                                                  0});
            this.redUD.Name = "redUD";
            this.redUD.Size = new System.Drawing.Size(64, 20);
            this.redUD.TabIndex = 1;
            this.redUD.Value = new System.Decimal(new int[] {
                                                                255,
                                                                0,
                                                                0,
                                                                0});
            this.redUD.ValueChanged += new System.EventHandler(this.RGBValueChanged);
            this.greenUD.Maximum = new System.Decimal(new int[] {
                                                                    255,
                                                                    0,
                                                                    0,
                                                                    0});
            this.greenUD.Name = "greenUD";
            this.greenUD.Size = new System.Drawing.Size(64, 20);
            this.greenUD.TabIndex = 3;
            this.greenUD.ValueChanged += new System.EventHandler(this.RGBValueChanged);
            this.blueUD.Maximum = new System.Decimal(new int[] {
                                                                   255,
                                                                   0,
                                                                   0,
                                                                   0});
            this.blueUD.Name = "blueUD";
            this.blueUD.Size = new System.Drawing.Size(64, 20);
            this.blueUD.TabIndex = 5;
            this.blueUD.ValueChanged += new System.EventHandler(this.RGBValueChanged);
            this.satUD.Name = "satUD";
            this.satUD.Size = new System.Drawing.Size(64, 20);
            this.satUD.TabIndex = 3;
            this.satUD.ValueChanged += new System.EventHandler(this.HSLValueChanged);
            this.hueUD.Maximum = new System.Decimal(new int[] {
                                                                  360,
                                                                  0,
                                                                  0,
                                                                  0});
            this.hueUD.Name = "hueUD";
            this.hueUD.Size = new System.Drawing.Size(64, 20);
            this.hueUD.TabIndex = 1;
            this.hueUD.ValueChanged += new System.EventHandler(this.HSLValueChanged);
            this.lumUD.Name = "lumUD";
            this.lumUD.Size = new System.Drawing.Size(64, 20);
            this.lumUD.TabIndex = 5;
            this.lumUD.ValueChanged += new System.EventHandler(this.HSLValueChanged);
            this.blackUD.Name = "blackUD";
            this.blackUD.Size = new System.Drawing.Size(64, 20);
            this.blackUD.TabIndex = 7;
            this.blackUD.ValueChanged += new System.EventHandler(this.CMYKValueChanged);
            this.magentaUD.Name = "magentaUD";
            this.magentaUD.Size = new System.Drawing.Size(64, 20);
            this.magentaUD.TabIndex = 3;
            this.magentaUD.ValueChanged += new System.EventHandler(this.CMYKValueChanged);
            this.cyanUD.Name = "cyanUD";
            this.cyanUD.Size = new System.Drawing.Size(64, 20);
            this.cyanUD.TabIndex = 0;
            this.cyanUD.ValueChanged += new System.EventHandler(this.CMYKValueChanged);
            this.yellowUD.Name = "yellowUD";
            this.yellowUD.Size = new System.Drawing.Size(64, 20);
            this.yellowUD.TabIndex = 5;
            this.yellowUD.ValueChanged += new System.EventHandler(this.CMYKValueChanged);
            this.uUD.Maximum = new System.Decimal(new int[] {
                                                                87,
                                                                0,
                                                                0,
                                                                0});
            this.uUD.Name = "uUD";
            this.uUD.Size = new System.Drawing.Size(64, 20);
            this.uUD.TabIndex = 3;
            this.uUD.ValueChanged += new System.EventHandler(this.YUVValueChanged);
            this.yUD.Name = "yUD";
            this.yUD.Size = new System.Drawing.Size(64, 20);
            this.yUD.TabIndex = 1;
            this.yUD.ValueChanged += new System.EventHandler(this.YUVValueChanged);
            this.vUD.Maximum = new System.Decimal(new int[] {
                                                                123,
                                                                0,
                                                                0,
                                                                0});
            this.vUD.Name = "vUD";
            this.vUD.Size = new System.Drawing.Size(64, 20);
            this.vUD.TabIndex = 5;
            this.vUD.ValueChanged += new System.EventHandler(this.YUVValueChanged);
            this.sUD.Name = "sUD";
            this.sUD.Size = new System.Drawing.Size(64, 20);
            this.sUD.TabIndex = 3;
            this.sUD.ValueChanged += new System.EventHandler(this.HSBValueChanged);
            this.hUD.Maximum = new System.Decimal(new int[] {
                                                                360,
                                                                0,
                                                                0,
                                                                0});
            this.hUD.Name = "hUD";
            this.hUD.Size = new System.Drawing.Size(64, 20);
            this.hUD.TabIndex = 1;
            this.hUD.ValueChanged += new System.EventHandler(this.HSBValueChanged);
            this.bUD.Name = "bUD";
            this.bUD.Size = new System.Drawing.Size(64, 20);
            this.bUD.TabIndex = 5;
            this.bUD.ValueChanged += new System.EventHandler(this.HSBValueChanged);
            this.preview.BackColor = System.Drawing.Color.Red;
            this.preview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.preview.Name = "preview";
            this.preview.TabIndex = 5;
            this.Load += new System.EventHandler(this.Color_model_Load);
            ((System.ComponentModel.ISupportInitialize)(this.redUD)).EndInit();
            this.ResumeLayout(false);
            redUD.Value = color.R;
            blueUD.Value = color.B;
            greenUD.Value = color.G;
        }
        #endregion

        #region Handlers
        private void RGBValueChanged(object sender, System.EventArgs e)
        {
            if (!this.updating)
            {
                this.preview.BackColor = Color.FromArgb((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value);

                this.updating = true;
                UpdateHSL(ColorSpaceHelper.RGBtoHSL((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateHSB(ColorSpaceHelper.RGBtoHSB((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateCMYK(ColorSpaceHelper.RGBtoCMYK((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateYUV(ColorSpaceHelper.RGBtoYUV((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                this.updating = false;
            }
        }

        private void HSLValueChanged(object sender, System.EventArgs e)
        {
            if (!this.updating)
            {
                this.updating = true;
                UpdateRGB(ColorSpaceHelper.HSLtoRGB((double)this.hueUD.Value, (double)this.satUD.Value / 100.0, (double)this.lumUD.Value / 100.0));
                UpdateHSB(ColorSpaceHelper.RGBtoHSB((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateCMYK(ColorSpaceHelper.RGBtoCMYK((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateYUV(ColorSpaceHelper.RGBtoYUV((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                this.updating = false;
            }
        }

        private void HSBValueChanged(object sender, System.EventArgs e)
        {
            if (!this.updating)
            {
                this.updating = true;
                UpdateRGB(ColorSpaceHelper.HSBtoRGB((double)this.hUD.Value, (double)this.sUD.Value / 100.0, (double)this.bUD.Value / 100.0));
                UpdateHSL(ColorSpaceHelper.RGBtoHSL((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateCMYK(ColorSpaceHelper.RGBtoCMYK((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateYUV(ColorSpaceHelper.RGBtoYUV((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                this.updating = false;
            }
        }

        private void CMYKValueChanged(object sender, System.EventArgs e)
        {
            if (!this.updating)
            {
                this.updating = true;
                UpdateRGB(ColorSpaceHelper.CMYKtoRGB((double)this.cyanUD.Value / 100.0, (double)this.magentaUD.Value / 100.0, (double)this.yellowUD.Value / 100.0, (double)this.blackUD.Value / 100.0));
                UpdateHSL(ColorSpaceHelper.RGBtoHSL((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateHSB(ColorSpaceHelper.RGBtoHSB((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateYUV(ColorSpaceHelper.RGBtoYUV((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                this.updating = false;
            }
        }

        private void YUVValueChanged(object sender, System.EventArgs e)
        {
            if (!this.updating)
            {
                this.updating = true;
                UpdateRGB(ColorSpaceHelper.YUVtoRGB((double)this.yUD.Value / 100.0, (-0.436 + ((double)this.uUD.Value / 100.0)), (-0.615 + ((double)this.vUD.Value / 100.0))));
                UpdateCMYK(ColorSpaceHelper.RGBtoCMYK((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateHSL(ColorSpaceHelper.RGBtoHSL((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                UpdateHSB(ColorSpaceHelper.RGBtoHSB((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
                this.updating = false;
            }
        }

        #endregion

        #region Updates
        private void UpdateRGB(RGB rgb)
        {
            if (Convert.ToInt32(redUD.Value) != rgb.Red) this.redUD.Value = rgb.Red;
            if (Convert.ToInt32(greenUD.Value) != rgb.Green) this.greenUD.Value = rgb.Green;
            if (Convert.ToInt32(blueUD.Value) != rgb.Blue) this.blueUD.Value = rgb.Blue;

            this.preview.BackColor = Color.FromArgb(rgb.Red, rgb.Green, rgb.Blue);
        }

        private void UpdateHSL(HSL hsl)
        {
            if (Convert.ToInt32(hsl.Hue) != (int)this.hueUD.Value) this.hueUD.Value = Convert.ToInt32(hsl.Hue);
            if (Convert.ToInt32(hsl.Saturation * 100) != (int)this.satUD.Value) this.satUD.Value = Convert.ToInt32(hsl.Saturation * 100);
            if (Convert.ToInt32(hsl.Luminance * 100) != (int)this.lumUD.Value) this.lumUD.Value = Convert.ToInt32(hsl.Luminance * 100);
        }

        private void UpdateHSB(HSB hsb)
        {
            if (Convert.ToInt32(hsb.Hue) != (int)this.hUD.Value) this.hUD.Value = Convert.ToInt32(hsb.Hue);
            if (Convert.ToInt32(hsb.Saturation * 100) != (int)this.sUD.Value) this.sUD.Value = Convert.ToInt32(hsb.Saturation * 100);
            if (Convert.ToInt32(hsb.Brightness * 100) != (int)this.bUD.Value) this.bUD.Value = Convert.ToInt32(hsb.Brightness * 100);
        }

        private void UpdateCMYK(CMYK cmyk)
        {
            if (Convert.ToInt32(cmyk.Cyan * 100) != (int)this.cyanUD.Value) this.cyanUD.Value = Convert.ToInt32(cmyk.Cyan * 100);
            if (Convert.ToInt32(cmyk.Magenta * 100) != (int)this.magentaUD.Value) this.magentaUD.Value = Convert.ToInt32(cmyk.Magenta * 100);
            if (Convert.ToInt32(cmyk.Yellow * 100) != (int)this.yellowUD.Value) this.yellowUD.Value = Convert.ToInt32(cmyk.Yellow * 100);
            if (Convert.ToInt32(cmyk.Black * 100) != (int)this.blackUD.Value) this.blackUD.Value = Convert.ToInt32(cmyk.Black * 100);
        }

        private void UpdateYUV(YUV yuv)
        {
            if (Convert.ToInt32(yuv.Y * 100) != (int)this.yUD.Value) this.yUD.Value = Convert.ToInt32(yuv.Y * 100);
            if (Convert.ToInt32((yuv.U + 0.436) * 100) != (int)this.uUD.Value) this.uUD.Value = Convert.ToInt32((yuv.U + 0.436) * 100);
            if (Convert.ToInt32((yuv.V + 0.615) * 100) != (int)this.vUD.Value) this.vUD.Value = Convert.ToInt32((yuv.V + 0.615) * 100);
        }

        #endregion



        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void Color_model_Load(object sender, EventArgs e)
        {
            this.updating = true;
            UpdateHSL(ColorSpaceHelper.RGBtoHSL((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
            UpdateHSB(ColorSpaceHelper.RGBtoHSB((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
            UpdateCMYK(ColorSpaceHelper.RGBtoCMYK((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
            UpdateYUV(ColorSpaceHelper.RGBtoYUV((int)redUD.Value, (int)greenUD.Value, (int)blueUD.Value));
            this.updating = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            color.R = preview.BackColor.R;
            color.G = preview.BackColor.G;
            color.B = preview.BackColor.B;
            this.Hide();
        }
    }
}
