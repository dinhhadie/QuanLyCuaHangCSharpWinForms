using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
namespace DAL
{
    public class DAL_ThanhToan : Connect
    {
        public bool Check(string matt)
        {
            string sql = "SELECT COUNT(1) FROM THANHTOAN WHERE MATT = @matt";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@matt", SqlDbType.VarChar).Value = matt;
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }    
        }
        public bool Insert(ThanhToan tt, HoaDon hd)
        {
            if (!Check(tt.MaTT))
            {
                string sql = @"BEGIN TRANSACTION;
                            INSERT INTO THANHTOAN VALUES (@matt, @mahd, @ngaytt, @sotien, @phuongthuc);
                            
                            UPDATE HOADONBANHANG SET TRANGTHAI = @trangthai WHERE MAHD = @mahd;
                            COMMIT TRANSACTION;";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@matt", SqlDbType.VarChar).Value = tt.MaTT;
                    cmd.Parameters.Add("@mahd", SqlDbType.VarChar).Value = tt.MaHD;
                    cmd.Parameters.Add("@ngaytt", SqlDbType.Date).Value = tt.NgayThanhToan;
                    cmd.Parameters.Add("@sotien", SqlDbType.Decimal).Value = tt.SoTien;
                    cmd.Parameters.Add("@phuongthuc", SqlDbType.NVarChar).Value = tt.PhuongThuc;
                    cmd.Parameters.Add("@trangthai", SqlDbType.NVarChar).Value = hd.TrangThai;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
