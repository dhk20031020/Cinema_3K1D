using _Final_Project_Cinema_Theater;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace datve11
{
    public partial class FrmBanVe : Form
    {
        SQLCONNECTION connDB = new SQLCONNECTION();
        public static FrmBanVe Instance { get; private set; }

        private Dictionary<Button, bool> selectedSeats = new Dictionary<Button, bool>();

        private int plusPoint = 0; // Add the plusPoint field

        // Constructor mới để nhận idLichChieu
        public FrmBanVe(string idLichChieu)
        {
            InitializeComponent();
            Instance = this;
            this.IdLichChieu = idLichChieu; // Gán idLichChieu được truyền vào
            CreateSeats();
        }

        private string idLichChieu;

        public string IdLichChieu
        {
            get { return idLichChieu; }
            set { idLichChieu = value; }
        }


        public string HotenLabel
        {
            get { return lbTenkh.Text; }
            set { lbTenkh.Text = value; }
        }

        public int DiemTichLuy
        {
            get { return int.Parse(lbPoint.Text); }
            set { lbPoint.Text = value.ToString(); }
        }

        int rows = 5;
        int columns = 8;
        int gap = 5;

        private void checkkhachhangthanhvien_CheckedChanged(object sender, EventArgs e)
        {
            if (checkkhachhangthanhvien.Checked)
            {
                //Hiển thị thông tin khách hàng thành viên bằng form loginthanhvien
                loginthanhvien f = new loginthanhvien();
                f.ShowDialog();
                //nếu không có tên khách hàng thì bấm nút đăng nhập thì ko hiện pnCustomer
                if (string.IsNullOrEmpty(lbTenkh.Text))
                {
                    pnCustomer.Visible = false;
                    checkkhachhangthanhvien.Checked = false;
                }
                else
                {
                    pnCustomer.Visible = true;
                }
            }
            else
            {
                pnCustomer.Visible = false;
            }
        }

        private void btnSeat_Click(object sender, EventArgs e)
        {
            Button ghe = sender as Button;

            if (!selectedSeats.ContainsKey(ghe))
            {
                selectedSeats.Add(ghe, false);
            }

            char row = ghe.Text[0];
            int column = int.Parse(ghe.Text.Substring(2));
            string loaiGhe = GetLoaiGhe(row, column);

            // kiểm tra loại ghế được chọn có hợp lệ hay không
            if (LoaiGheHopLe(loaiGhe))
            {
                if (selectedSeats[ghe] == false)
                {
                    // Nếu ghế chưa được chọn, đánh dấu là đã chọn
                    selectedSeats[ghe] = true;
                    ghe.BackColor = Color.Red; // Đổi màu để chỉ ra ghế đã được chọn

                    // Tăng điểm cộng khi chọn ghế
                    plusPoint++;
                }
                else
                {
                    // Nếu ghế đã được chọn, hủy chọn
                    selectedSeats[ghe] = false;
                    // Phục hồi màu ban đầu của ghế
                    RestoreSeatColor(ghe);

                    // Giảm điểm cộng khi hủy chọn ghế
                    if (plusPoint > 0)
                    {
                        plusPoint--;
                    }
                }

                // Cập nhật điểm cộng lên label
                lbdiemcong.Text = plusPoint.ToString();
                //Lưu lại điểm cộng trong database trong bảng KhachHang cột DiemTichLuy bằng cách lấy điểm từ database cộng thêm điểm cộng mới
                string sql = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + @DiemTichLuy WHERE HoTen = @HoTen";
                using (SqlConnection connection = new SqlConnection(connDB.conn.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@DiemTichLuy", plusPoint);
                    command.Parameters.AddWithValue("@HoTen", lbTenkh.Text);
                    command.ExecuteNonQuery();
                }

                // Cập nhật giá tiền của các ghế
                UpdatePrices();
            }
            else
            {
                MessageBox.Show("Bạn chỉ có thể chọn 1 loại ghế", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RestoreSeatColor(ghe);
                UpdatePrices();
            }
        }

        // Phương thức để trả về loại ghế dựa trên hàng và cột
        private string GetLoaiGhe(char row, int column)
        {
            if (row < 'C')
            {
                return "Thường";
            }
            else if (row < 'E' || (row == 'E' && column < 1))
            {
                return "VIP";
            }
            else
            {
                return "Deluxe";
            }
        }

        public void ResetTxt()
        {
            txtDoiDiem.Text = "";
            lblDiscount.Text = "";
            lbTotal.Text = "";
            lbdiemcong.Text = "";
        }

        // Phương thức kiểm tra loại ghế có hợp lệ hay không
        private bool LoaiGheHopLe(string loaiGhe)
        {
            foreach (var seat in selectedSeats)
            {
                Button btnSeat = seat.Key;
                bool isSelected = seat.Value;

                if (isSelected)
                {
                    char row = btnSeat.Text[0];
                    int column = int.Parse(btnSeat.Text.Substring(2));
                    string currentLoaiGhe = GetLoaiGhe(row, column);

                    if (currentLoaiGhe != loaiGhe)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void DiemTichLuyKhachHang()
        {
            //Hàm dùng để cập nhật điểm tích lũy của khách hàng sau khi mua vé bằng cách lấy điểm từ database trừ đi điểm cộng đã dùng từ TxtDiemDoi
            string sql = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy - @DiemTichLuy WHERE HoTen = @HoTen";
            using (SqlConnection connection = new SqlConnection(connDB.conn.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@DiemTichLuy", int.Parse(txtDoiDiem.Text));
                command.Parameters.AddWithValue("@HoTen", lbTenkh.Text);
                command.ExecuteNonQuery();
            }
        }

        // Phục hồi màu ban đầu của ghế
        private void RestoreSeatColor(Button ghe)
        {
            char row = ghe.Text[0];
            int column = int.Parse(ghe.Text.Substring(2));
            if (row < 'C')
            {
                ghe.BackColor = Color.Pink;
            }
            else if (row < 'E' || (row == 'E' && column < 1))
            {
                ghe.BackColor = Color.Yellow;
            }
            else
            {
                ghe.BackColor = Color.Violet;
            }
        }

        private void UpdatePrices()
        {
            int totalPrice = 0;
            foreach (var seat in selectedSeats)
            {
                Button btnSeat = seat.Key;
                bool isSelected = seat.Value;

                if (isSelected)
                {
                    char row = btnSeat.Text[0];
                    int column = int.Parse(btnSeat.Text.Substring(2));
                    string loaiGhe = GetLoaiGhe(row, column);
                    string idLichChieu = IdLichChieu; // Lấy idLichChieu từ form BanVe hoặc từ nguồn khác

                    // Truy vấn dữ liệu giá vé từ bảng LichChieu dựa trên idLichChieu và loại ghế
                    string query = "SELECT GiaVe FROM LichChieu WHERE idLichChieu = @idLichChieu";
                    //Using theo connDB.conn
                    using (SqlConnection connection = new SqlConnection(connDB.conn.ConnectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@idLichChieu", idLichChieu);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            int ticketPrice = Convert.ToInt32(result);

                            // Thực hiện các thay đổi giá vé dựa trên loại ghế
                            if (loaiGhe == "Thường")
                            {
                                // Nếu là ghế thường
                                totalPrice += ticketPrice;
                            }
                            else if (loaiGhe == "VIP")
                            {
                                // Nếu là ghế VIP
                                totalPrice += ticketPrice + 5000;
                            }
                            else if (loaiGhe == "Deluxe")
                            {
                                // Nếu là ghế Deluxe
                                totalPrice += ticketPrice + 10000;
                            }
                        }
                    }
                }
            }
            // Cập nhật tổng giá vé
            lbTotal.Text = $"{totalPrice:#,0} VND";
            int discount = 0;
            if (!string.IsNullOrEmpty(lblDiscount.Text))
            {
                string discountText = lblDiscount.Text.Replace("(-", "").Replace(" VND)", "").Replace(",", "");
                if (int.TryParse(discountText, out int discountValue))
                {
                    discount = discountValue;
                }
                else
                {
                    MessageBox.Show("Giá trị chiết khấu không đúng định dạng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Calculate and display payment amount
            int payment = totalPrice - discount;
            lbPayment.Text = $"{payment:#,0} VND";
        }


        private void CreateSeats()
        {
            PanelGhe.Controls.Clear();

            List<string> rowLabels = new List<string>() { "A", "B", "C", "D", "E" };
            int panelWidth = PanelGhe.ClientSize.Width;
            int panelHeight = PanelGhe.ClientSize.Height;
            int seatWidth = (panelWidth - (columns + 1) * gap) / columns;
            int seatHeight = (panelHeight - (rows + 1) * gap) / rows;

            // Tạo danh sách các ghế đã được chọn
            List<string> selectedSeats = new List<string>();
            string selectedSeatsQuery = "SELECT MaGheNgoi FROM Ve WHERE idLichChieu = @idLichChieu";

            using (SqlConnection connection = new SqlConnection(connDB.conn.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(selectedSeatsQuery, connection);
                command.Parameters.AddWithValue("@idLichChieu", IdLichChieu);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string[] maGheNgoi = reader["MaGheNgoi"].ToString().Split(',');
                    selectedSeats.AddRange(maGheNgoi);
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    string seatName = $"{rowLabels[i]}_{j + 1}";
                    Button btnSeat = new Button();
                    btnSeat.Name = seatName;
                    btnSeat.Text = seatName;
                    btnSeat.Size = new Size(seatWidth, seatHeight);
                    btnSeat.Location = new Point(j * (seatWidth + gap) + gap, i * (seatHeight + gap) + gap);

                    // Set the button color based on the row label
                    if (rowLabels[i] == "A" || rowLabels[i] == "B")
                    {
                        btnSeat.BackColor = Color.Pink;
                    }
                    else if (rowLabels[i] == "C" || rowLabels[i] == "D")
                    {
                        btnSeat.BackColor = Color.Yellow;
                    }
                    else if (rowLabels[i] == "E")
                    {
                        btnSeat.BackColor = Color.Violet;
                    }
                    else
                    {
                        btnSeat.BackColor = Color.White;
                    }

                    // Kiểm tra xem ghế đã được chọn hay chưa
                    if (selectedSeats.Contains(seatName))
                    {
                        btnSeat.Enabled = false;
                        btnSeat.BackColor = Color.DarkGray;
                    }

                    btnSeat.Click += btnSeat_Click;
                    PanelGhe.Controls.Add(btnSeat);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn hủy tất cả những vé đã chọn không?",
        "Hủy Mua Vé", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            // Khôi phục màu ban đầu của các hàng ghế
            foreach (var seat in selectedSeats.ToList())
            {
                Button btnSeat = seat.Key;
                // Kiểm tra xem ghế đã được thanh toán thành công hay chưa
                if (btnSeat.BackColor == Color.Gray)
                {
                    MessageBox.Show("Không thể hủy ghế đã thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue; // Bỏ qua ghế đã thanh toán và tiếp tục vòng lặp
                }
                // Nếu ghế chưa được thanh toán, khôi phục màu ghế
                RestoreSeatColor(btnSeat);
                btnSeat.Enabled = true;
                selectedSeats.Remove(btnSeat); // Loại bỏ ghế khỏi danh sách đã chọn
            }

            // Cập nhật lại giá trị và trạng thái các ghế đã chọn
            plusPoint = 0;
            lbdiemcong.Text = plusPoint.ToString();
            UpdatePrices();

        }

        public void SetMovieTitle(string movieTitle, string gioChieuFormatted)
        {
            // Hiển thị tiêu đề cho chỗ đặt ghế xem phim
            LbTieuDe.Text = $"Chỗ đặt ghế xem phim - {movieTitle} \r\n Thời gian: {gioChieuFormatted}";
        }

        private void btnFreeTicket_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDoiDiem.Text, out int pointsToExchange))
            {
                MessageBox.Show("Vui lòng nhập số điểm hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int currentPoints = int.Parse(lbPoint.Text);
            if (pointsToExchange > currentPoints)
            {
                MessageBox.Show("Số điểm đổi không được vượt quá số điểm hiện có.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int discount = pointsToExchange * 1000;
            lblDiscount.Text = $"(-{discount:#,0} VND)";
            int pointsAfterExchange = currentPoints - pointsToExchange;
            lbPoint.Text = pointsAfterExchange.ToString();

            UpdatePrices();
        }


        private void btnPayment_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn ghế đỏ (đã chọn) chưa
            bool hasSelectedSeat = false;
            List<string> selectedSeatsList = new List<string>();
            foreach (var seat in selectedSeats)
            {
                if (seat.Key.BackColor == Color.Red)
                {
                    hasSelectedSeat = true;
                    selectedSeatsList.Add(seat.Key.Text);
                }
            }

            // Nếu không có ghế nào được chọn, thông báo và return
            if (!hasSelectedSeat)
            {
                MessageBox.Show("Vui lòng chọn ghế trước khi thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lưu thông tin vé vào bảng Ve chỉ cho các ghế đang chọn (màu đỏ)
            string query = "INSERT INTO Ve (LoaiVe, idLichChieu, MaGheNgoi, idKhachHang, TrangThai, TienBanVe, idNV, NgayMua) VALUES (@LoaiVe, @idLichChieu, @MaGheNgoi, @idKhachHang, @TrangThai, @TienBanVe, @idNV, @NgayMua)";
            using (SqlConnection connection = new SqlConnection(connDB.conn.ConnectionString))
            {
                connection.Open();

                // Lấy idNV từ form FrmLogin theo tên nhân viên đăng nhập theo tên tài khoản của bảng TaiKhoan 
                string getNhanVienQuery = "SELECT idNV FROM TaiKhoan WHERE UserName = @UserName";
                SqlCommand getNhanVienCommand = new SqlCommand(getNhanVienQuery, connection);
                getNhanVienCommand.Parameters.AddWithValue("@UserName", FrmLogin.username);
                object resultNhanVien = getNhanVienCommand.ExecuteScalar();
                if (resultNhanVien == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên với tên đăng nhập đã cho.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string idNV = resultNhanVien.ToString();

                // Kiểm tra xem khách hàng là thành viên hay không
                string tenKhachHang = lbTenkh.Text;
                string idKhachHang = null;
                int currentDiemTichLuy = 0;
                bool isKhachHangThanhVien = false;

                if (!string.IsNullOrEmpty(tenKhachHang))
                {
                    // Nếu tên khách hàng được nhập, tiến hành lấy idKhachHang từ CSDL
                    string getKhachHangQuery = "SELECT idKhachHang, DiemTichLuy FROM KhachHang WHERE HoTen = @HoTen";
                    SqlCommand getKhachHangCommand = new SqlCommand(getKhachHangQuery, connection);
                    getKhachHangCommand.Parameters.AddWithValue("@HoTen", tenKhachHang);
                    SqlDataReader khReader = getKhachHangCommand.ExecuteReader();
                    if (khReader.Read())
                    {
                        idKhachHang = khReader["idKhachHang"].ToString();
                        currentDiemTichLuy = Convert.ToInt32(khReader["DiemTichLuy"]);
                        isKhachHangThanhVien = true;
                    }
                    khReader.Close();
                }

                // Cập nhật điểm tích lũy nếu là khách hàng thành viên
                if (isKhachHangThanhVien)
                {
                    if (!int.TryParse(txtDoiDiem.Text, out int pointsToExchange))
                    {
                        MessageBox.Show("Vui lòng nhập số điểm hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    int pointsAfterExchange = currentDiemTichLuy - pointsToExchange + int.Parse(lbdiemcong.Text);

                    // Cập nhật điểm tích lũy của khách hàng trong cơ sở dữ liệu
                    string updatePointsQuery = "UPDATE KhachHang SET DiemTichLuy = @DiemTichLuy WHERE idKhachHang = @idKhachHang";
                    SqlCommand updatePointsCommand = new SqlCommand(updatePointsQuery, connection);
                    updatePointsCommand.Parameters.AddWithValue("@DiemTichLuy", pointsAfterExchange);
                    updatePointsCommand.Parameters.AddWithValue("@idKhachHang", idKhachHang);
                    updatePointsCommand.ExecuteNonQuery();
                }

                // Gom nhóm các ghế đã chọn thành một chuỗi
                string combinedSeats = string.Join(",", selectedSeatsList);

                // Lấy loại vé từ loại ghế đầu tiên (giả sử tất cả các ghế đều có cùng loại)
                char row = selectedSeatsList[0][0]; // Lấy dòng để xác định loại ghế
                int column = int.Parse(selectedSeatsList[0].Substring(2));
                string loaiVe = GetLoaiGhe(row, column);

                // Chuyển đổi loại ghế thành giá trị số
                int loaiVeValue;
                switch (loaiVe)
                {
                    case "Thường":
                        loaiVeValue = 1;
                        break;
                    case "VIP":
                        loaiVeValue = 2;
                        break;
                    case "Deluxe":
                        loaiVeValue = 3;
                        break;
                    default:
                        loaiVeValue = 1; // Mặc định là 1 nếu không phù hợp
                        break;
                }

                // Lấy TienBanVe từ giá vé
                if (!int.TryParse(lbPayment.Text.Replace(",", "").Replace(" VND", ""), out int tienBanVe))
                {
                    MessageBox.Show("Giá vé không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Thực hiện lưu thông tin vé vào bảng Ve
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LoaiVe", loaiVeValue);
                command.Parameters.AddWithValue("@idLichChieu", IdLichChieu);
                command.Parameters.AddWithValue("@MaGheNgoi", combinedSeats);
                command.Parameters.AddWithValue("@idKhachHang", string.IsNullOrEmpty(idKhachHang) ? (object)DBNull.Value : idKhachHang);
                command.Parameters.AddWithValue("@TrangThai", 1); // 1 represents "đã đặt"
                command.Parameters.AddWithValue("@TienBanVe", tienBanVe);
                command.Parameters.AddWithValue("@idNV", idNV);
                // Lấy cả ngày và giờ mua vé
                command.Parameters.AddWithValue("@NgayMua", DateTime.Now);
                command.ExecuteNonQuery();
            }

            // Hiển thị thông báo khi mua vé thành công và đặt những ghế đã chọn thành màu đỏ và enable = false
            MessageBox.Show("Mua vé thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            foreach (var seat in selectedSeats.ToList())
            {
                Button btnSeat = seat.Key;

                // Nếu ghế đang chọn (màu đỏ) thì đặt lại màu và trạng thái của ghế
                if (btnSeat.BackColor == Color.Red)
                {
                    btnSeat.BackColor = Color.Gray;
                    btnSeat.Enabled = false;
                    selectedSeats.Remove(btnSeat);
                }
            }

            plusPoint = 0;
            lbdiemcong.Text = plusPoint.ToString();
            UpdatePrices();
            InHoaDon frm = new InHoaDon();
            frm.Show();
        }

        private void FrmBanVe_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            //Kiểm tra xem form InHoaDon có mở không, nếu có thì đóng lại
            if (Application.OpenForms["InHoaDon"] != null)
            {
                Application.OpenForms["InHoaDon"].Close();
            }
            //Mở lại form BanVe
            BanVe frm = new BanVe();
            //frm dưới dạng hàm con
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}