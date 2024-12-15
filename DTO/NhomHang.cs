using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhomHang
    {
        private string manhom, manganh, tennhom;

        public string Manhom { get => manhom; set => manhom = value; }
        public string Manganh { get => manganh; set => manganh = value; }
        public string Tennhom { get => tennhom; set => tennhom = value; }

        public NhomHang() { }
        public NhomHang(string manhom)
        {
            this.manhom = manhom;
        }
        public NhomHang(string manhom, string manganh, string tennhom)
        {
            this.manhom = manhom;
            this.manganh = manganh;
            this.tennhom = tennhom;
        }
    }
}
