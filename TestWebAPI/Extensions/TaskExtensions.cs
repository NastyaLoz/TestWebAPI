using System.Collections.Generic;
using System.Linq;
using TestWebAPI.Dto;
using TestWebAPI.Models;

namespace TestWebAPI.Extensions
{
    public static class TaskExtensions
    {
        public static TaskDto ToJsonDto(this Tasks task)
        {
            return new TaskDto
            {
                TaskId = task.TaskId,
                Name = task.Name,
                Description = task.Description,
                DifficaltyLvl = task.DifficaltyLvl,
                TypeId = task.TypeId
            };
        }

        public static TaskDto[] ToJsonDto(this IEnumerable<Tasks> tasks)
        {
            return tasks.Select(at => at.ToJsonDto()).ToArray();
        }
    }
}