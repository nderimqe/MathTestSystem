using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Application.DTO_s
{
    public class TaskResultDto
    {
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string StudentId { get; set; }
            public string StudentName { get; set; }
            public string ExamId { get; set; }
            public string TaskId { get; set; }
            public string Expression { get; set; }
            public double Expected { get; set; }
            public double Calculated { get; set; }
            public bool IsCorrect { get; set; }
        
    }
}
