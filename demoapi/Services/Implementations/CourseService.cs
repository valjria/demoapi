using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CourseService : ICourseService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CourseService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _context.Courses.ToListAsync();
        return _mapper.Map<IEnumerable<CourseDto>>(courses); // Model'den DTO'ya dönüşüm
    }

    public async Task<CourseDto> GetCourseByIdAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) return null;

        return _mapper.Map<CourseDto>(course); // Model'den DTO'ya dönüşüm
    }

    public async Task<CourseDto> AddCourseAsync(CourseDto courseDto)
    {
        var course = _mapper.Map<Course>(courseDto); // DTO'dan Model'e dönüşüm
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return _mapper.Map<CourseDto>(course); // Model'den DTO'ya dönüşüm
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null) return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CourseDto> UpdateCourseAsync(CourseDto courseDto)
    {
        var course = await _context.Courses.FindAsync(courseDto.CourseId);
        if (course == null) return null;

        // DTO'dan mevcut modele alanları aktar
        _mapper.Map(courseDto, course);

        await _context.SaveChangesAsync();
        return _mapper.Map<CourseDto>(course); // Güncellenen Model'den DTO'ya dönüşüm
    }
}
