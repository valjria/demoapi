using demoapi.DTO;

namespace demoapi.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<CourseDto> AddCourseAsync(CourseDto courseDto);
        Task<bool> DeleteCourseAsync(int id);
    }
}
