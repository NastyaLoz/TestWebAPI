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
        /// <summary>
        /// Взять все Tasks
        /// </summary>
        /// <remarks>
        /// Вернет массив Tasks
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
        /// Взять Task по его id
        /// </summary>
        /// <remarks>
        /// Вернет Task по заданному Id. Пример ввода:
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
        
        /// <summary>
        /// Создать новый Task
        /// </summary>
        /// <remarks>
        /// Вернет созданный Task. Пример ввода:
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
        
        /// <summary>
        /// Обновить Task
        /// </summary>
        /// <remarks>
        /// Вернет обновленный Task. Пример ввода:
        /// 
        ///     {
        ///         taskId: 2,
        ///         name: NewNameTask
        ///         description: newEmail@gmail.com,
        ///         difficaltyLvl: newLogin,
        ///         typeId: ghkflkg,
        ///     }
        /// 
        /// Если опустить некоторые значения (кроме taskId), эти поля сохранят свои текущие значения. Пример ввода:
        /// 
        ///     {
        ///         taskId: 2,
        ///         name: NewNameTask
        ///     }
        ///
        /// Этот код изменит только параметр name у Task
        /// </remarks>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<TaskDto> UpdateTask([FromBody]TaskDto task)
        {
            var result = await _taskRepository.UpdateTaskAsync(task);
            return result;
        }
        
        /// <summary>
        /// Удалить Task по id
        /// </summary>
        /// <remarks>
        /// Вернет код результата. Важно! Удалит все связанные с данной задачей (по typeId) значения из связанных таблиц WordsTasks и CompletedTasks. Пример ввода:
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