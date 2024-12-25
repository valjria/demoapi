using demoapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

/* namespace MyProject.Models
{
    public class EducationDbContext : DbContext
    {
        public EducationDbContext(DbContextOptions<EducationDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Course Entity Configuration
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Topics)
                .WithOne(t => t.Course)
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Grades)
                .WithOne(g => g.Course)
                .HasForeignKey(g => g.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Topic Entity Configuration
            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Topics)
                .HasForeignKey(t => t.CourseId);

            // Student Entity Configuration
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.Student)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Grade Entity Configuration
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Course)
                .WithMany(c => c.Grades)
                .HasForeignKey(g => g.CourseId);
        }
    }
}  */
namespace demoapi.Data
{
    public static class EducationDbSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseName = "Aviation Safety and Operations", Description = "An introductory course on aviation safety and cockpit operations." },
                new Course { CourseId = 2, CourseName = "Aerodynamics", Description = "Basics of aerodynamics and flight principles." }
            );

            // Seed Topics
            modelBuilder.Entity<Topic>().HasData(
                new Topic { TopicId = 1, TopicName = "Aviation Safety Basics", CourseId = 1 },
                new Topic { TopicId = 2, TopicName = "Cockpit Procedures", CourseId = 1 },
                new Topic { TopicId = 3, TopicName = "Emergency Protocols", CourseId = 1 },
                new Topic { TopicId = 4, TopicName = "Lift and Drag", CourseId = 2 },
                new Topic { TopicId = 5, TopicName = "Flight Maneuvers", CourseId = 2 }
            );

            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, Name = "John Doe", Role = "Kokpit" },
                new Student { StudentId = 2, Name = "Jane Smith", Role = "Kabin" }
            );

            // Seed Grades
            modelBuilder.Entity<Grade>().HasData(
                new Grade { GradeId = 1, StudentId = 1, CourseId = 1, Value = 85 },
                new Grade { GradeId = 2, StudentId = 2, CourseId = 1, Value = 90 },
                new Grade { GradeId = 3, StudentId = 1, CourseId = 2, Value = 88 },
                new Grade { GradeId = 4, StudentId = 2, CourseId = 2, Value = 92 }
            );
        }
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationships
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Topics)
                .WithOne(t => t.Course)
                .HasForeignKey(t => t.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Grades)
                .WithOne(g => g.Course)
                .HasForeignKey(g => g.CourseId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.Student)
                .HasForeignKey(g => g.StudentId);

            modelBuilder.Entity<Topic>()
                 .HasOne(t => t.Course)
                 .WithMany(c => c.Topics)
                 .HasForeignKey(t => t.CourseId);
            // Seed Data
            EducationDbSeeder.Seed(modelBuilder);
        }
    }
}

