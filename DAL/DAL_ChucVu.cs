using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class DAL_ChucVu : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string macv)
        {
            string sql = "SELECT COUNT(1) FROM CHUCVU WHERE MACV = @macv";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@macv", macv);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }
        }
        public bool Insert (ChucVu cv)
        {
            if (!Check(cv.maCV))
            {
                string sql = "INSERT INTO CHUCVU VALUES (@macv, @tencv)";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@macv", SqlDbType.VarChar).Value = cv.maCV;
                    cmd.Parameters.Add("@tencv", SqlDbType.NVarChar).Value = cv.tenCV;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
        public void Update(ChucVu cv)
        {
            string sql = "UPDATE CHUCVU SET TENCV = @tencv WHERE MACV = @macv";
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@macv", SqlDbType.VarChar).Value = cv.maCV;
                cmd.Parameters.Add("@tencv", SqlDbType.NVarChar).Value = cv.tenCV;
                ExecuteCMD(cmd);
            }
        }
        public void Delete (ChucVu cv)
        {
            string sql = "DELETE FROM CHUCVU WHERE MACV = @macv";
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@macv", SqlDbType.VarChar).Value = cv.maCV;
                ExecuteCMD(cmd);
            }
        }
    }
}
