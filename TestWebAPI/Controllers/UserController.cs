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
        // Взять все Users
        /// <summary>
        /// Take all Users
        /// </summary>
        /// <remarks>
        /// Returns an array of Users
        /// </remarks>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult<UserDto[]>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            if (users == null) return NoContent();
            return users;
        }
        
        /// Взять User по его id
        /// <summary>
        /// Take User by id
        /// </summary>
        /// <remarks>
        /// Return a User with the corresponding Id . Example input:
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
        
        // Создадим нового User
        /// <summary>
        /// Creating a New User
        /// </summary>
        /// <remarks>
        /// Returns the created User. Example input:
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
        
        // Обновление User
        /// <summary>
        /// Update User
        /// </summary>
        /// <remarks>
        /// Return the corresponding User with changes. Example input:
        /// 
        ///     {
        ///         userId: 2,
        ///         email: newEmail@gmail.com,
        ///         login: newLogin,
        ///         password: ghkflkg,
        ///         name: New Name
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<UserDto> UpdateUser([FromBody] UpdateUserDto user)
        {
            var result = await _userRepository.UpdateUserAsync(user);
            return result;
        }
        
        // Удаление User по id
        /// <summary>
        /// Removing a User by its Id
        /// </summary>
        /// <remarks>
        /// Returns a result code. Example input:
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