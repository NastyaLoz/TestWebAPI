using System;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebAPI.Dto;
using TestWebAPI.Dto.Other;
using TestWebAPI.Extensions;


namespace TestWebAPI.Repositories
{
    public class OtherRepository : IOtherRepository
    {
        public ApplicationDbContext Context;

        public OtherRepository(ApplicationDbContext context) : base()
        {
            Context = context;
        }

        public async Task<WordWithPercentDto[]> GetWordsByUserAsync(int UserId)
        {
            var user = await Context.Users.SingleOrDefaultAsync(q => q.UserId == UserId);
            if (user == null)
            {
                throw new SecurityException("There is no user with this id");
            }
            //var words = await Context.LearnedWords.Where(q => q.UserId == user.UserId).ToListAsync();
            var res =  await Context.LearnedWords.Where(q => q.UserId == UserId)
                .Select(q => new WordWithPercentDto {Word = q.Words.Word, Percent = q.Percent}).ToArrayAsync();
            return res;
        }

        public async Task<TaskByUserAndTypeDto[]> GetTaskByUserFilterAsync(TypeUserDifDto filters)
        {
            var user = await Context.Users.SingleOrDefaultAsync(q => q.UserId == filters.UserId);
            if (user == null)
            {
                throw new SecurityException("There is no User with this id");
            }
            var type = await Context.Types.SingleOrDefaultAsync(q => q.TypeId == filters.TypeId);
            if (type == null)
            {
                throw new SecurityException("There is no Type with this id");
            }
            if (filters.DifLvlMin >= filters.DifLvlMax)
            {
                throw new SecurityException("Parameters are set incorrectly. The minimum difficulty level cannot be greater than or equal to the maximum.");
            }
            
            var dateToday = DateTime.Today;
            var dayOfWeek = (int)dateToday.DayOfWeek;
            var dateMonday = dateToday.AddDays(1-dayOfWeek);
            
            // var comTasks = await Context.CompletedTasks.Where(q => q.UserId == user.UserId && q.Tasks.TypeId == type.TypeId).ToArrayAsync();
            
            var res = await Context.CompletedTasks
                // возьмем выполненные задачи (ComponentTasks) по пользователю (UserId) и типу (TypeId) 
                .Where(q => q.UserId == user.UserId && q.Tasks.TypeId == type.TypeId)
                // далее извлечем задачи, удовлетворяющие условию: DifLvlMin < DifficaltyLvl < DifLvlMax
                .Where(q => q.Tasks.DifficaltyLvl > filters.DifLvlMin && q.Tasks.DifficaltyLvl < filters.DifLvlMax)
                // выполненные втечение текущей недели
                .Where(q=>DateTime.Compare(q.DateCompletion, dateMonday) >= 0 && DateTime.Compare(q.DateCompletion, dateToday)<=0)
                // задачи, имя которых начинается с указанного в фильтрах значения StartWithStr
                .Where(q=>q.Tasks.Name.StartsWith(filters.StartWithStr))
                // преобразем к нужной модели данных
                .Select(q=>new TaskByUserAndTypeDto{Name = q.Tasks.Name, Description = q.Tasks.Description, DifficaltyLvl = q.Tasks.DifficaltyLvl})
                .ToArrayAsync();

            return res;
        }

        public async Task<TaskDto[]> GetTaskByWorkAndUserAsync(WorkAndUserDto filters)
        {
            var user = await Context.Users.SingleOrDefaultAsync(q => q.UserId == filters.UserId);
            if (user == null)
            {
                throw new SecurityException("There is no User with this id");
            }

            var res = await Context.CompletedTasks
                .Where(q => q.UserId == filters.UserId && q.Tasks.Description.Contains(filters.Word))
                .Select(q=>
                    new TaskDto
                    {
                        TaskId = q.TaskId,
                        Name = q.Tasks.Name,
                        Description = q.Tasks.Description,
                        DifficaltyLvl = q.Tasks.DifficaltyLvl,
                        TypeId = q.Tasks.TypeId
                    })
                .ToArrayAsync();
            return res;
        }

        public async Task<UserDto[]> GetTopUsersOrderByPercentAsync(TopNumPercentDto filters)
        {
            var res= await Context.LearnedWords
                // возьмем изученные слова с процентом изученности больше заданного
                .Where(q => q.Percent > filters.Percent)
                // преобразуем в новый тип - извлечем кол-во изученных каждым пользователем слов и самих пользователей
                .Select(q => new {user = q.Users, count = q.Users.LearnedWords.Count})
                // оставим только уникальных пользователей
                .Distinct()
                // отсортируем по убыванию на основе кол-ва изученных слов
                .OrderByDescending(q=>q.count)
                // извлечем пользователей
                .Select(q=>q.user)
                // возмем n-е кол-во первых пользователей
                .Take(filters.TopNum)
                .ToArrayAsync();
            
            return res.ToJsonDto();
        }
    }
}