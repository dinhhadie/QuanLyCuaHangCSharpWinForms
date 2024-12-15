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
    public class DAL_ChiTietPhieuNhap : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string mapn, string mahh)
        {
            string sql = "SELECT COUNT(1) FROM CHITIETPHIEUNHAP WHERE MAPN = @mapn AND MAHH = @mahh";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@mapn", SqlDbType.VarChar).Value = mapn;
                cmd.Parameters.Add("mahh", SqlDbType.VarChar).Value = mahh;
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }    
        }
        public bool Insert(ChiTietPhieuNhap ctpn, PhieuNhap pn)
        {
            if (!Check(ctpn.MaPN, ctpn.MaHH))
            {
                string sql = @"BEGIN TRANSACTION;
                INSERT INTO CHITIETPHIEUNHAP VALUES (@mapn, @mahh, @sluong, @dgia, @chietkhau);
                UPDATE PHIEUNHAP SET TONGPHAITRA = 
                (SELECT SUM(SLUONG * DGIA + SOLUONG * DONGIA * CHIETKHAU / 100) FROM CHITIETPHIEUNHAP
                WHERE PHIEUNHAP.MAPN = CHITIETPHIEUNHAP.MAPN)
                AND MAPN = @mapn;
                COMMMIT TRANSACTION;";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@mapn", SqlDbType.VarChar).Value = ctpn.MaPN;
                    cmd.Parameters.Add("@mahh", SqlDbType.VarChar).Value = ctpn.MaHH;
                    cmd.Parameters.Add("@sluong", SqlDbType.Int).Value = ctpn.SLuong;
                    cmd.Parameters.Add("@dgia", SqlDbType.Decimal).Value = ctpn.DGia;
                    cmd.Parameters.Add("@chietkhau", SqlDbType.Decimal).Value = ctpn.ChietKhau ?? (object)DBNull.Value;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
        public bool Update(ChiTietPhieuNhap ctpn, PhieuNhap pn)
        {
            if (Check(ctpn.MaPN, ctpn.MaHH))
            {
                string sql = @"BEGIN TRANSACTION;
                UPDATE CHITIETPHIEUNHAP SET SLUONG = @sluong, DGIA = @dgia, CHIETKHAU = @chietkhau 
                WHERE MAPN = @mapn AND MAHH = @mahh;
                UPDATE CHITIETPHIEUNHAP SET TONGPHAITRA = 
                COMMIT TRANSACTION;
                ";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@sluong", SqlDbType.Int).Value = ctpn.SLuong;
                    cmd.Parameters.Add("@dgia", SqlDbType.Decimal).Value = ctpn.DGia;
                    cmd.Parameters.Add("@chietkhau", SqlDbType.Decimal).Value = ctpn.ChietKhau ?? (object)DBNull.Value;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
