using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhieuNhap
    {
        public string MaPN {  get; set; }
        public string MaNCC { get; set; }
        public string MaNV { get; set; }
        public DateTime NgayNhap { get; set; }
        public string MaChungTu { get; set; }
        public string LoaiHoaDon { get; set; }
        public decimal? TongPhaiTra {  get; set; }

        
        public PhieuNhap() { }
        public PhieuNhap(string MaPN, string MaNCC, string MaNV, DateTime NgayNhap, string MaChungTu, string LoaiHoaDon, decimal? TongPhaiTra)
        {
            this.MaPN = MaPN;
            this.MaNCC = MaNCC;
            this.MaNV = MaNV;
            this.NgayNhap = NgayNhap;
            this.MaChungTu = MaChungTu;
            this.TongPhaiTra = TongPhaiTra;
        }
        public PhieuNhap(string MaPN)
        {
            this.MaPN = MaPN;
        }
    }
}
