 
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
		private int _tableId;
		private int _orderId;
		OrderBLL orderBLL = new OrderBLL();
        public frmOrder()
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
		}

		public frmOrder(int tableId)
		{
			InitializeComponent();
			_tableId = tableId;
		}

		private void frmOrder_Load(object sender, EventArgs e)
        {
			_orderId = orderBLL.GetUnpaidOrder(_tableId);

			if (_orderId == -1)
			{
				_orderId = orderBLL.CreateOrder(_tableId);
			}

			LoadData();
        }

        private void dgvOrderDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void LoadData()
        {
            dgvOrderDetail.AutoGenerateColumns = false;

            BLL.OrderBLL orderBll = new BLL.OrderBLL();
            int currentOrderId = 1;
            dgvOrderDetail.DataSource = orderBll.GetOrderDetails(currentOrderId);
            UpdateTotal();
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

        private void UpdateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvOrderDetail.Rows)
            {
                if (row.IsNewRow) continue;

                // dùng Giá * Số lượng (vì bạn đang hiển thị 2 cột này)
                decimal gia = Convert.ToDecimal(row.Cells["Column3"].Value);
                int sl = Convert.ToInt32(row.Cells["Column2"].Value);

                total += gia * sl;
            }

            lblTotal.Text = "TỔNG: " + total.ToString("N0") + " VNĐ";
        }

		private void LoadOrder()
		{
			OrderBLL orderBLL = new OrderBLL();

			_orderId = orderBLL.GetUnpaidOrder(_tableId);

			if (_orderId == -1)
			{
				_orderId = orderBLL.CreateOrder(_tableId);
			}

			dgvOrderDetail.DataSource = orderBLL.GetOrderDetails(_orderId);

			UpdateTotal();
		}
	}
}
