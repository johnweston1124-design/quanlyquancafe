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
    public partial class frmAccount : Form
    {
        public frmAccount()
        {
            InitializeComponent();
            FormatHelper.ConfigDataGridView(dgvData);
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            btnAdd.BackColor = ThemeHelper.PrimaryColor;
            btnEdit.BackColor = ThemeHelper.PrimaryColor;
            btnSave.BackColor = Color.ForestGreen;
            btnDelete.BackColor = Color.IndianRed;
            if (cboRole.Items.Count > 0) cboRole.SelectedIndex = 0;
        }
    }
}
