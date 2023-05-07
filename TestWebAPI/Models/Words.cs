using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestWebAPI.Models
{
    public class Words
    {
        [Key]
        public int WordId { get; set; }
        public string Word { get; set; }
        public string Translation { get; set; }
        public string ImagePath { get; set; }
        
        public virtual ICollection<WordsTasks> WordsTasks { get; set; }
        public virtual ICollection<LearnedWords> LearnedWords { get; set; }
    }
}