using System;

namespace DTO
{
#nullable enable
    public class PhieuXuat
    {
        public string MaPX { get; set; }
        public string MaNV { get; set; }
        public DateTime NgayXuat { get; set; }
        public string MaKH { get; set; }
        public string? TrangThai { get; set; }

        public PhieuXuat() { }

        public PhieuXuat(string MaPX, string MaNV, DateTime NgayXuat, string MaKH, string? TrangThai)
        {
            this.MaPX = MaPX;
            this.MaNV = MaNV;
            this.NgayXuat = NgayXuat;
            this.MaKH = MaKH;
            this.TrangThai = TrangThai;
        }
    }
}
