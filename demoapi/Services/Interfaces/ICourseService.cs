using demoapi.DTO;

namespace demoapi.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<CourseDto> AddCourseAsync(CourseDto courseDto);
        Task<CourseDto> UpdateCourseAsync(CourseDto courseDto);
        Task<bool> DeleteCourseAsync(int id);
        Task<IEnumerable<CourseDto>> FilterCoursesAsync(string? courseName, string? description);
        Task<PaginationDto<CourseDto>> GetAllCoursesWithPaginationAsync(int page, int pageSize);
        
    }
}
