using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Services;
using demoapi.Services.Implementations;
using demoapi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProject.Services
{
    public class GradeServiceTest
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        private IMapper GetMapper()
        {//Create the mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Grade, GradeDto>().ReverseMap();
                cfg.CreateMap<Student, StudentDto>().ReverseMap();
                cfg.CreateMap<Course, CourseDto>().ReverseMap();
            });
            return config.CreateMapper();
        }

        [Fact]
        public async Task GetAllGradesAsync_ShouldReturnAllGrades()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new GradeService(context, mapper);
            //Add test data to the database
            var studentDto1 = new StudentDto { StudentId = 1, Name = "John Doe", Role = "Kabin" };
            var studentDto2 = new StudentDto { StudentId = 2, Name = "Jane Smith", Role = "Kokpit" };
            context.Students.AddRange(mapper.Map<Student>(studentDto1), mapper.Map<Student>(studentDto2));
            //Add test data to the database
            var courseDto1 = new CourseDto { CourseId = 1, CourseName = "Math 101", Description = "Basic Mathematics" };
            var courseDto2 = new CourseDto { CourseId = 2, CourseName = "Physics 101", Description = "Basic Physics" };
            context.Courses.AddRange(mapper.Map<Course>(courseDto1), mapper.Map<Course>(courseDto2));
            //Save the changes
            var grade1 = new Grade { GradeId = 1, StudentId = 1, CourseId = 1, Value = 85 };
            var grade2 = new Grade { GradeId = 2, StudentId = 2, CourseId = 2, Value = 90 };
            context.Grades.AddRange(grade1, grade2);
            //Save the changes
            await context.SaveChangesAsync();
            //Execute the service method
            var result = await service.GetAllGradesAsync();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetGradeByIdAsync_ShouldReturnGrade_WhenGradeExists()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new GradeService(context, mapper);
            //Add test data to the database
            var studentDto = new StudentDto { StudentId = 1, Name = "John Doe", Role = "Kabin" };
            var courseDto = new CourseDto { CourseId = 1, CourseName = "Math 101", Description = "Basic Mathematics" };
            context.Students.Add(mapper.Map<Student>(studentDto));
            context.Courses.Add(mapper.Map<Course>(courseDto));
            await context.SaveChangesAsync();
            //Add test data to the database
            var grade = new Grade { GradeId = 1, StudentId = 1, CourseId = 1, Value = 85 };
            context.Grades.Add(grade);
            await context.SaveChangesAsync();
            //Execute the service method
            var result = await service.GetGradeByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(85, result.Value);
        }

        [Fact]
        public async Task AddGradeAsync_ShouldAddGrade()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new GradeService(context, mapper);
            //Add test data to the database
            var studentDto = new StudentDto { StudentId = 1, Name = "John Doe", Role = "Kabin" };
            var courseDto = new CourseDto { CourseId = 1, CourseName = "Math 101", Description = "Basic Mathematics" };
            context.Students.Add(mapper.Map<Student>(studentDto));
            context.Courses.Add(mapper.Map<Course>(courseDto));
            await context.SaveChangesAsync();
            //Add test data to the database
            var gradeDto = new GradeDto { StudentId = 1, CourseId = 1, Value = 85 };
            //Execute the service method
            var result = await service.AddGradeAsync(gradeDto);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(85, result.Value);
        }

        [Fact]
        public async Task UpdateGradeAsync_ShouldUpdateExistingGrade()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new GradeService(context, mapper);
            //Add test data to the database
            var studentDto = new StudentDto { StudentId = 1, Name = "John Doe", Role = "Kabin" };
            var courseDto = new CourseDto { CourseId = 1, CourseName = "Math 101", Description = "Basic Mathematics" };
            context.Students.Add(mapper.Map<Student>(studentDto));
            context.Courses.Add(mapper.Map<Course>(courseDto));
            await context.SaveChangesAsync();
            //Add test data to the database
            var grade = new Grade { GradeId = 1, StudentId = 1, CourseId = 1, Value = 85 };
            context.Grades.Add(grade);
            await context.SaveChangesAsync();
            //Execute the service method
            var gradeDto = new GradeDto { GradeId = 1, StudentId = 1, CourseId = 1, Value = 90 };
            //Assert
            var result = await service.UpdateGradeAsync(gradeDto);

            Assert.NotNull(result);
            Assert.Equal(90, result.Value);
        }

        [Fact]
        public async Task DeleteGradeAsync_ShouldRemoveGrade()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new GradeService(context, mapper);
            //Add test data to the database
            var studentDto = new StudentDto { StudentId = 1, Name = "John Doe", Role = "Kabin" };
            var courseDto = new CourseDto { CourseId = 1, CourseName = "Math 101", Description = "Basic Mathematics" };
            context.Students.Add(mapper.Map<Student>(studentDto));
            context.Courses.Add(mapper.Map<Course>(courseDto));
            await context.SaveChangesAsync();
            //Add test data to the database
            var grade = new Grade { GradeId = 1, StudentId = 1, CourseId = 1, Value = 85 };
            context.Grades.Add(grade);
            await context.SaveChangesAsync();
            //Execute the service method
            var result = await service.DeleteGradeAsync(1);

            Assert.True(result);

            var deletedGrade = await context.Grades.FindAsync(1);
            Assert.Null(deletedGrade);
        }
    }
}
