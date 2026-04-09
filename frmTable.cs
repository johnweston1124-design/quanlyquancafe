using CafeManager;
using quanlyquancafe.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CafeManager
{
    public partial class frmTable : Form
    {
        TableBLL tableBLL = new TableBLL();

        public frmTable()
        {
            InitializeComponent();
        }

        private void frmTable_Load(object sender, EventArgs e)
        {
            LoadTables();
        }

        void LoadTables()
        {
            flowLayoutPanel1.Controls.Clear();
            DataTable dt = tableBLL.GetTables();

            foreach (DataRow row in dt.Rows)
            {
                Button btn = new Button();
                btn.Width = 120;
                btn.Height = 80;

                btn.Text = row["TableName"].ToString();

                string status = row["Status"].ToString();

                if (status == "Empty")
                    btn.BackColor = Color.LightGreen;
                else
                    btn.BackColor = Color.OrangeRed;
                btn.Tag = row["TableID"];
                btn.Click += Btn_Click;

                flowLayoutPanel1.Controls.Add(btn);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmTable_Load1(object sender, EventArgs e)
        {
            LoadTables();
        }
        void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int tableId = (int)btn.Tag;

            frmOrder f = new frmOrder(tableId);
            f.ShowDialog();

            LoadTables();
        }
    }
}