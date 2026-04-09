using CafeManager;
using CafeManager.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlyquancafe.BLL;

namespace  quanlyquancafe
{
    public partial class frmTable : Form
    {
        TableBLL tableBLL = new TableBLL();

        public frmTable()
        {
            InitializeComponent();
            FormatHelper.ConfigDataGridView(dgvTable);
            lblTitle.ForeColor = ThemeHelper.PrimaryColor;
            btnAdd.BackColor = ThemeHelper.PrimaryColor;
            btnEdit.BackColor = ThemeHelper.PrimaryColor;
            btnSave.BackColor = Color.ForestGreen;
            btnDelete.BackColor = Color.IndianRed;
            cboStatus.SelectedIndex = 0;
            txtTableName.Text = "Nhập tên bàn (Vd: Bàn 01)...";
            txtTableName.ForeColor = Color.Gray;
            LoadData();
            txtTableName.Enter += (s, e) => {
                if (txtTableName.Text == "Nhập tên bàn (Vd: Bàn 01)...")
                {
                    txtTableName.Text = "";
                    txtTableName.ForeColor = Color.Black;
        }
        {
        }
            };
        }

        {
            flowLayoutPanel1.Controls.Clear();
            DataTable dt = tableBLL.GetTables();

            {
                Button btn = new Button();
                btn.Width = 120;
                btn.Height = 80;

                btn.Text = row["TableName"].ToString();

                string status = row["Status"].ToString();


            }

        private void frmTable_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        {

        }

        {
        }
        {


        }
    }
}