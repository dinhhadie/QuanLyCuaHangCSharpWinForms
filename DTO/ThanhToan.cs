using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThanhToan
    {
        public string MaTT { get; set; }
        public string MaHD { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public decimal SoTien { get; set; }
        public string PhuongThuc {  get; set; }
        public ThanhToan() { }
        public ThanhToan(string MaTT, string MaHD, DateTime NgayThanhToan, decimal SoTien, string PhuongThuc)
        {
            this.MaTT = MaTT;
            this.MaHD = MaHD;
            this.NgayThanhToan = NgayThanhToan;
            this.SoTien = SoTien;
            this.PhuongThuc = PhuongThuc;
        }
    }
}
