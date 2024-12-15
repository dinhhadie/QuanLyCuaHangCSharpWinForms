using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietPhieuNhap
    {
        public string MaPN { get; set; }
        public string MaHH { get; set; }

        public int SLuong { get; set; }
        public decimal DGia { get; set; }
        public decimal? ChietKhau  { get; set; }

        public ChiTietPhieuNhap() { }
        public ChiTietPhieuNhap(string MaPN, string MaHH, int SLuong, decimal DGia, decimal? ChietKhau)
        {
            this.MaPN = MaPN;
            this.MaHH = MaHH;
            this.SLuong = SLuong;
            this.DGia = DGia;
            this.ChietKhau = ChietKhau;
        }
    }
}
