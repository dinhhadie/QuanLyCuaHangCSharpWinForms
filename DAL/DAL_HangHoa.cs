using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_HangHoa : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string mahh)
        {
            string sql = "SELECT COUNT(1) FROM HANGHOA WHERE MAHH = @mahh";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@mahh", mahh);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }
        }
        public bool Insert(HangHoa hh)
        {
            if (!Check(hh.MaHH))
            {
                string sql = "INSERT INTO HANGHOA VALUES (@mahh, @manh, @tenhh, @dvt, @trongluong, @hsd, @nsx)";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@mahh", SqlDbType.VarChar).Value = hh.MaHH;
                    cmd.Parameters.Add("@manh", SqlDbType.VarChar).Value = hh.MaNH;
                    cmd.Parameters.Add("@tenhh", SqlDbType.NVarChar).Value = hh.TenHH;
                    cmd.Parameters.Add("@dvt", SqlDbType.NVarChar).Value = hh.DonViTinh;
                    cmd.Parameters.Add("@trongluong", SqlDbType.Float).Value = hh.TrongLuong;
                    cmd.Parameters.Add("@hsd", SqlDbType.Date).Value = hh.HanSD;
                    cmd.Parameters.Add("@nsx", SqlDbType.NVarChar).Value = hh.NoiSX;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
        
    }
}
