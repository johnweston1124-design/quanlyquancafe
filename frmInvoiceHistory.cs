using System;
using System.Windows.Forms;

namespace quanlyquancafe.GUI
{
    public partial class frmInvoiceHistory : Form
    {
        public frmInvoiceHistory()
        {
            InitializeComponent();
        }

        private void frmInvoiceHistory_Load(object sender, EventArgs e)
        {
            lblInfo.Text = "Form lịch sử hóa đơn chưa nối BLL/DAL.\r\nMày có thể gắn tiếp sau.";
        }
    }
}