namespace quanlyquancafe
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnEmployee = new System.Windows.Forms.Button();
            this.BtnTable = new System.Windows.Forms.Button();
            this.RevenueReport = new System.Windows.Forms.Button();
            this.btnInvoiceHistory = new System.Windows.Forms.Button();
            this.btnCategory = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.btnAccount = new System.Windows.Forms.Button();
            this.btnProduct = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.Controls.Add(this.btnEmployee);
            this.pnlSidebar.Controls.Add(this.BtnTable);
            this.pnlSidebar.Controls.Add(this.RevenueReport);
            this.pnlSidebar.Controls.Add(this.btnInvoiceHistory);
            this.pnlSidebar.Controls.Add(this.btnCategory);
            this.pnlSidebar.Controls.Add(this.btnOrder);
            this.pnlSidebar.Controls.Add(this.btnAccount);
            this.pnlSidebar.Controls.Add(this.btnProduct);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(267, 554);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnEmployee
            // 
            this.btnEmployee.Location = new System.Drawing.Point(124, 173);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Size = new System.Drawing.Size(100, 62);
            this.btnEmployee.TabIndex = 0;
            this.btnEmployee.Text = "Employee";
            this.btnEmployee.UseVisualStyleBackColor = true;
            this.btnEmployee.Click += new System.EventHandler(this.btnEmployee_Click);
            // 
            // BtnTable
            // 
            this.BtnTable.Location = new System.Drawing.Point(124, 101);
            this.BtnTable.Name = "BtnTable";
            this.BtnTable.Size = new System.Drawing.Size(100, 62);
            this.BtnTable.TabIndex = 0;
            this.BtnTable.Text = "Table";
            this.BtnTable.UseVisualStyleBackColor = true;
            this.BtnTable.Click += new System.EventHandler(this.btnTable_Click);
            // 
            // RevenueReport
            // 
            this.RevenueReport.Location = new System.Drawing.Point(16, 241);
            this.RevenueReport.Name = "RevenueReport";
            this.RevenueReport.Size = new System.Drawing.Size(100, 62);
            this.RevenueReport.TabIndex = 0;
            this.RevenueReport.Text = "Revenue Report";
            this.RevenueReport.UseVisualStyleBackColor = true;
            this.RevenueReport.Click += new System.EventHandler(this.btnRevenueReport_Click);
            // 
            // btnInvoiceHistory
            // 
            this.btnInvoiceHistory.Location = new System.Drawing.Point(124, 241);
            this.btnInvoiceHistory.Name = "btnInvoiceHistory";
            this.btnInvoiceHistory.Size = new System.Drawing.Size(100, 62);
            this.btnInvoiceHistory.TabIndex = 0;
            this.btnInvoiceHistory.Text = "Invoice History ";
            this.btnInvoiceHistory.UseVisualStyleBackColor = true;
            this.btnInvoiceHistory.Click += new System.EventHandler(this.btnInvoiceHistory_Click);
            // 
            // btnCategory
            // 
            this.btnCategory.Location = new System.Drawing.Point(124, 27);
            this.btnCategory.Margin = new System.Windows.Forms.Padding(4);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(100, 62);
            this.btnCategory.TabIndex = 0;
            this.btnCategory.Text = "Category";
            this.btnCategory.UseVisualStyleBackColor = true;
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(16, 173);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(100, 62);
            this.btnOrder.TabIndex = 0;
            this.btnOrder.Text = "Order";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.Location = new System.Drawing.Point(16, 101);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(100, 62);
            this.btnAccount.TabIndex = 0;
            this.btnAccount.Text = "Account";
            this.btnAccount.UseVisualStyleBackColor = true;
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // btnProduct
            // 
            this.btnProduct.Location = new System.Drawing.Point(16, 27);
            this.btnProduct.Margin = new System.Windows.Forms.Padding(4);
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Size = new System.Drawing.Size(100, 62);
            this.btnProduct.TabIndex = 0;
            this.btnProduct.Text = "Product";
            this.btnProduct.UseVisualStyleBackColor = true;
            this.btnProduct.Click += new System.EventHandler(this.btnProduct_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.SystemColors.Window;
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(267, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 74);
            this.pnlHeader.TabIndex = 1;
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(267, 74);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(800, 480);
            this.pnlContent.TabIndex = 2;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnProduct;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.Button btnEmployee;
        private System.Windows.Forms.Button BtnTable;
        private System.Windows.Forms.Button btnInvoiceHistory;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Button btnAccount;
        private System.Windows.Forms.Button RevenueReport;
    }
}

