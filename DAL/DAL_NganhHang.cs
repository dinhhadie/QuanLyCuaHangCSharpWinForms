using DTO;
using System;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class DAL_NganhHang : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string manganh)
        {
            string sql = "SELECT COUNT(1) FROM NGANHHANG WHERE MANGANH = @manganh";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@manganh", manganh);
                con.Open();
                //int count = (int)cmd.ExecuteScalar();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }
        }
        public bool Insert(NganhHang nh)
        {
            //dùng Add => Khai báo tường minh kiểu dữ liệu bằng SqlDbType, độ rộng (nếu muốn)
            //Dùng AddWithValue, tùy giá trị mình nhập vào => chuyển đổi ngầm định, ví dụ nhập Đính, sẽ tự hiểu là nvarchar
            /*string sql = "INSERT INTO NGANHHANG VALUES (@manganh, @tennganh)";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                try
                {
                    cmd.Parameters.Add("@manganh", SqlDbType.VarChar).Value = nh.Manganh; //1
                                                                                          //2 cách viết
                    cmd.Parameters.Add(new SqlParameter("@tennganh", SqlDbType.NVarChar) { Value = nh.Tennganh }); //2
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    throw new Exception($"Lỗi khi thêm dữ liệu: {e.Message}", e);
                }
                finally
                {
                    con.Close();
                }
            }    */

            if (!Check(nh.Manganh))
            {
                string sql = "INSERT INTO NGANHHANG VALUES (@manganh, @tennganh)";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@manganh", SqlDbType.VarChar).Value = nh.Manganh;
                        cmd.Parameters.Add("@tennganh", SqlDbType.NVarChar).Value = nh.Tennganh;
                        ExecuteCMD(cmd);
                    }
                }
                return true;
            }
            return false;  
        }
        public void Update(NganhHang nh)
        {
            /*string sql = "UPDATE NGANHHANG SET TENNGANH = @tennganh WHERE MANGANH = @manganh";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add(new SqlParameter("@tennganh", SqlDbType.NVarChar) { Value = nh.Tennganh });
            cmd.Parameters.Add(new SqlParameter("@manganh", SqlDbType.VarChar) { Value = nh.Manganh});
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();*/


            /*using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE NGANHHANG SET TENNGANH = @tennganh WHERE MANGANH = @manganh";
                cmd.Parameters.AddWithValue("@tennganh", nh.Tennganh);
                cmd.Parameters.AddWithValue("@manganh", nh.Manganh);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }*/

            //Dùng phương thức Add tốt hơn vì có thể chỉ định kiểu dữ liệu chính xác
            //AddWithValue có thể khiến chuyển đổi kiểu sai ý muốn, ví dụ int nó có thể chuyển thành varchar??

            using (SqlCommand cmd = new SqlCommand("UPDATE NGANHHANG SET TENNGANH = @tennganh WHERE MANGANH = @manganh", con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@tennganh", SqlDbType.NVarChar) { Value = nh.Tennganh });
                cmd.Parameters.Add(new SqlParameter("@manganh", SqlDbType.VarChar) { Value = nh.Manganh });
               
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    throw new Exception($"Lỗi khi cập nhật dữ liệu: {e.Message}", e);
                }
                finally
                {
                    con.Close(); 
                }
            }
        }
        public void Delete(NganhHang nh)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM NGANHHANG WHERE MANGANH = @manganh", con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("manganh", SqlDbType.VarChar).Value = nh.Manganh;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch(SqlException e)
                {
                    throw new Exception($"Lỗi khi xóa dữ liệu: {e.Message}", e);
                } 
                finally
                {
                    con.Close();
                }
                
            }    
        }
    }
}
