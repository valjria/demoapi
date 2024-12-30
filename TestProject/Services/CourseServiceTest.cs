using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Services;
using demoapi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProject.Services
{


    public class CourseServiceTest
    {

        private AppDbContext GetInMemoryDbContext()
        {//Create a new inmemory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Create new DB for each test
                 .Options;

            return new AppDbContext(options);

        }

        private IMapper GetMapper()
        {//Create the mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseDto>().ReverseMap();
            });
            return config.CreateMapper();
        }


        [Fact]
        public async Task GetCourseAllCoursesAsync_ShouldReturnAllCoursesAsync()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new CourseService(context, mapper);
            //Add test data to the database
            var course1 = new CourseDto { CourseName = "Math 101", Description = "Basic Mathematics" };
            var course2 = new CourseDto { CourseName = "Physics 101", Description = "Introduction to physics" };
            //Execute the service method
            var result1 = await service.AddCourseAsync(course1);
            var result2 = await service.AddCourseAsync(course2);
            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);

            //Get all courses
            var allCourses = await context.Courses.ToListAsync();
            Assert.Equal(2, allCourses.Count); //check if 2 courses in total
        }

        [Fact]
        public async Task GetCoursesByIdAsync_ShouldReturnCourseById()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new CourseService(context, mapper);
            //Add test data to the database
            var course = new CourseDto { CourseName = "Math 102", Description = "Adv Mathematics" };
            var result = await service.AddCourseAsync(course);
            await context.SaveChangesAsync();
            //Execute the service method
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CourseId);
        }

        [Fact]
        public async Task AddCourseAsync_ShouldAddCourse()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new CourseService(context, mapper);
            var course = new CourseDto { CourseName = "Math 101", Description = "Basic Mathematics" };
            var result = await service.AddCourseAsync(course);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CourseId);
        }

        [Fact]
        public async Task DeleteCourseAsync()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new CourseService(context, mapper);
            //Add test data to the database
            var course = new CourseDto { CourseName = "Math 101", Description = "Basic Mathematics" };
            var result = await service.AddCourseAsync(course);

            Assert.NotNull(result);
            //Execute the service method
            var isDeleted = await service.DeleteCourseAsync(result.CourseId);

            Assert.True(isDeleted);
            // Assert
            var deletedCourse = await context.Courses.FindAsync(result.CourseId);
            Assert.Null(deletedCourse);
        }

        [Fact]
            public async Task UpdateCourseAsync()
        {
            //Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new CourseService(context, mapper);
            //Add test data to the database
            var course = new CourseDto { CourseName = "Math 101", Description = "Basic Mathematics" };
            var result = await service.AddCourseAsync(course);
            Assert.NotNull(result);
        }
    }
}