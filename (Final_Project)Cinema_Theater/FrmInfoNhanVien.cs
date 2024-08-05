using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _Final_Project_Cinema_Theater
{
    public partial class FrmInfoNhanVien : Form
    {
        public FrmInfoNhanVien()
        {
            InitializeComponent();
        }
        //Lưu giá trị hiện lên ở TxtID và TxtTen  
        public static string idNV;
        public static string nameNV;
        //Dùng class SQLCONNECTION để kết nối database 
        SQLCONNECTION connDB = new SQLCONNECTION();
        SQLCONNECTION cmd = new SQLCONNECTION();
        SQLCONNECTION dta = new SQLCONNECTION();
        SQLCONNECTION dt = new SQLCONNECTION();
        public void Reset()
        {
            TxtCCCD.Text = "";
            TxtDiaChi.Text = "";
            TxtID.Text = "";
            TxtTen.Text = "";
            MTxtSDT.Text = "";
            AnhNhanVien.Image = null;
        }
        //Hàm kiểm tra cú pháp idNV
        private bool CheckIDNV(string idNV)
        {
            return idNV.Substring(0, 2) == "NV" && idNV.Substring(2).All(char.IsDigit);
        }
        private bool CheckTrungCCCD(string CCCD)
        {
            string sqlCheck = "SELECT COUNT(*) FROM NhanVien WHERE CCCD = @CCCD";
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
        // Hàm kiểm tra trùng IDNV
        private bool CheckTrungIDNV(string idNV)
        {
            string sqlCheck = "SELECT COUNT(*) FROM NhanVien WHERE idNV = @idNV";
            using (SqlCommand cmd = new SqlCommand(sqlCheck, connDB.conn))
            {
                cmd.Parameters.AddWithValue("@idNV", idNV);
                if (connDB.conn.State != ConnectionState.Open)
                {
                    connDB.conn.Open();
                }
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        private void FrmInfoNhanVien_Load(object sender, EventArgs e)
        {
            // Tạo câu lệnh sql và dữ liệu vào DsNhanVien
            string sql = "select * from NhanVien";
            if (connDB.conn.State == ConnectionState.Closed)
            {
                connDB.conn.Open();
            }
            connDB.cmd = new SqlCommand(sql, connDB.conn);
            connDB.dta = connDB.cmd.ExecuteReader();
            connDB.dt = new DataTable();
            connDB.dt.Load(connDB.dta);
            DsNhanVien.DataSource = connDB.dt;
            connDB.conn.Close();
        }

        private void DsNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Khi click vào 1 dòng trong DsNhanVien thì sẽ hiện thông tin của dòng đó lên các textbox và AnhNhanVien
            int i = DsNhanVien.CurrentRow.Index;
            TxtID.Text = DsNhanVien.Rows[i].Cells[0].Value.ToString();
            TxtTen.Text = DsNhanVien.Rows[i].Cells[1].Value.ToString();
            DtpNgaySinh.Text = DsNhanVien.Rows[i].Cells[2].Value.ToString();
            TxtDiaChi.Text = DsNhanVien.Rows[i].Cells[3].Value.ToString();
            MTxtSDT.Text = DsNhanVien.Rows[i].Cells[4].Value.ToString();
            TxtCCCD.Text = DsNhanVien.Rows[i].Cells[5].Value.ToString();
            AnhNhanVien.Image = (Bitmap)DsNhanVien.Rows[i].Cells[6].FormattedValue;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtID.Text) || string.IsNullOrEmpty(TxtTen.Text) || string.IsNullOrEmpty(DtpNgaySinh.Text) || string.IsNullOrEmpty(TxtDiaChi.Text) || string.IsNullOrEmpty(MTxtSDT.Text) || string.IsNullOrEmpty(TxtCCCD.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (TxtCCCD.Text.Length != 12 || !TxtCCCD.Text.All(char.IsDigit))
            {
                MessageBox.Show("CCCD phải là 12 ký tự số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!CheckIDNV(TxtID.Text))
            {
                MessageBox.Show("IDNV phải bắt đầu bằng NV và theo sau là 4 ký tự số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (CheckTrungIDNV(TxtID.Text))
            {
                MessageBox.Show("IDNV đã tồn tại, vui lòng chọn IDNV khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (CheckTrungCCCD(TxtCCCD.Text))
            {
                MessageBox.Show("CCCD đã tồn tại, vui lòng chọn CCCD khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (AnhNhanVien.Image == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MTxtSDT.Text.Length != 10 || !MTxtSDT.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (connDB.conn.State == ConnectionState.Closed)
                    {
                        connDB.conn.Open();
                    }

                    string sql = "INSERT INTO NhanVien VALUES(@ID, @Ten, @NgaySinh, @DiaChi, @SDT, @CCCD, @HinhAnh)";
                    using (SqlCommand cmd = new SqlCommand(sql, connDB.conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", TxtID.Text);
                        cmd.Parameters.AddWithValue("@Ten", TxtTen.Text);
                        cmd.Parameters.AddWithValue("@NgaySinh", DtpNgaySinh.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", TxtDiaChi.Text);
                        cmd.Parameters.AddWithValue("@SDT", MTxtSDT.Text);
                        cmd.Parameters.AddWithValue("@CCCD", TxtCCCD.Text);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            AnhNhanVien.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] imageBytes = ms.ToArray();
                            cmd.Parameters.AddWithValue("@HinhAnh", imageBytes);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmInfoNhanVien_Load(sender, e);
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connDB.conn.Close();
                }
            }
        }
        //Xoá nhân viên
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtID.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (FrmLogin.username == TxtID.Text)
                {
                    MessageBox.Show("Không thể xoá tài khoản đang đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá nhân viên này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string sqlVe = "update Ve set idNV = null where idNV = '" + TxtID.Text + "'";
                    string sql = "delete from TaiKhoan where idNV = '" + TxtID.Text + "'";
                    string sql2 = "delete from NhanVien where idNV = '" + TxtID.Text + "'";
                    if (connDB.conn.State == ConnectionState.Closed)
                    {
                        connDB.conn.Open();
                    }
                    connDB.cmd = new SqlCommand(sqlVe, connDB.conn);
                    connDB.cmd.ExecuteNonQuery();
                    connDB.cmd = new SqlCommand(sql, connDB.conn);
                    connDB.cmd.ExecuteNonQuery();
                    connDB.cmd = new SqlCommand(sql2, connDB.conn);
                    connDB.cmd.ExecuteNonQuery();

                    MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    FrmInfoNhanVien_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connDB.conn.Close();
            }
        }
        //Chuyển sang form tài khoản
        private void button5_Click(object sender, EventArgs e)
        {
            idNV = TxtID.Text;
            nameNV = TxtTen.Text;
            if (TxtID.Text == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                FrmTaiKhoan frm = new FrmTaiKhoan();
                frm.MdiParent = this.MdiParent;
                frm.Show();
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //Tìm thông tin nhân viên dựa trên TxtTim theo idNV, HoTen, SDT, CCCD
            string sql = "select * from NhanVien where idNV like '%" + TxtTim.Text + "%' or HoTen like N'%" + TxtTim.Text + "%' or SDT like '%" + TxtTim.Text + "%' or CCCD like '%" + TxtTim.Text + "%'";
            if (connDB.conn.State == ConnectionState.Closed)
            {
                connDB.conn.Open();
            }
            connDB.cmd = new SqlCommand(sql, connDB.conn);
            connDB.dta = connDB.cmd.ExecuteReader();
            connDB.dt = new DataTable();
            connDB.dt.Load(connDB.dta);
            DsNhanVien.DataSource = connDB.dt;
            connDB.conn.Close();
        }

        private void TxtTim_TextChanged(object sender, EventArgs e)
        {
            if (TxtTim.Text == "")
            {
                FrmInfoNhanVien_Load(sender, e);
            }
        }
        //Reset lại các textbox và ảnh 
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Sửa thông tin nhân viên bao gồm HoTen, NgaySinh, DiaChi, SDT, CCCD, HinhAnh
            if (string.IsNullOrEmpty(TxtID.Text) || string.IsNullOrEmpty(TxtTen.Text) || string.IsNullOrEmpty(DtpNgaySinh.Text) || string.IsNullOrEmpty(TxtDiaChi.Text) || string.IsNullOrEmpty(MTxtSDT.Text) || string.IsNullOrEmpty(TxtCCCD.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (TxtCCCD.Text.Length != 12 || !TxtCCCD.Text.All(char.IsDigit))
            {
                MessageBox.Show("CCCD phải là 12 ký tự số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (AnhNhanVien.Image == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MTxtSDT.Text.Length != 10 || !MTxtSDT.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (connDB.conn.State == ConnectionState.Closed)
                    {
                        connDB.conn.Open();
                    }

                    string sql = "UPDATE NhanVien SET HoTen = @Ten, NgaySinh = @NgaySinh, DiaChi = @DiaChi, SDT = @SDT, CCCD = @CCCD, HinhAnh = @HinhAnh WHERE idNV = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, connDB.conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", TxtID.Text);
                        cmd.Parameters.AddWithValue("@Ten", TxtTen.Text);
                        cmd.Parameters.AddWithValue("@NgaySinh", DtpNgaySinh.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", TxtDiaChi.Text);
                        cmd.Parameters.AddWithValue("@SDT", MTxtSDT.Text);
                        cmd.Parameters.AddWithValue("@CCCD", TxtCCCD.Text);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            AnhNhanVien.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] imageBytes = ms.ToArray();
                            cmd.Parameters.AddWithValue("@HinhAnh", imageBytes);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmInfoNhanVien_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connDB.conn.Close();
                }
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            //Chọn ảnh từ máy tính
            OpenFileDialog ofd = new OpenFileDialog();
            //Tất cả kiểu của ảnh
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                AnhNhanVien.Image = new Bitmap(ofd.FileName);
            }
        }
    }
}

