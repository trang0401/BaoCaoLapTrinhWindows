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
    public partial class frmDMChatLieu : Form
    {
        DataTable tbcl;
        public frmDMChatLieu()
        {
            InitializeComponent();
        }

        private void frmDMChatLieu_Load(object sender, EventArgs e)
        {
            txtMaChatLieu.Enabled = false;
            btnLuu.Enabled= false;
            btnBoQua.Enabled= false;
            LoaDataGridView();
           
        }
        private void LoaDataGridView()
        {
            string sql;
            sql = "SELECT MaChatLieu, TenChatLieu FROM tblChatLieu";
            tbcl = Functions.GetDataToTable(sql); 
            dgvChatLieu.DataSource = tbcl;           
            dgvChatLieu.Columns[0].HeaderText = "Mã chất liệu";
            dgvChatLieu.Columns[1].HeaderText = "Tên chất liệu";
            dgvChatLieu.Columns[0].Width = 100;
            dgvChatLieu.Columns[1].Width = 300;
            dgvChatLieu.AllowUserToAddRows = false; 
            dgvChatLieu.EditMode = DataGridViewEditMode.EditProgrammatically; 
        }

        private void dgvChatLieu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled==false)
            {
                MessageBox.Show("Thêm Mới !", "Thông báo! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaChatLieu.Focus();
                return;
            }
            if (tbcl.Rows.Count==0)
            {
                MessageBox.Show("Không có dữ liệu  !", "Thông báo! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaChatLieu.Text = dgvChatLieu.CurrentRow.Cells["MaChatLieu"].Value.ToString();
            txtTenChatLieu.Text = dgvChatLieu.CurrentRow.Cells["TenChatLieu"].Value.ToString();
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
            ResetValue(); 
            txtMaChatLieu.Enabled = true; 
            txtMaChatLieu.Focus();
        }
        private void ResetValue() //xoa trang cac textbox
        {
            txtMaChatLieu.Text = "";
            txtTenChatLieu.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
                string sql; //luu sql
                if (txtMaChatLieu.Text.Trim ().Length == 0) 
                {
                    MessageBox.Show("Mã chất liệu không được để trống ", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaChatLieu.Focus();
                    return;
                }
                if (txtTenChatLieu.Text.Trim().Length == 0) 
                {
                    MessageBox.Show("Tên chất liệu không được để trống", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenChatLieu.Focus();
                    return;
                }
                sql = "Select MaChatLieu From tblChatLieu where MaChatLieu=N'" + txtMaChatLieu.Text.Trim() + "'";
                if (Functions.CheckKey(sql))
                {
                    MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaChatLieu.Focus();
                    return;
                }

                sql = "INSERT INTO tblChatLieu VALUES = ( N'" +
                txtMaChatLieu.Text + "',N'" + txtTenChatLieu.Text + "')";
                Functions.RunSQL(sql); //chay sql
                LoaDataGridView(); //nhap lai dgv
                ResetValue();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnBoQua.Enabled = false;
                btnLuu.Enabled = false;
                txtMaChatLieu.Enabled = false;
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; 
            if (tbcl.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaChatLieu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn thông tin nào", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenChatLieu.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tblChatLieu SET TenChatLieu=N'" +
                txtTenChatLieu.Text.ToString() +
                "' WHERE MaChatLieu=N'" + txtMaChatLieu.Text + "'";
            Class.Functions.RunSQL(sql);
            LoaDataGridView();
            ResetValue();

            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
                string sql;
            if (tbcl.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaChatLieu.Text == "") 
            {
                MessageBox.Show("Bạn chưa chọn thông tin nào!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblChatLieu WHERE MaChatLieu=N'" + txtMaChatLieu.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoaDataGridView();
                ResetValue();
            }
           
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            try
            {

                ResetValue();
                btnBoQua.Enabled = false;
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                txtMaChatLieu.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        private void txtMaChatLieu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)// dung enter thay tab 
                SendKeys.Send("{TAB}");
        }
    }
    
}

