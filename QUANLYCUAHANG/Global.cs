using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QUANLYCUAHANG
{
    public static class Global
    {
        public static SqlConnection con = new SqlConnection("Data Source=LAPTOP-3ICHCQG9\\MSSQLSERVER2022;Initial Catalog=QUANLYCUAHANG;Integrated Security=True");
        
        public static string manv {  get; set; }
        public static string tendangnhap { get; set; }
        public static string chucvu { get; set; }

        public static string mahd {  get; set; }
        public static bool Dangnhap(string tencv, string tendn, string matkhau)
        {
            string sql = "SELECT COUNT(*) FROM NHANVIEN INNER JOIN CHUCVU ON CHUCVU.MACV = NHANVIEN.MACV INNER JOIN QUYENDANGNHAP ON NHANVIEN.MANV = QUYENDANGNHAP.MANV WHERE TENCV = @tencv AND TENDANGNHAP = @tendn AND MATKHAU = @matkhau";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("tencv", SqlDbType.NVarChar).Value = tencv;
            cmd.Parameters.Add("tendn", SqlDbType.VarChar).Value = tendn;
            cmd.Parameters.Add("matkhau", SqlDbType.VarChar).Value = matkhau;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return count > 0; 
            
        }
        public static string Chuyendoi(string tennv)
        {
            string tendangnhap = tennv.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in tendangnhap)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark && c != ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().ToLower();
        }
        public static DataTable LoadDL(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(sql, Global.con);
            ad.Fill(dt);
            return dt;
        }
        
    }
}
