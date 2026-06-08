using MathTestSystem.Application.Interfaces;
using MathTestSystem.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MathTestSystem.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IExamResultRepository _examResultRepository;

        public StudentController(IExamResultRepository examResultRepository)
        {
            _examResultRepository = examResultRepository;
        }

        public async Task<IActionResult> Analytics(string? studentId)
        {
            var model = new StudentAnalyticsVm
            {
                StudentId = studentId ?? string.Empty
            };

            if (string.IsNullOrWhiteSpace(studentId))
            {
                return View(model);
            }

            model.Results = await _examResultRepository.GetResultsByStudentAsync(studentId.Trim());

            if (!model.Results.Any())
            {
                model.ErrorMessage = "No exam results found for this student ID.";
            }

            return View(model);
        }
    }
}
