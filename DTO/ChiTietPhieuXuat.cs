using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietPhieuXuat
    {
        public string MaPX { get; set; }
        public string MaHH { get; set; }
        public int SLuong { get; set; }
        public ChiTietPhieuXuat() { }
        public ChiTietPhieuXuat(string MaPX, string MaHH, int SLuong)
        {
            this.MaPX = MaPX;
            this.MaHH = MaHH;
            this.SLuong = SLuong;
        }
    }
}
