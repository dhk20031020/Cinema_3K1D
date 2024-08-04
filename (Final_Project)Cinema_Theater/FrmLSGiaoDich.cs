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
    public partial class FrmLSGiaoDich : Form
    {
        SQLCONNECTION mycon = new SQLCONNECTION();
        SQLCONNECTION cmd = new SQLCONNECTION();
        SQLCONNECTION dta = new SQLCONNECTION();

        //Khởi tạo biến idVe, idKhachHang, idNhanVien
        string idVe;
        string idKhachHang;
        string idNhanVien;

        public FrmLSGiaoDich()
        {
            InitializeComponent();
        }

        private void FrmLSGiaoDich_Load(object sender, EventArgs e)
        {
            PhanQuyen();
            ChonDongDauTien();
        }
        //Phân quyền nếu là admin thì có thể xem thông tin tất cả các vé, còn không thì chỉ xem thông tin của mình
        public void PhanQuyen()
        {
            mycon.conn.Open();
            string sql = "SELECT LoaiTK FROM TaiKhoan WHERE UserName = '" + FrmLogin.username + "'";
            mycon.cmd = new SqlCommand(sql, mycon.conn);
            SqlDataReader dta = mycon.cmd.ExecuteReader();
            if (dta.Read() == true)
            {
                if (Convert.ToInt32(dta["LoaiTK"]) == 1)
                {
                    //Admin
                    //Nhân viên có thể xem thông tin của tất cả các vé
                    mycon.conn.Close();
                    LoadData();
                    mycon.conn.Close();
                }
                else
                {
                    //Nhân viên
                    //Nhân viên chỉ xem thông tin của vé mà mình đã bán
                    string sql1 = "SELECT * FROM Ve WHERE idNV = (SELECT idNV FROM TaiKhoan WHERE UserName = '" + FrmLogin.username + "')";
                    mycon.cmd = new SqlCommand(sql1, mycon.conn);
                    SqlDataAdapter dta1 = new SqlDataAdapter(mycon.cmd);
                    DataTable dt = new DataTable();
                    mycon.conn.Close();
                    mycon.conn.Open();
                    dta1.Fill(dt);
                    DsGiaoDIch.DataSource = dt;
                    mycon.conn.Close();
                }
            }
            mycon.conn.Close();
        }
        //Hàm load dữ liệu từ bảng Ve vào datagridview và sắp xếp theo mới nhất (idVe giảm dần)
        public void LoadData()
        {
            mycon.conn.Open();
            string sql = "SELECT * FROM Ve ORDER BY idVe DESC";
            mycon.cmd = new SqlCommand(sql, mycon.conn);
            SqlDataAdapter dta = new SqlDataAdapter(mycon.cmd);
            DataTable dt = new DataTable();
            dta.Fill(dt);
            DsGiaoDIch.DataSource = dt;
            mycon.conn.Close();
        }
        //Hàm chọn dòng đầu tiên trong datagridview
        private void ChonDongDauTien()
        {
            DsGiaoDIch.Rows[0].Selected = true;
            LblIdVe.Text = DsGiaoDIch.Rows[0].Cells[0].Value.ToString();
            LblKhachHang.Text = LayTenKhachHang(DsGiaoDIch.Rows[0].Cells[4].Value.ToString());
            LblNhanVien.Text = LayTenNhanVien(DsGiaoDIch.Rows[0].Cells[7].Value.ToString());
            LblNgayMua.Text = DsGiaoDIch.Rows[0].Cells[8].Value.ToString();
        }
        //Lấy tên khách hàng từ bảng KhachHang dựa vào idKhachHang trong bảng Ve
        public string LayTenKhachHang(string idKhachHang)
        {
            string tenKhachHang = "";
            if (mycon.conn.State != ConnectionState.Open)
            {
                mycon.conn.Open();
            }
            string sql = "SELECT HoTen FROM KhachHang WHERE idKhachHang = '" + idKhachHang + "'";
            mycon.cmd = new SqlCommand(sql, mycon.conn);
            mycon.conn.Close();
            mycon.conn.Open();
            using (SqlDataReader dta = mycon.cmd.ExecuteReader())
            {
                while (dta.Read())
                {
                    tenKhachHang = dta["HoTen"].ToString();
                }
            }
            mycon.conn.Close();
            return tenKhachHang;
        }
        //Lấy tên nhân viên đăng nhập.
        public string LayTenNhanVien(string idNhanVien)
        {
            string tenNhanVien = "";
            mycon.conn.Open();
            string sql = "SELECT HoTen FROM NhanVien WHERE idNV = '" + idNhanVien + "'";
            mycon.cmd = new SqlCommand(sql, mycon.conn);
            SqlDataReader dta = mycon.cmd.ExecuteReader();
            while (dta.Read())
            {
                tenNhanVien = dta["HoTen"].ToString();
            }
            mycon.conn.Close();
            return tenNhanVien;
        }
        private void DsGiaoDIch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Khi click vào 1 dòng trong DsGiaoDich thì dữ liệu của dòng đó sẽ được load lên các textbox
            int i = DsGiaoDIch.CurrentRow.Index;
            //idVe load vào combobox CboIdVe
            //Còn nếu như cột được chọn không phải là các cột trên thì các textbox sẽ được lấy theo dòng chọn cột đó
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DsGiaoDIch.Rows[e.RowIndex];
                LblIdVe.Text = row.Cells[0].Value.ToString();
                //Nếu idKhachHang hoặc idNhanVien không có giá trị thì sẽ load lên textbox là "Khách hàng không là thành viên"
                if (row.Cells[4].Value.ToString() == "")
                {
                    LblKhachHang.Text = "Khách hàng không là thành viên";
                }
                else
                {
                    LblKhachHang.Text = LayTenKhachHang(row.Cells[4].Value.ToString());
                }
                LblNhanVien.Text = LayTenNhanVien(row.Cells[7].Value.ToString());
                LblNgayMua.Text = row.Cells[8].Value.ToString();
            }
            else
            {
                return;
            }

        }
        //Hàm lấy idVe từ bảng Ve cho vào biến idVe

        private void BtnInVe_Click(object sender, EventArgs e)
        {
            // Lấy idVe từ hàng được chọn trong DataGridView
            if (DsGiaoDIch.CurrentRow != null)
            {
                DataGridViewRow row = DsGiaoDIch.CurrentRow;
                string idVe = row.Cells["idVe"].Value.ToString();

                // Truyền idVe vào constructor của InHoaDon
                InHoaDon frm = new InHoaDon(idVe);
                frm.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một vé để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
