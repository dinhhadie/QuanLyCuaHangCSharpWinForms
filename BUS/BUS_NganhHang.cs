using DAL;
using DTO;
using System;
using System.Data;
namespace BUS
{
    public class BUS_NganhHang
    {
        private readonly DAL_NganhHang dal = new DAL_NganhHang();
        public DataTable Load(string sql)
        {
            return dal.LoadDL(sql);
        }
        public bool Insert(NganhHang nh)
        {
            try
            {
                return dal.Insert(nh);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm dữ liệu vào cơ sở dữ liệu: {ex.Message}");
            }
        }
        public void Update(NganhHang nh)
        {
            dal.Update(nh);
        }
        public void Delete(NganhHang nh) 
        { 
            dal.Delete(nh); 
        }
    }
}
