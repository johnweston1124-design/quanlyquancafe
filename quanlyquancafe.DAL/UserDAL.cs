using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CoffeeShop.DAL;
using quanlyquancafe.DTO;

namespace quanlyquancafe.DAL
{
    public class UserDAL
    {
        public UserDTO GetByUsername(string username)
        {
            string query = @"
                SELECT u.UserId, u.Username, u.[Password], u.Role,
                       e.FullName
                FROM Users u
                INNER JOIN Employees e ON u.EmployeeId = e.EmployeeId
                WHERE u.Username = @Username AND u.IsActive = 1";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query,
                parameters: new SqlParameter("@Username", username));

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new UserDTO
                {
                    Id = (int)row["UserId"],
                    Username = row["Username"].ToString(),
                    PasswordHash = row["Password"].ToString(),
                    FullName = row["FullName"].ToString(),
                    Role = row["Role"].ToString()
                };
            }
            return null;
        }

        public bool IsUsernameExist(string username)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
            object result = DataProvider.Instance.ExecuteScalar(query,
                parameters: new SqlParameter("@Username", username));
            return (int)result > 0;
        }

        public bool Insert(UserRegisterDTO dto)
        {
            string query = @"
                INSERT INTO Users (Username, [Password], Role, IsActive, EmployeeId)
                VALUES (@Username, @Password, @Role, 1, @EmployeeId)";

            return DataProvider.Instance.ExecuteNonQuery(query,
                parameters: new SqlParameter[]
                {
                    new SqlParameter("@Username",   dto.Username),
                    new SqlParameter("@Password",   dto.Password),
                    new SqlParameter("@Role",       dto.Role),
                    new SqlParameter("@EmployeeId", dto.EmployeeId)
                }) > 0;
        }
    }
}