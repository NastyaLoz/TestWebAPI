using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebAPI.Models
{
    public class WordsTasks
    {
        [ForeignKey(nameof(Tasks))]
        public int TaskId { get; set; }
        
        [ForeignKey(nameof(Words))]
        public int WordId { get; set; }

        public virtual Tasks Tasks { get; set; }
        public virtual Words Words { get; set; }
    }
}