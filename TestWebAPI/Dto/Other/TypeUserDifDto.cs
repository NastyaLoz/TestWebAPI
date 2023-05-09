using System;

namespace TestWebAPI.Dto.Other
{
    public class TypeUserDifDto
    {
        public int TypeId { get; set; }
        public int UserId { get; set; }
        public string StartWithStr { get; set; }
        public double DifLvlMin { get; set; }
        public double DifLvlMax { get; set; }
    }
}