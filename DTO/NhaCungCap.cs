using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
#nullable enable
    public class NhaCungCap
    {
        private string MaNCC, TenNCC;
        private string? DiaChi, DienThoai, Fax, Email;

        public string maNCC { get => MaNCC; set => MaNCC = value; }
        public string tenNCC { get => TenNCC; set => TenNCC = value; }
        public string? diaChi { get => DiaChi; set => DiaChi = value; }
        public string? dienThoai { get => DienThoai; set => DienThoai = value; }
        public string? fax { get => Fax; set => Fax = value; }
        public string? email { get => Email; set => Email = value; }

        public NhaCungCap() { }
        public NhaCungCap(string maNCC, string tenNCC, string? diaChi, string? dienThoai, string? fax, string? email)
        {
            this.maNCC = maNCC;
            this.tenNCC = tenNCC;
            this.diaChi = diaChi;
            this.dienThoai = dienThoai;
            this.fax = fax;
            this.email = email;
        }
        public NhaCungCap(string maNCC)
        {
            this.maNCC = maNCC;
        }
    }
}
