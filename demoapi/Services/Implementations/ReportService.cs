using AutoMapper;
using demoapi.Data;
using demoapi.DTO;
using demoapi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace demoapi.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReportService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportDto>> GetAllReportsAsync()
        {
            var reports = from grade in _context.Grades
                          join student in _context.Students on grade.StudentId equals student.StudentId
                          join course in _context.Courses on grade.CourseId equals course.CourseId
                          select new ReportDto
                          {
                              StudentName = student.Name,
                              CourseName = course.CourseName,
                              Grade = grade.Value
                          };

            return await reports.ToListAsync();
        }
    }
}
