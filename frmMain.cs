using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmMain : Form
    {
        private Form currentChildForm;
        public frmMain()
        {
            InitializeComponent();
            pnlSidebar.BackColor = ThemeHelper.SidebarColor;
            pnlHeader.BackColor = ThemeHelper.PrimaryColor;
            pnlContent.BackColor = ThemeHelper.BackgroundColor;
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
            OpenChildForm(new GUI.frmProduct());
        }
        private void btnCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.frmCategory());
        }
        private void btnTable_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.frmTable());
        }
        private void btnOrder_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.frmOrder());
        }
        private void btnAccount_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.frmAccount());
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.frmEmployee());
        }
        private void btnInvoiceHistory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GUI.frmInvoiceHistory());
        }


    }
}
