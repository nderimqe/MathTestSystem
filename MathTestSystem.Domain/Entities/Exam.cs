using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Domain.Entities
{
    public class Exam
    {
        public int Id { get; set; }

        public string ExternalExamId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public List<TaskItem> Tasks { get; set; } = new();

        public double TotalScore { get; set; }
    }
}
