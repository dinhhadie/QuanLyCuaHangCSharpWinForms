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
    public class BUS_ChiTietHoaDon
    {
        DAL_ChiTietHoaDon dal = new DAL_ChiTietHoaDon();
        public DataTable Load(string sql)
        {
            return dal.Load(sql);
        }
        public bool Insert(ChiTietHoaDon cthd, HoaDon hd)
        {
            return dal.Insert(cthd, hd);
        }
    }
}
