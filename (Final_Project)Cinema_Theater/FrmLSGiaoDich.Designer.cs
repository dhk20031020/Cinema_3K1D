namespace _Final_Project_Cinema_Theater
{
    partial class FrmLSGiaoDich
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.DsGiaoDIch = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnInVe = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LblKhachHang = new System.Windows.Forms.Label();
            this.LblNgayMua = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LblNhanVien = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.LblIdVe = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DsGiaoDIch)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Aquamarine;
            this.panel1.Controls.Add(this.LblNhanVien);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.LblNgayMua);
            this.panel1.Controls.Add(this.LblKhachHang);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 283);
            this.panel1.TabIndex = 0;
            // 
            // DsGiaoDIch
            // 
            this.DsGiaoDIch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DsGiaoDIch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DsGiaoDIch.Location = new System.Drawing.Point(0, 0);
            this.DsGiaoDIch.Name = "DsGiaoDIch";
            this.DsGiaoDIch.RowHeadersWidth = 51;
            this.DsGiaoDIch.RowTemplate.Height = 24;
            this.DsGiaoDIch.Size = new System.Drawing.Size(800, 317);
            this.DsGiaoDIch.TabIndex = 1;
            this.DsGiaoDIch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DsGiaoDIch_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DsGiaoDIch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 283);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 317);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 80);
            this.panel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(372, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "LỊCH SỬ GIAO DỊCH ";
            // 
            // BtnInVe
            // 
            this.BtnInVe.BackColor = System.Drawing.Color.LightCoral;
            this.BtnInVe.Location = new System.Drawing.Point(52, 20);
            this.BtnInVe.Name = "BtnInVe";
            this.BtnInVe.Size = new System.Drawing.Size(97, 47);
            this.BtnInVe.TabIndex = 1;
            this.BtnInVe.Text = "In Vé";
            this.BtnInVe.UseVisualStyleBackColor = false;
            this.BtnInVe.Click += new System.EventHandler(this.BtnInVe_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.SpringGreen;
            this.panel4.Controls.Add(this.LblIdVe);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(600, 80);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 203);
            this.panel4.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tên khách hàng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ngày mua";
            // 
            // LblKhachHang
            // 
            this.LblKhachHang.AutoSize = true;
            this.LblKhachHang.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblKhachHang.Location = new System.Drawing.Point(257, 103);
            this.LblKhachHang.Name = "LblKhachHang";
            this.LblKhachHang.Size = new System.Drawing.Size(179, 30);
            this.LblKhachHang.TabIndex = 5;
            this.LblKhachHang.Text = "Tên Khách Hàng";
            // 
            // LblNgayMua
            // 
            this.LblNgayMua.AutoSize = true;
            this.LblNgayMua.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNgayMua.Location = new System.Drawing.Point(257, 161);
            this.LblNgayMua.Name = "LblNgayMua";
            this.LblNgayMua.Size = new System.Drawing.Size(118, 30);
            this.LblNgayMua.TabIndex = 6;
            this.LblNgayMua.Text = "Ngày mua";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(61, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 30);
            this.label6.TabIndex = 8;
            this.label6.Text = "Mã Vé";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(26, 219);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 30);
            this.label7.TabIndex = 7;
            this.label7.Text = "Nhân viên";
            // 
            // LblNhanVien
            // 
            this.LblNhanVien.AutoSize = true;
            this.LblNhanVien.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNhanVien.Location = new System.Drawing.Point(257, 219);
            this.LblNhanVien.Name = "LblNhanVien";
            this.LblNhanVien.Size = new System.Drawing.Size(116, 30);
            this.LblNhanVien.TabIndex = 8;
            this.LblNhanVien.Text = "Nhân viên";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panel5.Controls.Add(this.BtnInVe);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 119);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 84);
            this.panel5.TabIndex = 9;
            // 
            // LblIdVe
            // 
            this.LblIdVe.AutoSize = true;
            this.LblIdVe.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdVe.Location = new System.Drawing.Point(35, 68);
            this.LblIdVe.Name = "LblIdVe";
            this.LblIdVe.Size = new System.Drawing.Size(131, 42);
            this.LblIdVe.TabIndex = 10;
            this.LblIdVe.Text = "Mã Vé";
            // 
            // FrmLSGiaoDich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmLSGiaoDich";
            this.Text = "FrmLSGiaoDich";
            this.Load += new System.EventHandler(this.FrmLSGiaoDich_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DsGiaoDIch)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DsGiaoDIch;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LblNgayMua;
        private System.Windows.Forms.Label LblKhachHang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button BtnInVe;
        private System.Windows.Forms.Label LblNhanVien;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LblIdVe;
    }
}