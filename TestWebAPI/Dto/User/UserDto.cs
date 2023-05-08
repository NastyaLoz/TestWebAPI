using System;

namespace TestWebAPI.Dto
{
    public class UserDto
    {
        
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime DateReg { get; set; }
    }
}