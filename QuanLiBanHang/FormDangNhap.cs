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
using System.Configuration;

namespace QuanLiBanHang
{
    public partial class FormDangNhap : Form
    {
        public static string UserName = "";
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void cbShow_CheckedChanged(object sender, EventArgs e)
        {
           
                if (cbShow.Checked)
                {
                    txtPass.PasswordChar = (char)0;
                }
                else
                {
                    txtPass.PasswordChar = '*';
                }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
                Application.Exit();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                //  string sql;
                ///   conn.Open();
                //  SqlCommand cmd = new SqlCommand();
                //  cmd.CommandType = CommandType.StoredProcedure;
                // cmd.CommandText = "SP_AuthoLogin";
                // cmd.Parameters.AddWithValue("@UserName", txtTK.Text);
                // cmd.Parameters.AddWithValue("@Password", txtPass.Text);
                //  cmd.Connection = conn;
                // UserName = txtTK.Text;
                // object kq = cmd.ExecuteScalar();
                // int code = Convert.ToInt32(kq);
                string taikhoan = txtTK.Text.Trim();
                string matkhau = txtPass.Text.Trim();

             
               
                if (txtPass.Text!="12345")
                {
                    MessageBox.Show("mật khẩu không đúng ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (txtTK.Text != "Admin")
                {
                    MessageBox.Show("tài khoản không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                Form form = new FromMain();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtTK_MouseClick(object sender, MouseEventArgs e)
        {
            txtTK.SelectAll();
        }

        private void txtPass_MouseClick(object sender, MouseEventArgs e)
        {
            txtPass.SelectAll();
        }
    }
}
