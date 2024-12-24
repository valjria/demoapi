using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demoapi.Data;
using demoapi.Models;
using demoapi.DTO;
using AutoMapper;

[ApiController]
[Route("api/courses/{courseId}/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TopicsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTopics(int courseId)
    {
        var topics = await _context.Topics
            .Where(t => t.CourseId == courseId)
            .ToListAsync();

        // Topics listesini DTO'ya mapliyoruz
        var topicDtos = _mapper.Map<List<TopicDto>>(topics);

        return Ok(topicDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTopic(int courseId, [FromBody] TopicDto topicDto)
    {
        if (topicDto == null)
        {
            return BadRequest("Konu bilgileri eksik.");
        }

        // DTO'dan modele dönüşüm
        var topic = _mapper.Map<Topic>(topicDto);
        topic.CourseId = courseId;

        _context.Topics.Add(topic);
        await _context.SaveChangesAsync();

        // Eklenen konuyu DTO'ya mapleyerek döndür
        var createdTopicDto = _mapper.Map<TopicDto>(topic);

        return CreatedAtAction(nameof(GetTopics), new { courseId = courseId }, createdTopicDto);
    }
}
