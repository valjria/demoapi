using AutoMapper;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Repository.Interfaces;
using demoapi.Services.Interfaces;

namespace demoapi.Services.Implementations
{
    public class CourseService: ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return course == null ? null : _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> AddCourseAsync(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var addedCourse = await _courseRepository.AddAsync(course);
            return _mapper.Map<CourseDto>(addedCourse);
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            return await _courseRepository.DeleteAsync(id);
        }
    }
}
