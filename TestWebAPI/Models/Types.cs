using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestWebAPI.Models
{
    public class Types
    {
        [Key]
        public int TypeId { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}