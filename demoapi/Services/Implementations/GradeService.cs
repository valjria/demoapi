using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Models;
using demoapi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace demoapi.Services.Implementations
{
    public class GradeService : IGradeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GradeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginationDto<GradeDto>> GetAllGradesWithPaginationAsync(int page, int pageSize)
        {
            var query = _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .AsQueryable();

            var totalCount = await query.CountAsync();
            var grades = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationDto<GradeDto>
            {
                Items = _mapper.Map<IEnumerable<GradeDto>>(grades),
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<IEnumerable<GradeDto>> GetAllGradesAsync()
        {
            var grades = await _context.Grades.Include(g => g.Student).Include(g => g.Course).ToListAsync();
            return _mapper.Map<IEnumerable<GradeDto>>(grades);
        }
        public async Task<IEnumerable<GradeDto>> FilterGradesAsync(int? studentId, int? courseId, int? minValue, int? maxValue)
        {
            var query = _context.Grades.AsQueryable();

            if (studentId.HasValue)
            {
                query = query.Where(g => g.StudentId == studentId.Value);
            }
            if (courseId.HasValue)
            {
                query = query.Where(g => g.CourseId == courseId.Value);
            }
            if (minValue.HasValue)
            {
                query = query.Where(g => g.Value >= minValue.Value);
            }
            if (maxValue.HasValue)
            {
                query = query.Where(g => g.Value <= maxValue.Value);
            }

            var grades = await query.ToListAsync();
            return _mapper.Map<IEnumerable<GradeDto>>(grades);
        }

        public async Task<GradeDto> GetGradeByIdAsync(int id)
        {
            var grade = await _context.Grades.Include(g => g.Student).Include(g => g.Course).FirstOrDefaultAsync(g => g.GradeId == id);
            if (grade == null) return null;

            return _mapper.Map<GradeDto>(grade);
        }

        public async Task<GradeDto> AddGradeAsync(GradeDto gradeDto)
        {
            var grade = _mapper.Map<Grade>(gradeDto);
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return _mapper.Map<GradeDto>(grade);
        }

        public async Task<bool> DeleteGradeAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return false;

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GradeDto> UpdateGradeAsync(GradeDto gradeDto)
        {
            var grade = await _context.Grades.FindAsync(gradeDto.GradeId);
            if (grade == null) return null;

            _mapper.Map(gradeDto, grade);
            await _context.SaveChangesAsync();

            return _mapper.Map<GradeDto>(grade);
        }
    }
}