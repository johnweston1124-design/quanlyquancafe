using quanlyquancafe.BLL;
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
    public partial class frmAccount : Form
    {
        UserBLL userBLL = new UserBLL();
        public frmAccount()
        {
            InitializeComponent();
            FormatHelper.ConfigDataGridView(dgvDataAcc);
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            btnAdd.BackColor = ThemeHelper.PrimaryColor;
            btnEdit.BackColor = ThemeHelper.PrimaryColor;
            btnSave.BackColor = Color.ForestGreen;
            btnDelete.BackColor = Color.IndianRed;
            if (cboRole.Items.Count > 0) cboRole.SelectedIndex = 0;
            LoadData();
        }

        private void pnlInput_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadData()
        {
            dgvDataAcc.DataSource = userBLL.GetAll();
        }

        private void frmAccount_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadEmployeeCombo()
        {
            EmployeeBLL empBLL = new EmployeeBLL();
            cboEmployee.DataSource = empBLL.GetAll();

            cboEmployee.DisplayMember = "FullName";
            cboEmployee.ValueMember = "EmployeeId";
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
