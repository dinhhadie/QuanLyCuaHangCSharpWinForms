using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_ThanhToan
    {
        DAL_ThanhToan dal = new DAL_ThanhToan();
        public bool Insert(ThanhToan tt, HoaDon hd)
        {
            return dal.Insert(tt, hd);
        }
    }
}
