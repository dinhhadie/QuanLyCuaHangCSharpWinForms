using BUS;
using DTO;
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
        BUS_ChiTietPhieuXuat bus2 = new BUS_ChiTietPhieuXuat();
        private void FPhieuXuat_Load(object sender, EventArgs e)
        {
            LoadDuLieu();
        }
        private void LoadDuLieu()
        {
            dgvPhieuXuat.DataSource = bus.Load("SELECT * FROM PHIEUXUAT");
            dgvChiTietPhieuXuat.DataSource = bus.Load("SELECT * FROM CHITIETPHIEUXUAT");
            cboMaKH.DataSource = bus.Load("SELECT MAKH FROM KHACHHANG");
            cboMaKH.DisplayMember = "MAKH";
            cboMaPX.DataSource = bus.Load("SELECT MAPX FROM PHIEUXUAT");
            cboMaPX.DisplayMember = "MAPX";
            cboMaPX2.DataSource = bus.Load("SELECT MAPX FROM PHIEUXUAT");
            cboMaPX2.DisplayMember = "MAPX";
            cboMaHH.DataSource = bus.Load("SELECT * FROM HANGHOA");
            cboMaHH.DisplayMember = "TENHH";
            cboMaHH.ValueMember = "MAHH";
        }

        private void btnThemCTPX_Click(object sender, EventArgs e)
        {
            
            int soluong;
            if (!int.TryParse(txtSoLuong.Text, out soluong))
            {
                MessageBox.Show("Số  lượng không hợp lệ!");
                txtSoLuong.Focus();
                return;
            }
            ChiTietPhieuXuat ctpx = new ChiTietPhieuXuat(cboMaPX.Text, cboMaHH.SelectedValue.ToString(), soluong);
            if (bus2.Insert(ctpx))
            {
                MessageBox.Show("Thêm chi tiết phiếu xuất thành công!");
                LoadDuLieu();
            }
            else
            {
                MessageBox.Show("Nhập trùng mã phiếu xuất và mã hàng hóa!");
                return;
            }

        }

        private void txtMaNV_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = Global.manv;
        }

        private void btnThemPX_Click(object sender, EventArgs e)
        {
            PhieuXuat px = new PhieuXuat(txtMaPX.Text, txtMaNV.Text, Convert.ToDateTime(dpNgayXuat.Text), cboMaKH.Text, txtTrangThai.Text ?? null);
            if (bus.Insert(px))
            {
                MessageBox.Show("Thêm phiếu xuất thành công!");
                LoadDuLieu();
            }
            else
            {
                MessageBox.Show("Nhập trùng mã phiếu xuất!");
                txtMaPX.Focus();
                return;

            }    
        }
    }
}
