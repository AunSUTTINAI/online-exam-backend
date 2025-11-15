using class_room.Models;

namespace class_room.UnitOfWork.Interface
{
    public interface IUowClassRoom : IDisposable
    {
        Task<List<ExamQuestion>> GetExams();

        Task<SendExamResponse> SubmitExam(SubmitExamRequest req);

    }
}
