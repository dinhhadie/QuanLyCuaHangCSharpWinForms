using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYCUAHANG
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            //UIStyleManager.UICulture = new CultureInfo("en-US"); // Đặt toàn bộ ứng dụng dùng tiếng Anh
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FDangnhap());
        }
    }
}
