using datve11;
using info;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Final_Project_Cinema_Theater
{
    public partial class QLyRapPhim : Form
    {
        public QLyRapPhim()
        {
            InitializeComponent();
        }



        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            //Hiển thị tên nhân viên đăng nhập thông qua bảng NhanVien va bảng TaiKhoan
            SQLCONNECTION mycon = new SQLCONNECTION();
            mycon.conn.Open();
            string sql = "SELECT HoTen FROM NhanVien WHERE idNV = (SELECT idNV FROM TaiKhoan WHERE UserName = '" + FrmLogin.username + "')";
            mycon.cmd = new SqlCommand(sql, mycon.conn);
            SqlDataReader dta = mycon.cmd.ExecuteReader();
            if (dta.Read() == true)
            {
                toolStripStatusLabel1.Text = "Tên tài khoản: " + dta["HoTen"].ToString();
            }
            mycon.conn.Close();
            //toolStripStatusLabel1.Text = "Tên tài khoản: " + FrmLogin.username;
        }

        private void QLyRapPhim_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1_Click(sender, e);
            PhanQuyen();
        }

        public void PhanQuyen()
        {
            //Phân quyền cho tài khoản. Admin thì all, nhân viên thì chỉ xem thông tin, Bán vé, About
            SQLCONNECTION mycon = new SQLCONNECTION();
            mycon.conn.Open();
            string sql = "SELECT LoaiTK FROM TaiKhoan WHERE UserName = '" + FrmLogin.username + "'";
            mycon.cmd = new SqlCommand(sql, mycon.conn);
            SqlDataReader dta = mycon.cmd.ExecuteReader();
            if (dta.Read() == true)
            {
                if (Convert.ToInt32(dta["LoaiTK"]) == 1)
                {
                    //Admin
                    nhânViênToolStripMenuItem.Visible = true;
                    kháchHàngToolStripMenuItem.Visible = true;
                    phimToolStripMenuItem.Visible = true;
                    đăngXuấtToolStripMenuItem.Visible = true;
                }
                else if (Convert.ToInt32(dta["LoaiTK"]) == 2)
                {
                    //Nhân viên
                    nhânViênToolStripMenuItem.Visible = false;
                    kháchHàngToolStripMenuItem.Visible = true;
                    phimToolStripMenuItem.Visible = false;
                    đăngXuấtToolStripMenuItem.Visible = true;
                    //Và bánVéToolStripMenuItem1 sẽ tự động mở khi nhân viên đăng nhập
                    BanVe frm = new BanVe();
                    frm.MdiParent = this;
                    frm.Show();
                }
            }
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Hiển thị form quản lý nhân viên dưới dạng form con
            FrmInfoNhanVien frm = new FrmInfoNhanVien();
            frm.MdiParent = this;
            frm.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInfoKhachHang frm = new FrmInfoKhachHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void thêmXuấtChiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmXuatChieu frm = new FrmXuatChieu();
            frm.MdiParent = this;
            frm.Show();
        }

        private void thêmPhimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddPhim frm = new FrmAddPhim();
            frm.MdiParent = this;
            frm.Show();
        }

        private void doanhThuPhimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDoanhThu frm = new FrmDoanhThu();
            frm.MdiParent = this;
            frm.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bánVéToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BanVe frm = new BanVe();
            frm.MdiParent = this;
            frm.Show();
        }

        private void thànhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Mở form about.cs
            about frm = new about();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ứngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutapp frm = new aboutapp();
            frm.MdiParent = this;
            frm.Show();
        }

        private void BáoCáoHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLSGiaoDich frm = new FrmLSGiaoDich();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
