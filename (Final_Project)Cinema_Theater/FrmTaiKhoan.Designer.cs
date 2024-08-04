namespace _Final_Project_Cinema_Theater
{
    partial class FrmTaiKhoan
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
            this.LblHoTen = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.TxtUsername = new System.Windows.Forms.TextBox();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.RbNV = new System.Windows.Forms.RadioButton();
            this.RbAdmin = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.LblHoTen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 80);
            this.panel1.TabIndex = 17;
            // 
            // LblHoTen
            // 
            this.LblHoTen.AutoSize = true;
            this.LblHoTen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.LblHoTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHoTen.Location = new System.Drawing.Point(94, 9);
            this.LblHoTen.Name = "LblHoTen";
            this.LblHoTen.Size = new System.Drawing.Size(255, 29);
            this.LblHoTen.TabIndex = 8;
            this.LblHoTen.Text = "Tài Khoản Nhân viên";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Username";
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(169, 184);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(233, 22);
            this.TxtPassword.TabIndex = 14;
            // 
            // TxtUsername
            // 
            this.TxtUsername.Location = new System.Drawing.Point(169, 117);
            this.TxtUsername.Name = "TxtUsername";
            this.TxtUsername.Size = new System.Drawing.Size(233, 22);
            this.TxtUsername.TabIndex = 13;
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.BtnUpdate.Location = new System.Drawing.Point(327, 261);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(75, 55);
            this.BtnUpdate.TabIndex = 19;
            this.BtnUpdate.Text = "Sửa";
            this.BtnUpdate.UseVisualStyleBackColor = false;
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.Lime;
            this.BtnAdd.Location = new System.Drawing.Point(226, 261);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 55);
            this.BtnAdd.TabIndex = 18;
            this.BtnAdd.Text = "Thêm";
            this.BtnAdd.UseVisualStyleBackColor = false;
            // 
            // RbNV
            // 
            this.RbNV.AutoSize = true;
            this.RbNV.Checked = true;
            this.RbNV.Location = new System.Drawing.Point(25, 71);
            this.RbNV.Name = "RbNV";
            this.RbNV.Size = new System.Drawing.Size(113, 24);
            this.RbNV.TabIndex = 11;
            this.RbNV.TabStop = true;
            this.RbNV.Text = "Nhân viên";
            this.RbNV.UseVisualStyleBackColor = true;
            // 
            // RbAdmin
            // 
            this.RbAdmin.AutoSize = true;
            this.RbAdmin.Location = new System.Drawing.Point(25, 38);
            this.RbAdmin.Name = "RbAdmin";
            this.RbAdmin.Size = new System.Drawing.Size(82, 24);
            this.RbAdmin.TabIndex = 10;
            this.RbAdmin.TabStop = true;
            this.RbAdmin.Text = "Admin";
            this.RbAdmin.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Cyan;
            this.groupBox1.Controls.Add(this.RbNV);
            this.groupBox1.Controls.Add(this.RbAdmin);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(26, 232);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 112);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Loại Tài Khoản";
            // 
            // FrmTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(459, 372);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.TxtUsername);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmTaiKhoan";
            this.Text = "FrmTaiKhoan";
            this.Load += new System.EventHandler(this.FrmTaiKhoan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LblHoTen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.TextBox TxtUsername;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.RadioButton RbNV;
        private System.Windows.Forms.RadioButton RbAdmin;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}