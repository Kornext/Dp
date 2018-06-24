namespace Diplom_1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pict_edit = new System.Windows.Forms.PictureBox();
            this.pict_rot_lef = new System.Windows.Forms.PictureBox();
            this.pict_rot_rig = new System.Windows.Forms.PictureBox();
            this.pict_timer = new System.Windows.Forms.PictureBox();
            this.pict_right = new System.Windows.Forms.PictureBox();
            this.pict_left = new System.Windows.Forms.PictureBox();
            this.pict_minus = new System.Windows.Forms.PictureBox();
            this.pict_plus = new System.Windows.Forms.PictureBox();
            this.pict_expand = new System.Windows.Forms.PictureBox();
            this.pict_open = new System.Windows.Forms.PictureBox();
            this.pict_panel = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pict_fon = new System.Windows.Forms.PictureBox();
            this.pict_delete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pict_edit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_rot_lef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_rot_rig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_minus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_plus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_expand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_open)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_panel)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_fon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_delete)).BeginInit();
            this.SuspendLayout();
            // 
            // pict_edit
            // 
            this.pict_edit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_edit.ErrorImage = null;
            this.pict_edit.Image = global::Diplom_1.Properties.Resources.редактирование;
            this.pict_edit.Location = new System.Drawing.Point(400, 381);
            this.pict_edit.Name = "pict_edit";
            this.pict_edit.Size = new System.Drawing.Size(30, 30);
            this.pict_edit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_edit.TabIndex = 20;
            this.pict_edit.TabStop = false;
            this.pict_edit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_edit_MouseDown);
            this.pict_edit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_edit_MouseUp);
            // 
            // pict_rot_lef
            // 
            this.pict_rot_lef.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_rot_lef.ErrorImage = null;
            this.pict_rot_lef.Image = global::Diplom_1.Properties.Resources.повернуть_вправо;
            this.pict_rot_lef.Location = new System.Drawing.Point(455, 381);
            this.pict_rot_lef.Name = "pict_rot_lef";
            this.pict_rot_lef.Size = new System.Drawing.Size(30, 30);
            this.pict_rot_lef.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_rot_lef.TabIndex = 19;
            this.pict_rot_lef.TabStop = false;
            this.pict_rot_lef.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_rot_lef_MouseDown);
            this.pict_rot_lef.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_rot_lef_MouseUp);
            // 
            // pict_rot_rig
            // 
            this.pict_rot_rig.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_rot_rig.ErrorImage = null;
            this.pict_rot_rig.Image = global::Diplom_1.Properties.Resources.повернуть_влево;
            this.pict_rot_rig.Location = new System.Drawing.Point(491, 381);
            this.pict_rot_rig.Name = "pict_rot_rig";
            this.pict_rot_rig.Size = new System.Drawing.Size(30, 30);
            this.pict_rot_rig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_rot_rig.TabIndex = 18;
            this.pict_rot_rig.TabStop = false;
            this.pict_rot_rig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_rot_rig_MouseDown);
            this.pict_rot_rig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_rot_rig_MouseUp);
            // 
            // pict_timer
            // 
            this.pict_timer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_timer.ErrorImage = null;
            this.pict_timer.Image = global::Diplom_1.Properties.Resources.воспроизвести;
            this.pict_timer.Location = new System.Drawing.Point(251, 381);
            this.pict_timer.Name = "pict_timer";
            this.pict_timer.Size = new System.Drawing.Size(30, 30);
            this.pict_timer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_timer.TabIndex = 17;
            this.pict_timer.TabStop = false;
            this.pict_timer.Click += new System.EventHandler(this.pict_timer_Click);
            this.pict_timer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_timer_MouseDown);
            this.pict_timer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_timer_MouseUp);
            // 
            // pict_right
            // 
            this.pict_right.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_right.ErrorImage = null;
            this.pict_right.Image = global::Diplom_1.Properties.Resources.вправо;
            this.pict_right.Location = new System.Drawing.Point(287, 381);
            this.pict_right.Name = "pict_right";
            this.pict_right.Size = new System.Drawing.Size(30, 30);
            this.pict_right.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_right.TabIndex = 16;
            this.pict_right.TabStop = false;
            this.pict_right.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_right_MouseDown);
            this.pict_right.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_right_MouseUp);
            // 
            // pict_left
            // 
            this.pict_left.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_left.Image = global::Diplom_1.Properties.Resources.влево;
            this.pict_left.Location = new System.Drawing.Point(215, 381);
            this.pict_left.Name = "pict_left";
            this.pict_left.Size = new System.Drawing.Size(30, 30);
            this.pict_left.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_left.TabIndex = 15;
            this.pict_left.TabStop = false;
            this.pict_left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_left_MouseDown);
            this.pict_left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_left_MouseUp);
            // 
            // pict_minus
            // 
            this.pict_minus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_minus.Image = global::Diplom_1.Properties.Resources.уменьшить;
            this.pict_minus.Location = new System.Drawing.Point(159, 381);
            this.pict_minus.Name = "pict_minus";
            this.pict_minus.Size = new System.Drawing.Size(30, 30);
            this.pict_minus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_minus.TabIndex = 14;
            this.pict_minus.TabStop = false;
            this.pict_minus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_minus_MouseDown);
            this.pict_minus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_minus_MouseUp);
            // 
            // pict_plus
            // 
            this.pict_plus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_plus.Image = global::Diplom_1.Properties.Resources.увеличить;
            this.pict_plus.Location = new System.Drawing.Point(123, 381);
            this.pict_plus.Name = "pict_plus";
            this.pict_plus.Size = new System.Drawing.Size(30, 30);
            this.pict_plus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_plus.TabIndex = 13;
            this.pict_plus.TabStop = false;
            this.pict_plus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_plus_MouseDown);
            this.pict_plus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_plus_MouseUp);
            // 
            // pict_expand
            // 
            this.pict_expand.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_expand.Image = global::Diplom_1.Properties.Resources.развернуть;
            this.pict_expand.Location = new System.Drawing.Point(71, 381);
            this.pict_expand.Name = "pict_expand";
            this.pict_expand.Size = new System.Drawing.Size(30, 30);
            this.pict_expand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_expand.TabIndex = 12;
            this.pict_expand.TabStop = false;
            this.pict_expand.Click += new System.EventHandler(this.pict_expand_Click);
            this.pict_expand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_expand_MouseDown);
            this.pict_expand.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_expand_MouseUp);
            // 
            // pict_open
            // 
            this.pict_open.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_open.Image = global::Diplom_1.Properties.Resources.выбор_папки;
            this.pict_open.Location = new System.Drawing.Point(11, 381);
            this.pict_open.Name = "pict_open";
            this.pict_open.Size = new System.Drawing.Size(30, 30);
            this.pict_open.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_open.TabIndex = 1;
            this.pict_open.TabStop = false;
            this.pict_open.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_open_MouseDown);
            this.pict_open.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_open_MouseUp);
            // 
            // pict_panel
            // 
            this.pict_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pict_panel.Image = global::Diplom_1.Properties.Resources.panel;
            this.pict_panel.Location = new System.Drawing.Point(5, 378);
            this.pict_panel.Name = "pict_panel";
            this.pict_panel.Size = new System.Drawing.Size(524, 36);
            this.pict_panel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_panel.TabIndex = 11;
            this.pict_panel.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Diplom_1.Properties.Resources.Fon;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(538, 369);
            this.panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(538, 369);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // pict_fon
            // 
            this.pict_fon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pict_fon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pict_fon.Image = global::Diplom_1.Properties.Resources.Fon;
            this.pict_fon.InitialImage = null;
            this.pict_fon.Location = new System.Drawing.Point(0, 0);
            this.pict_fon.Name = "pict_fon";
            this.pict_fon.Size = new System.Drawing.Size(542, 427);
            this.pict_fon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_fon.TabIndex = 10;
            this.pict_fon.TabStop = false;
            // 
            // pict_delete
            // 
            this.pict_delete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pict_delete.ErrorImage = null;
            this.pict_delete.Image = ((System.Drawing.Image)(resources.GetObject("pict_delete.Image")));
            this.pict_delete.Location = new System.Drawing.Point(342, 381);
            this.pict_delete.Name = "pict_delete";
            this.pict_delete.Size = new System.Drawing.Size(30, 30);
            this.pict_delete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_delete.TabIndex = 21;
            this.pict_delete.TabStop = false;
            this.pict_delete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_delete_MouseDown);
            this.pict_delete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_delete_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(537, 426);
            this.Controls.Add(this.pict_delete);
            this.Controls.Add(this.pict_edit);
            this.Controls.Add(this.pict_rot_lef);
            this.Controls.Add(this.pict_rot_rig);
            this.Controls.Add(this.pict_timer);
            this.Controls.Add(this.pict_right);
            this.Controls.Add(this.pict_left);
            this.Controls.Add(this.pict_minus);
            this.Controls.Add(this.pict_plus);
            this.Controls.Add(this.pict_expand);
            this.Controls.Add(this.pict_open);
            this.Controls.Add(this.pict_panel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pict_fon);
            this.MinimumSize = new System.Drawing.Size(553, 465);
            this.Name = "Form1";
            this.Text = "KMP Viewer";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pict_edit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_rot_lef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_rot_rig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_minus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_plus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_expand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_open)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_panel)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_fon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_delete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pict_fon;
        private System.Windows.Forms.PictureBox pict_panel;
        private System.Windows.Forms.PictureBox pict_open;
        private System.Windows.Forms.PictureBox pict_expand;
        private System.Windows.Forms.PictureBox pict_plus;
        private System.Windows.Forms.PictureBox pict_minus;
        private System.Windows.Forms.PictureBox pict_left;
        private System.Windows.Forms.PictureBox pict_right;
        private System.Windows.Forms.PictureBox pict_timer;
        private System.Windows.Forms.PictureBox pict_rot_rig;
        private System.Windows.Forms.PictureBox pict_rot_lef;
        private System.Windows.Forms.PictureBox pict_edit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pict_delete;
    }
}

