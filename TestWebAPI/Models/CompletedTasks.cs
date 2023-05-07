using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebAPI.Models
{
    public class CompletedTasks
    {
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        
        [ForeignKey(nameof(Tasks))]
        public int TaskId { get; set; }
        
        public DateTime DateCompletion { get; set; }
        
        public virtual Users Users { get; set; }
        public virtual Tasks Tasks { get; set; }
    }
}