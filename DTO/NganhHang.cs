using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NganhHang
    {
        private string manganh;
        private string tennganh;

        public string Manganh { get => manganh; set => manganh = value; }
        public string Tennganh { get => tennganh; set => tennganh = value; }

        public NganhHang() { }
        public NganhHang(string manganh, string tennganh)
        {
            this.manganh = manganh;
            this.tennganh = tennganh;
        }
        public NganhHang(string manganh)
        {
            this.manganh = manganh;
        }
    }
}
