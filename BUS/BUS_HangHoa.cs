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
    public class BUS_HangHoa
    {
        DAL_HangHoa dal = new DAL_HangHoa();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }
        public bool Insert(HangHoa hh)
        {
            return dal.Insert(hh);
        }
    }
}
