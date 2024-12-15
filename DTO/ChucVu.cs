using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChucVu
    {
        private string MaCV, TenCV;

        public string maCV { get => MaCV; set => MaCV = value; }
        public string tenCV { get => TenCV; set => TenCV = value; }

        public ChucVu() { } 
        public ChucVu(string MaCV, string TenCV)
        {
            this.MaCV = MaCV;
            this.TenCV = TenCV;
        }
        public ChucVu(string MaCV)
        {
            this.MaCV = MaCV;
        }
        
    }
}
