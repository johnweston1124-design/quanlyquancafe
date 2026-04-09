using System;
using System.Data;
using System.Windows.Forms;
using quanlyquancafe.BLL;

namespace quanlyquancafe.GUI
{
    public partial class frmEmployee : Form
    {
        private EmployeeBLL employeeBLL = new EmployeeBLL();

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            cboGender.Items.Clear();
            cboGender.Items.Add("Nam");
            cboGender.Items.Add("Nữ");
            cboGender.Items.Add("Khác");
            cboGender.SelectedIndex = 0;

            LoadEmployeeData();
            ResetForm();
        }

        private void LoadEmployeeData()
        {
            dgvEmployee.DataSource = employeeBLL.GetAllEmployees();

            if (dgvEmployee.Columns.Count > 0)
            {
                if (dgvEmployee.Columns.Contains("EmployeeId"))
                    dgvEmployee.Columns["EmployeeId"].HeaderText = "Mã NV";

                if (dgvEmployee.Columns.Contains("FullName"))
                    dgvEmployee.Columns["FullName"].HeaderText = "Họ tên";

                if (dgvEmployee.Columns.Contains("Gender"))
                    dgvEmployee.Columns["Gender"].HeaderText = "Giới tính";

                if (dgvEmployee.Columns.Contains("DateOfBirth"))
                    dgvEmployee.Columns["DateOfBirth"].HeaderText = "Ngày sinh";

                if (dgvEmployee.Columns.Contains("Phone"))
                    dgvEmployee.Columns["Phone"].HeaderText = "Số điện thoại";

                if (dgvEmployee.Columns.Contains("Address"))
                    dgvEmployee.Columns["Address"].HeaderText = "Địa chỉ";

                if (dgvEmployee.Columns.Contains("HireDate"))
                    dgvEmployee.Columns["HireDate"].HeaderText = "Ngày vào làm";

                if (dgvEmployee.Columns.Contains("Salary"))
                    dgvEmployee.Columns["Salary"].HeaderText = "Lương";
            }
        }

        private void ResetForm()
        {
            txtEmployeeId.Text = "";
            txtFullName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtSalary.Text = "0";
            txtKeyword.Text = "";
            cboGender.SelectedIndex = 0;
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-18);
            dtpHireDate.Value = DateTime.Now;
            txtFullName.Focus();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvEmployee.DataSource = employeeBLL.SearchEmployees(txtKeyword.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            decimal salary;
            if (!decimal.TryParse(txtSalary.Text.Trim(), out salary))
            {
                MessageBox.Show("Lương không hợp lệ.");
                return;
            }

            string message;
            bool result = employeeBLL.AddEmployee(
                txtFullName.Text.Trim(),
                cboGender.Text,
                dtpDateOfBirth.Value,
                txtPhone.Text.Trim(),
                txtAddress.Text.Trim(),
                dtpHireDate.Value,
                salary,
                out message
            );

            MessageBox.Show(message);

            if (result)
            {
                LoadEmployeeData();
                ResetForm();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int employeeId;
            decimal salary;

            if (!int.TryParse(txtEmployeeId.Text.Trim(), out employeeId))
            {
                MessageBox.Show("Mã nhân viên không hợp lệ.");
                return;
            }

            if (!decimal.TryParse(txtSalary.Text.Trim(), out salary))
            {
                MessageBox.Show("Lương không hợp lệ.");
                return;
            }

            string message;
            bool result = employeeBLL.UpdateEmployee(
                employeeId,
                txtFullName.Text.Trim(),
                cboGender.Text,
                dtpDateOfBirth.Value,
                txtPhone.Text.Trim(),
                txtAddress.Text.Trim(),
                dtpHireDate.Value,
                salary,
                out message
            );

            MessageBox.Show(message);

            if (result)
            {
                LoadEmployeeData();
                ResetForm();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int employeeId;
            if (!int.TryParse(txtEmployeeId.Text.Trim(), out employeeId))
            {
                MessageBox.Show("Chọn nhân viên cần xóa.");
                return;
            }

            if (MessageBox.Show("Xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string message;
            bool result = employeeBLL.DeleteEmployee(employeeId, out message);
            MessageBox.Show(message);

            if (result)
            {
                LoadEmployeeData();
                ResetForm();
            }
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvEmployee.Rows.Count == 0)
                return;

            DataGridViewRow row = dgvEmployee.Rows[e.RowIndex];

            if (row.Cells["EmployeeId"] != null)
                txtEmployeeId.Text = Convert.ToString(row.Cells["EmployeeId"].Value);

            if (row.Cells["FullName"] != null)
                txtFullName.Text = Convert.ToString(row.Cells["FullName"].Value);

            if (row.Cells["Gender"] != null)
                cboGender.Text = Convert.ToString(row.Cells["Gender"].Value);

            if (row.Cells["DateOfBirth"] != null && row.Cells["DateOfBirth"].Value != DBNull.Value)
                dtpDateOfBirth.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);

            if (row.Cells["Phone"] != null)
                txtPhone.Text = Convert.ToString(row.Cells["Phone"].Value);

            if (row.Cells["Address"] != null)
                txtAddress.Text = Convert.ToString(row.Cells["Address"].Value);

            if (row.Cells["HireDate"] != null && row.Cells["HireDate"].Value != DBNull.Value)
                dtpHireDate.Value = Convert.ToDateTime(row.Cells["HireDate"].Value);

            if (row.Cells["Salary"] != null)
                txtSalary.Text = Convert.ToString(row.Cells["Salary"].Value);
        }
    }
}