using System;
using System.Data;
using System.Data.SqlClient;

namespace quanlyquancafe.DAL
{
    public class TableDAL
    {
        public DataTable GetAll()
        {
            string query = @"
                SELECT 
                    TableId,
                    TableName,
                    Status,
                    Capacity,
                    IsActive,
                    Zone
                FROM CafeTables
            ";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        public void Insert(string name, int capacity, string status, string zone)
        {
            string query = @"
                INSERT INTO CafeTables(TableName, Capacity, Status, Zone)
                VALUES (@name, @capacity, @status, @zone)
            ";

            DataProvider.Instance.ExecuteNonQuery(
                query,
                CommandType.Text,
                null,
                new SqlParameter("@name", name),
                new SqlParameter("@capacity", capacity),
                new SqlParameter("@status", status),
                new SqlParameter("@zone", (object)zone ?? DBNull.Value)
            );
        }

        public void Update(int id, string name, int capacity, string status, bool isActive, string zone)
        {
            string query = @"
                UPDATE CafeTables
                SET 
                    TableName = @name,
                    Capacity = @capacity,
                    Status = @status,
                    IsActive = @active,
                    Zone = @zone
                WHERE TableId = @id
            ";

            DataProvider.Instance.ExecuteNonQuery(
                query,
                CommandType.Text,
                null,
                new SqlParameter("@id", id),
                new SqlParameter("@name", name),
                new SqlParameter("@capacity", capacity),
                new SqlParameter("@status", status),
                new SqlParameter("@active", isActive),
                new SqlParameter("@zone", (object)zone ?? DBNull.Value)
            );
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM CafeTables WHERE TableId = @id";

            DataProvider.Instance.ExecuteNonQuery(
                query,
                CommandType.Text,
                null,
                new SqlParameter("@id", id)
            );
        }
    }   
}