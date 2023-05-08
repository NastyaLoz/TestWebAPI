using System.Security;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security;
using TestWebAPI.Models;
using TestWebAPI.Dto;
using TestWebAPI.Extensions;

namespace TestWebAPI.Repositories.Tasks
{
    public class TaskRepository : ITaskRepository
    {
        public ApplicationDbContext Context;
        
        public TaskRepository(ApplicationDbContext context) : base()
        {
            Context = context;
        }

        public async Task<TaskDto[]> GetTasksAsync()
        {
            var res = await Context.Tasks.ToArrayAsync();
            return res.ToJsonDto();
        }

        public async Task<TaskDto> GetTaskAsync(int TaskId)
        {
            var task = await Context.Tasks.SingleOrDefaultAsync(q => q.TaskId == TaskId);
            if (task == null)
            {
                throw new SecurityException("There is no task with this id");
            }
            return task.ToJsonDto();
        }

        public async Task<TaskDto> CreateTaskAsync(NewTaskDto task)
        {
            var newTask = new Models.Tasks
            {
                Name = task.Name,
                Description = task.Description,
                DifficaltyLvl = task.DifficaltyLvl,
                TypeId = task.TypeId
            };
            await Context.Tasks.AddAsync(newTask);
            await Context.SaveChangesAsync();
            return  newTask.ToJsonDto();
        }

        public async Task<TaskDto> UpdateTaskAsync(TaskDto updateTask)
        {
            var taskDb = await Context.Tasks.SingleOrDefaultAsync(q => q.TaskId == updateTask.TaskId);
            if (taskDb == null)
                throw new SecurityException("There is no such task");

            taskDb.Name = updateTask.Name ?? taskDb.Name;
            taskDb.Description = updateTask.Description ?? taskDb.Description;
            taskDb.DifficaltyLvl = updateTask.DifficaltyLvl ?? taskDb.DifficaltyLvl;
            taskDb.TypeId = updateTask.TypeId ?? taskDb.TypeId;
            await Context.SaveChangesAsync();
            return taskDb.ToJsonDto();
        }

        public async Task RemoveTaskAsync(int TaskId)
        {
            var task = await Context.Tasks.SingleOrDefaultAsync(q => q.TaskId == TaskId);
            if (task == null)
                throw new SecurityException("There is no such task");

            Context.Tasks.Remove(task);
            await Context.SaveChangesAsync();
        }
    }
}