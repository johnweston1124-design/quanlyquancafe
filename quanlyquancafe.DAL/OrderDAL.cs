using System.Data;
using quanlyquancafe.DTO;

namespace quanlyquancafe.DAL
{
    public class OrderDAL
    {
        DBHelper db = new DBHelper();

        public int CreateOrder(int tableId, int employeeId = 2) // Defaulting to 2 based on your insert data
        {
            string query = $@"INSERT INTO Orders(TableId, EmployeeId, OrderStatus, CreatedAt, TotalAmount, FinalAmount) 
                     VALUES ({tableId}, {employeeId}, N'Pending', GETDATE(), 0, 0)";
            db.ExecuteNonQuery(query);

            DataTable dt = db.ExecuteQuery("SELECT TOP 1 OrderId FROM Orders ORDER BY OrderId DESC");
            return int.Parse(dt.Rows[0]["OrderId"].ToString());
        }

        public void AddItem(int orderId, int productId, int quantity, decimal price)
        {
            string query = $"INSERT INTO OrderDetails VALUES ({orderId},{productId},{quantity},{price})";
            db.ExecuteNonQuery(query);
        }

        public DataTable GetDetails(int orderId)
        {
            string query = $@"SELECT d.OrderDetailID, p.ProductName, d.Quantity, p.Price 
                     FROM OrderDetails d 
                     JOIN Products p ON d.ProductID = p.ProductID 
                     WHERE d.OrderID = {orderId}";

            return db.ExecuteQuery(query);
        }

        public void Pay(int orderId)
        {
            db.ExecuteNonQuery($"UPDATE Orders SET IsPaid = 1 WHERE OrderID = {orderId}");
        }
        public int GetUnpaidOrder(int tableId)
        {
            DataTable dt = db.ExecuteQuery(
                $"SELECT TOP 1 OrderID FROM Orders WHERE TableID = {tableId} AND IsPaid = 0");

            if (dt.Rows.Count > 0)
                return int.Parse(dt.Rows[0]["OrderID"].ToString());

            return -1;
        }
        public void DeleteItem(int orderDetailId)
        {
            string query = $"DELETE FROM OrderDetails WHERE OrderDetailID = {orderDetailId}";
            db.ExecuteNonQuery(query);
        }
    }

}