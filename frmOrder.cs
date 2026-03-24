using System;
using System.Data;
using System.Windows.Forms;
using CafeManager.BLL;

namespace CafeManager
{
    public partial class frmOrder : Form
    {
        int tableId;
        int orderId;
        OrderBLL orderBLL = new OrderBLL();
        private TextBox txtProductID;
        private Label label1;
        private TextBox txtQty;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnAdd;
        private Button btnPay;
        private DataGridView dataGridView1;
        private Label lblTotal;
        private TextBox txtPrice;
        private Button btnDelete;
        TableBLL tableBLL = new TableBLL();

        public frmOrder(int tableId)
        {
            InitializeComponent();
            this.tableId = tableId;
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            orderId = orderBLL.GetUnpaidOrder(tableId);

            if (orderId == -1)
            {
                orderId = orderBLL.CreateOrder(tableId);
            }

            tableBLL.SetOccupied(tableId);
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(txtProductID.Text);
            int qty = int.Parse(txtQty.Text);
            decimal price = decimal.Parse(txtPrice.Text);

            orderBLL.AddItem(orderId, productId, qty, price);
            LoadData();
        }

        void LoadData()
        {
            dataGridView1.DataSource = orderBLL.GetDetails(orderId);
            lblTotal.Text = "Tổng: " + orderBLL.GetTotal(orderId).ToString("N0");
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            orderBLL.Pay(orderId);
            tableBLL.SetEmpty(tableId);

            MessageBox.Show("Thanh toán thành công!");
            this.Close();
        }

        private void InitializeComponent()
        {
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnPay = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtProductID
            // 
            this.txtProductID.Location = new System.Drawing.Point(262, 27);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.Size = new System.Drawing.Size(100, 20);
            this.txtProductID.TabIndex = 1;
            this.txtProductID.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(386, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 3;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(262, 77);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(100, 20);
            this.txtQty.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mã món";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Số lượng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Giá";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(145, 171);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm món";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnPay
            // 
            this.btnPay.Location = new System.Drawing.Point(287, 171);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(75, 23);
            this.btnPay.TabIndex = 0;
            this.btnPay.Text = "Thanh toán";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(145, 200);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(217, 72);
            this.dataGridView1.TabIndex = 9;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(142, 275);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(44, 13);
            this.lblTotal.TabIndex = 10;
            this.lblTotal.Text = "Tổng: 0";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(262, 135);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(100, 20);
            this.txtPrice.TabIndex = 11;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(64, 171);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Xóa món";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmOrder
            // 
            this.ClientSize = new System.Drawing.Size(685, 323);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProductID);
            this.Name = "frmOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gọi món";
            this.Load += new System.EventHandler(this.frmOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }


        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int orderDetailId = Convert.ToInt32(
                    dataGridView1.CurrentRow.Cells["OrderDetailID"].Value);

                orderBLL.DeleteItem(orderDetailId);

                LoadData();
            }
            else
            {
                MessageBox.Show("Chọn món cần xóa!");
            }
        }
    }
}