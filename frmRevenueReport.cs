using System;
using System.Data;
using System.Windows.Forms;
using quanlyquancafe.BLL;

namespace quanlyquancafe.GUI
{
    public partial class frmRevenueReport : Form
    {
        private ReportBLL reportBLL = new ReportBLL();

        public frmRevenueReport()
        {
            InitializeComponent();
        }

        private void frmRevenueReport_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Today.AddDays(-7);
            dtpToDate.Value = DateTime.Today;
            LoadRevenueReport();
        }

        private void LoadRevenueReport()
        {
            DataTable dt = reportBLL.GetRevenueByDate(dtpFromDate.Value.Date, dtpToDate.Value.Date.AddDays(1).AddSeconds(-1));
            dgvRevenue.DataSource = dt;

            decimal totalRevenue = reportBLL.GetTotalRevenue(dtpFromDate.Value.Date, dtpToDate.Value.Date.AddDays(1).AddSeconds(-1));
            lblTotalRevenueValue.Text = totalRevenue.ToString("N0") + " VNĐ";
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadRevenueReport();
        }
    }
}