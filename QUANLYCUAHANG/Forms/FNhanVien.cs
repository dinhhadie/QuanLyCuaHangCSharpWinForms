using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using DAL;
namespace QUANLYCUAHANG.Forms
{
    public partial class FNhanVien : Form
    {
        public FNhanVien()
        {
            InitializeComponent();
        }
        private readonly BUS_NhanVien bus = new BUS_NhanVien();
       
        private  void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                NhanVien nv = new NhanVien(txtMaNV.Text, cboCV.SelectedValue.ToString(), txtTenNV.Text, cboGT.Text, txtSdt.Text, tbDiaChi.Text);
                string tenDangNhap = Global.Chuyendoi(txtTenNV.Text);
                TaiKhoan tk = new TaiKhoan(txtMaNV.Text.ToLower() + tenDangNhap, txtMaNV.Text, "123456", 1);
                BUS_NhanVien bus = new BUS_NhanVien();
                if (bus.Insert(nv, tk))
                {
                    MessageBox.Show("Thêm nhân viên và tài khoản thành công!");
                    View();
                }   
                else
                {
                    MessageBox.Show("Nhập trùng mã nhân viên!");
                    txtMaNV.Focus();
                    return;
                }    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
            /*try
            {
                Global.con.Open();
                using (SqlTransaction transaction = Global.con.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmdNhanVien = new SqlCommand("INSERT INTO NHANVIEN (MaNV, MaCV, TenNV, GioiTinh, Sodienthoai, DiaChi) VALUES (@manv, @macv, @tennv, @gioitinh, @sdt, @diachi)", Global.con, transaction);
                        cmdNhanVien.Parameters.AddWithValue("@manv", txtMaNV.Text);
                        cmdNhanVien.Parameters.AddWithValue("@macv", txtMaCV.Text);
                        cmdNhanVien.Parameters.AddWithValue("@tennv", txtTenNV.Text);
                        cmdNhanVien.Parameters.AddWithValue("@gioitinh", txtGioitinh.Text);
                        cmdNhanVien.Parameters.AddWithValue("@sdt", txtSdt.Text);
                        cmdNhanVien.Parameters.AddWithValue("@diachi", tbDiaChi.Text);
                        cmdNhanVien.ExecuteNonQuery();
                        string maNV = txtMaNV.Text;
                        string tendangnhap = Global.Chuyendoi(txtTenNV.Text);
                        string matkhau = "123456"; 
                        byte trangthai = 1; 

                        SqlCommand cmdTaiKhoan = new SqlCommand("INSERT INTO QUYENDANGNHAP (TenDangNhap, MaNV, MatKhau, TrangThai) VALUES (@tendangnhap, @manv, @matkhau, @trangthai)", Global.con, transaction);
                        cmdTaiKhoan.Parameters.AddWithValue("@tendangnhap", tendangnhap);
                        cmdTaiKhoan.Parameters.AddWithValue("@manv", maNV);
                        cmdTaiKhoan.Parameters.AddWithValue("@matkhau", matkhau);
                        cmdTaiKhoan.Parameters.AddWithValue("@trangthai", trangthai);
                        cmdTaiKhoan.ExecuteNonQuery();
                        transaction.Commit();

                        MessageBox.Show("Thêm nhân viên và tài khoản thành công!");
                        View();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Lỗi: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}");
            }
            finally
            {
                if (Global.con.State == ConnectionState.Open)
                {
                    Global.con.Close();
                }
            }*/
        }
        private void View()
        {
            DgvNhanVien.DataSource = bus.Load("SELECT * FROM NHANVIEN");
            dgvQuyenDangNhap.DataSource = bus.Load("SELECT * FROM QUYENDANGNHAP");
        }

        private void FNhanVien_Load(object sender, EventArgs e)
        {
            View();
            cboCV.DataSource = bus.Load("SELECT * FROM CHUCVU");
            cboCV.DisplayMember = "TENCV";
            cboCV.ValueMember = "MACV";
            List<string> gioitinh = new List<string> { "Nam", "Nữ" };
            /*foreach(var item in gioitinh)
            {
                cboGT.Items.Add(item);
          
            }*/
            cboGT.DataSource = gioitinh;
        }

        private void chondong(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < DgvNhanVien.Rows.Count)
            {
                int i = e.RowIndex;
                txtMaNV.Text = DgvNhanVien.Rows[i].Cells[0]?.Value?.ToString() ?? string.Empty;
                cboCV.Text = DgvNhanVien.Rows[i].Cells[1]?.Value?.ToString() ?? string.Empty;
                txtTenNV.Text = DgvNhanVien.Rows[i].Cells[2]?.Value?.ToString() ?? string.Empty;
                cboGT.Text = DgvNhanVien.Rows[i].Cells[3]?.Value?.ToString() ?? string.Empty;
                txtSdt.Text = DgvNhanVien.Rows[i].Cells[4]?.Value?.ToString() ?? string.Empty;
                tbDiaChi.Text = DgvNhanVien.Rows[i].Cells[5]?.Value?.ToString() ?? string.Empty;
            }
            else
            {
                txtMaNV.Text = string.Empty;
                cboCV.Text = string.Empty;
                txtTenNV.Text = string.Empty;
                cboGT.Text = string.Empty;
                txtSdt.Text = string.Empty;
                tbDiaChi.Text = string.Empty;
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Mã nhân viên không được để trống!");
                return;
            }
            TaiKhoan tk = new TaiKhoan(txtMaNV.Text);
            NhanVien nv = new NhanVien(txtMaNV.Text);
            if (bus.Delete(nv, tk))
            {
                MessageBox.Show("Xóa nhân viên và tài khoản thành công!");
                View();
            }
            else
            {
                MessageBox.Show("Mã nhân viên không tồn tại!");
            }
        }

     
    }
}
