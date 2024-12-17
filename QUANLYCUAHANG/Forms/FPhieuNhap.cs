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
using Microsoft.Reporting.WinForms;

namespace QUANLYCUAHANG.Forms
{
    public partial class FPhieuNhap : Form
    {
        public FPhieuNhap()
        {
            InitializeComponent();
        }
        BUS_PhieuNhap bus = new BUS_PhieuNhap();
        private void FPhieuNhap_Load(object sender, EventArgs e)
        {
            cboMaNCC.DataSource = bus.Load("SELECT MANCC, TENNCC FROM NHACUNGCAP");
            cboMaNCC.DisplayMember = "TENNCC";
            cboMaNCC.ValueMember = "MANCC";
            cboMaPN.DataSource = bus.Load("SELECT * FROM PHIEUNHAP");
            cboMaPN.DisplayMember = "MAPN";
            Global.manv = txtMaNV.Text;
            dgvPhieuNhap.DataSource = bus.Load("SELECT * FROM PHIEUNHAP");
            dgvChiTietPhieuNhap.DataSource = bus.Load("SELECT * FROM CHITIETPHIEUNHAP");
            this.reportViewer1.RefreshReport();
        }

        private void btnThemPhieuNhap_Click(object sender, EventArgs e)
        {
            BUS_PhieuNhap bus = new BUS_PhieuNhap();
            if (string.IsNullOrEmpty(txtMaPN1.Text) ||
                string.IsNullOrEmpty(cboMaNCC.Text) ||
                string.IsNullOrEmpty(txtMaNV.Text) ||
                string.IsNullOrEmpty(NgayNhap.Text) ||
                string.IsNullOrEmpty(txtMaChungtu.Text) ||
                string.IsNullOrEmpty(txtLoaiHoaDon.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }
            DateTime ngaynhap = Convert.ToDateTime(NgayNhap.Text);
            PhieuNhap pn = new PhieuNhap(txtMaPN1.Text, cboMaNCC.SelectedValue.ToString(), txtMaNV.Text, ngaynhap, txtMaChungtu.Text, txtLoaiHoaDon.Text,
            string.IsNullOrEmpty(txtTongphaitra.Text) ? (decimal?)null : Convert.ToDecimal(txtTongphaitra.Text));
            if (bus.Insert(pn))
            {
                MessageBox.Show("Thêm phiếu nhập thành công!");
                dgvPhieuNhap.DataSource = bus.Load("SELECT * FROM PHIEUNHAP");
            }    
            else
            {
                MessageBox.Show("Nhập trùng mã phiếu nhập!");
                txtMaPN1.Focus();
                return;
            }    
        }

        private void ChondongdgvPhieuNhap(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMaPN1.Text = dgvPhieuNhap.Rows[i].Cells[0].Value.ToString();
            cboMaNCC.Text = dgvPhieuNhap.Rows[i].Cells[1].Value.ToString();
            txtMaNV.Text = dgvPhieuNhap.Rows[i].Cells[2].Value.ToString();
            NgayNhap.Text = dgvPhieuNhap.Rows[i].Cells[3].Value.ToString();
            txtMaChungtu.Text = dgvPhieuNhap.Rows[i].Cells[4].Value.ToString();
            txtLoaiHoaDon.Text = dgvPhieuNhap.Rows[i].Cells[5].Value.ToString();
            txtTongphaitra.Text = dgvPhieuNhap.Rows[i].Cells[6].Value.ToString();
        }

        private void ChondongDgvCTPN(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMaPN2.Text = dgvChiTietPhieuNhap.Rows[i].Cells[0].Value.ToString();
            cboMaHH.Text = dgvChiTietPhieuNhap.Rows[i].Cells[1].Value.ToString();
            txtSoLuong.Text = dgvChiTietPhieuNhap.Rows[i].Cells[2].Value.ToString();
            txtDonGia.Text = dgvChiTietPhieuNhap.Rows[i].Cells[3].Value.ToString();
            txtChietKhau.Text = dgvChiTietPhieuNhap.Rows[i].Cells[4].Value.ToString() ?? null;
        }

        private void BtnInPhieuNhap_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT TENNCC, NHACUNGCAP.DIACHI, DIENTHOAI, PHIEUNHAP.MANV, TENNV, PHIEUNHAP.MAPN, NGAYNHAP, CHITIETPHIEUNHAP.MAHH,  TENHH, SLUONG, DGIA, SLUONG * DGIA AS THANHTIEN, CHIETKHAU, TONGPHAITRA
            FROM NHACUNGCAP, PHIEUNHAP, CHITIETPHIEUNHAP, HANGHOA, NHANVIEN
            WHERE NHACUNGCAP.MANCC = PHIEUNHAP.MANCC
            AND PHIEUNHAP.MAPN = CHITIETPHIEUNHAP.MAPN
            AND CHITIETPHIEUNHAP.MAHH = HANGHOA.MAHH
            AND PHIEUNHAP.MANV = NHANVIEN.MANV
            AND PHIEUNHAP.MAPN = '" + cboMaPN.Text + "'";
            DataTable dt = bus.Load(sql);
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;

            reportViewer1.LocalReport.ReportPath = "D:\\Windows Form\\QLCH\\QUANLYCUAHANG\\Forms\\PhieuNhap.rdlc";
            if (dt.Rows.Count > 0)
            {
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "PHIEUNHAP";
                rds.Value = dt;
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
            else MessageBox.Show("Không có dữ liệu");
        }
    }
}
