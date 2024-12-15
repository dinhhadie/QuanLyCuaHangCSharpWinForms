using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using BUS; 
using DTO;
namespace QUANLYCUAHANG.Forms
{
    public partial class FNganhHang : Form
    {
        public FNganhHang()
        {
            InitializeComponent();
        }
        BUS_NganhHang bus = new BUS_NganhHang();

        private void FNganhHang_Load(object sender, EventArgs e)
        {
            uiDataGridView1.DataSource = bus.Load("SELECT * FROM NGANHHANG");
        }
        public void View()
        {
            uiDataGridView1.DataSource = bus.Load("SELECT * FROM NGANHHANG");
        }

        private void chondong(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtmanganh.Text = uiDataGridView1.Rows[i].Cells[0].Value?.ToString();
            txttennganh.Text = uiDataGridView1.Rows[i].Cells[1].Value?.ToString();
        }

        private void ThemNganhHang_Click(object sender, EventArgs e)
        {
            if (txtmanganh.Text == "" || txttennganh.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            if (txtmanganh.Text.Length > 20)
            {
                MessageBox.Show("Mã ngành tối đa 20 ký tự!");
                txtmanganh.Focus();
                return;
            }
            if (txttennganh.Text.Length > 50)
            {
                MessageBox.Show("Tên ngành tối đa 50 ký tự!");
                txttennganh.Focus();
                return;
            }
            try
            {
                NganhHang nh = new NganhHang(txtmanganh.Text, txttennganh.Text);

                if (bus.Insert(nh))
                    MessageBox.Show("Thêm ngành hàng thành công!");
                else MessageBox.Show("Nhập trùng mã ngành!");
                txtmanganh.Focus();
                View();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SuaNganhHang_Click(object sender, EventArgs e)
        {
            if (txtmanganh.Text == "" || txttennganh.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            NganhHang nh = new NganhHang(txtmanganh.Text, txttennganh.Text);
            bus.Update(nh);
            MessageBox.Show("Cập nhật ngành hàng thành công!");
            txtmanganh.Clear();
            txttennganh.Clear();
            View();
        }

        private void XoaNganhHang_Click(object sender, EventArgs e)
        {
            if (txtmanganh.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã ngành.");
                return;
            }
            NganhHang nh = new NganhHang(txtmanganh.Text);
            bus.Delete(nh);
            MessageBox.Show("Xóa ngành hàng thành công!");
            txtmanganh.Clear();
            txttennganh.Clear();
            View();
        }
    }
}
