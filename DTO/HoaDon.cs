using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HoaDon
    {
        public string MaHD {  get; set; }
        public string MaNV { get; set; }
        public string MaKH { get; set; }
        public DateTime NgayBan { get; set; }
        public decimal? TongTienHD { get; set; }
        public string? TrangThai {  get; set; }
        public HoaDon() { }
        public HoaDon(string MaHD)
        {
            this.MaHD = MaHD;
        }
        public HoaDon(string MaHD, string TrangThai)
        {
            this.MaHD = MaHD;
            this.TrangThai = TrangThai;
        }
        public HoaDon(string MaHD, string MaNV, string MaKH, DateTime NgayBan, decimal? TongTienHD, string? TrangThai)
        {
            this.MaHD = MaHD;
            this.MaNV = MaNV;
            this.MaKH = MaKH;
            this.NgayBan = NgayBan;
            this.TongTienHD = TongTienHD;
            this.TrangThai = TrangThai;
        }
       
    }
}
