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
    public class BUS_ChucVu
    {
        private readonly DAL_ChucVu dal = new DAL_ChucVu();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }
        public bool Insert(ChucVu cv)
        {
            return dal.Insert(cv);
        }
        public void Update(ChucVu cv)
        {
            dal.Update(cv);
        }
        public void Delete(ChucVu cv)
        {
            dal.Delete(cv);
        }
    }
}
