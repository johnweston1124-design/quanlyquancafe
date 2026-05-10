using System;
using System.Collections.Generic;
using System.Text;

namespace quanlyquancafe.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public int EmployeeId { get; set; }
    }
}
