using MathTestSystem.Application.DTO_s;
using MathTestSystem.Application.Interfaces;
using System.Globalization;
using System.Xml.Linq;

namespace MathTestSystem.Application.Services
{
    public class XMLService : IXMLService
    {
        private readonly IMathProcessor _mathProcessor;

        public XMLService(IMathProcessor mathProcessor)
        {
            _mathProcessor = mathProcessor;
        }

        public List<TaskResultDto> Process(Stream xmlStream)
        {
            xmlStream.Position = 0;

            var results = new List<TaskResultDto>();
            var doc = XDocument.Load(xmlStream);

            var teacherElement = doc.Element("Teacher");

            if (teacherElement == null)
            {
                throw new Exception("Invalid XML structure: Missing <Teacher>");
            }

            var teacherId = teacherElement.Attribute("ID")?.Value;
            var teacherName = teacherElement.Element("Name")?.Value;
            var students = teacherElement.Element("Students")?.Elements("Student");

            if (students == null)
            {
                throw new Exception("Invalid XML structure: Missing <Students>");
            }

            foreach (var student in students)
            {
                var studentId = student.Attribute("ID")?.Value;
                var studentName = student.Element("Name")?.Value;

                foreach (var exam in student.Elements("Exam"))
                {
                    var examId = exam.Attribute("Id")?.Value;

                    foreach (var task in exam.Elements("Task"))
                    {
                        var raw = task.Value?.Trim();
                        if (string.IsNullOrEmpty(raw))
                        {
                            continue;
                        }

                        var parts = raw.Split('=');
                        if (parts.Length != 2)
                        {
                            continue;
                        }

                        var expression = parts[0].Trim();
                        var submittedAnswer = double.Parse(parts[1].Trim(), CultureInfo.InvariantCulture);
                        var calculated = _mathProcessor.Evaluate(expression);

                        results.Add(new TaskResultDto
                        {
                            TeacherId = teacherId,
                            TeacherName = teacherName,
                            StudentId = studentId,
                            ExamId = examId,
                            StudentName = studentName,
                            TaskId = task.Attribute("id")?.Value,
                            Expression = expression,
                            Expected = submittedAnswer,
                            Calculated = calculated,
                            IsCorrect = Math.Abs(calculated - submittedAnswer) < 0.0001
                        });
                    }
                }
            }

            return results;
        }
    }
}
