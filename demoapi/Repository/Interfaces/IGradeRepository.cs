using demoapi.Models;

namespace demoapi.Repositories.Interfaces
{
    public interface IGradeRepository
    {
        Task<IEnumerable<Grade>> GetAllAsync();
        Task<Grade> GetByIdAsync(int id);
        Task<Grade> AddAsync(Grade grade);
        Task<bool> DeleteAsync(int id);
    }
}