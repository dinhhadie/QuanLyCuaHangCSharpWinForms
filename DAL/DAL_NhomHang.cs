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
    public class DAL_NhomHang : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string manhom)
        {
            string sql = "SELECT COUNT(1) FROM NHOMHANG WHERE MANHOM = @manhom";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("manhom", manhom);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }
        }
        public bool Insert(NhomHang nh)
        {
            if (!Check(nh.Manhom))
            {
                string sql = "INSERT INTO NHOMHANG VALUES (@manhom, @manganh, @tennhom)";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@manhom", SqlDbType.VarChar).Value = nh.Manhom;
                    cmd.Parameters.Add("@manganh", SqlDbType.VarChar).Value = nh.Manganh;
                    cmd.Parameters.Add("tennhom", SqlDbType.NVarChar).Value = nh.Tennhom;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
        public bool Update(NhomHang nh)
        {
            if (Check(nh.Manhom))
            {
                string sql = "UPDATE CHUCVU SET TENNHOM @tennhom, MANGANH = @manganh WHERE MANHOM = @manhom";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@tennhom", SqlDbType.NVarChar).Value = nh.Tennhom;
                    cmd.Parameters.Add("@manganh", SqlDbType.VarChar).Value = nh.Manganh;
                    cmd.Parameters.Add("@manhom", SqlDbType.VarChar).Value = nh.Manhom;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
        public bool Delete(NhomHang nh)
        {
            if (Check(nh.Manhom))
            {
                string sql = "DELETE FROM NHOMHANG WHERE MANHOM = @manhom";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@manhom", SqlDbType.VarChar).Value = nh.Manhom;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
