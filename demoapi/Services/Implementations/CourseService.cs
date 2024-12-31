using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Services.Interfaces;
using demoapi.Pagination;
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
    public async Task<PaginationDto<CourseDto>> GetAllCoursesWithPaginationAsync(int page = 1, int pageSize = 20)
    {
        return await PaginationMethod.PaginateAsync<Course, CourseDto>(
            _context.Courses.AsQueryable(), _mapper, page, pageSize
        );
    }
    /*public async Task<PaginationDto> GetAllCoursesWithPaginationAsync(int page = 1, int pageSize = 20)
    {
        var query = _context.Courses.AsQueryable();

        // Toplam kayıt sayısını al
        var totalCount = await query.CountAsync();

        // İlgili sayfanın kayıtlarını al
        var courses = await query
            .Skip((page - 1) * pageSize) // Atlanacak kayıtlar
            .Take(pageSize) // Sayfa başına gösterilecek kayıtlar
            .ToListAsync();

        // Paginated DTO döndür
        return new PaginationDto
        {
            Items = _mapper.Map<IEnumerable<CourseDto>>(courses), // DTO dönüşümü
            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = totalCount

        };
    } */

     public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
     {
         var courses = await _context.Courses.ToListAsync();
         return _mapper.Map<IEnumerable<CourseDto>>(courses); // Model'den DTO'ya dönüşüm
     }
    public async Task<IEnumerable<CourseDto>> FilterCoursesAsync(string? courseName, string? description)
    {
        var query = _context.Courses.AsQueryable();

        if (!string.IsNullOrEmpty(courseName))
        {
            query = query.Where(c => c.CourseName.Contains(courseName));
        }
        if (!string.IsNullOrEmpty(description))
        {
            query = query.Where(c => c.Description.Contains(description));
        }

        var courses = await query.ToListAsync();
        return _mapper.Map<IEnumerable<CourseDto>>(courses);
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
