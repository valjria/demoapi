using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProject.Services
{
    public class ReportServiceTest
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReportDto, ReportDto>().ReverseMap();
            });
            return config.CreateMapper();
        }

        [Fact]
        public async Task GetAllReportsAsync_ShouldReturnAllReports()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new ReportService(context, mapper);

            // Add test data to Students table
            var student1 = new Student { StudentId = 1, Name = "John Doe", Role = "Kabin" };
            var student2 = new Student { StudentId = 2, Name = "Jane Smith", Role = "Kokpit" };
            context.Students.AddRange(student1, student2);

            // Add test data to Courses table
            var course1 = new Course { CourseId = 1, CourseName = "Math 101", Description = "Basic Mathematics" };
            var course2 = new Course { CourseId = 2, CourseName = "Physics 101", Description = "Basic Physics" };
            context.Courses.AddRange(course1, course2);

            // Add test data to Grades table
            var grade1 = new Grade { GradeId = 1, StudentId = 1, CourseId = 1, Value = 85 };
            var grade2 = new Grade { GradeId = 2, StudentId = 2, CourseId = 2, Value = 90 };
            context.Grades.AddRange(grade1, grade2);

            await context.SaveChangesAsync();

            // Execute the service method
            var result = await service.GetAllReportsAsync();

            // Validate the results
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            var firstReport = result.First();
            Assert.Equal("John Doe", firstReport.StudentName);
            Assert.Equal("Math 101", firstReport.CourseName);
            Assert.Equal(85, firstReport.Grade);
        }
    }
}
