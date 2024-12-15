using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_KhachHang
    {
        DAL_KhachHang dal = new DAL_KhachHang();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }
        public bool Insert(KhachHang kh)
        {
            return dal.Insert(kh);
        }
        public bool Update(KhachHang kh)
        {
            return dal.Update(kh);
        }
    }
}
