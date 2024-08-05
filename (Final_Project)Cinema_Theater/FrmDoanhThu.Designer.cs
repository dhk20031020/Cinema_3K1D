namespace _Final_Project_Cinema_Theater
{
    partial class FrmDoanhThu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDoanhThu));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DsDoanhThu = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LblTopic = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsbPhim = new System.Windows.Forms.ToolStripButton();
            this.TsbNhanVien = new System.Windows.Forms.ToolStripButton();
            this.TsbPhongChieu = new System.Windows.Forms.ToolStripButton();
            this.TsTimKiem = new System.Windows.Forms.ToolStripButton();
            this.TxtTimKiem = new System.Windows.Forms.ToolStripTextBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DsDoanhThu)).BeginInit();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(800, 503);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Doanh Thu";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 54);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 447);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Goldenrod;
            this.panel3.Controls.Add(this.DsDoanhThu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 73);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(794, 374);
            this.panel3.TabIndex = 2;
            // 
            // DsDoanhThu
            // 
            this.DsDoanhThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DsDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DsDoanhThu.Location = new System.Drawing.Point(0, 0);
            this.DsDoanhThu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DsDoanhThu.Name = "DsDoanhThu";
            this.DsDoanhThu.RowHeadersWidth = 51;
            this.DsDoanhThu.RowTemplate.Height = 24;
            this.DsDoanhThu.Size = new System.Drawing.Size(794, 374);
            this.DsDoanhThu.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel2.Controls.Add(this.LblTopic);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(794, 73);
            this.panel2.TabIndex = 1;
            // 
            // LblTopic
            // 
            this.LblTopic.AutoSize = true;
            this.LblTopic.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTopic.Location = new System.Drawing.Point(155, 18);
            this.LblTopic.Name = "LblTopic";
            this.LblTopic.Size = new System.Drawing.Size(504, 36);
            this.LblTopic.TabIndex = 0;
            this.LblTopic.Text = "Doanh thu theo các thông tin";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightCoral;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbPhim,
            this.TsbNhanVien,
            this.TsbPhongChieu,
            this.TsTimKiem,
            this.TxtTimKiem});
            this.toolStrip1.Location = new System.Drawing.Point(3, 27);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(794, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TsbPhim
            // 
            this.TsbPhim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbPhim.Image = ((System.Drawing.Image)(resources.GetObject("TsbPhim.Image")));
            this.TsbPhim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbPhim.Name = "TsbPhim";
            this.TsbPhim.Size = new System.Drawing.Size(46, 24);
            this.TsbPhim.Text = "Phim";
            this.TsbPhim.Click += new System.EventHandler(this.TsbPhim_Click_1);
            // 
            // TsbNhanVien
            // 
            this.TsbNhanVien.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbNhanVien.Image = ((System.Drawing.Image)(resources.GetObject("TsbNhanVien.Image")));
            this.TsbNhanVien.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbNhanVien.Name = "TsbNhanVien";
            this.TsbNhanVien.Size = new System.Drawing.Size(81, 24);
            this.TsbNhanVien.Text = "Nhân Viên";
            this.TsbNhanVien.Click += new System.EventHandler(this.TsbNhanVien_Click_1);
            // 
            // TsbPhongChieu
            // 
            this.TsbPhongChieu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbPhongChieu.Image = ((System.Drawing.Image)(resources.GetObject("TsbPhongChieu.Image")));
            this.TsbPhongChieu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbPhongChieu.Name = "TsbPhongChieu";
            this.TsbPhongChieu.Size = new System.Drawing.Size(96, 24);
            this.TsbPhongChieu.Text = "Phòng Chiếu";
            this.TsbPhongChieu.Click += new System.EventHandler(this.TsbPhongChieu_Click_1);
            // 
            // TsTimKiem
            // 
            this.TsTimKiem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsTimKiem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("TsTimKiem.Image")));
            this.TsTimKiem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsTimKiem.Name = "TsTimKiem";
            this.TsTimKiem.Size = new System.Drawing.Size(74, 24);
            this.TsTimKiem.Text = "Tìm kiếm";
            this.TsTimKiem.Visible = false;
            this.TsTimKiem.Click += new System.EventHandler(this.TsTimKiem_Click);
            // 
            // TxtTimKiem
            // 
            this.TxtTimKiem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TxtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtTimKiem.Name = "TxtTimKiem";
            this.TxtTimKiem.Size = new System.Drawing.Size(199, 27);
            this.TxtTimKiem.Visible = false;
            this.TxtTimKiem.TextChanged += new System.EventHandler(this.TxtTimKiem_TextChanged);
            // 
            // FrmDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 503);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmDoanhThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doanh Thu Phim";
            this.Load += new System.EventHandler(this.FrmDoanhThu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DsDoanhThu)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TsbPhim;
        private System.Windows.Forms.ToolStripButton TsbNhanVien;
        private System.Windows.Forms.ToolStripButton TsbPhongChieu;
        private System.Windows.Forms.DataGridView DsDoanhThu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label LblTopic;
        private System.Windows.Forms.ToolStripButton TsTimKiem;
        private System.Windows.Forms.ToolStripTextBox TxtTimKiem;
    }
}