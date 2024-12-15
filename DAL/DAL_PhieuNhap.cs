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
    public class DAL_PhieuNhap : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string mapn)
        {
            string sql = "SELECT COUNT(1) FROM PHIEUNHAP WHERE MAPN = @mapn";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@mapn", mapn);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }
        }
        public bool Insert(PhieuNhap pn)
        {
            if (!Check(pn.MaPN))
            {
                string sql = "INSERT INTO PHIEUNHAP VALUES (@mapn, @mancc, @manv, @ngaynhap, @machungtu, @loaihoadon, @tongphaitra)";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@mapn", SqlDbType.VarChar).Value = pn.MaPN;
                    cmd.Parameters.Add("@mancc", SqlDbType.VarChar).Value = pn.MaNCC;
                    cmd.Parameters.Add("@manv", SqlDbType.VarChar).Value = pn.MaNV;
                    cmd.Parameters.Add("ngaynhap", SqlDbType.Date).Value = pn.NgayNhap;
                    cmd.Parameters.Add("@machungtu", SqlDbType.VarChar).Value = pn.MaChungTu;
                    cmd.Parameters.Add("@loaihoadon", SqlDbType.NVarChar).Value = pn.LoaiHoaDon;
                    cmd.Parameters.Add("@tongphaitra", SqlDbType.Decimal).Value = pn.TongPhaiTra ?? (object)DBNull.Value;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
