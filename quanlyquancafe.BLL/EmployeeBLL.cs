using System;
using System.Data;
using quanlyquancafe.DAL;
using quanlyquancafe.DTO;

namespace quanlyquancafe.BLL
{
    public class EmployeeBLL
    {
        private EmployeeDAL employeeDAL = new EmployeeDAL();

        public DataTable GetAllEmployees()
        {
            return employeeDAL.GetAll();
        }

        public DataTable SearchEmployees(string keyword)
        {
            if (keyword == null)
                keyword = "";

            return employeeDAL.Search(keyword.Trim());
        }

        public bool AddEmployee(string fullName, string gender, DateTime dateOfBirth,
            string phone, string address, DateTime hireDate, decimal salary, out string message)
        {
            message = "";

            EmployeeDTO employee = new EmployeeDTO
            {
                FullName = fullName.Trim(),
                Gender = gender.Trim(),
                DateOfBirth = dateOfBirth,
                Phone = phone.Trim(),
                Address = address == null ? "" : address.Trim(),
                HireDate = hireDate,
                Salary = salary
            };

            bool result = employeeDAL.Insert(employee);
            message = result ? "Thêm nhân viên thành công." : "Thêm nhân viên thất bại.";
            return result;
        }

        public bool UpdateEmployee(int employeeId, string fullName, string gender, DateTime dateOfBirth,
            string phone, string address, DateTime hireDate, decimal salary, out string message)
        {
            message = "";

            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeId = employeeId,
                FullName = fullName.Trim(),
                Gender = gender.Trim(),
                DateOfBirth = dateOfBirth,
                Phone = phone.Trim(),
                Address = address == null ? "" : address.Trim(),
                HireDate = hireDate,
                Salary = salary
            };

            bool result = employeeDAL.Update(employee);
            message = result ? "Cập nhật nhân viên thành công." : "Cập nhật nhân viên thất bại.";
            return result;
        }

        public bool DeleteEmployee(int employeeId, out string message)
        {
            message = "";

            bool result = employeeDAL.Delete(employeeId);
            message = result ? "Xóa nhân viên thành công." : "Xóa nhân viên thất bại.";
            return result;
        }
    }
}