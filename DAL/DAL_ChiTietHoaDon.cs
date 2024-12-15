using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Data.SqlClient;
using DTO;
namespace DAL
{
    public class DAL_ChiTietHoaDon : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string mahd, string mahh)
        {
            string sql = "SELECT COUNT(1) FROM CHITIETHOADON WHERE MAHD = @mahd AND MAHH = @mahh";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@mahd", mahd);
                cmd.Parameters.AddWithValue("@mahh", mahh);
                con.Open();
                int count  = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }    
        }
        public bool Insert(ChiTietHoaDon cthd, HoaDon hd)
        {
            if (!Check(cthd.MaHD, cthd.MaHH))
            {
                string sql = @"BEGIN TRANSACTION;
                            INSERT INTO CHITIETHOADON VALUES (@mahd, @mahh, @soluong, @dongia, @vat, @thanhtien);
                            UPDATE HOADONBANHANG
                            SET TongTienHD = (
                                                SELECT SUM(THANHTIEN)
                                                FROM CHITIETHOADON
                                                WHERE CHITIETHOADON.MAHD = HOADONBANHANG.MAHD
                                                )
                            WHERE HOADONBANHANG.MAHD = @mahd;
                            COMMIT TRANSACTION;";
                            
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@mahd", SqlDbType.VarChar).Value = cthd.MaHD;
                    cmd.Parameters.Add("@mahH", SqlDbType.VarChar).Value = cthd.MaHH;
                    cmd.Parameters.Add("@soluong", SqlDbType.Int).Value = cthd.SoLuong;
                    cmd.Parameters.Add("@dongia", SqlDbType.Decimal).Value = cthd.DonGia;
                    cmd.Parameters.Add("@vat", SqlDbType.Decimal).Value = cthd.Tile_VAT;
                    cmd.Parameters.Add("@thanhtien", SqlDbType.Decimal).Value = cthd.ThanhTien;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
