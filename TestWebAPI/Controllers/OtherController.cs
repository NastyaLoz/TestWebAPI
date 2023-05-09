using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebAPI.Dto;
using TestWebAPI.Dto.Other;
using TestWebAPI.Repositories;

namespace TestWebAPI.Controllers
{
    [ApiController]
    [Route("other")]
    [Produces("application/json")]
    public class OtherController : ControllerBase
    {
        private readonly IOtherRepository _otherRepository;

        public OtherController(IOtherRepository otherRepository)
        {
            _otherRepository = otherRepository;
        }
        
        /// <summary>
        /// Вывести выученные пользователем слова с процентами
        /// </summary>
        /// <remarks>
        /// Выведет все слова, которые выучил заданный пользователь (по userId), с процентами. Пример ввода:
        /// 
        ///     {
        ///         userId: 1
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpGet("wordsByUser/userId={userId}")]
        public async Task<ActionResult<WordWithPercentDto[]>> GetWordsByUser(int userId)
        {
            var res = await _otherRepository.GetWordsByUserAsync(userId);
            if (res == null) return NoContent();
            return res;
        }
        
        /// <summary>
        /// Вывести решенные пользователем задания по фильтрам
        /// </summary>
        /// <remarks>
        /// Выведет все задания заданного типа (по typeId), которые решил заданный пользователь (по userId) за последнюю неделю (вычисляется на основе текущей даты), уровень сложности которых от X (difLvlMin) до Y (difLvlMax), а название начинается со слова (startWithStr). Пример ввода:
        /// 
        ///     {
        ///         typeId: 2
        ///         userId: 121
        ///         startWithStr: "Повторение"
        ///         difLvlMin: 21.36
        ///         difLvlMax: 123
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPut("taskByFilter")]
        public async Task<ActionResult<TaskByUserAndTypeDto[]>> GetTaskByUserFilter([FromBody]TypeUserDifDto filters)
        {
            var res = await _otherRepository.GetTaskByUserFilterAsync(filters);
            if (res == null) return NoContent();
            return res;
        }
        
        /// <summary>
        /// Вывести выполненные пользователем задания с определенным словом
        /// </summary>
        /// <remarks>
        /// Выведет все задания, в которые входит заданное слово (word) и статус, выполнено ли это задание заданным пользователем (userId). Пример ввода:
        /// 
        ///     {
        ///         word: "mauris"
        ///         userId: 121
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPut("taskByWorkAndUser")]
        public async Task<ActionResult<TaskDto[]>> GetTaskByWorkAndUser([FromBody]WorkAndUserDto filters)
        {
            var res = await _otherRepository.GetTaskByWorkAndUserAsync(filters);
            if (res == null) return NoContent();
            return res;
        }
        
        /// <summary>
        /// Вывести Топ N пользователей по проценту и фильтрам
        /// </summary>
        /// <remarks>
        /// Выведет ТОП N пользователей (topNum), отсортированных по количеству изученных слов на процент, больше ХХ% (percent). Таким образом, выведется заданное кол-во пользователей, изучивших большее кол-во слов не менее чем на заданный процент (имеется ввиду процент изученности каждого слова) Пример ввода:
        /// 
        ///     {
        ///         topNum: 5
        ///         percent: 63
        ///     }
        /// 
        /// </remarks>
        /// <returns></returns>
        [HttpPut("topUsersOrderByPercent")]
        public async Task<ActionResult<UserDto[]>> GetTopUsersOrderByPercent([FromBody]TopNumPercentDto filters)
        {
            var res = await _otherRepository.GetTopUsersOrderByPercentAsync(filters);
            if (res == null) return NoContent();
            return res;
        }
    }
}