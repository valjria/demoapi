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
    public class TopicServiceTest
    { //Create a new inmemory database
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }
        //Create the mapper configuration
        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Topic, TopicDto>().ReverseMap();
                cfg.CreateMap<Course, CourseDto>().ReverseMap();
            });
            return config.CreateMapper();
        }

        [Fact]
        public async Task GetAllTopicsAsync_ShouldReturnAllTopics()
        { //Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new TopicService(context, mapper);
            var service2 = new CourseService(context, mapper);
            //Add test data to the database
            var courseDto = new CourseDto { CourseId = 1, CourseName = "Math 101", Description = "Basic Mathematics" };
            await service2.AddCourseAsync(courseDto);
            //  Add test data to the database
            var topicDto1 = new TopicDto { TopicId = 1, TopicName = "Introduction", CourseId = 1 };
            var topicDto2 = new TopicDto { TopicId = 2, TopicName = "Advanced", CourseId = 1 };

            context.Topics.AddRange(mapper.Map<Topic>(topicDto1), mapper.Map<Topic>(topicDto2));
            await context.SaveChangesAsync();

            var result = await service.GetAllTopicsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetTopicByIdAsync_ShouldReturnTopic_WhenTopicExists()
        { //Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new TopicService(context, mapper);
            //Add test data to the database
            var topicDto = new TopicDto { TopicId = 1, TopicName = "Introduction", CourseId = 1 };
            context.Topics.Add(mapper.Map<Topic>(topicDto));
            await context.SaveChangesAsync();
            //Execute the service method
            var result = await service.GetTopicByIdAsync(topicDto.TopicId);

            Assert.NotNull(result);
            Assert.Equal("Introduction", result.TopicName);
        }

        [Fact]
        public async Task AddTopicAsync_ShouldAddTopic()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new TopicService(context, mapper);
            //Add test data to the database
            var topicDto = new TopicDto { TopicName = "Introduction", CourseId = 1 };
            //Execute the service method
            var result = await service.AddTopicAsync(topicDto);

            Assert.NotNull(result);
            Assert.Equal("Introduction", result.TopicName);

            var topic = await context.Topics.FindAsync(result.TopicId);
            Assert.NotNull(topic);
        }

        [Fact]
        public async Task UpdateTopicAsync_ShouldUpdateExistingTopic()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new TopicService(context, mapper);
            //Add test data to the database
            var topicDto = new TopicDto { TopicId = 1, TopicName = "Introduction", CourseId = 1 };
            context.Topics.Add(mapper.Map<Topic>(topicDto));
            await context.SaveChangesAsync();
            // Execute the service method
            var updatedTopicDto = new TopicDto { TopicId = 1, TopicName = "Advanced", CourseId = 1 };
            //Execute the service method
            var result = await service.UpdateTopicAsync(updatedTopicDto);
            //
            Assert.NotNull(result);
            Assert.Equal("Advanced", result.TopicName);
            //Check if student is updated in the database
            var updatedTopic = await context.Topics.FindAsync(topicDto.TopicId);
            Assert.Equal("Advanced", updatedTopic.TopicName);
        }

        [Fact]
        public async Task DeleteTopicAsync_ShouldRemoveTopic()
        {//Create a new inmemory database
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new TopicService(context, mapper);
            //Add test data to the database
            var topicDto = new TopicDto { TopicId = 1, TopicName = "Introduction", CourseId = 1 };
            context.Topics.Add(mapper.Map<Topic>(topicDto));
            await context.SaveChangesAsync();
            //Execute the service method
            var result = await service.DeleteTopicAsync(topicDto.TopicId);

            Assert.True(result);

            var deletedTopic = await context.Topics.FindAsync(topicDto.TopicId);
            Assert.Null(deletedTopic);
        }
        [Fact]
        public async Task GetAllTopicsWithPaginationAsync_ShouldReturnCorrectPage()
        {
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new TopicService(context, mapper);

            // Arrange
            for (int i = 1; i <= 40; i++)
            {
                context.Topics.Add(new Topic
                {
                    TopicName = $"Topic {i}",
                    CourseId = 1
                });
            }
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetAllTopicsWithPaginationAsync(4, 10); // Fourth page, 10 items per page

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Items.Count()); // Page size should be 10
            Assert.Equal("Topic 31", result.Items.First().TopicName); // First item of page 4
        }

        [Fact]
        public async Task FilterTopicsAsync_ShouldReturnFilteredResults()
        {
            using var context = GetInMemoryDbContext();
            var mapper = GetMapper();
            var service = new TopicService(context, mapper);

            // Arrange
            context.Topics.AddRange(
                new Topic { TopicName = "Aviation Safety", CourseId = 1 },
                new Topic { TopicName = "Cockpit Procedures", CourseId = 2 },
                new Topic { TopicName = "Emergency Protocols", CourseId = 1 }
            );
            await context.SaveChangesAsync();

            // Act
            var result = await service.FilterTopicsAsync("Safety",1);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Only one topic contains "Safety"
            Assert.Equal("Aviation Safety", result.First().TopicName);
        }
    }
}
