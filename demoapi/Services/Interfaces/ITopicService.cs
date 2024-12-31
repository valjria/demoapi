using demoapi.DTO;

namespace demoapi.Services.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicDto>> GetAllTopicsAsync();
        Task<TopicDto> GetTopicByIdAsync(int id);
        Task<TopicDto> AddTopicAsync(TopicDto topicDto);
        Task<TopicDto> UpdateTopicAsync(TopicDto topicDto);

        Task<bool> DeleteTopicAsync(int id);
        Task<IEnumerable<TopicDto>> FilterTopicsAsync(string? topicName, int? courseId);
        Task<PaginationDto<TopicDto>> GetAllTopicsWithPaginationAsync(int page, int pageSize);

    }
}
