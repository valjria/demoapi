using demoapi.Data;
using demoapi.Models;
using demoapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace demoapi.Repositories.Implementations
{
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext _context;

        public GradeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grade>> GetAllAsync()
        {
            return await _context.Grades.Include(g => g.Student).Include(g => g.Course).ToListAsync();
        }

        public async Task<Grade> GetByIdAsync(int id)
        {
            return await _context.Grades.Include(g => g.Student).Include(g => g.Course).FirstOrDefaultAsync(g => g.GradeId == id);
        }

        public async Task<Grade> AddAsync(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return grade;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
                return false;

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}