using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL; using DTO;
namespace BUS
{
    public class BUS_NhomHang
    {
        private readonly DAL_NhomHang dal = new DAL_NhomHang();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }
        public bool Insert(NhomHang nh)
        {
            return dal.Insert(nh);
        }
        public bool Update(NhomHang nh)
        {
            return dal.Update(nh);
        }
        public bool Delete(NhomHang nh)
        {
            return dal.Delete(nh);
        }
    }
}
