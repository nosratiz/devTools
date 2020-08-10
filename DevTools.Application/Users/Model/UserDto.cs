using System;

namespace DevTools.Application.Users.Model
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public double Wallet { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}