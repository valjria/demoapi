using demoapi.Models;

namespace demoapi.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Topic>> GetAllAsync();
        Task<Topic> GetByIdAsync(int id);
        Task<Topic> AddAsync(Topic topic);
        Task<bool> DeleteAsync(int id);
    }
}