using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TaiKhoan
    {
        private string tendn, manv, matkhau;
        private byte trangthai;

        public string Tendn { get => tendn; set => tendn = value; }
        public string Manv { get => manv; set => manv = value; }
        public string Matkhau { get => matkhau; set => matkhau = value; }
        public byte Trangthai { get => trangthai; set => trangthai = value; }

        public TaiKhoan() { }
        public TaiKhoan(string manv)
        {
            this.manv = manv;
        }
        public TaiKhoan(string tendn, string matkhau)
        {
            this.tendn = tendn;
            this.matkhau = matkhau;
        }
        public TaiKhoan(string tendn, string manv, string matkhau, byte trangthai)
        {
            this.tendn = tendn;
            this.manv = manv;
            this.matkhau = matkhau;
            this.trangthai = trangthai;
        }
    }
}
