using System.Data;

namespace CafeManager.DAL
{
    internal class OrderDAL
    {
        DBHelper db = new DBHelper();

        public int CreateOrder(int tableId)
        {
            string query = $"INSERT INTO Orders(TableID, CreatedDate, IsPaid) VALUES ({tableId}, GETDATE(), 0)";
            db.ExecuteNonQuery(query);

            DataTable dt = db.ExecuteQuery("SELECT TOP 1 OrderID FROM Orders ORDER BY OrderID DESC");
            return int.Parse(dt.Rows[0]["OrderID"].ToString());
        }

        public void AddItem(int orderId, int productId, int quantity, decimal price)
        {
            string query = $"INSERT INTO OrderDetails VALUES ({orderId},{productId},{quantity},{price})";
            db.ExecuteNonQuery(query);
        }

        public DataTable GetDetails(int orderId)
        {
            return db.ExecuteQuery($"SELECT * FROM OrderDetails WHERE OrderID = {orderId}");
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