using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public StudentService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginationDto<StudentDto>> GetAllStudentsWithPaginationAsync(int page, int pageSize)
    {
        var query = _context.Students.AsQueryable();

        var totalCount = await query.CountAsync();
        var students = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginationDto<StudentDto>
        {
            Items = _mapper.Map<IEnumerable<StudentDto>>(students),
            CurrentPage = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
    public async Task<IEnumerable<StudentDto>> FilterStudentsAsync(string name, string role)
    {
        var query = _context.Students.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(s => s.Name.Contains(name));
        }
        if (!string.IsNullOrEmpty(role))
        {
            query = query.Where(s => s.Role == role);
        }

        var students = await query.ToListAsync();
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _context.Students.ToListAsync();
        return _mapper.Map<IEnumerable<StudentDto>>(students); // AutoMapper ile dönüşüm
    }

    public async Task<StudentDto> GetStudentByIdAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return null;

        return _mapper.Map<StudentDto>(student); // AutoMapper ile dönüşüm
    }

    public async Task<StudentDto> AddStudentAsync(StudentDto studentDto)
    {
        var student = _mapper.Map<Student>(studentDto); // DTO'dan Model'e dönüşüm
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return _mapper.Map<StudentDto>(student); // Model'den DTO'ya dönüşüm
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<StudentDto> UpdateStudentAsync(StudentDto studentDto)
    {
        var student = await _context.Students.FindAsync(studentDto.StudentId);
        if (student == null) return null;

        _mapper.Map(studentDto, student); // DTO'dan mevcut Model'e alanları aktar

        await _context.SaveChangesAsync();
        return _mapper.Map<StudentDto>(student); // Güncellenen Model'den DTO'ya dönüşüm
    }
}
