using quanlyquancafe.BLL;
using quanlyquancafe.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlyquancafe.GUI
{
    public partial class frmEmployee : Form
    {
        private EmployeeBLL employeeBLL = new EmployeeBLL();
        private int selectedEmployeeId = -1;
        public frmEmployee()
        {
            this.Load += frmEmployee_Load;
            InitializeComponent();
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            txtEmployeeName.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            StyleButton(btnAdd, ThemeHelper.PrimaryColor);
            StyleButton(btnEdit, ThemeHelper.PrimaryColor);
            StyleButton(btnSave, Color.ForestGreen);
            StyleButton(btnDelete, Color.IndianRed);
            if (dgvData != null)
            {
                FormatHelper.ConfigDataGridView(dgvData);
            }
        }

        private void ResetForm()
        {
            txtEmployeeName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();

            selectedEmployeeId = -1;

            dgvData.ClearSelection();

            txtEmployeeName.Focus();
        }
        private void StyleButton(Button btn, Color color)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            dgvData.DataSource = employeeBLL.GetAll();

            dgvData.Columns["EmployeeId"].HeaderText = "Mã NV";
            dgvData.Columns["FullName"].HeaderText = "Họ và tên";
            dgvData.Columns["Phone"].HeaderText = "Số điện thoại";
            dgvData.Columns["HireDate"].HeaderText = "Ngày vào làm";

            // Ẩn cột không cần
            dgvData.Columns["Gender"].Visible = false;
            dgvData.Columns["DateOfBirth"].Visible = false;
            dgvData.Columns["Address"].Visible = false;
            dgvData.Columns["Salary"].Visible = false;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string message;

            EmployeeBLL employeeBLL = new EmployeeBLL();

            bool result = employeeBLL.AddEmployee(
                txtEmployeeName.Text.Trim(),
                txtPhone.Text.Trim(),
                txtAddress.Text.Trim(),
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
            if (selectedEmployeeId == -1)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa nhân viên này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
                return;

            string message;

            bool success = employeeBLL.DeleteEmployee(selectedEmployeeId, out message);

            MessageBox.Show(message);

            if (success)
            {
                LoadEmployeeData();
                ResetForm();
                selectedEmployeeId = -1;
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvData.Rows[e.RowIndex];

            selectedEmployeeId = Convert.ToInt32(row.Cells["EmployeeId"].Value);

            txtEmployeeName.Text = row.Cells["FullName"].Value.ToString();
            txtPhone.Text = row.Cells["Phone"].Value.ToString();
            txtAddress.Text = row.Cells["Address"].Value.ToString();
        }   
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grpEmployeeInfo_Enter(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvData.DataSource;

            foreach (DataRow row in dt.Rows)
            {
                string message;

                // ADD
                if (row.RowState == DataRowState.Added)
                {
                    employeeBLL.AddEmployee(
                        row["FullName"].ToString(),
                        row["Phone"].ToString(),
                        row["Address"].ToString(),
                        out message
                    );
                }

                // UPDATE
                else if (row.RowState == DataRowState.Modified)
                {
                    employeeBLL.UpdateEmployee(
                        Convert.ToInt32(row["EmployeeId"]),
                        row["FullName"].ToString(),
                        row["Phone"].ToString(),
                        row["Address"].ToString(),
                        out message
                    );
                }
            }

            MessageBox.Show("Lưu thành công");

            LoadEmployeeData();
            ResetForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeId == -1)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.");
                return;
            }

            string message;

            EmployeeBLL employeeBLL = new EmployeeBLL();

            bool result = employeeBLL.UpdateEmployee(
                selectedEmployeeId,
                txtEmployeeName.Text.Trim(),
                txtPhone.Text.Trim(),
                txtAddress.Text.Trim(),
                out message
            );

            MessageBox.Show(message);

            if (result)
            {
                LoadEmployeeData();
                ResetForm();
            }
        }
    }
}
