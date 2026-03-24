using CoffeeShop.DAL.Repositories;
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
{
    public partial class frmTable : Form
    {
        public frmTable()
        {
            InitializeComponent();
            FormatHelper.ConfigDataGridView(dgvData);
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
            var repo = new ProductRepository();
            dgvData.DataSource = repo.GetAllAvailable(); // hoặc GetAll()
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
    }
}
