using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
=======
using System.Data;
using System.Windows.Forms;
using quanlyquancafe.BLL;
>>>>>>> feature/product

namespace quanlyquancafe.GUI
{
    public partial class frmRevenueReport : Form
    {
<<<<<<< HEAD
        public frmRevenueReport()
        {
            InitializeComponent();
            FormatHelper.ConfigDataGridView(dgvRevenue);
            btnExport.BackColor = Color.FromArgb(230, 126, 34);
            btnExport.ForeColor = Color.White;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.FlatAppearance.BorderSize = 0;
            cboMonth.Text = DateTime.Now.Month.ToString();
            cboYear.Text = DateTime.Now.Year.ToString();
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
        }
    }
}
=======
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
>>>>>>> feature/product
