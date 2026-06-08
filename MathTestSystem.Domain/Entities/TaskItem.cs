using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Domain.Entities
{

        public class TaskItem
        {
            public int Id { get; set; }

            public int ExamId { get; set; }
            public Exam Exam { get; set; }

            public int TaskNumber { get; set; }

            public string Expression { get; set; }   // "2+3/6-4"
            public double ExpectedResult { get; set; }

            public double CalculatedResult { get; set; }
            public bool IsCorrect { get; set; }
        }
    }

