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
    public class BUS_ChiTietPhieuNhap
    {
        DAL_ChiTietPhieuNhap dal = new DAL_ChiTietPhieuNhap();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }
        public bool Insert(ChiTietPhieuNhap ctpn, PhieuNhap pn)
        {
            return dal.Insert(ctpn, pn);
        }
        public bool Update(ChiTietPhieuNhap ctpn, PhieuNhap pn)
        {
            return dal.Update(ctpn, pn);
        }
    }
}
