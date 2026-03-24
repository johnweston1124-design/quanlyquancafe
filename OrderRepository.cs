using System;
using System.Data;
using System.Data.SqlClient;

namespace quanlyquancafe.DAL
{
    public class OrderRepository
    {
        public int CreateOrder(int tableId, int employeeId, string notes = null)
        {
            if (tableId <= 0)
            {
                throw new ArgumentOutOfRangeException("tableId");
            }

            if (employeeId <= 0)
            {
                throw new ArgumentOutOfRangeException("employeeId");
            }

            return DataProvider.Instance.ExecuteInTransaction(delegate (SqlTransaction transaction)
            {
                const string checkTableSql = @"
SELECT COUNT(1)
FROM CafeTables
WHERE TableId = @TableId
  AND IsActive = 1;";

                int tableExists = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(
                    checkTableSql,
                    CommandType.Text,
                    transaction,
                    new SqlParameter("@TableId", SqlDbType.Int) { Value = tableId }));

                if (tableExists == 0)
                {
                    throw new Exception("Table not found or inactive.");
                }

                const string insertOrderSql = @"
INSERT INTO Orders
(
    TableId,
    EmployeeId,
    OrderStatus,
    CreatedAt,
    TotalAmount,
    DiscountAmount,
    VATAmount,
    SurchargeAmount,
    FinalAmount,
    Notes
)
VALUES
(
    @TableId,
    @EmployeeId,
    @OrderStatus,
    SYSDATETIME(),
    0,
    0,
    0,
    0,
    0,
    @Notes
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

                int orderId = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(
                    insertOrderSql,
                    CommandType.Text,
                    transaction,
                    new SqlParameter("@TableId", SqlDbType.Int) { Value = tableId },
                    new SqlParameter("@EmployeeId", SqlDbType.Int) { Value = employeeId },
                    new SqlParameter("@OrderStatus", SqlDbType.NVarChar, 20) { Value = "Pending" },
                    new SqlParameter("@Notes", SqlDbType.NVarChar, 500) { Value = (object)notes ?? DBNull.Value }));

                const string updateTableSql = @"
UPDATE CafeTables
SET Status = @Status
WHERE TableId = @TableId
  AND IsActive = 1;";

                int affectedRows = DataProvider.Instance.ExecuteNonQuery(
                    updateTableSql,
                    CommandType.Text,
                    transaction,
                    new SqlParameter("@Status", SqlDbType.NVarChar, 20) { Value = "Occupied" },
                    new SqlParameter("@TableId", SqlDbType.Int) { Value = tableId });

                if (affectedRows == 0)
                {
                    throw new Exception("Failed to update table status.");
                }

                return orderId;
            });
        }

        public int AddOrderDetail(int orderId, int productId, int quantity, string notes = null)
        {
            if (orderId <= 0)
            {
                throw new ArgumentOutOfRangeException("orderId");
            }

            if (productId <= 0)
            {
                throw new ArgumentOutOfRangeException("productId");
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException("quantity");
            }

            return DataProvider.Instance.ExecuteInTransaction(delegate (SqlTransaction transaction)
            {
                const string checkOrderSql = @"
SELECT COUNT(1)
FROM Orders
WHERE OrderId = @OrderId
  AND OrderStatus = @OrderStatus;";

                int orderExists = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(
                    checkOrderSql,
                    CommandType.Text,
                    transaction,
                    new SqlParameter("@OrderId", SqlDbType.Int) { Value = orderId },
                    new SqlParameter("@OrderStatus", SqlDbType.NVarChar, 20) { Value = "Pending" }));

                if (orderExists == 0)
                {
                    throw new Exception("Order not found or is not pending.");
                }

                const string getProductPriceSql = @"
SELECT Price
FROM Products
WHERE ProductId = @ProductId
  AND IsActive = 1
  AND Status = @Status;";

                object priceObject = DataProvider.Instance.ExecuteScalar(
                    getProductPriceSql,
                    CommandType.Text,
                    transaction,
                    new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId },
                    new SqlParameter("@Status", SqlDbType.NVarChar, 20) { Value = "Available" });

                if (priceObject == null || priceObject == DBNull.Value)
                {
                    throw new Exception("Product not found or unavailable.");
                }

                decimal unitPrice = Convert.ToDecimal(priceObject);
                decimal lineTotal = unitPrice * quantity;

                const string insertDetailSql = @"
INSERT INTO OrderDetails
(
    OrderId,
    ProductId,
    Quantity,
    UnitPrice,
    LineTotal,
    Notes
)
VALUES
(
    @OrderId,
    @ProductId,
    @Quantity,
    @UnitPrice,
    @LineTotal,
    @Notes
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

                int orderDetailId = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(
                    insertDetailSql,
                    CommandType.Text,
                    transaction,
                    new SqlParameter("@OrderId", SqlDbType.Int) { Value = orderId },
                    new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId },
                    new SqlParameter("@Quantity", SqlDbType.Int) { Value = quantity },
                    new SqlParameter("@UnitPrice", SqlDbType.Decimal) { Value = unitPrice, Precision = 18, Scale = 2 },
                    new SqlParameter("@LineTotal", SqlDbType.Decimal) { Value = lineTotal, Precision = 18, Scale = 2 },
                    new SqlParameter("@Notes", SqlDbType.NVarChar, 255) { Value = (object)notes ?? DBNull.Value }));

                const string updateOrderSql = @"
UPDATE Orders
SET TotalAmount = TotalAmount + @LineTotal,
    FinalAmount = (TotalAmount + @LineTotal) - DiscountAmount + VATAmount + SurchargeAmount
WHERE OrderId = @OrderId
  AND OrderStatus = @OrderStatus;";

                int affectedRows = DataProvider.Instance.ExecuteNonQuery(
                    updateOrderSql,
                    CommandType.Text,
                    transaction,
                    new SqlParameter("@LineTotal", SqlDbType.Decimal) { Value = lineTotal, Precision = 18, Scale = 2 },
                    new SqlParameter("@OrderId", SqlDbType.Int) { Value = orderId },
                    new SqlParameter("@OrderStatus", SqlDbType.NVarChar, 20) { Value = "Pending" });

                if (affectedRows == 0)
                {
                    throw new Exception("Failed to update order total amount.");
                }

                return orderDetailId;
            });
        }

        public DataTable GetAll()
        {
            const string sql = @"
        SELECT 
            o.OrderId,
            o.CreatedAt,
            e.FullName AS EmployeeName,
            t.TableName,
            o.FinalAmount,
            o.OrderStatus
        FROM Orders o
        INNER JOIN Employees e ON e.EmployeeId = o.EmployeeId
        INNER JOIN CafeTables t ON t.TableId = o.TableId
        ORDER BY o.CreatedAt DESC";

            return DataProvider.Instance.ExecuteQuery(
                sql,
                CommandType.Text,
                null);
        }

        public int MarkAsPaid(int orderId, decimal discountAmount, decimal vatAmount, decimal surchargeAmount, string notes = null)
        {
            if (orderId <= 0)
            {
                throw new ArgumentOutOfRangeException("orderId");
            }

            if (discountAmount < 0 || vatAmount < 0 || surchargeAmount < 0)
            {
                throw new ArgumentException("Amounts must be greater than or equal to 0.");
            }

            return DataProvider.Instance.ExecuteInTransaction(delegate (SqlTransaction transaction)
            {
                const string updateOrderSql = @"
UPDATE Orders
SET OrderStatus = @PaidStatus,
    PaidAt = SYSDATETIME(),
    DiscountAmount = @DiscountAmount,
    VATAmount = @VATAmount,
    SurchargeAmount = @SurchargeAmount,
    FinalAmount = TotalAmount - @DiscountAmount + @VATAmount + @SurchargeAmount,
    Notes = CASE
                WHEN @Notes IS NULL OR LTRIM(RTRIM(@Notes)) = N'' THEN Notes
                ELSE @Notes
            END
WHERE OrderId = @OrderId
  AND OrderStatus = @PendingStatus;";

                int affectedRows = DataProvider.Instance.ExecuteNonQuery(
                    updateOrderSql,
                    CommandType.Text,
                    transaction,
                    new SqlParameter("@PaidStatus", SqlDbType.NVarChar, 20) { Value = "Paid" },
                    new SqlParameter("@DiscountAmount", SqlDbType.Decimal) { Value = discountAmount, Precision = 18, Scale = 2 },
                    new SqlParameter("@VATAmount", SqlDbType.Decimal) { Value = vatAmount, Precision = 18, Scale = 2 },
                    new SqlParameter("@SurchargeAmount", SqlDbType.Decimal) { Value = surchargeAmount, Precision = 18, Scale = 2 },
                    new SqlParameter("@Notes", SqlDbType.NVarChar, 500) { Value = (object)notes ?? DBNull.Value },
                    new SqlParameter("@OrderId", SqlDbType.Int) { Value = orderId },
                    new SqlParameter("@PendingStatus", SqlDbType.NVarChar, 20) { Value = "Pending" });

                if (affectedRows == 0)
                {
                    throw new Exception("Order not found or is not pending.");
                }

                const string updateTableSql = @"
UPDATE ct
SET ct.Status = @TableStatus
FROM CafeTables AS ct
INNER JOIN Orders AS o
    ON o.TableId = ct.TableId
WHERE o.OrderId = @OrderId;";

                DataProvider.Instance.ExecuteNonQuery(
                    updateTableSql,
                    CommandType.Text,
                    transaction,
                    new SqlParameter("@TableStatus", SqlDbType.NVarChar, 20) { Value = "Empty" },
                    new SqlParameter("@OrderId", SqlDbType.Int) { Value = orderId });

                return affectedRows;
            });
        }
    }
}
