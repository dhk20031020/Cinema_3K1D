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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        public void PhanQuyen()
        {
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            //Thoát chương trình
            Application.Exit();
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Hiện thông báo xác nhận thoát chương trình
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        //Hàm kiểm tra tài khoản và mật khẩu trong database ở bảng TaiKhoan để đăng nhập
        private bool CheckLogin(string username, string password)
        {
            SQLCONNECTION mycon = new SQLCONNECTION();
            mycon.conn.Open();
            string sql = "SELECT * FROM TaiKhoan WHERE UserName = '" + username + "' AND Pass = '" + password + "'";
            mycon.cmd = new SqlCommand(sql, mycon.conn);
            SqlDataReader dta = mycon.cmd.ExecuteReader();
            if (dta.Read() == true)
            {
                mycon.conn.Close();
                return true;
            }
            else
            {
                mycon.conn.Close();
                return false;
            }
        }
        //Tạo biến username để lưu tên nhân viên đăng nhập
        public static string username;
        public static string password;
        private void BtnDNhap_Click(object sender, EventArgs e)
        {
            //Kiểm tra nếu là tài khoản có LoaiTK là số 1 thì sẽ mở form quản lý rạp phim còn nếu là LoaiTK là số 2 thì sẽ mở form quản lý NhanVien
            if (CheckLogin(TxtUsername.Text, TxtPassword.Text) == true)
            {
                SQLCONNECTION mycon = new SQLCONNECTION();
                mycon.conn.Open();
                string sql = "SELECT * FROM TaiKhoan WHERE UserName = '" + TxtUsername.Text + "' AND Pass = '" + TxtPassword.Text + "'";
                mycon.cmd = new SqlCommand(sql, mycon.conn);
                SqlDataReader dta = mycon.cmd.ExecuteReader();
                if (dta.Read() == true)
                {
                    username = dta["UserName"].ToString();
                    password = dta["Pass"].ToString();
                    if (dta["LoaiTK"].ToString() == "1")
                    {
                        QLyRapPhim qlrp = new QLyRapPhim();
                        qlrp.Show();
                        this.Hide();
                        //Khi QLyRapPhim đóng thì show lên lại
                        qlrp.FormClosed += (s, args) => this.Show();
                    }
                    else if (dta["LoaiTK"].ToString() == "2")
                    {
                        //FrmInfoNhanVien fnv = new QLyRapPhim();
                        //Hiển thị QLyRapPhim
                        QLyRapPhim qlrp = new QLyRapPhim();
                        qlrp.Show();
                        this.Hide();
                        PhanQuyen();
                        //Khi QLyRapPhim đóng thì show lên lại
                        qlrp.FormClosed += (s, args) => this.Show();
                    }
                }
                mycon.conn.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
