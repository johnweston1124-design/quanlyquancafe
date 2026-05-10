using quanlyquancafe.DAL;
using quanlyquancafe.DTO;
using System;
using System.Data;

namespace quanlyquancafe.BLL
{
    public class EmployeeBLL
    {
        private EmployeeDAL employeeDAL = new EmployeeDAL();

        public DataTable GetAll()
        {
            return employeeDAL.GetAll();
        }

        public DataTable SearchEmployees(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return employeeDAL.GetAll();

            return employeeDAL.Search(keyword.Trim());
        }

        public bool AddEmployee(
            string fullName,
            string phone,
            string address,
            out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(fullName))
            {
                message = "Vui lòng nhập họ tên.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                message = "Vui lòng nhập số điện thoại.";
                return false;
            }

            if (employeeDAL.IsPhoneExists(phone))
            {
                message = "Số điện thoại đã tồn tại.";
                return false;
            }

            EmployeeDTO employee = new EmployeeDTO
            {
                FullName = fullName.Trim(),
                Phone = phone.Trim(),
                Address = address?.Trim() ?? "",

                // default values
                Gender = "Male",
                DateOfBirth = DateTime.Now,
                HireDate = DateTime.Now,
                Salary = 0
            };

            bool result = employeeDAL.Insert(employee);

            message = result
                ? "Thêm nhân viên thành công."
                : "Thêm nhân viên thất bại.";

            return result;
        }

        public bool UpdateEmployee(
            int employeeId,
            string fullName,
            string phone,
            string address,
            out string message)
        {
            message = "";

            if (employeeId <= 0)
            {
                message = "Mã nhân viên không hợp lệ.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                message = "Vui lòng nhập họ tên.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                message = "Vui lòng nhập số điện thoại.";
                return false;
            }

            if (employeeDAL.IsPhoneExists(phone, employeeId))
            {
                message = "Số điện thoại đã tồn tại.";
                return false;
            }

            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeId = employeeId,
                FullName = fullName.Trim(),
                Phone = phone.Trim(),
                Address = address?.Trim() ?? "",

                // default values
                Gender = "Male",
                DateOfBirth = DateTime.Now,
                HireDate = DateTime.Now,
                Salary = 0
            };

            bool result = employeeDAL.Update(employee);

            message = result
                ? "Cập nhật nhân viên thành công."
                : "Cập nhật nhân viên thất bại.";

            return result;
        }

        public bool DeleteEmployee(int employeeId, out string message)
        {
            message = "";

            if (employeeId <= 0)
            {
                message = "Vui lòng chọn nhân viên.";
                return false;
            }

            bool result = employeeDAL.Delete(employeeId);

            message = result
                ? "Xóa nhân viên thành công."
                : "Xóa nhân viên thất bại.";

            return result;
        }
    }
}