using Microsoft.Reporting.WinForms;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _Final_Project_Cinema_Theater
{
    public partial class InHoaDon : Form
    {
        private string idVe;

        public InHoaDon(string idVe = null)
        {
            InitializeComponent();
            this.idVe = idVe;
        }

        private void InHoaDon_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            LoadReportData();
        }

        SQLCONNECTION connDB = new SQLCONNECTION();
        string idLichChieu;
        string idGhe;
        string TienBanVe;
        DateTime ngayBan;
        string idKhachHang;
        string tenkh = "Không phải khách hàng thành viên";
        string idPhim;
        string tenPhim;
        DateTime ngayChieu;

        private void LoadReportData()
        {
            if (string.IsNullOrEmpty(idVe))
            {
                // Lấy idVe cuối cùng từ bảng Ve
                using (SqlConnection connection = new SqlConnection(connDB.conn.ConnectionString))
                {
                    connection.Open();
                    string sqlVe = $"SELECT * FROM Ve WHERE idVe = (SELECT MAX(idVe) FROM Ve)";
                    using (SqlCommand command = new SqlCommand(sqlVe, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idVe = reader["idVe"].ToString();
                                idLichChieu = reader["idLichChieu"].ToString();
                                idGhe = reader["MaGheNgoi"].ToString();
                                TienBanVe = reader["TienBanVe"].ToString();
                                ngayBan = DateTime.Parse(reader["NgayMua"].ToString());
                                idKhachHang = reader["idKhachHang"].ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                // Lấy dữ liệu từ bảng Ve dựa trên idVe truyền vào
                using (SqlConnection connection = new SqlConnection(connDB.conn.ConnectionString))
                {
                    connection.Open();
                    string sqlVe = $"SELECT * FROM Ve WHERE idVe='{idVe}'";
                    using (SqlCommand command = new SqlCommand(sqlVe, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idLichChieu = reader["idLichChieu"].ToString();
                                idGhe = reader["MaGheNgoi"].ToString();
                                TienBanVe = reader["TienBanVe"].ToString();
                                ngayBan = DateTime.Parse(reader["NgayMua"].ToString());
                                idKhachHang = reader["idKhachHang"].ToString();
                            }
                        }
                    }
                }
            }

            // Lấy dữ liệu idPhim từ bảng LichChieu
            using (SqlConnection connection = new SqlConnection(connDB.conn.ConnectionString))
            {
                connection.Open();
                string sqlLichChieu = $"SELECT * FROM LichChieu WHERE idLichChieu='{idLichChieu}'";
                using (SqlCommand command = new SqlCommand(sqlLichChieu, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idPhim = reader["idPhim"].ToString();
                            ngayChieu = DateTime.Parse(reader["GioChieu"].ToString());
                        }
                    }
                }
            }

            // Lấy dữ liệu tên phim từ bảng Phim
            using (SqlConnection connection = new SqlConnection(connDB.conn.ConnectionString))
            {
                connection.Open();
                string sqlPhim = $"SELECT * FROM Phim WHERE idPhim='{idPhim}'";
                using (SqlCommand command = new SqlCommand(sqlPhim, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tenPhim = reader["tenPhim"].ToString();
                        }
                    }
                }
            }

            // Lấy dữ liệu tên khách hàng từ bảng KhachHang nếu idKhachHang không null
            if (!string.IsNullOrEmpty(idKhachHang))
            {
                using (SqlConnection connection = new SqlConnection(connDB.conn.ConnectionString))
                {
                    connection.Open();
                    string sqlKhachHang = $"SELECT * FROM KhachHang WHERE idKhachHang='{idKhachHang}'";
                    using (SqlCommand command = new SqlCommand(sqlKhachHang, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tenkh = reader["HoTen"].ToString();
                            }
                        }
                    }
                }
            }

            // Tạo các tham số cho báo cáo
            ReportParameter[] parameters = new ReportParameter[7];
            parameters[0] = new ReportParameter("DonHang", idVe);
            parameters[1] = new ReportParameter("KhachHang", tenkh);
            parameters[2] = new ReportParameter("NgayBan", ngayBan.ToString("dd/MM/yyyy"));
            parameters[3] = new ReportParameter("TenPhim", tenPhim);
            parameters[4] = new ReportParameter("NgayChieu", ngayChieu.ToString("dd/MM/yyyy"));
            parameters[5] = new ReportParameter("SoLuongGhe", idGhe);
            parameters[6] = new ReportParameter("TongTien", TienBanVe);

            // Truyền dữ liệu vào Hoadon.rdlc
            reportViewer1.LocalReport.SetParameters(parameters);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.RefreshReport();
        }
    }
}
