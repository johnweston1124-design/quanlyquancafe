namespace WindowsFormsApp1.GUI
{
    partial class frmOrder
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblTableSelected = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.dgvOrderDetail = new System.Windows.Forms.DataGridView();
            this.cboCategorySelect = new System.Windows.Forms.ComboBox();
            this.flpProductList = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnPayment = new System.Windows.Forms.Button();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetail)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.dgvOrderDetail);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 60);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(500, 390);
            this.pnlLeft.TabIndex = 1;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.panel1);
            this.pnlRight.Controls.Add(this.flpProductList);
            this.pnlRight.Controls.Add(this.cboCategorySelect);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(500, 60);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(300, 390);
            this.pnlRight.TabIndex = 0;
            // 
            // lblTableSelected
            // 
            this.lblTableSelected.AutoSize = true;
            this.lblTableSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableSelected.Location = new System.Drawing.Point(640, 9);
            this.lblTableSelected.Name = "lblTableSelected";
            this.lblTableSelected.Size = new System.Drawing.Size(133, 20);
            this.lblTableSelected.TabIndex = 0;
            this.lblTableSelected.Text = "Bàn: (Chưa chọn)";
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(436, 60);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "GỌI MÓN  VÀ THANH TOÁN";
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTableSelected);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 60);
            this.pnlHeader.TabIndex = 2;
            // 
            // dgvOrderDetail
            // 
            this.dgvOrderDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetail.Location = new System.Drawing.Point(0, 0);
            this.dgvOrderDetail.Name = "dgvOrderDetail";
            this.dgvOrderDetail.ReadOnly = true;
            this.dgvOrderDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderDetail.Size = new System.Drawing.Size(500, 390);
            this.dgvOrderDetail.TabIndex = 0;
            // 
            // cboCategorySelect
            // 
            this.cboCategorySelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboCategorySelect.FormattingEnabled = true;
            this.cboCategorySelect.Location = new System.Drawing.Point(0, 0);
            this.cboCategorySelect.Name = "cboCategorySelect";
            this.cboCategorySelect.Size = new System.Drawing.Size(300, 21);
            this.cboCategorySelect.TabIndex = 0;
            // 
            // flpProductList
            // 
            this.flpProductList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpProductList.Location = new System.Drawing.Point(0, 21);
            this.flpProductList.Name = "flpProductList";
            this.flpProductList.Size = new System.Drawing.Size(300, 369);
            this.flpProductList.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPayment);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 180);
            this.panel1.TabIndex = 0;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTotal.Location = new System.Drawing.Point(6, 14);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(142, 31);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Tổng Giá:";
            // 
            // btnPayment
            // 
            this.btnPayment.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnPayment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPayment.Location = new System.Drawing.Point(0, 120);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(300, 60);
            this.btnPayment.TabIndex = 1;
            this.btnPayment.Text = "THANH TOÁN";
            this.btnPayment.UseVisualStyleBackColor = false;
            // 
            // frmOrder
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmOrder";
            this.Text = "frmOrder";
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetail)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblTableSelected; // FIX
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.DataGridView dgvOrderDetail;
        private System.Windows.Forms.ComboBox cboCategorySelect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.FlowLayoutPanel flpProductList;
    }
}