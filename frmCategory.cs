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

namespace quanlyquancafe.GUI
{
    public partial class frmCategory : Form
    {
        // ✅ Khai báo categoryBLL
        private CategoryBLL categoryBLL = new CategoryBLL();

        // ✅ ID đang chọn (thay vì txtCategoryID readonly dùng để lưu)
        private int _selectedCategoryId = 0;
        public frmCategory()
        {
            InitializeComponent();
            if (dgvData != null)
            {
                FormatHelper.ConfigDataGridView(dgvData);
            }
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            btnAdd.BackColor = ThemeHelper.PrimaryColor;
            btnEdit.BackColor = ThemeHelper.PrimaryColor;
            btnSave.BackColor = Color.ForestGreen;
            btnDelete.BackColor = Color.IndianRed;
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtCategoryID.BorderStyle = BorderStyle.FixedSingle;
            txtCategoryName.BorderStyle = BorderStyle.FixedSingle;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnSave.Click += btnSave_Click;

            // Wire up load
            this.Load += frmCategory_Load;


            SetupPlaceholder();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            LoadCategoryData();
            ResetForm();
        }

        private void LoadCategoryData()
        {
            DataTable dt = categoryBLL.GetAllCategories();
            dgvData.DataSource = dt;

            if (dgvData.Columns.Contains("CategoryId"))
                dgvData.Columns["CategoryId"].HeaderText = "Mã danh mục";
            if (dgvData.Columns.Contains("CategoryName"))
                dgvData.Columns["CategoryName"].HeaderText = "Tên danh mục";
            if (dgvData.Columns.Contains("Description"))
                dgvData.Columns["Description"].Visible = false;
            if (dgvData.Columns.Contains("IsActive"))
                dgvData.Columns["IsActive"].Visible = false;
            if (dgvData.Columns.Contains("CreatedAt"))
                dgvData.Columns["CreatedAt"].Visible = false;
            if (dgvData.Columns.Contains("UpdatedAt"))
                dgvData.Columns["UpdatedAt"].Visible = false;
        }

        private void ResetForm()
        {
            _selectedCategoryId = 0;
            txtCategoryID.Text = "";
            txtCategoryName.Text = "Nhập tên danh mục...";
            txtCategoryName.ForeColor = Color.Gray;
        }

        private void SetupPlaceholder()
        {
            txtCategoryName.Text = "Nhập tên danh mục...";
            txtCategoryName.ForeColor = Color.Gray;

            txtCategoryName.Enter += (s, e) =>
            {
                if (txtCategoryName.Text == "Nhập tên danh mục...")
                {
                    txtCategoryName.Text = "";
                    txtCategoryName.ForeColor = Color.Black;
                }
            };

            txtCategoryName.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
                {
                    txtCategoryName.Text = "Nhập tên danh mục...";
                    txtCategoryName.ForeColor = Color.Gray;
                }
            };
        }

        private string GetCategoryNameInput()
        {
            string name = txtCategoryName.Text.Trim();
            if (name == "Nhập tên danh mục...") return "";
            return name;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = GetCategoryNameInput();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục.");
                return;
            }

            CategoryDTO category = new CategoryDTO
            {
                CategoryName = name,
                IsActive = true
            };

            bool result = categoryBLL.AddCategory(category);
            MessageBox.Show(result ? "Thêm danh mục thành công." : "Thêm danh mục thất bại.");

            if (result) { LoadCategoryData(); ResetForm(); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedCategoryId == 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa.");
                return;
            }

            string name = GetCategoryNameInput();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục.");
                return;
            }

            CategoryDTO category = new CategoryDTO
            {
                CategoryId = _selectedCategoryId,
                CategoryName = name,
                IsActive = true
            };

            bool result = categoryBLL.UpdateCategory(category);
            MessageBox.Show(result ? "Cập nhật thành công." : "Cập nhật thất bại.");

            if (result) { LoadCategoryData(); ResetForm(); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCategoryId == 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.");
                return;
            }

            if (MessageBox.Show("Xóa danh mục này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            bool result = categoryBLL.DeleteCategory(_selectedCategoryId);
            MessageBox.Show(result ? "Xóa thành công." : "Xóa thất bại.");

            if (result) { LoadCategoryData(); ResetForm(); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // SAVE = tìm kiếm hoặc refresh tùy logic của bạn
            LoadCategoryData();
        }
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvData.Rows[e.RowIndex];

            _selectedCategoryId = Convert.ToInt32(row.Cells["CategoryId"].Value);
            txtCategoryID.Text = _selectedCategoryId.ToString();
            txtCategoryName.Text = row.Cells["CategoryName"].Value.ToString();
            txtCategoryName.ForeColor = Color.Black;
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
