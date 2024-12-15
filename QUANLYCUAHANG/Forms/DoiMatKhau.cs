using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYCUAHANG.Controls
{
    public partial class DoiMatKhau : UserControl
    {
        public DoiMatKhau(string chucvu)
        {
            InitializeComponent();
            uiLabel2.Text = "Đổi mật khẩu cho " + chucvu;
        }
        private static DoiMatKhau _instance;
        /*public static DoiMatKhau Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DoiMatKhau();
                return _instance;
            }
        }*/
    }
}
