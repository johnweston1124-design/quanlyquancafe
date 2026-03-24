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
    public partial class frmEmployee : Form
    {
        public frmEmployee()
        {
            InitializeComponent();
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            txtEmployeeName.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtAddress.BorderStyle = BorderStyle.FixedSingle;
            StyleButton(btnAdd, ThemeHelper.PrimaryColor);
            StyleButton(btnEdit, ThemeHelper.PrimaryColor);
            StyleButton(btnSave, Color.ForestGreen);
            StyleButton(btnDelete, Color.IndianRed);
            if (dgvData != null)
            {
                FormatHelper.ConfigDataGridView(dgvData);
            }
        }
        private void StyleButton(Button btn, Color color)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        }
    }
}
