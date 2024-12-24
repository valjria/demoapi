using demoapi.Data;
using demoapi.Models;
using demoapi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace demoapi.Repository.Implementations
{
    public class CourseRepository : ICourseRepository
    {

        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Topics) // Topics dahil ediliyor
                .ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Topics)
                .FirstOrDefaultAsync(c => c.CourseId == id);
        }

        public async Task<Course> AddAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
