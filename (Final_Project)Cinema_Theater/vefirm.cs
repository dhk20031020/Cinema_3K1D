using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Final_Project_Cinema_Theater
{
    internal class vefirm
    {
        string IDVE;


        public string idGhe { get; set; }
        public string TongTien { get; set; }



        public vefirm(string term)
        {
            this.IDVE = term;
        }
      

        public vefirm( string idGhe,string tongtien)
        {

            idGhe = idGhe;
            TongTien = tongtien;

        }

        string chuoiKetNoi = @"Data Source=DESKTOP-3TRF427\HOANGKA;Initial Catalog=QLRapPhim;Integrated Security=True";



        public List<vefirm> Getvefirm(string idhd)
        {
            using (SqlConnection connection = new SqlConnection(chuoiKetNoi))
            {
                connection.Open();

                string sqlkh = $"SELECT MaGheNgoi,TienBanVe FROM VE WHERE idVe = '{37}'";

                using (SqlCommand command = new SqlCommand(sqlkh, connection))
                {
                    command.Parameters.AddWithValue("@IDHD", idhd);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<vefirm> list = new List<vefirm>();
                        while (reader.Read())
                        {
                            list.Add(new vefirm(

                                reader.GetString(0),
                                reader.GetString(1)

                            ));
                        }
                        return list;
                    }
                }
            }
        }
    }

}

