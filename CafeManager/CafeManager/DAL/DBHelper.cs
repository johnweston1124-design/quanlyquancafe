using System;
using System.Data;
using System.Data.SqlClient;

namespace CafeManager.DAL
{
    internal class DBHelper
    {
        private string connStr = "Server=localhost\\SQLEXPRESS;Database=CafeDB;Trusted_Connection=True;";

        // Lấy dữ liệu (SELECT)
        public DataTable ExecuteQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Thực thi INSERT, UPDATE, DELETE
        public void ExecuteNonQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}