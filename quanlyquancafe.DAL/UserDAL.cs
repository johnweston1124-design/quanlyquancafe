using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using quanlyquancafe.DTO;

namespace quanlyquancafe.DAL
{
    public class UserDAL
    {
        public DataTable GetAll()
        {
            string query = @"
                SELECT 
                    u.UserId,
                    u.Username,
                    u.Role,
                    u.IsActive,
                    e.FullName
                FROM Users u
                JOIN Employees e ON u.EmployeeId = e.EmployeeId
            ";

            return DataProvider.ExecuteQuery(query);
        }

        public void Update(int id, string username, string password,
            int employeeId, string role, bool isActive)
        {
            string query = @"
                UPDATE Users SET
                    Username = @Username,
                    [Password] = @Password,
                    Role = @Role,
                    EmployeeId = @EmployeeId,
                    IsActive = @IsActive
                WHERE UserId = @UserId
            ";

            DataProvider.ExecuteNonQuery(query,
                new SqlParameter[]
                {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password),
                    new SqlParameter("@Role", role),
                    new SqlParameter("@EmployeeId", employeeId),
                    new SqlParameter("@IsActive", isActive),
                    new SqlParameter("@UserId", id)
                });
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Users WHERE UserId = @UserId";

            DataProvider.ExecuteNonQuery(query,
                new SqlParameter[]
                {
                    new SqlParameter("@UserId", id)
                });
        }

        public UserDTO GetByUsername(string username)
        {
            string query = @"
                SELECT 
                    u.UserId,
                    u.Username,
                    u.[Password],
                    u.Role
                FROM Users u
                WHERE u.Username = @Username
                AND u.IsActive = 1
            ";

            DataTable dt = DataProvider.ExecuteQuery(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@Username", username)
                });

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                return new UserDTO
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    Username = row["Username"].ToString(),
                    Password = row["Password"].ToString(),
                    Role = row["Role"].ToString()
                };
            }

            return null;
        }

        public bool IsUsernameExist(string username)
        {
            string query =
                "SELECT COUNT(1) FROM Users WHERE Username = @Username";

            object result = DataProvider.ExecuteScalar(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@Username", username)
                });

            return Convert.ToInt32(result) > 0;
        }

        public bool Insert(UserRegisterDTO dto)
        {
            string query = @"
                INSERT INTO Users
                (
                    Username,
                    [Password],
                    Role,
                    IsActive,
                    EmployeeId
                )
                VALUES
                (
                    @Username,
                    @Password,
                    @Role,
                    1,
                    @EmployeeId
                )
            ";

            int rows = DataProvider.ExecuteNonQuery(
                query,
                new SqlParameter[]
                {
                    new SqlParameter("@Username", dto.Username),
                    new SqlParameter("@Password", dto.Password),
                    new SqlParameter("@Role", dto.Role),
                    new SqlParameter("@EmployeeId", dto.EmployeeId)
                });

            return rows > 0;
        }
    }
}