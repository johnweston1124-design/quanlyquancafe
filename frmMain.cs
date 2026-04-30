using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using quanlyquancafe.BLL;
using quanlyquancafe.DTO;

namespace quanlyquancafe
{
    public partial class frmMain : Form
    {
        private int selectedTableId = -1;
        private string selectedTableName = "";
        private Form currentChildForm;
        private UserDTO currentUser;

        public frmMain()
        {
            InitializeComponent();
            pnlSidebar.BackColor = ThemeHelper.SidebarColor;
            pnlHeader.BackColor = ThemeHelper.PrimaryColor;
            pnlContent.BackColor = ThemeHelper.BackgroundColor;
        }

        public frmMain(UserDTO user)
        {
            InitializeComponent();
            pnlSidebar.BackColor = ThemeHelper.SidebarColor;
            pnlHeader.BackColor = ThemeHelper.PrimaryColor;
            pnlContent.BackColor = ThemeHelper.BackgroundColor;
            currentUser = user;
        }

        public void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmProduct());
        }
        private void btnCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmCategory());
        }
        private void btnTable_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmTable());
        }
        // File: quanlyquancafe.frmMain.cs
        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (selectedTableId != -1)
            {
                // Now providing both required parameters: int and string
                frmOrder orderForm = new frmOrder(selectedTableId, selectedTableName);
                OpenChildForm(orderForm);
            }
            else
            {
                MessageBox.Show("Please select a table from the Table list first!", "Warning");
            }
        }
        private void btnAccount_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmAccount());
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmEmployee());
        }
        private void btnInvoiceHistory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmInvoiceHistory());
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        public void SetSelectedTable(int tableId, string tableName) // Update this
        {
            selectedTableId = tableId;
            selectedTableName = tableName;
        }
        private void btnRevenueReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.frmRevenueReport());
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
