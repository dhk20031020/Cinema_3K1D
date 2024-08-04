using System;
using System.Data;
using System.Data.SqlClient;

namespace _Final_Project_Cinema_Theater
{
    internal class SQLCONNECTION
    {
        // Chuỗi kết nối
        private string connectionString;

        // Đối tượng kết nối
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataReader dta;
        public DataTable dt;

        public SQLCONNECTION()
        {
            // Khởi tạo chuỗi kết nối
            connectionString = "Data Source=DESKTOP-3TRF427\\HOANGKA;Initial Catalog=QLRapPhim;Integrated Security=True";

            // Khởi tạo đối tượng kết nối
            conn = new SqlConnection(connectionString);
        }

        // Trả về đối tượng kết nối
        public static SqlConnection GetConnection()
        {
            SQLCONNECTION sqlConnection = new SQLCONNECTION();
            return sqlConnection.conn;
        }
    }
}
