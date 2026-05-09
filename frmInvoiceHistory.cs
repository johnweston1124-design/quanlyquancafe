<<<<<<< HEAD
﻿ 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace  quanlyquancafe
=======
﻿using System;
using System.Windows.Forms;

namespace quanlyquancafe.GUI
>>>>>>> feature/product
{
    public partial class frmInvoiceHistory : Form
    {
        public frmInvoiceHistory()
        {
            InitializeComponent();
<<<<<<< HEAD
            if (dgvInvoices != null)
            {
                FormatHelper.ConfigDataGridView(dgvInvoices);
            }
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            btnFilter.BackColor = ThemeHelper.PrimaryColor;
            btnFilter.ForeColor = Color.White;
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.FlatAppearance.BorderSize = 0;
            btnPrintInvoice.BackColor = ThemeHelper.PrimaryColor;
            btnPrintInvoice.ForeColor = Color.White;
            btnPrintInvoice.FlatStyle = FlatStyle.Flat;
            btnPrintInvoice.FlatAppearance.BorderSize = 0;
            dtpToDate.Value = DateTime.Now;
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            LoadData();
        }

        private void pnlBottom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadData()
        {
            // hoặc GetAll()
=======
>>>>>>> feature/product
        }

        private void frmInvoiceHistory_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            LoadData();
        }

        private void dgvInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
=======
            lblInfo.Text = "Form lịch sử hóa đơn chưa nối BLL/DAL.\r\nMày có thể gắn tiếp sau.";
        }
    }
}
>>>>>>> feature/product
