<<<<<<< HEAD
﻿using quanlyquancafe;
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
    public partial class frmCategory : Form
    {
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
            SetupPlaceholder();
            LoadData();
        }
        private void SetupPlaceholder()
        {
            txtCategoryName.Text = "Nhập tên danh mục...";
            txtCategoryName.ForeColor = Color.Gray;

            txtCategoryName.Enter += (s, e) => {
                if (txtCategoryName.Text == "Nhập tên danh mục...")
                {
                    txtCategoryName.Text = "";
                    txtCategoryName.ForeColor = Color.Black;
                }
            };
            txtCategoryName.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
                {
                    txtCategoryName.Text = "Nhập tên danh mục...";
                    txtCategoryName.ForeColor = Color.Gray;
                }
            };
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadData()
        {// hoặc GetAll()
=======
﻿using System;
using System.Data;
using System.Windows.Forms;
using quanlyquancafe.BLL;

namespace quanlyquancafe.GUI
{
    public partial class frmCategory : Form
    {
        private CategoryBLL categoryBLL = new CategoryBLL();

        public frmCategory()
        {
            InitializeComponent();
>>>>>>> feature/product
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            LoadData();
        }
    }
}
=======
            LoadCategoryData();
        }

        private void LoadCategoryData()
        {
            DataTable dt = categoryBLL.GetAllCategories();
            dgvCategory.DataSource = dt;

            if (dgvCategory.Columns.Count > 0)
            {
                if (dgvCategory.Columns.Contains("CategoryId"))
                    dgvCategory.Columns["CategoryId"].HeaderText = "Mã danh mục";

                if (dgvCategory.Columns.Contains("CategoryName"))
                    dgvCategory.Columns["CategoryName"].HeaderText = "Tên danh mục";

                if (dgvCategory.Columns.Contains("Description"))
                    dgvCategory.Columns["Description"].HeaderText = "Mô tả";

                if (dgvCategory.Columns.Contains("IsActive"))
                    dgvCategory.Columns["IsActive"].HeaderText = "Hoạt động";

                if (dgvCategory.Columns.Contains("CreatedAt"))
                    dgvCategory.Columns["CreatedAt"].HeaderText = "Ngày tạo";

                if (dgvCategory.Columns.Contains("UpdatedAt"))
                    dgvCategory.Columns["UpdatedAt"].HeaderText = "Ngày cập nhật";
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadCategoryData();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            using (frmEmployee f = new frmEmployee())
            {
                f.ShowDialog();
            }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            using (frmProduct f = new frmProduct())
            {
                f.ShowDialog();
            }
        }

        private void btnRevenueReport_Click(object sender, EventArgs e)
        {
            using (frmRevenueReport f = new frmRevenueReport())
            {
                f.ShowDialog();
            }
        }

        private void btnInvoiceHistory_Click(object sender, EventArgs e)
        {
            using (frmInvoiceHistory f = new frmInvoiceHistory())
            {
                f.ShowDialog();
            }
        }
    }
}
>>>>>>> feature/product
