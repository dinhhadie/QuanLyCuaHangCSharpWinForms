using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable
namespace DTO
{
    public class KhachHang
    {
        public string MaKH {  get; set; }
        public string TenKH { get; set; }
        public string? DiaChi { get; set; }
        public string? DienThoai { get; set; }
        public string? Email { get; set; }
        public KhachHang() { }
        public KhachHang(string MaKH, string TenKH, string? DiaChi, string? DienThoai, string? Email)
        {
            this.MaKH = MaKH;
            this.TenKH = TenKH;
            this.DiaChi = DiaChi;  
            this.DienThoai = DienThoai;
            this.Email = Email;
        }
    }
}
