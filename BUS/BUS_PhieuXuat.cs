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
    public class BUS_PhieuXuat
    {
        DAL_PhieuXuat dal = new DAL_PhieuXuat();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }
        public bool Insert(PhieuXuat px)
        {
            return dal.Insert(px);
        }
    }
}
