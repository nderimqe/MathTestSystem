using MathTestSystem.Application.DTO_s;
using MathTestSystem.Application.Interfaces;
using MathTestSystem.Domain.Entities;
using MathTestSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MathTestSystem.Infrastructure.Repositories
{
    public class ExamResultRepository : IExamResultRepository
    {
        private readonly MathTestDbContext _context;

        public ExamResultRepository(MathTestDbContext context)
        {
            _context = context;
        }

        public async Task SaveResultsAsync(List<TaskResultDto> results)
        {
            if (results.Count == 0)
            {
                return;
            }

            foreach (var teacherGroup in results.GroupBy(x => new { x.TeacherId, x.TeacherName }))
            {
                var teacher = await GetOrCreateTeacherAsync(teacherGroup.Key.TeacherId, teacherGroup.Key.TeacherName);

                foreach (var studentGroup in teacherGroup.GroupBy(x => new { x.StudentId, x.StudentName, x.ExamId }))
                {
                    var student = await GetOrCreateStudentAsync(studentGroup.Key.StudentId, studentGroup.Key.StudentName);
                    var exam = new Exam
                    {
                        ExternalExamId = studentGroup.Key.ExamId ?? string.Empty,
                        Teacher = teacher,
                        Student = student,
                        TotalScore = studentGroup.Count(x => x.IsCorrect) * 100.0 / studentGroup.Count()
                    };

                    foreach (var result in studentGroup)
                    {
                        exam.Tasks.Add(new TaskItem
                        {
                            TaskNumber = ParseTaskNumber(result.TaskId),
                            Expression = result.Expression ?? string.Empty,
                            ExpectedResult = result.Expected,
                            CalculatedResult = result.Calculated,
                            IsCorrect = result.IsCorrect
                        });
                    }

                    _context.Exams.Add(exam);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskResultDto>> GetResultsAsync()
        {
            return await BuildResultsQuery().ToListAsync();
        }

        public async Task<List<TaskResultDto>> GetResultsByStudentAsync(string studentId)
        {
            return await BuildResultsQuery()
                .Where(x => x.StudentId == studentId)
                .ToListAsync();
        }

        private IQueryable<TaskResultDto> BuildResultsQuery()
        {
            return _context.Tasks
                .AsNoTracking()
                .Include(x => x.Exam)
                .ThenInclude(x => x.Teacher)
                .Include(x => x.Exam)
                .ThenInclude(x => x.Student)
                .Select(x => new TaskResultDto
                {
                    TeacherId = x.Exam.Teacher.ExternalTeacherId,
                    TeacherName = x.Exam.Teacher.TeacherName,
                    StudentId = x.Exam.Student.ExternalStudentId,
                    StudentName = x.Exam.Student.StudentName,
                    ExamId = x.Exam.ExternalExamId,
                    TaskId = x.TaskNumber.ToString(),
                    Expression = x.Expression,
                    Expected = x.ExpectedResult,
                    Calculated = x.CalculatedResult,
                    IsCorrect = x.IsCorrect
                });
        }

        private async Task<Teacher> GetOrCreateTeacherAsync(string? externalId, string? name)
        {
            var normalizedId = externalId ?? string.Empty;
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.ExternalTeacherId == normalizedId);

            if (teacher != null)
            {
                return teacher;
            }

            teacher = new Teacher
            {
                ExternalTeacherId = normalizedId,
                TeacherName = name ?? string.Empty
            };

            _context.Teachers.Add(teacher);
            return teacher;
        }

        private async Task<Student> GetOrCreateStudentAsync(string? externalId, string? name)
        {
            var normalizedId = externalId ?? string.Empty;
            var student = await _context.Students.FirstOrDefaultAsync(x => x.ExternalStudentId == normalizedId);

            if (student != null)
            {
                return student;
            }

            student = new Student
            {
                ExternalStudentId = normalizedId,
                StudentName = name ?? string.Empty
            };

            _context.Students.Add(student);
            return student;
        }

        private static int ParseTaskNumber(string? taskId)
        {
            return int.TryParse(taskId, out var taskNumber) ? taskNumber : 0;
        }
    }
}
