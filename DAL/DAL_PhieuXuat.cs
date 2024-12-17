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
    public class DAL_PhieuXuat : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string mapx)
        {
            string sql = "SELECT COUNT(1) FROM PHIEUXUAT WHERE MAPX = @mapx";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@mapx", SqlDbType.VarChar).Value = mapx;
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }    
        }
        public bool Insert(PhieuXuat px)
        {
            if (!Check(px.MaPX))
            {
                string sql = "INSERT INTO PHIEUXUAT VALUES (@mapx, @manv, @ngayxuat, @makh, @trangthai)";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@mapx", SqlDbType.VarChar).Value = px.MaPX;
                    cmd.Parameters.Add("@manv", SqlDbType.VarChar).Value = px.MaNV;
                    cmd.Parameters.Add("@ngayxuat", SqlDbType.Date).Value = px.NgayXuat;
                    cmd.Parameters.Add("@makh", SqlDbType.VarChar).Value = px.MaKH;
                    cmd.Parameters.Add("@trangthai", SqlDbType.NVarChar).Value = px.TrangThai ?? (object)DBNull.Value;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
