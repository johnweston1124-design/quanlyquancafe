using System;
using System.Data;
using System.Data.SqlClient;
using quanlyquancafe.DTO;

namespace quanlyquancafe.DAL
{
    public class EmployeeDAL
    {
        public DataTable GetAll()
        {
            string query = @"
                SELECT EmployeeId, FullName, Gender, DateOfBirth, Phone, Address, HireDate, Salary
                FROM Employees
                ORDER BY EmployeeId DESC";

            return DataProvider.ExecuteQuery(query);
        }

        public DataTable Search(string keyword)
        {
            string query = @"
                SELECT EmployeeId, FullName, Gender, DateOfBirth, Phone, Address, HireDate, Salary
                FROM Employees
                WHERE FullName LIKE @Keyword
                   OR Phone LIKE @Keyword
                   OR Address LIKE @Keyword
                ORDER BY EmployeeId DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };

            return DataProvider.ExecuteQuery(query, parameters);
        }

        public bool Insert(EmployeeDTO employee)
        {
            string query = @"
                INSERT INTO Employees (FullName, Gender, DateOfBirth, Phone, Address, HireDate, Salary)
                VALUES (@FullName, @Gender, @DateOfBirth, @Phone, @Address, @HireDate, @Salary)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FullName", employee.FullName),
                new SqlParameter("@Gender", employee.Gender),
                new SqlParameter("@DateOfBirth", employee.DateOfBirth),
                new SqlParameter("@Phone", employee.Phone),
                new SqlParameter("@Address", employee.Address),
                new SqlParameter("@HireDate", employee.HireDate),
                new SqlParameter("@Salary", employee.Salary)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Update(EmployeeDTO employee)
        {
            string query = @"
                UPDATE Employees
                SET FullName = @FullName,
                    Gender = @Gender,
                    DateOfBirth = @DateOfBirth,
                    Phone = @Phone,
                    Address = @Address,
                    HireDate = @HireDate,
                    Salary = @Salary
                WHERE EmployeeId = @EmployeeId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeId", employee.EmployeeId),
                new SqlParameter("@FullName", employee.FullName),
                new SqlParameter("@Gender", employee.Gender),
                new SqlParameter("@DateOfBirth", employee.DateOfBirth),
                new SqlParameter("@Phone", employee.Phone),
                new SqlParameter("@Address", employee.Address),
                new SqlParameter("@HireDate", employee.HireDate),
                new SqlParameter("@Salary", employee.Salary)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Delete(int employeeId)
        {
            string query = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeId", employeeId)
            };

            return DataProvider.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool IsPhoneExists(string phone, int excludeEmployeeId = 0)
        {
            string query = @"
                SELECT COUNT(*)
                FROM Employees
                WHERE Phone = @Phone AND EmployeeId <> @EmployeeId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Phone", phone),
                new SqlParameter("@EmployeeId", excludeEmployeeId)
            };

            object result = DataProvider.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result) > 0;
        }
    }
}