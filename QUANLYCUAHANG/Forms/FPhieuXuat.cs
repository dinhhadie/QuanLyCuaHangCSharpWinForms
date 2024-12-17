using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYCUAHANG.Forms
{
    public partial class FPhieuXuat : Form
    {
        public FPhieuXuat()
        {
            InitializeComponent();
        }
        BUS_PhieuXuat bus = new BUS_PhieuXuat();
        private void FPhieuXuat_Load(object sender, EventArgs e)
        {
            dgvPhieuXuat.DataSource = bus.Load("SELECT * FROM PHIEUNHAP");
            cboMaKH.DataSource = bus.Load("SELECT MAKH FROM KHACHHANG");
            cboMaKH.DisplayMember = "MAKH";
            this.reportViewer1.RefreshReport();
        }
    }
}
