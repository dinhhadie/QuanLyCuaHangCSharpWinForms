using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Connect
    {
        public SqlConnection con = new SqlConnection("Data Source=LAPTOP-3ICHCQG9\\MSSQLSERVER2022;Initial Catalog=QUANLYCUAHANG;Integrated Security=True");
        public DataTable Load(string sql)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(sql, con);
                ad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi: {ex.Message}");
            }
        }
        public void ExecuteCMD(SqlCommand cmd)
        {
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thực thi lệnh SQL: {ex.Message}");
            }
            finally
            {
                con.Close();
            }

    }
    public void ExecuteAD(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            ad.Fill(dt);
            ad.Update(dt);
        }
    }
}
