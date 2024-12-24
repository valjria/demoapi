using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demoapi.Data;
using demoapi.Models;
using demoapi.DTO;
using AutoMapper;
using demoapi.Services.Interfaces;

namespace demoapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopics()
        {
            var topics = await _topicService.GetAllTopicsAsync();
            return Ok(topics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopic(int id)
        {
            var topic = await _topicService.GetTopicByIdAsync(id);
            if (topic == null)
                return NotFound();

            return Ok(topic);
        }

        [HttpPost]
        public async Task<IActionResult> AddTopic([FromBody] TopicDto topicDto)
        {
            if (topicDto == null)
                return BadRequest("Konu bilgileri eksik.");

            var addedTopic = await _topicService.AddTopicAsync(topicDto);
            return CreatedAtAction(nameof(GetTopic), new { id = addedTopic.TopicId }, addedTopic);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var isDeleted = await _topicService.DeleteTopicAsync(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}

