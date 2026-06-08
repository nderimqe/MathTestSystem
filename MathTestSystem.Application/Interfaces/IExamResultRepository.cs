using MathTestSystem.Application.DTO_s;

namespace MathTestSystem.Application.Interfaces
{
    public interface IExamResultRepository
    {
        Task SaveResultsAsync(List<TaskResultDto> results);
        Task<List<TaskResultDto>> GetResultsAsync();
        Task<List<TaskResultDto>> GetResultsByStudentAsync(string studentId);
    }
}
