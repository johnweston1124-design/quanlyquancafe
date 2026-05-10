using quanlyquancafe.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace quanlyquancafe

{
    public partial class frmTable : Form
    {
        private TableBLL tableBLL = new TableBLL();

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

            txtTableName.Enter += (s, e) => {
                if (txtTableName.Text == "Nhập tên bàn (Vd: Bàn 01)...")
                {
                    txtTableName.Text = "";
                    txtTableName.ForeColor = Color.Black;
                }
            };
            txtTableName.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtTableName.Text))
                {
                    txtTableName.Text = "Nhập tên bàn (Vd: Bàn 01)...";
                    txtTableName.ForeColor = Color.Gray;
                }
            };
            LoadData();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            dgvTable.DataSource = tableBLL.GetAll();
        }

        private void frmTable_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvTable.Rows[e.RowIndex];
                int id = Convert.ToInt32(row.Cells["colID"].Value);
                string name = row.Cells["colName"].Value?.ToString();
                int capacity = Convert.ToInt32(row.Cells["colCapacity"].Value);
                string status = row.Cells["colStatus"].Value?.ToString();
                bool active = Convert.ToBoolean(row.Cells["IsActive"].Value);
                string zone = row.Cells["colZone"].Value?.ToString();

                if (this.TopLevelControl is frmMain mainForm)
                {
                    mainForm.SetSelectedTable(id, name);
                }

                // Enable buttons now that something is selected
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
    }
}
