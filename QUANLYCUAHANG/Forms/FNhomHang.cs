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
    public partial class FNhomHang : Form
    {
        public FNhomHang()
        {
            InitializeComponent();
        }
        BUS_NhomHang bus = new BUS_NhomHang();
        private void ThemNhomHang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtMaNhomHang.Text))
            {
                MessageBox.Show("Mã nhóm hàng không được để trống!");
                return;
            }
            NhomHang nh = new NhomHang(TxtMaNhomHang.Text, CboMaNganh.SelectedValue.ToString(), TxtTenNhomHang.Text);
            if (bus.Insert(nh))
            {
                MessageBox.Show("Thêm nhóm hàng thành công!");
                DgvNhomHang.DataSource = bus.Load("SELECT * FROM NHOMHANG");
            }
            else
            {
                MessageBox.Show("Nhập trùng mã nhóm hàng!");
            }    
        }

        private void FNhomHang_Load(object sender, EventArgs e)
        {
            CboMaNganh.DataSource = bus.Load("SELECT * FROM NGANHHANG");
            CboMaNganh.DisplayMember = "TENNGANH";
            CboMaNganh.ValueMember = "MANGANH";
            DgvNhomHang.DataSource = bus.Load("SELECT * FROM NHOMHANG");
           
        }

        private void chondong(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            TxtMaNhomHang.Text = DgvNhomHang.Rows[i].Cells[0].Value.ToString();
            CboMaNganh.Text = DgvNhomHang.Rows[i].Cells[1].Value.ToString();
            TxtTenNhomHang.Text = DgvNhomHang.Rows[i].Cells[2].Value.ToString();
        }

        private void SuaNhomHang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtMaNhomHang.Text) ||
                string.IsNullOrEmpty(TxtTenNhomHang.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }    
            NhomHang nh = new NhomHang(TxtMaNhomHang.Text, CboMaNganh.Text, TxtTenNhomHang.Text);
            if (bus.Update(nh))
            {
                MessageBox.Show("Sửa nhóm hàng thành công!");
            }
            else
                MessageBox.Show("Mã nhóm hàng không tồn tại!");
                TxtMaNhomHang.Focus();
            return;
        }

        private void XoaNhomHang_Click(object sender, EventArgs e)
        {
            NhomHang nh = new NhomHang(TxtMaNhomHang.Text);
            if (bus.Delete(nh))
            {
                MessageBox.Show("Xóa nhóm hàng thành công!");
            }
            else
            {
                MessageBox.Show("Mã nhóm hàng không tồn tại!");
            } 
                
        }
    }
}
