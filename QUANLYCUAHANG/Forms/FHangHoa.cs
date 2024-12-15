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
    public partial class FHangHoa : Form
    {
        public FHangHoa()
        {
            InitializeComponent();
        }
        BUS_HangHoa bus = new BUS_HangHoa();
        private void ThemHangHoa_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtMaHH.Text) ||
                string.IsNullOrEmpty(cboMaNhomHang.SelectedValue.ToString()) ||
                string.IsNullOrEmpty(txtTenHH.Text) ||
                string.IsNullOrEmpty(txtDonViTinh.Text) ||
                string.IsNullOrEmpty(txtTrongLuong.Text) ||
                string.IsNullOrEmpty(txtNSX.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }
            float trongluong;
            if (!float.TryParse(txtTrongLuong.Text, out trongluong))
            {
                MessageBox.Show("Trọng lượng không hợp lệ!\nVui lòng nhập lại!");
                txtTrongLuong.Focus();
                return;
            }
            DateTime hsd = Convert.ToDateTime(HSD.Text);
            HangHoa hh = new HangHoa(txtMaHH.Text, cboMaNhomHang.SelectedValue.ToString(),txtTenHH.Text, txtDonViTinh.Text, trongluong, hsd, txtNSX.Text);
            if (bus.Insert(hh))
            {
                MessageBox.Show("Thêm hàng hóa thành công!");
                dgvHangHoa.DataSource = bus.Load("SELECT * FROM HANGHOA");

            }
            else
                MessageBox.Show("Nhập trùng mã hàng hóa!");
            return;
        }

        private void FHangHoa_Load(object sender, EventArgs e)
        {
            cboMaNhomHang.DataSource = bus.Load("SELECT * FROM NHOMHANG");
            cboMaNhomHang.DisplayMember = "TENNHOM";
            cboMaNhomHang.ValueMember = "MANHOM";
            dgvHangHoa.DataSource = bus.Load("SELECT * FROM HANGHOA");
            dgvHangHoa.BringToFront();
        }

       

        private void SuaHangHoa_Click(object sender, EventArgs e)
        {

        }

        private void dgvHangHoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void XoaHangHoa_Click(object sender, EventArgs e)
        {

        }
    }
}
