using System.Data;

namespace quanlyquancafe.DAL
{
    public class TableDAL
    {
        DBHelper db = new DBHelper();

        public DataTable GetTables()
        {
            return db.ExecuteQuery("SELECT * FROM CafeTables");
        }

        public void UpdateStatus(int tableId, string status)
        {
            string query = $"UPDATE CafeTables SET Status = N'{status}' WHERE TableID = {tableId}";
            db.ExecuteNonQuery(query);
        }
    }
}