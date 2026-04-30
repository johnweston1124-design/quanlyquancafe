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
    public partial class frmRevenueReport : Form
    {
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
