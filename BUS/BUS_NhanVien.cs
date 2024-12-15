using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BUS
{
    public class BUS_NhanVien
    {
        private readonly DAL_NhanVien dal = new DAL_NhanVien();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }
        public bool Insert(NhanVien nv, TaiKhoan tk)
        {
            return dal.Insert(nv, tk);
        }
        public bool DoiMatKhau(TaiKhoan tk)
        {
            string matkhaucu = dal.CheckMatKhau(tk.Tendn);

            if (matkhaucu == null)
            {
                throw new Exception("Tài khoản không tồn tại!");
            }

            if (matkhaucu == tk.Matkhau)
            {
                return false;
            }

            dal.DoiMatKhau(tk);
            return true;
        }
 
        public bool Delete(NhanVien nv, TaiKhoan tk)
        {
            return dal.Delete(nv, tk);
        }
    }
}
