namespace class_room.Models
{
    public class SubmitExamRequest
    {
        public string FullName { get; set; }
        public List<SubmitExamItemsRequest> Answers { get; set; }
    }

    public class SubmitExamItemsRequest
    {
        public int QuestionId { get; set; }
        public string SelectedAnswer { get; set; }
    }

    public class SendExamResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int AttemptNumber { get; set; }
        public double Score { get; set; }    
    }

}
