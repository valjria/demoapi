using demoapi.DTO;
using demoapi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllTopicsWithPagination(int page = 1, int pageSize = 20)
        {
            var result = await _topicService.GetAllTopicsWithPaginationAsync(page, pageSize);
            return Ok(result);
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
            if (topic == null) return NotFound();

            return Ok(topic);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> FilterTopics([FromQuery] string? topicName, [FromQuery] int? courseId)
        {
            var topics = await _topicService.FilterTopicsAsync(topicName, courseId);
            if (!topics.Any())
            {
                return NotFound("No topics match the given criteria.");
            }
            return Ok(topics);
        }

        [HttpPost]
        public async Task<IActionResult> AddTopic([FromBody] TopicDto topicDto)
        {
            if (topicDto == null) return BadRequest("Konu bilgileri eksik.");

            var addedTopic = await _topicService.AddTopicAsync(topicDto);
            return CreatedAtAction(nameof(GetTopic), new { id = addedTopic.TopicId }, addedTopic);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopic(int id, [FromBody] TopicDto topicDto)
        {
            if (topicDto == null || topicDto.TopicId != id)
                return BadRequest("Geçersiz veri veya ID eşleşmiyor.");

            var updatedTopic = await _topicService.UpdateTopicAsync(topicDto);
            if (updatedTopic == null) return NotFound("Konu bulunamadı.");

            return Ok(updatedTopic);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var isDeleted = await _topicService.DeleteTopicAsync(id);
            if (!isDeleted) return NotFound("Konu bulunamadı.");

            return NoContent();
        }
    }
}
