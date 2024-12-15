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
    public class DAL_NhanVien : Connect
    {
        public DataTable LoadDL(string sql)
        {
            return Load(sql);
        }
        public bool Check(string manv)
        {
            string sql = "SELECT COUNT(1) FROM NHANVIEN WHERE MANV = @manv";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@manv", SqlDbType.VarChar).Value = manv;
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return count > 0;
            }
        }
        public bool Insert(NhanVien nv, TaiKhoan tk)
        {
            string sql = @"
                        BEGIN TRANSACTION;
                        INSERT INTO NHANVIEN (MaNV, MaCV, TenNV, GioiTinh, Sodienthoai, DiaChi)
                        VALUES (@manv, @macv, @tennv, @gioitinh, @sdt, @diachi);
                        INSERT INTO QUYENDANGNHAP (Tendangnhap, MaNV, MatKhau, TrangThai)
                        VALUES (@tendn, @manv, @matkhau, @trangthai);
                        COMMIT TRANSACTION;";
            if (!Check(nv.MaNV))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@manv", SqlDbType.VarChar).Value = nv.MaNV;
                    cmd.Parameters.Add("@macv", SqlDbType.VarChar).Value = nv.MaCV;
                    cmd.Parameters.Add("@tennv", SqlDbType.NVarChar).Value = nv.TenNV;
                    cmd.Parameters.Add("@gioitinh", SqlDbType.NVarChar).Value = nv.GioiTinh;
                    cmd.Parameters.Add("@sdt", SqlDbType.VarChar).Value = nv.Sdt;
                    cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = nv.DiaChi;
                    cmd.Parameters.Add("@tendn", SqlDbType.VarChar).Value = tk.Tendn;
                    cmd.Parameters.Add("@matkhau", SqlDbType.VarChar).Value = tk.Matkhau;
                    cmd.Parameters.Add("@trangthai", SqlDbType.Bit).Value = tk.Trangthai;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }
            return false;
        }
        public string CheckMatKhau(string tendangnhap)
        {
            string sql = "SELECT MATKHAU FROM QUYENDANGNHAP WHERE TENDANGNHAP = @tendangnhap";
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@tendangnhap", SqlDbType.VarChar).Value = tendangnhap;

                con.Open();
                object result = cmd.ExecuteScalar();
                con.Close();

                return result?.ToString(); 
            }
        }
        public void DoiMatKhau(TaiKhoan tk)
        {
            string sql = "UPDATE QUYENDANGNHAP SET MATKHAU = @matkhau WHERE TENDANGNHAP = @tendangnhap";
            using (SqlCommand cmd = new SqlCommand(sql))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@matkhau", SqlDbType.VarChar).Value = tk.Matkhau;
                cmd.Parameters.Add("@tendangnhap", SqlDbType.VarChar).Value = tk.Tendn;
                ExecuteCMD(cmd);
            }
        }
        public bool Delete(NhanVien nv, TaiKhoan tk)
        {
            if (Check(nv.MaNV))
            {
                string sql = @"
                    BEGIN TRANSACTION;
                    DELETE FROM QUYENDANGNHAP WHERE MANV = @manv; 
                    DELETE FROM NHANVIEN WHERE MANV = @manv;      
                    COMMIT TRANSACTION;";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@manv", SqlDbType.VarChar).Value = nv.MaNV;
                    ExecuteCMD(cmd);
                }
                return true;
            }
            return false;
        }
    }
}
