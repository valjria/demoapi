using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace demoapi.Services.Implementations
{
    public class TopicService : ITopicService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TopicService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginationDto<TopicDto>> GetAllTopicsWithPaginationAsync(int page, int pageSize)
        {
            var query = _context.Topics.AsQueryable();

            var totalCount = await query.CountAsync();
            var topics = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationDto<TopicDto>
            {
                Items = _mapper.Map<IEnumerable<TopicDto>>(topics),
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<IEnumerable<TopicDto>> GetAllTopicsAsync()
        {
            var topics = await _context.Topics.Include(t => t.Course)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TopicDto>>(topics);
        }
        public async Task<IEnumerable<TopicDto>> FilterTopicsAsync(string? topicName, int? courseId)
        {
            var query = _context.Topics.AsQueryable();

            if (!string.IsNullOrEmpty(topicName))
            {
                query = query.Where(t => t.TopicName.Contains(topicName));
            }
            if (courseId.HasValue)
            {
                query = query.Where(t => t.CourseId == courseId.Value);
            }

            var topics = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TopicDto>>(topics);
        }

        public async Task<TopicDto> GetTopicByIdAsync(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null) return null;

            return _mapper.Map<TopicDto>(topic);
        }

        public async Task<TopicDto> AddTopicAsync(TopicDto topicDto)
        {
            var topic = _mapper.Map<Topic>(topicDto);
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return _mapper.Map<TopicDto>(topic);
        }

        public async Task<bool> DeleteTopicAsync(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null) return false;

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TopicDto> UpdateTopicAsync(TopicDto topicDto)
        {
            var topic = await _context.Topics.FindAsync(topicDto.TopicId);
            if (topic == null) return null;

            _mapper.Map(topicDto, topic);
            await _context.SaveChangesAsync();

            return _mapper.Map<TopicDto>(topic);
        }
    }
}
