using System.Data;
using System.Data.SqlClient;

namespace CoffeeShop.DAL.Repositories
{
    public class UserRepository
    {
        public DataRow Login(string username, string password)
        {
            const string sql = @"
SELECT
    u.UserId,
    u.Username,
    u.Role,
    u.IsActive,
    u.EmployeeId,
    e.FullName,
    e.Phone
FROM Users AS u
INNER JOIN Employees AS e
    ON e.EmployeeId = u.EmployeeId
WHERE u.Username = @Username
  AND u.[Password] = @Password
  AND u.IsActive = 1;";

            DataTable table = DataProvider.Instance.ExecuteQuery(
                sql,
                CommandType.Text,
                null,
                new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = username },
                new SqlParameter("@Password", SqlDbType.NVarChar, 255) { Value = password });

            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }

        public DataRow GetByUsername(string username)
        {
            const string sql = @"
SELECT
    u.UserId,
    u.Username,
    u.Role,
    u.IsActive,
    u.EmployeeId,
    e.FullName,
    e.Phone,
    e.HireDate
FROM Users AS u
INNER JOIN Employees AS e
    ON e.EmployeeId = u.EmployeeId
WHERE u.Username = @Username;";

            DataTable table = DataProvider.Instance.ExecuteQuery(
                sql,
                CommandType.Text,
                null,
                new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = username });

            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }
    }
}
