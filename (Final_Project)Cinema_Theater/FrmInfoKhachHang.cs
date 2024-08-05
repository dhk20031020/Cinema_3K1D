using datve11;
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
    public partial class FrmInfoKhachHang : Form
    {
        public FrmInfoKhachHang()
        {
            InitializeComponent();
        }
        //Dùng class SQLCONNECTION để kết nối database
        SQLCONNECTION connDB = new SQLCONNECTION();
        SQLCONNECTION cmd = new SQLCONNECTION();
        SQLCONNECTION dta = new SQLCONNECTION();
        SQLCONNECTION dt = new SQLCONNECTION();
        //Hàm kiểm tra cú pháp idNV
        private bool CheckidKH(string idNV)
        {
            if (string.IsNullOrEmpty(idNV) || idNV.Length < 3)
            {
                return false;
            }

            return idNV.Substring(0, 2) == "KH" && idNV.Substring(2).All(char.IsDigit);
        }

        private void FrmInfoKhachHang_Load(object sender, EventArgs e)
        {
            PhanQuyen();
            // Tạo câu lệnh sql và dữ liệu vào DsKhachHang
            string sql = "select * from KhachHang";
            if (connDB.conn.State == ConnectionState.Closed)
            {
                connDB.conn.Open();
            }
            connDB.cmd = new SqlCommand(sql, connDB.conn);
            connDB.dta = connDB.cmd.ExecuteReader();
            connDB.dt = new DataTable();
            connDB.dt.Load(connDB.dta);
            DsKhachHang.DataSource = connDB.dt;
            connDB.conn.Close();
        }
        //Hàm kiểm tra CCCD có hợp lệ không là dạng ký tự kiểu số và có 12 ký tự viết ngắn gọn
        private bool CheckTrungCCCD(string CCCD)
        {
            string sqlCheck = "SELECT COUNT(*) FROM KhachHang WHERE CCCD = @CCCD";
            using (SqlCommand cmd = new SqlCommand(sqlCheck, connDB.conn))
            {
                cmd.Parameters.AddWithValue("@CCCD", CCCD);
                if (connDB.conn.State != ConnectionState.Open)
                {
                    connDB.conn.Open();
                }
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void PhanQuyen()
        {
            //Phân quyền cho tài khoản. Admin thì all, nhân viên thì chỉ được thêm, sửa, tìm thông tin khách hàng chứ không được xóa
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
                    TsbThem.Visible = true;
                    TsbSua.Visible = true;
                    TsbXoa.Visible = true;
                }
                else if (Convert.ToInt32(dta["LoaiTK"]) == 2)
                {
                    //Nhân viên
                    TsbThem.Visible = true;
                    TsbSua.Visible = true;
                    TsbXoa.Visible = false;
                }
            }
        }
        //danh sach khach hang
        private void DsKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Khi click vào 1 cell trong DsKhachHang thì hiện thông tin lên các textbox
            int i = DsKhachHang.CurrentRow.Index;
            TxtIdKH.Text = DsKhachHang.Rows[i].Cells[0].Value.ToString();
            TxtHoTenKH.Text = DsKhachHang.Rows[i].Cells[1].Value.ToString();
            DtpNgaySinhKH.Text = DsKhachHang.Rows[i].Cells[2].Value.ToString();
            TxtDiaChiKH.Text = DsKhachHang.Rows[i].Cells[3].Value.ToString();
            MTxtSDTKH.Text = DsKhachHang.Rows[i].Cells[4].Value.ToString();
            TxtCCCDKH.Text = DsKhachHang.Rows[i].Cells[5].Value.ToString();
            LblDiemTichLuy.Text = DsKhachHang.Rows[i].Cells[6].Value.ToString();
        }

        private void TsbThem_Click(object sender, EventArgs e)
        {
            //Thêm thông tin khách hàng vào database gồm các thông tin: idKH, HoTenKH, NgaySinhKH, DiaChiKH, SDTKH, CCCDKH, DiemTichLuy thì khi tạo sẽ có giá trị mặc định là 0
            if (TxtHoTenKH.Text == "" || TxtDiaChiKH.Text == "" || MTxtSDTKH.Text == "" || TxtCCCDKH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (connDB.conn.State == ConnectionState.Closed)
                {
                    connDB.conn.Open();
                }
                //Kiểm tra xem idKH đã tồn tại chưa
                string sqlCheckDuplicateId = "SELECT COUNT(*) FROM KhachHang WHERE idKhachHang = @idKH";
                connDB.cmd = new SqlCommand(sqlCheckDuplicateId, connDB.conn);
                connDB.cmd.Parameters.AddWithValue("@idKH", TxtIdKH.Text);
                int count = Convert.ToInt32(connDB.cmd.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Id đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (TxtCCCDKH.Text.Length != 12 || !TxtCCCDKH.Text.All(char.IsDigit))
                    {
                        MessageBox.Show("CCCD không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (CheckTrungCCCD(TxtCCCDKH.Text))
                    {
                        MessageBox.Show("CCCD đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (MTxtSDTKH.Text.Length != 10 || !MTxtSDTKH.Text.All(char.IsDigit))
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (CheckidKH(TxtIdKH.Text))
                        {
                            string sql = "INSERT INTO KhachHang VALUES(@idKH, @HoTenKH, @NgaySinhKH, @DiaChiKH, @SDTKH, @CCCDKH, 0)";
                            connDB.cmd = new SqlCommand(sql, connDB.conn);
                            connDB.cmd.Parameters.AddWithValue("@idKH", TxtIdKH.Text);
                            connDB.cmd.Parameters.AddWithValue("@HoTenKH", TxtHoTenKH.Text);
                            connDB.cmd.Parameters.AddWithValue("@NgaySinhKH", DtpNgaySinhKH.Value);
                            connDB.cmd.Parameters.AddWithValue("@DiaChiKH", TxtDiaChiKH.Text);
                            connDB.cmd.Parameters.AddWithValue("@SDTKH", MTxtSDTKH.Text);
                            connDB.cmd.Parameters.AddWithValue("@CCCDKH", TxtCCCDKH.Text);
                            connDB.cmd.ExecuteNonQuery();
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FrmInfoKhachHang_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Id không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void TsbSua_Click(object sender, EventArgs e)
        {
            //Sửa thông tin khách hàng trong database
            if (TxtHoTenKH.Text == "" || TxtDiaChiKH.Text == "" || MTxtSDTKH.Text == "" || TxtCCCDKH.Text == "")
            {
                MessageBox.Show("Vui lòng chọn thông tin cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Kiểm tra nhập đúng cú pháp idKH chưa
                if (CheckidKH(TxtIdKH.Text))
                {
                    //Kiểm tra idKH có tồn tại chưa
                    string sqlCheckDuplicateId = "SELECT COUNT(*) FROM KhachHang WHERE idKhachHang = @idKH";
                    connDB.cmd = new SqlCommand(sqlCheckDuplicateId, connDB.conn);
                    connDB.cmd.Parameters.AddWithValue("@idKH", TxtIdKH.Text);
                    if (connDB.conn.State == ConnectionState.Closed)
                    {
                        connDB.conn.Open();
                    }
                    int count = Convert.ToInt32(connDB.cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        if (TxtCCCDKH.Text.Length != 12 || !TxtCCCDKH.Text.All(char.IsDigit))
                        {
                            MessageBox.Show("CCCD không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (MTxtSDTKH.Text.Length != 10 || !MTxtSDTKH.Text.All(char.IsDigit))
                        {
                            MessageBox.Show("Số điện thoại không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            string sql = "UPDATE KhachHang SET HoTen = @HoTenKH, NgaySinh = @NgaySinhKH, DiaChi = @DiaChiKH, SDT = @SDTKH, CCCD = @CCCD WHERE idKhachHang = @idKH";
                            connDB.cmd = new SqlCommand(sql, connDB.conn);
                            connDB.cmd.Parameters.AddWithValue("@idKH", TxtIdKH.Text);
                            connDB.cmd.Parameters.AddWithValue("@HoTenKH", TxtHoTenKH.Text);
                            connDB.cmd.Parameters.AddWithValue("@NgaySinhKH", DtpNgaySinhKH.Value);
                            connDB.cmd.Parameters.AddWithValue("@DiaChiKH", TxtDiaChiKH.Text);
                            connDB.cmd.Parameters.AddWithValue("@SDTKH", MTxtSDTKH.Text);
                            connDB.cmd.Parameters.AddWithValue("@CCCD", TxtCCCDKH.Text);
                            connDB.cmd.ExecuteNonQuery();
                            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FrmInfoKhachHang_Load(sender, e);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Id không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Id không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void TsbXoa_Click(object sender, EventArgs e)
        {
            //Xóa thông tin khách hàng trong database
            if (TxtHoTenKH.Text == "" || TxtDiaChiKH.Text == "" || MTxtSDTKH.Text == "" || TxtCCCDKH.Text == "")
            {
                MessageBox.Show("Vui lòng chọn thông tin cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Hỏi xác nhận xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //Nếu chọn Yes thì xóa thông tin khách hàng bỏ qua kiểm tra lại
                if (result == DialogResult.Yes)
                {
                    if (connDB.conn.State != ConnectionState.Open)
                    {
                        connDB.conn.Open();
                    }
                    //Xoá trong bảng khách hàng và bảng vé trở thành giá trị null
                    string sql = "UPDATE Ve SET idKhachHang = NULL WHERE idKhachHang = @idKH";
                    string sql2 = "DELETE FROM KhachHang WHERE idKhachHang = @idKH";
                    connDB.cmd = new SqlCommand(sql, connDB.conn);
                    connDB.cmd.Parameters.AddWithValue("@idKH", TxtIdKH.Text);
                    connDB.cmd.ExecuteNonQuery();
                    connDB.cmd = new SqlCommand(sql2, connDB.conn);
                    connDB.cmd.Parameters.AddWithValue("@idKH", TxtIdKH.Text);
                    connDB.cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmInfoKhachHang_Load(sender, e);
                }
            }
        }

        private void TsbTim_Click(object sender, EventArgs e)
        {
            //Tìm kiếm thông tin khách hàng trong database
            if (TxtTim.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin cần tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //Tìm kiếm thông tin khách hàng theo idKH, HoTenKH, CCCD, SDT, DiaChi, NgaySinh
                string sql = "SELECT * FROM KhachHang WHERE idKhachHang LIKE @idKH OR HoTen LIKE @HoTenKH OR CCCD LIKE @CCCD OR SDT LIKE @SDT OR DiaChi LIKE @DiaChi OR NgaySinh LIKE @NgaySinh";
                connDB.cmd = new SqlCommand(sql, connDB.conn);
                connDB.cmd.Parameters.AddWithValue("@idKH", "%" + TxtTim.Text + "%");
                connDB.cmd.Parameters.AddWithValue("@HoTenKH", "%" + TxtTim.Text + "%");
                connDB.cmd.Parameters.AddWithValue("@CCCD", "%" + TxtTim.Text + "%");
                connDB.cmd.Parameters.AddWithValue("@SDT", "%" + TxtTim.Text + "%");
                connDB.cmd.Parameters.AddWithValue("@DiaChi", "%" + TxtTim.Text + "%");
                connDB.cmd.Parameters.AddWithValue("@NgaySinh", "%" + TxtTim.Text + "%");
                if (connDB.conn.State == ConnectionState.Closed)
                {
                    connDB.conn.Open();
                }
                connDB.dta = connDB.cmd.ExecuteReader();
                connDB.dt = new DataTable();
                connDB.dt.Load(connDB.dta);
                DsKhachHang.DataSource = connDB.dt;
                connDB.conn.Close();
            }
        }

        private void TxtTim_TextChanged(object sender, EventArgs e)
        {
            //Reset lại DsKhachHang khi không có thông tin tìm kiếm
            if (TxtTim.Text == "")
            {
                FrmInfoKhachHang_Load(sender, e);
            }
        }

    }
}
