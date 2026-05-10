using System;
using System.Data;
using System.Data.SqlClient;

namespace quanlyquancafe.DAL
{
    public class OrderDAL
    {
        public int CreateOrder(int tableId, int employeeId = 2)
        {
            if (tableId <= 0)
                throw new ArgumentException("Invalid tableId", nameof(tableId));

            string checkQuery = "SELECT COUNT(1) FROM CafeTables WHERE TableId = @TableId";

            object exists = DataProvider.ExecuteScalar(
                checkQuery,
                new SqlParameter[]
                {
                    new SqlParameter("@TableId", tableId)
                }
            );

            if (Convert.ToInt32(exists) == 0)
                throw new InvalidOperationException($"TableId {tableId} does not exist in CafeTables.");

            string query = @"
                INSERT INTO Orders(TableId, EmployeeId, OrderStatus, CreatedAt, TotalAmount, FinalAmount)
                OUTPUT INSERTED.OrderId
                VALUES (@TableId, @EmployeeId, N'Pending', GETDATE(), 0, 0)";

            object result = DataProvider.ExecuteScalar(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@TableId", tableId),
                    new SqlParameter("@EmployeeId", employeeId)
                }
            );

            return Convert.ToInt32(result);
        }

        public void AddItem(int orderId, int productId, int quantity, decimal unitPrice)
        {
            string query = @"
                INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
                VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal)";

            DataProvider.ExecuteNonQuery(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@OrderId", orderId),
                    new SqlParameter("@ProductId", productId),
                    new SqlParameter("@Quantity", quantity),
                    new SqlParameter("@UnitPrice", unitPrice),
                    new SqlParameter("@LineTotal", quantity * unitPrice)
                }
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

            return DataProvider.ExecuteQuery(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@OrderId", orderId)
                }
            );
        }

        public decimal GetTotal(int orderId)
        {
            string query = @"
                SELECT ISNULL(SUM(LineTotal), 0)
                FROM OrderDetails
                WHERE OrderId = @OrderId";

            object result = DataProvider.ExecuteScalar(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@OrderId", orderId)
                }
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

            DataProvider.ExecuteNonQuery(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@OrderId", orderId)
                }
            );
        }

        public int GetUnpaidOrder(int tableId)
        {
            string query = @"
                SELECT TOP 1 OrderId 
                FROM Orders 
                WHERE TableId = @tableId AND OrderStatus = N'Pending'
            ";

            object result = DataProvider.ExecuteScalar(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@tableId", tableId)
                }
            );

            if (result != null && result != DBNull.Value)
                return Convert.ToInt32(result);

            return -1;
        }

        public int GetActiveOrderId(int tableId)
        {
            string query = @"
                SELECT OrderId 
                FROM Orders 
                WHERE TableId = @tableId 
                AND OrderStatus = N'Pending'
            ";

            DataTable dt = DataProvider.ExecuteQuery(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@tableId", tableId)
                }
            );

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["OrderId"]);
            }

            return -1;
        }

        public void DeleteItem(int orderDetailId)
        {
            string query = "DELETE FROM OrderDetails WHERE OrderDetailId = @Id";

            DataProvider.ExecuteNonQuery(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@Id", orderDetailId)
                }
            );
        }
    }
}