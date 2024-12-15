using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using Sunny.UI;
namespace QUANLYCUAHANG.Forms
{
    public partial class FDoiMatKhau : Form
    {
        public FDoiMatKhau()
        {
            InitializeComponent();
        }
        BUS_NhanVien bus = new BUS_NhanVien();

        private void FDoiMatKhau_Load(object sender, EventArgs e)
        {
            gbDoiMatKhau.Text = "Đổi mật khẩu cho " + Global.chucvu;
            button1.Image = ResizeImage(Properties.Resources.eye_open, button1.Width, button1.Height);
            button2.Image = ResizeImage(Properties.Resources.eye_open, button2.Width, button2.Height);
            button1.BackColor = Color.Transparent;
            button2.BackColor = Color.Transparent;
            button1.BringToFront();
            button2.BringToFront();
            txtMaNV.Text = Global.manv;
        }

        private Image ResizeImage(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(img, new Size(width, height));
            return bmp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMKmoi.Text == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu!");
                return;
            }    
            if (txtMKmoi.PasswordChar == '*')
            {
                txtMKmoi.PasswordChar = '\0';
                button1.Image = ResizeImage(Properties.Resources.eye_open, button1.Width, button1.Height);
                button1.ImageAlign = ContentAlignment.MiddleCenter;

            }
            else
            {
                txtMKmoi.PasswordChar = '*';
                button1.Image = ResizeImage(Properties.Resources.eye_close, button1.Width, button1.Height);
                button1.ImageAlign = ContentAlignment.MiddleCenter; 

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtxacnhanMK.Text == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu xác nhận!");
                return;
            }
            if (txtxacnhanMK.PasswordChar == '*')
            {
                txtxacnhanMK.PasswordChar = '\0';
                button2.Image = ResizeImage(Properties.Resources.eye_open, button2.Width, button2.Height);
                button2.ImageAlign = ContentAlignment.MiddleCenter;

            }
            else
            {
                txtxacnhanMK.PasswordChar = '*';
                button2.Image = ResizeImage(Properties.Resources.eye_close, button2.Width, button2.Height);
                button2.ImageAlign = ContentAlignment.MiddleCenter;

            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMKmoi.Text == string.Empty || txtxacnhanMK.Text  == string.Empty)
            {
                this.ShowErrorTip("Vui lòng nhập đủ các trường!");
                return;
            }    
            if (txtMKmoi.Text.Equals(txtxacnhanMK.Text) == false)
            {
                this.ShowErrorTip("Mật khẩu không trùng khớp!");
                return;
            }
            else
            {
                string tendangnhap = Global.tendangnhap;
                string matkhau = txtMKmoi.Text;
                TaiKhoan tk = new TaiKhoan(tendangnhap, matkhau);


                //in DAL Nhân viên
                if (bus.DoiMatKhau(tk))
                {
                    this.ShowSuccessTip("Đổi mật khẩu thành công");
                    txtMKmoi.Clear();
                    txtxacnhanMK.Clear();
                }
                else
                {
                    this.ShowErrorTip("Bạn đã nhập mật khẩu cũ!\nVui lòng nhập lại");
                    txtMKmoi.Focus();
                    return;
                }
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
