using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_KhachHang : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string makh)
        {
            string sql = "SELECT COUNT(1) FROM KHACHHANG WHERE MAKH = @makh";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@makh", SqlDbType.VarChar).Value = makh;
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }    
        }
        public bool Insert(KhachHang kh)
        {
            if (!Check(kh.MaKH))
            {
                string sql = "INSERT INTO KHACHHANG VALUES (@makh, @tenkh, @diachi, @sdt, @email)";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@makh", SqlDbType.VarChar).Value = kh.MaKH;
                    cmd.Parameters.Add("@tenkh", SqlDbType.NVarChar).Value = kh.TenKH ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = kh.DiaChi ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@sdt", SqlDbType.VarChar).Value = kh.DienThoai ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = kh.Email ?? (object)DBNull.Value;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
        public bool Update(KhachHang kh)
        {
            if (Check(kh.MaKH))
            {
                string sql = "UPDATE KHACHHANG SET EMAIL = @email, DIENTHOAI = @dt, DIACHI = @diachi, TENKH = @tenkh WHERE MAKH = @makh";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
              
                    cmd.Parameters.Add("@tenkh", SqlDbType.NVarChar).Value = kh.TenKH ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = kh.DiaChi ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@sdt", SqlDbType.VarChar).Value = kh.DienThoai ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = kh.Email ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@makh", SqlDbType.VarChar).Value = kh.MaKH;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
