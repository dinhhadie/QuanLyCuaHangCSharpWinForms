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
    public class DAL_ChiTietPhieuXuat : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string mapx, string mahh)
        {
            string sql = "SELECT COUNT(1) FROM CHITIETPHIEUXUAT WHERE MAPX = @mapx AND MAHH = @mahh";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@mapx", SqlDbType.VarChar).Value = mapx;
                cmd.Parameters.Add("@mahh", SqlDbType.VarChar).Value = mahh;
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }    
        }
        public bool Insert(ChiTietPhieuXuat ctpx)
        {
            if (!Check(ctpx.MaPX, ctpx.MaHH))
            {
                string sql = "INSERT INTO CHITIETPHIEUXUAT VALUES (@mapx, @mahh, @sluong)";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@mapx", SqlDbType.VarChar).Value = ctpx.MaPX;
                    cmd.Parameters.Add("@mahh", SqlDbType.VarChar).Value = ctpx.MaHH;
                    cmd.Parameters.Add("@sluong", SqlDbType.Int).Value = ctpx.SLuong;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
