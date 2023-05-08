using System.Threading.Tasks;
using TestWebAPI.Dto;

namespace TestWebAPI.Repositories.Tasks
{
    public interface ITaskRepository
    {
        Task<TaskDto[]> GetTasksAsync();
        Task<TaskDto> GetTaskAsync(int TaskId);
        Task<TaskDto> CreateTaskAsync(NewTaskDto task);
        Task<TaskDto> UpdateTaskAsync(TaskDto task);
        Task RemoveTaskAsync(int TaskId);
    }
}