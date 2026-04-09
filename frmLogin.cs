using quanlyquancafe;
using quanlyquancafe.BLL;
using quanlyquancafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlyquancafe
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            btnLogin.BackColor = ThemeHelper.PrimaryColor;
        }

        private UserBLL _bll = new UserBLL();

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new UserLoginDTO
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };

            var user = _bll.Login(dto);

            if (user != null)
            {
                this.Hide();
                if (user.Role == "Admin")
                    new frmMain(user).Show();       // Admin: full quyền
                else
                    new frmMain(user).Show();       // Staff: giới hạn quyền (làm sau)
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }

        private void btnExit_TextChanged(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
