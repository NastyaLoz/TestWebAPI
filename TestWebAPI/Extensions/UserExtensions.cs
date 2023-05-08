using System;
using System.Collections.Generic;
using System.Linq;
using TestWebAPI.Dto;
using TestWebAPI.Models;

namespace TestWebAPI.Extensions
{
    public static class UserExtensions
    {
        public static UserDto ToJsonDto(this Users user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                Name = user.Name,
                DateReg = user.DateReg
            };
        }

        public static UserDto[] ToJsonDto(this IEnumerable<Users> users)
        {
            return users.Select(at => at.ToJsonDto()).ToArray();
        }
    }
}