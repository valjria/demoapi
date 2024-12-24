using AutoMapper;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Repositories.Interfaces;
using demoapi.Services.Interfaces;

namespace demoapi.Services.Implementations
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicService(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TopicDto>> GetAllTopicsAsync()
        {
            var topics = await _topicRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TopicDto>>(topics);
        }

        public async Task<TopicDto> GetTopicByIdAsync(int id)
        {
            var topic = await _topicRepository.GetByIdAsync(id);
            return topic == null ? null : _mapper.Map<TopicDto>(topic);
        }

        public async Task<TopicDto> AddTopicAsync(TopicDto topicDto)
        {
            var topic = _mapper.Map<Topic>(topicDto);
            var addedTopic = await _topicRepository.AddAsync(topic);
            return _mapper.Map<TopicDto>(addedTopic);
        }

        public async Task<bool> DeleteTopicAsync(int id)
        {
            return await _topicRepository.DeleteAsync(id);
        }
    }
}