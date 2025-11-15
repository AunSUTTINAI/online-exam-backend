namespace class_room.Models
{
    public class ExamQuestion
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public List<string>? Options { get; set; }
        public string? Correct_Answer { get; set; }
    }
}
