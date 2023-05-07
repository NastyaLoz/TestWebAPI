using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebAPI.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DifficaltyLvl { get; set; }
        [ForeignKey(nameof(Types))]
        public int TypeId { get; set; }
        
        public virtual Types Types { get; set; }
        public virtual ICollection<CompletedTasks> CompletedTasks { get; set; }
        public virtual ICollection<WordsTasks> WordsTasks { get; set; }
    }
}