using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietHoaDon
    {
        public string MaHD {  get; set; }
        public string MaHH { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal Tile_VAT {  get; set; }
        public decimal ThanhTien { get; set; }
        public ChiTietHoaDon() { }
        public ChiTietHoaDon(string MaHD, string MaHH, int SoLuong, decimal DonGia, decimal Tile_VAT, decimal ThanhTien)
        {
            this.MaHD = MaHD;
            this.MaHH = MaHH;
            this.SoLuong = SoLuong;
            this.DonGia = DonGia;
            this.Tile_VAT = Tile_VAT;
            this.ThanhTien = ThanhTien;
        }
    }
}
