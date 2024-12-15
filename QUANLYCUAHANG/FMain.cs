using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Sunny.UI.Win32;
using QUANLYCUAHANG.Forms;
using System.Data.SqlClient;
using QUANLYCUAHANG.Controls;
using DTO;
using BUS;
using VietQRHelper;
//using QRCoder;
using Microsoft.Reporting.WinForms;
using System.Text.RegularExpressions;
using static System.Runtime.CompilerServices.RuntimeHelpers;
//using Microsoft.Reporting.WebForms;

namespace QUANLYCUAHANG
{
    public partial class FMain : Form
    {
#pragma warning disable
        public FMain()
        {
            InitializeComponent();
        }
        private readonly BUS_ChucVu bus = new BUS_ChucVu();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Form chính Load 
        private void FMain_Load(object sender, EventArgs e)
        {
            //FDangnhap fDangnhap = new FDangnhap();
            lbtendangnhap.Text = Global.tendangnhap;
            lbchucvu.Text = Global.chucvu;
            this.WindowState = FormWindowState.Maximized;
            string sql = "SELECT MANV FROM QUYENDANGNHAP WHERE TENDANGNHAP = '" + Global.tendangnhap + "'";
            DataTable dt = bus.Load(sql);
            foreach (DataRow dr in dt.Rows)
            {
                txtMaNV.Text = dr[0].ToString();
                txtMaNV.Enabled = false;
            }
            Global.manv = txtMaNV.Text;
            string[] PhuongThuc = { "QR Chuyển khoản", "Tiền mặt" };
            foreach (var item in PhuongThuc)
            {
                cboPhuongThuc.Items.Add(item);
            }
            this.reportViewer1.RefreshReport();
            //---------------------------tab Hóa đơn
            cboMaKH.DataSource = busHD.Load("SELECT MAKH FROM KHACHHANG");
            cboMaKH.DisplayMember = "MAKH";
            dgvHoaDon.DataSource = busHD.Load("SELECT * FROM HOADONBANHANG");
            dgvCTHD.DataSource = busHD.Load("SELECT * FROM CHITIETHOADON");



            //---------------------------tab Thanh toán

            cboMaHD.DataSource = bus.Load("SELECT MAHD FROM HOADONBANHANG");
            cboMaHD.DisplayMember = "MAHD";

            //---------------------------tab Khách hàng
            dgvKhachHang.DataSource = buskh.Load("SELECT * FROM KHACHHANG");

            //---------------------------
            cboMaHH.DataSource = bus.Load("SELECT MAHH, TENHH FROM HANGHOA");
            cboMaHH.DisplayMember = "TENHH";
            cboMaHH.ValueMember = "MAHH";

            //---------------------------tab Thanh toán
            dgvThanhToan.DataSource = bus.Load("SELECT * FROM THANHTOAN");
        }

        //Đẩy các Form con lên tab page
        private void HienthiForm(Form f, TabPage tp)
        {
            f.TopLevel = false;
            tp.Controls.Clear();
            tp.Controls.Add(f);
            //f.Dock = DockStyle.Fill;
            f.Show();
        }

        //Chọn 1 node trong cây (menu bên trái)
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            TabPage tp = tabControl1.TabPages[0];
            if (e.Node != null)
            {
                if (e.Node.Text == "ĐỔI MẬT KHẨU")
                {
                    Global.chucvu = lbchucvu.Text;
                    Form f1 = new FDoiMatKhau();
                    /*f1.TopLevel = false; 
                    tp.Controls.Clear();
                    tp.Controls.Add(f1);
                    f1.Dock = DockStyle.Fill;
                    f1.Show();*/
                    HienthiForm(f1, tp); //viết hàm để tái sử dụng
                }
                if (e.Node.Text == "NGÀNH HÀNG")
                {
                    Form f2 = new FNganhHang();
                    HienthiForm(f2, tp);
                }
                if (e.Node.Text == "NHÓM HÀNG")
                {
                    Form f3 = new FNhomHang();
                    HienthiForm(f3, tp);
                }
                if (e.Node.Text == "HÀNG HÓA")
                {
                    Form f4 = new FHangHoa();
                    HienthiForm(f4, tp);
                }
                if (e.Node.Text == "NHÀ CUNG CẤP")
                {
                    Form f5 = new FNhaCungCap();
                    HienthiForm(f5, tp);
                }
                if (e.Node.Text == "NHÂN VIÊN")
                {
                    if (lbchucvu.Text == "Quản lý")
                    {
                        Form f6 = new FNhanVien();
                        f6.TopLevel = false;
                        tp.Controls.Clear();
                        tp.Controls.Add(f6);
                        f6.Dock = DockStyle.None;
                        f6.Show();
                    }
                    else
                    {
                        MessageBox.Show("Chức năng chỉ dành cho Quản lý");
                        return;
                    }

                }
                if (e.Node.Text == "PHIẾU NHẬP")
                {
                    Form f7 = new FPhieuNhap();
                    HienthiForm(f7, tp);
                }
                if (e.Node.Text == "KHÁCH HÀNG")
                {
                    tabControl1.SelectedTab = tabKhachhang;
                }
            }
        }

        //Sự kiện chọn 1 tab trong tabcontrol
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

            int i = e.TabPageIndex;
            TabPage tp = tabControl1.TabPages[i];
            if (tp.Text == "")
            {
                tp.SetDisabled();
            }
            if (i == 0)
            {
                if (tp.Controls.Count > 0)
                    tp.Controls.Clear();
            }
            if (i == 2)
            {
                txtMaHD1.Enabled = false;
                txtMaNV.Enabled = false;
                cboMaKH.Enabled = false;
                dpNgayban.Enabled = false;
                txtTongtien.Enabled = false;
                txtTrangthai.Enabled = false;

                MaHD2.Enabled = false;
                cboMaHH.Enabled = false;
                txtsoluong.Enabled = false;
                txtdongia.Enabled = false;
                txtVAT.Enabled = false;
                txtthanhtien.Enabled = false;
            }
            if (i == 4)
            {
                txtMaCV.Enabled = false;
                txtTenCV.Enabled = false;
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                dgvChucVu.DataSource = Global.LoadDL("SELECT * FROM CHUCVU");

            }

        }

        //Hàm tính tiền
        private void Tinhtien()
        {
            if (decimal.TryParse(txtsoluong.Text, out decimal soluong) &&
                decimal.TryParse(txtdongia.Text, out decimal dongia) &&
                decimal.TryParse(txtVAT.Text, out decimal VAT))
            {
                decimal thanhTien = soluong * dongia * (1 + VAT / 100);
                txtthanhtien.Text = thanhTien.ToString();
                //txtthanhtien.Text = thanhTien.ToString("C2"); 
            }
            else
            {
                txtthanhtien.Text = "0";
            }
        }



        private void uiButton1_Click(object sender, EventArgs e)
        {

        }


        //Tính tổng tiền của hóa đơn
        private decimal Tinhtong(string MAHD)
        {
            decimal tongtien = 0;
            using (Global.con)
            {
                Global.con.Open();
                string query = "SELECT SUM(ThanhTien) FROM CHITIETHOADON WHERE MaHD = @MaHD";
                using (SqlCommand cmd = new SqlCommand(query, Global.con))
                {
                    cmd.Parameters.AddWithValue("@MaHD", MAHD);
                    tongtien = (decimal)cmd.ExecuteScalar();
                }
            }

            return tongtien;
        }

        //Button dấu + khi muốn thêm bản ghi => set các textbox thành true, cho phép nhập liệu 
        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            MaHD2.Enabled = true;
            cboMaHH.Enabled = true;
            txtsoluong.Enabled = true;
            txtdongia.Enabled = true;
            txtVAT.Enabled = true;
            txtthanhtien.Enabled = true;
        }


        //Tính tiền hóa đơn (tab hóa đơn)
        
        private async void TinhtienHD(string MAHD)
        {
            UseWaitCursor = true;
            decimal tongtienHD = await Task.Run(() => Tinhtong(MAHD));
            txtTongtien.Invoke((MethodInvoker)(() =>
            {
                txtTongtien.Text = tongtienHD.ToString();
                //txtTongtien.Text = tongtienHD.ToString("C"); // Định dạng tiền tệ
            }));

        }

        //Button reset các textbox
        private void uiSymbolButton3_Click(object sender, EventArgs e)
        {
            cboMaHH.Text = null;
            txtsoluong.Text = null;
            txtdongia.Text = null;
            txtthanhtien.Text = null;
            txtVAT.Text = null;
        }


        private void uiListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UC_NhaCungCap ncc = new UC_NhaCungCap();
            int i = uiListBox1.SelectedIndex;
            if (i == 0)
            {
                uiSplitContainer1.Panel2.Controls.Add(ncc);
            }
        }


        //Button đăng xuất form chính
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show(
                                    "Bạn có muốn đăng xuất không?",
                                    "Xác nhận",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Question);
            if (kq == DialogResult.OK)
            {
                
                Form fdn = new FDangnhap();
                fdn.Show();
                this.Hide();
            }
            else
            {

            }
        }

        //Button thêm chức vụ
        private void btnThem_Click(object sender, EventArgs e)
        {
            ChucVu cv = new ChucVu(txtMaCV.Text, txtTenCV.Text);
            if (txtMaCV.Text == null || txtTenCV.Text == null)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }
            if (bus.Insert(cv))
            {
                MessageBox.Show("Thêm chức vụ thành công!");
                dgvChucVu.DataSource = bus.Load("SELECT * FROM CHUCVU");
            }
            else
            {
                MessageBox.Show("Nhập trùng mã chức vụ!");
                return;
            }
        }

        //Button edit, cho phép chỉnh sửa và thao tác với các textbox
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            txtMaCV.Enabled = true;
            txtTenCV.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        //Form chính thay đổi kích thước
        private void FMain_Resize(object sender, EventArgs e)
        {
            treeView1.Height = this.ClientSize.Height - treeView1.Top - 10;
        }

        //Button sửa chức vụ
        private void btnSua_Click(object sender, EventArgs e)
        {
            ChucVu cv = new ChucVu(txtMaCV.Text, txtTenCV.Text);
            bus.Update(cv);
            MessageBox.Show("Sửa chức vụ thành công!");
            dgvChucVu.DataSource = bus.Load("SELECT * FROM CHUCVU");

        }

        //Button xóa chức vụ
        private void btnXoa_Click(object sender, EventArgs e)
        {
            ChucVu cv = new ChucVu(txtMaCV.Text);
            bus.Delete(cv);
            MessageBox.Show("Xóa chức vụ thành công!");
            dgvChucVu.DataSource = bus.Load("SELECT * FROM CHUCVU");

        }

        //Chọn dòng (chức vụ)
        private void chondong(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvChucVu.Rows.Count)
            {
                int i = e.RowIndex;
                txtMaCV.Text = dgvChucVu.Rows[i].Cells[0]?.Value?.ToString() ?? string.Empty;
                txtTenCV.Text = dgvChucVu.Rows[i].Cells[1]?.Value?.ToString() ?? string.Empty;
            }
            else
            {
                txtMaCV.Text = string.Empty;
                txtTenCV.Text = string.Empty;
            }
        }

        //Button cho phép nhập liệu

        private void PlusBtn_Click(object sender, EventArgs e)
        {
            txtMaHD1.Enabled = true;
            txtMaNV.Enabled = true;
            cboMaKH.Enabled = true;
            dpNgayban.Enabled = true;
            txtTongtien.Enabled = false;
            txtTrangthai.Enabled = true;
        }
        BUS_HoaDon busHD = new BUS_HoaDon();
        //Thêm Hóa đơn
        private void ThemHoaDon(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD1.Text) ||
                string.IsNullOrEmpty(txtMaNV.Text) ||
                string.IsNullOrEmpty(cboMaKH.Text) ||
                string.IsNullOrEmpty(dpNgayban.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }

            else
            {

                HoaDon hd = new HoaDon(
                        txtMaHD1.Text,
                        txtMaNV.Text,
                        cboMaKH.Text,
                        Convert.ToDateTime(dpNgayban.Text),
                        string.IsNullOrEmpty(txtTongtien.Text) ? (decimal?)null : Convert.ToDecimal(txtTongtien.Text),
                        txtTrangthai.Text);

                if (busHD.Insert(hd))
                {
                    MessageBox.Show("Thêm hóa đơn thành công!");
                    dgvHoaDon.DataSource = busHD.Load("SELECT * FROM HOADONBANHANG");
                }
                else
                {
                    MessageBox.Show("Nhập trùng mã hóa đơn!");
                    txtMaHD1.Focus();
                    return;
                }

            }
        }

        //Khi nhập liệu cho mã hóa đơn, tự động textbox của mã hóa đơn (CTHD) được điền sẵn (cho tiện)
        private void txtMaHD1_TextChanged(object sender, EventArgs e)
        {
            MaHD2.Text = txtMaHD1.Text;
        }

        private void uiNavBar1_MenuItemClick(string itemText, int menuIndex, int pageIndex)
        {

        }

        //In bill

        private void BtnInHoaDon_Click(object sender, EventArgs e)
        {
            string sql = "SELECT HOADONBANHANG.MAHD, NHANVIEN.MANV, NGAYBAN, HANGHOA.MAHH, " +
                "TENHH, SOLUONG, DONGIA, THANHTIEN, TILE_VAT, TONGTIENHD, PHUONGTHUC" +
                " FROM HOADONBANHANG, NHANVIEN, CHITIETHOADON, HANGHOA, THANHTOAN" +
                " WHERE HOADONBANHANG.MAHD = CHITIETHOADON.MAHD" +
                " AND NHANVIEN.MANV = HOADONBANHANG.MANV" +
                " AND HANGHOA.MAHH = CHITIETHOADON.MAHH" +
                " AND HOADONBANHANG.MAHD = THANHTOAN.MAHD" +
                " AND HOADONBANHANG.MAHD = '" + cboMaHD.Text + "'";
            DataTable dt = bus.Load(sql);
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;

       
            reportViewer1.LocalReport.ReportPath = "D:\\Windows Form\\QUANLYCUAHANG\\QUANLYCUAHANG\\HoaDon.rdlc";
            ReportParameter[] reportParameters = new ReportParameter[]
       {
                new ReportParameter("TienKhachTra", txtTienKhachTra.Text),
                new ReportParameter("TienThua", txtTienThua.Text)
       };
            reportViewer1.LocalReport.SetParameters(reportParameters);
            if (dt.Rows.Count > 0)
            {
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "HOADON";
                rds.Value = dt;
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
            else MessageBox.Show("Không có dữ liệu");
        }

        //Dự phòng (chọn mã hđ, hiện lên tổng tiền của hđ đó)
        private void ChonMaHD(object sender, EventArgs e)
        {
            DataTable dtTien = bus.Load("SELECT TOP 1 TONGTIENHD FROM HOADONBANHANG WHERE MAHD = '" + cboMaHD.Text + "'");
            if (dtTien.Rows.Count > 0)
            {
                txtSoTien.Text = dtTien.Rows[0]["TONGTIENHD"].ToString();
            }
            else
            {
                txtSoTien.Text = "0";
            }
        }
        BUS_ChiTietHoaDon buscthd = new BUS_ChiTietHoaDon();
        //Thêm Chi tiết hóa đơn vào CSDL
        private void btnLuuCTHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MaHD2.Text) ||
                string.IsNullOrEmpty(cboMaHH.Text) ||
                string.IsNullOrEmpty(txtsoluong.Text) ||
                string.IsNullOrEmpty(txtdongia.Text) ||
                string.IsNullOrEmpty(txtVAT.Text) ||
                string.IsNullOrEmpty(txtthanhtien.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }
            int soluong;
            if (!int.TryParse(txtsoluong.Text, out soluong))
            {
                MessageBox.Show("Số lượng không hợp lệ!");
                txtsoluong.Focus();
                return;
            }
            decimal dongia;
            if (!decimal.TryParse(txtdongia.Text, out dongia))
            {
                MessageBox.Show("Đơn giá không hợp lệ!");
                txtdongia.Focus();
            }
            decimal VAT;
            if (!decimal.TryParse(txtVAT.Text, out VAT))
            {
                MessageBox.Show("Tỉ lệ VAT không hợp lệ!");
                txtVAT.Focus();
                return;
            }
            decimal thanhtien = decimal.Parse(txtthanhtien.Text);
            ChiTietHoaDon cthd = new ChiTietHoaDon(MaHD2.Text, cboMaHH.SelectedValue.ToString(), soluong, dongia, VAT, thanhtien);
            HoaDon hd = new HoaDon(txtMaHD1.Text);
            if (buscthd.Insert(cthd, hd))
            {
                MessageBox.Show("Lưu chi tiết hóa đơn thành công!");
                dgvCTHD.DataSource = buscthd.Load("SELECT * FROM CHITIETHOADON");
                dgvHoaDon.DataSource = buscthd.Load("SELECT * FROM HOADONBANHANG");
            }
        }

        // Cho phép thêm CTHD
        private void PlusBtnCTHD_Click(object sender, EventArgs e)
        {
            MaHD2.Enabled = true;
            cboMaHH.Enabled = true;
            txtsoluong.Enabled = true;
            txtdongia.Enabled = true;
            txtVAT.Enabled = true;
            txtthanhtien.Enabled = true;
        }

        //Thêm khách hàng
        BUS_KhachHang buskh = new BUS_KhachHang();
        private void BtnThemKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Mã khách hàng không được để trống!");
                txtMaKH.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txtEmailKH.Text) && !Regex.IsMatch(txtEmailKH.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ.");
                txtEmailKH.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txtSDTKH.Text))
            {
                if (!Regex.IsMatch(txtSDTKH.Text, @"^(0[1-9])(\d{8})$") || txtSDTKH.Text.Length < 9)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ hoặc phải có ít nhất 9 chữ số.");
                    txtSDTKH.Focus();
                    return;
                }
            }

            KhachHang kh = new KhachHang(
                txtMaKH.Text,
                string.IsNullOrEmpty(txtTenKH.Text) ? "KVL" : txtTenKH.Text,
                string.IsNullOrEmpty(txtDiaChiKH.Text) ? null : txtDiaChiKH.Text,
                string.IsNullOrEmpty(txtSDTKH.Text) ? null : txtSDTKH.Text,
                string.IsNullOrEmpty(txtEmailKH.Text) ? null : txtEmailKH.Text
            );

            if (buskh.Insert(kh))
            {
                MessageBox.Show("Thêm khách hàng thành công!");
                cboMaKH.DataSource = bus.Load("SELECT MAKH FROM KHACHHANG");
                cboMaKH.DisplayMember = "MAKH";
                dgvKhachHang.DataSource = buskh.Load("SELECT * FROM KHACHHANG");
            }
            else
            {
                MessageBox.Show("Nhập trùng mã khách hàng!");
                txtMaKH.Focus();
            }
        }

        private void QuaTrangThanhToan_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabThanhtoan;
            Global.mahd = txtMaHD1.Text;
            cboMaHD.DataSource = bus.Load("SELECT MAHD FROM HOADONBANHANG");
            cboMaHD.DisplayMember = "MAHD";
            cboMaHD.Text = Global.mahd;

        }

        private void DongFMain(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }


        //Tạo mã QR thanh toán
        //Số tiền được lấy từ textbox, nội dung chuyển tiền + mã hóa đơn lấy từ tb mahd
        private void btnTaoMaQR_Click(object sender, EventArgs e)
        {

            string stk = lbSTK.Text;
            string sotien = txtSoTien.Text;
            string noidungchuyentien = "Thanh toán hóa đơn " + cboMaHD.Text;

            var qrPay = QRPay.InitVietQR(
                bankBin: BankApp.BanksObject[BankKey.VIETINBANK].bin,
                bankNumber: stk,
                amount: sotien,
                purpose: noidungchuyentien
            );

            var content = qrPay.Build();

            var imageQR = QRCodeHelper.TaoVietQRCodeImage(content);

            QRBox.Image = imageQR;
        }

        //Button sửa khách hàng
        private void BtnSuaKH_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSDTKH.Text))
            {
                if (!Regex.IsMatch(txtSDTKH.Text, @"^(0[1-9])(\d{8})$"))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ.");
                    txtSDTKH.Focus();
                    return;

                }
                if (txtSDTKH.Text.Length < 9)
                {
                    MessageBox.Show("Số điện thoại phải có ít nhất 9 chữ số.");
                    txtSDTKH.Focus();
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtEmailKH.Text))
            {
                if (!Regex.IsMatch(txtEmailKH.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Email không hợp lệ.");
                    txtEmailKH.Focus();
                    return;
                }
            }

            KhachHang kh = new KhachHang(txtMaKH.Text, txtTenKH.Text, string.IsNullOrEmpty(txtDiaChiKH.Text) ? null : txtDiaChiKH.Text, string.IsNullOrEmpty(txtSDTKH.Text) ? null : txtSDTKH.Text, string.IsNullOrEmpty(txtEmailKH.Text) ? null : txtEmailKH.Text);
            if (buskh.Update(kh))
            {
                MessageBox.Show("Cập nhật khách hàng thành công!");
            }
            else
                MessageBox.Show("Mã khách hàng không tồn tại!");
            txtMaKH.Focus();
            return;
        }

        //Chọn dòng - dgv Hóa đơn
        private void chondongdgvHoaDon(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i < dgvHoaDon.Rows.Count)
            {
                txtMaHD1.Text = dgvHoaDon.Rows[i].Cells[0].Value.ToString();
                txtMaNV.Text = dgvHoaDon.Rows[i].Cells[1].Value.ToString();
                cboMaKH.Text = dgvHoaDon.Rows[i].Cells[2].Value.ToString();
                dpNgayban.Text = dgvHoaDon.Rows[i].Cells[3].Value.ToString();
                txtTongtien.Text = dgvHoaDon.Rows[i].Cells[4].Value.ToString();
            }    
           else
            {
                txtMaHD1.Text = string.Empty;
                txtMaNV.Text = string.Empty;
                cboMaKH.Text = string.Empty;
                dpNgayban.Text = string.Empty;
                txtTongtien.Text = string.Empty;
            }    
        }

        //Sự kiện cho textbox số lượng, thay đổi số lượng lập tức số tiền thay đổi
        private void txtsoluong_TextChanged(object sender, EventArgs e)
        {
            Tinhtien();
        }
        //tương tự
        private void txtdongia_TextChanged(object sender, EventArgs e)
        {
            Tinhtien();
        }
        //tương tự
        private void txtVAT_TextChanged(object sender, EventArgs e)
        {
            Tinhtien();
        }

        //Nhập số tiền khách trả, tiền thừa sẽ được tự động tính dựa vào giá trị của ô này
        private void txtTienKhachTra_TextChanged(object sender, EventArgs e)
        {
            decimal sotien = decimal.Parse(txtSoTien.Text);
            decimal tienkhachtra;
            if (!decimal.TryParse(txtTienKhachTra.Text, out tienkhachtra))
            {
                MessageBox.Show("Số tiền không hợp lệ!\nVui lòng nhập lại.");
                txtTienKhachTra.Focus();
                return;
            }    
            decimal tienthua = tienkhachtra - sotien;
            txtTienThua.Text = tienthua.ToString();
        }


        private void ResetHD_Click(object sender, EventArgs e)
        {
            txtMaHD1.Clear();
            txtTongtien.Clear();
            txtTrangthai.Clear();
        }

        private void ResetCTHD_Click(object sender, EventArgs e)
        {
            MaHD2.Clear();
            txtsoluong.Clear();
            txtdongia.Clear();
        }

        //Khách hàng thanh toán xong, lưu bản ghi vào CSDL
        //Đồng thời cập nhật trạng thái cho hóa đơn đó thành Đã thanh toán
        private void btnLuuThanhToan_Click(object sender, EventArgs e)
        {
            BUS_ThanhToan bus = new BUS_ThanhToan();
            DateTime ngaytt = DateTime.Parse(NgayTT.Text);
            decimal sotien = decimal.Parse(txtSoTien.Text);
            ThanhToan tt = new ThanhToan(txtMaTT.Text, cboMaHD.Text, ngaytt, sotien, cboPhuongThuc.Text);
            HoaDon hd = new HoaDon(cboMaHD.Text, "Đã thanh toán");
            if (bus.Insert(tt, hd))
            {
                MessageBox.Show("Lưu thanh toán thành công!\nĐã cập nhật tình trạng hóa đơn.");
                dgvHoaDon.DataSource = buscthd.Load("SELECT * FROM HOADONBANHANG");
                dgvThanhToan.DataSource = buscthd.Load("SELECT * FROM THANHTOAN");
            }
            else
                MessageBox.Show("Nhập trùng mã thanh toán!");
            txtMaTT.Focus();
            return;
         }

        //Sự kiện đóng form main
        //Nếu là do người dùng nhấn dấu x, thì đóng toàn bộ các form kể cả form đăng nhập
        //(Khác với button Đăng xuất, nếu người dùng chỉ đăng xuất thôi thì hiện form đăng nhập lên)
        //Chỉ khii ng dùng nhấn thoát ở form đăng xuất thì mới thoát hoàn toàn ứng dụng
        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            { 
                e.Cancel = true;
                Application.Exit();
            }
        }

        private void ChonDongdgvThanhToan(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMaTT.Text = dgvThanhToan.Rows[i].Cells[0].Value.ToString();
            cboMaHD.Text = dgvThanhToan.Rows[i].Cells[1].Value.ToString();
            NgayTT.Text = dgvThanhToan.Rows[i].Cells[2].Value.ToString();
            txtSoTien.Text = dgvThanhToan.Rows[i].Cells[3].Value.ToString();
            cboPhuongThuc.Text = dgvThanhToan.Rows[i].Cells[4].Value.ToString();
        }

        //Khi form load, dgv được chọn tự động => tb mã nv có thể bị sai
        //Lấy mã nhân viên của phiên đăng nhập hiện tại 
        private void txtMaNV_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = Global.manv;
        }


        //Phiếu nhập 
        /*
         *Ngày tháng năm
         *Họ tên nhà cung cấp
         *Địa chỉ, SĐT NCC (nên có)
         *Mã nhân viên
         *Mã phiếu nhập
         *STT - Mã hàng hóa - Tên hàng hóa - Số lượng - Đơn giá - Chiết khấu (nếu có) - Thành tiền
         *=> Tính tổng tiền
         *Ngày... (cắt date từ chuỗi ngày nhập), tháng....(cắt month), năm...(cắt year ra)
         *Người lập phiếu (Tên NV), Người giao hàng (ký + họ tên) 
         */
        //Phiếu xuất
        /*
         * Ngày tháng năm
         * Tên khách hàng
         * Địa chỉ, SĐT (nếu có)
         * 
         */
        //Báo cáo thông tin nhập hàng hóa gần đây của mỗi nhà cung cấp
        //Báo cáo doanh thu của mỗi hàng hóa trong ngày
        //Báo cáo số lượng hàng tồn kho
        //Báo cáo số lượng đơn hàng của mỗi nhân viên trong tháng
        //Báo cáo những mặt hàng bán được nhiều nhất trong ngày, trong tháng, trong quý (tùy nhu cầu)

    }
}
