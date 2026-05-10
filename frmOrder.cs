using quanlyquancafe.BLL;
using quanlyquancafe.DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace quanlyquancafe
{
    public partial class frmOrder : Form
    {
        private int selectedTableId;
        private int currentOrderId = -1;
        private OrderBLL orderBLL = new OrderBLL();
        private ProductBLL productBLL = new ProductBLL();

        public frmOrder(int tableId, string tableName)
        {
            InitializeComponent();
            FormatHelper.ConfigDataGridView(dgvOrderDetail);

            lblTotal.Text = "TỔNG: 0 VND";
            lblTotal.ForeColor = Color.Red;
            btnPayment.FlatStyle = FlatStyle.Flat;
            btnPayment.FlatAppearance.BorderSize = 0;
            btnPayment.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnPayment.ForeColor = Color.White;
            btnPayment.BackColor = Color.ForestGreen;
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;

            this.selectedTableId = tableId;
            lblTableSelected.Text = "Bàn: " + tableName;

            // Wire up events
            btnPayment.Click += btnPayment_Click;
            cboCategorySelect.SelectedIndexChanged += cboCategorySelect_SelectedIndexChanged;

            LoadCategories();
            EnsureOrder();
            LoadOrder();
        }

        private void EnsureOrder()
        {
            currentOrderId = orderBLL.GetActiveOrderIdByTable(selectedTableId);
            if (currentOrderId == -1)
            {
                currentOrderId = orderBLL.CreateOrder(selectedTableId);
            }
        }

        private void LoadCategories()
        {
            DataTable dt = productBLL.GetCategories();
            DataRow all = dt.NewRow();
            all["CategoryId"] = -1;
            all["CategoryName"] = "--- Tất cả ---";
            dt.Rows.InsertAt(all, 0);

            cboCategorySelect.DataSource = dt;
            cboCategorySelect.DisplayMember = "CategoryName";
            cboCategorySelect.ValueMember = "CategoryId";
            cboCategorySelect.SelectedIndex = 0;
        }

        private void LoadProducts(int categoryId = -1)
        {
            flpProductList.Controls.Clear();

            DataTable dt = categoryId == -1
                ? productBLL.GetAllProducts()
                : productBLL.SearchProducts("", categoryId);

            foreach (DataRow row in dt.Rows)
            {
                int productId = Convert.ToInt32(row["ProductId"]);
                string productName = row["ProductName"].ToString();
                decimal price = Convert.ToDecimal(row["Price"]);

                Button btn = new Button();
                btn.Text = $"{productName}\n{price:N0} đ";
                btn.Size = new Size(130, 70);
                btn.Margin = new Padding(5);
                btn.BackColor = ThemeHelper.PrimaryColor;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;
                btn.Tag = new object[] { productId, price };

                btn.Click += (s, e) =>
                {
                    var tag = (object[])((Button)s).Tag;
                    int pid = (int)tag[0];
                    decimal unitPrice = (decimal)tag[1];

                    orderBLL.AddItem(currentOrderId, pid, 1, unitPrice);
                    LoadOrder();
                };

                flpProductList.Controls.Add(btn);
            }
        }

        private void LoadOrder()
        {
            dgvOrderDetail.DataSource = null;
            dgvOrderDetail.Columns.Clear();
            dgvOrderDetail.AutoGenerateColumns = true;

            if (currentOrderId == -1)
            {
                lblTotal.Text = "TỔNG: 0 VND";
                return;
            }

            DataTable dt = orderBLL.GetOrderDetails(currentOrderId);

            if (dt == null || dt.Rows.Count == 0)
            {
                lblTotal.Text = "TỔNG: 0 VND";
                return;
            }

            dgvOrderDetail.DataSource = dt;

            if (dgvOrderDetail.Columns.Contains("OrderDetailId"))
                dgvOrderDetail.Columns["OrderDetailId"].Visible = false;
            if (dgvOrderDetail.Columns.Contains("ProductName"))
                dgvOrderDetail.Columns["ProductName"].HeaderText = "Tên món";
            if (dgvOrderDetail.Columns.Contains("Quantity"))
                dgvOrderDetail.Columns["Quantity"].HeaderText = "Số lượng";
            if (dgvOrderDetail.Columns.Contains("UnitPrice"))
                dgvOrderDetail.Columns["UnitPrice"].HeaderText = "Đơn giá";
            if (dgvOrderDetail.Columns.Contains("LineTotal"))
                dgvOrderDetail.Columns["LineTotal"].HeaderText = "Thành tiền";

            decimal total = orderBLL.GetTotal(currentOrderId);
            lblTotal.Text = "TỔNG: " + total.ToString("N0") + " VND";
        }

        private void cboCategorySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int categoryId = -1;
            if (cboCategorySelect.SelectedValue is int val)
                categoryId = val;
            LoadProducts(categoryId);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (currentOrderId == -1)
            {
                MessageBox.Show("Không có order để thanh toán.");
                return;
            }

            decimal total = orderBLL.GetTotal(currentOrderId);
            if (total == 0)
            {
                MessageBox.Show("Chưa có món nào trong order.");
                return;
            }

            if (MessageBox.Show(
                $"Thanh toán {total:N0} VND?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                orderBLL.Pay(currentOrderId);
                MessageBox.Show("Thanh toán thành công!");
                currentOrderId = -1;
                dgvOrderDetail.DataSource = null;
                lblTotal.Text = "TỔNG: 0 VND";
            }
        }
        private void frmOrder_Load(object sender, EventArgs e) { }
        private void dgvOrderDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvOrderDetail.Rows[e.RowIndex];

            // Lấy OrderDetailId từ cột ẩn
            if (!dgvOrderDetail.Columns.Contains("OrderDetailId")) return;

            int orderDetailId = Convert.ToInt32(row.Cells["OrderDetailId"].Value);
            string productName = row.Cells["ProductName"].Value?.ToString();

            if (MessageBox.Show(
                $"Xóa món '{productName}' khỏi order?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                orderBLL.DeleteItem(orderDetailId);
                LoadOrder();
            }
        }
        private void flpProductList_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void lblTotal_Click(object sender, EventArgs e) { }
        private void pnlHeader_Paint(object sender, PaintEventArgs e) { }
    }
}


