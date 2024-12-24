using demoapi.Models;

namespace demoapi.Repository.Interfaces
{
    public interface ICourseRepository
    {

        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task<Course> AddAsync(Course course);
        Task<bool> DeleteAsync(int id);

    }
}
