﻿using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Services;
using demoapi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProject.Services
{
    public class StudentServiceTest
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
                cfg.CreateMap<Student, StudentDto>().ReverseMap();
            });
            return config.CreateMapper();
        }

        [Fact]
        public async Task GetAllStudentsAsync_ShouldReturnAllStudents()
        {
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new StudentService(context, mapper);

            var studentDto1 = new StudentDto { StudentId = 1, Name = "John Doe", Role = "Kabin" };
            var studentDto2 = new StudentDto { StudentId = 2, Name = "Jane Smith", Role = "Kokpit" };

            context.Students.AddRange(mapper.Map<Student>(studentDto1), mapper.Map<Student>(studentDto2));
            await context.SaveChangesAsync();

            var studentsInDb = await context.Students.ToListAsync();
            Assert.Equal(2, studentsInDb.Count); // Ensure students are in the database

            var result = await service.GetAllStudentsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetStudentByIdAsync_ShouldReturnStudent_WhenStudentExists()
        {
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new StudentService(context, mapper);

            var studentDto = new StudentDto { StudentId = 1, Name = "John Doe", Role = "Kabin" };
            context.Students.Add(mapper.Map<Student>(studentDto));
            await context.SaveChangesAsync();

            var result = await service.GetStudentByIdAsync(studentDto.StudentId);

            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public async Task AddStudentAsync_ShouldAddStudent()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new StudentService(context, mapper);
            //Add test data to the database
            var studentDto = new StudentDto { Name = "John Doe", Role = "Kabin" };
            //Execute the service method
            var result = await service.AddStudentAsync(studentDto);

            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            //Check if student is in the database
            var student = await context.Students.FindAsync(result.StudentId);
            Assert.NotNull(student);
        }

        [Fact]
        public async Task UpdateStudentAsync_ShouldUpdateExistingStudent()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new StudentService(context, mapper);
            //Add test data to the database
            var studentDto = new StudentDto { StudentId = 1, Name = "John Doe", Role = "Kabin" };
            context.Students.Add(mapper.Map<Student>(studentDto));
            await context.SaveChangesAsync();
            // Execute the service method
            var updatedStudentDto = new StudentDto { StudentId = 1, Name = "Jane Smith", Role = "Kokpit" };

            var result = await service.UpdateStudentAsync(updatedStudentDto);

            Assert.NotNull(result);
            Assert.Equal("Jane Smith", result.Name);
            //Check if student is updated in the database
            var updatedStudent = await context.Students.FindAsync(studentDto.StudentId);
            Assert.Equal("Jane Smith", updatedStudent.Name);
        }

        [Fact]
        public async Task DeleteStudentAsync_ShouldRemoveStudent()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new StudentService(context, mapper);
            //Add test data to the database
            var studentDto = new StudentDto { StudentId = 1, Name = "John Doe", Role = "Kabin" };
            context.Students.Add(mapper.Map<Student>(studentDto));
            await context.SaveChangesAsync();
            //Execute the service method
            var result = await service.DeleteStudentAsync(studentDto.StudentId);

            Assert.True(result);
            //Check if student is deleted from the database
            var deletedStudent = await context.Students.FindAsync(studentDto.StudentId);
            Assert.Null(deletedStudent);
        }
    }
}
