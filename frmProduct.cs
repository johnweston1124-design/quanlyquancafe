using System;
using System.Data;
using System.Windows.Forms;
using quanlyquancafe.BLL;
using quanlyquancafe.DTO;

namespace quanlyquancafe.GUI
{
    public partial class frmProduct : Form
    {
        private ProductBLL productBLL = new ProductBLL();

        public frmProduct()
        {
            InitializeComponent();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            LoadCategoryComboBox();
            LoadProductData();
            ResetForm();
        }

        private void LoadCategoryComboBox()
        {
            DataTable dt = productBLL.GetCategories();
            cboCategory.DataSource = dt;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryId";
            cboCategory.SelectedIndex = -1;
        }

        private void LoadProductData()
        {
            dgvProduct.DataSource = productBLL.GetAllProducts();

            if (dgvProduct.Columns.Count > 0)
            {
                if (dgvProduct.Columns.Contains("ProductId"))
                    dgvProduct.Columns["ProductId"].HeaderText = "Mã SP";

                if (dgvProduct.Columns.Contains("ProductName"))
                    dgvProduct.Columns["ProductName"].HeaderText = "Tên sản phẩm";

                if (dgvProduct.Columns.Contains("Price"))
                    dgvProduct.Columns["Price"].HeaderText = "Giá";

                if (dgvProduct.Columns.Contains("Description"))
                    dgvProduct.Columns["Description"].HeaderText = "Mô tả";

                if (dgvProduct.Columns.Contains("Unit"))
                    dgvProduct.Columns["Unit"].HeaderText = "Đơn vị";

                if (dgvProduct.Columns.Contains("Image"))
                    dgvProduct.Columns["Image"].HeaderText = "Hình";

                if (dgvProduct.Columns.Contains("Status"))
                    dgvProduct.Columns["Status"].HeaderText = "Trạng thái";

                if (dgvProduct.Columns.Contains("CategoryName"))
                    dgvProduct.Columns["CategoryName"].HeaderText = "Danh mục";
            }
        }

        private void ResetForm()
        {
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "0";
            txtDescription.Text = "";
            txtUnit.Text = "";
            txtImage.Text = "";
            txtKeyword.Text = "";
            chkStatus.Checked = true;
            cboCategory.SelectedIndex = -1;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadProductData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvProduct.DataSource = productBLL.SearchProducts(txtKeyword.Text.Trim());
        }

        private ProductDTO BuildProductFromForm()
        {
            decimal price;
            decimal.TryParse(txtPrice.Text.Trim(), out price);

            ProductDTO product = new ProductDTO();
            product.ProductName = txtProductName.Text.Trim();
            product.Price = price;
            product.Description = txtDescription.Text.Trim();
            product.Unit = txtUnit.Text.Trim();
            product.Image = txtImage.Text.Trim();
            product.Status = chkStatus.Checked;

            if (cboCategory.SelectedValue != null)
                product.CategoryId = Convert.ToInt32(cboCategory.SelectedValue);

            if (!string.IsNullOrWhiteSpace(txtProductId.Text))
                product.ProductId = Convert.ToInt32(txtProductId.Text);

            return product;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductDTO product = BuildProductFromForm();
            bool result = productBLL.AddProduct(product);

            MessageBox.Show(result ? "Thêm sản phẩm thành công." : "Thêm sản phẩm thất bại.");

            if (result)
            {
                LoadProductData();
                ResetForm();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductId.Text))
            {
                MessageBox.Show("Chọn sản phẩm cần sửa.");
                return;
            }

            ProductDTO product = BuildProductFromForm();
            bool result = productBLL.UpdateProduct(product);

            MessageBox.Show(result ? "Cập nhật sản phẩm thành công." : "Cập nhật sản phẩm thất bại.");

            if (result)
            {
                LoadProductData();
                ResetForm();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int productId;
            if (!int.TryParse(txtProductId.Text.Trim(), out productId))
            {
                MessageBox.Show("Chọn sản phẩm cần xóa.");
                return;
            }

            if (MessageBox.Show("Xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            bool result = productBLL.DeleteProduct(productId);
            MessageBox.Show(result ? "Xóa sản phẩm thành công." : "Xóa sản phẩm thất bại.");

            if (result)
            {
                LoadProductData();
                ResetForm();
            }
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvProduct.Rows.Count == 0)
                return;

            DataGridViewRow row = dgvProduct.Rows[e.RowIndex];

            if (row.Cells["ProductId"] != null)
                txtProductId.Text = Convert.ToString(row.Cells["ProductId"].Value);

            if (row.Cells["ProductName"] != null)
                txtProductName.Text = Convert.ToString(row.Cells["ProductName"].Value);

            if (row.Cells["Price"] != null)
                txtPrice.Text = Convert.ToString(row.Cells["Price"].Value);

            if (row.Cells["Description"] != null)
                txtDescription.Text = Convert.ToString(row.Cells["Description"].Value);

            if (row.Cells["Unit"] != null)
                txtUnit.Text = Convert.ToString(row.Cells["Unit"].Value);

            if (row.Cells["Image"] != null)
                txtImage.Text = Convert.ToString(row.Cells["Image"].Value);

            if (row.Cells["Status"] != null && row.Cells["Status"].Value != DBNull.Value)
                chkStatus.Checked = Convert.ToBoolean(row.Cells["Status"].Value);

            if (row.Cells["CategoryId"] != null && row.Cells["CategoryId"].Value != DBNull.Value)
                cboCategory.SelectedValue = Convert.ToInt32(row.Cells["CategoryId"].Value);
        }
    }
}