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
            };
            txtTableName.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtTableName.Text))
                {
                    txtTableName.Text = "Nhập tên bàn (Vd: Bàn 01)...";
                    txtTableName.ForeColor = Color.Gray;
                }
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

                // Use the designer column name
            int tableId = Convert.ToInt32(dgvTable.Rows[e.RowIndex].Cells["colID"].Value);

            frmMain main = this.ParentForm as frmMain;
            if (main != null)
            {
                main.SetSelectedTable(tableId);
            }
        }
    }
}
