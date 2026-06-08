using MathTestSystem.Application.DTO_s;

namespace MathTestSystem.Web.ViewModels
{
    public class UploadExamVm
    {
      
        public List<TaskResultDto> Results { get; set; } = new();
        public List<StudentAnalyticsDto> Analytics { get; set; } = new();

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
