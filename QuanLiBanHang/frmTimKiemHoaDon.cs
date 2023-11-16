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
    public partial class frmTimKiemHoaDon : Form
    {
        DataTable tbTKDB;
        public frmTimKiemHoaDon()
        {
            InitializeComponent();
        }

        private void frmTimKiemHoaDon_Load(object sender, EventArgs e)
        {
            ResetValues();
            dgvTKHoaDon.DataSource = null;

        }
        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaHD.Focus();
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHD.Text == "") && (txtThang.Text == "") && (txtNam.Text == "") &&
               (txtMaNhanVien.Text == "") && (txtMaKH.Text == "") &&
               (txtTongTien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tblHDBan WHERE 1=1";
            if (txtMaHD.Text != "")
                sql = sql + " AND MaHDBan Like N'%" + txtMaHD.Text + "%'";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH =" + txtThang.Text;
            if (txtNam.Text != "")
                sql = sql + " AND YEAR =" + txtNam.Text;
            if (txtMaNhanVien.Text != "")
                sql = sql + " AND MaNhanVien Like N'%" + txtMaNhanVien.Text + "%'";
            if (txtMaKH.Text != "")
                sql = sql + " AND MaKhach Like N'%" + txtMaKH.Text + "%'";
            if (txtTongTien.Text != "")
                sql = sql + " AND TongTien <=" + txtTongTien.Text;
            tbTKDB = Functions.GetDataToTable(sql);
            if (tbTKDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tbTKDB.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvTKHoaDon.DataSource = tbTKDB;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            dgvTKHoaDon.Columns[0].HeaderText = "Mã HĐB";
            dgvTKHoaDon.Columns[1].HeaderText = "Mã nhân viên";
            dgvTKHoaDon.Columns[2].HeaderText = "Ngày bán";
            dgvTKHoaDon.Columns[3].HeaderText = "Mã khách";
            dgvTKHoaDon.Columns[4].HeaderText = "Tổng tiền";
            dgvTKHoaDon.Columns[0].Width = 150;
            dgvTKHoaDon.Columns[1].Width = 100;
            dgvTKHoaDon.Columns[2].Width = 80;
            dgvTKHoaDon.Columns[3].Width = 80;
            dgvTKHoaDon.Columns[4].Width = 80;
            dgvTKHoaDon.AllowUserToAddRows = false;
            dgvTKHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {

            ResetValues();
            dgvTKHoaDon.DataSource = null;
        }

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void dgvTKHoaDon_DoubleClick(object sender, EventArgs e)
        {
            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mahd = dgvTKHoaDon.CurrentRow.Cells["MaHDBan"].Value.ToString();
                frmTimKiemHoaDon frm = new frmTimKiemHoaDon();
                frm.txtMaHD.Text = mahd;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Bạn có muốn thoát không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
