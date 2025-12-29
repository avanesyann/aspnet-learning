using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLWalkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _context.Walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walkDomain = await _context.Walks.FindAsync(id);

            if (walkDomain == null)
                return null;

            return walkDomain;
        }
    }
}
