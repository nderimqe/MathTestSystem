using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string ExternalStudentId { get; set; }
        public string StudentName { get; set; }
        public List<Exam> Exams { get; set; } = new();
    }
}
