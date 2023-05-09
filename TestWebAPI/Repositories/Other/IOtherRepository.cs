using System.Threading.Tasks;
using TestWebAPI.Dto;
using TestWebAPI.Dto.Other;

namespace TestWebAPI.Repositories
{
    public interface IOtherRepository
    {
        Task<WordWithPercentDto[]> GetWordsByUserAsync(int UserId);
        Task<TaskByUserAndTypeDto[]> GetTaskByUserFilterAsync(TypeUserDifDto filters);
        Task<TaskDto[]> GetTaskByWorkAndUserAsync(WorkAndUserDto filters);
        Task<UserDto[]> GetTopUsersOrderByPercentAsync(TopNumPercentDto filters);
    }
}