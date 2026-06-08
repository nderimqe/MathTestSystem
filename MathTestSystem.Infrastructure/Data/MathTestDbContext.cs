using MathTestSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathTestSystem.Infrastructure.Data
{
    public class MathTestDbContext : DbContext
    {
        public MathTestDbContext(DbContextOptions<MathTestDbContext> options)
            : base(options)
        {
        }

        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .Property(x => x.ExternalTeacherId)
                .HasMaxLength(50);

            modelBuilder.Entity<Teacher>()
                .Property(x => x.TeacherName)
                .HasMaxLength(200);

            modelBuilder.Entity<Student>()
                .Property(x => x.ExternalStudentId)
                .HasMaxLength(50);

            modelBuilder.Entity<Student>()
                .Property(x => x.StudentName)
                .HasMaxLength(200);

            modelBuilder.Entity<Exam>()
                .Property(x => x.ExternalExamId)
                .HasMaxLength(50);

            modelBuilder.Entity<Exam>()
                .HasOne(x => x.Teacher)
                .WithMany(x => x.Exams)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exam>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Exams)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskItem>()
                .Property(x => x.Expression)
                .HasMaxLength(500);
        }
    }
}
