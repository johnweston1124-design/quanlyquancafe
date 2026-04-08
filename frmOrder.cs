using CoffeeShop.DAL.Repositories;
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
        public frmOrder()
        {
            InitializeComponent();
            FormatHelper.ConfigDataGridView(dgvOrderDetail);
            lblTotal.Text = "TỔNG: 0 VNĐ";
            lblTotal.ForeColor = Color.Red;
            btnPayment.FlatStyle = FlatStyle.Flat;
            btnPayment.FlatAppearance.BorderSize = 0;
            btnPayment.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnPayment.ForeColor = Color.White;
            btnPayment.BackColor = Color.ForestGreen;
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
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
            dgvOrderDetail.DataSource = orderBll.GetDetails(currentOrderId);
        }

        private void flpProductList_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
