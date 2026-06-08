using MathTestSystem.Application.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Application.Interfaces
{
    public interface IAnalyticsService
    {
        List<StudentAnalyticsDto> BuildAnalytics(List<TaskResultDto> results);
    }
}
