using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security;
using TestWebAPI.Models;
using TestWebAPI.Dto;
using TestWebAPI.Extensions;

namespace TestWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext Context;

        public UserRepository(ApplicationDbContext context) : base()
        {
            Context = context;
        }

        public async Task<UserDto[]> GetUsersAsync()
        {
            var res = await Context.Users.ToArrayAsync();
            return res.ToJsonDto();
        }

        public async Task<UserDto> GetUserAsync(int UserId)
        {
            var user = await Context.Users.SingleOrDefaultAsync(q => q.UserId == UserId);
            if (user == null)
            {
                throw new SecurityException("There is no user with this id");
            }
            return user.ToJsonDto();
        }

        public async Task<UserDto> CreateUserAsync(NewUserDto newUser)
        {
            var user = new Users
            {
                Email = newUser.Email,
                Login = newUser.Login,
                Password = newUser.Password,
                Name = newUser.Name,
                DateReg = DateTime.Today
            };
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
            return  user.ToJsonDto();
        }

        public async Task<UserDto> UpdateUserAsync(UpdateUserDto updateUser)
        {
            var userDb = await Context.Users.SingleOrDefaultAsync(q => q.UserId == updateUser.UserId);
            if (userDb == null)
                throw new SecurityException("There is no such user");

            userDb.Email = updateUser.Email ?? userDb.Email;
            userDb.Login = updateUser.Login ?? userDb.Login;
            userDb.Name = updateUser.Name ?? userDb.Name;
            userDb.Password = updateUser.Password ?? userDb.Password;
            await Context.SaveChangesAsync();
            return userDb.ToJsonDto();
        }

        public async Task RemoveUserAsync(int UserId)
        {
            var user = await Context.Users.SingleOrDefaultAsync(q => q.UserId == UserId);
            if (user == null)
                throw new SecurityException("There is no such user");

            Context.Users.Remove(user);
            await Context.SaveChangesAsync();
        }
    }
}