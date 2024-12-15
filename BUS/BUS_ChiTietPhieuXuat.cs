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
    public class BUS_ChiTietPhieuXuat
    {
        DAL_ChiTietPhieuXuat dal = new DAL_ChiTietPhieuXuat();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }    
        public bool Insert(ChiTietPhieuXuat ctpx)
        {
            return dal.Insert(ctpx); 
        }
    }
}
