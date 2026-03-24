using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.GUI
{
    public partial class frmCategory : Form
    {
        public frmCategory()
        {
            InitializeComponent();
            if (dgvData != null)
            {
                FormatHelper.ConfigDataGridView(dgvData);
            }
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            btnAdd.BackColor = ThemeHelper.PrimaryColor;
            btnEdit.BackColor = ThemeHelper.PrimaryColor;
            btnSave.BackColor = Color.ForestGreen;
            btnDelete.BackColor = Color.IndianRed;
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtCategoryID.BorderStyle = BorderStyle.FixedSingle;
            txtCategoryName.BorderStyle = BorderStyle.FixedSingle;
            SetupPlaceholder();
        }
        private void SetupPlaceholder()
        {
            txtCategoryName.Text = "Nhập tên danh mục...";
            txtCategoryName.ForeColor = Color.Gray;

            txtCategoryName.Enter += (s, e) => {
                if (txtCategoryName.Text == "Nhập tên danh mục...")
                {
                    txtCategoryName.Text = "";
                    txtCategoryName.ForeColor = Color.Black;
                }
            };
            txtCategoryName.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
                {
                    txtCategoryName.Text = "Nhập tên danh mục...";
                    txtCategoryName.ForeColor = Color.Gray;
                }
            };
        }
    }
}
