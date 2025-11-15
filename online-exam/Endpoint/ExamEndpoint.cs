using class_room.Models;
using class_room.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace class_room.Endpoint
{
    public static class ExamEndpoint
    {
        public static void Registers(WebApplication app)
        {
            // ดึงข้อสอบจาก JSON 
            app.MapGet("/api/get_exam", async () =>
            {
                var uow = new UowClassRoom();
                var res = await uow.GetExams();

                return Results.Json(res);
            })
                .WithName("GetExams")
                .WithTags("ClassRoom")
                .Produces<List<ExamQuestion>>(StatusCodes.Status200OK, "application/json");

            // ส่งคำตอบข้อสอบ
            app.MapPost("/api/submit_exam", async ([FromBody] SubmitExamRequest req) =>
            {
                var uow = new UowClassRoom();
                var res = await uow.SubmitExam(req);

                return Results.Json(res);
            })
                .WithName("SubmitExam")
                .WithTags("ClassRoom")
                .Accepts<SubmitExamRequest>("application/json")
                .Produces<SendExamResponse>(StatusCodes.Status200OK, "application/json");

        }
    }
}
