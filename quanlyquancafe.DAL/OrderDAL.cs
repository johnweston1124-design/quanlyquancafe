using System;
using System.Data;
using System.Data.SqlClient;

namespace quanlyquancafe.DAL
{
    public class OrderDAL
    {
        // File: quanlyquancafe.DAL\OrderDAL.cs
        public int CreateOrder(int tableId, int employeeId = 2)
        {
            if (tableId <= 0)
                throw new ArgumentException("Invalid tableId", nameof(tableId));

            // Verify CafeTables contains this tableId
            string checkQuery = "SELECT COUNT(1) FROM CafeTables WHERE TableId = @TableId";
            object exists = DataProvider.Instance.ExecuteScalar(
                checkQuery,
                new SqlParameter("@TableId", tableId));
            if (Convert.ToInt32(exists) == 0)
                throw new InvalidOperationException($"TableId {tableId} does not exist in CafeTables.");

            string query = @"
        INSERT INTO Orders(TableId, EmployeeId, OrderStatus, CreatedAt, TotalAmount, FinalAmount)
        OUTPUT INSERTED.OrderId
        VALUES (@TableId, @EmployeeId, N'Pending', GETDATE(), 0, 0)";

            object result = DataProvider.Instance.ExecuteScalar(
                query,
                new SqlParameter("@TableId", tableId),
                new SqlParameter("@EmployeeId", employeeId)
            );

            return Convert.ToInt32(result);
        }

        public void AddItem(int orderId, int productId, int quantity, decimal unitPrice)
        {
            string query = @"
                INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
                VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal)";

            DataProvider.Instance.ExecuteNonQuery(
                query,
                new SqlParameter("@OrderId", orderId),
                new SqlParameter("@ProductId", productId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@UnitPrice", unitPrice),
                new SqlParameter("@LineTotal", quantity * unitPrice)
            );
        }

        public DataTable GetDetails(int orderId)
        {
            string query = @"
                SELECT 
                    p.ProductName,
                    d.Quantity,
                    d.UnitPrice,
                    d.LineTotal
                FROM OrderDetails d
                JOIN Products p ON d.ProductId = p.ProductId
                WHERE d.OrderId = @OrderId";

            return DataProvider.Instance.ExecuteQuery(
                query,
                new SqlParameter("@OrderId", orderId)
            );
        }

        public decimal GetTotal(int orderId)
        {
            string query = @"
                SELECT ISNULL(SUM(LineTotal), 0)
                FROM OrderDetails
                WHERE OrderId = @OrderId";

            object result = DataProvider.Instance.ExecuteScalar(
                query,
                new SqlParameter("@OrderId", orderId)
            );

            return Convert.ToDecimal(result);
        }

        public void Pay(int orderId)
        {
            string query = @"
        UPDATE Orders
        SET OrderStatus = N'Paid',
            PaidAt = GETDATE()
        WHERE OrderId = @OrderId";

            DataProvider.Instance.ExecuteNonQuery(
                query,
                new SqlParameter("@OrderId", orderId)
            );
        }

        public int GetUnpaidOrder(int tableId)
        {
            string query = @"
        SELECT TOP 1 OrderId 
        FROM Orders 
        WHERE TableId = @tableId AND OrderStatus = N'Pending'
    ";

            object result = DataProvider.Instance.ExecuteScalar(query,
                new SqlParameter("@tableId", tableId));

            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);

            return -1;
        }

        public int GetActiveOrderId(int tableId)
        {
            // We only want the ID of the order that is 'Pending'
            string query = "SELECT OrderId FROM Orders WHERE TableId = @tableId AND OrderStatus = N'Pending'";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new SqlParameter("@tableId", tableId));

            if (dt.Rows.Count > 0)
            {
                // Return the OrderId if found
                return Convert.ToInt32(dt.Rows[0]["OrderId"]);
            }

            // Return -1 if no pending order exists (table is empty)
            return -1;
        }

        public void DeleteItem(int orderDetailId)
        {
            string query = "DELETE FROM OrderDetails WHERE OrderDetailId = @Id";

            DataProvider.Instance.ExecuteNonQuery(
                query,
                new SqlParameter("@Id", orderDetailId)
            );
        }
    }
}