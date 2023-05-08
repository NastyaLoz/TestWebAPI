namespace TestWebAPI.Dto
{
    public class NewTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double DifficaltyLvl { get; set; }
        public int TypeId { get; set; }
    }
}