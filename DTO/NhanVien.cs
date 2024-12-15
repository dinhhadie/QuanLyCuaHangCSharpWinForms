using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhanVien
    {
        private string maNV, maCV, tenNV, gioiTinh, sdt, diaChi;

        public string MaNV { get => maNV; set => maNV = value; }
        public string MaCV { get => maCV; set => maCV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }

        public NhanVien() { }
        public NhanVien(string maNV)
        {
            this.maNV = maNV;
        }
        public NhanVien(string maNV, string maCV, string tenNV, string gioiTinh, string sdt, string diaChi)
        {
            this.maNV= maNV;
            this.maCV = maCV;
            this.tenNV = tenNV;
            this.gioiTinh = gioiTinh;
            this.sdt = sdt;
            this.diaChi = diaChi;
        }
    }
}
