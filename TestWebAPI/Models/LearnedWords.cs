using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebAPI.Models
{
    public class LearnedWords
    {
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        
        [ForeignKey(nameof(Words))]
        public int WordId { get; set; }
        
        public int Percent { get; set; }

        public virtual Users Users { get; set; }
        public virtual Words Words { get; set; }
    }
}