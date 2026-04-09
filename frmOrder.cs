 
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

namespace quanlyquancafe
{
    public partial class frmOrder : Form
    {
        private int selectedTableId;
        private string selectedTableName;
        private OrderBLL orderBLL = new OrderBLL();
        public frmOrder(int tableId, string tableName)
        {
            InitializeComponent();
            FormatHelper.ConfigDataGridView(dgvOrderDetail);
            lblTotal.ForeColor = Color.Red;
            btnPayment.FlatStyle = FlatStyle.Flat;
            btnPayment.FlatAppearance.BorderSize = 0;
            btnPayment.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnPayment.ForeColor = Color.White;
            btnPayment.BackColor = Color.ForestGreen;
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            this.selectedTableId = tableId;
            lblTableSelected.Text = "Bàn: " + tableName;
            LoadOrder();
        }


        private void frmOrder_Load(object sender, EventArgs e)
        {
        }

        private void dgvOrderDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void flpProductList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }


        private void LoadOrder()
        {
            dgvOrderDetail.DataSource = null;
            dgvOrderDetail.Columns.Clear();

            int orderId = orderBLL.GetActiveOrderIdByTable(selectedTableId);

            if (orderId != -1)
            {
                DataTable dt = orderBLL.GetOrderDetails(orderId);
                dgvOrderDetail.AutoGenerateColumns = true;
                dgvOrderDetail.DataSource = dt;

                dgvOrderDetail.Columns["ProductName"].HeaderText = "Tên món";
                dgvOrderDetail.Columns["Quantity"].HeaderText = "Số lượng";
                dgvOrderDetail.Columns["UnitPrice"].HeaderText = "Đơn giá";
                // If OrderStatus is there and you want to hide it:
                if (dgvOrderDetail.Columns.Contains("OrderStatus"))
                    dgvOrderDetail.Columns["OrderStatus"].Visible = false;
                if (dgvOrderDetail.Columns.Contains("LineTotal"))
                    dgvOrderDetail.Columns["LineTotal"].Visible = false;

                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    // We do the math here because 'Thành tiền' isn't in your SQL query
                    decimal price = Convert.ToDecimal(row["UnitPrice"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);

                    total += price * quantity;
                }

                lblTotal.Text = "TỔNG: " + total.ToString("N0") + " VND";
            }
            else
            {
                lblTotal.Text = "TỔNG: 0 VND";
            }
        }
    }
}
