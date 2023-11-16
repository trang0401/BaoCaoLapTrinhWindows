using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLiBanHang.Class;

namespace QuanLiBanHang
{
    public partial class FromMain : Form
    {
        public FromMain()
        {
            InitializeComponent();// laap trinh 2 lop nen co connect vaf disconnect
        }

        private void FromMain_Load(object sender, EventArgs e)
        {
           Functions.Connect();// mở kn
       
        }
    private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect();
            Application.Exit();
        }

        private void mnuChatLieu_Click(object sender, EventArgs e)
        {
            frmDMChatLieu frm=new frmDMChatLieu();
            frm.ShowDialog();
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            frmDMHangHoa frm=new frmDMHangHoa();     
            frm.ShowDialog();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            frmDMNhanVien frm=  new frmDMNhanVien();
            frm.ShowDialog();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            frmDMKhachHang frm=new frmDMKhachHang();
            frm.ShowDialog();
        }

        private void mnuHoaDonBan_Click(object sender, EventArgs e)
        {
            frmHoaDonBanHang frm= new frmHoaDonBanHang();
            frm.ShowDialog();      
        }

        private void mnuFindHoaDon_Click(object sender, EventArgs e)
        {
            frmTimKiemHoaDon frm = new frmTimKiemHoaDon();
            frm.ShowDialog();
        }

        private void frmDangNhap_Click(object sender, EventArgs e)
        {
            FormDangNhap frm = new FormDangNhap();
            frm.ShowDialog();
        }
    }
}
