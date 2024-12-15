using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYCUAHANG.Forms
{
    public partial class FNhaCungCap : Form
    {
        public FNhaCungCap()
        {
            InitializeComponent();
        }
        private readonly BUS_NhaCungCap bus = new BUS_NhaCungCap();
        private void FNhaCungCap_Load(object sender, EventArgs e)
        {
            CboTinh.DataSource = bus.Load("SELECT * FROM TINH");
            CboTinh.DisplayMember = "NAME";
            CboTinh.ValueMember = "PROVINCE_ID";
            dgvNhaCungCap.DataSource = bus.Load("SELECT * FROM NHACUNGCAP");
        }
        
        private void Chontinh(object sender, EventArgs e)
        {
            if (CboTinh.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)CboTinh.SelectedItem;
                int provinceId = Convert.ToInt32(selectedRow["PROVINCE_ID"]);
                string sql = "SELECT DISTRICT_ID, NAME FROM QUANHUYEN WHERE PROVINCE_ID = " + provinceId;
                CboHuyen.DataSource = bus.Load(sql);
                CboHuyen.DisplayMember = "NAME";
                CboHuyen.ValueMember = "DISTRICT_ID";
            }
        }

        private void Chonhuyen(object sender, EventArgs e)
        {
            if (CboHuyen.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)CboHuyen.SelectedItem;
                int districtId = Convert.ToInt32(selectedRow["DISTRICT_ID"]);
                string sql = "SELECT WARDS_ID, NAME FROM PHUONGXA WHERE DISTRICT_ID = " + districtId;
                CboXa.DataSource = bus.Load(sql);
                CboXa.DisplayMember = "NAME";
            }

        }
        private bool Kiemtra()
        {
            if (string.IsNullOrEmpty(TxtMaNCC.Text) || string.IsNullOrEmpty(TxtTenNCC.Text))
            {
                MessageBox.Show("Mã NCC hoặc tên NCC không được để trống!");
                return false;
            }
            if (!string.IsNullOrEmpty(TxtEmail.Text))
            {
                if (!Regex.IsMatch(TxtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Email không hợp lệ.");
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(TxtDT.Text))
            {
                if (!Regex.IsMatch(TxtDT.Text, @"^(0[1-9])(\d{8})$"))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ.");
                    return false;
                }
                if (TxtDT.Text.Length < 9)
                {
                    MessageBox.Show("Số điện thoại phải có ít nhất 9 chữ số.");
                    return false;
                }
            }
            
            return true;
        }
        private void ThemNCC_Click(object sender, EventArgs e)
        {
            if (!Kiemtra()) return;
            NhaCungCap ncc = new NhaCungCap(TxtMaNCC.Text, TxtTenNCC.Text, TxtDiaChi.Text, TxtDT.Text, TxtFax.Text, TxtEmail.Text);
            if (bus.Insert(ncc))
            {
                MessageBox.Show("Thêm nhà cung cấp thành công!");
                dgvNhaCungCap.DataSource = bus.Load("SELECT * FROM NHACUNGCAP");
            }
            else
            {
                MessageBox.Show("Nhập trùng mã NCC!");
                return;
            }
        }

        private void TxtDCCT_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtDCCT.Text))
            {
                TxtDiaChi.Text = CboXa.Text + ", " + CboHuyen.Text + ", " + CboTinh.Text;
            }
            else
            {
                TxtDiaChi.Text = TxtDCCT.Text + ", " + CboXa.Text + ", " + CboHuyen.Text + ", " + CboTinh.Text;
            }
        }

        private void SuaNCC_Click(object sender, EventArgs e)
        {
            if (!Kiemtra()) return;
            NhaCungCap ncc = new NhaCungCap(TxtMaNCC.Text, TxtTenNCC.Text, TxtDiaChi.Text, TxtDT.Text, TxtFax.Text, TxtEmail.Text);
            bus.Update(ncc);
            dgvNhaCungCap.DataSource = bus.Load("SELECT * FROM NHACUNGCAP");
        }

        private void XoaNCC_Click(object sender, EventArgs e)
        {
            if (TxtMaNCC.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã NCC!");
                return;
            }    
            NhaCungCap ncc = new NhaCungCap(TxtMaNCC.Text);
            if (bus.Delete(ncc))
            {
                MessageBox.Show("Xóa NCC thành công!");
                dgvNhaCungCap.DataSource = bus.Load("SELECT * FROM NHACUNGCAP");
            }
            else
            {
                MessageBox.Show("Mã NCC không hợp lệ!");
            }
        }

        private void Chonxa(object sender, EventArgs e)
        {
            /*if (CboTinh.SelectedIndex != -1 && CboHuyen.SelectedIndex != -1 && CboXa.SelectedIndex != -1)
            {
                TxtDiaChi.Text = CboXa.Text + ", " + CboHuyen.Text + ", " + CboTinh.Text;
            }
            else
            {
                TxtDiaChi.Clear();
            }
            
            
            */
        }

        
    }
}
