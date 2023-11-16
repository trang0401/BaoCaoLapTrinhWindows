using QuanLiBanHang.Class;
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
using System.Data.SqlClient;
using QuanLiBanHang.Class;


namespace QuanLiBanHang
{
    public partial class frmDMHangHoa : Form
    {
        DataTable tblH;
        public frmDMHangHoa()
        {
            InitializeComponent();

        }

        private void frmDMHangHoa_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * from tblChatLieu";
            txtMaHang.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            Functions.FillCombo (sql, cbMaChatLieu, "MaChatLieu", "TenChatLieu");
            cbMaChatLieu.SelectedIndex = -1;
            ResetValues();
        }
       
        private void ResetValues()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cbMaChatLieu.Text = "";
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
            txtAnh.Text = "";
            picAnh.Image = null;
            txtGhiChu.Text = "";
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * from tblHang";
            tblH = Functions.GetDataToTable(sql);
            dgvDMHangHoa.DataSource = tblH;
            dgvDMHangHoa.Columns[0].HeaderText = "Mã hàng";
            dgvDMHangHoa.Columns[1].HeaderText = "Tên hàng";
            dgvDMHangHoa.Columns[2].HeaderText = "Chất liệu";
            dgvDMHangHoa.Columns[3].HeaderText = "Số lượng";
            dgvDMHangHoa.Columns[4].HeaderText = "Đơn giá nhập";
            dgvDMHangHoa.Columns[5].HeaderText = "Đơn giá bán";
            dgvDMHangHoa.Columns[6].HeaderText = "Ảnh";
            dgvDMHangHoa.Columns[7].HeaderText = "Ghi chú";
            dgvDMHangHoa.Columns[0].Width = 80;
            dgvDMHangHoa.Columns[1].Width = 140;
            dgvDMHangHoa.Columns[2].Width = 80;
            dgvDMHangHoa.Columns[3].Width = 80;
            dgvDMHangHoa.Columns[4].Width = 100;
            dgvDMHangHoa.Columns[5].Width = 100;
            dgvDMHangHoa.Columns[6].Width = 200;
            dgvDMHangHoa.Columns[7].Width = 300;
            dgvDMHangHoa.AllowUserToAddRows = false;
            dgvDMHangHoa.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        
        private void dgvDMHangHoa_Click(object sender, EventArgs e)
        {
            string MaChatLieu;
            string sql;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (tblH.Rows.Count == 0)
            {
                MessageBox.Show("Dữ liệu không tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaHang.Text = dgvDMHangHoa.CurrentRow.Cells["MaHang"].Value.ToString();
            txtTenHang.Text = dgvDMHangHoa.CurrentRow.Cells["TenHang"].Value.ToString();
            MaChatLieu = dgvDMHangHoa.CurrentRow.Cells["MaChatLieu"].Value.ToString();
            sql = "SELECT TenChatLieu FROM tblChatLieu WHERE MaChatLieu=N'" + MaChatLieu + "'";
            cbMaChatLieu.Text = Functions.GetFieldValues(sql);
            txtSoLuong.Text = dgvDMHangHoa.CurrentRow.Cells["SoLuong"].Value.ToString();
           /// txtDonGiaNhap.Text = dgvDMHangHoa.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
           /// txtDonGiaBan.Text = dgvDMHangHoa.CurrentRow.Cells["DonGiaBan"].Value.ToString();
            sql = "SELECT Anh FROM tblHang WHERE MaHang=N'" + txtMaHang.Text + "'";
            txtAnh.Text = Functions.GetFieldValues(sql);
            picAnh.Image = Image.FromFile(txtAnh.Text);
            sql = "SELECT GhiChu FROM tblHang WHERE MaHang = N'" + txtMaHang.Text + "'";
            txtGhiChu.Text = Functions.GetFieldValues(sql);
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            txtMaHang.Enabled = true;
            txtMaHang.Focus();
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
            ResetValues();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblH.Rows.Count == 0)
            {
                MessageBox.Show("Dữ liệu không được bỏ trống! ", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn nội dung nào", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Tên hàng không được bỏ trống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if (cbMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chất liệu không được bỏ trống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaChatLieu.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Ảnh minh hoạ cho hàng không được bỏ trống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAnh.Focus();
                return;
            }
            sql = "UPDATE tblHang SET TenHang=N'" + txtTenHang.Text.Trim().ToString() +
                "',MaChatLieu=N'" + cbMaChatLieu.SelectedValue.ToString() +
                "',SoLuong=" + txtSoLuong.Text +
                ",Anh='" + txtAnh.Text + "',GhiChu=N'" + txtGhiChu.Text + "' WHERE MaHang=N'" + txtMaHang.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn nội dung nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá nội dung này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblHang WHERE MaHang=N'" + txtMaHang.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Mã hàng không được bỏ trống! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;// kiem tra mh co trong hay k 
            }
            if (txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Tên hàng không được bỏ trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if (cbMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Chất liệu không được bỏ trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaChatLieu.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Ảnh minh hoạ cho hàng không được bỏ trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnMo.Focus();
                return;
            }
            sql = "SELECT MaHang FROM tblHang WHERE MaHang=N'" + txtMaHang.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã tồn tại?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            sql = "INSERT INTO tblHang(MaHang,TenHang,MaChatLieu,SoLuong,DonGiaNhap, DonGiaBan,Anh,GhiChu) VALUES(N'"
                + txtMaHang.Text.Trim() + "',N'" + txtTenHang.Text.Trim() +
                "',N'" + cbMaChatLieu.SelectedValue.ToString() +
                "'," + txtSoLuong.Text.Trim() + "," + txtDonGiaNhap.Text +
                "," + txtDonGiaBan.Text + ",'" + txtAnh.Text + "',N'" + txtGhiChu.Text.Trim() + "')";

            Functions.RunSQL(sql);
            LoadDataGridView();           
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaHang.Enabled = false;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
        }
        private void btnMo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHang.Text == "") && (txtTenHang.Text == "") && (cbMaChatLieu.Text == ""))
            {
                MessageBox.Show("Nhập sản phẩm cần tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tblHang WHERE 1=1";
            if (txtMaHang.Text != "")
                sql += " AND MaHang LIKE N'%" + txtMaHang.Text + "%'";
            if (txtTenHang.Text != "")
                sql += " AND TenHang LIKE N'%" + txtTenHang.Text + "%'";
            if (cbMaChatLieu.Text != "")
                sql += " AND MaChatLieu LIKE N'%" + cbMaChatLieu.SelectedValue + "%'";
            tblH = Functions.GetDataToTable(sql);
            if (tblH.Rows.Count == 0)
                MessageBox.Show("Không có nội dung tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblH.Rows.Count + "  nội dung thoả mãn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDMHangHoa.DataSource = tblH;
            ResetValues();
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MaHang,TenHang,MaChatLieu,SoLuong,DonGiaNhap,DonGiaBan,Anh,Ghichu FROM tblHang";
            tblH = Functions.GetDataToTable(sql);
            dgvDMHangHoa.DataSource = tblH;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaHang.Enabled = false;
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