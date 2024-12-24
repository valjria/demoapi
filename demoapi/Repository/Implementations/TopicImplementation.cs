using demoapi.Data;
using demoapi.Models;
using demoapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace demoapi.Repositories.Implementations
{
    public class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _context;

        public TopicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<Topic> GetByIdAsync(int id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public async Task<Topic> AddAsync(Topic topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
            return topic;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
                return false;

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}