using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Application.DTO_s
{
    public class StudentAnalyticsDto
    {
        public string StudentId { get; set; }

        public int TotalTasks { get; set; }

        public int CorrectTasks { get; set; }

        public int IncorrectTasks { get; set; }

        public double ScorePercentage { get; set; }
    }
}
