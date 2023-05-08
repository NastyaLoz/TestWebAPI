namespace TestWebAPI.Dto
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? DifficaltyLvl { get; set; }
        public int? TypeId { get; set; }
    }
}