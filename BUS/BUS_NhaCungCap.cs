using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data;
namespace BUS
{
    public class BUS_NhaCungCap
    {
        private readonly DAL_NhaCungCap dal = new DAL_NhaCungCap();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }
        public bool Insert(NhaCungCap ncc)
        {
            return dal.Insert(ncc);
        }
        public void Update(NhaCungCap ncc)
        {
            dal.Update(ncc);
        }
        public bool Delete(NhaCungCap ncc)
        {
            return dal.Delete(ncc);
        }
    }
}
