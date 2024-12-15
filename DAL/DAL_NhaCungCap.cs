using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class DAL_NhaCungCap : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string mancc)
        {
            string sql = "SELECT COUNT(1) FROM NHACUNGCAP WHERE MANCC = @mancc";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@mancc", SqlDbType.VarChar).Value = mancc;
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }
        }
        public bool Insert(NhaCungCap ncc)
        {
            if (!Check(ncc.maNCC))
            {
                string sql = "INSERT INTO NHACUNGCAP VALUES (@mancc, @tenncc, @diachi, @dt, @fax, @email)";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@mancc", SqlDbType.VarChar).Value = ncc.maNCC;
                    cmd.Parameters.Add("@tenncc", SqlDbType.NVarChar).Value = ncc.tenNCC;
                    cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = ncc.diaChi ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@dt", SqlDbType.VarChar).Value = ncc.dienThoai ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@fax", SqlDbType.VarChar).Value = ncc.fax ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = ncc.email ?? (object)DBNull.Value;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
        public void Update(NhaCungCap ncc)
        {
            string sql = "UPDATE NHACUNGCAP SET TENNCC = @tenncc, DIACHI = @diachi, @DIENTHOAI = @dt, FAX = @fax, EMAIL = @EMAIL WHERE MANCC = @mancc";
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@tenncc", SqlDbType.NVarChar).Value = ncc.tenNCC;
                cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = ncc.diaChi;
                cmd.Parameters.Add("@dt", SqlDbType.VarChar).Value = ncc.dienThoai ?? (object)DBNull.Value;
                cmd.Parameters.Add("@fax", SqlDbType.VarChar).Value = ncc.fax ?? (object)DBNull.Value;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = ncc.email ?? (object)DBNull.Value;
                cmd.Parameters.Add("@mancc", SqlDbType.VarChar).Value = ncc.maNCC ?? (object)DBNull.Value;
                ExecuteCMD(cmd);
            }
        }
        public bool Delete(NhaCungCap ncc)
        {
            if (Check(ncc.maNCC))
            {
                string sql = "DELETE FROM NHACUNGCAP WHERE MANCC = @mancc";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@mancc", SqlDbType.VarChar).Value = ncc.maNCC;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
