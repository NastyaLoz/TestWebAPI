using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestWebAPI.Dto;
using TestWebAPI.Models;
using TestWebAPI.Repositories;
using TestWebAPI.Repositories.Tasks;

namespace TestWebAPI.Controllers
{
    [ApiController]
    [Route("tasks")]
    [Produces("application/json")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
        ///////// Tasks /////////
        // Взять все Tasks
        /// <summary>
        /// Take all Tasks
        /// </summary>
        /// <remarks>
        /// Returns an array of Tasks
        /// </remarks>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<TaskDto[]>> GetTasks()
        {
            var task = await _taskRepository.GetTasksAsync();
            if (task == null) return NoContent();
            return task;
        }
        
        /// Взять Task по его id
        /// <summary>
        /// Take Task by id
        /// </summary>
        /// <remarks>
        /// Return a Task with the corresponding Id . Example input:
        /// 
        ///     {
        ///         taskId: 1
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskDto>> GetTask(int taskId)
        {
            var task = await _taskRepository.GetTaskAsync(taskId);
            if (task == null) return NoContent();
            return task;
        }
        
        // Создадим новый Task
        /// <summary>
        /// Creating a New Task
        /// </summary>
        /// <remarks>
        /// Returns the created Task. Example input:
        /// 
        ///     {
        ///         name: NewTask,
        ///         description: about this task,
        ///         difficaltyLvl: 10.25,
        ///         typeId: 2
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPost("newTask")]
        public async Task<TaskDto> CreateTask([FromBody]NewTaskDto newTask)
        {
            var result = await _taskRepository.CreateTaskAsync(newTask);
            return result;
        }
        
        // Обновление Task
        /// <summary>
        /// Update Task
        /// </summary>
        /// <remarks>
        /// Return the corresponding Task with changes. Example input:
        /// 
        ///     {
        ///         taskId: 2,
        ///         name: NewNameTask
        ///         description: newEmail@gmail.com,
        ///         difficaltyLvl: newLogin,
        ///         typeId: ghkflkg,
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<TaskDto> UpdateTask([FromBody]TaskDto task)
        {
            var result = await _taskRepository.UpdateTaskAsync(task);
            return result;
        }
        
        // Удаление Task по id
        /// <summary>
        /// Removing a Task by its Id
        /// </summary>
        /// <remarks>
        /// Returns a result code. Example input:
        /// 
        ///     {
        ///         taskId: 2
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpDelete("remove/{taskId}")]
        public async Task<StatusCodeResult> RemoveTaskAsync(int taskId)
        {
            await _taskRepository.RemoveTaskAsync(taskId);

            return NoContent();
        }
    }
}