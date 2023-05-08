using System.Threading.Tasks;
using TestWebAPI.Dto;

namespace TestWebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto[]> GetUsersAsync();
        Task<UserDto> GetUserAsync(int UserId);
        Task<UserDto> CreateUserAsync(NewUserDto user);
        Task<UserDto> UpdateUserAsync(UpdateUserDto user);
        Task RemoveUserAsync(int UserId);
    }
}