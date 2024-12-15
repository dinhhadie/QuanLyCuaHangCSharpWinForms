using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BUS
{
    public class BUS_PhieuNhap
    {
        DAL_PhieuNhap dal = new DAL_PhieuNhap();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }
        public bool Insert(PhieuNhap pn)
        {
            return dal.Insert(pn);
        }
    }
}
