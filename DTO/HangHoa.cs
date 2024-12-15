using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HangHoa
    {
        public string MaHH { get; set; }
        public string MaNH { get; set; }
        public string TenHH {  get; set; }
        public string DonViTinh { get; set; }
        public float TrongLuong { get; set; }
        public DateTime HanSD { get; set; }
        public string NoiSX { get; set; }
       public HangHoa() { }
       public HangHoa(string MaHH)
        {
            this.MaHH = MaHH;
        }
        public HangHoa(string MaHH, string MaNH, string TenHH, string DonViTinh, float TrongLuong, DateTime HanSD, string NoiSX)
        {
            this.MaHH = MaHH;
            this.MaNH = MaNH;
            this.TenHH = TenHH;
            this.DonViTinh = DonViTinh;
            this.TrongLuong = TrongLuong;
            this.HanSD = HanSD;
            this.NoiSX = NoiSX;
        }
    }
}
