using demoapi.Data;
using demoapi.DTO;
using demoapi.Repository.Interfaces;
using demoapi.Repository.Interfaces.demoapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace demoapi.Repositories.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReportDto>> GetAllAsync()
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