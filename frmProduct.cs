using quanlyquancafe.BLL;
using quanlyquancafe.DAL;
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
    public partial class frmProduct : Form
    {
        private ProductBLL productBLL = new ProductBLL();

        public frmProduct()
        {
            this.Load += frmProduct_Load;
            InitializeComponent();
            FormatHelper.ConfigDataGridView(dgvData);
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            btnAdd.BackColor = ThemeHelper.PrimaryColor;
            btnEdit.BackColor = ThemeHelper.PrimaryColor;
            btnSave.BackColor = ThemeHelper.PrimaryColor;
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            LoadProductData();
            ResetForm();
        }

        private void LoadProductData()
        {
            dgvData.AutoGenerateColumns = true;
            dgvData.Columns.Clear();

            dgvData.DataSource = productBLL.GetAllProducts();

            if (dgvData.Columns.Contains("ProductId"))
                dgvData.Columns["ProductId"].HeaderText = "Mã SP";
            if (dgvData.Columns.Contains("ProductName"))
                dgvData.Columns["ProductName"].HeaderText = "Tên SP";
            if (dgvData.Columns.Contains("Price"))
                dgvData.Columns["Price"].HeaderText = "Đơn giá";
            if (dgvData.Columns.Contains("Unit"))
                dgvData.Columns["Unit"].HeaderText = "Đơn vị";
            if (dgvData.Columns.Contains("CategoryName"))
                dgvData.Columns["CategoryName"].HeaderText = "Danh mục";

            if (dgvData.Columns.Contains("Description"))
                dgvData.Columns["Description"].Visible = false;
            if (dgvData.Columns.Contains("Image"))
                dgvData.Columns["Image"].Visible = false;
            if (dgvData.Columns.Contains("Status"))
                dgvData.Columns["Status"].Visible = false;
            if (dgvData.Columns.Contains("CreatedAt"))
                dgvData.Columns["CreatedAt"].Visible = false;
            if (dgvData.Columns.Contains("UpdatedAt"))
                dgvData.Columns["UpdatedAt"].Visible = false;
            if (dgvData.Columns.Contains("IsActive"))
                dgvData.Columns["IsActive"].Visible = false;
            if (dgvData.Columns.Contains("CategoryId"))
                dgvData.Columns["CategoryId"].Visible = false;
        }

        private void ExecuteSearch()
        {
            string keyword = txtSearch.Text.Trim();
            dgvData.DataSource = productBLL.SearchProducts(keyword);
        }

        private int _selectedProductId = 0;

        private void ResetForm()
        {
            _selectedProductId = 0;
            txtProductName.Text = "";
            txtPrice.Text = "0";
            txtQuantity.Text = "0";
            txtSearch.Text = "";
            txtProductName.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductDTO product = BuildProductFromForm();

            bool result = productBLL.AddProduct(product);

            MessageBox.Show(
                result
                ? "Thêm sản phẩm thành công."
                : "Thêm sản phẩm thất bại."
            );

            if (result)
            {
                LoadProductData();
                ResetForm();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedProductId == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật.");
                return;
            }

            ProductDTO product = BuildProductFromForm();

            bool result = productBLL.UpdateProduct(product);

            MessageBox.Show(
                result
                ? "Cập nhật sản phẩm thành công."
                : "Cập nhật sản phẩm thất bại."
            );

            if (result)
            {
                LoadProductData();
                ResetForm();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedProductId == 0)
            {
                MessageBox.Show("Chọn sản phẩm cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Xóa sản phẩm này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) == DialogResult.No)
            {
                return;
            }

            bool result = productBLL.DeleteProduct(_selectedProductId);

            MessageBox.Show(
                result
                ? "Xóa sản phẩm thành công."
                : "Xóa sản phẩm thất bại."
            );

            if (result)
            {
                LoadProductData();
                ResetForm();
            }
        }

        private void pnlControl_Paint(object sender, PaintEventArgs e)
        {
        }

        private ProductDTO BuildProductFromForm()
        {
            decimal price = 0;
            decimal.TryParse(txtPrice.Text.Trim(), out price);

            ProductDTO product = new ProductDTO();

            product.ProductId = _selectedProductId;
            product.ProductName = txtProductName.Text.Trim();
            product.Price = price;

            product.Description = "";
            product.Unit = "Ly";
            product.Image = "";

            product.Status = true;

            product.IsActive = true;
            product.CategoryId = 1;

            return product;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvData.Rows[e.RowIndex];

            _selectedProductId = Convert.ToInt32(row.Cells["ProductId"].Value);

            txtProductName.Text = row.Cells["ProductName"].Value.ToString();
            txtPrice.Text = row.Cells["Price"].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvData.DataSource = productBLL.SearchProducts(txtSearch.Text.Trim());
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}