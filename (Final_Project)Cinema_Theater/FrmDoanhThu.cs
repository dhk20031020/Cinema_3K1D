using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace _Final_Project_Cinema_Theater
{
    public partial class FrmDoanhThu : Form
    {
        public FrmDoanhThu()
        {
            InitializeComponent();
        }

        // Khởi tạo kết nối
        SQLCONNECTION connDB = new SQLCONNECTION();

        private void FrmDoanhThu_Load(object sender, EventArgs e)
        {
            LoadDataForNhanVien();
            LblTopic.Text = "Doanh thu theo nhân viên";
            PhanQuyen();
        }

        private void PhanQuyen()
        {
            using (SqlConnection conn = new SqlConnection(connDB.conn.ConnectionString))
            {
                conn.Open();
                string sql = "SELECT LoaiTK FROM TaiKhoan WHERE UserName = @username";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", FrmLogin.username);
                    using (SqlDataReader dta = cmd.ExecuteReader())
                    {
                        if (dta.Read())
                        {
                            bool isAdmin = Convert.ToInt32(dta["LoaiTK"]) == 1;
                            TsbNhanVien.Visible = isAdmin;
                            TsbPhim.Visible = isAdmin;
                            TsbPhongChieu.Visible = isAdmin;
                            TxtTimKiem.Visible = !isAdmin;
                            TsTimKiem.Visible = !isAdmin;
                        }
                    }
                }
            }
        }

        private void ChangeColorButton(string selectedButton)
        {
            Color defaultColor = Color.FromArgb(128, 255, 255);
            Color selectedColor = Color.FromArgb(0, 122, 204);

            TsbNhanVien.BackColor = selectedButton == "NhanVien" ? selectedColor : defaultColor;
            TsbPhim.BackColor = selectedButton == "Phim" ? selectedColor : defaultColor;
            TsbPhongChieu.BackColor = selectedButton == "PhongChieu" ? selectedColor : defaultColor;
        }

        private void TsbNhanVien_Click_1(object sender, EventArgs e)
        {
            LoadDataForNhanVien();
            ChangeColorButton("NhanVien");
            LblTopic.Text = "Doanh thu theo nhân viên";
            TxtTimKiem.Visible = true;
            TsTimKiem.Visible = true;
        }

        private void TsbPhim_Click_1(object sender, EventArgs e)
        {
            LoadDataForPhim();
            ChangeColorButton("Phim");
            LblTopic.Text = "Doanh thu theo phim";
            TxtTimKiem.Visible = true;
            TsTimKiem.Visible = true;
        }

        private void TsbPhongChieu_Click_1(object sender, EventArgs e)
        {
            LoadDataForPhongChieu();
            ChangeColorButton("PhongChieu");
            LblTopic.Text = "Doanh thu theo phòng chiếu";
            TxtTimKiem.Visible = false;
            TsTimKiem.Visible = false;
        }

        private void LoadDataForPhim()
        {
            using (SqlConnection conn = new SqlConnection(connDB.conn.ConnectionString))
            {
                conn.Open();
                string sqlPhim = @"
                    SELECT Phim.idPhim, Phim.TenPhim, SUM(Ve.TienBanVe) AS TongDoanhThu
                    FROM Phim
                    INNER JOIN LichChieu ON Phim.idPhim = LichChieu.idPhim
                    INNER JOIN Ve ON LichChieu.idLichChieu = Ve.idLichChieu
                    GROUP BY Phim.idPhim, Phim.TenPhim";

                using (SqlCommand cmd = new SqlCommand(sqlPhim, conn))
                using (SqlDataReader dta = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(dta);
                    DsDoanhThu.DataSource = dt;
                }
            }
        }

        public bool IsAdmin(string username)
        {
            using (SqlConnection conn = new SqlConnection(connDB.conn.ConnectionString))
            {
                conn.Open();
                string sql = "SELECT LoaiTK FROM TaiKhoan WHERE UserName = @username";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader dta = cmd.ExecuteReader())
                    {
                        if (dta.Read())
                        {
                            return Convert.ToInt32(dta["LoaiTK"]) == 1;
                        }
                    }
                }
            }
            return false;
        }

        private void LoadDataForNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(connDB.conn.ConnectionString))
            {
                //Nếu là admin thì hiển thị tất cả nhân viên, ngược lại chỉ hiển thị nhân viên đăng nhập
                string sqlNhanVien = IsAdmin(FrmLogin.username)
                    ? @"
                        SELECT NhanVien.idNV, NhanVien.HoTen, 
                               COUNT(Ve.idKhachHang) AS SoLuongKhachHang, 
                               SUM(Ve.TienBanVe) AS TongDoanhThu
                        FROM NhanVien
                        INNER JOIN Ve ON NhanVien.idNV = Ve.idNV
                        GROUP BY NhanVien.idNV, NhanVien.HoTen"
                    : @"
                        SELECT NhanVien.idNV, NhanVien.HoTen, 
                               COUNT(Ve.idKhachHang) AS SoLuongKhachHang, 
                               SUM(Ve.TienBanVe) AS TongDoanhThu
                        FROM NhanVien
                        INNER JOIN Ve ON NhanVien.idNV = Ve.idNV
                        WHERE NhanVien.idNV = (SELECT idNV FROM TaiKhoan WHERE UserName = @username)
                        GROUP BY NhanVien.idNV, NhanVien.HoTen";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sqlNhanVien, conn))
                {
                    if (!IsAdmin(FrmLogin.username))
                    {
                        cmd.Parameters.AddWithValue("@username", FrmLogin.username);
                    }
                    using (SqlDataReader dta = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dta);
                        DsDoanhThu.DataSource = dt;
                    }
                }
            }
        }

        private void LoadDataForPhongChieu()
        {
            using (SqlConnection conn = new SqlConnection(connDB.conn.ConnectionString))
            {
                conn.Open();
                string sqlPhongChieu = @"
                    SELECT PhongChieu.idPhongChieu, PhongChieu.TenPhong, 
                           MAX(LichChieu.GiaVe) AS GiaVe, 
                           STUFF((SELECT ', ' + Ve.MaGheNgoi
                                  FROM LichChieu
                                  INNER JOIN Ve ON LichChieu.idLichChieu = Ve.idLichChieu
                                  WHERE PhongChieu.idPhongChieu = LichChieu.idPhongChieu
                                  FOR XML PATH('')), 1, 2, '') AS MaGheNgoi, 
                           SUM(Ve.TienBanVe) AS TongDoanhThu
                    FROM PhongChieu
                    INNER JOIN LichChieu ON PhongChieu.idPhongChieu = LichChieu.idPhongChieu
                    INNER JOIN Ve ON LichChieu.idLichChieu = Ve.idLichChieu
                    GROUP BY PhongChieu.idPhongChieu, PhongChieu.TenPhong";

                using (SqlCommand cmd = new SqlCommand(sqlPhongChieu, conn))
                using (SqlDataReader dta = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(dta);
                    DsDoanhThu.DataSource = dt;
                }
            }
        }

        private void TsTimKiem_Click(object sender, EventArgs e)
        {
            if (LblTopic.Text == "Doanh thu theo phim")
            {
                using (SqlConnection conn = new SqlConnection(connDB.conn.ConnectionString))
                {
                    conn.Open();
                    string sqlPhim = @"
                        SELECT Phim.idPhim, Phim.TenPhim, SUM(Ve.TienBanVe) AS TongDoanhThu
                        FROM Phim
                        INNER JOIN LichChieu ON Phim.idPhim = LichChieu.idPhim
                        INNER JOIN Ve ON LichChieu.idLichChieu = Ve.idLichChieu
                        WHERE Phim.TenPhim LIKE @search
                        GROUP BY Phim.idPhim, Phim.TenPhim";

                    using (SqlCommand cmd = new SqlCommand(sqlPhim, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + TxtTimKiem.Text + "%");
                        using (SqlDataReader dta = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dta);
                            DsDoanhThu.DataSource = dt;
                        }
                    }
                }
            }
            else if (LblTopic.Text == "Doanh thu theo nhân viên")
            {
                using (SqlConnection conn = new SqlConnection(connDB.conn.ConnectionString))
                {
                    conn.Open();
                    string sqlNV = @"
                        SELECT NhanVien.idNV, NhanVien.HoTen, 
                               COUNT(Ve.idKhachHang) AS SoLuongKhachHang, 
                               SUM(Ve.TienBanVe) AS TongDoanhThu
                        FROM NhanVien
                        INNER JOIN Ve ON NhanVien.idNV = Ve.idNV
                        WHERE NhanVien.HoTen LIKE @search
                        GROUP BY NhanVien.idNV, NhanVien.HoTen";

                    using (SqlCommand cmd = new SqlCommand(sqlNV, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + TxtTimKiem.Text + "%");
                        using (SqlDataReader dta = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dta);
                            DsDoanhThu.DataSource = dt;
                        }
                    }
                }
            }
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtTimKiem.Text))
            {
                if (LblTopic.Text == "Doanh thu theo phim")
                {
                    LoadDataForPhim();
                }
                else if (LblTopic.Text == "Doanh thu theo nhân viên")
                {
                    LoadDataForNhanVien();
                }
            }
        }
    }
}
