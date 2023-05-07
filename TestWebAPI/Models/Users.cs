using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestWebAPI.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime DateReg { get; set; }
        
        public virtual ICollection<CompletedTasks> CompletedTasks { get; set; }
        public virtual ICollection<LearnedWords> LearnedWords { get; set; }
    }
}