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
using Sunny.UI;
namespace QUANLYCUAHANG
{
    public partial class FDangnhap : UILoginForm
    {
        public FDangnhap()
        {
            InitializeComponent();
        }
        
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {

            if (username.Text == string.Empty || password.Text == string.Empty)
            {
                this.ShowErrorTip("Vui lòng nhập đủ thông tin");
                return;
            }
            if (Global.Dangnhap(cboRole.Text, username.Text, password.Text))
            {
                this.ShowSuccessTip("Đăng nhập thành công! \nChào mừng bạn đến với Hệ Thống Quản Lý Bán Hàng EasyMart");
                this.Hide();
                Global.tendangnhap = username.Text;
                Global.chucvu = cboRole.Text;
                Form f = new FMain();
                //f.FormClosed += (s, args) => this.Close();
                f.Show();
            }
            else
            {
                this.ShowErrorTip("Thông tin đăng nhập không chính xác hoặc bạn không có quyền đăng nhập!");
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = Global.LoadDL("SELECT TENCV FROM CHUCVU");
            cboRole.DataSource = dt;
            cboRole.DisplayMember = "TENCV";
            //cboRole.Font = new Font("Quicksand", 10, FontStyle.Bold);
        }
    }
}
