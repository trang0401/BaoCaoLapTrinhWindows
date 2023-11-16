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
    public partial class frmDMKhachHang : Form
    {
        DataTable tblKH;
        public frmDMKhachHang()
        {
            InitializeComponent();
        }

        private void frmDMKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKhachHang.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * from tblKhach";
            tblKH = Functions.GetDataToTable(sql); //Lấy dữ liệu từ bảng
            dgvDMKhachHang.DataSource = tblKH; //Hiển thị vào dataGridView
            dgvDMKhachHang.Columns[0].HeaderText = "Mã khách";
            dgvDMKhachHang.Columns[1].HeaderText = "Tên khách";
            dgvDMKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dgvDMKhachHang.Columns[3].HeaderText = "Điện thoại";
            dgvDMKhachHang.Columns[0].Width = 100;
            dgvDMKhachHang.Columns[1].Width = 150;
            dgvDMKhachHang.Columns[2].Width = 150;
            dgvDMKhachHang.Columns[3].Width = 150;
            dgvDMKhachHang.AllowUserToAddRows = false;
            dgvDMKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void ResetValues()
        {
            txtMaKhachHang.Text = "";
            txtTenKhachHang.Text = "";
            txtDiaChi.Text = "";
            mtbDienThoai.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaKhachHang.Enabled = true;
            txtMaKhachHang.Focus();
        }

        private void dgvDMKhachHang_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhachHang.Focus();
                return;
            }
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaKhachHang.Text = dgvDMKhachHang.CurrentRow.Cells["MaKhach"].Value.ToString();
            txtTenKhachHang.Text = dgvDMKhachHang.CurrentRow.Cells["TenKhach"].Value.ToString();
            txtDiaChi.Text = dgvDMKhachHang.CurrentRow.Cells["DiaChi"].Value.ToString();
            mtbDienThoai.Text = dgvDMKhachHang.CurrentRow.Cells["DienThoai"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Mã khách không được để trống ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhachHang.Focus();
                return;
            }
            if (txtTenKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Tên khách không được để trống ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKhachHang.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Địa chỉ không được để trống ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (mtbDienThoai.Text == "(  )   -    ")
            {
                MessageBox.Show("Điện thoại không được để trống ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoai.Focus();
                return;
            }

            sql = "SELECT MaKhach FROM tblKhach WHERE MaKhach=N'" + txtMaKhachHang.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhachHang.Focus();
                return;
            }

            sql = "INSERT INTO tblKhach VALUES (N'" + txtMaKhachHang.Text.Trim() +
                "',N'" + txtTenKhachHang.Text.Trim() + "',N'" + txtDiaChi.Text.Trim() +
                "','" + mtbDienThoai.Text + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();

            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaKhachHang.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            {
                string sql;
                if (tblKH.Rows.Count == 0)
                {
                    MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtMaKhachHang.Text == "")
                {
                    MessageBox.Show("Bạn phải chọn nội dung cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtTenKhachHang.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Tên khách không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenKhachHang.Focus();
                    return;
                }
                if (txtDiaChi.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Địa chỉ không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDiaChi.Focus();
                    return;
                }
                if (mtbDienThoai.Text == "(  )   -    ")
                {
                    MessageBox.Show("Điện thoại không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtbDienThoai.Focus();
                    return;
                }
                sql = "UPDATE tblKhach SET TenKhach=N'" + txtTenKhachHang.Text.Trim().ToString() + "',DiaChi=N'" +
                    txtDiaChi.Text.Trim().ToString() + "',DienThoai='" + mtbDienThoai.Text.ToString() +
                    "' WHERE MaKhach=N'" + txtMaKhachHang.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
                btnBoQua.Enabled = false;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhachHang.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn nội dung nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá nội dung này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblKhach WHERE MaKhach=N'" + txtMaKhachHang.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            

                ResetValues();
                btnBoQua.Enabled = false;
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                txtMaKhachHang.Enabled = false;
            
           
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
