using class_room.Models;
using class_room.UnitOfWork.Interface;
using System.Text.Json;

namespace class_room.UnitOfWork
{
    public class UowClassRoom : IUowClassRoom
    {
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<ExamQuestion>> GetExams()
        {
            var jsonString = await File.ReadAllTextAsync("Masters/master_questions.json");

            var questions = JsonSerializer.Deserialize<List<ExamQuestion>>(
                jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                }
            );

            return questions ?? new List<ExamQuestion>();
        }

        public async Task<SendExamResponse> SubmitExam(SubmitExamRequest req)
        {
            var questions = await GetExams();
            int correctAnswers = 0;
            var fullName = req.FullName;

            #region "=> ตรวจคำตอบ"
            foreach (var answer in req.Answers)
            {
                var question = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question != null && question.Correct_Answer == answer.SelectedAnswer)
                {
                    correctAnswers++;
                }
            }
            #endregion

            #region "=> คำนวณคะแนน"
            var score = (double)correctAnswers / questions.Count * 100;
            var resultMessage =
                $"Student {fullName} scored {score}% ({correctAnswers} out of {questions.Count} correct).";
            #endregion

            #region "=> โฟลเดอร์เก็บผลสอบ"
            var folderPath = Path.Combine("ExamHistory");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            #endregion

            #region "=> path ไฟล์ผลสอบ"
            var filePath = Path.Combine(folderPath, $"{fullName}.txt");
            #endregion

            #region "=> จำนวนครั้งที่สอบ"
            int attemptNumber = 1;
            if (File.Exists(filePath))
            {
                var existingLines = await File.ReadAllLinesAsync(filePath);
                attemptNumber = existingLines.Count(l => l.StartsWith("Attempt")) + 1;
            }
            #endregion

            #region "=> เนื้อหาในไฟล์"
            var sb = new System.Text.StringBuilder();

            if (!File.Exists(filePath))
            {
                sb.AppendLine($"Full Name: {fullName}");
                sb.AppendLine("********************************");
            }

            sb.AppendLine($"Attempt: {attemptNumber}");
            sb.AppendLine($"Date: {DateTime.Now}");
            sb.AppendLine($"Score: {score}% ({correctAnswers}/{questions.Count})");
            sb.AppendLine("Answers:");
            foreach (var answer in req.Answers)
            {
                var question = questions.First(q => q.Id == answer.QuestionId);
                sb.AppendLine(
                    $"Q{question.Id}: selected={answer.SelectedAnswer}, correct={question.Correct_Answer}"
                );
            }

            sb.AppendLine("********************************");
            sb.AppendLine();
            #endregion

            // เขียนข้อมูลงในไฟล์เดิม ด้วยการต่อท้าย จำนวนครั้งที่สอบ
            await File.AppendAllTextAsync(filePath, sb.ToString());

            return new SendExamResponse
            {
                Success = true,
                Message = resultMessage,
                AttemptNumber = attemptNumber,
                Score = score
            };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    return;
                }

            }
            this.disposed = true;
        }
    }
}