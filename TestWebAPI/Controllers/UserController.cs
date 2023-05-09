using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestWebAPI.Dto;
using TestWebAPI.Models;
using TestWebAPI.Repositories;

namespace TestWebAPI.Controllers
{
    [ApiController]
    [Route("users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        ///////// Users /////////
        /// <summary>
        /// Взять всех Users
        /// </summary>
        /// <remarks>
        /// Вернет массив Users
        /// </remarks>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<UserDto[]>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            if (users == null) return NoContent();
            return users;
        }
        
        /// <summary>
        /// Взять User по его id
        /// </summary>
        /// <remarks>
        /// Вернет User по заданному Id. Пример ввода:
        /// 
        ///     {
        ///         userId: 1
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var user = await _userRepository.GetUserAsync(userId);
            if (user == null) return NoContent();
            return user;
        }
        
        /// <summary>
        /// Создать нового User
        /// </summary>
        /// <remarks>
        /// Вернет созданного User. Пример ввода:
        /// 
        ///     {
        ///         email: newEmail@gmail.com,
        ///         login: newLogin,
        ///         password: ghkflkg,
        ///         name: New Name
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPost("newUser")]
        public async Task<UserDto> CreateUser([FromBody]NewUserDto newUser)
        {
            var result = await _userRepository.CreateUserAsync(newUser);
            return result;
        }
        
        /// <summary>
        /// Обновить User
        /// </summary>
        /// <remarks>
        /// Вернет обновленный User. Пример ввода:
        /// 
        ///     {
        ///         userId: 5,
        ///         email: newEmail@gmail.com,
        ///         login: newLogin,
        ///         password: ghkflkg,
        ///         name: New Name
        ///     }
        ///
        /// Если опустить некоторые значения (кроме userId), эти поля сохранят свои текущие значения. Пример ввода:
        /// 
        ///     {
        ///         userId: 5,
        ///         name: NewName
        ///     }
        ///
        /// Этот код изменит только параметр name у User
        /// </remarks>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<UserDto> UpdateUser([FromBody] UpdateUserDto user)
        {
            var result = await _userRepository.UpdateUserAsync(user);
            return result;
        }
        
        /// <summary>
        /// Удалить User по Id
        /// </summary>
        /// <remarks>
        /// Вернет код результата. Важно! Удалит все связанные с данным пользователем (по userId) значения из связанных таблиц LearnedWords и CompletedTasks. Пример ввода:
        /// 
        ///     {
        ///         userId: 2
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpDelete("remove/{userId}")]
        public async Task<StatusCodeResult> RemoveUserAsync(int userId)
        {
            await _userRepository.RemoveUserAsync(userId);

            return NoContent();
        }
    }
}