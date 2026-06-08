using MathTestSystem.Application.DTO_s;
using MathTestSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        public List<StudentAnalyticsDto> BuildAnalytics(List<TaskResultDto> results)
        {
            return results
                .GroupBy(x => x.StudentId)
                .Select(g => new StudentAnalyticsDto
                {
                    StudentId = g.Key,
                    TotalTasks = g.Count(),
                    CorrectTasks = g.Count(x => x.IsCorrect),
                    IncorrectTasks = g.Count(x => !x.IsCorrect),
                    ScorePercentage = (double)g.Count(x => x.IsCorrect) / g.Count() * 100
                })
                .ToList();
        }
    }
}
