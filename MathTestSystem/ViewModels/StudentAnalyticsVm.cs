using MathTestSystem.Application.DTO_s;

namespace MathTestSystem.Web.ViewModels
{
    public class StudentAnalyticsVm
    {
        public string StudentId { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public List<TaskResultDto> Results { get; set; } = new();
    }
}
