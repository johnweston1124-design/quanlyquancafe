using System;
using System.Data;
using System.Data.SqlClient;
using quanlyquancafe.DAL;
using quanlyquancafe.DTO;

namespace quanlyquancafe.BLL
{
    public class UserBLL
    {
        private readonly UserDAL _dal = new UserDAL();

        public UserDTO Login(UserLoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Vui lòng nhập đầy đủ thông tin!");

            var user = _dal.GetByUsername(dto.Username);
            if (user == null) return null;

            return dto.Password == user.PasswordHash ? user : null;
        }

        public bool Register(UserRegisterDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Vui lòng nhập đầy đủ thông tin!");

            if (_dal.IsUsernameExist(dto.Username))
                throw new InvalidOperationException("Tên đăng nhập đã tồn tại!");

            return _dal.Insert(dto);
        }
    }
}