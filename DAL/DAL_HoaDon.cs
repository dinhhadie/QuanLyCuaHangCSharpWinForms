using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_HoaDon : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string mahd)
        {
            string sql = "SELECT COUNT(*) FROM HOADONBANHANG WHERE MAHD = @mahd";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("mahd", mahd);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }   
        }
        public bool Insert(HoaDon hd)
        {
            if (!Check(hd.MaHD))
            {
                string sql = "INSERT INTO HOADONBANHANG VALUES (@mahd, @manv, @makh, @ngayban, @tongtienhd, @trangthai)";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@mahd", SqlDbType.VarChar).Value = hd.MaHD;
                    cmd.Parameters.Add("@manv", SqlDbType.VarChar).Value = hd.MaNV;
                    cmd.Parameters.Add("@makh", SqlDbType.VarChar).Value = hd.MaKH;
                    cmd.Parameters.Add("@ngayban", SqlDbType.Date).Value = hd.NgayBan;
                    cmd.Parameters.Add("@tongtienhd", SqlDbType.Decimal).Value = hd.TongTienHD ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@trangthai", SqlDbType.NVarChar).Value = hd.TrangThai ?? (object)DBNull.Value;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
