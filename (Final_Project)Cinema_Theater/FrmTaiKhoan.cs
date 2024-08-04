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
    public partial class FrmTaiKhoan : Form
    {
        public FrmTaiKhoan()
        {
            InitializeComponent();
        }

        private void FrmTaiKhoan_Load(object sender, EventArgs e)
        {
            //Nhận biến idNV của form FrmInfoNhanVien và hiển thị thông tin lên các label
            LblHoTen.Text = $"Thông tin của {FrmInfoNhanVien.nameNV}\r\n ID Nhân Viên {FrmInfoNhanVien.idNV}";
            //Kiểm tra idNV đã có tài khoản chưa
            SQLCONNECTION mycon = new SQLCONNECTION();
            mycon.conn.Open();
            string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE idNV = @idNV";
            mycon.cmd = new SqlCommand(sql, mycon.conn);
            mycon.cmd.Parameters.AddWithValue("@idNV", FrmInfoNhanVien.idNV);
            int count = Convert.ToInt32(mycon.cmd.ExecuteScalar());
            //Nếu đã có tài khoản thì sẽ hiện thông tin ra các textbox
            if (count > 0)
            {
                string sqlSelect = "SELECT * FROM TaiKhoan WHERE idNV = @idNV";
                mycon.cmd = new SqlCommand(sqlSelect, mycon.conn);
                mycon.cmd.Parameters.AddWithValue("@idNV", FrmInfoNhanVien.idNV);
                SqlDataReader dta = mycon.cmd.ExecuteReader();
                if (dta.Read() == true)
                {
                    TxtUsername.Text = dta["UserName"].ToString();
                    TxtPassword.Text = dta["Pass"].ToString();
                    if (Convert.ToInt32(dta["LoaiTK"]) == 1)
                    {
                        RbAdmin.Checked = true;
                    }
                    else if (Convert.ToInt32(dta["LoaiTK"]) == 2)
                    {
                        RbNV.Checked = true;
                    }
                }
                BtnAdd.Visible = false;
            }
            else
            {
                BtnUpdate.Visible = false;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Lưu thông tin vào trong bảng TaiKhoan
            SQLCONNECTION mycon = new SQLCONNECTION();
            mycon.conn.Open();
            // Lúc chọn radio button có giá trị là Admin thì sẽ lưu 1, Nhân viên thì giá trị lưu sẽ là 2
            int loaiTK = 0;
            if (RbAdmin.Checked == true)
            {
                loaiTK = 1;
            }
            else if (RbNV.Checked == true)
            {
                loaiTK = 2;
            }
            // Đảm bảo rằng giá trị idNV là một chuỗi ký tự trước khi sử dụng nó trong câu lệnh SQL
            //Kiểm tra các thông tin đã được nhập hết chưa và có bị trùng idNV không
            if (TxtUsername.Text == "" || TxtPassword.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Kiểm tra trùng id
                string sqlCheckDuplicateId = "SELECT COUNT(*) FROM TaiKhoan WHERE idNV = @idNV";
                mycon.cmd = new SqlCommand(sqlCheckDuplicateId, mycon.conn);
                mycon.cmd.Parameters.AddWithValue("@idNV", FrmInfoNhanVien.idNV);

                int count = Convert.ToInt32(mycon.cmd.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("ID đã tồn tại trong cơ sở dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Thêm tài khoản mới vào bảng TaiKhoan
                    string sqlInsert = "INSERT INTO TaiKhoan (UserName, Pass, LoaiTK, idNV) VALUES (@UserName, @Pass, @LoaiTK, @idNV)";
                    mycon.cmd = new SqlCommand(sqlInsert, mycon.conn);
                    mycon.cmd.Parameters.AddWithValue("@UserName", TxtUsername.Text);
                    mycon.cmd.Parameters.AddWithValue("@Pass", TxtPassword.Text);
                    mycon.cmd.Parameters.AddWithValue("@LoaiTK", loaiTK);
                    mycon.cmd.Parameters.AddWithValue("@idNV", FrmInfoNhanVien.idNV);

                    mycon.cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            this.Close();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Cập nhật thông tin tài khoản
            SQLCONNECTION mycon = new SQLCONNECTION();
            mycon.conn.Open();
            // Lúc chọn radio button có giá trị là Admin thì sẽ lưu 1, Nhân viên thì giá trị lưu sẽ là 2
            int loaiTK = 0;
            if (RbAdmin.Checked == true)
            {
                loaiTK = 1;
            }
            else if (RbNV.Checked == true)
            {
                loaiTK = 2;
            }
            // Đảm bảo rằng giá trị idNV là một chuỗi ký tự trước khi sử dụng nó trong câu lệnh SQL
            //Kiểm tra các thông tin đã được nhập hết chưa
            if (TxtUsername.Text == "" || TxtPassword.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Cập nhật thông tin tài khoản
                string sqlUpdate = "UPDATE TaiKhoan SET UserName = @UserName, Pass = @Pass, LoaiTK = @LoaiTK WHERE idNV = @idNV";
                mycon.cmd = new SqlCommand(sqlUpdate, mycon.conn);
                mycon.cmd.Parameters.AddWithValue("@UserName", TxtUsername.Text);
                mycon.cmd.Parameters.AddWithValue("@Pass", TxtPassword.Text);
                mycon.cmd.Parameters.AddWithValue("@LoaiTK", loaiTK);
                mycon.cmd.Parameters.AddWithValue("@idNV", FrmInfoNhanVien.idNV);

                mycon.cmd.ExecuteNonQuery();
                MessageBox.Show("Cập nhật tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}
