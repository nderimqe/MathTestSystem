using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTestSystem.Domain.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string ExternalTeacherId { get; set; }
        public string TeacherName { get; set; }

        public List<Exam> Exams { get; set; } = new();
    }
}
