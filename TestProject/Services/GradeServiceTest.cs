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
        /* [Fact]
         public async Task GetAllGradesWithPaginationAsync_ShouldReturnCorrectPage()
         {
             using var context = GetInMemoryDbContext();
             var mapper = GetMapper();
             var service = new GradeService(context, mapper);

             // Arrange
             for (int i = 1; i <= 30; i++)
             {
                 context.Grades.Add(new Grade
                 {
                     Value = i,
                     StudentId = 1,
                     CourseId = 1
                 });
             }
             await context.SaveChangesAsync();

             // Act
             var result = await service.GetAllGradesWithPaginationAsync(2, 10); //Second page, 10 items per page

             // Assert
             Assert.NotNull(result);
             Assert.Equal(10, result.Items.Count()); //Page size should be 10
             Assert.Equal(11, result.Items.First().Value); //First item of page 2
         } */

        /* [Fact]
        public async Task FilterGradesAsync_ShouldReturnFilteredGrades()
        {
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new GradeService(context, mapper);

            // Arrange
            context.Students.Add(new Student { StudentId = 1, Name = "John Doe", Role = "Pilot" });
            context.Students.Add(new Student { StudentId = 2, Name = "Jane Smith", Role = "Engineer" });

            context.Courses.Add(new Course { CourseId = 1, CourseName = "Math 101", Description = "Basic Mathematics" });
            context.Courses.Add(new Course { CourseId = 2, CourseName = "Physics 101", Description = "Introduction to Physics" });

            context.Grades.AddRange(
                new Grade { GradeId = 1, Value = 95, StudentId = 1, CourseId = 1 },
                new Grade { GradeId = 2, Value = 85, StudentId = 2, CourseId = 1 },
                new Grade { GradeId = 3, Value = 75, StudentId = 1, CourseId = 2 },
                new Grade { GradeId = 4, Value = 65, StudentId = 2, CourseId = 2 }
            );

            await context.SaveChangesAsync();

            // Act
            var result = await service.FilterGradesAsync(null, 1, 80, 100); //Filter: courseId=1, minValue=80, maxValue=100

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); //85 olmalı, çünkü sadece courseId=1 ve 80-100 arasında 1 kayıt var
            Assert.Equal(85, result.First().Value); //Expecting
        } */


    }
}
